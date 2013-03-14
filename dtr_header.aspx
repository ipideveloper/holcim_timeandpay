<%@ Page Title="Holcim - DTR Application" Language="VB" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="false" CodeFile="dtr_header.aspx.vb" Inherits="dtr_header" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="btnAdd" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
        Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Apply" Width="87px"
        CssClass="Buttonhand1hover" />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
        AllowPaging="true" PageSize="10" BorderColor="#A0A0A0" BorderStyle="Solid" BorderWidth="1px"
        CellPadding="3" AllowSorting="true" Width="98%" HeaderStyle-Font-Size="12px"
        RowStyle-VerticalAlign="Top">
        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" LastPageText="Last" FirstPageText="First"
            NextPageText="Next" PreviousPageText="Prev" />
        <PagerStyle HorizontalAlign="Left" Font-Size="Small" />
        <RowStyle ForeColor="DimGray" Wrap="True" />
        <Columns>
            <%--<asp:TemplateField HeaderText="...">
                <ItemTemplate>
                    <asp:LinkButton ID="lnk_edit_ot" runat="server" CommandName="cmd_edit_ot" Text='<%# bind("isedit") %>'
                        ForeColor="Red"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Ref. No" SortExpression="ref_no">
                <EditItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox1"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lnk_ref_no" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                        CommandName="cmd_ref_no" Font-Underline="False"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:TemplateField>
            <asp:BoundField DataField="employee_name" HeaderText="Employee Name" SortExpression="employee_name">
            </asp:BoundField>
            <asp:BoundField DataField="employee_id" HeaderText="Personnel No." SortExpression="employee_id">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="CoveredDate" HeaderText="Covered Date" SortExpression="CoveredDate">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created">
                <ItemStyle HorizontalAlign="Center" Wrap="false" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Status" SortExpression="status">
                <EditItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Bind("status") %>' ID="TextBox2"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <center>
                        <asp:LinkButton ID="lnk_approver" runat="server" ForeColor="Red" Text='<%# Bind("status") %>'
                            CommandName="cmd_approver" Font-Underline="False"></asp:LinkButton>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                            CancelControlID="btn_close" BackgroundCssClass="modalBackground" TargetControlID="lnk_approver">
                        </cc1:ModalPopupExtender>
                    </center>
                    <asp:Panel Style="display: none; background-repeat: repeat-x; text-align: center"
                        ID="Panel2" runat="server" Width="559px" BorderColor="Black" BackColor="White"
                        Height="353px">
                        <br />
                        <br />
                        <center>
                            <table style="width: 500px; height: 302px; text-align: left">
                                <tbody>
                                    <tr style="color: #000000">
                                        <td style="font-weight: bold; color: gray; border-bottom: gray 1px solid" colspan="3">
                                            Approvers
                                        </td>
                                    </tr>
                                    <tr style="color: #000000">
                                        <td style="vertical-align: top; color: white; border-bottom: gray 1px solid; height: 173px"
                                            colspan="3">
                                            <asp:Panel ID="Panel3" runat="server" Width="100%" ScrollBars="Vertical" Height="230px">
                                                <asp:GridView ID="gv_approvers" runat="server" Width="96%" BorderWidth="1px" BorderColor="#CC9966"
                                                    BackColor="White" CellPadding="4" BorderStyle="None" AutoGenerateColumns="False">
                                                    <RowStyle Wrap="True" ForeColor="DimGray"></RowStyle>
                                                    <Columns>
                                                        <asp:BoundField DataField="employee_name" HeaderText="Name"></asp:BoundField>
                                                        <asp:BoundField DataField="approver_level" HeaderText="Level"></asp:BoundField>
                                                        <asp:BoundField DataField="status" HeaderText="Status"></asp:BoundField>
                                                        <asp:BoundField DataField="approval_date" HeaderText="Approval Date"></asp:BoundField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099"></FooterStyle>
                                                    <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="#330099"></PagerStyle>
                                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle>
                                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC"></HeaderStyle>
                                                    <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr style="color: #000000">
                                        <td style="height: 31px; text-align: center" colspan="3">
                                            <asp:Button ID="btn_close" runat="server" ForeColor="White" Font-Size="8pt" Width="87px"
                                                Text="Close" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red" Font-Names="arial"
                                                ValidationGroup="Val"></asp:Button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </center>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <PagerStyle BackColor="#F1EDED" ForeColor="#330099" CssClass="pager" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" CssClass="tblheader" />
        <AlternatingRowStyle BackColor="#E0E0E0" />
    </asp:GridView>
</asp:Content>
