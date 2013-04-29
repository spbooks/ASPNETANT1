<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ControlStateExample.aspx.cs" Inherits="ViewState.ControlState" EnableViewState="false" %>
<%@ Register TagPrefix="sp" Assembly="MaintainingState" Namespace="ViewState" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
<form id="form1" runat="server">
<div>
	<sp:ControlStateDemo ID="SubControlStateDemo1" runat="server" />
	<!-- <sp:SubControlStateDemo ID="demo" runat="server" /> -->
	<asp:Button ID="button" runat="server" Text="Post Back!" />
</div>
</form>
</body>
</html>
