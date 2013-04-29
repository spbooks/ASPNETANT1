<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentWithLabels.aspx.cs" Inherits="ContentWithLabels" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Content Bound to Labels</title>
</head>
<body>
    <form id="form1" runat="server">
<h1>
    <asp:Label 
        runat="Server" 
        ID="PageTitleLabel" 
        Text='<%# PageContent.Title %>' 
    />
</h1>
<asp:Label 
    runat="Server" 
    ID="IsHotLabel" 
    Text="This is Hawt!" 
    Font-Bold="true" 
    ForeColor="Red" 
    Visible='<%# PageContent.IsHot %>' 
 />
<asp:Label 
    runat="Server" 
    ID="ContentLabel" 
    Text='<%# PageContent.ContentText %>'
/>
    </form>
</body>
</html>
