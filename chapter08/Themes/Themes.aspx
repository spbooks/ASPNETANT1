<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Themes.aspx.cs" Inherits="Themes_Default"
    Theme="Crazy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Themes</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="links">
            <asp:BulletedList runat="server" ID="linkList" SkinID="LinkListSkin" >
                <asp:ListItem Value="http://www.OdeToCode.com/blogs/scott/">
                    Scott's Blog
                </asp:ListItem>
                <asp:ListItem Value="http://haacked.com/">
                    Phil's Blog
                </asp:ListItem>
                <asp:ListItem Value="http://weblogs.asp.net/jgalloway/">
                    Jon's Blog
                </asp:ListItem>
                <asp:ListItem Value="http://www.codinghorror.com/blog/">
                    Jeff's Blog
                </asp:ListItem>
                <asp:ListItem Value="http://www.sitepoint.com/blogs/category/net/">
                    Wyatt's Blog
                </asp:ListItem>
            </asp:BulletedList>
        </div>
        <div id="news">
            "The ASP.NET Anthology" is now available!
        </div>
    </form>
</body>
</html>
