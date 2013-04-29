#region Byline & Disclaimer
//
//  Author(s):
//
//      Atif Aziz (atif.aziz@skybow.com, http://www.raboof.com)
//      Andrew Cain (mistercain@gmail.com, http://dotnettricks.com/blogs/andrewcainblog/default.aspx)
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
    using System.Configuration;
    using System.Collections;

	#endregion

    /// <summary>
    /// A simple factory for creating instances of types specified in a 
    /// section of the configuration file.
    /// </summary>
	
    internal sealed class SimpleServiceProviderFactory
	{
        public static object CreateFromConfigSection(string sectionName)
        {
            Debug.AssertStringNotEmpty(sectionName);

            //
            // Get the configuration section with the settings.
            //
            
            IDictionary config = (IDictionary) ConfigurationManager.GetSection(sectionName);

            if (config == null)
            {
                return null;
            }

            //
            // We modify the settings by removing items as we consume 
            // them so make a copy here.
            //

            config = (IDictionary) ((ICloneable) config).Clone();

            //
            // Get the type specification of the service provider.
            //

            string typeSpec = StringEtc.MaskNull((string) config["type"]);
            
            if (typeSpec.Length == 0)
            {
                return null;
            }

            config.Remove("type");

            //
            // Locate, create and return the service provider object.
            //

            Type type = Type.GetType(typeSpec, true);
            return Activator.CreateInstance(type, new object[] { config });
        }

        private SimpleServiceProviderFactory() {}
	}
}
