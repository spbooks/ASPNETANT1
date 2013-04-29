<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoRefresh.aspx.cs" Inherits="updatepanel_autorefresh" %>

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
            <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                AssociatedUpdatePanelID="UpdatePanel1" 
                                DisplayAfter="200" DynamicLayout="true">
                 <ProgressTemplate>
                    <img src="spinner.gif" alt="Loading..." />
                 </ProgressTemplate>            
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Button ID="Button1" runat="server" Text="Get Server Time" 
                                OnClick="Button1_Click" />
                    <asp:Label ID="Label1" runat="server" Text="" />
                    
                    <asp:Timer ID="Timer1" runat="server" Interval="5000" OnTick="Timer1_Tick">
                    </asp:Timer>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
