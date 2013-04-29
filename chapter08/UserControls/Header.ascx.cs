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

public partial class UserControls_Header : UserControl
{
    public string Message
    {
        get { return GreetingLabel.Text; }
        set { GreetingLabel.Text = value; }
    }

    public string SearchTerm
    {
        get { return SearchTermTextBox.Text; }
        set { SearchTermTextBox.Text = value; }
    }
}
