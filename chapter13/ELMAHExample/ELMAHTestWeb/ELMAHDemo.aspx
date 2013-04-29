<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ELMAHDemo.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Test Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="ErrorButton" runat="server" OnClick="ErrorButton_Click" Text="Throw an Error" />
    </div>
    <p>Then come back to this page (browser back arror) and click the following link: <a href="ELMAH/default.aspx">View the ELMAH log</a></p>
    </form>
</body>
</html>
