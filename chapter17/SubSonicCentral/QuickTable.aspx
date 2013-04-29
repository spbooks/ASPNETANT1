<%@ Page Language="C#" AutoEventWireup="true" 
  CodeFile="QuickTable.aspx.cs" Theme="Default" Inherits="QuickTable" %>
<%@ Register Assembly="SubSonic" Namespace="SubSonic" TagPrefix="subsonic" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <title>QuickTable</title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
  <subsonic:QuickTable ID="ProductsTable" runat="server" 
    TableName="Products" 
    PageSize="15" 
    ShowSort="true"    
    ColumnList="ProductName,QuantityPerUnit,UnitPrice"
    WhereExpression="Discontinued=False" />
  </div>
  </form>
</body>
</html>
