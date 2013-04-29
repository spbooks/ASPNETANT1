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

namespace chapter_05_form_validation
{
public partial class WithoutValidationGroupExample : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}

	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		if(Page.IsValid)
		{
			//Do nothing.
		}
	}
	
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		if(Page.IsValid)
		{
			//Do Nothing.
		}
	}
}
}
