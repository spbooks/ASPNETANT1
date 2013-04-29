using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Factory class to get content.
/// </summary>
/// <remarks>
/// No, you should not ever set the content of something in a codebehind class. This is for illustration purposes only and should not ever approache a live application.
/// </remarks>
public static class ContentFactory
{
    public static Content GetContent(int id)
    {
        Content ret = null;
        switch (id)
        {
            case 1:
                ret = new Content();
                ret.Title = "First Page";
                ret.ContentText = "<p>Content ID 1.</p>";
                ret.IsHot = true;
                break;
            case 2:
                ret = new Content();
                ret.Title = "Second Page";
                ret.ContentText = "<p>Content ID 2.</p>";
                ret.IsHot = false;
                break;
            default:
                throw new ArgumentOutOfRangeException("No such content exists!");
                break;
        }
        return ret;
    }
}
