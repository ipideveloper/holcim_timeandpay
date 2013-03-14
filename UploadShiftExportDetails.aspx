<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UploadShiftExportDetails.aspx.vb" Inherits="UploadShiftExportDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div id="div-datagrid3">
    <asp:GridView ID="GridView5" runat="server" CellPadding="4" ForeColor="#333333" 
                            Width="100%">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Wrap="True" />
                            
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#999999" Wrap="False" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
    </div>
    </form>
</body>
</html>
