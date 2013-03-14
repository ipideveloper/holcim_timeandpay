<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="ob_apply.aspx.vb" Inherits="ob_apply" Title="Holcim - OB Application" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanelOB" runat ="server">
    <ContentTemplate>--%>
    <style type="text/css">
    .tablegrid th { font-size:11px;
    }
    </style>

    <table style="width: 740px; text-align: left">
        <tr style="color: #000000">
            <td colspan="4" style="color: gray; height: 16px; font-weight: bold; border-bottom: gray 1px solid;">
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            SECTION 1: TRAVELER INFORMATION
                        </td>
                        <td style="color: red; text-align: right">
                            Please fill up all required fields in yellow.
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Incomplete Traveler's Information.  Cannot process."
                    ShowMessageBox="True" ValidationGroup="ValG" />
            </td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 116px; height: 16px">
                Employee Name :
            </td>
            <td style="height: 16px; width: 388px;">
                <asp:DropDownList ID="dplEmployee" runat="server" Font-Size="8pt" Width="211px" BackColor="Yellow"
                    AutoPostBack="True" CssClass="Buttonhand1hover">
                </asp:DropDownList>
                <strong style="font-size: 14pt; color: red"></strong>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dplEmployee"
                    Font-Bold="False" Font-Size="1pt" ValidationGroup="Val">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="1" style="width: 72px; height: 16px">
                Contact No. :&nbsp;
            </td>
            <td colspan="1" style="height: 16px">
                <asp:TextBox ID="txtcontactno" runat="server" Font-Size="8pt" Width="126px" BackColor="Yellow"></asp:TextBox>
                <cc1:MaskedEditExtender ID="txtcontactno_MaskedEditExtender" runat="server" Filtered="0-9"
                    Mask="9999-999-9999" TargetControlID="txtcontactno">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtcontactno"
                    ErrorMessage="CONTACT NO cannot be blank. Please enter contact number" Font-Size="Large"
                    SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 116px; height: 16px">
                Type of Travel :
            </td>
            <td style="height: 16px; width: 388px;">
                <strong></strong>
                <asp:DropDownList ID="dpltravel_type" runat="server" Width="86px" Font-Size="8pt"
                    BackColor="Yellow" CssClass="Buttonhand1hover" AutoPostBack="True">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Local</asp:ListItem>
                    <asp:ListItem Value="Foreign">Foreign</asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                    ControlToValidate="dpltravel_type" ErrorMessage="TYPE OF TRAVEL cannot be blank. Please select one"
                    Font-Size="Large" SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="1" style="width: 72px; height: 16px">
                Cost Center :
            </td>
            <td colspan="1" style="height: 16px">
                <asp:TextBox ID="txtcostcenter" runat="server" Font-Size="8pt" Width="126px" BackColor="Yellow"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtcostcenter"
                    ErrorMessage="COST CENTER cannot be blank. Please enter cost center code" Font-Size="Large"
                    SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtcostcenter"
                    Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 116px; height: 16px">
                Purpose of Travel :
            </td>
            <td style="height: 16px; width: 388px;">
                <asp:TextBox ID="txtpurposeoftravel" runat="server" Font-Size="8pt" Width="324px"
                    TextMode="MultiLine" BackColor="Yellow"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator28" runat="server" ControlToValidate="txtpurposeoftravel"
                        ErrorMessage="PURPOSE OF TRAVEL cannot be blank.  Please enter purpose of travel."
                        Font-Size="Large" SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="1" style="width: 72px; height: 16px" nowrap="nowrap">
                Position/Dept. :
            </td>
            <td colspan="1" style="height: 16px">
                <asp:TextBox ID="txtposition" runat="server" Font-Size="8pt" Width="204px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 116px">
                Date/s of Travel :&nbsp;
            </td>
            <td style="vertical-align: top; width: 388px;">
                from
                <asp:TextBox ID="txtdatefrom" runat="server" Font-Size="8pt" Width="91px" BackColor="Yellow"
                    CssClass="Buttonhand1hover"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtdatefrom"
                    ErrorMessage="DATE FROM cannot be blank. Please enter start date." Font-Size="Large"
                    SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdatefrom"
                    Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator>to
                <asp:TextBox ID="txtdateto" runat="server" Font-Size="8pt" Width="91px" BackColor="Yellow"
                    CssClass="Buttonhand1hover"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtdateto"
                    ErrorMessage="DATE TO cannot be blank. Please enter end date" Font-Size="Large"
                    SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdateto"
                    Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator>&nbsp;
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdatefrom"
                    Format="dd-MMM-yyyy" PopupButtonID="txtdatefrom">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtdateto"
                    Format="dd-MMM-yyyy" PopupButtonID="txtdateto">
                </cc1:CalendarExtender>
            </td>
            <td colspan="1" style="vertical-align: top; width: 72px">
                OB Type:
            </td>
            <td colspan="1" style="vertical-align: top">
                <asp:RadioButton ID="RadioButton_RegularOB" GroupName="OB_Type" Text="Regular OB"
                    runat="server" />
                <asp:RadioButton ID="RadioButton_CrossPostingOB" GroupName="OB_Type" Text="Cross Posting OB"
                    Checked="true" runat="server" />
            </td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 116px"></td>
            <td style="vertical-align: top; width: 388px;"></td>
            <td colspan="1" style="vertical-align: top; width: 72px"></td>
            <td colspan="1" style="vertical-align: top"></td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 116px">Destination :</td>
            <td style="vertical-align: top; width: 388px;">
                <asp:TextBox ID="txtdestination" runat="server" Font-Size="8pt" Width="332px" BackColor="Yellow"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="txtdestination"
                    ErrorMessage="DESTINATION OF TRAVEL cannot be blank.  Please enter destination of travel."
                    Font-Size="Large" SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
            </td>
            <td colspan="1" style="vertical-align: top; width: 72px"></td>
            <td colspan="1" style="vertical-align: top"></td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 116px; height: 16px"></td>
            <td style="vertical-align: top; height: 16px; width: 388px;"></td>
            <td colspan="1" style="vertical-align: top; width: 72px; height: 16px"></td>
            <td colspan="1" style="vertical-align: top; height: 16px"></td>
        </tr>
        <tr style="color: #000000">
            <td style="height: 16px; color: gray; font-weight: bold; border-bottom: gray 1px solid;" colspan="4">SECTION 2: REQUIRED TRANSPORTATION
                <asp:LinkButton ID="lnkAddTranspo" runat="server" ForeColor="Red">[ Add ]</asp:LinkButton>
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="4">
                <!--Begin Transportation Gridview-->
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
                    CssClass="tablegrid" CellPadding="3" BorderWidth="0" CellSpacing="1" Width="100%">
                    <RowStyle ForeColor="DimGray" Wrap="True" />
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete_index" ForeColor="Red">Delete</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="vehicle_type" HeaderText="Type of Vehicle">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="date" HeaderText="Date">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="destination" HeaderText="Destination (From - To)">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="airline_code" HeaderText="Airline">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="flight_vessel" HeaderText="Flight/Vessel #">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="etd" HeaderText="ETD">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="eta" HeaderText="ETA">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pickup_time" HeaderText="Pick-up Time">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <HeaderStyle BackColor="#990000" Font-Bold="False" ForeColor="#FFFFCC"  />
                    <AlternatingRowStyle BackColor="#E0E0E0" />
                </asp:GridView>
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="4" style="height: 16px">
            </td>
        </tr>
        <tr style="color: #000000">
            <td style="font-weight: bold; color: gray; border-bottom: gray 1px solid;" colspan="4">
                SECTION 3: REQUIRED LODGING
                <asp:LinkButton ID="lnkAddLodging" runat="server" ForeColor="Red">[ Add ]</asp:LinkButton>
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="4">
                <!--Begin Lodging Gridview-->
                <asp:GridView ID="gv_lodging" runat="server" AutoGenerateColumns="False" BackColor="White"
                    CssClass="tablegrid" CellPadding="3" BorderWidth="0" CellSpacing="1" Width="100%"
                    AllowSorting="True">
                    <RowStyle ForeColor="DimGray" Wrap="True" />
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete_index" ForeColor="Red">Delete</asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                        </asp:TemplateField>
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
            <td colspan="4" style="height: 16px">
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="4" style="font-weight: bold; color: gray; border-bottom: gray 1px solid; height: 29px;">SECTION 4: TRAVEL ALLOWANCE
                <asp:LinkButton ID="lnk_add_travel_allowance" runat="server" Font-Bold="True" CausesValidation="false" ForeColor="Red">[ Add ]</asp:LinkButton>
                <asp:LinkButton ID="FakeTarget" runat="server" Font-Bold="True" CausesValidation="false" Style="display: none" ForeColor="Red">[ Add ]</asp:LinkButton>
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="4" style="vertical-align: top">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2" style="color:#808080; padding-left:10px;padding-right:20px;">
                            Note: Allowed only if the employee is not issued a corporate credit card, or when travelling
                            to places where cash is needed or credit card facility may not be available.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!-- Begin Allowance Gridview-->
                            <asp:GridView ID="gv_cash_advance" runat="server" AutoGenerateColumns="False" BackColor="White"
                            CssClass="tablegrid" CellPadding="3" BorderWidth="0" CellSpacing="1" Width="100%"
                                ShowFooter="True">
                                <RowStyle ForeColor="DimGray" Wrap="True" />
                                <Columns>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete_index" ForeColor="Red">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="purpose" HeaderText="Purpose of Cash Advance" />
                                    <asp:BoundField DataField="amount" HeaderText="Amount" DataFormatString="{0:N2}" HtmlEncode="True">
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
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
            <td colspan="4" style="vertical-align: top; height: 16px">
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="4" style="font-weight: bold; color: gray; border-bottom: gray 1px solid">
                SECTION 5: FOR FOREIGN TRAVEL ONLY
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="4">
                <table style="width: 100%">
                    <tr>
                        <td style="height: 26px;" colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 185px; height: 26px;">
                            Visa Required :
                        </td>
                        <td style="width: 265px; height: 26px;">
                            <asp:RadioButton ID="r_visa_yes" runat="server" GroupName="visa" Text="Yes" CssClass="Buttonhand1hover" />
                            <asp:RadioButton ID="r_visa_no" runat="server" Checked="True" GroupName="visa" Text="No"
                                CssClass="Buttonhand1hover" />
                        </td>
                        <td style="width: 84px; height: 26px;">
                            Passport No. :
                        </td>
                        <td style="height: 26px">
                            <asp:TextBox ID="txtpassport" runat="server" Font-Size="8pt" Width="172px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="txtpassport"
                                Enabled="False" ErrorMessage="Passport No  cannot be blank.  Please enter passport no."
                                Font-Size="Large" SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 185px">
                            In case of emergency, contact:
                        </td>
                        <td style="width: 265px">
                            <asp:TextBox ID="txtcontactemergency" runat="server" Font-Size="8pt" Width="241px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="txtcontactemergency"
                                Enabled="False" ErrorMessage="Contact person cannot be blank.  Please enter contact person."
                                Font-Bold="True" Font-Size="Large" SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 84px">
                            Phone No. :
                        </td>
                        <td>
                            <asp:TextBox ID="txtphone" runat="server" Font-Size="8pt" Width="172px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtphone"
                                Enabled="False" ErrorMessage="Phone No required" Font-Size="Large" SetFocusOnError="True"
                                ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 185px; height: 16px;">
                            With accompanying dependent(s)?
                        </td>
                        <td style="width: 265px; height: 16px;">
                            <asp:RadioButton ID="r_dependent_no" runat="server" Checked="True" GroupName="dependent"
                                Text="No" CssClass="Buttonhand1hover" />
                            <asp:RadioButton ID="r_dependent_yes" runat="server" GroupName="dependent" Text="If yes, please indicate details below"
                                CssClass="Buttonhand1hover" />
                        </td>
                        <td style="width: 84px; height: 16px;">
                        </td>
                        <td style="height: 16px">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 185px">
                            Dependent's Name :
                        </td>
                        <td style="width: 265px">
                            <asp:TextBox ID="txtdependentname1" runat="server" Font-Size="8pt" Width="172px"></asp:TextBox>
                            Age :
                            <asp:TextBox ID="txtdepage1" runat="server" Font-Size="8pt" Width="30px"></asp:TextBox>
                        </td>
                        <td style="width: 84px">
                            Passport No. :
                        </td>
                        <td>
                            <asp:TextBox ID="txtdeppass1" runat="server" Font-Size="8pt" Width="172px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 185px">
                            Dependent's Name :
                        </td>
                        <td style="width: 265px">
                            <asp:TextBox ID="txtdependentname2" runat="server" Font-Size="8pt" Width="172px"></asp:TextBox>
                            Age :
                            <asp:TextBox ID="txtdepage2" runat="server" Font-Size="8pt" Width="30px"></asp:TextBox>
                        </td>
                        <td style="width: 84px">
                            Passport No. :
                        </td>
                        <td>
                            <asp:TextBox ID="txtdeppass2" runat="server" Font-Size="8pt" Width="172px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="4" style="color: white; border-bottom: gray 1px solid; height: 17px">
                .
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="4" style="border-bottom-width: 1px; border-bottom-color: gray; color: white;
                height: 50px">
                <asp:Button ID="btnSave" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                    Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Submit" ValidationGroup="ValG"
                    Width="87px" OnClick="btnSave_Click" CssClass="Buttonhand1hover" />
                <asp:Button ID="btnCancel" runat="server" BackColor="Red" BorderColor="DarkGray"
                    BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Cancel"
                    Width="87px" OnClick="btnCancel_Click" CssClass="Buttonhand1hover" />
                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="True"
                    ConfirmText="Are you sure you want submit this form?" TargetControlID="btnSave">
                </cc1:ConfirmButtonExtender>
                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" ConfirmText="Cancel this form?"
                    TargetControlID="btnCancel">
                </cc1:ConfirmButtonExtender>
            </td>
        </tr>
    </table>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnCancelTranspo" PopupControlID="Panel2" TargetControlID="lnkAddTranspo">
    </cc1:ModalPopupExtender>
    <%--    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btn_cancel_allowance" PopupControlID="Panel1" TargetControlID="lnk_add_travel_allowance">
    </cc1:ModalPopupExtender>--%>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" DynamicServicePath=""
        Enabled="true" TargetControlID="FakeTarget" PopupControlID="Panel1" BackgroundCssClass="modalBackground"
        CancelControlID="btn_cancel_allowance" DropShadow="true">
    </cc1:ModalPopupExtender>
    <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground"
        CancelControlID="btnCancelLodging" PopupControlID="Panel3" TargetControlID="lnkAddLodging">
    </cc1:ModalPopupExtender>
    
    <!--Begin Lodging-->
    <asp:Panel ID="Panel3" runat="server" Height="276px" Width="507px" BackColor="White"
        BorderColor="Black">
        <table style="width: 450px; text-align: left">
            <center>
            </center>
            <center>
                <caption>
                    &nbsp;</caption>
            </center>
            <center>
                &nbsp;</center>
            <center>
                <table style="width: 450px; text-align: left">
                    <tr style="color: #000000">
                        <td colspan="2" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                            height: 16px">
                            Add Travel Lodging
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="width: 139px; color: white">
                        </td>
                        <td style="color: red; text-align: right">
                            Please fill up all required fields in yellow.
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="width: 139px; color: white">
                            .
                        </td>
                        <td style="vertical-align: top; width: 304px;">
                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" HeaderText="Incomplete Travel Allowance Information.  Cannot Process"
                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="lodging" />
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="width: 139px">
                            Type of Accomodation :
                        </td>
                        <td style="vertical-align: top; width: 304px;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="dplaccomodationtype" EnableViewState="false" AutoPostBack="true"
                                        runat="server" Width="150px" Font-Size="8pt" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                                        <asp:ListItem Value="">Please select...</asp:ListItem>
                                        <asp:ListItem Value="Guesthouse">Guesthouse</asp:ListItem>
                                        <asp:ListItem Value="Hotel">Hotel</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="dplaccomodationtype"
                                        ErrorMessage="ACCOMMODATION TYPE cannot be blank.  Please select one" Font-Size="Large"
                                        SetFocusOnError="True" ValidationGroup="lodging">*</asp:RequiredFieldValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:SqlDataSource ID="City_SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                SelectCommand="SELECT DISTINCT h.city_code AS CityCode, c.cdescription AS City FROM dbo.ob_hotel h INNER JOIN dbo.sys_city c ON h.city_code=c.city_code"
                                SelectCommandType="Text"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="Hotel_SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                SelectCommand="SELECT [city_code],[hotel_name]FROM [holcim_tmpsdb_test].[dbo].[ob_hotel] WHERE city_code = @city_code"
                                SelectCommandType="Text">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="CityCode_DropDownList" Name="city_code" Type="String"
                                        PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="width: 139px">
                            If hotel, specify city :
                        </td>
                        <td style="vertical-align: top; width: 304px;">
                            <asp:UpdatePanel ID="UpdatePanel_Accomodation" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="CityCode_DropDownList" Enabled="false" EnableViewState="false"
                                        AppendDataBoundItems="true" runat="server" AutoPostBack="true" Width="150px"
                                        Font-Size="8pt" DataSourceID="City_SqlDataSource" DataTextField="City" DataValueField="CityCode">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dplaccomodationtype" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="width: 139px">
                            If hotel, specify preferred :
                        </td>
                        <td style="vertical-align: top; width: 304px;">
                            <%--<asp:TextBox ID="txtpreferredhotel_old" runat="server" Font-Size="8pt" Width="259px"></asp:TextBox>--%>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:DropDownList ID="txtpreferredhotel" Visible="false" runat="server" EnableViewState="false"
                                        DataSourceID="Hotel_SqlDataSource" DataTextField="hotel_name" Width="150px" Font-Size="8pt"
                                        DataValueField="hotel_name">
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="CityCode_DropDownList" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="dplaccomodationtype" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td colspan="1" style="color: black; border-bottom: gray 1px solid; height: 16px">
                            Check In Date &amp; Time :
                        </td>
                        <td style="color: black; border-bottom: gray 1px solid; height: 16px">
                            <asp:TextBox ID="txtcheckin" runat="server" Font-Size="8pt" Width="126px" BackColor="#FFFFC0"
                                CssClass="Buttonhand1hover"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtcheckin"
                                ErrorMessage="CHECK IN cannot be blank.  Please enter check IN" Font-Size="Large"
                                SetFocusOnError="True" ValidationGroup="lodging">*</asp:RequiredFieldValidator>&nbsp;
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                PopupButtonID="txtcheckin" TargetControlID="txtcheckin">
                            </cc1:CalendarExtender>
                            <asp:DropDownList ID="dplCheckIN" runat="server" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                                <asp:ListItem Value="01"></asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem Value="03"></asp:ListItem>
                                <asp:ListItem Value="04"></asp:ListItem>
                                <asp:ListItem Value="05"></asp:ListItem>
                                <asp:ListItem Value="06"></asp:ListItem>
                                <asp:ListItem Value="07"></asp:ListItem>
                                <asp:ListItem Value="08"></asp:ListItem>
                                <asp:ListItem Value="09"></asp:ListItem>
                                <asp:ListItem Value="10"></asp:ListItem>
                                <asp:ListItem Value="11"></asp:ListItem>
                                <asp:ListItem Value="12"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:ListBox ID="lstCheckIN" runat="server" BackColor="#FFFFC0" Rows="1" CssClass="Buttonhand1hover">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem Value="30"></asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>41</asp:ListItem>
                                <asp:ListItem>42</asp:ListItem>
                                <asp:ListItem>43</asp:ListItem>
                                <asp:ListItem>44</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>46</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>48</asp:ListItem>
                                <asp:ListItem>49</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>51</asp:ListItem>
                                <asp:ListItem>52</asp:ListItem>
                                <asp:ListItem>53</asp:ListItem>
                                <asp:ListItem>54</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                                <asp:ListItem>56</asp:ListItem>
                                <asp:ListItem>57</asp:ListItem>
                                <asp:ListItem>58</asp:ListItem>
                                <asp:ListItem>59</asp:ListItem>
                            </asp:ListBox>
                            <asp:ListBox ID="ampmCheckIN" runat="server" BackColor="#FFFFC0" Rows="1" CssClass="Buttonhand1hover">
                                <asp:ListItem Value="PM">PM</asp:ListItem>
                                <asp:ListItem Value="AM">AM</asp:ListItem>
                            </asp:ListBox>
                            <%--(hh:mm)--%>
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td colspan="1" style="color: black; border-bottom: gray 1px solid; height: 16px">
                            Check Out Date &amp; Time
                        </td>
                        <td style="color: black; border-bottom: gray 1px solid; height: 16px">
                            <asp:TextBox ID="txtcheckout" runat="server" Font-Size="8pt" Width="126px" BackColor="#FFFFC0"
                                CssClass="Buttonhand1hover"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="txtcheckout"
                                ErrorMessage="CHECK OUT cannot be blank.  Please enter check out" Font-Size="Large"
                                SetFocusOnError="True" ValidationGroup="lodging">*</asp:RequiredFieldValidator>
                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy"
                                PopupButtonID="txtcheckout" TargetControlID="txtcheckout">
                            </cc1:CalendarExtender>
                            <asp:DropDownList ID="dplCheckOUT" runat="server" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                                <asp:ListItem Value="01"></asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem Value="03"></asp:ListItem>
                                <asp:ListItem Value="04"></asp:ListItem>
                                <asp:ListItem Value="05"></asp:ListItem>
                                <asp:ListItem Value="06"></asp:ListItem>
                                <asp:ListItem Value="07"></asp:ListItem>
                                <asp:ListItem Value="08"></asp:ListItem>
                                <asp:ListItem Value="09"></asp:ListItem>
                                <asp:ListItem Value="10"></asp:ListItem>
                                <asp:ListItem Value="11"></asp:ListItem>
                                <asp:ListItem Value="12"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:ListBox ID="lstCheckOUT" runat="server" BackColor="#FFFFC0" Rows="1" CssClass="Buttonhand1hover">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>26</asp:ListItem>
                                <asp:ListItem>27</asp:ListItem>
                                <asp:ListItem>28</asp:ListItem>
                                <asp:ListItem>29</asp:ListItem>
                                <asp:ListItem Value="30"></asp:ListItem>
                                <asp:ListItem>31</asp:ListItem>
                                <asp:ListItem>32</asp:ListItem>
                                <asp:ListItem>33</asp:ListItem>
                                <asp:ListItem>34</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>36</asp:ListItem>
                                <asp:ListItem>37</asp:ListItem>
                                <asp:ListItem>38</asp:ListItem>
                                <asp:ListItem>39</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>41</asp:ListItem>
                                <asp:ListItem>42</asp:ListItem>
                                <asp:ListItem>43</asp:ListItem>
                                <asp:ListItem>44</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>46</asp:ListItem>
                                <asp:ListItem>47</asp:ListItem>
                                <asp:ListItem>48</asp:ListItem>
                                <asp:ListItem>49</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>51</asp:ListItem>
                                <asp:ListItem>52</asp:ListItem>
                                <asp:ListItem>53</asp:ListItem>
                                <asp:ListItem>54</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                                <asp:ListItem>56</asp:ListItem>
                                <asp:ListItem>57</asp:ListItem>
                                <asp:ListItem>58</asp:ListItem>
                                <asp:ListItem>59</asp:ListItem>
                            </asp:ListBox>
                            <asp:ListBox ID="ampmCheckOUT" runat="server" BackColor="#FFFFC0" Rows="1" CssClass="Buttonhand1hover">
                                <asp:ListItem Value="PM">PM</asp:ListItem>
                                <asp:ListItem Value="AM">AM</asp:ListItem>
                            </asp:ListBox>
                            <%--(hh:mm)--%>
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="width: 139px; height: 8px">
                        </td>
                        <td style="width: 304px; height: 8px">
                        </td>
                    </tr>
                    <tr style="color: #000000">
                        <td style="width: 139px; height: 31px">
                        </td>
                        <td style="height: 31px; width: 304px;">
                            <asp:Button ID="btnAddLodging" runat="server" BackColor="Red" BorderColor="DarkGray"
                                BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Add"
                                Width="87px" ValidationGroup="lodging" CssClass="Buttonhand1hover" />
                            <asp:Button ID="btnCancelLodging" runat="server" BackColor="Red" BorderColor="DarkGray"
                                BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Cancel"
                                Width="87px" CssClass="Buttonhand1hover" />
                        </td>
                    </tr>
                </table>
            </center>
    </asp:Panel>
    <!--End Lodging-->
    
    <!--Begin Required Transportation-->
    <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Black" Height="381px"
        Style="display: none; background-repeat: repeat-x; text-align: center" Width="524px">
        <br />
        <center>
            <table style="width: 450px; text-align: left">
                <tr style="color: #000000">
                    <td colspan="2" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                        height: 16px">
                        Required Transportation
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 16px">
                    </td>
                    <td style="color: red; text-align: right">
                        Please fill up all required fields in yellow.
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 16px">
                    </td>
                    <td style="height: 16px; width: 322px;">
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Incomplete Transportation Information.  Cannot Process"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="transpo" />
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px;">
                        Type of Vehicle :
                    </td>
                    <td style="width: 322px">
                        <asp:UpdatePanel ID="TransportationType_UpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="dpl_vehicle" AutoPostBack="true" runat="server" Font-Size="8pt"
                                    Width="232px" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="Land - Drop Only">Land - Drop Only</asp:ListItem>
                                    <asp:ListItem Value="Land - Waiting Only">Land - Waiting Only</asp:ListItem>
                                    <asp:ListItem Value="Land - Pick-up Only">Land - Pick-up Only</asp:ListItem>
                                    <asp:ListItem Value="Air - Economy Class">Air - Economy Clas</asp:ListItem>
                                    <asp:ListItem Value="Air - Business Class">Air - Business Class</asp:ListItem>
                                    <asp:ListItem Value="Sea">Sea</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="dpl_vehicle"
                                    ErrorMessage="VEHICLE TYPE cannot be blank. Please select one." Font-Size="Large"
                                    SetFocusOnError="True" ValidationGroup="transpo">*</asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dpl_vehicle"
                                    Font-Bold="False" Font-Size="1pt" ValidationGroup="transpo"></asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 16px">
                        If air, specity airline:
                    </td>
                    <td>
                        <asp:UpdatePanel ID="Airline_UpdatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="airline_code" Visible="false" DataSourceID="AirLineSqlDataSource"
                                    DataTextField="airline_name" DataValueField="airline_code" runat="server" Font-Size="8pt"
                                    Width="232px" CssClass="Buttonhand1hover">
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="AirLineSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                                    SelectCommand="SELECT [airline_code],[airline_name] FROM [ob_airline]" SelectCommandType="Text">
                                </asp:SqlDataSource>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dpl_vehicle" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 16px">
                        Date &nbsp;:
                    </td>
                    <td style="height: 16px; width: 322px;">
                        <asp:TextBox ID="txttranspodate" runat="server" Font-Size="8pt" Width="225px" BackColor="#FFFFC0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Date cannot be blank. Please enter date."
                            Font-Size="Large" SetFocusOnError="True" ValidationGroup="transpo" ControlToValidate="txttranspodate">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txttranspodate"
                            Font-Bold="False" Font-Size="1pt" ValidationGroup="transpo"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                            TargetControlID="txttranspodate" PopupButtonID="txttranspodate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 16px">
                        <strong>Destination :</strong>
                    </td>
                    <td style="height: 16px; width: 322px;">
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; text-align: right">
                        From :
                    </td>
                    <td style="vertical-align: top; width: 322px;">
                        <asp:TextBox ID="txtdestinationfrom" runat="server" Font-Size="8pt" Width="225px"
                            BackColor="#FFFFC0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtdestinationfrom"
                            ErrorMessage="DESTINATION FROM cannot be blank. Please select origin." Font-Size="Large"
                            SetFocusOnError="True" ValidationGroup="transpo">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtdestinationfrom"
                            Font-Bold="False" Font-Size="1pt" ValidationGroup="transpo"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; text-align: right">
                        To :
                    </td>
                    <td style="vertical-align: top; width: 322px;">
                        <asp:TextBox ID="txtdestinationto" runat="server" Font-Size="8pt" Width="225px" BackColor="#FFFFC0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtdestinationto"
                            ErrorMessage="DESTINATION TO cannot be blank. Please enter destination." Font-Size="Large"
                            SetFocusOnError="True" ValidationGroup="transpo">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtdestinationto"
                            Font-Bold="False" Font-Size="1pt" ValidationGroup="transpo"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px">
                        Flight / Vessel # :&nbsp;
                    </td>
                    <td style="vertical-align: top; width: 322px;">
                        <asp:TextBox ID="txtvessel" runat="server" Font-Size="8pt" Width="225px"></asp:TextBox>&nbsp;
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px">
                        ETD :
                    </td>
                    <td style="vertical-align: top; width: 322px;">
                        <asp:TextBox ID="txtetd" runat="server" Font-Size="8pt" Width="225px" BackColor="#FFFFC0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtetd"
                            ErrorMessage="ETD cannot be blank. Please enter ETD." Font-Size="Large" SetFocusOnError="True"
                            ValidationGroup="transpo">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtetd"
                            Font-Bold="False" Font-Size="1pt" ValidationGroup="transpo"></asp:RequiredFieldValidator>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptAMPM="True"
                            Mask="99:99:99" MaskType="Time" TargetControlID="txtetd">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px">
                        ETA :
                    </td>
                    <td style="vertical-align: top; width: 322px;">
                        <asp:TextBox ID="txteta" runat="server" Font-Size="8pt" Width="225px">
                        </asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" AcceptAMPM="True"
                            Mask="99:99:99" MaskType="Time" TargetControlID="txteta">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px">
                        Pick-up Time :
                    </td>
                    <td style="vertical-align: top; width: 322px;">
                        <asp:TextBox ID="txtpickup" runat="server" Font-Size="8pt" Width="225px">
                        </asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtpickup"
                            AcceptAMPM="True" Mask="99:99:99" MaskType="Time">
                        </cc1:MaskedEditExtender>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td colspan="2" style="color: white; border-bottom: gray 1px solid; height: 16px">
                        .
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 31px">
                    </td>
                    <td style="height: 31px; width: 322px;">
                        &nbsp;<asp:Button ID="btn_add_transpo" runat="server" BackColor="Red" BorderColor="DarkGray"
                            BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Add"
                            Width="87px" ValidationGroup="transpo" CssClass="Buttonhand1hover" />
                        <asp:Button ID="btnCancelTranspo" runat="server" BackColor="Red" BorderColor="DarkGray"
                            BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Cancel"
                            Width="87px" CssClass="Buttonhand1hover" />
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
    <!--End Required Transportation-->
    <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="Black" Height="200px"
        Style="display: none; background-repeat: repeat-x; text-align: center" Width="524px">
        <br />
        <center>
            <table style="width: 450px; text-align: left">
                <tr style="color: #000000">
                    <td colspan="2" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                        height: 16px">
                        Add Travel Allowance
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 139px; color: white">
                    </td>
                    <td style="color: red; text-align: right">
                        Please fill up all required fields in yellow.
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 139px; color: white">
                        .
                    </td>
                    <td style="vertical-align: top">
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" HeaderText="Incomplete Travel Allowance Information.  Cannot Process"
                            ShowMessageBox="True" ShowSummary="False" ValidationGroup="allowance" />
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 139px">
                        Purpose of Cash Advance :
                    </td>
                    <td style="vertical-align: top">
                        <asp:TextBox ID="txt_cash_purpose" runat="server" Font-Size="8pt" Width="255px" BackColor="#FFFFC0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txt_cash_purpose"
                            ErrorMessage="CASH ADVANCE cannot be blank. Please enter cash advance." Font-Size="Large"
                            SetFocusOnError="True" ValidationGroup="allowance">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txt_cash_purpose"
                            Font-Bold="False" Font-Size="1pt" ValidationGroup="allowance"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 139px; height: 24px;">
                        Amount :
                    </td>
                    <td style="vertical-align: top; height: 24px;">
                        <asp:TextBox ID="txt_cash_amount" runat="server" Font-Size="8pt" Width="121px" BackColor="#FFFFC0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txt_cash_amount"
                            ErrorMessage="AMOUNT cannot be blank. Please enter amount." Font-Size="Large"
                            SetFocusOnError="True" ValidationGroup="allowance">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txt_cash_amount"
                            Font-Bold="False" Font-Size="1pt" ValidationGroup="allowance"></asp:RequiredFieldValidator>
                        <%--          <cc1:MaskedEditExtender ID="MaskedEditExtender_cashamount" runat="server" 
                               ClearMaskOnLostFocus ="true"  TargetControlID="txt_cash_amount"  >
                        </cc1:MaskedEditExtender>--%>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 139px; height: 73px">
                    </td>
                    <td style="height: 73px">
                        <asp:Button ID="btn_add_allowance" runat="server" BackColor="Red" BorderColor="DarkGray"
                            BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Add"
                            Width="87px" ValidationGroup="allowance" CssClass="Buttonhand1hover" />
                        <asp:Button ID="btn_cancel_allowance" runat="server" BackColor="Red" BorderColor="DarkGray"
                            BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Cancel"
                            Width="87px" CssClass="Buttonhand1hover" /><br />
                        <br />
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 139px; height: 31px">
                    </td>
                    <td style="height: 31px">
                    </td>
                </tr>
            </table>
        </center>
        <br />
        <br />
    </asp:Panel>
    <%--        </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
