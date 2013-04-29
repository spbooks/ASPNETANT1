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

	#endregion

    /// <summary>
    /// Binds an <see cref="Error"/> instance with the <see cref="ErrorLog"/>
    /// instance from where it was served.
    /// </summary>
    
    [ Serializable ]
	public class ErrorLogEntry
	{
        private readonly string _id;
        private readonly ErrorLog _log;
        private readonly Error _error;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLogEntry"/> class
        /// for a given unique error entry in an error log.
        /// </summary>
     
        public ErrorLogEntry(ErrorLog log, string id, Error error)
        {
            if (log == null)
                throw new ArgumentNullException("log");

            if (id == null)
                throw new ArgumentNullException("id");

            if (id.Length == 0)
                throw new ArgumentOutOfRangeException("id");

            if (error == null)
                throw new ArgumentNullException("error");
            
            _log = log;
            _id = id;
            _error = error;
        }

        /// <summary>
        /// Gets the <see cref="ErrorLog"/> instance where this entry 
        /// originated from.
        /// </summary>
   
        public ErrorLog Log
        {
            get { return _log; }
        }
        
        /// <summary>
        /// Gets the unique identifier that identifies the error entry 
        /// in the log.
        /// </summary>
        
        public string Id
        {
            get { return _id; }
        }

        /// <summary>
        /// Gets the <see cref="Error"/> object held in the entry.
        /// </summary>

        public Error Error
        {
            get { return _error; }
        }
    }
}
