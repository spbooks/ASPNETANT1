<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jokehost.aspx.cs" Inherits="updatepanel_jokehost" %>

<%@ Register Src="Joke1.ascx" TagName="Joke1" TagPrefix="otc" %>
<%@ Register Src="Joke2.ascx" TagName="Joke2" TagPrefix="otc" %>
<%@ Register Src="Joke3.ascx" TagName="Joke3" TagPrefix="otc" %>
<%@ Register Src="Joke4.ascx" TagName="Joke4" TagPrefix="otc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" ID="scriptManager1"/>
    <div>
        <otc:Joke1 ID="Joke1" runat="server" />       
    </div>
    </form>
</body>
</html>
