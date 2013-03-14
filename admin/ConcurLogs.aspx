<%@ Page Language="VB" EnableViewState="false" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false" CodeFile="ConcurLogs.aspx.vb" Inherits="admin_ConcurLogs" title="Concur Logs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
<style type="text/css">
    .loading
    {
        position: absolute;
        padding: 2px;
        opacity: 0.90;
        width: 860px;
        height: 300px;
        text-align: center;
        z-index: 100000;
    }
    .rgArrPart1
    {
        margin: 0px !important;
    }
    #filterdiv .rfButton
    {
       
        background-image: none !important;
        border: 1px solid #E1E1E1;
        min-height:26px !important;
        padding-bottom: 0px !important;
        background: none !important;
         
    }
    
    #RadFilter {
        float:left !important;
    }
    
    #filterdiv .rfApply a 
    {
        background-image: none !important;
    }
    
    #filterdiv .rfApply
    {
        float:left !important;
        margin-top: 0px;
        
    }
    
    #filterdiv
    {
        margin-bottom: 20px;
    }
    
    th .rgHeader input[type=submit], input[type=checkbox], input[type=radio], input[type=button], butto{
        background-image: none !important;
        border: 1px solid #E1E1E1;
        min-height:26px !important;
        padding-bottom: 0px !important;
        
    }
    td .rgCollapse input[type=submit], input[type=checkbox], input[type=radio], input[type=button], button{
        background-image: none !important;
        border: 1px solid #E1E1E1;
        min-height:26px !important;
        padding-bottom: 0px !important;
      
    }
    
    th .rgHeader a{
        background-image: none !important;
        margin:0px;
        padding:0px;
    }
    
    td GroupHeader_transparent input{
        margin:0px;
        padding:0px;
        min-height:26px !important;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div class="g12 nodrop">
        <div style="background-color: #FFFFFF;">
            <div style="float: left; margin-left: 10px;">
                <span style="font-size: 24px;">System Logs</span></div>
        </div>
        <div style="clear: both">
        </div>
        <fieldset>
            <div>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="loading"><img src="../images/ajax-loader.gif" /><br /><strong>Loading</strong><br /><br /></div>
                </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <div id="filterdiv">
                <div style="display:block; margin-bottom:5px;">Filter by:</div> 
                <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="RadGrid1" ShowApplyButton="true" ApplyButtonText="Apply Filter" />
                </div>
                <div style="clear:both;">
                </div>
                <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" DataSourceID="SystemLogsSqlDataSource"
                    GridLines="None" AllowPaging="true" PageSize="10" Skin="Transparent" AllowFilteringByColumn="true" AllowSorting="true" >
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                        <WebServiceSettings>
                            <ODataSettings InitialContainerName="">
                            </ODataSettings>
                        </WebServiceSettings>
                    </HeaderContextMenu>
                    <MasterTableView AutoGenerateColumns="False" DataSourceID="SystemLogsSqlDataSource"
                    IsFilterItemExpanded="false">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <CommandItemTemplate>
                            <telerik:RadToolBar runat="server" ID="RadToolBar1" OnButtonClick="RadToolBar1_ButtonClick">
                                <Items>
                                    <telerik:RadToolBarButton Runat="server" Text="Apply filter" CommandName="FilterRadGrid" ImageUrl="<%#GetFilterIcon() %>" ImagePosition="Right" />
                                </Items>
                            </telerik:RadToolBar>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridBoundColumn DataField="LogDate" DataType="System.DateTime" FilterControlAltText="Filter LogDate column"
                                HeaderText="Log Date" SortExpression="LogDate" UniqueName="LogDate">
                                <HeaderStyle Width="140px" HorizontalAlign="Center" />
                           
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Module"  FilterControlAltText="Filter Module column"
                                HeaderText="Module" SortExpression="Module" UniqueName="Module">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SystemEvent"  FilterControlAltText="Filter SystemEvent column"
                                HeaderText="Event" SortExpression="SystemEvent" UniqueName="SystemEvent">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="LogType"  FilterControlAltText="Filter LogType column"
                                HeaderText="Type" ReadOnly="True" SortExpression="LogType" UniqueName="LogType">
                                <ItemStyle Wrap="false" />
                            
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="LogMessage" FilterControlAltText="Filter LogMessage column"
                                HeaderText="Message" SortExpression="LogMessage" UniqueName="LogMessage">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Username" FilterControlAltText="Filter Username column"
                                HeaderText="User" SortExpression="Username" UniqueName="Username">
                            
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                        <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" FirstPageImageUrl="../images/first.png" NextPageImageUrl="../images/next.png" PrevPageImageUrl="../images/prev.png" LastPageImageUrl="../images/last.png"    AlwaysVisible="True" />
                    </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                        <WebServiceSettings>
                            <ODataSettings InitialContainerName="">
                            </ODataSettings>
                        </WebServiceSettings>
                    </FilterMenu>
                </telerik:RadGrid>
                </ContentTemplate>
                </asp:UpdatePanel>
                <%--<asp:GridView ID="GridView_SystemLogs" runat="server" 
                    AutoGenerateColumns="False" DataSourceID="SystemLogsSqlDataSource" 
                    PageSize="5">
                    <PagerSettings Mode="NextPreviousFirstLast" Position="TopAndBottom" />
                    <Columns>
                        <asp:BoundField DataField="LogDate" HeaderText="Log Date" 
                            SortExpression="LogDate">
                            <HeaderStyle Width="140px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Module" HeaderText="Module" 
                            SortExpression="Module" />
                        <asp:BoundField DataField="SystemEvent" HeaderText="Event" 
                            SortExpression="SystemEvent" />
                        <asp:BoundField DataField="LogType" HeaderText="Type" 
                            SortExpression="LogType" />
                        <asp:BoundField DataField="LogMessage" HeaderText="Message" 
                            SortExpression="LogMessage" />
                        <asp:BoundField DataField="Username" HeaderText="User" 
                            SortExpression="Username" />
                    </Columns>
                    <AlternatingRowStyle BackColor="#F2F2F2" />
                </asp:GridView>--%>
            </div>
        </fieldset>
    </div>
    <asp:SqlDataSource ID="SystemLogsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
        SelectCommand="spWeb_Get_SysManager_Logs" SelectCommandType="StoredProcedure" FilterExpression="Module LIKE '%{0}%'">
        <FilterParameters>
        <asp:Parameter Name="module" DefaultValue="Concur" Type="String" />
        </FilterParameters>
    </asp:SqlDataSource>
</asp:Content>

