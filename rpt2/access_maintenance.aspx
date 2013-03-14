<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="access_maintenance.aspx.vb" Inherits="access_maintenance" title="Holcim - Users Access Maintenance" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <asp:UpdatePanel id="UpdatePanel1" runat="server">
        <contenttemplate>
<table style="width: 100%" cellspacing=0 cellpadding=0><tbody><tr><td style="HEIGHT: 19px"><asp:Button id="lnk_search" runat="server" ForeColor="White" Font-Size="8pt" Width="69px" Text="Search" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red" Font-Names="arial" CssClass="Buttonhand1hover"></asp:Button> <asp:Label id="lblrecords" runat="server"></asp:Label></TD><TD style="HEIGHT: 19px; TEXT-ALIGN: right"><asp:Button id="btnSaveAll" onclick="btnSaveAll_Click" runat="server" ForeColor="White" Font-Size="8pt" Width="69px" Text="Save All" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red" Visible="False" Font-Names="arial" CssClass="Buttonhand1hover" ValidationGroup="Val"></asp:Button></td></tr></tbody></table>
<asp:GridView id="GridView1" runat="server" Width="100%" BorderWidth="1px" BorderColor="#CC9966" BackColor="White" CellPadding="4" BorderStyle="None" AutoGenerateColumns="False">
<RowStyle Wrap="True" ForeColor="DimGray"></RowStyle>
<Columns>
<asp:BoundField DataField="employee_id" HeaderText="Personnel No."></asp:BoundField>
<asp:BoundField DataField="employee_name" HeaderText="Name"></asp:BoundField>
<asp:TemplateField HeaderText="OT"><EditItemTemplate>
<asp:TextBox id="TextBox2" runat="server" Text='<%# Bind("ot") %>' __designer:wfdid="w9"></asp:TextBox> 
</EditItemTemplate>
<HeaderTemplate>
&nbsp;OT<br /><asp:CheckBox id="chkHOT" runat="server" Enabled="False" __designer:wfdid="w55" AutoPostBack="True" OnCheckedChanged="chkHOT_CheckedChanged"></asp:CheckBox> 
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox id="chkOT" runat="server" CssClass="Buttonhand1hover" Enabled="False" Checked='<%# Bind("ot") %>' __designer:wfdid="w54"></asp:CheckBox> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="OB"><EditItemTemplate>
<asp:TextBox id="TextBox3" runat="server" Text='<%# Bind("ob") %>' __designer:wfdid="w13"></asp:TextBox> 
</EditItemTemplate>
<HeaderTemplate>
&nbsp;OB<br /><asp:CheckBox id="chk_h_ob" runat="server" Enabled="False" __designer:wfdid="w32" AutoPostBack="True" OnCheckedChanged="chk_h_ob_CheckedChanged"></asp:CheckBox> 
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox id="chkOB" runat="server" CssClass="Buttonhand1hover" Enabled="False" __designer:wfdid="w12" Checked='<%# Bind("ob") %>'></asp:CheckBox> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Sys Admin"><EditItemTemplate>
<asp:TextBox id="TextBox9" runat="server" Text='<%# Bind("sa") %>' __designer:wfdid="w16"></asp:TextBox> 
</EditItemTemplate>
<HeaderTemplate>
Sys Admin<BR /><asp:CheckBox id="chk_h_sysad" runat="server" Enabled="False" __designer:wfdid="w52" AutoPostBack="True" OnCheckedChanged="chk_h_sysad_CheckedChanged"></asp:CheckBox> 
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox id="chkSA" runat="server" CssClass="Buttonhand1hover" Enabled="False" __designer:wfdid="w15" Checked='<%# Bind("sa") %>'></asp:CheckBox> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField><EditItemTemplate>
<asp:TextBox id="TextBox1" runat="server" __designer:wfdid="w50"></asp:TextBox> 
</EditItemTemplate>
<HeaderTemplate>
<asp:LinkButton id="lnk_edit_all" runat="server" ForeColor="White" Width="50px" __designer:wfdid="w53" CommandName="cmdEditAll">Edit</asp:LinkButton>
</HeaderTemplate>
<ItemTemplate>
<asp:LinkButton id="LinkButton1" runat="server" ForeColor="Red" __designer:wfdid="w49" CommandName="cmdEdit" Font-Underline="False">EDIT</asp:LinkButton> 
</ItemTemplate>
</asp:TemplateField>
    <asp:ButtonField CommandName="cmdReset" HeaderText="Reset Password" ShowHeader="True" Text="RESET" ControlStyle-ForeColor="Red" ControlStyle-Font-Underline="false"/>
