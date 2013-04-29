<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterExpandoAttributeExample.aspx.cs" Inherits="chapter_05_form_validation.FormValidation.RegisterExpandoAttributeExample" %>
<%@ Register TagPrefix="sp" Assembly="chapter_05_form_validation" Namespace="chapter_05_form_validation.FormValidation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
<script type="text/javascript">
	function whatIsExpando()
	{
		var expando = document.getElementById('expando');
		alert('Content editable: ' + expando.contenteditable);
	}
</script>
</head>
<body onload="whatIsExpando();">
    <form id="form1" runat="server">
    <div>
		<sp:ExpandoControl id="expando" runat="server" />
    </div>
    </form>
</body>
</html>
