<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="ob_header.aspx.vb" Inherits="ob_header" Title="Holcim - Official Business" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function freezeheader() {
            var GridId = "<%=Gridview1.ClientID %>";
            var ScrollHeight = 323;
            window.onload = function() {
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
        }

    
    </script>

    <asp:Button ID="btnAdd" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
        Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Apply" Width="87px"
        OnClick="btnAdd_Click" CssClass="Buttonhand1hover" />
    <table style="width: 100%">
        <tr>
            <td style="width: 88px">
                <!--Select Sort Type-->
            </td>
            <td style="width: 123px">
                <asp:DropDownList ID="DpSortOB" runat="server" Enabled="False">
                    <asp:ListItem Value="employee_name">Employee Name</asp:ListItem>
                    <asp:ListItem Value="employee_id">Personnel Number</asp:ListItem>
                    <asp:ListItem Value="travel_date">Travel Date</asp:ListItem>
                    <asp:ListItem Value="date_created">Date Filed</asp:ListItem>
                    <asp:ListItem>Status</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="BtnSortOB" runat="server" Text="Sort" Enabled="False" />
            </td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CC9966" BorderStyle="None" AllowSorting="true" BorderWidth="1px"
            CellPadding="4" Width="100%">
            <RowStyle ForeColor="DimGray" Wrap="True" />
            <Columns>
                <asp:TemplateField HeaderText="...">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_edit_ob" runat="server" CommandName="cmd_edit_ob" ForeColor="Red"
                            Text='<%# bind("isedit") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ref. No" SortExpression="ref_no">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_ref_no" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                            CommandName="cmd_ref_no" Font-Underline="False"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="employee_name" HeaderText="Employee Name" SortExpression="employee_name">
                </asp:BoundField>
                <asp:BoundField DataField="employee_id" HeaderText="Personnel No." SortExpression="employee_id">
                </asp:BoundField>
                <asp:BoundField DataField="travel_date" HeaderText="Travel Date" SortExpression="travel_date">
                </asp:BoundField>
                <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created">
                </asp:BoundField>
                <asp:TemplateField HeaderText="Status" SortExpression="status">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("status") %>' ID="TextBox2"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_approver" runat="server" ForeColor="Red" Text='<%# Bind("status") %>'
                            CommandName="cmd_approvers" Font-Underline="False">
                        </asp:LinkButton>
                        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btn_close"
                            TargetControlID="lnk_approver" BackgroundCssClass="modalBackground" PopupControlID="Panel2">
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
                                                    <asp:GridView ID="gv_approvers" Visible="true" runat="server" Width="96%" BorderWidth="1px" BorderColor="#CC9966"
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
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <AlternatingRowStyle BackColor="#E0E0E0" />
        </asp:GridView>
    </div>
</asp:Content>
