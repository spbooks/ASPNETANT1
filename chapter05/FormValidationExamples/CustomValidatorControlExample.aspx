<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomValidatorControlExample.aspx.cs" Inherits="chapter_05_form_validation.FormValidation.CustomValidatorControlExample" %>
<%@ Register TagPrefix="sp" Assembly="FormValidationExamples" Namespace="chapter_05_form_validation.FormValidation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Custom Validator</title>
</head>
<body>
    <form id="form1" runat="server">
<div>
  <asp:Label ID="pinLabel" runat="server" 
    Text="PIN:" 
    AssociatedControlID="pinTextBox" />
  <asp:TextBox ID="pinTextBox" runat="server" />
  <asp:RegularExpressionValidator ID="pinDigitValidator" runat="server" 
    ControlToValidate="pinTextBox"
    ErrorMessage="Pin must contain four to eight digits"
    ValidationExpression="\d{4,8}" />
  <sp:PinValidator id="pinValidator" runat="server" 
    ControlToValidate="pinTextBox" 
    ErrorMessage="You used that PIN recently." />
  <p>
    <asp:Button ID="submitButton" runat="server" Text="Change PIN" />
  </p>
</div>
    </form>
</body>
</html>
