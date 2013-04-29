using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace State
{
  public partial class PageInitialization : System.Web.UI.Page
  {
protected override void OnInit(EventArgs e)
{
  if (HttpContext.Current.Items["AcquireRequestState"] != null)
  {
    DateTime acquired = (DateTime) HttpContext.Current.Items["AcquireRequestState"];
    double interval = (DateTime.Now - acquired).TotalMilliseconds;
	Response.Write(string.Format("acquired: {0}<br />", acquired));
	Response.Write(string.Format("interval: {0}ms<br />", interval));
  }
}
  }
}
