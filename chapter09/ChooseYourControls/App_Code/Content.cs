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
/// Simple content class for use with the labels example.
/// </summary>
public class Content
{
    public Content()
    { }

    private string title;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    private string contentText;

    public string ContentText
    {
        get { return contentText; }
        set { contentText = value; }
    }

    private bool isHot;

    public bool IsHot
    {
        get { return isHot; }
        set { isHot = value; }
    }

}
