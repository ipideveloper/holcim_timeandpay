<%@ Page Title="" Language="VB"  AutoEventWireup="false" CodeFile="UploadShift_My_Details.aspx.vb" Inherits="UploadShift_My_Details" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<body>
<form runat="server">
<div>


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


</div>
</form>
</body>
</html>