<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OldSearch.aspx.cs" Inherits="SitePoint.Cookbook.OldSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:TextBox id="SearchTextBox" runat="server" />
		<asp:Button id="SearchButton" runat="server"  
			onclick="Button1_Click" Text="Search" />
    <br />
    <asp:Label id="SearchResultsLabel" runat="server" 
        Text="Label" />
    </div>
    </form>
</body>
</html>
