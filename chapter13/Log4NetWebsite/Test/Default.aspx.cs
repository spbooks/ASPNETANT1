using System;
using System.Reflection;
using log4net;

public partial class Test_Default : System.Web.UI.Page
{
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        Log.Info("Test within Test folder.");
    }
}
