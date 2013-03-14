<%@ Page Language="VB" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false"
    CodeFile="Concur.aspx.vb" Inherits="admin_Concur" Title="Concur / Travel Order Settings" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
        .rootNode
        {
            font-size: 13px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function MM_preloadImages() { //v3.0
            var d = document; if (d.images) {
                if (!d.MM_p) d.MM_p = new Array();
                var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                    if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; } 
            }
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Loader" runat="Server">

<div id="mybox">
        <div class="boxMid">Loading Organization Hierarchy... Please wait...  <img src="css/ajax-loader.gif" alt="loader"  /></div>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <center>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="text-align: left; width: 100%; margin-top: 0px;">
                    <div style="background-color: #FFFFFF; height: 60px;">
                        <div style="float: left; width: 150px;">
                            <img alt="Concur Logo" align="left" src="../images/Concur_logo.png" /></div>
                        <div style="float: left; margin-top: 20px;">
                            <span style="font-size: 24px;">Travel Order Settings</span></div>
                    </div>
                    <div style="clear: both">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </div>
                    <div style="padding: 20px; font-family: Arial,verdana,Sans Serif; font-size: 11px;
                        text-align: left; background-color: #F0F0F0; padding-top: 10px; padding-bottom: 7px;">
                        Here's where you disable Travel Order module in Time and Pay to specific departments
                        who will use Concur web services. Click on the checkbox of the department and click
                        "save setings" button.
                    </div>
                    <div style="padding: 20px; text-align: left; background-color: #63676A; padding-top: 10px;
                        padding-bottom: 7px;">
                        <asp:Button ID="Button1" runat="server" Text="Save Settings" />&nbsp;&nbsp;<asp:Button
                            ID="Button2" runat="server" Text="Refresh" />
                    </div>
                    <asp:Panel ID="SuccessMessage" Visible="false" runat="server" CssClass="alert success">Settings Saved!</asp:Panel>
                    <div style="padding: 20px; background-color: #DADADA;">
                        <div style="float: right; font-family: Arial,verdana,Sans Serif; font-size: 12px;">
                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Gray">Expand All</asp:LinkButton>
                        </div>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img src="css/light/images/loading.gif" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        
                        
                        <telerik:RadTreeView ID="RadTreeView1" runat="server" DataSourceID="SqlDataSource1"
                            DataFieldID="ID" DataFieldParentID="ParentID" OnNodeBound="RadTreeView1_NodeDataBound"
                            CheckBoxes="True" DataTextField="Text" DataValueField="ID" TriStateCheckBoxes="False"
                            CollapseAnimation-Duration="0" Skin="Vista" ShowLineImages="false" PersistLoadOnDemandNodes="false"
                            ExpandAnimation-Duration="0" AppendDataBoundItems="True" CheckChildNodes="True">
                            <CollapseAnimation Duration="1"></CollapseAnimation>
                            <ExpandAnimation Duration="1"></ExpandAnimation>
                            <DataBindings>
                                <telerik:RadTreeNodeBinding Expanded="True" Depth="0" ExpandMode="ClientSide" />
                            </DataBindings>
                        </telerik:RadTreeView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
        SelectCommand="SELECT  a.organ_id AS ID,a.organ_name AS Text,b.organ_id AS ParentID,ISNULL(c.concur_tag, 0) AS concur_tag FROM sys_organization a LEFT JOIN sys_organization b ON a.parent_id = b.organ_id LEFT JOIN dbo.z_ob_concur c ON a.organ_id=c.organ_id ORDER BY 2">
    </asp:SqlDataSource>

    <script type="text/javascript">
        $(window).ready(function() {
            $('#mybox').fadeOut(1000);
        });

    </script>

</asp:Content>
