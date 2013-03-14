<%@ Page Title="" Language="VB" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false" CodeFile="MaintenanceOBType.aspx.vb" Inherits="admin_MaintenanceOBType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Loader" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<div class="g12 nodrop">
        <div style="background-color: #FFFFFF;">
            <div style="float: left; margin-left: 10px;">
                <span style="font-size: 24px;">OB Type</span></div>
        </div>
        <div style="clear: both">
        </div>
        <p style="margin-left: 10px;">
            To add OB type, input required fields below.</p>
        <fieldset>
            <label>
                New OB Type</label>
          <section>
                <label for="ob_type_ID">ID&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator_TextBoxUsername" runat="server" ValidationGroup="adduser" 
                ControlToValidate="ob_type_ID" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label>
			    <div><asp:TextBox ID="ob_type_ID" runat="server" ValidationGroup="adduser"></asp:TextBox></div>
			</section>
			<section>
                <label for="ob_type">OB Type&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" runat="server" ValidationGroup="adduser" 
                ControlToValidate="ob_type" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label>
			    <div><asp:TextBox ID="ob_type" runat="server" ValidationGroup="adduser"></asp:TextBox></div>
			</section>
 
            <section>
				<div><asp:Button ID="Button_AddOBType" ValidationGroup="adduser" runat="server" Text="   Add New OB Type   "></asp:Button></div>
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
                        <asp:BoundField DataField="ob_type" HeaderText="OB Type" SortExpression="ob_type" />
                        
                        <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
                        <asp:CommandField ShowEditButton="true" EditText="Edit" />
                    </Columns>
                </asp:GridView>
            </div>
        </fieldset>
    </div>
    <asp:SqlDataSource ID="UserRoleSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
        SelectCommand="SELECT [ID], [ob_type], [IsActive] FROM [sys_ob_type]"
        UpdateCommand="UPDATE [sys_ob_type] Set [ob_type] = @ob_type, IsActive=@IsActive WHERE [ID] = @original_ID"
        UpdateCommandType="Text"
        OldValuesParameterFormatString="original_{0}">
    
            <UpdateParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
                <asp:Parameter Name="ob_type" Type="String" /> 
                <asp:Parameter Name="IsActive" Type="Boolean" /> 
            </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

