<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetrieveAppSetting.aspx.cs" Inherits="SitePoint.Cookbook.Configuration.RetrieveAppSetting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<ul>
			<li><asp:Label ID="appSettingValue" runat="server" /></li>
			<li><asp:Label ID="appSettingsExample" runat="server" Text="<%$ AppSettings:AnotherSetting %>" /></li>
			<li><asp:Label ID="connStr" runat="server" /></li>
			<li><asp:Label ID="connection" runat="server" Text="<%$ ConnectionStrings:sqlDb %>" /></li>
			<li><asp:Label ID="lblMyStuff" runat="server" /></li>
		</ul>
		<asp:SqlDataSource ID="ds" runat="server" ConnectionString="<%$ ConnectionSTrings:sqlDb %>"></asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
