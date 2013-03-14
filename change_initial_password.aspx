<%@ Page Language="VB" AutoEventWireup="false" CodeFile="change_initial_password.aspx.vb" Inherits="change_abc123_password" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style="font-size: 8pt; font-family: arial">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <center>
            <table style="border-top-width: thin; border-left-width: thin; border-left-color: silver;
                background-position-y: top; background-image: url(images/login_header.bmp); border-bottom-width: thin;
                border-bottom-color: silver; width: 900px; border-top-color: silver; background-repeat: no-repeat;
                height: 400px; border-right-width: thin; border-right-color: silver">
                <tr>
                    <td style="background-image: url(images/bg_ms.jpg); border-bottom-width: 1px; border-bottom-color: gray;
                        vertical-align: top; height: 65px; text-align: left">
                        .</td>
                </tr>
                <tr>
                    <td style="background-image: url(images/Bg_Up.JPG); vertical-align: top; background-repeat: repeat-x;
                        background-color: transparent; text-align: center">
                        <center>
                            <table style="background-position: right center; background-repeat: no-repeat; height: 100%"
                                width="800">
                                <tr>
                                    <td style="border-top-width: 1px; background-position: left bottom; border-left-width: 1px;
                                        border-left-color: black; background-attachment: fixed; border-bottom-width: 1px;
                                        border-bottom-color: black; vertical-align: top; width: 807px; border-top-color: black;
                                        background-repeat: no-repeat; height: 383px; text-align: right; border-right-width: 1px;
                                        border-right-color: black">
                                        <center style="background-position: right center; vertical-align: middle; background-repeat: no-repeat;
                                            text-align: center">
                                            &nbsp;</center>
                                        <center style="background-position: right center; vertical-align: middle; background-repeat: no-repeat;
                                            text-align: center">
                                            &nbsp;</center>
                                        <center style="background-position: right center; background-repeat: no-repeat">
                                            &nbsp;</center>
                                        <center>
                                            &nbsp;</center>
                                        <center style="text-align: right">
                                            &nbsp;</center>
                                        <center>
                                            &nbsp;</center>
                                        <center>
                                            &nbsp;</center>
                                        <center>
                                            &nbsp;</center>
                                        <center>
                                            &nbsp;</center>
                                        <center>
                                            <strong></strong>&nbsp;</center>
                                        <center style="color: red">
                                            &nbsp;</center>
                                        <center style="color: red">
                                            <strong><span style="font-size: 16pt">Welcome to the Time and Pay Online Application</span></strong></center>
                                        <center style="color: red">
                                            <span style="font-size: 12pt"><strong>You are required to change your initial password</strong></span></center>
                                        <center style="color: red">
                                            ___________________________________________________________________________________________________________________________________</center>
                                        <center style="background-position: right center; vertical-align: middle; background-repeat: no-repeat;
                                            text-align: center">
                                            &nbsp;</center>
                                        <center style="background-position: right center; vertical-align: middle; background-repeat: no-repeat;
                                            text-align: center">
                                            <table style="border-top: gray 1px solid; width: 403px; text-align: left">
                                                <tr style="color: #000000">
                                                    <td colspan="2" style="color: red; height: 17px">
                                                        <strong>* Required </strong>- Your new password must be 6-15 characters.</td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td colspan="1" style="width: 157px">
                                                        Personnel Number :</td>
                                                    <td colspan="2" style="height: 38px">
                                                        <asp:TextBox ID="TextBox1" runat="server" Enabled="False" Font-Size="8pt" ReadOnly="True"
                                                            Width="216px"></asp:TextBox></td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td style="width: 157px">
                                                        Enter your new password :</td>
                                                    <td style="width: 238px">
                                                        <asp:TextBox ID="txtnew" runat="server" Font-Size="8pt" TextMode="Password" Width="216px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtnew"
                                                            ErrorMessage="*" Font-Bold="True" Font-Size="14pt" ValidationGroup="Val"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtnew"
                                                            ErrorMessage="Minimum of 6 - 15 character!" ValidationExpression="(\s|.){6,15}"
                                                            ValidationGroup="Val"></asp:RegularExpressionValidator></td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td style="width: 157px">
                                                        Re-type your new password :</td>
                                                    <td style="width: 238px">
                                                        <asp:TextBox ID="txtretype" runat="server" Font-Size="8pt" TextMode="Password" Width="216px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtretype"
                                                            ErrorMessage="*" Font-Bold="True" Font-Size="14pt" ValidationGroup="Val"></asp:RequiredFieldValidator>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 157px">
                                                    </td>
                                                    <td style="width: 238px">
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtnew"
                                                            ControlToValidate="txtretype" ErrorMessage="New and retype password did not match"
                                                            ValidationGroup="Val"></asp:CompareValidator></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 157px; height: 45px">
                                                    </td>
                                                    <td style="width: 238px; height: 45px">
                                                        <asp:Button ID="btnChange" runat="server" BackColor="Red" BorderColor="DarkGray"
                                                            BorderWidth="1px" CssClass="Buttonhand1hover" Font-Names="arial" Font-Size="8pt"
                                                            ForeColor="White" OnClick="btnChange_Click" Text="Change" ValidationGroup="Val"
                                                            Width="87px" />
                                                        <asp:Button ID="btnBack" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                                                            CssClass="Buttonhand1hover" Font-Names="arial" Font-Size="8pt" ForeColor="White"
                                                            OnClick="btnChange_Click" Text="Log Out" Width="87px" /></td>
                                                </tr>
                                            </table>
                                        </center>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <table style="border-top: darkgray 1px solid; border-left-width: 1px; border-left-color: darkgray;
                            border-bottom-width: thin; border-bottom-color: darkgray; color: white; background-repeat: repeat-x;
                            height: 32px; text-align: left; border-right-width: 1px; border-right-color: darkgray"
                            width="100%">
                            <tr>
                                <td colspan="5" style="border-top-width: thin; font-weight: bold; border-left-width: thin;
                                    border-left-color: darkgray; border-bottom-width: thin; border-bottom-color: darkgray;
                                    color: gray; border-top-color: darkgray; border-right-width: thin; border-right-color: darkgray">
                                    Holcim Online Filing Version 1.0, Copyright @ 2010 All Rights Reserved.</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </center>
        <br />
    
    </div>
    </form>
</body>
</html>
