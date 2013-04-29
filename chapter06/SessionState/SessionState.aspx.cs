using System;

namespace SessionState
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Session["Var"] = DateTime.Now + ": Ok,Not Random";
		}
	}
}
