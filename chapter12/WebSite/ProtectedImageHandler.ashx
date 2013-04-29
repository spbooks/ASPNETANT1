<%@ WebHandler Language="C#" Class="ProtectedImageHandler" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;

public class ProtectedImageHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string imagePath = context.Request.QueryString["image"];

        bool hotlinked = true;
        long key;
        
        //A key is only good for 30 seconds after it is issued
        if (long.TryParse(context.Request.QueryString["key"], out key))
            hotlinked = !HotlinkProtection.IsKeyValid(key, 30);
        else
            hotlinked = true;

        //Check if the image is already cached to 
        //avoid extra graphic processing
        Image cachedImage = 
            context.Cache[GetCacheKey(imagePath,hotlinked)] as Image;
        if (cachedImage != null)
        {
            //We have a valid cached image, so  just write it and return
            context.Response.ContentType = "image/jpeg";
            cachedImage.Save(
                context.Response.OutputStream, 
                ImageFormat.Jpeg);
            return;  
        }

        Image image = null; 
        Graphics graphics = null;

        try 
	    {	
            string watermark =
                "Copyright " + GetCopyrightYear(
                    image, 
                    DateTime.Now.Year.ToString()
                );
            string sitename = "www.mysite.com";

            image = ConvertFromIndexed(
                Image.FromFile(context.Server.MapPath(imagePath))
                );
            graphics = Graphics.FromImage(image);

            //Pick an appropriate font size depending on image size
            int fontsize = 16;
            if (image.Width > 400) fontsize = 24;

            //Set up the font
            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            Font font = new Font("Verdana", fontsize,
                           System.Drawing.FontStyle.Bold,
                           System.Drawing.GraphicsUnit.Pixel);

            if (hotlinked)
            {
                WriteHotlinkMessage(context, image, sitename, graphics, font);
            }

            WriteWatermark(image, watermark, graphics, font);
            
            //Can check for different original filetype like this:
            //if(image.RawFormat.Equals(ImageFormat.Gif)
            //If so, need to deal with gif quantization, png streaming 
            //issues,etc.
            
            //Removed due to hotlink timeout check - we'll manage cache 
            //on server.
            //context.Response.Cache.SetCacheability(
            //    HttpCacheability.Public);

            //Add image to cache. Must clone image since it will be disposed
            context.Cache.Insert(
                GetCacheKey(imagePath,hotlinked), 
                image.Clone());

            //PNG's generally produce the best quality images,
            //but it takes a little more work to write them
            //since they require a seekable stream.
            using (Bitmap bitmapCopy = new Bitmap(image))
            {
                System.IO.MemoryStream memoryStream =
                    new System.IO.MemoryStream();
                context.Response.ContentType = "image/png";
                bitmapCopy.Save(
                    memoryStream,
                    System.Drawing.Imaging.ImageFormat.Png);
                memoryStream.WriteTo(context.Response.OutputStream);
            }
            context.Response.End();

            //JPEG's are easier to render, but the default encoding produces
            //lower quality images.
            
            //ImageCodecInfo[] codecs=ImageCodecInfo.GetImageEncoders();
            //ImageCodecInfo jpegFormat = Array.Find(codecs, 
            //    delegate(ImageCodecInfo ic) {return ic.MimeType == "image/jpeg";});
            //EncoderParameters ep=new EncoderParameters();
            //ep.Param[0]=new EncoderParameter(
            //    System.Drawing.Imaging.Encoder.Quality,90L);
            //context.Response.ContentType = "image/jpeg";
            //image.Save(context.Response.OutputStream,jpegFormat,ep);
	    }
	    finally
	    {
            if(graphics!=null)
                graphics.Dispose();
            if(image!=null)
                image.Dispose();
        }
    }

    private static void WriteWatermark(Image image, string watermark, Graphics g, Font font)
    {
        //Determine size of watermark to write background
        SizeF watermarkSize = g.MeasureString(watermark, font);

        int xPosition = 5;
        int yPosition = image.Height - (int)watermarkSize.Height - 10;

        //Draw a translucent (alpha = 100) background for watermark
        g.FillRectangle(
            new SolidBrush(Color.FromArgb(100, Color.GhostWhite)),
            new Rectangle(
                xPosition,
                yPosition,
                (int)watermarkSize.Width,
                (int)watermarkSize.Height));

        //Write watermark
        g.DrawString(watermark,
            font,
            new SolidBrush(Color.Blue),
            xPosition,
            yPosition);
    }

    private static void WriteHotlinkMessage(HttpContext context, Image image, string sitename, Graphics g, Font font)
    {
        //If hotlinked, draw hatched overlay
        g.FillRectangle(
            new HatchBrush(
                HatchStyle.LargeConfetti,
                Color.FromArgb(90, Color.Blue)),
            new Rectangle(0, 0, image.Width, image.Height));

        //Write our site name in the center of the image
        SizeF siteSize = g.MeasureString(sitename, font);
        g.DrawString(sitename,
            font,
            new SolidBrush(Color.White),
            (image.Width - siteSize.Width) / 2,
            (image.Height - siteSize.Height) / 2);
        context.Response.Cache.SetCacheability(
            HttpCacheability.Public
            );
    }

    private string GetCacheKey(string imagePath, bool hotlinked)
    {
        string cacheKey = imagePath;
        if (hotlinked) cacheKey += "_linked";
        return cacheKey;
    }


    /// <summary>
    /// "A Graphics object cannot be created from an 
    /// image that has an indexed pixel format."
    /// </summary>
    /// <param name="image"></param>
    /// <returns></returns>
    private Image ConvertFromIndexed(Image image)
    {
        switch (image.PixelFormat)
        {
            case PixelFormat.Format1bppIndexed:
            case PixelFormat.Format4bppIndexed:
            case PixelFormat.Format8bppIndexed:
            case PixelFormat.Format16bppArgb1555:
            case PixelFormat.Format16bppGrayScale:
                //The image is in an indexed pixel format
                //Convert by copying to a new image.
                Bitmap newImage = new Bitmap(
                    image.Width,
                    image.Height,
                    PixelFormat.Format32bppArgb);
                newImage.SetResolution(
                    image.HorizontalResolution,
                    image.VerticalResolution);
                Graphics g = Graphics.FromImage(newImage);
                g.DrawImageUnscaled(image, 0, 0);
                g.Dispose();
                return newImage;
            default:
                return image;
        }
    }
    
    /// <summary>
    /// Simple EXIF property reading demonstration
    /// </summary>
    /// <param name="image">Image to read</param>
    /// <param name="defaultValue">Default if no year found</param>
    /// <returns>Copyright year of image</returns>
    private string GetCopyrightYear(Image image, string defaultValue)
    {
        string value;
        //First try to read copyright (Exif Value 33432)
        if (GetExifString(image, ExifValues.Copyright, out value))
        {
            return GetYear(value,value);
        }
        //Now try Date Taken (Exif Value 36867)
        if (GetExifString(image, ExifValues.DateTimeOriginal, out value))
        {
            string datepart = value.Split(' ')[0];
            datepart = datepart.Replace(':','/');

            return GetYear(datepart, value);
        }
        return defaultValue;
    }

    /// <summary>
    /// Format year if available
    /// </summary>
    /// <param name="dateString">Date in string format</param>
    /// <param name="value">Default value</param>
    /// <returns></returns>
    private string GetYear(string dateString, string value)
    {
        DateTime date;
        if (DateTime.TryParse(dateString, out date))
            return date.Year.ToString();
        else
            return value;
    }

    /// <summary>
    /// Wrapper to read an EXIF value
    /// </summary>
    /// <param name="image">Image to read</param>
    /// <param name="id">ID of EXIF parameter</param>
    /// <param name="value">Value if read</param>
    /// <returns>True if property was read and was not blank</returns>
    private bool GetExifString(
        Image image, ExifValues exifProperty, out string value)
    {
        value = null;
        try
        {
            PropertyItem propertyItem = 
                image.GetPropertyItem((int)exifProperty);
            if (propertyItem != null)
            {
                value = Encoding.UTF8.GetString(propertyItem.Value)
                    .Trim() as string;
            }
        }
        catch { }
        return !string.IsNullOrEmpty(value);
    }
    
    /// <summary>
    /// Not maintaining state
    /// </summary>
    public bool IsReusable
    {
        get { return true; }
    }
    
    private enum ExifValues
    {
        ImageDescription = 270,
        Make = 271,
        Model = 272,
        Orientation = 274,
        XResolution = 282,
        YResolution = 283,
        ResolutionUnit = 296,
        Software = 305,
        DateTime = 306,
        WhitePoint = 318,
        PrimaryChromaticities = 319,
        YCbCrCoefficients = 529,
        YCbCrPositioning = 531,
        ReferenceBlackWhite = 532,
        Copyright = 33432,
        ExifOffset = 34665,
        Exposuretime = 33434,
        FNumber = 33437,
        ExposureProgram = 34850,
        ISOSpeedRatings = 34855,
        ExifVersion = 36864,
        DateTimeOriginal = 36867,
        DateTimeDigitized = 36868,
        ComponentsConfiguration = 37121,
        CompressedBitsPerPixel = 37122,
        ShutterSpeedValue = 37377,
        ApertureValue = 37378,
        BrightnessValue = 37379,
        ExposureBiasValue = 37380,
        MaxApertureValue = 37381,
        SubjectDistance = 37382,
        MeteringMode = 37383,
        LightSource = 37384,
        Flash = 37385,
        FocalLength = 37386,
        MakerNote = 37500,
        UserComment = 37510,
        SubsecTime = 37520,
        SubsecTimeOriginal = 37521,
        SubsecTimeDigitized = 37522,
        FlashPixVersion = 40960,
        ColorSpace = 40961,
        ExifImageWidth = 40962,
        ExifImageHeight = 40963,
        RelatedSoundFile = 40964,
        ExifInteroperabilityOffset = 40965,
        FocalPlaneXResolution = 41486,
        FocalPlaneYResolution = 41487,
        FocalPlaneResolutionUnit = 41488,
        ExposureIndex = 41493,
        SensingMethod = 41495,
        FileSource = 41728,
        SceneType = 41729,
        CFAPattern = 41730
    }
}

public class HotlinkProtection
{
    //Site specific multiplier - might want to keep in web.config
    private const long multiplier = 298467;

    public static long GetKey()
    {
        int minutes = GetSecondCount();
        return (multiplier * minutes);
    }

    private static int GetSecondCount()
    {
        TimeSpan span = DateTime.Now - new DateTime(2005, 1, 1);
        return (int)span.TotalSeconds;
    }

    public static bool IsKeyValid(long key, int timeoutSeconds)
    {
        try
        {
            int seconds = (int)(key / multiplier);
            int difference = Math.Abs(GetSecondCount() - seconds);
            return (difference < timeoutSeconds);
        }
        catch
        {
            return false;
        }
    }
}