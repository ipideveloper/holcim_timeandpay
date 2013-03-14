<%@ Page Language="VB" EnableViewState="false" MasterPageFile="~/admin/AdminMasterPage.master" AutoEventWireup="false"
    CodeFile="TravelOrderData.aspx.vb" Inherits="admin_TravelOrderData" Title="Travel Order Data" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
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
            border: 1px solid #666 !important;
            min-height: 26px !important;
            padding-bottom: 0px !important;
            background: none !important;
            min-width: 100px !important;
            width: 120px !important; 
            text-align:center !important;
            color: #666;
        }
        #RadFilter
        {
            float: left !important;
        }
        #filterdiv .rfApply a
        {
            background-image: none !important;
        }
        #filterdiv .rfApply
        {
            float: left !important;
            margin-top: 0px;
        }
        #filterdiv
        {
            margin-bottom: 20px;
        }
        th .rgHeader input
        {
            background-image: none !important;
            border: 1px solid #E1E1E1;
            min-height: 26px !important;
            padding-bottom: 0px !important;
            background: none !important;
        }
        th .rgHeader a
        {
            background-image: none !important;
            margin: 0px;
            padding: 0px;
        }
        td .rgGroupCol span
        {
            text-align: left !important;
        }
        #ctl00_MainContentPlaceHolder_RadGrid1_ctl00 td
        {
            text-align: left !important;
        }
        #ctl00_MainContentPlaceHolder_RadGrid1_ctl00 input.rgCollapse
        {
            margin: 0px !important;
            padding: 0px !important;
            border: 0px !important;
            width: 20px !important;
            height: 20px !important;
            display: table !important;
            min-width: 20px;
            min-height: 20px;
        }
        input .rgCollapse
        {
            margin: 0px !important;
            padding: 0px !important;
            border: 0px !important;
            width: 20px !important;
            height: 20px !important;
            display: table !important;
            min-width: 20px !important;
            min-height: 20px !important;
        }
        th .rgHeader a
        {
            background-image: none !important;
            margin: 0px;
            padding: 0px;
        }
        input 
        {
            margin: 0px !important;
            padding: 0px !important;
            border: 0px !important;
            
            display: table !important;
            min-width: 20px !important;
            min-height: 20px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div class="g12 nodrop">
        <div style="background-color: #FFFFFF; height: 60px;">
            <div style="float: left; width: 150px;">
                <img alt="Concur Logo" align="left" src="../images/Concur_logo.png" /></div>
            <div style="float: left; margin-top: 20px;">
                <span style="font-size: 24px;">Travel Order Data</span></div>
        </div>
        <div style="clear: both">
        </div>
        <fieldset>
            <div>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="loading">
                            <img src="../images/ajax-loader.gif" /><br />
                            <strong>Loading</strong><br />
                            <br />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div id="filterdiv">
                            <div style="display: block; margin-bottom: 5px;">
                                Filter by:</div>
                            <telerik:RadFilter runat="server" ID="RadFilter1" FilterContainerID="RadGrid1" ShowApplyButton="true"
                                ApplyButtonText="Apply Filter" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" DataSourceID="SystemLogsSqlDataSource"
                            GridLines="None" HorizontalAlign="Left" AllowPaging="true" PageSize="50" Skin="Transparent"
                            AllowFilteringByColumn="true">
                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                                <WebServiceSettings>
                                    <ODataSettings InitialContainerName="">
                                    </ODataSettings>
                                </WebServiceSettings>
                            </HeaderContextMenu>
                            <MasterTableView AutoGenerateColumns="False" DataSourceID="SystemLogsSqlDataSource"
                                IsFilterItemExpanded="false" GroupLoadMode="Client" ExpandCollapseColumn-Display="false"
                                HierarchyDefaultExpanded="false" GroupHeaderItemStyle-BackColor="#787B7E" GroupHeaderItemStyle-HorizontalAlign="Left"
                                GroupHeaderItemStyle-ForeColor="#FFFFFF">
                                <GroupHeaderTemplate>
                                    Upload Date:&nbsp;&nbsp;<%#Eval("UploadDate")%>
                                </GroupHeaderTemplate>
                               
                            
                                <GroupByExpressions>
                                    <telerik:GridGroupByExpression>
                                        <SelectFields>
                                            <telerik:GridGroupByField FieldAlias="UploadDate" FieldName="UploadDate"></telerik:GridGroupByField>
                                        </SelectFields>
                                        <GroupByFields>
                                            <telerik:GridGroupByField FieldName="UploadDate" SortOrder="Descending"></telerik:GridGroupByField>
                                        </GroupByFields>
                                    </telerik:GridGroupByExpression>
                                </GroupByExpressions>
                                <GroupHeaderItemStyle HorizontalAlign="Left" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="RefNo" FilterControlAltText="Filter RefNo column"
                                        HeaderText="Ref. No." SortExpression="RefNo" UniqueName="RefNo">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EmployeeID" FilterControlAltText="Filter EmployeeID column"
                                        HeaderText="Personnel No." ReadOnly="True" SortExpression="EmployeeID" UniqueName="EmployeeID">
                                        <ItemStyle Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Employee" FilterControlAltText="Filter Employee column"
                                        HeaderText="Employee" SortExpression="Employee" UniqueName="Employee">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Purpose" FilterControlAltText="Filter Purpose column"
                                        HeaderText="Purpose" SortExpression="Purpose" UniqueName="Purpose">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DateFrom" DataType="System.DateTime" FilterControlAltText="Filter DateFrom column"
                                        HeaderText="Date From" SortExpression="DateFrom" UniqueName="DateFrom">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DateTo" DataType="System.DateTime" FilterControlAltText="Filter DateTo column"
                                        HeaderText="Date To" SortExpression="DateTo" UniqueName="DateTo">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="UploadDate" DataType="System.DateTime" FilterControlAltText="Filter UploadDate column"
                                        HeaderText="Upload Date" SortExpression="UploadDate" UniqueName="UploadDate">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Status" FilterControlAltText="Filter Status column"
                                        HeaderText="Status" SortExpression="Status" UniqueName="Status">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Wrap="false" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ApprovedDate" FilterControlAltText="Filter ApprovedDate column"
                                        HeaderText="Approved Date" SortExpression="ApprovedDate" UniqueName="ApprovedDate">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Wrap="false" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    
                                    
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <PagerStyle Mode="NextPrevAndNumeric" Position="TopAndBottom" FirstPageImageUrl="../images/first.png"
                                    NextPageImageUrl="../images/next.png" PrevPageImageUrl="../images/prev.png" LastPageImageUrl="../images/last.png"
                                    AlwaysVisible="True" />
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
            </div>
        </fieldset>
    </div>
    <asp:SqlDataSource ID="SystemLogsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
        SelectCommand="spWeb_Get_OB_Concur" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
</asp:Content>
