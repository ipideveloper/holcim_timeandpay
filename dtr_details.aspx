<%@ Page Language="VB" AutoEventWireup="false" CodeFile="dtr_details.aspx.vb" Inherits="dtr_details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Holcim - DTR Application Details</title>
</head>
<body style="font-size: 8pt; font-family: arial">
    <form id="form1" runat="server">
    <div>
        <center>
            <table style="border-right: red 1px solid; border-top: red 1px solid; border-left: red 1px solid;
                width: 465px; border-bottom: red 1px solid; background-color: transparent; text-align: left">
                <tr>
                    <td style="vertical-align: top; width: 339px; height: 39px">
                        <img src="images/logo.jpg" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; width: 339px">
                        <table style="width: 451px; background-color: transparent">
                            <tr>
                                <td style="width: 95px">
                                    Employee Name :
                                </td>
                                <td>
                                    <asp:TextBox ID="employee_name" runat="server" Font-Size="8pt" ReadOnly="True" Width="219px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 95px">
                                    Personnel No :
                                </td>
                                <td>
                                    <asp:TextBox ID="employee_id" runat="server" Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 95px">
                                    Ref. No.
                                </td>
                                <td>
                                    <asp:TextBox ID="ref_no" runat="server" Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 95px">
                                    Date Filed :
                                </td>
                                <td>
                                    <asp:TextBox ID="date_created" runat="server" Font-Size="8pt" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%">
                            <RowStyle ForeColor="DimGray" Wrap="True" />
                            <Columns>
                                <asp:BoundField HeaderStyle-Width="125" ItemStyle-HorizontalAlign="Center" DataField="tran_date"
                                    HeaderText="Date/Time" />
                                <asp:BoundField HeaderStyle-Width="110" ItemStyle-HorizontalAlign="Center" DataField="tran_type"
                                    HeaderText="Transaction Type" />
                                <asp:BoundField ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                                    DataField="reason" HeaderText="Reason" />
                            </Columns>
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                            <HeaderStyle BackColor="#990000" Font-Bold="False" ForeColor="#FFFFCC" />
                            <AlternatingRowStyle BackColor="#E0E0E0" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br />
            <input id="Button1" type="button" value="Close Window" style="background-color: Red;
                border: 1px solid DarkGray; font-size: 8pt; color: #FFFFFF; width: 87px;" class="Buttonhand1hover"
                onclick="self.close()" />
        </center>
    </div>
    </form>
</body>
</html>
