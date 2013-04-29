<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomValidatorExample.aspx.cs" Inherits="chapter_05_form_validation.FormValidation.CustomValidatorExample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Custom Validator</title>
<script type="text/javascript">
  function isntQuiteValidPin(source, args)
  {
    var pin = args.Value;
    var xmlhttp;
    if (window.XMLHttpRequest)
    {
      // if IE 7, Mozilla, Safari, Opera, etc.
      xmlhttp = new XMLHttpRequest()
    }
    else if (window.ActiveXObject)
    {
      // use the ActiveX control for IE 5.x and IE 6
      xmlhttp = new ActiveXObject("Microsoft.XMLHTTP")
    }
    var date = new Date();
    var pinParam = "pin="+pin;
    xmlhttp.open('POST', '/FormValidation/IsPinValid.aspx?rnd='
        +date.getTime(), false);
    xmlhttp.setRequestHeader('Content-Type',
        'application/x-www-form-urlencoded');
    xmlhttp.send(pinParam);
    args.IsValid = eval(xmlhttp.responseText);  
  }
function isValidPin(source, args)
{
  var pin = args.Value;
  var xmlhttp;
  if (window.XMLHttpRequest)
  {
    xmlhttp = new XMLHttpRequest()
  }
  // code for IE
  else if (window.ActiveXObject)
  {
    xmlhttp = new ActiveXObject("Microsoft.XMLHTTP")
  }

  var date = new Date();
  var pinParam = "pin="+pin;
  xmlhttp.open('POST', '/FormValidation/IsPinValid.aspx?rnd='
      +date.getTime(), false);
  xmlhttp.setRequestHeader('Content-Type',
      'application/x-www-form-urlencoded');
  xmlhttp.send(pinParam);
  args.IsValid = eval(xmlhttp.responseText);  
    
  //xmlhttp.open('GET', '/IsPinValid.aspx?pin=' + pin, false);
  //xmlhttp.send(null);
  //args.IsValid = eval(xmlhttp.responseText);  
}
</script>
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
  <asp:CustomValidator ID="pinCustomValidator" 
    runat="server" 
    ErrorMessage="You used that PIN recently." 
    ControlToValidate="pinTextBox" 
    OnServerValidate="pinCustomValidator_ServerValidate"
    ClientValidationFunction="isValidPin"		
			/>
  <p>
    <asp:Button ID="submitButton" runat="server" Text="Change PIN" />
  </p>
</div>
    </form>
</body>
</html>
