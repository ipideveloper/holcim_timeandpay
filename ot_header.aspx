<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="ot_header.aspx.vb" Inherits="ot_header" Title="Holcim - Overtime" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<script type="text/javascript">
        var GridId = "<%=GridView1.ClientID %>";
        var ScrollHeight = 320;
        window.onload = function fixheader() {
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            gridWidth = parseInt(gridWidth) + 17;
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }
    </script>--%>
    <style type="text/css">
    .pager td {padding:0px;}
    .pager a {padding:3px; padding-top:5px;height:20px; display:block;}
    .pager a:hover {padding:3px;background-color:#CBD1E1;padding-top:5px;}
    .pager span {padding:3px; background-color:#CBD1E1; padding-top:5px;display:block;height:20px;}
    
    .tblheader th{font-size:11px;}
    </style>
    <asp:Button ID="btnAdd" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
        Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Apply" Width="87px"
        CssClass="Buttonhand1hover" />
    <asp:Button ID="btnSearch" runat="server" BackColor="Red" BorderColor="DarkGray"
        BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Search"
        Width="87px" Visible="False" CssClass="Buttonhand1hover" />
    &nbsp;
    <table style="width: 100%;">
        <tr>
            <td style="width: 92px">
                <!--Select Sort Type-->
            </td>
            <td style="width: 131px">
                <asp:DropDownList ID="Dpsort" runat="server" Enabled="False">
                    <asp:ListItem Value="employee_name">Employee Name</asp:ListItem>
                    <asp:ListItem Value="employee_id">Personnel Number</asp:ListItem>
                    <asp:ListItem Value="date_created">Date Filed</asp:ListItem>
                    <asp:ListItem Value="status">Status</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnsort" runat="server" Text="Sort" Enabled="False" />
            </td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            AllowPaging="true" PageSize="10" BorderColor="#A0A0A0" BorderStyle="Solid" BorderWidth="1px"
            CellPadding="3" AllowSorting="true" Width="98%" HeaderStyle-Font-Size="12px" RowStyle-VerticalAlign="Top">
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" LastPageText="Last" FirstPageText="First"
                NextPageText="Next" PreviousPageText="Prev" />
            <PagerStyle HorizontalAlign="Left" Font-Size="Small" />
            <RowStyle ForeColor="DimGray" Wrap="True" />
            <Columns>
                <asp:TemplateField HeaderText="...">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_edit_ot" runat="server" CommandName="cmd_edit_ot" Text='<%# bind("isedit") %>'
                            ForeColor="Red"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ref. No" SortExpression="ref_no">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_ref_no" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                            CommandName="cmd_ref_no" Font-Underline="False"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                   
                </asp:TemplateField>
                <asp:BoundField DataField="employee_name" HeaderText="Employee Name" SortExpression="employee_name">
                </asp:BoundField>
                <asp:BoundField DataField="employee_id" HeaderText="Personnel No." SortExpression="employee_id">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle Wrap="false" />
                </asp:BoundField>
                <asp:BoundField DataField="work_date" HeaderText="Requested Date and Time" SortExpression="work_date">
                <ItemStyle Wrap="false"  />
                </asp:BoundField>
                <asp:BoundField DataField="classification" HeaderText="Classification" SortExpression="classification" />
                <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created">
                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Status" SortExpression="status">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("status") %>' ID="TextBox2"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_approver" runat="server" ForeColor="Red" Text='<%# Bind("status") %>'
                            CommandName="cmd_approver" Font-Underline="False"></asp:LinkButton>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                            CancelControlID="btn_close" BackgroundCssClass="modalBackground" TargetControlID="lnk_approver">
                        </cc1:ModalPopupExtender>
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
    </div>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnSearch"
        BackgroundCssClass="modalBackground" CancelControlID="btnCancel" PopupControlID="Panel2"
        BehaviorID="ctl01_ModalPopupExtender1">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Black" Height="436px"
        Style="display: none; background-repeat: repeat-x; text-align: center" Width="524px">
        <br />
        <br />
        <center>
            <table style="width: 450px; text-align: left;">
                <tr style="color: #000000">
                    <td colspan="2" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                        height: 16px">
                        Add Employee Overtime
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 16px">
                    </td>
                    <td style="height: 16px">
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 16px">
                        <strong>Employee Name :</strong>
                    </td>
                    <td style="height: 16px">
                        <asp:DropDownList ID="dplEmployee" runat="server" Font-Size="8pt" Width="290px" CssClass="Buttonhand1hover">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dplEmployee"
                            ErrorMessage="*" Font-Bold="True" Font-Size="14pt" ValidationGroup="Val">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr style="color: #000000; font-weight: bold;">
                    <td style="width: 120px; height: 16px">
                        Date of Overtime :
                    </td>
                    <td style="height: 16px">
                        <asp:TextBox ID="txtDate" runat="server" Font-Size="8pt" TabIndex="2" Width="75px"
                            CssClass="Buttonhand1hover"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate"
                            ErrorMessage="*" Font-Bold="True" Font-Size="14pt" ValidationGroup="Val"></asp:RequiredFieldValidator>&nbsp;
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate">
                        </cc1:CalendarExtender>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 16px">
                        <strong>From :</strong>
                    </td>
                    <td style="height: 16px">
                        <strong></strong>
                        <asp:DropDownList ID="dplFrom" runat="server" CssClass="Buttonhand1hover">
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
                        &nbsp;<asp:ListBox ID="lstFrom" runat="server" Rows="1" CssClass="Buttonhand1hover">
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem Value="30"></asp:ListItem>
                        </asp:ListBox>
                        &nbsp;<asp:ListBox ID="ampmFrom" runat="server" Rows="1" CssClass="Buttonhand1hover">
                            <asp:ListItem Value="AM">AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:ListBox>
                        <br />
                        hrs:mins
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 16px">
                        <strong>To :</strong>
                    </td>
                    <td style="height: 16px">
                        <asp:DropDownList ID="dplTo" runat="server" CssClass="Buttonhand1hover">
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
                        &nbsp;<asp:ListBox ID="lstTo" runat="server" Rows="1" CssClass="Buttonhand1hover">
                            <asp:ListItem>00</asp:ListItem>
                            <asp:ListItem Value="30"></asp:ListItem>
                        </asp:ListBox>
                        &nbsp;<asp:ListBox ID="ampmTo" runat="server" Rows="1" CssClass="Buttonhand1hover">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:ListBox>
                        <br />
                        hrs:mins
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px">
                        <strong>Charge to CC :&nbsp;</strong>
                    </td>
                    <td style="vertical-align: top">
                        <asp:TextBox ID="txtChargetocc" runat="server" Font-Size="8pt" Width="225px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px">
                    </td>
                    <td style="vertical-align: top">
                        <asp:CheckBox ID="CheckBox1" runat="server" Font-Bold="True" Text="Call In" CssClass="Buttonhand1hover" />
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px">
                        <strong>Remarks :&nbsp;</strong>
                    </td>
                    <td style="vertical-align: top">
                        <asp:TextBox ID="txtRemarks" runat="server" Font-Size="8pt" Width="303px" Height="56px"
                            TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="height: 16px; border-bottom: gray 1px solid; color: white;" colspan="2">
                        .
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 120px; height: 31px;">
                    </td>
                    <td style="height: 31px;">
                        <asp:Button ID="btnSave" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Save" ValidationGroup="Val"
                            Width="87px" CssClass="Buttonhand1hover" />
                        <asp:Button ID="btnCancel" runat="server" BackColor="Red" BorderColor="DarkGray"
                            BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Cancel"
                            Width="87px" CssClass="Buttonhand1hover" />
                    </td>
                </tr>
            </table>
        </center>
    </asp:Panel>
</asp:Content>
