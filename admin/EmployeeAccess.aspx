<%@ Page Title="Employee Access Reports" Language="VB" MasterPageFile="~/admin/AdminMasterPage.master"
    AutoEventWireup="false" CodeFile="EmployeeAccess.aspx.vb" Inherits="admin_EmployeeAccess" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="Loader" runat="Server">
    <div id="mybox">
        <div class="boxMid">
            Generating User Access Report... Please wait...
            <img src="css/ajax-loader.gif" alt="loader" /></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="g12 nodrop">
        <div style="background-color: #FFFFFF;">
            <div style="float: left; margin-left: 10px;">
                <span style="font-size: 24px;">Employee Access Query</span></div>
        </div>
        <div style="clear: both">
        </div>
      <%--  <p style="margin-left: 10px;">
            Input personnel no. and click show access button to view user access.
        </p>--%>
        <%--<fieldset>
            <%--<label>
                New Approval Status</label>--%>
        <%--<section>
                <label for="username">ID&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator_TextBoxUsername" runat="server" ValidationGroup="adduser" 
                ControlToValidate="StatusID" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label><div><asp:TextBox ID="StatusID" runat="server" ValidationGroup="adduser"></asp:TextBox></div>
			</section>--%>
        <%--<section>
                <label for="username">Personnel No.&nbsp;&nbsp;<asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" runat="server" ValidationGroup="showaccess" 
                ControlToValidate="employeeid" Display="Dynamic" 
                ErrorMessage="&lt;img src=&quot;css/light/images/required.png&quot;&gt;"></asp:RequiredFieldValidator></label><div><asp:TextBox ID="employeeid" runat="server" ValidationGroup="showaccess"></asp:TextBox></div>
			</section>
            <section>
				<div>
				<asp:Button ID="Button_ShowAccess" ValidationGroup="showaccess" runat="server" Text="   Show Access   "></asp:Button>
				<asp:Button ID="Button_ShowAllAccess" ValidationGroup="showallaccess" runat="server" Text="   Show All   "></asp:Button>
				</div>
			</section>
        </fieldset>--%>
        <%--<label>Access List</label>--%>
        <fieldset>
            <div>
                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                    <ProgressTemplate>
                        <div id="mybox1">
                            <div class="boxMid" style="top: 70% !important;">
                                Loading Data... Please wait...
                                <img src="css/ajax-loader.gif" alt="loader" /></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div id="filterdiv">
                            <div style="display: block; margin-bottom: 5px;">
                                Filter by:</div>
                            <telerik:RadFilter runat="server" ID="RadFilter1"  FilterContainerID="RadGrid1" ShowApplyButton="true"
                                ApplyButtonText="Search / Apply Filter" AllowFilterOnBlur="True" />
                        </div>
                        <div style="clear: both;">
                        </div>
                        <telerik:RadGrid ID="RadGrid1" runat="server" CellSpacing="0" DataSourceID="SqlDataSource"
                            GridLines="None" HorizontalAlign="Left" AllowPaging="true" PageSize="10" Skin="Transparent"
                            AllowFilteringByColumn="true">
                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                                <WebServiceSettings>
                                    <ODataSettings InitialContainerName="">
                                    </ODataSettings>
                                </WebServiceSettings>
                            </HeaderContextMenu>
                            <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource" IsFilterItemExpanded="false"
                                GroupLoadMode="Client" ExpandCollapseColumn-Display="false" HierarchyDefaultExpanded="false"
                                GroupHeaderItemStyle-BackColor="#787B7E" GroupHeaderItemStyle-HorizontalAlign="Left"
                                GroupHeaderItemStyle-ForeColor="#FFFFFF">
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <%-- <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                                  Visible="True">
                                  <HeaderStyle Width="20px" />
                              </ExpandCollapseColumn>--%>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="employeeid" FilterControlAltText="Filter employeeid column"
                                        HeaderText="Personnel No." SortExpression="employeeid" UniqueName="employeeid" ItemStyle-Wrap="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fullname" FilterControlAltText="Filter fullname column"
                                        HeaderText="Name" SortExpression="fullname" UniqueName="fullname" ItemStyle-Wrap="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="sector" FilterControlAltText="Filter sector column"
                                        HeaderText="Sector" SortExpression="sector" UniqueName="sector" ItemStyle-Wrap="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_leave" DataType="System.Boolean" FilterControlAltText="Filter mod_leave column"
                                        HeaderText="LV" SortExpression="mod_leave" UniqueName="mod_leave">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_overtime" DataType="System.Boolean" FilterControlAltText="Filter mod_overtime column"
                                        HeaderText="OT" SortExpression="mod_overtime" UniqueName="mod_overtime">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_travel_order" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_travel_order column" HeaderText="OB"
                                        SortExpression="mod_travel_order" UniqueName="mod_travel_order">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_shift_sched" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_shift_sched column" HeaderText="SH"
                                        SortExpression="mod_shift_sched" UniqueName="mod_shift_sched">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_on_call" DataType="System.Boolean" FilterControlAltText="Filter mod_on_call column"
                                        HeaderText="OC" SortExpression="mod_on_call" UniqueName="mod_on_call">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn Visible="false" DataField="mod_dtr_app" DataType="System.Boolean" FilterControlAltText="Filter mod_dtr_app column"
                                        HeaderText="DTRA" SortExpression="mod_dtr_app" UniqueName="mod_dtr_app">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_request_status" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_request_status column" HeaderText="REQ"
                                        SortExpression="mod_request_status" UniqueName="mod_request_status">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_leave_balance" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_leave_balance column" HeaderText="BAL"
                                        SortExpression="mod_leave_balance" UniqueName="mod_leave_balance">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_my_approvals" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_my_approvals column" HeaderText="APV"
                                        SortExpression="mod_my_approvals" UniqueName="mod_my_approvals">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_my_shift_approvals" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_my_shift_approvals column" HeaderText="SHAPV"
                                        SortExpression="mod_my_shift_approvals" UniqueName="mod_my_shift_approvals">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_my_sched" DataType="System.Boolean" FilterControlAltText="Filter mod_my_sched column"
                                        HeaderText="SCHD" SortExpression="mod_my_sched" UniqueName="mod_my_sched">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_my_dtr" DataType="System.Boolean" FilterControlAltText="Filter mod_my_dtr column"
                                        HeaderText="DTY" SortExpression="mod_my_dtr" UniqueName="mod_my_dtr">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_my_employees_dtr" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_my_employees_dtr column" HeaderText="DTR"
                                        SortExpression="mod_my_employees_dtr" Visible="false" UniqueName="mod_my_employees_dtr">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_my_payslip" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_my_payslip column" HeaderText="PAY"
                                        SortExpression="mod_my_payslip" UniqueName="mod_my_payslip">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_change_password" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_change_password column" HeaderText="PASS"
                                        SortExpression="mod_change_password" UniqueName="mod_change_password">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_user_access" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_user_access column" HeaderText="UAC"
                                        SortExpression="mod_user_access" UniqueName="mod_user_access">
                                    </telerik:GridCheckBoxColumn>
                                    <telerik:GridCheckBoxColumn DataField="mod_cancel_app" DataType="System.Boolean"
                                        FilterControlAltText="Filter mod_cancel_app column" HeaderText="CNCL"
                                        SortExpression="mod_cancel_app" UniqueName="mod_cancel_app">
                                    </telerik:GridCheckBoxColumn>
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
                        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:DbConnectionString %>"
                            SelectCommand="SELECT * FROM temp_user_access ORDER BY fullname" SelectCommandType="Text">
                        </asp:SqlDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </fieldset>
    </div>

    <script type="text/javascript">
        $(window).ready(function() {
            $('#mybox').fadeOut(1000);
        });

    </script>

</asp:Content>
