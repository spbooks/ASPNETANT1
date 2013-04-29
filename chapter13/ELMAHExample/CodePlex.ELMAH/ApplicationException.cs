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

    using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
    using StreamingContext = System.Runtime.Serialization.StreamingContext;

    #endregion

    /// <summary>
    /// The exception that is thrown when a non-fatal error occurs. 
    /// This exception also serves as the base for all exceptions thrown by
    /// this library.
    /// </summary>

    [ Serializable ]
    public class ApplicationException : System.ApplicationException
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class.
        /// </summary>
        
        public ApplicationException() {}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class 
        /// with a specified error message.
        /// </summary>

        public ApplicationException(string message) : 
            base(message) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> 
        /// class with a specified error message and a reference to the 
        /// inner exception that is the cause of this exception.
        /// </summary>

        public ApplicationException(string message, Exception innerException) : 
            base(message, innerException) {}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationException"/> class 
        /// with serialized data.
        /// </summary>

        protected ApplicationException(SerializationInfo info, StreamingContext context) : 
            base(info, context) {}
    }
}

