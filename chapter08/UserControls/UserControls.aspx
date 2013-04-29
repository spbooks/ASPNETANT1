<%@ Page Language="C#" AutoEventWireup="true" 
         CodeFile="UserControls.aspx.cs" 
         Inherits="UserControls_Default" 
         Theme="Default" %>
<%@ Register Src="Header.ascx" TagName="Header" 
             TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Header ID="Header" runat="server" Message="Greetings!"
        />    
    </div>
    </form>
</body>
</html>
