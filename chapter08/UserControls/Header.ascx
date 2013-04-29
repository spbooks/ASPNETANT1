<%@ Control Language="C#" AutoEventWireup="true" 
    CodeFile="Header.ascx.cs" 
    Inherits="UserControls_Header" %>

<div class="header">
    
    <div id="Greeting">
        <asp:Label runat="server" ID="GreetingLabel" Text="Welcome" />
    </div>
    
    <div id="Search">
        <asp:TextBox runat="server" ID="SearchTermTextBox"/>
        <asp:Button runat="server" 
             ID="SearchButton" Text="Search" 
             PostBackUrl="~/UserControls/SearchResults.aspx" />
    </div>    
        
</div>

