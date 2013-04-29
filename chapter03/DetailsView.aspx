<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailsView.aspx.cs" Inherits="DetailsView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList 
			ID="ddlProducts" 
			runat="server" 
			AutoPostBack="True" 
			DataSourceID="dataSourceProductNames" 
			DataTextField="ProductName" 
			DataValueField="ProductID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="dataSourceProductNames" 
			runat="server" 
			ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>"
	        SelectCommand="SELECT [ProductID], [ProductName] FROM [Products]">
		</asp:SqlDataSource>
        <asp:SqlDataSource ID="dataSourceProductDetails" 
			runat="server" 
			ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>"
	        SelectCommand="SELECT [ProductID], [ProductName], [UnitPrice], [QuantityPerUnit], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued], [CategoryID], [SupplierID] FROM [Products] WHERE ([ProductID] = @ProductID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlProducts" Name="ProductID" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
		</asp:SqlDataSource>
        <asp:DetailsView ID="productDetails" runat="server" AutoGenerateRows="False" DataKeyNames="ProductID"
            DataSourceID="dataSourceProductDetails" Height="50px" Width="125px">
            <Fields>
                <asp:BoundField DataField="ProductID" HeaderText="ProductID" InsertVisible="False"
                    ReadOnly="True" SortExpression="ProductID" />
                <asp:BoundField DataField="ProductName" HeaderText="ProductName" SortExpression="ProductName" />
                <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice" />
                <asp:BoundField DataField="QuantityPerUnit" HeaderText="QuantityPerUnit" SortExpression="QuantityPerUnit" />
                <asp:BoundField DataField="UnitsInStock" HeaderText="UnitsInStock" SortExpression="UnitsInStock" />
                <asp:BoundField DataField="UnitsOnOrder" HeaderText="UnitsOnOrder" SortExpression="UnitsOnOrder" />
                <asp:BoundField DataField="ReorderLevel" HeaderText="ReorderLevel" SortExpression="ReorderLevel" />
                <asp:CheckBoxField DataField="Discontinued" HeaderText="Discontinued" SortExpression="Discontinued" />
            </Fields>
        </asp:DetailsView>
    </div>
    </form>
</body>
</html>
