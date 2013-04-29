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

public partial class HtmlControls_Label : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
protected void DoSometing_Click(object sender, EventArgs e)
{
    try
    {
        //update database or somesuch
        MessageContainer.Text = "Action Successful!";
        MessageContainer.ForeColor = System.Drawing.Color.Blue;
    }
    catch (ApplicationException aex)
    {
        //business error, send user error message.
        MessageContainer.Text = string.Format("Error doing something. Error was {0}", System.Drawing.Color.Red);
        MessageContainer.ForeColor = System.Drawing.Color.Red;
    }
}
}
