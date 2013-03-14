<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="leave_balances.aspx.vb" Inherits="leave_balances" Title="Holcim - My Leave Balances" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:GridView ID="gv_leave_balances" runat="server" AutoGenerateColumns="False" BackColor="White"
        BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%">
        <RowStyle ForeColor="DimGray" Wrap="True" />
        <Columns>
            <asp:BoundField DataField="description" HeaderText="Leave Type">
                <HeaderStyle Font-Size="11px" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="leave_credit" HeaderText="Beginning Balance" DataFormatString="{0:N}"
                HtmlEncode="False">
                <HeaderStyle Font-Size="11px" Width="130px" HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="leave_taken" HeaderText="Total Leaves Applied" DataFormatString="{0:N2}"
                HtmlEncode="False">
                <HeaderStyle Font-Size="11px" Width="130px" HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Pending Leaves" HeaderText="Pending Leaves" DataFormatString="{0:N2}"
                HtmlEncode="False">
                <HeaderStyle Font-Size="11px" Width="130px" HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="leave_avail" HeaderText="Available" DataFormatString="{0:N2}"
                HtmlEncode="False">
                <HeaderStyle Font-Size="11px" Width="130px" HorizontalAlign="Right" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
        <AlternatingRowStyle BackColor="#E0E0E0" />
    </asp:GridView>
    
</asp:Content>
