<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Hello &nbsp;<asp:LoginName ID="LoginName1" runat="server" />
        <asp:LoginStatus ID="LoginStatus1" runat="server" />
        <br />
        <br />
        <asp:LoginView ID="LoginView1" runat="server">
            <RoleGroups>
                <asp:RoleGroup Roles="Admins">
                    <ContentTemplate>
                        You are an admin!
                    </ContentTemplate>
                </asp:RoleGroup>
            </RoleGroups>
            <LoggedInTemplate>
                You are not an admin.
            </LoggedInTemplate>
        </asp:LoginView>
        
    </div>
    </form>
</body>
</html>
