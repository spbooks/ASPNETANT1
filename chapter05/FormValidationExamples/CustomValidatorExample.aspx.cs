using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace chapter_05_form_validation.FormValidation
{
	public partial class CustomValidatorExample : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

protected void pinCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
{
  Queue<string> pins = Session["Pins"] as Queue<string>;
  if (pins == null)
  {
    pins = new Queue<string>(3);
    Session["Pins"] = pins;
  }

  foreach(string pin in pins)
  {
    if(pin == args.Value)
    {
      args.IsValid = false;
      return;
    }
  }

  if (pins.Count == 3)
    pins.Dequeue();
  pins.Enqueue(args.Value);
}
	}
}
