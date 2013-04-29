<%@ Page Language="C#" AutoEventWireup="true" 
  CodeFile="QuickTable.aspx.cs" Theme="Default" Inherits="QuickTable" %>
<%@ Register Assembly="SubSonic" Namespace="SubSonic" TagPrefix="subsonic" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <title>QuickTable</title>
</head>
<body>
  <form id="form1" runat="server">
<p>DropDown bound to Employees table</p>
<subsonic:DropDown ID="ddlEmployees" runat="server" 
  TableName="Employees" />
<hr/>

<p>ManyManyList showing Territories selected for Employee ID 1</p>
<subsonic:ManyManyList ID="manyEmployeesTerritories" runat="server" 
  PrimaryTableName="Employees" 
  PrimaryKeyValue="1" 
  ForeignTableName="Territories" 
  MapTableName="EmployeeTerritories" 
  ProviderName="Northwind" 
  RepeatColumns="4" />
<hr/>

<p>RadioButton bound to Suppliers</p>
 <subsonic:RadioButtons ID="radiobuttons" runat="server" 
  TableName="Suppliers" 
  TextField="CompanyName" 
  ValueField="SupplierID" />
  </form>
</body>
</html>