using System;


namespace SitePoint.Cookbook
{
	public partial class NewSearch : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(Request["q"]))
			{
				SearchTextBox.Text = Request["q"];
				SearchResultsLabel.Text = "A search engine can find these results.";
			}
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(Request["q"]))
			{
			    Response.Redirect("NewSearch.aspx?q=" + SearchTextBox.Text);
				return;
			}
		}
	}
}
