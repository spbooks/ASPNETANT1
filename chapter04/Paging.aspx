<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Paging.aspx.cs" Inherits="Paging" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="OrderID,ProductID" DataSourceID="SqlDataSource1" EmptyDataText="There are no data records to display." AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="OrderID" ReadOnly="True" SortExpression="OrderID" />
                <asp:BoundField DataField="ProductID" HeaderText="ProductID" ReadOnly="True" SortExpression="ProductID" />
                <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" SortExpression="UnitPrice" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                <asp:BoundField DataField="Discount" HeaderText="Discount" SortExpression="Discount" />
            </Columns>
            <PagerTemplate>
                <asp:LinkButton ID="first" runat="server" Text="<< First" CommandArgument="First" CommandName="Page" />
                <asp:LinkButton ID="prev" runat="server" Text="< Previous" CommandArgument="Prev" CommandName="Page" />
                &nbsp;Page&nbsp;
                <asp:DropDownList ID="pages" runat="server" AutoPostBack="True" />
                &nbsp;of&nbsp;<asp:Label ID="count" runat="server" />&nbsp;
                <asp:LinkButton ID="next" runat="server" Text="Next >" CommandArgument="Next" CommandName="Page" />
                <asp:LinkButton ID="last" runat="server" Text="Last >>" CommandArgument="Last" CommandName="Page" />
            </PagerTemplate>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>"
            DeleteCommand="DELETE FROM [Order Details] WHERE [OrderID] = @OrderID AND [ProductID] = @ProductID"
            InsertCommand="INSERT INTO [Order Details] ([OrderID], [ProductID], [UnitPrice], [Quantity], [Discount]) VALUES (@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount)"
            ProviderName="<%$ ConnectionStrings:NorthwindConnectionString.ProviderName %>"
            SelectCommand="SELECT [OrderID], [ProductID], [UnitPrice], [Quantity], [Discount] FROM [Order Details]"
            UpdateCommand="UPDATE [Order Details] SET [UnitPrice] = @UnitPrice, [Quantity] = @Quantity, [Discount] = @Discount WHERE [OrderID] = @OrderID AND [ProductID] = @ProductID">
            <InsertParameters>
                <asp:Parameter Name="OrderID" Type="Int32" />
                <asp:Parameter Name="ProductID" Type="Int32" />
                <asp:Parameter Name="UnitPrice" Type="Decimal" />
                <asp:Parameter Name="Quantity" Type="Int16" />
                <asp:Parameter Name="Discount" Type="Single" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="UnitPrice" Type="Decimal" />
                <asp:Parameter Name="Quantity" Type="Int16" />
                <asp:Parameter Name="Discount" Type="Single" />
                <asp:Parameter Name="OrderID" Type="Int32" />
                <asp:Parameter Name="ProductID" Type="Int32" />
            </UpdateParameters>
            <DeleteParameters>
                <asp:Parameter Name="OrderID" Type="Int32" />
                <asp:Parameter Name="ProductID" Type="Int32" />
            </DeleteParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
