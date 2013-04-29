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

    using TextWriter = System.IO.TextWriter;
    
    #endregion

    /// <summary>
    /// Provides the base contract for implementations that render
    /// text-based formatting for an error.
    /// </summary>
    
    public abstract class ErrorTextFormatter
    {
        /// <summary>
        /// Gets the MIME type of the text format provided by the formatter
        /// implementation.
        /// </summary>
        
        public abstract string MimeType { get; }

        /// <summary>
        /// Formats a text representation of the given <see cref="Error"/> 
        /// instance using a <see cref="TextWriter"/>.
        /// </summary>

        public abstract void Format(TextWriter writer, Error error);
    }
}
