using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

namespace MoveViewState
{
    public class MoveViewStateFilter : System.IO.MemoryStream
    {
        System.IO.Stream _filter;
        readonly Encoding _encoding = Encoding.UTF8;
        bool _filtered = false;

        public MoveViewStateFilter(System.IO.Stream filter)
        {
            _filter = filter;
        }

        public override void Close()
        {
            if (!_filtered)
            {
                base.Close();
                return;
            }
            if (this.Length == 0)
            {
                _filter.Close();
                base.Close();
                return;
            }

            byte[] bytes;
            string html = _encoding.GetString(this.ToArray());

            int ViewStateStart = html.IndexOf("<input type=\"hidden\" name=\"__VIEWSTATE\"");
            if (ViewStateStart <= 0)
            {
                bytes = this.ToArray();
            }
            else
            {
                System.IO.StringWriter writer = new System.IO.StringWriter();

                // write the section of html before viewstate
                writer.Write(html.Substring(1, ViewStateStart - 1));

                int ViewStateEnd = html.IndexOf("/>", ViewStateStart) + 2;
                int FormEndStart = html.IndexOf("</form>");
                // write the section after the viewstate and up to the end of the FORM
                writer.Write(html.Substring(ViewStateEnd, html.Length - ViewStateEnd - (html.Length - FormEndStart)));
                // write the viewstate itself
                writer.Write(html.Substring(ViewStateStart, ViewStateEnd - ViewStateStart));
                // now write the FORM footer
                writer.Write(html.Substring(FormEndStart));

                bytes = _encoding.GetBytes(writer.ToString());
            }
            _filter.Write(bytes, 0, bytes.Length);
            _filter.Close();
            base.Close();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // only do this for text/html responses
            if (HttpContext.Current.Response.ContentType == "text/html")
            {
                base.Write(buffer, offset, count);
                _filtered = true;
            }
            else
            {
                _filter.Write(buffer, offset, count);
                _filtered = false;
            }
        }
    }
}