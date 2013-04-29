using System;

namespace SessionState
{
	public partial class SecondPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			sessionVar.Text = Session["Var"] as string;
		}
	}
}
