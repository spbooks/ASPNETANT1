using System;
using System.Threading;

public partial class updatepanel_autorefresh : System.Web.UI.Page
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
        Label2.Text = Label1.Text = DateTime.Now.ToLongTimeString();
        
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        UpdateServerTime();
    }
}
