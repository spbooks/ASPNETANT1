<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Log4Net.aspx.cs" Inherits="_Default" Trace="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Since this is a WebSite Project, we're configuring log4net via the Global.asax file:
    </div>
    <div>
    <pre>
    <code>
        void Application_Start(object sender, EventArgs e)
        {
          log4net.Config.XmlConfigurator.Configure(
              new System.IO.FileInfo(Server.MapPath("Log4net.config")));
        }
    </code>
    </pre>
    </div>
    </form>
</body>
</html>
