using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace chapter_05_form_validation.FormValidation
{
	public class PinValidator : BaseValidator
	{
        protected override bool EvaluateIsValid()
        {
          Queue<string> pins = HttpContext.Current.Session["Pins"] as Queue<string>;
          if (pins == null)
          {
            pins = new Queue<string>(3);
            HttpContext.Current.Session["Pins"] = pins;
          }

          string pinFromForm = GetControlValidationValue(ControlToValidate);
          foreach (string pin in pins)
          {
            if (pin == pinFromForm)
            {
              return false;
            }
          }

          if (pins.Count == 3)
            pins.Dequeue();
          pins.Enqueue(pinFromForm);
          return true;
        }

        ///<summary>
		///Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
		///</summary>
		///
		///<param name="e">A <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
	        base.OnPreRender(e);
        	
	        if(EnableClientScript)
	        {
		        string scriptUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "FormValidationExamples.Resources.Scripts.PinValidatorEvaluateIsValid.js");

		        if (!Page.ClientScript.IsClientScriptIncludeRegistered("PinValidatorEvaluateIsValid"))
		        {
			        Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "PinValidatorEvaluateIsValid", scriptUrl);
		        }
	        }
        }


        /// <summary>
        /// Adds the HTML attributes and styles that need to be rendered for the control to the specified <see cref="T:System.Web.UI.HtmlTextWriter"></see> object.
        /// </summary>
        /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter"></see> that represents the output stream to render HTML content on the client.</param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
	        base.AddAttributesToRender(writer);
	        if(RenderUplevel)
		        Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "evaluationfunction", "PinValidatorEvaluateIsValid");
	        else 
		        writer.AddAttribute("evaluationfunction", "PinValidatorEvaluateIsValid");
        }
	}
}
