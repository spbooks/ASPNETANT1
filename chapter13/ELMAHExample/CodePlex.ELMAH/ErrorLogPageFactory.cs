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
    using System.Web;

    using CultureInfo = System.Globalization.CultureInfo;
    using Encoding = System.Text.Encoding;

	#endregion

    /// <summary>
    /// HTTP handler factory that dispenses handlers for rendering views and 
    /// resources needed to display the error log.
    /// </summary>

    public class ErrorLogPageFactory : IHttpHandlerFactory
    {
        /// <summary>
        /// Returns an object that implements the <see cref="IHttpHandler"/> 
        /// interface and which is responsible for serving the request.
        /// </summary>
        /// <returns>
        /// A new <see cref="IHttpHandler"/> object that processes the request.
        /// </returns>

        public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            //
            // The request resource is determined by the looking up the
            // value of the PATH_INFO server variable.
            //

            string resource = context.Request.PathInfo.Length == 0 ? string.Empty :
                context.Request.PathInfo.Substring(1);

            switch (resource.ToLower(CultureInfo.InvariantCulture))
            {
                case "detail" :
                    
                    return new ErrorDetailPage();

                case "html" :
                    
                    return new ErrorHtmlPage();

                case "rss" :
                    
                    return new ErrorRssHandler();

                case "stylesheet" :

                    return new ManifestResourceHandler("ErrorLog.css", 
                        "text/css", Encoding.GetEncoding("Windows-1252"));

                case "test" :
                    
                    throw new TestException();

                default :
                {
                    if (resource.Length == 0)
                    {
                        return new ErrorLogPage();
                    }
                    else
                    {
                        throw new HttpException(404, "Resource not found.");
                    }
                }
            }
        }

        /// <summary>
        /// Enables the factory to reuse an existing handler instance.
        /// </summary>
        
        public virtual void ReleaseHandler(IHttpHandler handler)
        {
        }
    }
}
