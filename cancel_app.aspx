<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="cancel_app.aspx.vb" Inherits="cancel_app" Title="Cancel Application" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <style type="text/css">
  th {font-size:11px;}
  </style>
   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table style="width: 709px; height: 44px;">
                <tbody>
                    <tr>
                        <td style="width: 76px">
                            Enter Ref. No.
                        </td>
                        <td style="width: 136px">
                            <asp:TextBox ID="TextBox1" runat="server" Font-Size="8pt"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btn_view" OnClick="btnview_Click" runat="server" ForeColor="White"
                                Font-Size="8pt" Width="205px" Text="View Details" BorderWidth="1px" BorderColor="DarkGray"
                                BackColor="Red" Font-Names="arial" CssClass="Buttonhand1hover" 
                                style="height: 20px"></asp:Button>
                        </td>
                        <td>
                            <asp:Button ID="btn_cancel" OnClick="btnAdd_Click" runat="server" ForeColor="White"
                                Font-Size="8pt" Width="205px" Text="Cancel Application" BorderWidth="1px" BorderColor="DarkGray"
                                BackColor="Aqua" Font-Names="arial" CssClass="Buttonhand1hover" Enabled="False">
                            </asp:Button>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table style="width: 709px; height: 44px;">
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="15pt" Text="Details"
                            Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" Font-Names="Arial" BorderColor="#CC9966"
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="97%" AllowSorting="False">
                            <RowStyle Wrap="False" />
                            <EditRowStyle Wrap="False" />
                        </asp:GridView>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="True"
                ConfirmText="Cancel this application?" TargetControlID="btn_cancel">
            </cc1:ConfirmButtonExtender>
       <%-- </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_cancel"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
