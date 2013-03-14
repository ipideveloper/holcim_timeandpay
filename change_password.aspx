<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="change_password.aspx.vb" Inherits="change_password" title="Holcim - Change Password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%; height: 274px">
        <tr>
            <td style="text-align: center">
                &nbsp;<br />
                 <strong style="color: red">
                     <br />
                     <br />
                     <br />
                * Required </strong>- Your new password must be 6-15 characters<br />
                <center><table style="width: 403px; text-align: left; border-top: gray 1px solid;">
                    <tr>
                        <td style="width: 157px; height: 49px;">
                            Enter your old password :</td>
                        <td style="height: 49px; width: 238px;">
                            <asp:TextBox id="txtold" runat="server" Font-Size="8pt" Width="216px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtold"
                                ErrorMessage="*" Font-Bold="True" Font-Size="14pt" ValidationGroup="Val"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td style="height: 38px;" colspan="2">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 157px">
                            Enter your new password :</td>
                        <td style="width: 238px">
                            <asp:TextBox id="txtnew" runat="server" Font-Size="8pt" Width="216px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtnew"
                                ErrorMessage="*" Font-Bold="True" Font-Size="14pt" ValidationGroup="Val"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtnew"
                                ErrorMessage="Minimum of 6 - 15 character!" ValidationExpression="(\s|.){6,15}"
                                ValidationGroup="Val"></asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td style="width: 157px">
                            Re-type your new password :</td>
                        <td style="width: 238px">
                            <asp:TextBox id="txtretype" runat="server" Font-Size="8pt" Width="216px" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtretype"
                                ErrorMessage="*" Font-Bold="True" Font-Size="14pt" ValidationGroup="Val"></asp:RequiredFieldValidator>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 157px;">
                        </td>
                        <td style="width: 238px">
                            <asp:CompareValidator id="CompareValidator1" runat="server" ControlToCompare="txtnew"
                                ControlToValidate="txtretype" ErrorMessage="New and retype password did not match"
                                ValidationGroup="Val"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="color: white; border-bottom: gray 1px solid; height: 20px">
                            .</td>
                    </tr>
                    <tr>
                        <td style="width: 157px; height: 45px;">
                        </td>
                        <td style="height: 45px; width: 238px;">
                            <asp:Button id="btnChange" runat="server" BackColor="Red" BorderColor="DarkGray"
                                BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Change"
                                ValidationGroup="Val" Width="87px" OnClick="btnChange_Click" CssClass="Buttonhand1hover" />
                            <asp:Button ID="btncancel" runat="server" BackColor="Red" BorderColor="DarkGray"
                                BorderWidth="1px" CssClass="Buttonhand1hover" Font-Names="arial,8pt" ForeColor="White"
                                Height="19px" Text="Cancel" Width="87px" Font-Size="8pt" /></td>
                    </tr>
                </table></center>
                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="True"
                    ConfirmText="Are you sure you want to change your password?" TargetControlID="btnChange">
                </cc1:ConfirmButtonExtender>
                &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

