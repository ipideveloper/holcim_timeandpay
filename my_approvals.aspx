<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="my_approvals.aspx.vb" Inherits="my_approvals" Title="Holcim - My Approvals" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
    <div style="float:right; margin-right:300px; color: Green">Processing... please wait...</div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Button ID="btn_SelectAll" runat="server" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Select All" CssClass="Buttonhand1hover"
                            BackColor="Red" />
                        <asp:Button ID="btn_Approved" runat="server" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Approve" CssClass="Buttonhand1hover"
                            BackColor="Red" />
                        <asp:Button ID="btn_Disapprove" runat="server" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Disapprove" CssClass="Buttonhand1hover"
                            BackColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 295px"><table style="width: 100%">
                            <tr>
                                <td style="width: 93px">
                                    <!--Select Sort Type-->
                                </td>
                                <td style="width: 129px">
                                    <asp:DropDownList ID="DpMyApprovals" runat="server" Enabled="False">
                                        <asp:ListItem Value="employee_id">Personnel Number</asp:ListItem>
                                        <asp:ListItem Value="employee_name">Employee Name</asp:ListItem>
                                        <asp:ListItem Value="application_desc">Application Type</asp:ListItem>
                                        <asp:ListItem Value="date_created">Date Filed</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="Btnmyappsort" runat="server" Text="Sort" Enabled="False" />
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
            </table>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <asp:GridView ID="GridView1" runat="server" Width="100%" AllowSorting="true" BorderWidth="1px"
                BorderColor="#CC9966" BackColor="White" CellPadding="4" BorderStyle="None" AutoGenerateColumns="False"
                HeaderStyle-Font-Size="Smaller" >
                <RowStyle Wrap="False" ForeColor="DimGray"></RowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_select" runat="server" AutoPostBack="False" CssClass="Buttonhand1hover">
                            </asp:CheckBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ref. No." SortExpression="ref_no">
                        <HeaderStyle Font-Size="11px" />
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox2"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_ref_no" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                                CommandName="cmd_ref_no" Font-Underline="False"></asp:LinkButton>
                            <asp:HiddenField ID="h_app_type" runat="server" Value='<%# Bind("application_type") %>'>
                            </asp:HiddenField>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="employee_id" HeaderText="Personnel No." SortExpression="employee_id">
                        <HeaderStyle Font-Size="11px" />
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="employee_name" HeaderText="Employee Name" SortExpression="employee_name">
                        <HeaderStyle Font-Size="11px" />
                        <ItemStyle VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="application_desc" HeaderText="Application Type" SortExpression="application_desc">
                        <HeaderStyle Font-Size="11px" />
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="classification" HeaderText="Classification" SortExpression="classification">
                        <HeaderStyle Font-Size="11px" />
                        <ItemStyle VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CoveredDate" HeaderText="Covered Date" SortExpression="CoveredDate">
                        <HeaderStyle Font-Size="11px" />
                        <ItemStyle VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created">
                        <HeaderStyle Font-Size="11px" />
                        <ItemStyle VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Reason for Disapproval">
                        <HeaderStyle Font-Size="11px" />
                        <ItemTemplate>
                            <asp:TextBox ID="DisApprovedRemarks" TextMode="MultiLine" runat="server" Height="40px"
                                Font-Names="Arial" Font-Size="11px" Visible="true" Width="140px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Decision"><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" __designer:wfdid="w35"></asp:TextBox> 
</EditItemTemplate>
<ItemTemplate>
<asp:LinkButton id="lnk_approve" runat="server" Font-Bold="True" ForeColor="Navy" CommandName="cmd_approve" __designer:wfdid="w31" Font-Underline="False">APPROVE</asp:LinkButton>&nbsp; / &nbsp;<asp:LinkButton id="lnk_dis_approve" runat="server" ForeColor="Red" CommandName="cmd__dis_approve" __designer:wfdid="w32" Font-Underline="False">DISAPPROVE</asp:LinkButton> <cc1:ConfirmButtonExtender id="ConfirmButtonExtender1" runat="server" __designer:wfdid="w33" TargetControlID="lnk_approve" ConfirmOnFormSubmit="True" ConfirmText="Are you sure you want to Approve this application?"></cc1:ConfirmButtonExtender> <cc1:ConfirmButtonExtender id="ConfirmButtonExtender2" runat="server" __designer:wfdid="w34" TargetControlID="lnk_dis_approve" ConfirmOnFormSubmit="True" ConfirmText="Are you sure you want to Disapprove this application?"></cc1:ConfirmButtonExtender> 
</ItemTemplate>
</asp:TemplateField>--%>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099"></FooterStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="#330099"></PagerStyle>
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Font-Size="Smaller">
                </HeaderStyle>
                <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
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
