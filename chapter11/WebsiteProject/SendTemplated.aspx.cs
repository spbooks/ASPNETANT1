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
using System.Collections.Generic;
using System.Net.Mail;

public partial class SendTemplated : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MailDefinition md = new MailDefinition();
        md.BodyFileName = "mailtemplate.txt";

        Dictionary<string, string> d = new Dictionary<string, string>();
        d.Add("<customer>", "ACME Global");
        d.Add("<amount>", ".0002 cents");

        MailMessage m = md.CreateMailMessage("finance@acmeglobal.com", d, new LiteralControl());
        m.Subject = "account review";
        m.From = new MailAddress("support@initech.com");

        SmtpClient smtp = new SmtpClient();
        smtp.Send(m);
    }
}
