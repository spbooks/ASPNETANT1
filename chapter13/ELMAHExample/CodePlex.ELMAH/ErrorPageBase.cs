#region Byline & Disclaimer
//
//  Author(s):
//
//      Atif Aziz (atif.aziz@skybow.com, http://www.raboof.com)
//      Andrew Cain (mistercain@gmail.com, http://dotnettricks.com/blogs/andrewcainblog/default.aspx)
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
#endregion

namespace CodePlex.Elmah
{
	#region Imports

	using System;
    using System.Web.UI;

    using CultureInfo = System.Globalization.CultureInfo;
    
	#endregion

    /// <summary>
    /// Provides the base implementation and layout for most pages that render 
    /// HTML for the error log.
    /// </summary>

    internal abstract class ErrorPageBase : Page
    {
        private string _title;

        protected string BasePageName
        {
            get { return this.Request.ServerVariables["URL"]; }
        }

        protected virtual ErrorLog ErrorLog
        {
            get { return ErrorLog.Default; }
        }

        protected new virtual string Title
        {
            get { return StringEtc.MaskNull(_title); }
            set { _title = value; }
        }

        protected virtual string ApplicationName
        {
            get { return this.ErrorLog.ApplicationName; }
        }

        protected virtual void RenderDocumentStart(HtmlTextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            writer.RenderBeginTag(HtmlTextWriterTag.Html);  // <html>
            
            writer.RenderBeginTag(HtmlTextWriterTag.Head);  // <head>
            RenderHead(writer);
            writer.RenderEndTag();                          // </head>
            writer.WriteLine();

            writer.RenderBeginTag(HtmlTextWriterTag.Body);  // <body>
        }

        protected virtual void RenderHead(HtmlTextWriter writer)
        {
            //
            // Write the document title.
            //

            writer.RenderBeginTag(HtmlTextWriterTag.Title);
            Server.HtmlEncode(this.Title, writer);
            writer.RenderEndTag();
            writer.WriteLine();

            //
            // Write a <link> tag to relate the style sheet.
            //

            writer.AddAttribute("rel", "stylesheet");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/css");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.BasePageName + "/stylesheet");
            writer.RenderBeginTag(HtmlTextWriterTag.Link);
            writer.RenderEndTag();
            writer.WriteLine();
        }

        protected virtual void RenderDocumentEnd(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "Footer");
            writer.RenderBeginTag(HtmlTextWriterTag.P); // <p>

            //
            // Write the powered-by signature, that includes version information.
            //

            PoweredBy poweredBy = new PoweredBy();
            poweredBy.RenderControl(writer);

            //
            // Write out server date, time and time zone details.
            //

            DateTime now = DateTime.Now;

            writer.Write("Server date is ");
            this.Server.HtmlEncode(now.ToString("D", CultureInfo.InvariantCulture), writer);

            writer.Write(". Server time is ");
            this.Server.HtmlEncode(now.ToString("T", CultureInfo.InvariantCulture), writer);

            writer.Write(". All dates and times displayed are in the ");
            writer.Write(TimeZone.CurrentTimeZone.IsDaylightSavingTime(now) ?
                TimeZone.CurrentTimeZone.DaylightName : TimeZone.CurrentTimeZone.StandardName);
            writer.Write(" zone. ");

            //
            // Write out the source of the log.
            //

            writer.Write("This log is provided by the ");
            this.Server.HtmlEncode(this.ErrorLog.Name, writer);
            writer.Write('.');

            writer.RenderEndTag(); // </p>

            writer.RenderEndTag(); // </body>
            writer.WriteLine();

            writer.RenderEndTag(); // </html>
            writer.WriteLine();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            RenderDocumentStart(writer);
            RenderContents(writer);
            RenderDocumentEnd(writer);
        }

        protected virtual void RenderContents(HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}
