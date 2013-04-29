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
using System.Collections.Generic;

public partial class RepeaterMagic : System.Web.UI.Page
{
    public RepeaterMagic()
    {
        Init += new EventHandler(bindData);
    }

    protected List<Person> Bloggers;

    void bindData(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bloggers = Person.GetPersons();
            DataBind();
        }
    }

    protected void SendReminder(object sender, EventArgs e)
    {
        //Take button reference from sending button control
        IButtonControl sButton = (IButtonControl)sender;
        //Extract ID from the CommandArgument
        Guid id = new Guid(sButton.CommandArgument);
        PersonMailerService.SendMail(id);

        //Take reference of control for next part
        Control sControl = (Control)sender;
        //use the sender's naming container to find other controls in that RepeaterItem
        Control c=sControl.NamingContainer.FindControl("SentLabel");
        //show sent label, hid button
        c.Visible = true;
        sControl.Visible = false;
    }

    internal class PersonMailerService
    {
        public static void SendMail(Guid personId)
        {
            //do nothing
        }
    }
}
