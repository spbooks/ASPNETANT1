using System;


namespace State
{
	public partial class PageStateTest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Page.Items["PostCount"] == null)
				Page.Items["PostCount"] = 0;
			else
			{
				int count = (int) Page.Items["PostCount"];
				Page.Items["PostCount"] = ++count;
			}
			postCount.Text = Page.Items["PostCount"].ToString();
		}
	}
}
