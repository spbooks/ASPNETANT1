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
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using Assembly = System.Reflection.Assembly;
    using HttpUtility = System.Web.HttpUtility;
    using FileVersionInfo = System.Diagnostics.FileVersionInfo;
    using Cache = System.Web.Caching.Cache;
    using CacheItemPriority = System.Web.Caching.CacheItemPriority;
    using HttpRuntime = System.Web.HttpRuntime;

	#endregion

    /// <summary>
    /// Displays a "Powered-by ELMAH" message that also contains the assembly
    /// file version informatin and copyright notice.
    /// </summary>

    public sealed class PoweredBy : WebControl
    {
        private FileVersionInfo _versionInfo;

        /// <summary>
        /// Renders the contents of the control into the specified writer.
        /// </summary>

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            //
            // Write out the assembly title, version number and copyright.
            //

            writer.Write("Powered by ");
            HttpUtility.HtmlEncode(this.VersionInfo.ProductName, writer);
            writer.Write(", version ");
            HttpUtility.HtmlEncode(this.VersionInfo.FileVersion, writer);
            writer.Write(". ");
            HttpUtility.HtmlEncode(this.VersionInfo.LegalCopyright, writer);
            writer.Write(' ');
        }

        private FileVersionInfo VersionInfo
        {
            get
            {
                string cacheKey = GetType().Name;

                //
                // If cache is available then check if the version 
                // information is already residing in there.
                //

                if (this.Cache != null)
                {
                    _versionInfo = (FileVersionInfo) this.Cache[cacheKey];
                }

                //
                // Not found in the cache? Go out and get the version 
                // information of the assembly housing this component.
                //
                
                if (_versionInfo == null)
                {
                    Assembly thisAssembly = this.GetType().Assembly;
                    _versionInfo = FileVersionInfo.GetVersionInfo(thisAssembly.Location);

                    //
                    // Cache for next time if the cache is available.
                    //

                    if (this.Cache != null)
                    {
                        this.Cache.Add(cacheKey, _versionInfo, 
                            null, Cache.NoAbsoluteExpiration,
                            TimeSpan.FromMinutes(2), CacheItemPriority.Normal, null);
                    }
                }

                return _versionInfo;
            }
        }

        private Cache Cache
        {
            get
            {
                //
                // Get the cache from the container page, or failing that, 
                // from the runtime. The Page property can be null
                // if the control has not been added to a page's controls
                // hierarchy.
                //

                if (this.Page != null)
                {
                    return this.Page.Cache;
                }
                else
                {
                    return HttpRuntime.Cache;
                }
            }
        }
    }
}
