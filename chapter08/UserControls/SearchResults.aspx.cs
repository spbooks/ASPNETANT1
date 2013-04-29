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

public partial class UserControls_SearchResults : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ISearchTermSource source = PreviousPage as ISearchTermSource;
        if (source != null)
        {
            SearchResults.Text = "You searched for " + source.SearchTerm;
        }
    }

    private void FindSearchParameterWithFindControl()
    {
        Control header = PreviousPage.FindControl("Header");
        if (header != null)
        {
            TextBox searchBox = header.FindControl("SearchTermTextBox")
                                        as TextBox;
            if (searchBox != null)
            {
                SearchResults.Text = "You searched for " + searchBox.Text;
            }
        }
    }
}
