using System;
using System.Configuration;
using System.Web.Configuration;

namespace SitePoint.Cookbook.Configuration
{
	public partial class GetMySection : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			BlogSettings settings = ConfigurationManager.GetSection("BlogSettings") as BlogSettings;
			if (settings != null)
			{
				Response.Write("Title: " + settings.Title + "<br />");
				Response.Write("FrontPagePostCount: " + settings.FrontPagePostCount + "<br />");
			}

			AuthenticationSection auth = ConfigurationManager.GetSection("system.web/authentication") as AuthenticationSection;
			Response.Write(auth);
		}
	}
}
