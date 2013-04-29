<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentNakedBound.aspx.cs" Inherits="ContentNakedBound" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
<h1><%# PageContent.Title %></h1>
<p 
    runat="server"
    ID="HawtParagraph"
    class="hawt"
    visible="<%# PageContent.IsHot %>"
>
    This is HAWT!
</p>
<%# PageContent.ContentText %>
    </form>
</body>
</html>
