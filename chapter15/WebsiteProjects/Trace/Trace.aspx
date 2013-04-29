<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Trace.aspx.cs" Inherits="_Default" Trace="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Trace Sample</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Note - we're shoting the trace information directly in the page by including <em>Trace="true"</em> in the <em>@Page</em> directive. 
        You can also view Trace information for a site by browsing to <em>trace.axd</em>, as mentioned in the text which accompanies this sample.
    
    </div>
    </form>
</body>
</html>
