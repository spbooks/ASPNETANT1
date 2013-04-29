using System;

public partial class updatepanel_Default : System.Web.UI.Page
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
        Label1.Text = DateTime.Now.ToLongTimeString();
    }
}
