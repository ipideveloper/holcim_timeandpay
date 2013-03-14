<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="my_dtr.aspx.vb" Inherits="my_dtr" Title="Holcim - My DTR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

    <table style="width: 100%" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td style="vertical-align: top">
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
                    &nbsp;<asp:DropDownList ID="dpl_year" runat="server" Font-Size="8pt" CssClass="Buttonhand1hover">
                        <asp:ListItem>- - Select Year - -</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btn_bind" OnClick="btn_bind_Click" runat="server" ForeColor="White"
                        Font-Size="8pt" Width="87px" Text="Display" BorderWidth="1px" BorderColor="DarkGray"
                        BackColor="Red" CssClass="Buttonhand1hover" Font-Names="arial"></asp:Button>&nbsp;
                        
                    <asp:Button ID="btnPrintDTR" runat="server" Text="Print"
                        ForeColor="White" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red" CssClass="Buttonhand1hover"
                        Font-Names="arial" Font-Size="8pt" Width="87px"></asp:Button>
                </td>
                <td style="vertical-align: top; text-align: right">
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top">
                    <asp:TextBox ID="txtnolog" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="XX-Large"
                        Width="525px" Visible="False" ReadOnly="True" Font-Italic="True" BorderStyle="None">No logs for this month</asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
    <div>
        <asp:GridView ID="GridView1" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CC9966"
            BackColor="White" CellPadding="4" BorderStyle="None" AutoGenerateColumns="False">
            <RowStyle ForeColor="DimGray" Wrap="True" />
            <Columns>
                <asp:BoundField DataField="tran_date" HeaderText="Work Date">
                    <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="log_time" HeaderText="Time">
                    <HeaderStyle HorizontalAlign="Left" Width="100px"></HeaderStyle>
                    <ItemStyle Width="100px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="tran_type" HeaderText="Type">
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
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
