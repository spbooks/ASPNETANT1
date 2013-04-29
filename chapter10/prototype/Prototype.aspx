<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prototype.aspx.cs" Inherits="prototype_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prototype</title>
    <script type="text/javascript" src="../scripts/prototype/prototype.js"></script>
    <script type="text/javascript" src="PrototypeGetServerTime.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="button" id="getContentButton" value="Get Server Time" />
       
        <div id="content">
        </div>
    </form>
</body>
</html>
