<%@ Page Language="C#" MasterPageFile="~/MasterEvents/Interaction.master" 
    AutoEventWireup="true" CodeFile="MasterEvents.aspx.cs" Inherits="Interaction_Default" 
    Title="Interaction" Theme="Default" %>

<%@ MasterType VirtualPath="~/MasterEvents/Interaction.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Quisque vestibulum purus
    non tortor. Proin quis massa eu lacus rutrum imperdiet. Cum sociis natoque penatibus
    et magnis dis parturient montes, nascetur ridiculus mus. Praesent id urna. Integer
    nec lacus sed augue suscipit posuere.
    <br /><br />    
    <asp:Label ID="Message" runat="server" />
    <br />
</asp:Content>

