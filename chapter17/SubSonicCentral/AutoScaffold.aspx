<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoScaffold.aspx.cs" Inherits="AutoScaffold" ValidateRequest="false" EnableEventValidation="false" Theme="Default" MasterPageFile="~/res/MasterPage.master" Title="SubSonic AutoScaffold" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFull" Runat="Server">
<table style="width: 100%" id="tblWrapper" runat="server">
	<tr>
		<td style="white-space:nowrap;font-size:8pt;font-family:'Segoe UI', Verdana, Tahoma; background-color:#d3d3d3">
			<div style="font-weight:bold;padding:2px 0px 4px 2px">Provider</div>
			<asp:DropDownList ID="ddlProviders" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProviders_SelectedIndexChanged" CssClass="scaffoldEditItem">
			</asp:DropDownList>
			<div style="font-weight:bold;padding:10px 0px 4px 2px">Tables</div>
			<asp:Panel ID="pnlTableList" runat="server"></asp:Panel>
			<div style="padding-top:15px; padding-bottom:15px; text-align:center">
			<a href="?refresh=true">Refresh Providers</a>
			</div>
			</td>
		<td style="width: 100%">
			<div style="font-size:24px;padding:10px;font-weight:bold"><asp:Label ID="lblHeader" runat="server">AutoScaffold</asp:Label></div>
			<asp:Panel ID="pnlButtons" runat="server"></asp:Panel>
			<asp:Panel ID="pnlGridView" runat="server" Width="100%">
				<asp:GridView ID="grid" runat="server" SkinID="scaffold" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="50" OnPageIndexChanging="grid_PageIndexChanging" OnSorting="grid_Sorting">
				</asp:GridView>
			</asp:Panel>
			<asp:Panel ID="pnlDetail" runat="server" Width="100%">
				<table id="tblEditor" runat="server" width="600px"></table>
			</asp:Panel>
		</td>
	</tr>
</table>
<asp:Panel ID="pnlError" runat="server" Visible="false">
<h2>Connection Error</h2>
<p>SubSonic was unable to load a provider or connect to a database. This is usually caused by a configuration error in Web.Config.</p>

	<div class="Box">
		<div class="BoxTitle">
			SubSonic Error
		</div>
		<div class="BoxContent">
			<p style="font-weight:bold">
				<asp:Label ID="lblError" runat="server">
				</asp:Label>
		</p>
		</div>
	</div>
<p>Please check your configuration and try again.</p>
</asp:Panel>
</asp:Content>

