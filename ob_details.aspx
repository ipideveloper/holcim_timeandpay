<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ob_details.aspx.vb" Inherits="ob_details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Holcim - OB Details</title>
</head>
<body style="font-size: 8pt; font-family: arial">
    <form id="form1" runat="server">
    <div>
        <center><table style="border-right: red 1px solid; border-top: red 1px solid; border-left: red 1px solid;
            width: 749px; border-bottom: red 1px solid; background-color: transparent; text-align: left">
            <tr>
                <td style="vertical-align: top; width: 339px; height: 39px">
                    <img src="images/logo.jpg" /></td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 339px">
                    <table style="width: 740px; text-align: left">
                        <tr style="color: #000000">
                            <td colspan="5" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                                height: 16px">
                                SECTION 1: TRAVELER INFORMATION&nbsp; <asp:LinkButton ID="lnk_print" runat="server"
                                    Font-Underline="False">[Print]</asp:LinkButton></td>
                        </tr>
                        <tr style="color: #000000">
                            <td style="width: 116px; height: 16px">
                                Reference No.</td>
                            <td colspan="2" style="height: 16px">
                                <asp:TextBox ID="txt_ref_no" runat="server" Font-Size="8pt" ReadOnly="True" Width="105px"></asp:TextBox></td>
                            <td colspan="1" style="width: 72px; color: #000000; height: 16px">
                            </td>
                            <td colspan="1" style="height: 16px">
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td style="width: 116px; height: 16px">
                                Employee Name :</td>
                            <td colspan="2" style="height: 16px">
                                <span style="color: #000000">
                                    <asp:TextBox ID="txt_employee_name" runat="server" Font-Size="8pt" ReadOnly="True"
                                        Width="341px"></asp:TextBox></span></td>
                            <td colspan="1" style="width: 72px; color: #000000; height: 16px">
                                Personnel No.</td>
                            <td colspan="1" style="height: 16px">
                                <asp:TextBox ID="txt_employee_id" runat="server" Font-Size="8pt" ReadOnly="True"
                                    Width="126px"></asp:TextBox></td>
                        </tr>
                        <tr style="color: #000000">
                            <td style="width: 116px; height: 16px">
                                Type of Travel :</td>
                            <td colspan="2" style="height: 16px">
                                <asp:TextBox ID="txt_travel_type" runat="server" Font-Size="8pt" ReadOnly="True"
                                    Width="105px"></asp:TextBox></td>
                            <td colspan="1" style="width: 72px; height: 16px">
                                Contact No. :&nbsp;</td>
                            <td colspan="1" style="height: 16px">
                                <asp:TextBox ID="txtcontactno" runat="server" Font-Size="8pt" ReadOnly="True" Width="126px"></asp:TextBox></td>
                        </tr>
                        <tr style="color: #000000">
                            <td style="width: 116px; height: 16px">
                                Purpose of Travel :</td>
                            <td colspan="2" style="height: 16px">
                                <asp:TextBox ID="txtpurposeoftravel" runat="server" Font-Size="8pt" ReadOnly="True"
                                    Width="341px"></asp:TextBox></td>
                            <td colspan="1" style="width: 72px; height: 16px">
                                Cost Center :</td>
                            <td colspan="1" style="height: 16px">
                                <asp:TextBox ID="txtcostcenter" runat="server" Font-Size="8pt" ReadOnly="True" Width="126px"></asp:TextBox></td>
                        </tr>
                        <tr style="color: #000000">
                            <td style="width: 116px">
                                Date/s of Travel :&nbsp;</td>
                            <td colspan="2" style="vertical-align: top">
                                <asp:TextBox ID="txt_travel_dates" runat="server" Font-Size="8pt" ReadOnly="True"
                                    Width="236px"></asp:TextBox>
                                &nbsp;
                            </td>
                            <td style="vertical-align: top; width: 80px" nowrap="nowrap">
                                Position/Dept. :</td>
                            <td style="vertical-align: top">
                                <asp:TextBox ID="txtposition" runat="server" Font-Size="8pt" ReadOnly="True" Width="126px"></asp:TextBox></td>
                        </tr>
                        <tr style="color: #000000">
                            <td style="width: 116px">
                                Destination :</td>
                            <td colspan="2" style="vertical-align: top">
                                <asp:TextBox ID="txtdestination" runat="server" Font-Size="8pt" ReadOnly="True" Width="341px"></asp:TextBox></td>
                            <td colspan="1" style="vertical-align: top; width: 72px">
                                OB Type :</td>
                            <td colspan="2" style="vertical-align: top; width: 72px">
                                <asp:TextBox ID="OBType_Label" runat="server" Font-Size="8pt" ReadOnly="True" 
                                     Width="126px"></asp:TextBox>
                                 </td>
                        </tr>
                        <tr style="color: #000000">
                            <td style="width: 116px; height: 16px">
                            </td>
                            <td colspan="2" style="vertical-align: top; height: 16px">
                            </td>
                            <td colspan="1" style="vertical-align: top; width: 72px; height: 16px">
                            </td>
                            <td colspan="1" style="vertical-align: top; height: 16px">
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                                height: 16px">
                                SECTION 2: REQUIRED TRANSPORTATION
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5">
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%">
                                    <RowStyle ForeColor="DimGray" Wrap="True" />
                                    <Columns>
                                        <asp:BoundField DataField="vehicle_type" HeaderText="Type of Vehicle" />
                                        <asp:BoundField DataField="date" HeaderText="Date" />
                                        <asp:BoundField DataField="destination" HeaderText="Destination (From - To)" />
                                        <asp:BoundField DataField="airline_code" HeaderText="Airline" />
                                        <asp:BoundField DataField="flight_vessel" HeaderText="Flight/Vessel #" />
                                        <asp:BoundField DataField="etd" HeaderText="ETD" />
                                        <asp:BoundField DataField="eta" HeaderText="ETA" />
                                        <asp:BoundField DataField="pickup_time" HeaderText="Pick-up Time" />
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="False" ForeColor="#FFFFCC" />
                                    <AlternatingRowStyle BackColor="#E0E0E0" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5" style="height: 16px">
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5" style="font-weight: bold; color: gray; border-bottom: gray 1px solid">
                                SECTION 3: REQUIRED LODGING</td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5" style="height: 166px">
                                <asp:GridView ID="gv_lodging" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" Width="100%">
                                    <RowStyle ForeColor="DimGray" Wrap="True" />
                                    <Columns>
                                        <asp:BoundField DataField="Accommodation_Type" HeaderText="Accommodation Type" />
                                        <asp:BoundField DataField="preferred_hotel" HeaderText="Preferred Hotel" HtmlEncode="False" />
                                        <asp:BoundField DataField="check_in" HeaderText="Check In" />
                                        <asp:BoundField DataField="check_out" HeaderText="Check Out" />
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="False" ForeColor="#FFFFCC" />
                                    <AlternatingRowStyle BackColor="#E0E0E0" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5" style="height: 16px">
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5" style="font-weight: bold; color: gray; border-bottom: gray 1px solid">
                                SECTION 4: TRAVEL ALLOWANCE
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5" style="vertical-align: top">
                                <table style="width: 100%">
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="gv_cash_advance" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowFooter="True"
                                                Width="100%">
                                                <RowStyle ForeColor="DimGray" Wrap="True" />
                                                <Columns>
                                                    <asp:BoundField DataField="purpose" HeaderText="Purpose of Cash Advance" />
                                                    <asp:BoundField DataField="amount" HeaderText="Amount" />
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
                            </td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5" style="font-weight: bold; color: gray; border-bottom: gray 1px solid">
                                SECTION 5: FOR FOREIGN TRAVEL ONLY</td>
                        </tr>
                        <tr style="color: #000000">
                            <td colspan="5">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 185px">
                                            Visa Required :</td>
                                        <td style="width: 265px">
                                            <asp:TextBox ID="txt_visa_req" runat="server" Font-Size="8pt" ReadOnly="True" Width="40px"></asp:TextBox></td>
                                        <td style="width: 84px">
                                            Passport No. :</td>
                                        <td>
                                            <asp:TextBox ID="txtpassport" runat="server" Font-Size="8pt" ReadOnly="True" Width="172px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 185px">
                                            In case of emergency, contact:</td>
                                        <td style="width: 265px">
                                            <asp:TextBox ID="txtcontactemergency" runat="server" Font-Size="8pt" ReadOnly="True"
                                                Width="241px"></asp:TextBox></td>
                                        <td style="width: 84px">
                                            Phone No. :</td>
                                        <td>
                                            <asp:TextBox ID="txtphone" runat="server" Font-Size="8pt" ReadOnly="True" Width="172px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 185px">
                                            With accompanying dependent(s)?</td>
                                        <td style="width: 265px">
                                            <asp:TextBox ID="txt_accompanying" runat="server" Font-Size="8pt" ReadOnly="True"
                                                Width="40px"></asp:TextBox></td>
                                        <td style="width: 84px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 185px">
                                            Dependent's Name :</td>
                                        <td style="width: 265px">
                                            <asp:TextBox ID="txtdependentname1" runat="server" Font-Size="8pt" ReadOnly="True"
                                                Width="172px"></asp:TextBox>
                                            Age :
                                            <asp:TextBox ID="txtdepage1" runat="server" Font-Size="8pt" ReadOnly="True" Width="30px"></asp:TextBox></td>
                                        <td style="width: 84px">
                                            Passport No. :</td>
                                        <td>
                                            <asp:TextBox ID="txtdeppass1" runat="server" Font-Size="8pt" ReadOnly="True" Width="172px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 185px">
                                            Dependent's Name :</td>
                                        <td style="width: 265px">
                                            <asp:TextBox ID="txtdependentname2" runat="server" Font-Size="8pt" ReadOnly="True"
                                                Width="172px"></asp:TextBox>
                                            Age :
                                            <asp:TextBox ID="txtdepage2" runat="server" Font-Size="8pt" ReadOnly="True" Width="30px"></asp:TextBox></td>
                                        <td style="width: 84px">
                                            Passport No. :</td>
                                        <td>
                                            <asp:TextBox ID="txtdeppass2" runat="server" Font-Size="8pt" ReadOnly="True" Width="172px"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table></center>
        </div>
    </form>
</body>
</html>
