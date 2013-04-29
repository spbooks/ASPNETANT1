using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace chapter_05_form_validation.FormValidation
{
public class ExpandoControl : Label
{
	///<summary>
	///Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.Label"></see> control to render to the specified output stream. 
	///</summary>
	///
	///<param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter"></see> that represents the output stream to render HTML content on the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		Page.ClientScript.RegisterExpandoAttribute(this.ClientID, "contenteditable", "true");
		base.AddAttributesToRender(writer);
	}
}
}
