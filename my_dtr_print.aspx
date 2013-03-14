<%@ Page Language="VB" AutoEventWireup="false" CodeFile="my_dtr_print.aspx.vb" Inherits="my_dtr_print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>my DTR</title>
    <link href="StyleSheet.css" type="text/css" rel="stylesheet" media="all" />
    <style type="text/css" media="screen">
        BODY
        {
            margin: 0px;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=70);
            opacity: 0.7px;
        }
        .popupControl
        {
            background-color: Red;
            position: absolute;
            visibility: hidden;
        }
        .Buttonhand1hover
        {
            cursor: hand;
        }
        .printbutton
        {
            visibility: visible;
        }
        }</style>
    <style type="text/css" media="print">
        .printbutton
        {
            visibility: hidden;
        }
        }
        table td
        {
            font-family: Arial, Sans-Serif;
            font-size: 11px;
        }
        }</style>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 11px;
        }
        th
        {
            font-family: Arial;
            font-size: 11px;
        }
    </style>
</head>
<body onload="javascript:self.print();">
    <form id="form1" runat="server">
    <div>
        <center>
            <asp:Button ID="btn_print" runat="server" ForeColor="White" Font-Size="8pt" Width="87px"
                Text="Print" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red" CssClass="Buttonhand1hover printbutton"
                Font-Names="arial"></asp:Button>
            &nbsp;<asp:Button ID="btn_close" runat="server" ForeColor="White" Font-Size="8pt"
                Width="87px" Text="Close Window" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red"
                CssClass="Buttonhand1hover printbutton" Font-Names="arial"></asp:Button></center>
        <table style="border-top: gray 1px solid; border-left-width: 1px; border-left-color: black;
            border-bottom-width: 1px; border-bottom-color: black; color: #696969; background-repeat: repeat-x;
            height: 32px; background-color: transparent; text-align: left; border-right-width: 1px;
            border-right-color: black" width="100%">
            <tr>
                <td style="border-top-width: 1px; border-left-width: 1px; border-left-color: #A9A9A9;
                    border-bottom-width: 1px; border-bottom-color: #A9A9A9; border-top-color: #A9A9A9;
                    border-right-width: 1px; border-right-color: #A9A9A9">
                    Employee Name:&nbsp;<asp:Label ID="LastNameLabel" runat="server" Font-Bold="True"></asp:Label>,&nbsp;<asp:Label
                        ID="FirstNameLabel" runat="server" Font-Bold="True"></asp:Label>&nbsp;<asp:Label
                            ID="MiddleNameLabel" runat="server" Font-Bold="True"></asp:Label><br />
                    Personnel No.:&nbsp;<asp:Label ID="PeronnelNoLabel" runat="server" Font-Bold="True"></asp:Label><br />
                    Sector:&nbsp;<asp:Label ID="SectorLabel" runat="server" Font-Bold="True"></asp:Label>
                </td>
            </tr>
        </table>
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
    </form>
</body>
</html>
