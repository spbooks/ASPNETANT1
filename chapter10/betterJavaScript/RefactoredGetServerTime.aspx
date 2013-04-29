<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RefactoredGetServerTime.aspx.cs" Inherits="betterJavaScript_RefactoredDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Get Server Time</title>
    <script type="text/javascript" src="RefactoredGetServerTime.js"></script>
    <script type="text/javascript">        

        var contentManager;
        
        window.onload = function()
        {    
            contentManager = new ContentManager();
            contentManager.updateServerTime();
        }
        
        function getContent()
        {
            contentManager.updateServerTime();
        }
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
       
        <input type="button" id="getContentButton" value="Get Server Time" onclick="getContent();" />
        <div id="content">
        
        </div>
        
    </form>
    
</body>
</html>
