<%@ Page Language="VB" EnableViewState="false" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false"
    CodeFile="MaintenanceApprovalStatus.aspx.vb" Inherits="admin_MaintenanceApprovalStatus"
    Title="Maintenance: Approval Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="g12 nodrop">
        <div style="background-color: #FFFFFF;">
            <div style="float: left; margin-left: 10px;">
                <span style="font-size: 24px;">Approval Status</span></div>
        </div>
        <div style="clear: both">
        </div>
        <p style="margin-left: 10px;">
            To add approval status, input required fields below.</p>
        <fieldset>
            <label>
                New Approval Status</label>
            <%--<section>
                <label for="username">ID&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator_TextBoxUsername" runat="server" ValidationGroup="adduser" 
                ControlToValidate="StatusID" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label><div><asp:TextBox ID="StatusID" runat="server" ValidationGroup="adduser"></asp:TextBox></div>
			</section>--%>
			<section>
                <label for="username">Approval Status&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" runat="server" ValidationGroup="adduser" 
                ControlToValidate="Status" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label><div><asp:TextBox ID="Status" runat="server" ValidationGroup="adduser"></asp:TextBox></div>
			</section>
 
            <section>
				<div><asp:Button ID="Button_AddNewApprovalStatus" ValidationGroup="adduser" runat="server" Text="   Add New Approval Status   "></asp:Button></div>
			</section>
        </fieldset>
        <fieldset>
            <label>Approval Status List</label>
            <div>
                <asp:GridView ID="GridView_UserType" runat="server" AllowSorting="True" DataSourceID="UserRoleSqlDataSource"
                    AutoGenerateColumns="False" DataKeyNames="ID">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                            SortExpression="ID" Visible="true" />
                        <asp:TemplateField HeaderText="Approval Status" SortExpression="Status">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="TextBox1" ID="RequiredFieldValidator2" runat="server" ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;&nbsp;Cannot be blank"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ControlStyle Width="90%" />
                        </asp:TemplateField>
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
                        <asp:CommandField ShowEditButton="true" EditText="Edit" />
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <asp:SqlDataSource ID="UserRoleSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
        SelectCommand="SELECT [ID], [Status], [IsActive] FROM [sys_approval_status]"
        UpdateCommand="UPDATE [sys_approval_status] Set [Status] = @status, IsActive=@IsActive WHERE [ID] = @original_ID"
        UpdateCommandType="Text"
        OldValuesParameterFormatString="original_{0}">
        <UpdateParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="status" Type="String" /> 
                <asp:Parameter Name="IsActive" Type="Boolean" /> 
            </UpdateParameters>
    </asp:SqlDataSource>
    
</asp:Content>
