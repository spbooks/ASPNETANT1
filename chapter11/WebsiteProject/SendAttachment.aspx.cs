using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;

public partial class SendAttachment : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("manager@tchotchkes.com");
        mail.To.Add("joanna@tchotchkes.com");
        mail.Subject = "Flair guidelines";
        mail.Body = "See the attached file for more details";

        string path = Server.MapPath("~/MyDocument.txt");
        mail.Attachments.Add(new Attachment(path));
        
        SmtpClient client = new SmtpClient();
        client.Send(mail);

    }
}
