<%@ Page Language="C#" MasterPageFile="~/Header/Header.master" AutoEventWireup="true" CodeFile="Header.aspx.cs" Inherits="Header_Default" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Clear Header" />
    <br />
    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Add Meta Tag" />
    <br />
    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Add Style Sheet" />
</asp:Content>

