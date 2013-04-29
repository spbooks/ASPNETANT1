<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="chapter_13_error_handling.Log4NetWebApplication._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>log4net sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Since this is a Web Application Project, we're configuring log4net via AssemblyInfo.cs:
    </div>
    <div>
    <pre>
    <code>
        [assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4net.config", Watch = true)]
    </code>
    </pre>
    </div>
    <div>
        <a href="Subfolder/Default.aspx">View a page in a subfolder to demonstrate folder level logging.</a>
    </div>
    </form>
</body>
</html>
