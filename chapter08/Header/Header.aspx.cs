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

public partial class Header_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Header.Controls.Clear();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        HtmlMeta meta = new HtmlMeta();        
        meta.HttpEquiv = "Refresh";
        meta.Content = "2;URL=http://www.OdeToCode.com";
        Header.Controls.Add(meta);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        HtmlLink link = new HtmlLink();
        link.Href = "CustomStyles.css";
        link.Attributes.Add("rel", "stylesheet");
        link.Attributes.Add("type", "text/css");

        Header.Controls.Add(link);
    }
}
