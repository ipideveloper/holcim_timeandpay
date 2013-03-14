<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" Trace="False" AutoEventWireup="false" CodeFile="UploadShift2.aspx.vb" Inherits="Default2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type = "text/javascript">

    function tab3() {
        var GridId = "<%=GridView6.ClientID %>";
        var ScrollHeight = 400;
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
    function gridtemp() {
        var GridId = "<%=GridView8.ClientID %>";
        var ScrollHeight = 400;
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

    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="3" 
        Height="845px" Width="1000px" Font-Size="8pt" ScrollBars="Both" 
        AutoPostBack="True">
        
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Shift Schedule">
            <HeaderTemplate>Shift Schedule</HeaderTemplate>
        

            

<ContentTemplate><table style="width: 100%"><tr><td style="width: 84px"></td><td style="width: 113px"></td><td style="width: 68px"></td><td></td><td>&#160;</td><td></td></tr><tr><td style="width: 84px"><asp:Label ID="Label10" runat="server" Text="Month"></asp:Label></td><td style="width: 113px"><asp:DropDownList ID="DDL_Month_SS" runat="server" Height="20px" Width="94px"><asp:ListItem Value="1">January</asp:ListItem><asp:ListItem Value="2">February</asp:ListItem><asp:ListItem Value="3">March</asp:ListItem><asp:ListItem Value="4">April</asp:ListItem><asp:ListItem Value="5">May</asp:ListItem><asp:ListItem Value="6">June</asp:ListItem><asp:ListItem Value="7">July</asp:ListItem><asp:ListItem Value="8">August</asp:ListItem><asp:ListItem Value="9">September</asp:ListItem><asp:ListItem Value="10">October</asp:ListItem><asp:ListItem Value="11">November</asp:ListItem><asp:ListItem Value="12">December</asp:ListItem></asp:DropDownList></td></tr>
    <caption>
        <tr>
            <td style="width: 68px">
                <asp:Label ID="Label11" runat="server" Text="Year"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DDL_Year_SS" runat="server" Height="20px" 
                    style="margin-left: 0px" Width="92px">
                </asp:DropDownList>
            </td>
            <caption>
                <caption>
                    <tr>
                        <td>
                            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="12pt" 
                                Text="Kindly click the shift to view history." Visible="False"></asp:Label>
                        </td>
                        <caption>
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </caption>
                    </tr>
                </caption>
            </caption>
        </tr>
    </caption>
    </table><table style="width: 100%"><tr><td><asp:Label ID="lbl_stat" runat="server" Font-Bold="True" Visible="False"></asp:Label>
                                </td></tr><caption><tr><td>
                                
                                </td></tr></caption>
        <caption>
            <tr>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Process" />
                    <asp:Button ID="Button4" runat="server" Text="View Schedule" Visible="False" />
                </td>
                <caption>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </caption>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </tr>
        </caption>
        <caption></caption><caption><tr><td><asp:Button ID="btn_ExportToExcel" runat="server" 
                                Text="Export Shift Schedule to Excel" Visible="False" /><asp:Button ID="btn_ExportToExcel0" runat="server" 
                                Text="Export Shift Schedule to Excel" Visible="False" /><asp:HyperLink ID="HyperLink1" runat="server" Visible="False">HyperLink</asp:HyperLink><asp:Label ID="Label_Error_Message" runat="server" Text="Label" Visible="False"></asp:Label></td><caption><tr><td></td></tr></caption></tr><caption><tr><td><div id="div-datagrid">
    <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#CC9966" 
                                        BorderStyle="None" BorderWidth="1px" 
        CellPadding="4" CssClass="Grid" 
                                        PageSize="15" Visible="False"><EditRowStyle Wrap="False" /><AlternatingRowStyle BackColor="#E0E0E0" CssClass="GridAltRow" />
        <HeaderStyle CssClass="locked" BackColor="#990000" Font-Bold="True" 
            ForeColor="#FFFFCC" Wrap="False" /><Columns>
            <asp:BoundField DataField="employee_name" 
                                                HeaderText="Employee Name">
                <HeaderStyle HorizontalAlign="Left" Wrap="False" Width="250px" /><ItemStyle HorizontalAlign="Left" /></asp:BoundField><asp:BoundField DataField="employee_id" HeaderText="Personnel No."><HeaderStyle Wrap="False" /></asp:BoundField>
            <asp:TemplateField HeaderText="1"><EditItemTemplate><asp:TextBox ID="TextBox55" runat="server" Text='<%# Bind("1") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_1" runat="server" CommandName="cmd1" 
                                                        Text='<%# Bind("1") %>' /></ItemTemplate><controlstyle width="100px" /><HeaderStyle Width="150px" /></asp:TemplateField><asp:TemplateField HeaderText="2"><EditItemTemplate><asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("2") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_2" runat="server" CommandName="cmd2" 
                                                        Text='<%# Bind("2") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="3"><EditItemTemplate><asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("3") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_3" runat="server" CommandName="cmd3" 
                                                        Text='<%# Bind("3") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="4"><EditItemTemplate><asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("4") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_4" runat="server" CommandName="cmd4" 
                                                        Text='<%# Bind("4") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="5"><EditItemTemplate><asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("5") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_5" runat="server" CommandName="cmd5" 
                                                        Text='<%# Bind("5") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="6"><EditItemTemplate><asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("6") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_6" runat="server" CommandName="cmd6" 
                                                        Text='<%# Bind("6") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="7"><EditItemTemplate><asp:TextBox ID="TextBox10" runat="server" Text='<%# Bind("7") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_7" runat="server" CommandName="cmd7" 
                                                        Text='<%# Bind("7") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="8"><EditItemTemplate><asp:TextBox ID="TextBox32" runat="server" Text='<%# Bind("8") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_8" runat="server" CommandName="cmd8" 
                                                        Text='<%# Bind("8") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="9"><EditItemTemplate><asp:TextBox ID="TextBox33" runat="server" Text='<%# Bind("9") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_9" runat="server" CommandName="cmd9" 
                                                        Text='<%# Bind("9") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="10"><EditItemTemplate><asp:TextBox ID="TextBox34" runat="server" Text='<%# Bind("10") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_10" runat="server" CommandName="cmd10" 
                                                        Text='<%# Bind("10") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="11"><EditItemTemplate><asp:TextBox ID="TextBox11" runat="server" Text='<%# Bind("11") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_11" runat="server" CommandName="cmd11" 
                                                        Text='<%# Bind("11") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="12"><EditItemTemplate><asp:TextBox ID="TextBox12" runat="server" Text='<%# Bind("12") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_12" runat="server" CommandName="cmd12" 
                                                        Text='<%# Bind("12") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="13"><EditItemTemplate><asp:TextBox ID="TextBox13" runat="server" Text='<%# Bind("13") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_13" runat="server" CommandName="cmd13" 
                                                        Text='<%# Bind("13") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="14"><EditItemTemplate><asp:TextBox ID="TextBox14" runat="server" Text='<%# Bind("14") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_14" runat="server" CommandName="cmd14" 
                                                        Text='<%# Bind("14") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="15"><EditItemTemplate><asp:TextBox ID="TextBox15" runat="server" Text='<%# Bind("15") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_15" runat="server" CommandName="cmd15" 
                                                        Text='<%# Bind("15") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="16"><EditItemTemplate><asp:TextBox ID="TextBox16" runat="server" Text='<%# Bind("16") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_16" runat="server" CommandName="cmd16" 
                                                        Text='<%# Bind("16") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="17"><EditItemTemplate><asp:TextBox ID="TextBox17" runat="server" Text='<%# Bind("17") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_17" runat="server" CommandName="cmd17" 
                                                        Text='<%# Bind("17") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="18"><EditItemTemplate><asp:TextBox ID="TextBox18" runat="server" Text='<%# Bind("18") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_18" runat="server" CommandName="cmd18" 
                                                        Text='<%# Bind("18") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="19"><EditItemTemplate><asp:TextBox ID="TextBox19" runat="server" Text='<%# Bind("19") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_19" runat="server" CommandName="cmd19" 
                                                        Text='<%# Bind("19") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="20"><EditItemTemplate><asp:TextBox ID="TextBox20" runat="server" Text='<%# Bind("20") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_20" runat="server" CommandName="cmd20" 
                                                        Text='<%# Bind("20") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="21"><EditItemTemplate><asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("21") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_21" runat="server" CommandName="cmd21" 
                                                        Text='<%# Bind("21") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="22"><EditItemTemplate><asp:TextBox ID="TextBox22" runat="server" Text='<%# Bind("22") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_22" runat="server" CommandName="cmd22" 
                                                        Text='<%# Bind("22") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="23"><EditItemTemplate><asp:TextBox ID="TextBox23" runat="server" Text='<%# Bind("23") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_23" runat="server" CommandName="cmd23" 
                                                        Text='<%# Bind("23") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="24"><EditItemTemplate><asp:TextBox ID="TextBox24" runat="server" Text='<%# Bind("24") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_24" runat="server" CommandName="cmd24" 
                                                        Text='<%# Bind("24") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="25"><EditItemTemplate><asp:TextBox ID="TextBox25" runat="server" Text='<%# Bind("25") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_25" runat="server" CommandName="cmd25" 
                                                        Text='<%# Bind("25") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="26"><EditItemTemplate><asp:TextBox ID="TextBox26" runat="server" Text='<%# Bind("26") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_26" runat="server" CommandName="cmd26" 
                                                        Text='<%# Bind("26") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="27"><EditItemTemplate><asp:TextBox ID="TextBox27" runat="server" Text='<%# Bind("27") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_27" runat="server" CommandName="cmd27" 
                                                        Text='<%# Bind("27") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="28"><EditItemTemplate><asp:TextBox ID="TextBox28" runat="server" Text='<%# Bind("28") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_28" runat="server" CommandName="cmd28" 
                                                        Text='<%# Bind("28") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="29"><EditItemTemplate><asp:TextBox ID="TextBox29" runat="server" Text='<%# Bind("29") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_29" runat="server" CommandName="cmd29" 
                                                        Text='<%# Bind("29") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="30"><EditItemTemplate><asp:TextBox ID="TextBox30" runat="server" Text='<%# Bind("30") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_30" runat="server" CommandName="cmd30" 
                                                        Text='<%# Bind("30") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="31"><EditItemTemplate><asp:TextBox ID="TextBox31" runat="server" Text='<%# Bind("31") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_31" runat="server" CommandName="cmd31" 
                                                        Text='<%# Bind("31") %>'></asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><FooterStyle BackColor="#FFFFCC" ForeColor="#330099" /><PagerStyle BackColor="#FFFFCC" ForeColor="#330099" /><RowStyle ForeColor="DimGray" Wrap="False" /><SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" 
                                        Wrap="False" /></asp:GridView></div></td><td></td></tr></caption></caption></table><table style="width: 100%"><caption><tr><td>
                                        <div id ="Div1">
                                        <asp:GridView ID="GridView9" runat="server" CellPadding="4" 
                                Width="100%" AutoGenerateColumns="False" BackColor="White" 
                                BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" PageSize="15"><RowStyle ForeColor="DimGray" Wrap="False" /><Columns><asp:BoundField DataField="employee_name" HeaderText="Employee Name"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="employee_id" HeaderText="Personnel No."><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="1" HeaderText="1"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="2" HeaderText="2"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="3" HeaderText="3"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="4" HeaderText="4"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="5" HeaderText="5"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="6" HeaderText="6"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="7" HeaderText="7"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="8" HeaderText="8"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="9" HeaderText="9"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="10" HeaderText="10"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="11" HeaderText="11"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="12" HeaderText="12"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="13" HeaderText="13"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="14" HeaderText="14"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="15" HeaderText="15"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="16" HeaderText="16"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="17" HeaderText="17"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="18" HeaderText="18"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="19" HeaderText="19"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="20" HeaderText="20"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="21" HeaderText="21"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="22" HeaderText="22"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="23" HeaderText="23"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="24" HeaderText="24"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="25" HeaderText="25"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="26" HeaderText="26"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="27" HeaderText="27"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="28" HeaderText="28"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="29" HeaderText="29"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="30" HeaderText="30"><HeaderStyle Wrap="False" /></asp:BoundField><asp:BoundField DataField="31" HeaderText="31"><HeaderStyle Wrap="False" /></asp:BoundField></Columns><FooterStyle BackColor="#FFFFCC" ForeColor="#330099" /><PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" /><SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" /><HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" /><AlternatingRowStyle BackColor="#E0E0E0" /></asp:GridView></td><td></td></tr></caption><caption><tr><td><asp:GridView ID="GridView5" runat="server" CellPadding="4" ForeColor="#333333" 
                            Width="254px"><RowStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="False" /><FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" /><SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /><HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" /><EditRowStyle BackColor="#999999" Wrap="False" /><AlternatingRowStyle BackColor="White" ForeColor="#284775" /></asp:GridView></div></td><caption><tr><td></td></tr></caption></tr></caption></table></ContentTemplate>
        

            

</cc1:TabPanel>
        
        
        <cc1:TabPanel runat="server" HeaderText="Shift Schedule Batch Upload" ID="TabPanel1">
        <HeaderTemplate>Shift Schedule Batch Upload</HeaderTemplate>
            

            

<ContentTemplate><table style="width: 100%"><tr><td style="width: 84px"></td><td style="width: 141px"></td><td style="width: 68px"></td><td></td></tr><tr><td style="width: 84px"><asp:Label ID="Label_Month" runat="server" 
                        Text="Month"></asp:Label></td><td style="width: 141px"><asp:DropDownList ID="DDL_Month_Update" runat="server" Height="20px" 
                    Width="94px"><asp:ListItem Value="1">January</asp:ListItem><asp:ListItem Value="2">February</asp:ListItem><asp:ListItem Value="3">March</asp:ListItem><asp:ListItem Value="4">April</asp:ListItem><asp:ListItem Value="5">May</asp:ListItem><asp:ListItem Value="6">June</asp:ListItem><asp:ListItem Value="7">July</asp:ListItem><asp:ListItem Value="8">August</asp:ListItem><asp:ListItem Value="9">September</asp:ListItem><asp:ListItem Value="10">October</asp:ListItem><asp:ListItem Value="11">November</asp:ListItem><asp:ListItem Value="12">December</asp:ListItem></asp:DropDownList></td><td style="width: 68px"><asp:Label ID="Label_Year" runat="server" Text="Year"></asp:Label></td><td><asp:DropDownList ID="DDL_Year_Update" runat="server" Height="20px" 
                            Width="92px"></asp:DropDownList></td></tr><tr><td style="width: 84px"></td><td style="width: 141px"></td><td style="width: 68px"></td><td></td></tr></table><table><tr><td></td><td><asp:FileUpload ID="FileUpload1" runat="server" Height="22px" 
                            style="margin-bottom: 0px" Width="688px" /></td><td></td><td></td><td></td></tr><tr><td></td><td><asp:Button ID="UploadCSVFile" runat="server" Height="23px" 
                                Text="Upload CSV File" Visible="False" Width="123px" /><asp:Label ID="Label5" runat="server" Text="Label" Visible="False"></asp:Label><asp:Button ID="Button1" runat="server" Height="22px" Text="Upload" 
                                Width="125px" /></td><td></td><td></td><td></td></tr></table><table style="width: 200%; height: 231px"><tr><td></td><td></td><td></td><td></td><td></td><td></td></tr><tr><td></td><td><asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="12pt" 
                    Text="Data from Excel" Visible="False"></asp:Label></td><td></td><td></td><td></td><td></td></tr><tr><td></td><td><asp:GridView ID="GridView1" runat="server" 
                BorderColor="#336666" Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" 
                        BackColor="White" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
                        GridLines="Horizontal"><FooterStyle BackColor="White" ForeColor="#333333" /><PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" /><SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" 
                            Wrap="True" /><HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" 
                            Wrap="True" /><EditRowStyle Wrap="True" /><RowStyle Wrap="False" HorizontalAlign="Center" BackColor="White" 
                            ForeColor="#333333" /></asp:GridView></td><td></td><td></td><td></td><td></td></tr><tr><td></td><td></td><td></td></tr><tr><td></td><td><asp:Button ID="btn_Capture" runat="server" Height="23px" Text="Capture" 
                Visible="False" Width="126px" /><asp:Button ID="btn_Save" runat="server" Height="23px" Text="Submit" 
                Visible="False" Width="126px" /><asp:Button ID="btn_cancel" runat="server" Height="24px" Text="Cancel" 
                Visible="False" Width="122px" /></td><td></td></tr><tr><td></td><td><asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Size="12pt" 
                                    Text="Data to be Updated" Visible="False"></asp:Label></td><td></td></tr><tr><td></td><td><asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="4" Font-Size="8pt" Width="669px"><AlternatingRowStyle BackColor="#E0E0E0" /><Columns><asp:BoundField DataField="employee_name" HeaderText="Employee Name" /><asp:BoundField DataField="employee_id" HeaderText="Personnel No." /><asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" /><asp:BoundField DataField="shift_from" HeaderText="From" /><asp:BoundField DataField="shift_to" HeaderText="To" /></Columns><FooterStyle BackColor="#FFFFCC" ForeColor="#330099" /><HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" /><PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" /><RowStyle ForeColor="DimGray" Wrap="True" /><SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" /></asp:GridView><br /><table style="width: 100%"><tr><td><asp:GridView ID="GridView3" runat="server"></asp:GridView></td><td></td></tr><tr><td></td><td></td></tr></table></td><td></td></tr></table></ContentTemplate>
        

            

</cc1:TabPanel>
        
        
        
        
        <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
        <HeaderTemplate>Shift Schedule Requests Status</HeaderTemplate>
        

            

<ContentTemplate>

            <div>
            <asp:GridView ID="GridView6" runat="server"  
            AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="85%"><AlternatingRowStyle 
            BackColor="#E0E0E0" /><Columns><asp:TemplateField HeaderText="Ref. No"><EditItemTemplate><asp:TextBox 
                ID="TextBox1" runat="server" Text='<%# Bind("ref_no") %>'>
                        </asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_ref_no" runat="server" __designer:wfdid="w17" 
                    CommandName="cmd_ref_no" Text='<%# Bind("ref_no") %>'>
                    </asp:LinkButton></ItemTemplate></asp:TemplateField><asp:BoundField DataField="planner_id" 
                HeaderText="Planner ID"></asp:BoundField><asp:BoundField 
                DataField="planner_name" HeaderText="Planner Name"></asp:BoundField><asp:BoundField 
                DataField="Date Filed" HeaderText="Date Filed"></asp:BoundField><asp:BoundField 
                DataField="Disapproval_Remarks" HeaderText="Reason for Disapproved"></asp:BoundField><asp:TemplateField 
                HeaderText="Status"><EditItemTemplate><asp:TextBox ID="TextBox1" runat="server" 
                    Text='<%# Bind("status") %>'></asp:TextBox></EditItemTemplate><ItemTemplate><asp:LinkButton ID="lnk_approver_shift" runat="server" 
                    CommandName="cmd_approver_shift" Font-Underline="False" ForeColor="Red" 
                    Text='<%# Bind("status") %>'></asp:LinkButton><cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
                    BackgroundCssClass="modalBackground" CancelControlID="btn_close" 
                    PopupControlID="Panel2" TargetControlID="lnk_approver_shift"></cc1:ModalPopupExtender><asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Black" 
                    Height="353px" 
                    style="DISPLAY: none; BACKGROUND-REPEAT: repeat-x; TEXT-ALIGN: center" 
                    Width="559px"><br /><br /><center><table 
                    style="WIDTH: 500px; HEIGHT: 302px; TEXT-ALIGN: left"><tbody><tr style="COLOR: #000000"><td colspan="3" 
                    style="FONT-WEIGHT: bold; COLOR: gray; BORDER-BOTTOM: gray 1px solid">Approvers</td></tr><tr style="COLOR: #000000"><td colspan="3" 
                    style="VERTICAL-ALIGN: top; COLOR: white; BORDER-BOTTOM: gray 1px solid; HEIGHT: 173px"><asp:Panel ID="Panel3" runat="server" 
                    Height="230px" ScrollBars="Vertical" Width="100%"><asp:GridView ID="gv_approvers_shift" 
                    runat="server" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    Width="96%"><RowStyle ForeColor="DimGray" 
                    Wrap="True"></RowStyle><Columns><asp:BoundField DataField="employee_name" HeaderText="Name"></asp:BoundField><asp:BoundField DataField="approver_level" HeaderText="Level"></asp:BoundField><asp:BoundField DataField="status" HeaderText="Status"></asp:BoundField><asp:BoundField DataField="approval_date" HeaderText="Approval Date"></asp:BoundField></Columns><FooterStyle BackColor="#FFFFCC" 
                    ForeColor="#330099"></FooterStyle><PagerStyle BackColor="#FFFFCC" 
                    ForeColor="#330099" HorizontalAlign="Center"></PagerStyle><SelectedRowStyle 
                    BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle><HeaderStyle BackColor="#990000" 
                    Font-Bold="True" ForeColor="#FFFFCC"></HeaderStyle><AlternatingRowStyle 
                    BackColor="#E0E0E0"></AlternatingRowStyle></asp:GridView> </div></asp:Panel></td></tr><tr style="COLOR: #000000"><td colspan="3" 
                    style="HEIGHT: 31px; TEXT-ALIGN: center"><asp:Button ID="btn_close" runat="server" 
                    BackColor="Red" BorderColor="DarkGray" BorderWidth="1px" Font-Names="arial" 
                    Font-Size="8pt" ForeColor="White" Text="Close" ValidationGroup="Val" 
                    Width="87px"></asp:Button></td></tr></tbody></table></center></asp:Panel></ItemTemplate><ItemStyle Wrap="False"></ItemStyle></asp:TemplateField></Columns><FooterStyle BackColor="#FFFFCC" ForeColor="#330099" /><HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" /><PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" /><RowStyle ForeColor="DimGray" Wrap="False" /><SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" /></asp:GridView>
                <div>
                <asp:GridView ID="GridView8" runat="server" CellPadding="4" ForeColor="#333333" 
                    GridLines="Horizontal" Width="798px">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                </asp:GridView>
                </div>
<table style="width: 100%"></table></ContentTemplate>
        

            

</cc1:TabPanel>
        
        
        <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
        <HeaderTemplate>Shift Schedule History</HeaderTemplate>
        

            

<ContentTemplate><table style="width: 100%"><tr><td style="width: 126px"></td><td></td></tr><tr><td style="width: 126px"><asp:Label ID="Label12" runat="server" 
                    Text="Work Date From"></asp:Label></td><td><asp:TextBox ID="DateFrom" runat="server"></asp:TextBox><cc1:CalendarExtender ID="DateFrom_CalendarExtender" runat="server" 
                    TargetControlID="DateFrom" Enabled="True" Format="dd-MMM-yyyy"></cc1:CalendarExtender></td></tr><tr><td style="width: 126px"><asp:Label ID="Label13" runat="server" 
                    Text="Work Date To"></asp:Label></td><td><asp:TextBox ID="DateTo" runat="server"></asp:TextBox><cc1:CalendarExtender ID="DateTo_CalendarExtender" runat="server" 
                        TargetControlID="DateTo" Enabled="True" Format="dd-MMM-yyyy"></cc1:CalendarExtender></td></tr><tr><td style="width: 126px"></td><td><asp:Button ID="Button3" runat="server" Text="View History" /></td></tr><tr><td style="width: 126px"></td><td><asp:Button ID="btn_HistoryToExcel" runat="server" Text="Export to excel" 
                Visible="False" Width="189px" /></td></tr></table><table><tr><td><asp:GridView ID="GridView7" runat="server" 
                    CellPadding="4" ForeColor="#333333" Width="100%"><RowStyle BackColor="#FFFBD6" ForeColor="#333333" Wrap="False" /><FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" /><SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" /><HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" /><EditRowStyle Wrap="False" /><AlternatingRowStyle BackColor="White" /></asp:GridView></td></tr></table></ContentTemplate>
        

            

</cc1:TabPanel>
        
        
    </cc1:TabContainer>

    <script type="text/javascript">



        function DisplayFullImage(ctrlimg) {

            txtCode = "<HTML><HEAD>"

        + "</HEAD><BODY TOPMARGIN=0 LEFTMARGIN=0 MARGINHEIGHT=0 MARGINWIDTH=0><CENTER>"

        + "<IMG src='" + ctrlimg.src + "' BORDER=0 NAME=FullImage "

        + "onload='window.resizeTo(document.FullImage.width,document.FullImage.height)'>"

        + "</CENTER>"

        + "</BODY></HTML>";

            mywindow = window.open('', 'image', 'toolbar=0,location=0,menuBar=0,scrollbars=0,resizable=0,width=1,height=1');

            mywindow.document.open();

            mywindow.document.write(txtCode);

            mywindow.document.close();

        }

        
        

</script> 
  
    


</asp:Content>

