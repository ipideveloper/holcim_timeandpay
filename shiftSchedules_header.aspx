<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="shiftSchedules_header.aspx.vb" Inherits="shiftSchedules_header" title="Holcim - Shift Schedules" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button id="btnAdd" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
        Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Apply" Width="87px" OnClick="btnAdd_Click" />
    <asp:Panel id="Panel1" runat="server" Height="309px" ScrollBars="Vertical" Width="100%">
        <asp:GridView id="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%">
            <rowstyle forecolor="DimGray" wrap="True" />
            <columns>
<asp:TemplateField HeaderText="Ref. No"><EditItemTemplate>
<asp:TextBox runat="server" Text='<%# Bind("ref_no") %>' id="TextBox1"></asp:TextBox>
</EditItemTemplate>
<ItemTemplate>
<asp:LinkButton id="lnk_ref_no" runat="server" Text='<%# Bind("ref_no") %>' __designer:wfdid="w17" CommandName="cmd_ref_no"></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="employee_name" HeaderText="Employee Name"></asp:BoundField>
<asp:BoundField DataField="work_date" HeaderText="Work Date"></asp:BoundField>
<asp:BoundField DataField="shift_code" HeaderText="Shift Code"></asp:BoundField>
<asp:BoundField DataField="date_created" HeaderText="Date Filed"></asp:BoundField>
<asp:BoundField DataField="status" HeaderText="Status"></asp:BoundField>
</columns>
            <footerstyle backcolor="#FFFFCC" forecolor="#330099" />
            <pagerstyle backcolor="#FFFFCC" forecolor="#330099" horizontalalign="Center" />
            <selectedrowstyle backcolor="#FFCC66" font-bold="True" forecolor="#663399" />
            <headerstyle backcolor="#990000" font-bold="True" forecolor="#FFFFCC" />
            <alternatingrowstyle backcolor="#E0E0E0" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>

