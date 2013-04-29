<%@ Page Language="C#" AutoEventWireup="true" 
  CodeFile="ScaffoldSample.aspx.cs" Theme="Default" Inherits="ScaffoldSample" %>
<%@ Register Assembly="SubSonic" Namespace="SubSonic" TagPrefix="subsonic" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <title>Scaffold</title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
  <subsonic:Scaffold ID="Scaffold1" runat="server" 
    TableName="Products" GridViewSkinID="scaffold" />
  </div>
  </form>
</body>
</html>
