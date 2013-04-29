<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimpleForm.aspx.cs" Inherits="chapter_05_form_validation.FormValidation.SimpleForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        label
        {
            width: 300px;
            display: block;
            font-size: .7em;
            font-family: verdana;
        }
        
        ul
        {
            list-style-type: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

<asp:ValidationSummary ID="vldMessages" runat="server" />

<ul>
	<li>
	    <label for="txtUsername">Username:</label>
	    <asp:TextBox ID="txtUsername" runat="server" />
	    <asp:RequiredFieldValidator ID="vldUsernameRequired" 	    
        runat="server" 
        Text="*"
        ErrorMessage="Username is Required" 
        ControlToValidate="txtUsername" />
	</li>
	<li>
	    <label for="txtZip">Zip:</label>
	    <asp:TextBox ID="txtZip" runat="server" />
	    <asp:RegularExpressionValidator ID="vldZip" 
	      runat="server" 
	      ValidationExpression="\d{5}(-?\d{4})?"
	      ErrorMessage="The zipcode is not valid."
	      Text="*"
	      ControlToValidate="txtZip" />
	</li>
	<li>
	    <label for="txtPassword">Password:</label>
	    <asp:TextBox ID="txtPassword" runat="server"    
        TextMode="Password" />
	    <asp:RequiredFieldValidator ID="vldPasswordRequired" 
	      runat="server" 
	      ErrorMessage="Password is Required"
	      Text="*"
	      ControlToValidate="txtPassword" />
	</li>
	<li>
	    <label for="txtPasswordRepeated">Repeat Password:</label>
	    <asp:TextBox ID="txtPasswordRepeated" runat="server"
        TextMode="Password" />
	    <asp:CompareValidator ID="vldPasswordsMatch" 
	      runat="server" 
	      ErrorMessage="The Passwords do not match"
	      Text="*"
	      ControlToValidate="txtPassword" 
	      ControlToCompare="txtPasswordRepeated" />
	</li>

	<li>
	    <asp:Button ID="btnSubmit" runat="server" 
	      Text="Submit" 
	      OnClick="btnSubmit_Click" />
	</li>
</ul>

    </form>
</body>
</html>
