<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SimpleFormWithSideMessages.aspx.cs" Inherits="chapter_05_form_validation.FormValidation.SimpleFormWithSideMessages" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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


<ul>
	<li>
	    <label for="usernameTextBox">Username:</label>
	    <asp:TextBox ID="usernameTextBox" runat="server" />
	    <asp:RequiredFieldValidator ID="usernameRequiredValidator" 	    
        runat="server" 
        ErrorMessage="Username is Required" 
        ControlToValidate="usernameTextBox" />
	</li>
	<li>
	    <label for="zipTextBox">Zip:</label>
	    <asp:TextBox ID="zipTextBox" runat="server" />
	    <asp:RegularExpressionValidator ID="zipValidator" 
	      runat="server" 
	      ValidationExpression="\d{5}(-?\d{4})?"
	      ErrorMessage="The zipcode is not valid."
	      ControlToValidate="zipTextBox" />
	</li>
	<li>
	    <label for="passwordTextBox">Password:</label>
	    <asp:TextBox ID="passwordTextBox" runat="server"    
        TextMode="Password" />
	    <asp:RequiredFieldValidator ID="passwordRequiredValidator" 
	      runat="server" 
	      ErrorMessage="Password is Required"
	      ControlToValidate="passwordTextBox" />
	</li>
	<li>
	    <label for="passwordRepeatedTextBox">Repeat Password:</label>
	    <asp:TextBox ID="passwordRepeatedTextBox" runat="server"
        TextMode="Password" />
	    <asp:CompareValidator ID="passwordCompareValidator" 
	      runat="server"
	      Operator="Equal"
	      ErrorMessage="The Passwords do not match"
	      ControlToValidate="passwordTextBox" 
	      ControlToCompare="passwordRepeatedTextBox" />
	</li>

	<li>
	    <asp:Button ID="submitButton" runat="server" 
	      Text="Submit" 
	      OnClick="submitButton_Click" />
	</li>
</ul>

    </form>
</body>
</html>
