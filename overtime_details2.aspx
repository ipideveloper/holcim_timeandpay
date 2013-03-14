<%@ Page Language="VB" AutoEventWireup="false" CodeFile="overtime_details2.aspx.vb" Inherits="overtime_details2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Holcim - Overtime Details</title>
</head>
<body style="font-size: 8pt; font-family: arial">
    <form id="form1" runat="server">
    <div>
        <center>
            &nbsp;</center>
        <center>
            <table style="border-right: red 1px solid; border-top: red 1px solid; border-left: red 1px solid;
            width: 465px; border-bottom: red 1px solid; background-color: transparent; text-align: left">
            <tr>
                <td style="vertical-align: top; width: 339px; height: 39px">
                    <img src="images/logo.jpg" /></td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 339px">
                    <table style="width: 451px; background-color: transparent">
                        <tr>
                            <td style="width: 95px">
                                Employee Name :</td>
                            <td>
                                <asp:TextBox ID="txt_name" runat="server" Font-Size="8pt" ReadOnly="True" Width="219px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Personnel No :</td>
                            <td>
                                <asp:TextBox ID="txt_emp_id" runat="server" Font-Size="8pt" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Ref. No.</td>
                            <td>
                                <asp:TextBox ID="txt_ref_no" runat="server" Font-Size="8pt" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Planner :</td>
                            <td>
                                <asp:TextBox ID="txt_planner" runat="server" Font-Size="8pt" ReadOnly="True" Width="219px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Date and Time :</td>
                            <td>
                                <asp:TextBox ID="txt_date_time" runat="server" Font-Size="8pt" ReadOnly="True"
                                    Width="258px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Classification :</td>
                            <td>
                                <asp:DropDownList ID="ddl_classification" runat="server" Enabled="False" Font-Size="8pt" Width="261px">
                                    <asp:ListItem Value="0">-</asp:ListItem>
                                    <asp:ListItem Value="1">With Warm Body Replacement</asp:ListItem>
                                    <asp:ListItem Value="2">With Out Warm Body Replacement</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Reason :</td>
                            <td>
                                <asp:TextBox ID="txt_reasons" runat="server" Font-Size="8pt" ReadOnly="True" Width="338px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                On Call :</td>
                            <td>
                                <asp:TextBox ID="txt_on_call" runat="server" Font-Size="8pt" ReadOnly="True" Width="49px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Date Filed :
                            </td>
                            <td>
                                <asp:TextBox ID="txt_date_filed" runat="server" Font-Size="8pt" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Remarks :</td>
                            <td>
                                <asp:TextBox ID="txt_remarks" runat="server" Font-Size="8pt" Height="86px" ReadOnly="True"
                                    TextMode="MultiLine" Width="338px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Status :</td>
                            <td>
                                <asp:TextBox ID="txt_status" runat="server" Font-Size="8pt" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </center>
    
    </div>
    </form>
</body>
</html>
