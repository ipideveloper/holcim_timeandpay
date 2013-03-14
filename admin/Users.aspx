<%@ Page Language="VB" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false"
    CodeFile="Users.aspx.vb" Inherits="admin_Users" Title="Admin Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="g12 nodrop">
        <div style="background-color: #FFFFFF;">
            <div style="float: left; margin-left: 10px;">
                <span style="font-size: 24px;">Users</span></div>
        </div>
        <div style="clear: both">
        </div>
        <p style="margin-left: 10px;">
            To add system users, input required fields below.</p>
        <fieldset>
            <label>
                New User</label>
            <section>
                <label for="username">User Type&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator_DropDownList_UserType" runat="server" 
                ControlToValidate="DropDownList_UserType" ValidationGroup="adduser" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label><div style="padding-top:0px !important; padding-bottom:0px !important;"><asp:DropDownList ID="DropDownList_UserType" runat="server" 
                        AppendDataBoundItems="True" DataSourceID="UserRoleSqlDataSource" 
                        DataTextField="UserRole" DataValueField="ID" >
			        <asp:ListItem Value="">Select User Type...</asp:ListItem>
			        </asp:DropDownList></div>
			</section>
            <section>
                <label for="username">Username&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator_TextBoxUsername" runat="server" ValidationGroup="adduser" 
                ControlToValidate="username" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label>
			    <div><asp:TextBox ID="username" runat="server" ValidationGroup="adduser"></asp:TextBox></div>
			</section>
			<section>
                <label for="employeeid">Employee ID&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" runat="server" ValidationGroup="adduser" 
                ControlToValidate="employeeid" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label>
			    <div><asp:TextBox ID="employeeid" runat="server" ValidationGroup="adduser"></asp:TextBox></div>
			</section>
            <section>
                <label for="password">Password<%--<br /><br />Confirm Password--%></label>
				<div>Default password is 'abc123'<%--<asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox><br />
				<asp:TextBox ID="confirmpassword" runat="server" TextMode="Password"></asp:TextBox>--%></div>
			</section>
            <section>
				<div><asp:Button ID="Button_AddNewUser" ValidationGroup="adduser" runat="server" Text="   Add New User   "></asp:Button></div>
			</section>
        </fieldset>
        <fieldset>
            <label>
                Browse Users</label>
            <div>
                <asp:GridView ID="GridView_Users" runat="server" AllowSorting="True" DataSourceID="UsersSqlDataSource"
                    AutoGenerateColumns="False" DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                            SortExpression="ID" Visible="False" />
                        <asp:BoundField DataField="Username" ReadOnly="true" HeaderText="Username" SortExpression="Username" />
                        <asp:BoundField DataField="Employee" HeaderText="Employee" ReadOnly="True" SortExpression="Employee"
                            Visible="False" />
                        <asp:TemplateField HeaderText="Employee" SortExpression="Employee">
                            <ItemTemplate>
                                <asp:Label ID="EmployeeLabel" runat="server" Text='<%#Eval("Employee") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                <asp:Label ID="EmployeeLabel" runat="server" Text='<%#Eval("Employee") %>'></asp:Label>
                             </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Type" SortExpression="UserRole">
                            <ItemTemplate>
                                <asp:Label ID="UserRoleLabel" runat="server" Text='<%#Eval("UserRole") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                               <asp:DropDownList ID="UserRoleID" runat="server" DataSourceID="UserRoleSqlDataSource"
                                                DataTextField="UserRole" DataValueField="ID"
                                                SelectedValue='<%# Bind("UserRoleID", "{0}") %>'>
                                </asp:DropDownList>
                                
                             </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DateCreated" HeaderText="Date Created" ReadOnly="True"
                            SortExpression="DateCreated">
                            <HeaderStyle Width="140px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DateLastLogin" HeaderText="Last Login" ReadOnly="True"
                            SortExpression="DateLastLogin">
                            <HeaderStyle Width="140px" />
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
                        <asp:CommandField ShowEditButton="true" EditText="Edit" />
                        
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <asp:SqlDataSource ID="UserRoleSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
        SelectCommand="SELECT [ID], [UserRole] FROM [z_sysmanager_user_roles] WHERE ([IsActive] = @IsActive)">
        <SelectParameters>
            <asp:Parameter DefaultValue="True" Name="IsActive" Type="Boolean" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="UsersSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
        SelectCommand="spWeb_Get_SysManager_Users" SelectCommandType="StoredProcedure"
        UpdateCommand="UPDATE [z_sysmanager_users ] SET UserRoleID = @UserRoleID, IsActive=@IsActive WHERE [ID] = @original_ID" 
        UpdateCommandType="Text"
        OldValuesParameterFormatString="original_{0}"
        >
        <UpdateParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="UserRoleID" Type="Int32" />
                <asp:Parameter Name="IsActive" Type="Boolean" /> 
            </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
