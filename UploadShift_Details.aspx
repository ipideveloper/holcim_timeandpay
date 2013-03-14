<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="UploadShift_Details.aspx.vb" Inherits="UploadShift_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 100%">
        <tr>
            <td style="width: 295px">
                <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Size="12pt" 
                    Text="Label"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 295px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:GridView runat="server" CellPadding="4" GridLines="Horizontal" ForeColor="#333333" Width="798px" ID="GridView8">
        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White">
        </FooterStyle>
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White">
        </HeaderStyle>
        <PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333">
        </PagerStyle>
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy">
        </SelectedRowStyle>
    </asp:GridView>
</asp:Content>

