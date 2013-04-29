using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Web;
using System.Web.Services;

namespace chapter_12_rendering_binary_content
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://sitepoint.com/examples/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class TextRenderer : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            
            string text = request.QueryString["Text"] ?? "";
            if (text.Length == 0)
                text = "Nothing To Render!";

            int fontSize;
            if (!int.TryParse(request.QueryString["fontsize"], out fontSize))
                fontSize = 30;

            Font font = GetFont(fontSize);
            SizeF textSize = MeasureString(text, font);

            Rectangle layoutRectangle = new Rectangle(0, 0, (int)textSize.Width + 10, (int)textSize.Height);

            using (Bitmap image = new Bitmap((int)textSize.Width, (int)textSize.Height, PixelFormat.Format32bppArgb))
            using(Graphics g = Graphics.FromImage(image))
            {
                g.Clear(GetBackgroundColor(request));
                
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                SolidBrush brush = new SolidBrush(GetForegroundColor(request));

                g.DrawString(text, font, brush, layoutRectangle);

                context.Response.ContentType = "image/gif";
                image.Save(context.Response.OutputStream, ImageFormat.Gif);
            }
            context.Response.End();
        }

        static SizeF MeasureString(string text, Font font)
        {
            //Use a dummy bitmap to measure the string.
            using (Bitmap image = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(image))
            {
                return g.MeasureString(text, font);
            }
        }

        static Font GetFont(int fontSize)
        {
            FontFamily fontFamily = new FontFamily("Arial");
            return new Font(fontFamily, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        static Color GetBackgroundColor(HttpRequest request)
        {
            string colorName = request.QueryString["bgcolor"];
            return GetColorFromText(colorName, Color.White);
        }

        static Color GetForegroundColor(HttpRequest request)
        {
            string colorName = request.QueryString["color"];
            return GetColorFromText(colorName, Color.Black);
        }

        private static Color GetColorFromText(string colorName, Color defaultColor)
        {
            if (string.IsNullOrEmpty(colorName))
                return defaultColor;

            if (colorName.IndexOf(',') < 0)
            {
                bool ignoreCase = true;
                try
                {
                    KnownColor knownColor = (KnownColor) Enum.Parse(typeof (KnownColor), colorName, ignoreCase);
                    return Color.FromKnownColor(knownColor);
                }
                catch(ArgumentException)
                {
                    //ignore.
                }

                if (colorName.Length == 6)
                {
                    int redHex = GetIntFromString(colorName.Substring(0, 2), 255, NumberStyles.AllowHexSpecifier);
                    int greenHex = GetIntFromString(colorName.Substring(2, 2), 255, NumberStyles.AllowHexSpecifier);
                    int blueHex = GetIntFromString(colorName.Substring(4, 2), 255, NumberStyles.AllowHexSpecifier);

                    return Color.FromArgb(255, redHex, greenHex, blueHex);
                }
            }
            else
            {
                string[] argb = colorName.Split(',');
                if (argb.Length == 4)
                {
                    int alpha = GetIntFromString(argb[0], 255);
                    int red = GetIntFromString(argb[1], 255);
                    int green = GetIntFromString(argb[2], 255);
                    int blue = GetIntFromString(argb[3], 255);
                    return Color.FromArgb(alpha, red, green, blue);
                }
            }
            return defaultColor;
        }

        static int GetIntFromString(string s, int defaultValue, NumberStyles styles)
        {
            int parsedInt;
            if (!int.TryParse(s, styles, CultureInfo.InvariantCulture, out parsedInt))
                return defaultValue;

            return parsedInt;
        }

        static int GetIntFromString(string s, int defaultValue)
        {
            return GetIntFromString(s, defaultValue, NumberStyles.Any);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
