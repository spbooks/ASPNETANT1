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
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Master.SendEmail += new SendEmailEventHandler(Master_SendEmail);
    }

    void Master_SendEmail(object sender, SendEmailEventArgs args)
    {
        string toAddress = args.EmailAddress;

        //
        // code to send the email ...
        //      
    }    
}
