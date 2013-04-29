using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.Services;

namespace chapter_12_rendering_binary_content
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ExcelHandler : IHttpHandler
    {
        int GetSizeFromQueryString(HttpContext context, string key)
        {
          string intText = context.Request.QueryString[key];
          int parsedInt;

          if (int.TryParse(intText, out parsedInt) && parsedInt > -1)
            return parsedInt;

          return 1;
        }

        public void ProcessRequest(HttpContext context)
        {
            int width = GetSizeFromQueryString(context, "width");
            int height = 20; // hard-code for now
            using(Bitmap graph = new Bitmap(width, height))
            using(Graphics g = Graphics.FromImage(graph))
            {
                g.Clear(Color.Blue);

                context.Response.ContentType = "image/gif";
                graph.Save(context.Response.OutputStream, ImageFormat.Gif);
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
