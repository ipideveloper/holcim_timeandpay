<%@ Page Language="VB" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false"
    CodeFile="ChangePassword.aspx.vb" Inherits="admin_ChangePassword" Title="Account Settings / Change Password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div class="g12 nodrop">
        <div style="background-color: #FFFFFF;">
            <div style="float: left; margin-left: 10px;">
                <span style="font-size: 24px;">Change Password</span></div>
        </div>
        <div style="clear: both">
        </div>
        <p style="margin-left: 10px;">To reset your password, provide your current password </p>
        
        <asp:Panel ID="SuccessMessage" Visible="false" runat="server" CssClass="alert success">
        Password changed successfully!
        </asp:Panel>
        
        <fieldset>
            <section>
                <label for="CurrentPassword">Current Password&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator_CurrentPassword" runat="server" 
                ControlToValidate="CurrentPassword" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;" /></label>
                <div><asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox><asp:Label ID="Label_CurrentPassword" runat="server" Text=""></asp:Label></div>
            </section>
            <section>
                <label for="username">New Password&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator_NewPassword" runat="server" 
                ControlToValidate="NewPassword" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;" /></label>
                <div><asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="ConfirmPassword" ControlToCompare="NewPassword" runat="server" ErrorMessage="* password not the same"></asp:CompareValidator>
                </div>
            </section>
            <section>
                <label for="username">Confirm Password&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator_ConfirmPassword" runat="server" 
                ControlToValidate="ConfirmPassword" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;" /></label>
                <div><asp:TextBox ID="ConfirmPassword" runat="server" TextMode="Password"></asp:TextBox></div>
            </section>
            <section>
                <div><asp:Button ID="Button_ChangePassword" runat="server" Text="Change Password"></asp:Button>&nbsp;&nbsp;<asp:Button ID="Button_Cancel" CausesValidation="false" runat="server" Text="Cancel"></asp:Button></div>
            </section>
        </fieldset>
    </div>
</asp:Content>
