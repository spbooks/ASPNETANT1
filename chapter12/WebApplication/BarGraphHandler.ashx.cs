using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace chapter_12_rendering_binary_content
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class BarGraphHandler : IHttpHandler
{
    int GetSizeFromQueryString(HttpContext context, string key)
    {
        string intText = context.Request.QueryString[key];
        int parsedInt;

        if (int.TryParse(intText, out parsedInt) && parsedInt > -1)
            return parsedInt;

        return 1;
    }

    int[] GetDataPoints(HttpContext context)
    {
        string data = context.Request.QueryString["datapoints"];
        if (String.IsNullOrEmpty(data))
            return new int[] {};

        string[] dataPoints = data.Split(',');
        //Could throw an exception
        return Array.ConvertAll(dataPoints, new Converter<string, int>(int.Parse));
    }

public void ProcessRequest(HttpContext context)
{
  int width = GetSizeFromQueryString(context, "width");
  int scale = GetSizeFromQueryString(context, "scale");
  int[] dataPoints = GetDataPoints(context);
  //These values hard-coded for now.
  int barHeight = 20; //height of an individual bar.
  int padding = 4; //Padding between bars.

  scale = AdjustScaleToLargestDatapoint(dataPoints, scale);

  //Get the height.
  int height = (barHeight + padding)*dataPoints.Length + padding;
    
  //Create the bitmap using the scale, later 
  //we'll scale it down to the requested width.
  using (Bitmap graph = new Bitmap(width, height))
  using (Graphics g = Graphics.FromImage(graph))
  {
    g.Clear(Color.White);

      //Draw a border.
    g.DrawRectangle(new Pen(Color.Black), 0, 0, graph.Width - 1, graph.Height - 1);
      
    ScaleGraphToImageWidth(scale, graph, g);

    DrawBars(barHeight, dataPoints, g, padding);

    context.Response.ContentType = "image/gif";
    graph.Save(context.Response.OutputStream, ImageFormat.Gif);
  }
}

private void ScaleGraphToImageWidth(int scale, Bitmap graph, Graphics g)
{
  float scaling = graph.Width / (float)scale;
  g.ScaleTransform(scaling, 1);
}

private static int AdjustScaleToLargestDatapoint(int[] dataPoints, int scale)
{
  foreach(int dataPoint in dataPoints)
  {
      scale = Math.Max(dataPoint, scale);
  }
  return scale;
}

private static void DrawBars(int barHeight, int[] dataPoints, Graphics g, int padding)
{
  int y = padding;
  foreach(int dataPoint in dataPoints)
  {
    Brush brush = new SolidBrush(Color.Blue);
    Brush shadow = new SolidBrush(Color.Black);
    g.FillRectangle(shadow, 0, y + 1, dataPoint + 2, barHeight);
    g.FillRectangle(brush, 0, y, dataPoint, barHeight);
    y = y + barHeight + padding;
  }
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