</Columns>

<FooterStyle BackColor="#FFFFCC" ForeColor="#330099"></FooterStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#FFFFCC" ForeColor="#330099"></PagerStyle>

<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399"></SelectedRowStyle>

<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC"></HeaderStyle>

<AlternatingRowStyle BackColor="#E0E0E0"></AlternatingRowStyle>
</asp:GridView> <asp:Panel style="DISPLAY: none; BACKGROUND-REPEAT: repeat-x; TEXT-ALIGN: center" id="Panel2" runat="server" Width="459px" BorderColor="Black" BackColor="White" Height="275px"><BR /><BR /><CENTER><TABLE style="WIDTH: 416px; TEXT-ALIGN: left"><TBODY><TR style="COLOR: #000000">
                <TD style="FONT-WEIGHT: bold; COLOR: gray; BORDER-BOTTOM: gray 1px solid; HEIGHT: 16px" 
                    colSpan=2>Search Employee</TD></TR><TR style="COLOR: #000000"><TD style="WIDTH: 131px; HEIGHT: 16px"></TD>
                    <TD style="HEIGHT: 16px"></TD></TR><TR style="COLOR: #000000"><TD style="WIDTH: 131px; HEIGHT: 16px"><STRONG>Plant :</STRONG></TD>
                <TD style="HEIGHT: 16px"><asp:DropDownList id="dpl_plant" runat="server" Font-Size="8pt" Width="160px" CssClass="Buttonhand1hover" Enabled="False"></asp:DropDownList> </TD></TR><TR style="COLOR: #000000"><TD style="WIDTH: 131px; HEIGHT: 16px"><STRONG>Org Unit :</STRONG></TD>
                <TD style="HEIGHT: 16px"><asp:DropDownList id="dpl_organization" runat="server" Font-Size="8pt" Width="268px" CssClass="Buttonhand1hover"></asp:DropDownList></TD></TR><TR style="COLOR: #000000"><TD style="WIDTH: 131px"><STRONG>Employee ID :</STRONG></TD>
                <TD><asp:TextBox id="txt_emp_id" tabIndex=2 runat="server" Font-Size="8pt" Width="153px"></asp:TextBox></TD></TR><TR style="COLOR: #000000"><TD style="WIDTH: 131px; HEIGHT: 16px"><STRONG>Employee Last Name :</STRONG></TD>
                <TD style="HEIGHT: 16px"><asp:TextBox id="txt_emp_lastname" tabIndex=2 runat="server" Font-Size="8pt" Width="153px"></asp:TextBox></TD></TR><TR style="FONT-WEIGHT: bold; COLOR: #000000"><TD style="WIDTH: 131px; HEIGHT: 16px">Employee First Name&nbsp; :</TD>
                <TD style="HEIGHT: 16px"><asp:TextBox id="txt_emp_firstname" tabIndex=2 runat="server" Font-Size="8pt" Width="153px"></asp:TextBox> &nbsp; </TD></TR><TR style="COLOR: #000000">
                <TD style="COLOR: white; BORDER-BOTTOM: gray 1px solid; HEIGHT: 16px" colSpan=2>.</TD></TR><TR style="COLOR: #000000">
                <TD style="HEIGHT: 31px; TEXT-ALIGN: center" colSpan=2><asp:Button id="btn_search" onclick="btn_search_Click" runat="server" ForeColor="White" Font-Size="8pt" Width="87px" Text="Search" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red" Font-Names="arial" CssClass="Buttonhand1hover" ValidationGroup="Val"></asp:Button> <asp:Button id="btnCancel" runat="server" ForeColor="White" Font-Size="8pt" Width="87px" Text="Cancel" BorderWidth="1px" BorderColor="DarkGray" BackColor="Red" Font-Names="arial" CssClass="Buttonhand1hover"></asp:Button></TD></TR></TBODY></TABLE></CENTER></asp:Panel> <cc1:ModalPopupExtender id="ModalPopupExtender1" runat="server" TargetControlID="lnk_search" PopupControlID="Panel2" CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender> 
</contenttemplate>
    </asp:UpdatePanel>
</asp:Content>

