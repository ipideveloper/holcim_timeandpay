<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="leave_app_header.aspx.vb" Inherits="leave_app_header" Title="Holcim - Leave Applications" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
    .pager td {padding:0px;}
    .pager a {padding:3px; padding-top:5px;height:20px; display:block;}
    .pager a:hover {padding:3px;background-color:#CBD1E1;padding-top:5px;}
    .pager span {padding:3px; background-color:#CBD1E1; padding-top:5px;display:block;height:20px;}
    
    .tblheader th{font-size:11px;}
    </style>
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

    <asp:Button ID="btnApply" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px" 
        Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Apply" Width="87px"
        OnClick="btnApply_Click" CssClass="Buttonhand1hover" />
    <asp:Button ID="btnSearch" runat="server" BackColor="Red" BorderColor="DarkGray"
        BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Search"
        Width="87px" Visible="False" CssClass="Buttonhand1hover" />
    &nbsp;&nbsp;
    <%--<asp:Panel id="Panel1" runat="server" Height="360px" ScrollBars="Vertical" 
        Width="101%">--%><br />
    <div>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                loading data...
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <table style="width: 100%;">
        <tr>
            <td style="width: 92px">
                <!--Select Sort Type:-->
            </td>
            <td style="width: 157px">
                <asp:DropDownList ID="DpSortLeave" runat="server" Enabled="False">
                    <asp:ListItem Value="employee_id">Employee ID</asp:ListItem>
                    <asp:ListItem Value="employee_name">Employee Name</asp:ListItem>
                    <asp:ListItem Value="application_type ">Application Type</asp:ListItem>
                    <asp:ListItem Value="date_covered">Date Covered</asp:ListItem>
                    <asp:ListItem Value="days">Days</asp:ListItem>
                    <asp:ListItem Value="date_created">Date Filed</asp:ListItem>
                    <asp:ListItem Value="status">Status</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="LeaveSort" runat="server" Text="Sort" Enabled="False" />
            </td>
        </tr>
    </table>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
            AllowSorting="True">
            <RowStyle ForeColor="DimGray" Wrap="False" />
            <Columns>
                <asp:TemplateField HeaderText="Ref. No">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_ref_no" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                            Font-Underline="False" CommandName="cmd_ref_no"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="employee_id" HeaderText="Employee ID" SortExpression="employee_id" />
                <asp:BoundField DataField="employee_name" HeaderText="Employee Name" SortExpression="employee_name" />
                <asp:BoundField DataField="application_type" HeaderText="Application Type" SortExpression="application_type">
                </asp:BoundField>
                <asp:BoundField DataField="date_covered" HeaderText="Date Covered" SortExpression="date_covered">
                </asp:BoundField>
                <asp:BoundField DataField="days" HeaderText="Days" SortExpression="days"></asp:BoundField>
                <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created">
                </asp:BoundField>
                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# Bind("status") %>' ID="TextBox2"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_approver" runat="server" ForeColor="Red" Text='<%# Eval("status") %>' Font-Underline="False"></asp:LinkButton>
                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" CssClass="tblheader" />
            <AlternatingRowStyle BackColor="#E0E0E0" />
        </asp:GridView>
    </div>
    <%--</asp:Panel>--%>
    <%--<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnk_approver"
                            BackgroundCssClass="modalBackground" CancelControlID="btn_close" PopupControlID="Panel2">
                        </cc1:ModalPopupExtender>
                       --%>
</asp:Content>