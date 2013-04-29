using System;
using System.Threading;

public partial class updatepanel_Slow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UpdateServerTime();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        UpdateServerTime();
    }

    private void UpdateServerTime()
    {
        Thread.Sleep(2000);
        Label1.Text = DateTime.Now.ToLongTimeString();
    }
}
