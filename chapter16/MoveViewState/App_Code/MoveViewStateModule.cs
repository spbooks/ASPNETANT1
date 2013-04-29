using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MoveViewState
{
    public class MoveViewStateModule : System.Web.IHttpModule
    {
        public MoveViewStateModule() { }

        void System.Web.IHttpModule.Dispose() { }

        void System.Web.IHttpModule.Init(System.Web.HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.BeginRequestHandler);
        }

        void BeginRequestHandler(object sender, EventArgs e)
        {
            System.Web.HttpApplication application = (System.Web.HttpApplication)sender;
            application.Response.Filter = new MoveViewStateFilter(application.Response.Filter);
        }
    }
}