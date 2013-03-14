<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="shift_apply.aspx.vb" Inherits="shift_apply" title="Holcim - Shift Schedules" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DropDownList id="dpl_month" runat="server" Font-Size="8pt" Width="147px" CssClass="Buttonhand1hover">
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
    <asp:Button id="btn_bind" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
        Font-Names="arial" Font-Size="8pt" ForeColor="White" onclick="btn_bind_Click"
        Text="Display" Width="87px" CssClass="Buttonhand1hover" />
</asp:Content>

