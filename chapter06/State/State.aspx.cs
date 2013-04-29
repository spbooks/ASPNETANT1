using System;

namespace State
{
public partial class Default : System.Web.UI.Page
{
  protected override void OnInit(EventArgs e)
  {
  	Application["Start-Time"] = DateTime.Now;

    Page.Items["InitTime"] = DateTime.Now;
    base.OnInit(e);
  }

  protected override void OnPreRender(EventArgs e)
  {
  	double elapsed = (DateTime.Now - (DateTime)Page.Items["InitTime"]).TotalMilliseconds;
	Response.Write(elapsed + " seconds elapsed.");
    base.OnPreRender(e);
  }
}
}
