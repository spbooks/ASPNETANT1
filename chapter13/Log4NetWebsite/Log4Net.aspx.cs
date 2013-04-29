using System;
using System.Reflection;
using log4net;

public partial class _Default : System.Web.UI.Page 
{
    private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        Log.Debug("Debug Message, only for debbuging.");
        Log.Info("Informational message.");
        Log.Warn("Warning Will Robinson!");

        try
        {
            //Some potientially exceptional operation
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected exception occurred.", ex);
        }
    }
}
