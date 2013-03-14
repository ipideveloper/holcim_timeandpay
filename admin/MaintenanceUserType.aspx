<%@ Page Language="VB" EnableViewState="false" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false"
    CodeFile="MaintenanceUserType.aspx.vb" Inherits="admin_MaintenanceUserType" Title="Maintenance: User Type" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="g12 nodrop">
        <div style="background-color: #FFFFFF;">
            <div style="float: left; margin-left: 10px;">
                <span style="font-size: 24px;">User Type</span></div>
        </div>
        <div style="clear: both">
        </div>
        <p style="margin-left: 10px;">
            To add user type, input required fields below.</p>
        <fieldset>
            <label>
                New User Type</label>
<%--            <section>
                <label for="UserTypeID">ID&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator_TextBoxUsername" runat="server" ValidationGroup="adduser" 
                ControlToValidate="UserTypeID" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label>
			    <div><asp:TextBox ID="UserTypeID" runat="server" ValidationGroup="adduser"></asp:TextBox></div>
			</section>--%>
			<section>
                <label for="UserType">User Type&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" runat="server" ValidationGroup="adduser" 
                ControlToValidate="UserType" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label>
			    <div><asp:TextBox ID="UserType" runat="server" ValidationGroup="adduser"></asp:TextBox></div>
			</section>
 
            <section>
				<div><asp:Button ID="Button_AddUserType" ValidationGroup="adduser" runat="server" Text="   Add New User Type   "></asp:Button></div>
			</section>
        </fieldset>
        <fieldset>
            <label>User Type List</label>
            <div>
                <asp:GridView ID="GridView_UserType" runat="server" AllowSorting="True" DataSourceID="UserRoleSqlDataSource"
                    AutoGenerateColumns="False" DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                            SortExpression="ID" Visible="true" />
                        <asp:BoundField DataField="UserRole" HeaderText="User Type" SortExpression="UserRole" />
                        
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
                        <asp:CommandField ShowEditButton="true" EditText="Edit" />
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <asp:SqlDataSource ID="UserRoleSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
        SelectCommand="SELECT [ID], [UserRole], [IsActive] FROM [z_sysmanager_user_roles]"
        UpdateCommand="UPDATE [z_sysmanager_user_roles] Set [UserRole] = @UserRole, IsActive=@IsActive WHERE [ID] = @original_ID"
        UpdateCommandType="Text"
        OldValuesParameterFormatString="original_{0}">
    
            <UpdateParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="UserRole" Type="String" /> 
                <asp:Parameter Name="IsActive" Type="Boolean" /> 
            </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
