using System;
using System.Configuration;

namespace SitePoint.Cookbook.Configuration
{
	public partial class RetrieveAppSetting : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//The old way appSettingValue.Text = ConfigurationSettings.AppSettings["MySetting"];
			appSettingValue.Text = ConfigurationManager.AppSettings["MySetting"];
			connStr.Text = ConfigurationManager.ConnectionStrings["sqlDb"].ConnectionString;
			lblMyStuff.Text = MySettings.Settings.Bar;
		}
	}
}
