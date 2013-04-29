using System;
using System.Web.UI;

namespace ViewState
{
public class SubControlStateDemo : ControlStateDemo
{
  public int AnotherCount
  {
    get { return this.anotherCount; }
    set { this.anotherCount = value; }
  }

  private int anotherCount;

  protected override void OnLoad(EventArgs e)
  {
    AnotherCount++;
    base.OnLoad(e);
  }

  protected override void Render(HtmlTextWriter writer)
  {
    base.Render(writer);
    writer.Write("<p>AnotherCount:" + this.AnotherCount + "</p>");
  }

  protected override object SaveControlState()
  {
    //grab the state for the base control.
    object baseState = base.SaveControlState();

    //create an array to hold the base control's state 
    //and this control's state.
    object thisState = new object[] {baseState, this.anotherCount};
    return thisState;
  }

  protected override void LoadControlState(object savedState)
  {
    object[] stateLastRequest = (object[]) savedState;
    
    //Grab the state for the base class 
    //and give it to it.
    object baseState = stateLastRequest[0];
    base.LoadControlState(baseState);

    //Now load this control's state.
    this.anotherCount = (int) stateLastRequest[1];
  }
}
}
