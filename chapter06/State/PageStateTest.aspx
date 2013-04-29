<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageStateTest.aspx.cs" Inherits="State.PageStateTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:Literal id="postCount" runat="server" />
		<asp:Button ID="postButton" Text="Click me!" runat="server" />
    </div>
    </form>
</body>
</html>
