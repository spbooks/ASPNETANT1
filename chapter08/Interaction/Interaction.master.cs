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

public partial class Interaction_Site : System.Web.UI.MasterPage
{
    public string WelcomeMessage
    {
        get { return this.HeaderSpan.InnerText; }
        set { HeaderSpan.InnerText = value; }
    }	
}
