<%@ Page Language="VB" AutoEventWireup="false" CodeFile="leave_details.aspx.vb" Inherits="leave_details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Holcim - Leave Details</title>
</head>
<body style="font-size: 8pt; font-family: arial">
    <form id="form1" runat="server">
    <div style="background-position: left top; background-repeat: no-repeat">
        &nbsp;</div>
        <center><table style="width: 705px; background-color: transparent; border-right: red 1px solid; border-top: red 1px solid; border-left: red 1px solid; border-bottom: red 1px solid; text-align: left;">
            <tr>
                <td style="vertical-align: top; width: 339px; height: 39px">
                    <img src="images/logo.jpg" /></td>
                <td rowspan="1" style="font-size: 8pt; vertical-align: bottom; text-align: left">
                    List of Date Covered</td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 339px">
                    <table style="width: 336px; background-color: transparent;">
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
                                Leave Type :</td>
                            <td>
                                <asp:TextBox ID="txt_leave_type" runat="server" Font-Size="8pt" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Date Covered :</td>
                            <td>
                                <asp:TextBox ID="txt_date_covered" runat="server" Font-Size="8pt" ReadOnly="True" Width="156px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 95px">
                                Total Days :</td>
                            <td>
                                <asp:TextBox ID="txt_total_days" runat="server" Font-Size="8pt" ReadOnly="True" Width="47px"></asp:TextBox></td>
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
                                    TextMode="MultiLine" Width="219px"></asp:TextBox></td>
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
                <td rowspan="1" style="font-size: 8pt; vertical-align: top">
                    <asp:Panel ID="Panel1" runat="server" Height="320px" ScrollBars="Vertical" Width="350px">
                    <asp:GridView ID="gv_dates" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="95%">
                        <RowStyle ForeColor="DimGray" Wrap="True" />
                        <Columns>
                            <asp:BoundField DataField="workdate" HeaderText="Date" />
                            <asp:TemplateField HeaderText="Halfday">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_half" runat="server" Checked='<%# bind("half_day") %>' Enabled="False" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:RadioButton ID="r_first" runat="server" Enabled="False" GroupName="halfday"
                                        Text="1st half" Checked='<%# bind("first_half") %>' />
                                    <asp:RadioButton ID="r_second" runat="server" Enabled="False" GroupName="halfday"
                                        Text="2nd half" Checked='<%# bind("second_half") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Holiday">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_holiday" runat="server" Checked='<%# bind("chk_holiday") %>'
                                        Enabled="False" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                        <AlternatingRowStyle BackColor="#E0E0E0" />
                    </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
        </table></center>
    </form>
</body>
</html>
