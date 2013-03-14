<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="leave_app_form.aspx.vb" Inherits="leaveapplications" Title="Holcim - Apply Leave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 103%">
                <tbody>
                    <tr>
                        <td style="font-weight: bold; color: red; border-bottom: gray 1px solid; text-align: right"
                            colspan="2">
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tbody>
                                    <tr>
                                        <td style="color: gray; text-align: left">
                                            File Leaves
                                        </td>
                                        <td style="width: 488px; color: red; text-align: right">
                                            Please fill up all required fields in yellow.
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 315px; height: 82px">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValG"
                                HeaderText="Incomplete Data. Cannot process your leave application." ShowMessageBox="True">
                            </asp:ValidationSummary>
                        </td>
                        <td style="vertical-align: top; width: 358px; height: 82px; text-align: right">
                            <asp:Label ID="lbl_days_hidden" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lbl_total_days" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; width: 315px">
                            <table style="font-weight: normal; font-size: 8pt; width: 312px" id="TABLE1">
                                <tbody>
                                    <tr style="font-size: 8pt">
                                        <td style="width: 91px; height: 22px">
                                            Leave Type:
                                        </td>
                                        <td style="width: 158px; height: 22px">
                                            <asp:DropDownList ID="dplType" runat="server" Font-Size="8pt" Width="168px" BackColor="#FFFFC0"
                                                ValidationGroup="Val" AppendDataBoundItems="true" CssClass="Buttonhand1hover" AutoPostBack="True" OnSelectedIndexChanged="dplType_SelectedIndexChanged">
                                            <asp:ListItem Value="">Please select leave type...</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Font-Bold="True"
                                                Font-Size="Large" Width="2px" ValidationGroup="ValG" ControlToValidate="dplType"
                                                ErrorMessage="LEAVE TYPE cannot be blank. Please select one">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Font-Bold="True"
                                                Font-Size="1pt" ValidationGroup="Val" ControlToValidate="dplType" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 8pt; color: #000000">
                                        <td style="width: 91px; height: 16px">
                                            Available Balance :
                                        </td>
                                        <td style="width: 158px; height: 16px">
                                            <asp:TextBox ID="txt_credit" runat="server" Font-Size="8pt" Width="39px" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 8pt; color: #000000">
                                        <td style="width: 91px; height: 16px">
                                            Date From :
                                        </td>
                                        <td style="width: 158px; height: 16px">
                                            <asp:TextBox ID="txtStart" runat="server" Font-Size="8pt" Width="75px" BackColor="#FFFFC0"
                                                CssClass="Buttonhand1hover"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rFrom" runat="server" Font-Bold="False" Font-Size="1pt"
                                                ValidationGroup="Val" ControlToValidate="txtStart"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator4" runat="server" Font-Bold="True" Font-Size="Large"
                                                    Width="2px" ValidationGroup="ValG" ControlToValidate="txtStart" ErrorMessage="DATE FROM cannot be blank. Please enter start date">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 8pt; color: #000000">
                                        <td style="width: 91px; height: 16px">
                                            Date To :
                                        </td>
                                        <td style="width: 158px; height: 16px">
                                            <asp:TextBox ID="txtTo" runat="server" Font-Size="8pt" Width="75px" BackColor="#FFFFC0"
                                                CssClass="Buttonhand1hover"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rTo" runat="server" Font-Bold="False" Font-Size="1pt"
                                                ValidationGroup="Val" ControlToValidate="txtTo"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Font-Bold="True"
                                                Font-Size="Large" Width="2px" ValidationGroup="ValG" ControlToValidate="txtTo"
                                                ErrorMessage="DATE TO cannot be blank. Please enter end date">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Font-Bold="False"
                                                Font-Size="1pt" ValidationGroup="ValSub" ControlToValidate="txtReason"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 8pt; color: #000000">
                                        <td style="width: 91px; height: 24px">
                                            Reason :
                                        </td>
                                        <td style="width: 158px; height: 24px">
                                            <asp:TextBox ID="txtReason" runat="server" Width="189px" BackColor="#FFFFC0" ValidationGroup="Val"
                                                Height="66px" TextMode="MultiLine"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator6" runat="server" Font-Bold="True" Font-Size="Large"
                                                    Width="2px" ValidationGroup="ValG" ControlToValidate="txtReason" ErrorMessage="REASON cannot be blank. Please enter your reasons">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr style="font-size: 8pt; color: #000000">
                                        <td style="width: 91px; height: 44px">
                                        </td>
                                        <td style="vertical-align: top; width: 158px; height: 44px">
                                            <asp:Button ID="btn_bind" OnClick="btn_bind_Click" runat="server" ForeColor="White"
                                                Font-Size="8pt" Width="87px" Text="Refresh Details" BorderWidth="1px" BorderColor="DarkGray"
                                                BackColor="Red" ValidationGroup="ValG" CssClass="Buttonhand1hover" Font-Names="arial">
                                            </asp:Button>&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" ForeColor="White"
                                                Font-Size="8pt" Width="87px" Text="Submit" BorderWidth="1px" BorderColor="DarkGray"
                                                BackColor="Red" Visible="False" ValidationGroup="ValSub" CssClass="Buttonhand1hover"
                                                Font-Names="arial" Style="height: 20px"></asp:Button><br />
                                        </td>
                                    </tr>
                                    <tr style="font-size: 8pt; color: #000000">
                                        <td style="width: 91px; height: 25px">
                                        </td>
                                        <td style="width: 158px; height: 25px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                                Format="dd-MMM-yyyy" PopupButtonID="txtTo">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStart"
                                Format="dd-MMM-yyyy" PopupButtonID="txtStart">
                            </cc1:CalendarExtender>
                            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnSubmit"
                                ConfirmOnFormSubmit="True" ConfirmText="Are you sure you want to submit this application?">
                            </cc1:ConfirmButtonExtender>
                        </td>
                        <td style="font-size: 8pt; vertical-align: top; width: 358px">
                            <asp:GridView ID="gv_dates" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CC9966"
                                BackColor="White" AutoGenerateColumns="False" BorderStyle="None" CellPadding="4">
                                <RowStyle Wrap="True" ForeColor="DimGray"></RowStyle>
                                <Columns>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="TextBox1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnk_delete" runat="server" ForeColor="Red" CommandName="cmd_delete"
                                                Font-Underline="False" CssClass="Buttonhand1hover">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="date" HeaderText="Date"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Halfday">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_half" runat="server" AutoPostBack="True" OnCheckedChanged="chk_half_CheckedChanged"
                                                CssClass="Buttonhand1hover"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="TextBox3"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="r_first" runat="server" Text="1st half" GroupName="halfday"
                                                Enabled="False" CssClass="Buttonhand1hover"></asp:RadioButton>
                                            <asp:RadioButton ID="r_second" runat="server" Text="2nd half" GroupName="halfday"
                                                Enabled="False" CssClass="Buttonhand1hover"></asp:RadioButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="date_status" HeaderText="Date Status"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Holiday">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="check_holiday" runat="server" AutoPostBack="True" Enabled="False">
                                            </asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Without Pay">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="check_withoutpay" runat="server" AutoPostBack="True" Enabled="False">
                                            </asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099"></FooterStyle>
                                <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="#330099"></PagerStyle>
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle>
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC"></HeaderStyle>
                                <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="btn_bind"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>