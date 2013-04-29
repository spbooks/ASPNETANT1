using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Simple helper page to get content cleanly
/// </summary>
public class ContentPage : Page
{
	public ContentPage()
	{
        Init += new EventHandler(getContent);
        Load += new EventHandler(bindData);
	}

    private void bindData(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            DataBind();
        }
    }

    private void getContent(object sender, EventArgs e)
    {
        int cid = 1;
        if (Request.QueryString["ID"] != null)
        {
            cid = int.Parse(Request.QueryString["ID"]);
        }
        pageContent = ContentFactory.GetContent(cid);
    }

    private Content pageContent;
    protected Content PageContent
    {
        get { return pageContent; }
    }
}
