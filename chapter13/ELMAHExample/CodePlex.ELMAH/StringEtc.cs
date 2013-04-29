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
    /// Collection of utility methods that complement <see cref="String"/>.
    /// </summary>
    
    internal sealed class StringEtc
    {
        public static string MaskNull(string s)
        {
            return s == null ? string.Empty : s;
        }

        private StringEtc() {}
    }
}
