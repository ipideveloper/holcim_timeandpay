<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holcim - User Login </title>
</head>
<body style="font-size: 8pt; font-family: arial">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <br />
                    <center><table style="border-top-width: thin; border-left-width: thin; border-left-color: silver;
                border-bottom-width: thin; border-bottom-color: silver; width: 900px; border-top-color: silver;
                height: 400px; border-right-width: thin; border-right-color: silver; background-position-y: top; background-image: url(images/login_header.bmp); background-repeat: no-repeat;">
                <tr>
                    <td style="background-image: url(images/bg_ms.jpg); vertical-align: top;
                        text-align: left; border-bottom-width: 1px; border-bottom-color: gray; height: 65px;">
                        .</td>
                </tr>
                <tr>
                    <td style="background-image: url(images/Bg_Up.JPG); vertical-align: top; background-repeat: repeat-x;
                        background-color: transparent; text-align: center">
                        <center><table style="background-position: right center;
                            background-repeat: no-repeat; height: 100%" width="800">
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
                                    <center style="background-position: right center; background-repeat: no-repeat;">
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
                                    <center style="color: red">
                                        <strong></strong>&nbsp;</center>
                                    <center style="color: red">
                                        &nbsp;</center>
                                    <center style="color: red">
                                        <strong><span style="font-size: 16pt">Welcome to the Time and Pay Online Application</span></strong></center>
                                    <center style="color: red">
                                        <span style="font-size: 12pt">
                                        Enter your Personnel No. and Password to log on to the
                                            system</span></center>
                                    <center style="color: red">
                                        ___________________________________________________________________________________________________________________________________</center>
                                    <center style="background-position: right center; vertical-align: middle; background-repeat: no-repeat;
                                        text-align: center">
                                        &nbsp;</center>
                                    <center style="background-position: right center; vertical-align: middle; background-repeat: no-repeat;
                                        text-align: center">
                                        <center><table style="font-weight: bold; width: 296px; color: gray">
                                            <tr>
                                                <td style="width: 910px; height: 53px; text-align: left">
                                                </td>
                                                <td style="width: 422px; height: 53px; text-align: left">
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="Smaller"
                                                        HeaderText="Invalid Username or Password." Height="1px" ValidationGroup="ValG"
                                                        Width="195px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 910px; text-align: left">
                                                    Personnel No.</td>
                                                <td style="text-align: left; width: 422px;">
                                                    <asp:TextBox ID="TextBox1" runat="server" BackColor="White" BorderColor="DarkGray"
                                                        BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Width="178px" TabIndex="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                                        ErrorMessage="Invalid PERSONNEL NO" Font-Bold="True" Font-Size="14pt" ValidationGroup="ValG" Width="2px">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 910px; text-align: left; height: 41px;">
                                                    Password</td>
                                                <td style="text-align: left; height: 41px; width: 422px;">
                                                    <asp:TextBox ID="TextBox2" runat="server" BackColor="White" BorderColor="DarkGray"
                                                        BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" TextMode="Password"
                                                        Width="178px" TabIndex="2"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                                                        ErrorMessage="PASSWORD cannot be blank." Font-Bold="True" Font-Size="14pt" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                                                    <asp:LinkButton ID="forgotpassword" runat="server" TabIndex="3" Width="138px" ToolTip=" ">Forgot your password?</asp:LinkButton></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 910px; text-align: left">
                                                </td>
                                                <td style="text-align: left; width: 422px;">
                                                    <br />
                                                    <asp:Button ID="btnLogin" runat="server" BackColor="Red" BorderWidth="1px" Font-Names="arial" onmouseover = "this.style.cursor = 'hand'"
                                                        Font-Size="8pt" ForeColor="White"  Text="Login" ValidationGroup="ValG"
                                                        Width="89px" TabIndex="4" Font-Overline="False" Font-Underline="False" CssClass="Buttonstyle1hover"  /></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 35px; text-align: left; color: white; border-bottom-width: 1px; border-bottom-color: red;" colspan="2">
                                                    .</td>
                                            </tr>
                                        </table></center>
                                    </center>
                                </td>
                            </tr>
                        </table></center>
                        <table style="border-top: darkgray 1px solid; border-left-width: 1px; border-left-color: darkgray;
                            border-bottom-width: thin; border-bottom-color: darkgray; color: white; background-repeat: repeat-x;
                            height: 32px; text-align: left; border-right-width: 1px;
                            border-right-color: darkgray" width="100%">
                            <tr>
                                <td colspan="5" style="border-top-width: thin; font-weight: bold; border-left-width: thin; border-left-color: darkgray; border-bottom-width: thin; border-bottom-color: darkgray; color: gray; border-top-color: darkgray; border-right-width: thin; border-right-color: darkgray;">
                                    Holcim Online Filing Version 1.1.8, Copyright @ 2011 All Rights Reserved.</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table></center>
        <br />
        
    </form>
</body>
</html>
