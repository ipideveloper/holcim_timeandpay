<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ot_header2.aspx.vb" Inherits="Default2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type = "text/javascript">
    var GridId = "<%=GridView1.ClientID %>";
    var ScrollHeight = 300;
    window.onload = function () {
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
</script>


<asp:Button id="btnAdd" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
        Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Apply" Width="87px" OnClick="btnAdd_Click" CssClass="Buttonhand1hover" />
        
        <%--<asp:Panel id="Panel1" runat="server" Height="508px" Width="100%" >--%>
            
            <div  >
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    Width="100%" GridLines="Vertical" style="margin-top: 0px" 
                    >
            <rowstyle forecolor="Black" wrap="True" BackColor="#EEEEEE" />
            
            <columns>
            <asp:TemplateField HeaderText="...">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk_edit_ob" runat="server" CommandName="cmd_edit_ob" ForeColor="Red"
                            Text='<%# bind("isedit") %>'></asp:LinkButton>
                    </ItemTemplate>
                    </asp:TemplateField>
            
                    <asp:TemplateField HeaderText="Ref. No"><EditItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' id="TextBox1"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                    <asp:LinkButton id="lnk_ref_no" runat="server" ForeColor="Red" Text='<%# Bind("ref_no") %>' CommandName="cmd_ref_no" Font-Underline="False"></asp:LinkButton> 
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="employee_name" HeaderText="Employee Name"></asp:BoundField>
                    <asp:BoundField DataField="employee_id" HeaderText="Personnel No."></asp:BoundField>
                    <asp:BoundField DataField="travel_date" HeaderText="Travel Date"></asp:BoundField>
                    <asp:BoundField DataField="date_created" HeaderText="Date Filed"></asp:BoundField>
                    
                    <asp:TemplateField HeaderText="Status">
                    <EditItemTemplate>
                    <asp:TextBox runat="server" Text='<%# Bind("status") %>' id="TextBox2"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                    <asp:LinkButton id="lnk_approver" runat="server" ForeColor="Red" Text='<%# Bind("status") %>' CommandName="cmd_approvers" Font-Underline="False">
                    </asp:LinkButton> 
                    
                    
                    
                    <cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" CancelControlID="btn_close" TargetControlID="lnk_approver" BackgroundCssClass="modalBackground" PopupControlID="Panel2">
                    </cc1:ModalPopupExtender> <asp:Panel style="DISPLAY: none; BACKGROUND-REPEAT: repeat-x; TEXT-ALIGN: center" id="Panel2" runat="server" Width="559px" BorderColor="Black" BackColor="White" Height="353px">
                    <BR /><BR /><CENTER><TABLE style="WIDTH: 500px; HEIGHT: 302px; TEXT-ALIGN: left"><TBODY><TR style="COLOR: #000000"><TD style="FONT-WEIGHT: bold; COLOR: gray; BORDER-BOTTOM: gray 1px solid" colSpan=3>Approvers</TD></TR><TR style="COLOR: #000000"><TD style="VERTICAL-ALIGN: top; COLOR: white; BORDER-BOTTOM: gray 1px solid; HEIGHT: 173px" colSpan=3>

                    <asp:Panel id="Panel3" runat="server" Width="100%" ScrollBars="Vertical" Height="230px">
                    <asp:GridView id="gv_approvers" runat="server" Width="96%" BorderWidth="1px" BorderColor="#CC9966" BackColor="White" CellPadding="4" BorderStyle="None" AutoGenerateColumns="False">
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
                    </TD></TR><TR style="COLOR: #000000"><TD style="HEIGHT: 31px; TEXT-ALIGN: center" colSpan=3>
                    <asp:Button id="btn_close" runat="server" ForeColor="White" Font-Size="8pt" Width="87px" Text="Close" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red" Font-Names="arial" ValidationGroup="Val"></asp:Button> </TD></TR></TBODY></TABLE></CENTER>
                    </asp:Panel> 
                    
                    </ItemTemplate>
                    </asp:TemplateField>        
            </columns>
            <footerstyle backcolor="#CCCCCC" forecolor="Black" />
            <pagerstyle backcolor="#999999" forecolor="Black" horizontalalign="Center" />
            <selectedrowstyle backcolor="#008A8C" font-bold="True" forecolor="White" />
            <headerstyle backcolor="#000084" font-bold="True" forecolor="White" />
            
            
            </asp:GridView>
            </div>    
			        
        

        <%--</asp:Panel>--%>









</asp:Content>

