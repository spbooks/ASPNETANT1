<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Triggered.aspx.cs" Inherits="updatepanel_Triggered" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" 
                           EnablePartialRendering="true">
        </asp:ScriptManager>
        <div>
            <asp:Button ID="Button1" runat="server" Text="Get Server Time" 
                        OnClick="Button1_Click" />
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>                    
                    <asp:Label ID="Label1" runat="server" Text="" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button1" />
                </Triggers>
            </asp:UpdatePanel>
            
        </div>
    </form>
</body>
</html>
