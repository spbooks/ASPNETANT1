<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MSAjaxGetServerTime.aspx.cs" Inherits="msajax_GetServerTime" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ASP.NET AJAX</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" >
            <Scripts>
                <asp:ScriptReference Path="MSAjaxGetServerTime.js" />
            </Scripts>
            <Services>
                <asp:ServiceReference Path="~/ServerTime.asmx" />
            </Services>
        </asp:ScriptManager>
        
        <input type="button" id="getContentButton" value="Get Server Time" />       
        <div id="content">
        </div>
        
    </form>
</body>
</html>
