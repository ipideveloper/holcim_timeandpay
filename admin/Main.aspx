<%@ Page Language="VB" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false"
    CodeFile="Main.aspx.vb" Inherits="admin_Main" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    <div class="g12 nodrop">
        <div style="background-color: #FFFFFF;">
            <div style="float: left; margin-left: 10px;">
                <span style="font-size: 24px;">Welcome</span></div>
        </div>
        <div style="clear: both">
        </div>
        <p style="margin-left: 10px;">
            Welcome to Time and Pay System Manager.</p>
            <div class="g12"><h4 style="color:#DD6153">Quick Links</h4>
            </div>
            <asp:Panel ID="Panel_ConcurModule" runat="server">
            <div class="g3">
				<h4>Concur</h4>
				<p class="j">
					<li><a href="Concur.aspx">Concur/Travel Order Settings</a></li>
				    <li><a href="ConcurUpload.aspx">Data Import</a></li>
				    <li><a href="TravelOrderData.aspx">Travel Order Data</a></li>
				    <li><a href="ConcurLogs.aspx">Concur System Logs</a></li>
				</p>
			</div>
            </asp:Panel>
            <asp:Panel ID="Panel_SystemAdmin" runat="server">
			<div class="g3">
				<h4>System Administration</h4>
				<p class="j">
					<li><a href="MaintenanceApprovalStatus.aspx">Maintenance: Approval Status</a></li>
					<li><a href="MaintenanceUserType.aspx">Maintenance: User Type</a></li>
					<li><a href="Users.aspx">Users</a></li>
					<li><a href="SystemLogs.aspx">System Logs</a></li>
			    </p>
			</div>
			</asp:Panel>
    </div>
</asp:Content>
