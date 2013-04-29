using System;
using System.Web.UI.WebControls;

namespace ViewState
{
public class ControlStateDemo : WebControl
{
  public int ViewPostCount
  {
    get { return (int)(ViewState["ViewProp"] ?? 0); }
    set { ViewState["ViewProp"] = value; }
  }

  public int ControlPostCount
  {
    get { return controlPostCount; }
    set { controlPostCount = value; }
  }
  
  private int controlPostCount;

  protected override void OnInit(EventArgs e)
  {
    //Let the page know this control needs the control state.
    Page.RegisterRequiresControlState(this);
    base.OnInit(e);
  }

  protected override void OnLoad(EventArgs e)
  {
    ViewPostCount++;
    ControlPostCount++;
    base.OnLoad(e);
  }

  protected override void Render(System.Web.UI.HtmlTextWriter writer)
  {
    writer.Write("<p>ViewState: " + this.ViewPostCount + "</p>");
    writer.Write("<p>ControlState:" + this.ControlPostCount + "</p>");
    base.Render(writer);
  }
  
  protected override void LoadControlState(object savedState)
  {
    int state = (int)(savedState ?? 0);
    this.controlPostCount = state;
  }

  protected override object SaveControlState()
  {
    return controlPostCount;
  }
}
}
