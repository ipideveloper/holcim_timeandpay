<%@ Page Language="VB" AutoEventWireup="false" CodeFile="oncall_details.aspx.vb" Inherits="oncall_details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Holcim - OnCall Details</title>
</head>
<body style="font-size: 8pt; font-family: arial">
    <form id="form1" runat="server">
    <div>
        <center>
            &nbsp;</center>
        <center>
            <table style="border-right: red 1px solid; border-top: red 1px solid; border-left: red 1px solid;
            width: 396px; border-bottom: red 1px solid; background-color: transparent; text-align: left">
            <tr>
                <td style="vertical-align: top; width: 339px; height: 39px">
                    <img src="images/logo.jpg" /></td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 339px">
                <table style="width: 381px; background-color: transparent">
                        <tr>
                            <td style="width: 95px">
                                Employee Name :</td>
                            <td>
                                <asp:TextBox ID="txt_name" runat="server" Font-Size="8pt" ReadOnly="True" Width="219px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Personnel No. :</td>
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
                                Date Covered :</td>
                            <td>
                                <asp:TextBox ID="txt_date_covered" runat="server" Font-Size="8pt" ReadOnly="True"
                                    Width="265px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Contacted by :</td>
                            <td>
                                <asp:TextBox ID="txt_contacted_by" runat="server" Font-Size="8pt" ReadOnly="True" Width="219px"></asp:TextBox></td>
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
                                Reason :</td>
                            <td>
                                <asp:TextBox ID="txt_reason" runat="server" Font-Size="8pt" Height="86px" ReadOnly="True"
                                    TextMode="MultiLine" Width="265px"></asp:TextBox></td>
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
