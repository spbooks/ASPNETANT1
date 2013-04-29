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
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

public partial class _Default : System.Web.UI.Page
{
    //Sample Render overrides. These are commented out since the HttpModule will take care of this.
    #region "Page specific implementation"
    ///// <summary>
    ///// Standard implementation if not using HttpModule
    ///// (uncomment to use)
    ///// </summary>
    ///// <param name="writer"></param>
    //protected override void Render(HtmlTextWriter writer)
    //{
    //    MoveViewState(writer);
    //}

    ///// <summary>
    ///// Test implementation which shows time taken in moving ViewState
    ///// (uncomment to test)
    ///// </summary>
    ///// <param name="writer"></param>
    //protected override void Render(HtmlTextWriter writer)
    //{
    //    Stopwatch s = new Stopwatch();
    //    s.Start();
    //    MoveViewState(writer);
    //    s.Stop();
    //    Response.Write(
    //        string.Format("<!-- ticks:{0} milliseconds:{1}-->", 
    //        s.ElapsedTicks, 
    //        s.ElapsedMilliseconds));
    //}
    #endregion

    //This is only used in the Render() overrides, which have been commented out.
    protected void MoveViewState(HtmlTextWriter writer)
    {
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        base.Render(hw);
        string html = sw.ToString();

        int ViewStateStart = html.IndexOf("<input type=\"hidden\" name=\"__VIEWSTATE\"");
        if (ViewStateStart <= 0)
        {
            writer.Write(html);
            return;
        }

        // write the section of html before viewstate
        writer.Write(html.Substring(1, ViewStateStart - 1));

        int ViewStateEnd = html.IndexOf("/>", ViewStateStart) + 2;
        int FormEndStart = html.IndexOf("</form>");
        // write the section after the viewstate and up to the end of the FORM
        writer.Write(html.Substring(ViewStateEnd, html.Length - ViewStateEnd - (html.Length - FormEndStart)));
        // write the viewstate itself
        writer.Write(html.Substring(ViewStateStart, ViewStateEnd - ViewStateStart));
        // now write the FORM footer
        writer.Write(html.Substring(FormEndStart));
    }
}