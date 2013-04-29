using System;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string xml = HtmlScraper.GetHtmlAsXml().OuterXml;
        XmlDataSource1.Data = xml;
        Trace.Write("INFO", xml);
        XmlDataSource1.DataBind();
        Trace.Write("Info", "PRE BIND: Repeater Item Count " + this.Repeater1.Items.Count);
        this.Repeater1.DataBind();
        Trace.Write("Info", "POST BIND: Repeater Item Count " + this.Repeater1.Items.Count);
    }
}
