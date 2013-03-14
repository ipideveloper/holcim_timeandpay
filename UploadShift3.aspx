<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    EnableEventValidation="false" CodeFile="UploadShift3.aspx.vb" Inherits="Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function tab3() {
            var GridId = "<%=GridView6.ClientID %>";
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

        function tab2() {
            var GridId = "<%=Gridview1.ClientID %>";
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

        function tab4() {
            var GridId = "<%=Gridview7.ClientID %>";
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
        th
        {
            font-size: 11px;
            border: 1px solid #CCC;
        }
    </style>
    <asp:Button ID="btn1" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="1" Text="Shift Schedule" UseSubmitBehavior="true" Width="100px" Font-Bold="False" />
    <asp:Button ID="btn2" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="1" Text="Shift Schedule Batch Upload" UseSubmitBehavior="true" Width="160px"
        Font-Bold="False" />
    <asp:Button ID="btn3" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="1" Text="Shift Schedule Request Status" UseSubmitBehavior="true" Width="163px"
        Font-Bold="False" />
    <asp:Button ID="btn4" runat="server" BackColor="White" BorderColor="Silver" onmouseover="this.style.cursor = 'hand'"
        BorderWidth="1px" Font-Size="8pt" ForeColor="#696969" Style="text-align: center"
        TabIndex="1" Text="Shift Schedule History" UseSubmitBehavior="true" Width="130px"
        Font-Bold="False" />
    <%--<asp:Button ID="btn1" runat="server" Text="Shift Schedule" />
    <asp:Button ID="btn2" runat="server" Text="Shift Schedule Batch Upload" />
    <asp:Button ID="btn3" runat="server" Text="Shift Schedule Request Status" />
    <asp:Button ID="btn4" runat="server" Text="Shift Schedule History" />--%>
    <asp:Panel ID="PN1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="width: 16px">
                    &nbsp;
                </td>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 142px">
                                &nbsp;
                            </td>
                            <td style="width: 185px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 142px">
                                &nbsp; Month
                            </td>
                            <td style="width: 185px">
                                &nbsp;
                                <asp:DropDownList ID="DDL_Month_SS" runat="server" Height="20px" Width="94px">
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 142px">
                                &nbsp; Year
                            </td>
                            <td style="width: 185px">
                                &nbsp;
                                <asp:DropDownList ID="DDL_Year_SS" runat="server" Height="20px" Style="margin-left: 0px"
                                    Width="92px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="8pt" Text="Kindly click the shift to view history."></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <contenttemplate><asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline" UpdateMode="Conditional"><ContentTemplate>   
                            <asp:UpdateProgress 
                ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" 
                DisplayAfter="10">
                <progresstemplate><img src="images/updateprogress1.gif" 
                style="width: 24px; height: 23px" /> Processing ...............</progresstemplate></asp:UpdateProgress>   
                            <asp:Label ID="lbl_stat" runat="server" Font-Bold="True" Visible="False"></asp:Label><br />   
                            <asp:Button ID="Button2" runat="server" Text="Process" />
                            
                </ContentTemplate>
                </asp:UpdatePanel>   
                             
                                <asp:Button ID="Button4" runat="server" Text="View Schedule" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 142px">
                                &nbsp;
                            </td>
                            <td style="width: 185px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 142px">
                                &nbsp;
                            </td>
                            <td style="width: 185px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 16px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PN2" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="width: 16px">
                    &nbsp;
                </td>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 17px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17px">
                                <asp:Label ID="Label_Month" runat="server" Text="Month"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_Month_Update" runat="server" Height="20px" Width="94px">
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17px">
                                <asp:Label ID="Label_Year" runat="server" Text="Year"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_Year_Update" runat="server" Height="20px" Width="92px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:FileUpload ID="FileUpload1" runat="server" Height="22px" Style="margin-bottom: 0px"
                                    Width="688px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="UploadCSVFile" runat="server" Height="23px" Text="Upload CSV File"
                                    Visible="False" Width="123px" />
                                <asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label>
                                <asp:Button ID="Button1" runat="server" Height="22px" Text="Upload" Width="125px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12pt" Text="Data from Excel"
                                    Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div>
                                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#336666"
                                        BorderStyle="Double" BorderWidth="3px" CellPadding="0" Font-Names="Arial" Font-Size="8pt"
                                        GridLines="Horizontal"  Width="95%">
                                        <EditRowStyle Wrap="True" CssClass="AndLeft" />
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" Wrap="True" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#333333" HorizontalAlign="Left" Wrap="False" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" Wrap="True" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btn_Capture" runat="server" Height="23px" Text="Capture" Visible="False"
                                    Width="126px" />
                                <asp:Button ID="btn_Save" runat="server" Height="23px" Text="Submit" Visible="False"
                                    Width="126px" />
                                <asp:Button ID="btn_cancel" runat="server" Height="24px" Text="Cancel" Visible="False"
                                    Width="122px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12pt" Text="Data to be Updated"
                                    Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="8pt"
                                    Width="669px">
                                    <AlternatingRowStyle BackColor="#E0E0E0" />
                                    <Columns>
                                        <asp:BoundField DataField="employee_name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="employee_id" HeaderText="Personnel No." />
                                        <asp:BoundField DataField="date" DataFormatString="{0:MM/dd/yyyy}" HeaderText="Date" />
                                        <asp:BoundField DataField="shift_from" HeaderText="From" />
                                        <asp:BoundField DataField="shift_to" HeaderText="To" />
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle ForeColor="DimGray" Wrap="True" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="GridView3" runat="server">
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 17px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 16px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PN3" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="width: 16px">
                    &nbsp;
                </td>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="3">
                                <div>
                                    <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="97%"
                                        AllowSorting="True">
                                        <AlternatingRowStyle BackColor="#E0E0E0" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Ref. No">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ref_no") %>'>
                                                    </asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_ref_no" runat="server" __designer:wfdid="w17" CommandName="cmd_ref_no"
                                                        Text='<%# Bind("ref_no") %>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="planner_id" HeaderText="Planner ID" SortExpression="planner_id" />
                                            <asp:BoundField DataField="planner_name" HeaderText="Planner Name" SortExpression="planner_name" />
                                            <asp:BoundField DataField="Date Filed" HeaderText="Date Filed" SortExpression="Date Filed" />
                                            <asp:BoundField DataField="Disapproval_Remarks" HeaderText="Reason for Disapproval" />
                                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("status") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk_approver_shift" runat="server" CommandName="cmd_approver_shift"
                                                        Font-Underline="False" ForeColor="Red" Text='<%# Bind("status") %>'></asp:LinkButton>
                                                    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground"
                                                        CancelControlID="btn_close" PopupControlID="Panel2" TargetControlID="lnk_approver_shift">
                                                    </cc1:ModalPopupExtender>
                                                    <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Black" Height="353px"
                                                        Style="display: none; background-repeat: repeat-x; text-align: center" Width="559px">
                                                        <br />
                                                        <br />
                                                        <center>
                                                            <table style="width: 500px; height: 302px; text-align: left">
                                                                <tbody>
                                                                    <tr style="color: #000000">
                                                                        <td colspan="3" style="font-weight: bold; color: gray; border-bottom: gray 1px solid">
                                                                            Approvers
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="color: #000000">
                                                                        <td colspan="3" style="vertical-align: top; color: white; border-bottom: gray 1px solid;
                                                                            height: 173px">
                                                                            <asp:Panel ID="Panel3" runat="server" Height="230px" ScrollBars="Vertical" Width="100%">
                                                                                <asp:GridView ID="gv_approvers_shift" runat="server" AutoGenerateColumns="False"
                                                                                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px"
                                                                                    CellPadding="4" Width="96%">
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
                                                                                </div>
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
                                                                </tbody>
                                                            </table>
                                                        </center>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                        <RowStyle ForeColor="DimGray" Wrap="False" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 259px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="GridView8" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Horizontal"
                                    Width="798px">
                                    <AlternatingRowStyle BackColor="White" />
                                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 16px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PN4" runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="width: 16px">
                    &nbsp;
                </td>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 97px">
                                Work Shift From
                            </td>
                            <td>
                                <asp:TextBox ID="DateFrom" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="DateFrom_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="DateFrom">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 97px">
                                Work Shift To
                            </td>
                            <td>
                                <asp:TextBox ID="DateTo" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="DateTo_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="DateTo">
                                </cc1:CalendarExtender>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 97px">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="View History" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 97px">
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btn_HistoryToExcel" runat="server" Text="Export to excel" Visible="False"
                                    Width="189px" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 97px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 97px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div>
                                    <asp:GridView ID="GridView7" runat="server" CellPadding="0" CellSpacing="0" ForeColor="#333333"
                                        Width="97%">
                                        <EditRowStyle Wrap="False" />
                                        <AlternatingRowStyle BackColor="White" />
                                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="False" ForeColor="White" Font-Size="Smaller" />
                                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" Wrap="False" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 97px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 16px">
                    &nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>