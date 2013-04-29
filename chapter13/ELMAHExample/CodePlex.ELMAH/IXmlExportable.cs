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

    using XmlWriter = System.Xml.XmlWriter;
    using XmlReader = System.Xml.XmlReader;

	#endregion

    /// <summary>
    /// Defines methods to convert an object to and from its XML representation.
    /// </summary>

    public interface IXmlExportable
	{
        /// <summary>
        /// Reads the object state from its XML representation.
        /// </summary>

        void FromXml(XmlReader reader);

        /// <summary>
        /// Writes the XML representation of the object.
        /// </summary>

        void ToXml(XmlWriter writer);
	}
}
