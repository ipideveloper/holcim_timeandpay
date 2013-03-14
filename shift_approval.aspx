<%@ Page Language="VB" AutoEventWireup="false" CodeFile="shift_approval.aspx.vb" Inherits="shift_approval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
<script language="javascript">
function closewindow()
{
	window.close()
}
</script>

</head>
<body style="font-size: 8pt; font-family: arial">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <asp:Label ID="lbl_header" runat="server"></asp:Label>
                    <asp:Label ID="lbl_month" runat="server" Visible="False"></asp:Label></td>
                <td style="text-align: right">
    
    <input type= "button" onclick="closewindow();" value="Close Window" style="font-size: 10pt; color: white; font-family: arial; background-color: red" size="15"></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%">
            <RowStyle ForeColor="DimGray" Wrap="True" />
            <Columns>
                <asp:BoundField DataField="employee_name" HeaderText="Employee Name">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="employee_id" HeaderText="Personel No.">
                    <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="1" HeaderText="1" />
                <asp:BoundField DataField="2" HeaderText="2" />
                <asp:BoundField DataField="3" HeaderText="3" />
                <asp:BoundField DataField="4" HeaderText="4" />
                <asp:BoundField DataField="5" HeaderText="5" />
                <asp:BoundField DataField="6" HeaderText="6" />
                <asp:BoundField DataField="7" HeaderText="7" />
                <asp:BoundField DataField="8" HeaderText="8" />
                <asp:BoundField DataField="9" HeaderText="9" />
                <asp:BoundField DataField="10" HeaderText="10" />
                <asp:BoundField DataField="11" HeaderText="11" />
                <asp:BoundField DataField="12" HeaderText="12" />
                <asp:BoundField DataField="13" HeaderText="13" />
                <asp:BoundField DataField="14" HeaderText="14" />
                <asp:BoundField DataField="15" HeaderText="15" />
                <asp:BoundField DataField="16" HeaderText="16" />
                <asp:BoundField DataField="17" HeaderText="17" />
                <asp:BoundField DataField="18" HeaderText="18" />
                <asp:BoundField DataField="19" HeaderText="19" />
                <asp:BoundField DataField="20" HeaderText="20" />
                <asp:BoundField DataField="21" HeaderText="21" />
                <asp:BoundField DataField="22" HeaderText="22" />
                <asp:BoundField DataField="23" HeaderText="23" />
                <asp:BoundField DataField="24" HeaderText="24" />
                <asp:BoundField DataField="25" HeaderText="25" />
                <asp:BoundField DataField="26" HeaderText="26" />
                <asp:BoundField DataField="27" HeaderText="27" />
                <asp:BoundField DataField="28" HeaderText="28" />
                <asp:BoundField DataField="29" HeaderText="29" />
                <asp:BoundField DataField="30" HeaderText="30" />
                <asp:BoundField DataField="31" HeaderText="31" />
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <AlternatingRowStyle BackColor="#E0E0E0" />
        </asp:GridView>
                <asp:Timer ID="Timer1" runat="server" Interval="50" OnTick="Timer1_Tick">
                </asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            &nbsp;</center>
        <center>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <img src="images/updateprogress1.gif" />&nbsp; Transaction still on process, please
                wait.
            </ProgressTemplate>
        </asp:UpdateProgress>
            &nbsp;</center>
    
    </div>
    </form>
</body>
</html>

