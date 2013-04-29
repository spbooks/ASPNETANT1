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

public partial class _Default : System.Web.UI.Page
{

  protected void Button1_Click(object sender, EventArgs e)
  {
    throw new Exception("This is a test exception!");
  }

  protected void Button2_Click(object sender, EventArgs e)
  {
    MailMessage message = new MailMessage();
    message.To.Add(new MailAddress("from@contoso.com"));
    message.From = new MailAddress("to@contoso.com");
    message.Subject = "Subject Line";
    message.Body = "HMTL-formatted body";
    message.IsBodyHtml = true;
    SmtpClient client = new SmtpClient();
    client.Send(message); 
  }
}
