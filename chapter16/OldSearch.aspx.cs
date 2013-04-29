using System;

namespace SitePoint.Cookbook
{
	public partial class OldSearch : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			
		}
		
		protected void Button1_Click(object sender, EventArgs e)
		{
			SearchResultsLabel.Text = "If these were real search results, search bots would not see them";
		}
	}
}
