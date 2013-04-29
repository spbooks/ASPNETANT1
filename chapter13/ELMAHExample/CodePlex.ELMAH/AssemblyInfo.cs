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

#region Imports

using System.Reflection;

using CLSCompliantAttribute = System.CLSCompliantAttribute;
using ComVisible = System.Runtime.InteropServices.ComVisibleAttribute;

#endregion

[assembly: AssemblyTitle("ELMAH")]
[assembly: AssemblyDescription("Error Logging Modules and Handlers (ELMAH) for ASP.NET")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("ELMAH")]
[assembly: AssemblyCopyright("Copyright (c) 2004, Atif Aziz, Skybow AG. All rights reserved.")]
[assembly: AssemblyCulture("")]

[assembly: AssemblyVersion("2.0.50727.42")]
[assembly: AssemblyFileVersion("2.0.50727.42")]

[assembly: AssemblyDelaySign(false)]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: CLSCompliant(true)] 
[assembly: ComVisible(false)]
