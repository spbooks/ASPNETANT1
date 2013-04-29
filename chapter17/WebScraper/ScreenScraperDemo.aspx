<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <title>Untitled Page</title>
  <style type="text/css">
    ul li {list-style-type: none; margin: 0; padding: 0;}
    table {border-collapse: collapse;}
    td {border: solid 1px #999; padding: 10px;}
  </style>
</head>

<body>
  <form id="form1" runat="server">
  <div>
    
<asp:Repeater ID="Repeater1" runat="server" DataSourceID="XmlDataSource1">
  <HeaderTemplate>
    <table class="stocks">
      <tr>
        <td>
          <ul>
            <li><strong>Stock</strong></li>
            <li>Price</li>
            <li>Change</li>
          </ul>
        </td>
  </HeaderTemplate>
  <ItemTemplate>
    <td>
      <ul>
        <li><strong><%# XPathBinder.Eval(Container.DataItem, "td[position() = 1]") %></strong></li>
        <li><%# XPath("td[position() = 2]")%></li>
        <li><%# XPath("td[position() = 3]")%></li>
      </ul>
    </td>
  </ItemTemplate>
  <FooterTemplate>
      </tr>
    </table>
  </FooterTemplate>
</asp:Repeater>
    
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" XPath="html/body/table/tr[position() > 1]"></asp:XmlDataSource>
  </div>
  </form>
</body>
</html>
