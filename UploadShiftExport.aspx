<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UploadShiftExport.aspx.vb"
    Inherits="UploadShiftExport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function lockCol(tblID) {

            var table = document.getElementById(tblID);
            var button = document.getElementById('toggle');
            var cTR = table.getElementsByTagName('tr');  //collection of rows

            if (table.rows[0].cells[0].className == '') {
                for (i = 0; i < cTR.length; i++) {
                    var tr = cTR.item(i);
                    tr.cells[0].className = 'locked'
                    tr.cells[1].className = 'locked'
                }
                button.innerText = "Unlock First Column";
            }
            else {
                for (i = 0; i < cTR.length; i++) {
                    var tr = cTR.item(i);
                    tr.cells[0].className = ''
                    tr.cells[1].className = ''
                }
                button.innerText = "Lock First Column";
            }
        }
        
    </script>

</head>
<body>
    <form id="form1" method="post" runat="server">
    <asp:Button ID="btn_ExportToExcel" runat="server" Visible="true" Text="Export Shift Schedule to Excel" />
    <asp:HyperLink ID="HyperLink1" runat="server" Visible="False">HyperLink</asp:HyperLink>
    <asp:Label ID="Label_Error_Message" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />
    <div id="div-datagrid">
        <asp:GridView ID="GridView4" runat="server" Width="3000px" AutoGenerateColumns="False"
            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" UseAccessibleHeader="true" GridLines="None">
            <EditRowStyle Wrap="false" />
            <AlternatingRowStyle BackColor="#E0E0E0" />
            <Columns>
                <asp:BoundField DataField="employee_name" HeaderStyle-Width="10%" HeaderText="Employee Name"
                    ItemStyle-CssClass="AndLeft">
                    <HeaderStyle HorizontalAlign="Left" Wrap="False" />
                    <ItemStyle CssClass="AndLeft" />
                </asp:BoundField>
                <asp:BoundField DataField="employee_id" HeaderStyle-Width="150px" HeaderText="Personnel No.">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderStyle-Width="150" HeaderText="1">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox55" runat="server" Text='<%# Bind("1") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_1" runat="server" CommandName="cmd1" Text='<%# Bind("1") %>' />
                    </ItemTemplate>
                    <HeaderStyle Width="150px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="2">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("2") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_2" runat="server" CommandName="cmd2" Text='<%# Bind("2") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="3">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("3") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_3" runat="server" CommandName="cmd3" Text='<%# Bind("3") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="4">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("4") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_4" runat="server" CommandName="cmd4" Text='<%# Bind("4") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="5">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("5") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_5" runat="server" CommandName="cmd5" Text='<%# Bind("5") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="6">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("6") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_6" runat="server" CommandName="cmd6" Text='<%# Bind("6") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="7">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("7") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_7" runat="server" CommandName="cmd7" Text='<%# Bind("7") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="8">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("8") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_8" runat="server" CommandName="cmd8" Text='<%# Bind("8") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="9">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("9") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_9" runat="server" CommandName="cmd9" Text='<%# Bind("9") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="10">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("10") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_10" runat="server" CommandName="cmd10" Text='<%# Bind("10") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="11">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("11") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_11" runat="server" CommandName="cmd11" Text='<%# Bind("11") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="12">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("12") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_12" runat="server" CommandName="cmd12" Text='<%# Bind("12") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="13">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("13") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_13" runat="server" CommandName="cmd13" Text='<%# Bind("13") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="14">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("14") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_14" runat="server" CommandName="cmd14" Text='<%# Bind("14") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="15">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("15") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_15" runat="server" CommandName="cmd15" Text='<%# Bind("15") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="16">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("16") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_16" runat="server" CommandName="cmd16" Text='<%# Bind("16") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="17">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("17") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_17" runat="server" CommandName="cmd17" Text='<%# Bind("17") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="18">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("18") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_18" runat="server" CommandName="cmd18" Text='<%# Bind("18") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="19">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("19") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_19" runat="server" CommandName="cmd19" Text='<%# Bind("19") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="20">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("20") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_20" runat="server" CommandName="cmd20" Text='<%# Bind("20") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="21">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("21") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_21" runat="server" CommandName="cmd21" Text='<%# Bind("21") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="22">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("22") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_22" runat="server" CommandName="cmd22" Text='<%# Bind("22") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="23">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("23") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_23" runat="server" CommandName="cmd23" Text='<%# Bind("23") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="24">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox24" runat="server" Text='<%# Bind("24") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_24" runat="server" CommandName="cmd24" Text='<%# Bind("24") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="25">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox25" runat="server" Text='<%# Bind("25") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_25" runat="server" CommandName="cmd25" Text='<%# Bind("25") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="26">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("26") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_26" runat="server" CommandName="cmd26" Text='<%# Bind("26") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="27">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox27" runat="server" Text='<%# Bind("27") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_27" runat="server" CommandName="cmd27" Text='<%# Bind("27") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="28">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox28" runat="server" Text='<%# Bind("28") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_28" runat="server" CommandName="cmd28" Text='<%# Bind("28") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="29">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("29") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_29" runat="server" CommandName="cmd29" Text='<%# Bind("29") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="30">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("30") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_30" runat="server" CommandName="cmd30" Text='<%# Bind("30") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="31">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("31") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_31" runat="server" CommandName="cmd31" Text='<%# Bind("31") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Wrap="False" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <RowStyle ForeColor="DimGray" Wrap="False" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" Wrap="False" />
        </asp:GridView>
    </div>
    <br />
    <br />
    <table style="width: 100%">
        <caption>
            <tr>
                <td>
                    <div>
                        <asp:GridView ID="GridView9" runat="server" CellPadding="4" Width="100%" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                            PageSize="15" Visible="False">
                            <RowStyle ForeColor="DimGray" Wrap="False" />
                            <Columns>
                                <asp:BoundField DataField="employee_name" HeaderText="Employee Name">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="employee_id" HeaderText="Personnel No.">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="1" HeaderText="1">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="2" HeaderText="2">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="3" HeaderText="3">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="4" HeaderText="4">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="5" HeaderText="5">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="6" HeaderText="6">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="7" HeaderText="7">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="8" HeaderText="8">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="9" HeaderText="9">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="10" HeaderText="10">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="11" HeaderText="11">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="12" HeaderText="12">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="13" HeaderText="13">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="14" HeaderText="14">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="15" HeaderText="15">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="16" HeaderText="16">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="17" HeaderText="17">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="18" HeaderText="18">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="19" HeaderText="19">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="20" HeaderText="20">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="21" HeaderText="21">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="22" HeaderText="22">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="23" HeaderText="23">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="24" HeaderText="24">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="25" HeaderText="25">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="26" HeaderText="26">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="27" HeaderText="27">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="28" HeaderText="28">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="29" HeaderText="29">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="30" HeaderText="30">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="31" HeaderText="31">
                                    <HeaderStyle Wrap="False" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            <AlternatingRowStyle BackColor="#E0E0E0" />
                        </asp:GridView>
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </caption>
        <caption>
            <tr>
                <td>
                    <div id="div-datagrid3">
                        <asp:GridView ID="GridView5" runat="server" CellPadding="4" ForeColor="#333333" Width="1524px"
                            Visible="False">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="True" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" Wrap="False" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </div>
                    <%--<asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px">
                        </asp:DetailsView>--%>
                </td>
                <caption>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </caption>
            </tr>
        </caption>
    </table>
   
    </form>
</body>
</html>
