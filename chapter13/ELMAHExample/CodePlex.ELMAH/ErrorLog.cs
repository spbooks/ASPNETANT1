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

    using HttpContext = System.Web.HttpContext;
    using IList = System.Collections.IList;
    using HttpRuntime = System.Web.HttpRuntime;

    #endregion

    /// <summary>
    /// Represents an error log capable of storing and retrieving errors
    /// generated in an ASP.NET Web application.
    /// </summary>

    public abstract class ErrorLog
    {
        [ ThreadStatic ] private static ErrorLog _defaultLog;

        /// <summary>
        /// Logs an error in log for the application.
        /// </summary>
        
        public abstract void Log(Error error);

        /// <summary>
        /// Retrieves a single application error from log given its 
        /// identifier, or null if it does not exist.
        /// </summary>

        public abstract ErrorLogEntry GetError(string id);
        
        /// <summary>
        /// Retrieves a page of application errors from the log in 
        /// descending order of logged time.
        /// </summary>
        
        public abstract int GetErrors(int pageIndex, int pageSize, IList errorEntryList);

        /// <summary>
        /// Get the name of this log.
        /// </summary>

        public virtual string Name
        {
            get { return this.GetType().Name; }   
        }

        /// <summary>
        /// Gets the name of the application to which the log is scoped.
        /// </summary>
        
        public virtual string ApplicationName
        {
            get { return HttpRuntime.AppDomainAppId; }   
        }

        /// <summary>
        /// Gets the default error log implementation specified in the 
        /// configuration file, or the in-memory log implemention if
        /// none is configured.
        /// </summary>
        
        public static ErrorLog Default
        {
            get 
            { 
                if (_defaultLog == null)
                {
                    //
                    // Determine the default store type from the configuration and 
                    // create an instance of it.
                    //

                    ErrorLog log = (ErrorLog) SimpleServiceProviderFactory.CreateFromConfigSection("CodePlex.elmah/errorLog");

                    //
                    // If no object got created (probably because the right 
                    // configuration settings are missing) then default to 
                    // the in-memory log implementation.
                    //

                    _defaultLog = log != null ? log : new MemoryErrorLog();
                }

                return _defaultLog;
            }
        }
    }
}
