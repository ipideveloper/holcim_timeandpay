<%@ Page Language="VB" AutoEventWireup="false" CodeFile="leave_approvers.aspx.vb"
    Inherits="leave_approvers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Approvers List</title>
    <link href="StyleSheet.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        BODY
        {
            margin: 0px;
            font-family: Arial, Sans-Serif, Verdana;
            font-size: 11px;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=70);
            opacity: 0.7px;
        }
        .popupControl
        {
            background-color: Red;
            position: absolute;
            visibility: hidden;
        }
        .Buttonhand1hover
        {
            cursor: hand;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel Style="background-repeat: repeat-x; text-align: center" ID="Panel2" runat="server"
            Width="100%" BorderColor="Black" BackColor="White">
            <center>
                <table style="width: 100%; text-align: left">
                    <tr style="color: #000000">
                        <td style="font-weight: bold; color: gray; border-bottom: gray 1px solid; padding-top: 10px;
                            padding-left: 5px; padding-bottom: 3px; font-size: 16px;">
                            Approvers
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="vertical-align: top; color: white; border-bottom: gray 1px solid;">
                            <asp:Panel ID="Panel3" runat="server" Width="100%">
                                <asp:GridView ID="gv_approvers" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CC9966"
                                    BackColor="White" AutoGenerateColumns="False" BorderStyle="None" CellPadding="4">
                                    <RowStyle Wrap="True" ForeColor="DimGray"></RowStyle>
                                    <Columns>
                                        <asp:BoundField DataField="employee_name" HeaderText="Name">
                                            <HeaderStyle Font-Size="11px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="approver_level" HeaderText="Level">
                                            <HeaderStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="status" HeaderText="Status">
                                            <HeaderStyle Font-Size="11px" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="approval_date" HeaderText="Approval Date">
                                            <HeaderStyle Font-Size="11px" />
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099"></FooterStyle>
                                    <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="#330099"></PagerStyle>
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle>
                                    <HeaderStyle BackColor="#990000" ForeColor="#FFFFCC"></HeaderStyle>
                                    <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="text-align: center">
                            <asp:Button ID="btn_close" runat="server" ForeColor="White" Font-Size="8pt" Width="87px"
                                Text="Close" BorderWidth="1px" OnClientClick="self.close();" BorderColor="DarkGray"
                                BackColor="Red" Font-Names="arial" ValidationGroup="Val"></asp:Button>
                        </td>
                    </tr>
                </table>
            </center>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
