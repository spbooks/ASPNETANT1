using System;

namespace ViewState
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
ViewState["FirstKey"] = "Hello";
ViewState["Firstkey"] += " World";
Response.Write(ViewState["Firstkey"]);
		}

public string City
{
get { return (string)ViewState["City"]; }
set { ViewState["City"] = value; }
}

public bool Enabled
{
	get
	{
		return (bool)(ViewState["Enabled"] ?? false);
	}
	set
	{
		ViewState["Enabled"] = value;
	}
}
	}
}
