<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Panels.aspx.cs" Inherits="Panels_Default" %>
<%@ Register Assembly="HilightPanel" Namespace="HilightPanel" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Panels</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:hilightpanel id="HilightPanel1" runat="server" 
                          CssClass="dark" HilightCssClass="bright">
        
            Frodo lives.
            <br /><br /><br />
        
        </cc1:hilightpanel>    
    </div>
    </form>
</body>
</html>
