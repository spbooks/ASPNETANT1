using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using System.Reflection;

namespace chapter_13_error_handling.Log4NetWebApplication
{
    public partial class _Default : System.Web.UI.Page
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info("In The Root");

            Log.Debug("Debug Message, only for debbuging.");
            Log.Info("Informational message.");
            Log.Warn("Warning Will Robinson!");
            try
            {
                //Divide by zero to throw an exception. Compiler will catch 1 / 0, so do it on two lines.
                int i = 1;
                i = 1 / (i - i);
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected exception occurred.", ex);
            }
        }
    }
}
