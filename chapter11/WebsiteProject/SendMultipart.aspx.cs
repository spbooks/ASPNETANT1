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
using System.Net.Mail;

public partial class SendMultipart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MailMessage m = new MailMessage();

        m.From = new MailAddress("manager@tchotchkes.com");
        m.To.Add("joanna@tchotchkes.com");

        m.Subject = "Your Flair";
        m.Body = "I need to talk to you about your Flair.";

        AlternateView html = AlternateView.CreateAlternateViewFromString(
            "I need to talk to you about <b>your Flair</b>.", null, "text/html");
        m.AlternateViews.Add(html);

        SmtpClient sc = new SmtpClient();
        sc.Send(m);

    }
}
