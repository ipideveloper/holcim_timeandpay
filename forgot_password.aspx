<%@ Page Language="VB" AutoEventWireup="false" CodeFile="forgot_password.aspx.vb" Inherits="forgot_password" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Forgot Password</title>
    <style type="text/css">
        .Buttonhand1hover
        {
            height: 20px;
        }
    </style>
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
                                            <span style="font-size: 12pt">
                                            Enter your Personnel No. and your password will be emailed&nbsp; to you</span></center>
                                        <center style="color: red">
                                            ___________________________________________________________________________________________________________________________________</center>
                                        <center style="background-position: right center; vertical-align: middle; background-repeat: no-repeat;
                                            text-align: center">
                                            &nbsp;</center>
                                        <center style="background-position: right center; vertical-align: middle; background-repeat: no-repeat;
                                            text-align: center">
                                            <center>
                                                <table style="font-weight: bold; width: 296px; color: gray">
                                                    <tr>
                                                        <td style="width: 87px; height: 12px; text-align: left">
                                                        </td>
                                                        <td style="width: 210px; height: 12px; text-align: left">
                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Incomplete data.  Cannot process"
                                                                ValidationGroup="ValG" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px; height: 44px; text-align: left">
                                                            Personnel No.</td>
                                                        <td style="height: 44px; text-align: left; width: 210px;">
                                                            <asp:TextBox ID="TextBox1" runat="server" BackColor="White" BorderColor="DarkGray"
                                                                BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Width="178px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                                                ErrorMessage="We need your PERSONNEL NO to retrieve your email." Font-Bold="True" Font-Size="Large" ValidationGroup="ValG" Width="2px">*</asp:RequiredFieldValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 87px; text-align: left">
                                                        </td>
                                                        <td style="text-align: left; width: 210px;">
                                                            <br />
                                                            <asp:Button ID="btnLogin" runat="server" BackColor="Red" BorderWidth="1px" Font-Names="arial"
                                                                Font-Size="8pt" ForeColor="White" Text="Submit" ValidationGroup="ValG" Width="89px" CssClass="Buttonhand1hover" />
                                                            <asp:Button ID="Back" runat="server" BackColor="Red" BorderWidth="1px" Font-Names="arial"
                                                                Font-Size="8pt" ForeColor="White" Text="Back" Width="89px" CssClass="Buttonhand1hover" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="border-bottom-width: 1px; border-bottom-color: red; color: white;
                                                            height: 35px; text-align: left">
                                                            .</td>
                                                    </tr>
                                                </table>
                                            </center>
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
