<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetServerTime.aspx.cs" Inherits="betterJavaScript_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Get Server Time</title>
    <script type="text/javascript" src="GetServerTime.js"></script>
</head>
<body>
    
    <form id="form1" runat="server">
       
        <input type="button" id="getContentButton" 
               onclick="getContent();" value="Get Server Time" />
        <div id="content">
        
        </div>        
    </form>    
</body>
</html>
