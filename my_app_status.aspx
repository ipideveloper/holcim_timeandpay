<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="my_app_status.aspx.vb" Inherits="my_app_status" Title="Holcim - My Application Status" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--     
/*var GridId = "<%=gv_ob.ClientID %>";*/
var hGridClientID = document.getElementById('<%=hdnGridClientID.ClientID%>')
        var GridId = document.getElementById(hGridClientID.value);
         var GridId = "ctl00_ContentPlaceHolder1_gv_ob";
        var GridId = "ctl00_ContentPlaceHolder1_gv_overtime";
--%>
    <script type="text/javascript">

        function button1() {
            var GridId = "<%=gv_ob.ClientID %>";
            var ScrollHeight = 320;
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

        function button2() {
            var GridId = "<%=gv_overtime.ClientID %>";
            var ScrollHeight = 320;
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
                    gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width) + "px";
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
        function button3() {
            var GridId = "<%=gv_oncall.ClientID %>";
            var ScrollHeight = 320;
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
        function button4() {
            var GridId = "<%=GridView1.ClientID %>";
            var ScrollHeight = 320;
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
        function button5() {
            var GridId = "<%=gv_shift.ClientID %>";
            var ScrollHeight = 320;
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
    <style>
    th {font-size:11px;}
    
    </style>

    <asp:Button ID="Button1" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="1" Text="Travel Order" UseSubmitBehavior="true" Width="100px" Font-Bold="False" />
    <asp:Button ID="Button2" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="2" Text="Overtime / Extra Shift" UseSubmitBehavior="true" Width="140px"
        Font-Bold="False" />
    <asp:Button ID="Button3" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="3" Text="On Call" UseSubmitBehavior="true" Width="100px" Font-Bold="False" />
    <asp:Button ID="Button4" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="4" Text="Leaves" UseSubmitBehavior="true" Width="100px" Font-Bold="False" />
    <asp:Button ID="Button5" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="5" Text="Shift Schedule" UseSubmitBehavior="true" Width="120px" Font-Bold="False" />
    <asp:Button ID="Button_DTR" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="6" Text="DTR" UseSubmitBehavior="true" Width="120px" Font-Bold="False" />
    <%--    &nbsp;<asp:Button ID="Button1" runat="server" Text="Travel Order" />      
    &nbsp;<asp:Button ID="Button2" runat="server" Text="Overtime / Extra Shift" />
    &nbsp;<asp:Button ID="Button3" runat="server" Text="On Call" />
    &nbsp;<asp:Button ID="Button4" runat="server" Text="Leaves" />
    &nbsp;<asp:Button ID="Button5" runat="server" Text="Shift Schedule" />
    &nbsp;--%>
    <br />
    <asp:Table ID="Table1" runat="server" CellPadding="1" CellSpacing="1">
    </asp:Table>
    <br />
    <asp:Panel ID="PN1" runat="server">
        <div id="Div1">
            <asp:GridView runat="server" AutoGenerateColumns="False" CellPadding="4" BackColor="White"
                BorderColor="#CC9966" BorderWidth="1px" BorderStyle="None" Font-Size="8pt" Width="100%"
                ID="gv_ob" AllowSorting="True">
                <RowStyle Wrap="False" ForeColor="DimGray"></RowStyle>
                <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_ob" runat="server" CommandName="cmd_edit_ob" ForeColor="Red"
                                Text='<%# bind("isedit") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ref. No">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_ref_no_ob" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                                Font-Underline="False" CommandName="cmd_ref_no_ob"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox2"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="travel_date" HeaderText="Travel Date" SortExpression="travel_date">
                    </asp:BoundField>
                    <asp:BoundField DataField="created_by" HeaderText="Planner" SortExpression="created_by">
                    </asp:BoundField>
                    <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created">
                    </asp:BoundField>
                    <asp:BoundField DataField="Disapproval_Remarks" HeaderText="Reason for Disapproval">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_approver_ob" runat="server" ForeColor="Red" Text='<%# Bind("status") %>'
                                Font-Underline="False" CommandName="cmd_approver_ob"></asp:LinkButton>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                                BackgroundCssClass="modalBackground" TargetControlID="lnk_approver_ob" CancelControlID="btn_close">
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
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%" Height="230px" ScrollBars="Vertical">
                                                        <asp:GridView ID="gv_approvers_ob" runat="server" Width="96%" BorderWidth="1px" BorderColor="#CC9966"
                                                            BackColor="White" BorderStyle="None" CellPadding="4" AutoGenerateColumns="False">
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
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("status") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099"></FooterStyle>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC"></HeaderStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="#330099"></PagerStyle>
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle>
            </asp:GridView>
        </div>
        <br />
    </asp:Panel>
    <asp:Panel ID="PN2" runat="server">
        <div>
            <asp:GridView runat="server" AutoGenerateColumns="False" CellPadding="2" BackColor="White"
                BorderColor="#CC9966" BorderWidth="1px" BorderStyle="None" Font-Size="8pt" ID="gv_overtime"
                AllowSorting="True">
                <RowStyle ForeColor="DimGray"></RowStyle>
                <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_ot" runat="server" CommandName="cmd_edit_ot" ForeColor="Red"
                                Text='<%# bind("isedit") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ref. No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="8pt" ItemStyle-Width="84px" ItemStyle-Wrap="True">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_ref_no_ot" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                                Font-Underline="False" CommandName="cmd_ref_no_ot"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox2"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="work_date" HeaderText="Requested Date and Time" SortExpression="work_date" ItemStyle-Width="160px">
                    </asp:BoundField>
                    <asp:BoundField DataField="created_by" HeaderText="Planner" SortExpression="created_by" ItemStyle-HorizontalAlign="Center">
                    </asp:BoundField>
                    <asp:BoundField DataField="classification" HeaderText="Classification" HeaderStyle-Font-Size="8pt" ItemStyle-Width="76px" ItemStyle-Wrap="true">
                    </asp:BoundField>
                    <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                    </asp:BoundField>
                    <asp:BoundField DataField="Disapproval_Remarks" HeaderText="Reason for Disapproval" ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="8pt" ItemStyle-Width="130px">
                    
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_approver_ot" runat="server" ForeColor="Red" Text='<%# Bind("status") %>'
                                Font-Underline="False" CommandName="cmd_approver_ot"></asp:LinkButton>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                                BackgroundCssClass="modalBackground" TargetControlID="lnk_approver_ot" CancelControlID="btn_close">
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
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%" Height="230px" ScrollBars="Vertical">
                                                        <asp:GridView ID="gv_approvers_ot" runat="server" Width="96%" BorderWidth="1px" BorderColor="#CC9966"
                                                            BackColor="White" BorderStyle="None" CellPadding="4" AutoGenerateColumns="False">
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
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("status") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099"></FooterStyle>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC"></HeaderStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="#330099"></PagerStyle>
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle>
            </asp:GridView>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN3" runat="server">
        <div>
            <asp:GridView runat="server" AutoGenerateColumns="False" CellPadding="4" BackColor="White"
                BorderColor="#CC9966" BorderWidth="1px" BorderStyle="None" Font-Size="8pt" Width="100%"
                ID="gv_oncall" AllowSorting="True">
                <RowStyle Wrap="False" ForeColor="DimGray"></RowStyle>
                <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_oncall" runat="server" CommandName="cmd_edit_oncall" ForeColor="Red"
                                Text='<%# bind("isedit") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ref. No">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_ref_no_oncall" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                                Font-Underline="False" CommandName="cmd_ref_no_oncall"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox2"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="work_date" HeaderText="Requested Date and Time" SortExpression="work_date">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="created_by" HeaderText="Planner" SortExpression="created_by">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Disapproval_Remarks" HeaderText="Reason for Disapproval">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_approver_oncall" runat="server" ForeColor="Red" Text='<%# Bind("status") %>'
                                Font-Underline="False" CommandName="cmd_approver_oncall"></asp:LinkButton>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                                BackgroundCssClass="modalBackground" TargetControlID="lnk_approver_oncall" CancelControlID="btn_close">
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
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%" Height="230px" ScrollBars="Vertical">
                                                        <asp:GridView ID="gv_approvers_oncall" runat="server" Width="96%" BorderWidth="1px"
                                                            BorderColor="#CC9966" BackColor="White" BorderStyle="None" CellPadding="4" AutoGenerateColumns="False">
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
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("status") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099"></FooterStyle>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC"></HeaderStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="#330099"></PagerStyle>
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle>
            </asp:GridView>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN4" runat="server">
        <div>
            <asp:GridView runat="server" AutoGenerateColumns="False" CellPadding="2" BackColor="White"
                BorderColor="#CC9966" BorderWidth="1px" BorderStyle="None" Font-Size="8pt" Width="97%"
                ID="GridView1" AllowSorting="True">
                <RowStyle Wrap="False" ForeColor="DimGray"></RowStyle>
                <AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="...">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_edit" runat="server" CommandName="cmd_edit_leave" ForeColor="Red"
                                Text='<%# bind("isedit") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ref. No">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_ref_no_leave" runat="server" CommandName="cmd_ref_no_leave"
                                Font-Underline="False" ForeColor="Red" Text='<%# Bind("ref_no") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ref_no") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="employee_name" HeaderText="Employee Name" SortExpression="employee_name">
                    </asp:BoundField>
                    <asp:BoundField DataField="application_type" HeaderText="Application Type" SortExpression="application_type">
                    </asp:BoundField>
                    <asp:BoundField DataField="date_covered" HeaderText="Date Covered" SortExpression="date_covered">
                    </asp:BoundField>
                    <asp:BoundField DataField="days" HeaderText="Days"></asp:BoundField>
                    <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created">
                    </asp:BoundField>
                    <asp:BoundField DataField="Disapproval_Remarks" HeaderText="Reason for Disapproval">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_approver" runat="server" CommandName="cmd_approver" Font-Underline="False"
                                ForeColor="Red" Text='<%# Bind("status") %>'></asp:LinkButton>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="btn_close" PopupControlID="Panel2" TargetControlID="lnk_approver">
                            </cc1:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Black" Height="353px"
                                Style="display: none; background-repeat: repeat-x; text-align: center" Width="559px">
                                <br />
                                <br />
                                <center>
                                    <table style="width: 500px; height: 302px; text-align: left">
                                        <tr style="color: #000000">
                                            <td colspan="3" style="font-weight: bold; color: gray; border-bottom: gray 1px solid">
                                                Approvers
                                            </td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td colspan="3" style="vertical-align: top; color: white; border-bottom: gray 1px solid;
                                                height: 173px">
                                                <asp:Panel ID="Panel3" runat="server" Height="230px" ScrollBars="Vertical" Width="100%">
                                                    <asp:GridView ID="gv_approvers_leave" runat="server" AutoGenerateColumns="False"
                                                        BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="4" Font-Size="8pt" Width="96%">
                                                        <RowStyle ForeColor="DimGray" Wrap="True" />
                                                        <Columns>
                                                            <asp:BoundField DataField="employee_name" HeaderText="Name" />
                                                            <asp:BoundField DataField="approver_level" HeaderText="Level" />
                                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                                            <asp:BoundField DataField="approval_date" HeaderText="Approval Date" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr style="color: #000000">
                                            <td colspan="3" style="height: 31px; text-align: center">
                                                <asp:Button ID="btn_close" runat="server" BackColor="Red" BorderColor="DarkGray"
                                                    BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Close"
                                                    ValidationGroup="Val" Width="87px" />
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </asp:Panel>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("status") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099"></FooterStyle>
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC"></HeaderStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="#330099"></PagerStyle>
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle>
            </asp:GridView>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN5" runat="server">
        <div>
            <asp:GridView ID="gv_shift" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
                Font-Size="8pt" AllowSorting="True">
                <RowStyle ForeColor="DimGray" Wrap="True" />
                <Columns>
                    <asp:TemplateField HeaderText="Ref. No">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox2"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_ref_no_shift" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                                Font-Underline="False" CommandName="cmd_ref_no_shift"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Planner" HeaderText="Planner" SortExpression="Planner">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="Date Filed">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Disapproval_Remarks" HeaderText="Reason for Disapproval">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("status") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_approver_shift" runat="server" ForeColor="Red" Text='<%# Bind("status") %>'
                                Font-Underline="False" CommandName="cmd_approver_shift"></asp:LinkButton>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                                BackgroundCssClass="modalBackground" TargetControlID="lnk_approver_shift" CancelControlID="btn_close">
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
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%" Height="230px" ScrollBars="Vertical">
                                                        <asp:GridView ID="gv_approvers_shift" runat="server" Width="96%" BorderWidth="1px"
                                                            BorderColor="#CC9966" BackColor="White" BorderStyle="None" CellPadding="4" AutoGenerateColumns="False">
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
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <AlternatingRowStyle BackColor="#E0E0E0" />
            </asp:GridView>
        </div>
    </asp:Panel>
    <asp:Panel ID="PN6" runat="server">
        <div>
            <asp:GridView ID="gv_dtr" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
                Font-Size="8pt" AllowSorting="True">
                <RowStyle ForeColor="DimGray" Wrap="True" />
                <Columns>
                    <asp:TemplateField HeaderText="Ref. No" SortExpression="ref_no" >
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' ID="TextBox2"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_ref_no_dtr" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>'
                                Font-Underline="False" CommandName="cmd_ref_no_shift"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Wrap="False"></ItemStyle>
                     
                    </asp:TemplateField>
                   <%-- <asp:BoundField DataField="employee_name" HeaderText="Employee Name" SortExpression="employee_name">
                    </asp:BoundField>
                    <asp:BoundField DataField="employee_id" HeaderText="Personnel No." SortExpression="employee_id">
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Wrap="false" />
                    </asp:BoundField>--%>
                    
                    <asp:BoundField DataField="CoveredDate" HeaderText="Covered Date" SortExpression="CoveredDate">
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Planner_name" HeaderText="Planner" SortExpression="Planner_name">
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                     
                    <asp:BoundField DataField="date_created" HeaderText="Date Filed" SortExpression="date_created">
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                    </asp:BoundField>
                   <asp:BoundField DataField="Disapproval_Remarks" HeaderText="Reason for Disapproval">
                    </asp:BoundField>
                    
                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("status") %>' ID="TextBox1"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_approver_dtr" runat="server" ForeColor="Red" Text='<%# Bind("status") %>'
                                Font-Underline="False" CommandName="cmd_approver_dtr"></asp:LinkButton>
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                                BackgroundCssClass="modalBackground" TargetControlID="lnk_approver_dtr" CancelControlID="btn_close">
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
                                                    <asp:Panel ID="Panel3" runat="server" Width="100%" Height="230px" ScrollBars="Vertical">
                                                        <asp:GridView ID="gv_approvers_dtr" runat="server" Width="96%" BorderWidth="1px"
                                                            BorderColor="#CC9966" BackColor="White" BorderStyle="None" CellPadding="4" AutoGenerateColumns="False">
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
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                <AlternatingRowStyle BackColor="#E0E0E0" />
            </asp:GridView>
        </div>
    </asp:Panel>
    <br />
</asp:Content>
