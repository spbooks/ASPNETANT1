<%@ Page Language="C#" AutoEventWireup="true" CodeFile="URLRewriting.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Links to rewritten pages</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                Refrigerators</p>
            <ul>
                <li><a href="/refrigerators/stainless">View our stainless steel refrigerators</a></li>
                <li><a href="/refrigerators/standard">View our standard refrigerators</a> </li>
            </ul>
            <p>
                General Appliances</p>
            <ul>
                <li><a href="/appliances/kitchen">View our kitchen appliances</a></li>
                <li><a href="/appliances/laundry">View our laundry appliances</a></li>
            </ul>
        </div>
    </form>
</body>
</html>
