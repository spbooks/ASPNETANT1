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
using System.ComponentModel;
using System.Diagnostics;

public partial class SendAsync : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MailMessage m = new MailMessage("mwaddams@initech.com", "blumbergh@initech.com", "Stapler", "I believe you have my stapler.");

        SmtpClient sc = new SmtpClient();
        sc.SendCompleted += new SendCompletedEventHandler(MailSendCompleted);
        sc.SendAsync(m, m);
    }

    public static void MailSendCompleted(
                     object sender, AsyncCompletedEventArgs e)
    {
        MailMessage m = e.UserState as MailMessage;
        if (e.Cancelled)
        {
            Debug.Write("Email to " + m.To + " was cancelled.");
        }
        if (e.Error != null)
        {
            Debug.Write("Email to " + m.To + " failed.");
            Debug.Write(e.Error.ToString());
        }
        else
            Debug.Write("Message sent.");
    }
}