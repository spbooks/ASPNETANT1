<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidationGroupExample.aspx.cs" Inherits="chapter_05_form_validation.ValidatorsExample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Validation Group Example</title>
</head>
<body>
<form id="form1" runat="server">
	
<p>
	<asp:ValidationSummary ID="vldMessages" runat="server" />
</p>

Search: <asp:TextBox id="txtSearch" runat="server" ValidationGroup="Search" /> 
<asp:RequiredFieldValidator ID="vldSearchRequired" runat="server" 
	ErrorMessage="Search"
	ControlToValidate="txtSearch"
	ValidationGroup="Search"
	/>

<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />

<ul>
	<li>
		<label for="txtFirstName">First Name:</label>
		<asp:TextBox ID="txtFirstName" runat="server" ValidationGroup="UserForm" />
		<asp:RequiredFieldValidator ID="vldFirstRequired" runat="server" 
			ErrorMessage="First Name" 
			ControlToValidate="txtFirstName" 
			ValidationGroup="UserForm"
			 />
	</li>
	<li>
		<label for="txtLastName">Last Name:</label>
		<asp:TextBox ID="txtLastName" runat="server" ValidationGroup="UserForm" />
		<asp:RequiredFieldValidator ID="vldLastRequired" runat="server" 
			ErrorMessage="Last Name"
			ControlToValidate="txtLastName"
			ValidationGroup="UserForm" />
	</li>
	<li>
		<asp:Button ID="btnSubmit" runat="server" 
			Text="Submit" 
			OnClick="btnSubmit_Click" 
			ValidationGroup="UserForm"
			 />
	</li>
</ul>

</form>
</body>
</html>
