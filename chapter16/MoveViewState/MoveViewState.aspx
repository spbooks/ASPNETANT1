<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoveViewState.aspx.cs" Inherits="_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Moving ViewState</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="gridSample" runat="server" AllowPaging="True" DataSourceID="xmlDataSource"
            PageSize="100">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:XmlDataSource ID="xmlDataSource" runat="server" DataFile="~/App_Data/cia.xml" />
    </form>
</body>
</html>