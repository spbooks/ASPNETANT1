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

namespace chapter_13_error_handling.Log4NetWebApplication.Subfolder
{
    public partial class Default : System.Web.UI.Page
    {
        private static readonly ILog Log = LogManager.GetLogger(
MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info("In The Subfolder");
        }
    }
}
