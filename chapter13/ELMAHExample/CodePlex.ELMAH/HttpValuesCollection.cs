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

    using NameValueCollection = System.Collections.Specialized.NameValueCollection;
    using XmlReader = System.Xml.XmlReader;
    using XmlWriter = System.Xml.XmlWriter;
    using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
    using StreamingContext = System.Runtime.Serialization.StreamingContext;

	#endregion

    /// <summary>
    /// A name-values collection implementation suitable for web-based collections 
    /// (like server variables, query strings, forms and cookies) that can also
    /// be written and read as XML.
    /// </summary>
    
    [ Serializable ]
    internal sealed class HttpValuesCollection : NameValueCollection, IXmlExportable
    {
        public HttpValuesCollection() {}        

        public HttpValuesCollection(NameValueCollection other) : 
            base(other) {}
                
        public HttpValuesCollection(int capacity) : 
            base(capacity) {}
        
        public HttpValuesCollection(int capacity, NameValueCollection other) : 
            base(capacity, other) {}
                
        private HttpValuesCollection(SerializationInfo info, StreamingContext context) : 
            base(info, context) {}

        void IXmlExportable.FromXml(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            if (this.IsReadOnly)
                throw new InvalidOperationException("Object is read-only.");

            reader.Read();

            //
            // Add entries into the collection as <item> elements
            // with child <value> elements are found.
            //

            while (reader.IsStartElement("item"))
            {
                string name = reader.GetAttribute("name");
                bool isNull = reader.IsEmptyElement; 

                reader.Read(); // <item>

                if (!isNull)
                {

                    while (reader.IsStartElement("value")) // <value ...>
                    {
                        string value = reader.GetAttribute("string");
                        Add(name, value);
                        reader.Read();
                    }

                    reader.ReadEndElement(); // </item>
                }
                else
                {
                    Add(name, null);
                }
            }

            reader.ReadEndElement();
        }

        void IXmlExportable.ToXml(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            if (this.Count == 0)
            {
                return;
            }

            //
            // Write out a named multi-value collection as follows 
            // (example here is the ServerVariables collection):
            //
            //      <item name="HTTP_URL">
            //          <value string="/myapp/somewhere/page.aspx" />
            //      </item>
            //      <item name="QUERY_STRING">
            //          <value string="a=1&amp;b=2" />
            //      </item>
            //      ...
            //

            foreach (string key in this.Keys)
            {
                writer.WriteStartElement("item");
                writer.WriteAttributeString("name", key);
                
                string[] values = GetValues(key);

                if (values != null)
                {
                    foreach (string value in values)
                    {
                        writer.WriteStartElement("value");
                        writer.WriteAttributeString("string", value);
                        writer.WriteEndElement();
                    }
                }
                
                writer.WriteEndElement();
            }
        }
    }
}
