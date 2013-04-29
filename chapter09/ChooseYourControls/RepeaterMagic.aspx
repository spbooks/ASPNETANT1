<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepeaterMagic.aspx.cs" Inherits="RepeaterMagic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Hawt ASP.NET Blogs
        <asp:Repeater runat="Server" ID="ExemplarRepeater" DataSource="<%# Bloggers %>">
            <HeaderTemplate>
                <ul>
            </HeaderTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
            <ItemTemplate>
                <li>
                    <%# Eval("FirstName") %> 
                    <%# Eval("LastName") %>
                    <asp:Button 
                        runat="Server" 
                        ID="SendReminderButton" 
                        Text="Send a Reminder" 
                        CommandArgument='<%# Eval("Id") %>'
                        OnClick="SendReminder"
                    />
                    <span 
                        runat="Server" 
                        id="SentLabel" 
                        visible="false"
                        class="sent" 
                    >
                        SENT
                   </span>
                </li>    
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
