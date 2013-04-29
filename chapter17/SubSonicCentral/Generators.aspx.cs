using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SubSonic;
using Northwind;
using SubSonic.Controls;

public partial class Generators : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //bool missingPath = !String.IsNullOrEmpty(CodeService.TemplateDirectory) && !Directory.Exists(CodeService.TemplateDirectory);

        //divWarning.Visible = missingPath;
        //ClassGenerator1.Visible = !missingPath;
        //ScaffoldGenerator1.Visible = !missingPath;

    }

    protected override void OnPreRender(EventArgs e)
    {
        WebUIHelper.EmitClientScripts(this);
        base.OnPreRender(e);
    }
}
