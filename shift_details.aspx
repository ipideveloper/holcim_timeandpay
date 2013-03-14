<%@ Page Language="VB" AutoEventWireup="false" CodeFile="shift_details.aspx.vb" Inherits="shift_details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Holcim - Shift Schedule Details</title>
     <style type="text/css">
BODY { MARGIN: 0px }
.modalBackground { 
background-color:Black; 
filter:alpha(opacity=70); 
opacity:0.7px; 
} 
.popupControl{
background-color:Red;
position:absolute;
visibility:hidden;
}
		</style>
<script  type language="javascript">
function closewindow()
{
	window.close()
}
</script>

</head>
<body style="font-size: 8pt; font-family: arial">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="height: 41px; width: 345px;">
                    <asp:Label ID="lbl_header" runat="server"></asp:Label>
                    <asp:Label ID="lbl_month" runat="server" Visible="False"></asp:Label>&nbsp;- &nbsp;<asp:Label
                        ID="lbl_year" runat="server" Width="64px"></asp:Label></td>
                <td style="text-align: right; height: 41px;">
    
                    <asp:LinkButton ID="LinkButton1" runat="server">Upload from Excel File</asp:LinkButton>
                        <input onclick="closewindow();" size="15" 
                        style="font-size: 10pt; color: white; font-family: arial; background-color: red" 
                        type="button" value="Close Window"></input></td>
            </tr>
        </table>
                <asp:Timer ID="Timer1" runat="server" Interval="50">
                </asp:Timer>
                <asp:Label ID="lbl_status" runat="server" Font-Bold="True" Font-Size="9pt" ForeColor="Red"></asp:Label><br />
                    
                <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%" AllowPaging="True" PageSize="15" >
            <RowStyle ForeColor="DimGray" Wrap="True" />
            <Columns>
                <asp:BoundField DataField="employee_name" HeaderText="Employee Name">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="employee_id" HeaderText="Personnel No.">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="1">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("1") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_1" runat="server" CommandName="cmd1" Text='<%# Bind("1") %>'></asp:LinkButton>
                    </ItemTemplate>
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
                        &nbsp;<asp:LinkButton ID="lnk_11" runat="server" CommandName="cmd11" Text='<%# Bind("11") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="12">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("12") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_12" runat="server" CommandName="cmd12" Text='<%# Bind("12") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="13">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("13") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_13" runat="server" CommandName="cmd13" Text='<%# Bind("13") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="14">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("14") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_14" runat="server" CommandName="cmd14" Text='<%# Bind("14") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="15">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("15") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_15" runat="server" CommandName="cmd15" Text='<%# Bind("15") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="16">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("16") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_16" runat="server" CommandName="cmd16" Text='<%# Bind("16") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="17">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("17") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_17" runat="server" CommandName="cmd17" Text='<%# Bind("17") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="18">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("18") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_18" runat="server" CommandName="cmd18" Text='<%# Bind("18") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="19">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("19") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_19" runat="server" CommandName="cmd19" Text='<%# Bind("19") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="20">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("20") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_20" runat="server" CommandName="cmd20" Text='<%# Bind("20") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="21">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("21") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_21" runat="server" CommandName="cmd21" Text='<%# Bind("21") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="22">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("22") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_22" runat="server" CommandName="cmd22" Text='<%# Bind("22") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="23">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("23") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_23" runat="server" CommandName="cmd23" Text='<%# Bind("23") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="24">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox24" runat="server" Text='<%# Bind("24") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_24" runat="server" CommandName="cmd24" Text='<%# Bind("24") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="25">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox25" runat="server" Text='<%# Bind("25") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_25" runat="server" CommandName="cmd25" Text='<%# Bind("25") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="26">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("26") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_26" runat="server" CommandName="cmd26" Text='<%# Bind("26") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="27">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox27" runat="server" Text='<%# Bind("27") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_27" runat="server" CommandName="cmd27" Text='<%# Bind("27") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="28">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox28" runat="server" Text='<%# Bind("28") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_28" runat="server" CommandName="cmd28" Text='<%# Bind("28") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="29">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("29") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_29" runat="server" CommandName="cmd29" Text='<%# Bind("29") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="30">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("30") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_30" runat="server" CommandName="cmd30" Text='<%# Bind("30") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="31">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("31") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        &nbsp;<asp:LinkButton ID="lnk_31" runat="server" CommandName="cmd31" Text='<%# Bind("31") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <AlternatingRowStyle BackColor="#E0E0E0" />
        </asp:GridView>
                <br />
                <table style="width: 832px">
                        <tr>
                            <td style="vertical-align: top; width: 962px;">
                                <table style="width: 832px">
                                    <tr>
                            <td style="vertical-align: top; height: 97px; width: 828px;">
                                &nbsp;&nbsp;
                                <table style="width: 676px; text-align: left">
                                    <tr style="color: #000000">
                                        <td colspan="6" style="height: 13px">
                                            <br />
                                            <strong>Employee Name:
                    </strong>
                                            <asp:TextBox ID="txt_employee_name" runat="server" Height="17px" Width="282px"></asp:TextBox>
                                        </td>
                                    </tr>
            <tr style="color: #000000">
                <td style="width: 143px; height: 16px">
                    <strong>
                        Personnel No. :</strong></td>
                <td colspan="2" style="height: 16px">
                    <strong>
                    Day :</strong></td>
                <td colspan="1" style="height: 16px; width: 93px;">
                    <strong>Current Shift :</strong></td>
                <td colspan="1" style="height: 16px; width: 112px;">
                    <strong>Change to shift : </strong>
                </td>
                <td colspan="1" style="height: 16px; width: 414px;">
                </td>
            </tr>
            <tr style="color: #000000">
                <td style="width: 143px; height: 16px">
                    <asp:TextBox ID="txt_employee_id" runat="server" Font-Size="8pt" MaxLength="50" TabIndex="2" Width="104px" ReadOnly="True" ></asp:TextBox></td>
                <td colspan="2" style="font-size: 15pt; color: red; height: 16px">
                    <asp:TextBox ID="txt_day" runat="server" Font-Size="8pt" MaxLength="50"
                        TabIndex="2" Width="31px" ReadOnly="True"></asp:TextBox></td>
                <td colspan="1" style="font-size: 15pt; color: red; height: 16px; width: 93px;">
                    <asp:TextBox ID="txt_current_shift" runat="server" Font-Size="8pt" MaxLength="50" TabIndex="2" Width="61px" ReadOnly="True"></asp:TextBox></td>
                <td colspan="1" style="font-size: 15pt; color: red; height: 16px; width: 112px;">
                    <asp:DropDownList ID="dpl_new_shift" runat="server" BackColor="#FFFFC0" Font-Size="8pt"
                                                Width="98px">
                    </asp:DropDownList></td>
                <td colspan="1" style="font-size: 15pt; color: red; height: 16px; width: 414px;">
                    <asp:Button ID="btnSave" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Add"
                                                Width="87px" OnClick="btnSave_Click" Enabled="False"  />
                    <asp:Button ID="btn_save" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Save"
                                                Width="87px" Enabled="False" OnClick="btn_save_Click"   />
                    <asp:Button ID="btn_submit" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                                                Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Submit"
                                                Width="87px" Enabled="False" OnClick="btn_submit_Click"   /></td>
            </tr>
            <tr style="color: #000000">
                <td colspan="6" style="color: white; border-bottom: gray 1px solid; height: 16px">
                    .<cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="True"
                        ConfirmText="Save This Schedules?" TargetControlID="btn_save">
                    </cc1:ConfirmButtonExtender><cc1:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmOnFormSubmit="True"
                        ConfirmText="Submit This Schedules?" TargetControlID="btn_submit">
                    </cc1:ConfirmButtonExtender>
                    &nbsp;
                </td>
            </tr>
                                    <tr style="color: #000000">
                                        <td colspan="6" style="border-bottom-width: 1px; border-bottom-color: gray; height: 24px">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                DisplayAfter="10">
                <ProgressTemplate>
                    <img src="images/updateprogress1.gif" style="width: 24px; height: 23px" />
                    Processing ...............
                </ProgressTemplate>
            </asp:UpdateProgress>
                                        </td>
                                    </tr>
        </table>
                            </td>
                                    </tr>
                </table>
                                <br />
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="669px">
                    <RowStyle ForeColor="DimGray" Wrap="True" />
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete_index" ForeColor="Red">Delete</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="employee_name" HeaderText="Employee Name" />
                        <asp:BoundField DataField="employee_id" HeaderText="Personnel No." />
                        <asp:BoundField DataField="day" HeaderText="Day" />
                        <asp:BoundField DataField="shift_from" HeaderText="From" />
                        <asp:BoundField DataField="shift_to" HeaderText="To" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <AlternatingRowStyle BackColor="#E0E0E0" />
                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                <center>
                </center>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <center>
            &nbsp;</center>
        <center>
            &nbsp;&nbsp;</center>
        <center>
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
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;&nbsp;</center>
    
    </div>
    </form>
</body>
</html>
