<%@ Page Language="C#" AutoEventWireup="true" %>

<script runat="server">
void Page_Load(object sender, EventArgs e)
{
    System.IO.DirectoryInfo dir = 
        new System.IO.DirectoryInfo(Server.MapPath("images"));
    imageRepeater.DataSource = dir.GetFiles();
    imageRepeater.DataBind();
}
</script>

<html>
<head>
    <title>Image Handler Example</title>
    <style type="text/css">
        img {padding: 4px; float: left; clear: both;}
    </style>
</head>
<body>
    <asp:Repeater ID="imageRepeater" runat="server">
    <ItemTemplate>
        <img src='<%# "ProtectedImageHandler.ashx?image=images\\" 
            + Eval("Name") 
            + "&key=" + HotlinkProtection.GetKey() %>' />
    </ItemTemplate>
    </asp:Repeater>
</body>
</html>
