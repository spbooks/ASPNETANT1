#region Byline & Disclaimer
//
//  Author(s):
//
//      Atif Aziz (atif.aziz@skybow.com, http://www.raboof.com)
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

	#endregion

    /// <summary>
    /// Renders an HTML page displaying the detailed host-generated (ASP.NET)
    /// HTML recorded for an error from the error log.
    /// </summary>
	
    internal sealed class ErrorHtmlPage : ErrorPageBase
	{
        protected override void Render(HtmlTextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            //
            // Retrieve the ID of the error to display and read it from 
            // the log.
            //

            string errorId = StringEtc.MaskNull(this.Request.QueryString["id"]);

            if (errorId.Length == 0)
                return;

            ErrorLogEntry errorEntry = this.ErrorLog.GetError(errorId);

            if (errorEntry == null)
                return;

            //
            // If we have a host (ASP.NET) formatted HTML message 
            // for the error then just stream it out as our response.
            //

            if (errorEntry.Error.WebHostHtmlMessage.Length != 0)
            {
                writer.Write(errorEntry.Error.WebHostHtmlMessage);
            }
        }
	}
}
