using System;

namespace chapter_05_form_validation.FormValidation
{
    public partial class SimpleForm : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(Page.IsValid) // Calls Page.Validate()
            {
                //Register User
            }
        }
    }
}
