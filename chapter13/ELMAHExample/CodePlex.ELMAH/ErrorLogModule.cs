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
    
    using Trace = System.Diagnostics.Trace;

    #endregion

    /// <summary>
    /// HTTP module implementation that logs unhandled exceptions in an
    /// ASP.NET Web application to an error log.
    /// </summary>
    
    public class ErrorLogModule : IHttpModule
    {

        /// <summary>
        /// Initializes the module and prepares it to handle requests.
        /// </summary>

        public virtual void Init(HttpApplication application)
        {
            if (application == null)
                throw new ArgumentNullException("application");

            application.Error += new EventHandler(OnError);
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module.
        /// </summary>
        
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Gets the <see cref="ErrorLog"/> instance to which the module
        /// will log exceptions.
        /// </summary>
        
        protected virtual ErrorLog ErrorLog
        {
            get { return ErrorLog.Default; }
        }

        /// <summary>
        /// The handler called when an unhandled exception bubbles up to 
        /// the module.
        /// </summary>

        protected virtual void OnError(object sender, EventArgs args)
        {
            HttpApplication application = (HttpApplication) sender;
            LogException(application.Server.GetLastError(), application.Context);
        }

        /// <summary>
        /// Logs an exception and its context to the error log.
        /// </summary>

        protected virtual void LogException(Exception e, HttpContext context)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            try
            {
                this.ErrorLog.Log(new Error(e, context));
            }
            catch (Exception localException)
            {
                //
                // IMPORTANT! We swallow any exception raised during the 
                // logging and send them out to the trace . The idea 
                // here is that logging of exceptions by itself should not 
                // be  critical to the overall operation of the application.
                // The bad thing is that we catch ANY kind of exception, 
                // even system ones and potentially let them slip by.
                //

                Trace.WriteLine(localException);
            }
        }
    }
}
