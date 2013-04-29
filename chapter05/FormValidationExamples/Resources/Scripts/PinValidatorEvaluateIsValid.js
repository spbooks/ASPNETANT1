function PinValidatorEvaluateIsValid(val) 
{
  var pin = ValidatorTrim(ValidatorGetValue(val.controltovalidate));
  var xmlhttp;
  if(window.XMLHttpRequest)
  {
    xmlhttp = new XMLHttpRequest();
  }
  else if (window.ActiveXObject) 
  {
     // ...otherwise, use the ActiveX control for IE5.x and IE6
     xmlhttp = new ActiveXObject('MSXML2.XMLHTTP.3.0');
  }
  
  //need to prevent caching.
  var date = new Date();
  xmlhttp.open('POST', '/IsPinValid.aspx?pin=' + pin + '&rnd=' + date.getTime(), false);
  xmlhttp.send(null);
  return eval(xmlhttp.responseText);  
} 
