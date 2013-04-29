<%@ Page Language="C#" AutoEventWireup="true" 
    CodeFile="SqlCacheDependency.aspx.cs" 
    Inherits="chapter_15_performance.SqlCacheDependency" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sql Cache Dependency</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="True"
            DataSourceID="SqlDataSource1" EmptyDataText="There are no data records to display.">
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString1 %>"
            ProviderName="<%$ ConnectionStrings:NorthwindConnectionString1.ProviderName %>"
            SelectCommand="SELECT [ProductID], [ProductName], [SupplierID], [CategoryID], [QuantityPerUnit], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued] FROM [Products]"
            DataSourceMode="DataSet" EnableCaching="False" SqlCacheDependency="NorthwindConnectionString1:Products">
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>