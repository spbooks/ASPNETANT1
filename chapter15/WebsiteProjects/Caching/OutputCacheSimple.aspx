<%@ Page Language="C#" AutoEventWireup="true"   
    CodeFile="OutputCacheSimple.aspx.cs" 
    Inherits="chapter_15_performance.Performance.OutputCacheSimple" %>
<%@ OutputCache Duration="120" VaryByParam="none" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Output Cache Example</title>
</head>
<body>
    <form id="form1" runat="server">
    <h1>Output Cache Example</h1>
    <div>
        <%= DateTime.Now.ToLongTimeString() %>
    </div>
    </form>
</body>
</html>