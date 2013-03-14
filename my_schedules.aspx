<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="my_schedules.aspx.vb" Inherits="my_schedules" Title="Holcim - My Schedules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        function freezeheader() {
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

    
    </script>

    <asp:DropDownList ID="dpl_month" runat="server" Font-Size="8pt" Width="147px" CssClass="Buttonhand1hover">
        <asp:ListItem>- - Select Month - -</asp:ListItem>
        <asp:ListItem>January</asp:ListItem>
        <asp:ListItem>February</asp:ListItem>
        <asp:ListItem>March</asp:ListItem>
        <asp:ListItem>April</asp:ListItem>
        <asp:ListItem>May</asp:ListItem>
        <asp:ListItem>June</asp:ListItem>
        <asp:ListItem>July</asp:ListItem>
        <asp:ListItem>August</asp:ListItem>
        <asp:ListItem>September</asp:ListItem>
        <asp:ListItem>October</asp:ListItem>
        <asp:ListItem>November</asp:ListItem>
        <asp:ListItem>December</asp:ListItem>
    </asp:DropDownList>
    <asp:DropDownList ID="dpl_year" runat="server" Font-Size="8pt" CssClass="Buttonhand1hover">
        <asp:ListItem>- - Select Year - -</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="btn_bind" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
        Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Display" Width="87px"
        OnClick="btn_bind_Click" CssClass="Buttonhand1hover" />&nbsp;
        
        <asp:Button ID="btnPrint" runat="server" Text="Print"
                        ForeColor="White" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red" CssClass="Buttonhand1hover"
                        Font-Names="arial" Font-Size="8pt" Width="87px"></asp:Button>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%"
            EnableSortingAndPagingCallbacks="True">
            <RowStyle ForeColor="DimGray" Wrap="True" />
            <Columns>
                <asp:BoundField DataField="work_date" HeaderText="Work Date">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="day_name" HeaderText="Day">
                    <HeaderStyle HorizontalAlign="Left" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="shift_code" HeaderText="Shift Code">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="shift_time" HeaderText="Shift Time">
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <AlternatingRowStyle BackColor="#E0E0E0" />
        </asp:GridView>
    </div>
</asp:Content>
