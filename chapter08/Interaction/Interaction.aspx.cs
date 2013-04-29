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

public partial class Interaction_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //UseFindControl();
        UseMasterProperty();
    }

    private void UseFindControl()
    {
        HtmlGenericControl span;
        span = Master.FindControl("HeaderSpan") as HtmlGenericControl;
        if (span != null)
        {
            span.InnerText = "Welcome Back!";
        }
    }

    private void UseMasterProperty()
    {
        Master.WelcomeMessage = "Welcome Back!";   
    }
}
