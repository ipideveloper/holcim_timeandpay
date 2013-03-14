<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="my_shift_approval_header.aspx.vb" Inherits="my_shift_approval_header"
    Title="Holcim - My Shift Schedule Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="btn_SelectAll" runat="server" Text="Select All" />
                    </td>
                    <td>
                        <asp:Button ID="btn_Approved" runat="server" Text="Approve" Style="height: 26px" />
                    </td>
                    <td>
                        <asp:Button ID="btn_Disapprove" runat="server" Text="Disapprove" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 90px">
                                    <!--Select Sort Type-->
                                </td>
                                <td style="width: 135px">
                                    <asp:DropDownList ID="DpMyShiftapp" runat="server" Enabled="False">
                                        <asp:ListItem Value="planner_name">Requested By</asp:ListItem>
                                        <asp:ListItem Value="planner_id">Personnel Number</asp:ListItem>
                                        <asp:ListItem Value="year">Year</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="Btnmyshiftapp" runat="server" Text="Sort" Enabled="False" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                ForeColor="#333333" GridLines="None" Width="100%" AllowSorting="True">
                <RowStyle ForeColor="DimGray" Wrap="false" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_select" runat="server" AutoPostBack="False" CssClass="Buttonhand1hover">
                            </asp:CheckBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_view" runat="server" CommandName="cmd_view" Font-Underline="False"
                                ForeColor="Navy">VIEW</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="planner_name" HeaderText="Requested by" SortExpression="planner_name" HtmlEncode="false" HtmlEncodeFormatString="false" />
                    <asp:BoundField DataField="planner_id" HeaderText="Personnel No." SortExpression="planner_id" HtmlEncode="false" HtmlEncodeFormatString="false" />
                    <asp:BoundField DataField="month" />
                    <asp:BoundField HeaderText="Month Requested" SortExpression="month" />
                    <asp:BoundField DataField="year" HeaderText="Year" SortExpression="toyear" HtmlEncode="false" HtmlEncodeFormatString="false"/>
                    <asp:TemplateField HeaderText="Reason for Disapproval">
                        <ItemTemplate>
                            <asp:TextBox ID="DisApprovedRemarks" runat="server" Height="22px" Visible="true" Width="268px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ref_no" HeaderText="Reference Number" SortExpression="ref_no"/>
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="#E0E0E0" />
            </asp:GridView>
            <div id="DivDisAppRem">
                <asp:Label ID="LabelDisApproved" Text="Please add reason for disapproval for the ff reference no."
                    runat="server" Visible="False" Font-Bold="True" Font-Size="12pt"></asp:Label>
                <br />
                <asp:ListBox ID="ListBox1" runat="server" Font-Bold="True" Font-Size="10pt" Visible="False">
                </asp:ListBox>
            </div>
        <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="GridView1"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
