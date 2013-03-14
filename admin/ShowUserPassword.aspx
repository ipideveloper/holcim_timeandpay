<%@ Page Language="VB" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false" CodeFile="ShowUserPassword.aspx.vb" Inherits="admin_ShowUserPassword" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div class="g12 nodrop">
	<h1>User Password</h1>
	<p>Input Personnel no. to retrieve web password</p>
	<fieldset>
	    <section>
	        <label>Password</label>
	        <div>
	        <asp:TextBox ID="Web_Password" runat="server"></asp:TextBox>
	        </div>
	    </section>
	    <section>
	        <label for="input">Personnel No.</label>
			<div><asp:TextBox ID="employee_id" runat="server" required></asp:TextBox>
			</div>
		</section>
		<section>
		    <div><asp:Button ID="RetrieveButton" runat="server" Text="Retrieve Password"></asp:Button></div>
		</section>
	</fieldset>

</div>
</asp:Content>

