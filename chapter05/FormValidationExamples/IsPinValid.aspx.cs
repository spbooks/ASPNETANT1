using System;
using System.Collections.Generic;

namespace chapter_05_form_validation.FormValidation
{
    public partial class IsPinValid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          string pinFromJS = Request.QueryString["pin"];

          Queue<string> pins = Session["Pins"] as Queue<string>;
          if (pins != null)
          {
            foreach (string pin in pins)
            {
              if (pin == pinFromJS)
              {
                Response.Write("false");
                return;
              }
            }
          }

          Response.Write("true");
        }
    }
}
