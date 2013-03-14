<%@ Page Title="Holcim - DTR Application" Language="VB" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="false" CodeFile="dtr_apply.aspx.vb" Inherits="dtr_apply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="HiddenField_employeeid" runat="server" />
    <table style="width: 739px; text-align: left">
        <tr style="color: #000000">
            <td colspan="3" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                height: 16px">
                <table style="width: 100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            DTR Application
                        </td>
                        <td style="color: red; text-align: right">
                            Please fill up all required fields in yellow.
                        </td>
                    </tr>
                </table>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Incomplete DTR data.  Cannot process"
                    ShowMessageBox="True" ValidationGroup="ValG" />
            </td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 230px;">
                &nbsp;</td>
            <td style="font-size: 15pt; color: red; " colspan="2">
                &nbsp;</td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 230px;">
                <strong>Employee Name : </strong>
            </td>
            <td style="font-size: 15pt; color: red; " colspan="2">
                <asp:DropDownList ID="dplEmployee" runat="server" Font-Size="8pt" Width="290px" AutoPostBack="True"
                    BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dplEmployee"
                    Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator>
            </td>
        </tr>
       <%-- <tr style="font-weight: bold; color: #000000">
            <td style="width: 230px">
                Payroll Period :
            </td>
            <td style="font-weight: normal; " colspan="2">
                <asp:DropDownList ID="payroll_period" runat="server" Font-Size="8pt" 
                    Width="290px" BackColor="#FFFFC0"
                    CssClass="Buttonhand1hover" DataSourceID="SqlDataSource1" 
                    DataTextField="payroll_date" DataValueField="payroll_date">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="payroll_period"
                    Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator>
            </td>
        </tr>--%>
       <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:DbConnectionString %>" 
            SelectCommand="spWeb_Get_PayrollPeriod" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="HiddenField_employeeid" Name="employeeid" 
                    PropertyName="Value" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>--%>
        <tr style="font-weight: bold; color: #000000">
            <td style="width: 230px">
                Date/Time Log:
            </td>
            <td style="font-weight: normal; " colspan="2">
                <asp:TextBox ID="tran_date" runat="server" Font-Size="8pt" TabIndex="2" Width="75px"
                    BackColor="#FFFFC0" CssClass="Buttonhand1hover"></asp:TextBox>
                   
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="tran_date"
        Format="dd-MMM-yyyy" PopupButtonID="tran_date" OnClientShown="calendarShown">
    </cc1:CalendarExtender>
    <script>
    function calendarShown(sender, args)  

    {  

         sender._popupBehavior._element.style.zIndex = 10005;  

     } 

    </script>
   
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tran_date"
                    ErrorMessage="Date cannot be blank. Please enter date" Font-Size="Large" SetFocusOnError="True"
                    ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                <asp:DropDownList ID="hh" runat="server" BackColor="#FFFFC0" CssClass="Buttonhand1hover"
                    AutoPostBack="False">
                    <asp:ListItem Value="01"></asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem Value="03"></asp:ListItem>
                    <asp:ListItem Value="04"></asp:ListItem>
                    <asp:ListItem Value="05"></asp:ListItem>
                    <asp:ListItem Value="06"></asp:ListItem>
                    <asp:ListItem Value="07"></asp:ListItem>
                    <asp:ListItem Value="08"></asp:ListItem>
                    <asp:ListItem Value="09"></asp:ListItem>
                    <asp:ListItem Value="10"></asp:ListItem>
                    <asp:ListItem Value="11"></asp:ListItem>
                    <asp:ListItem Value="12"></asp:ListItem>
                </asp:DropDownList>
                <asp:ListBox ID="mm" runat="server" Rows="1" BackColor="#FFFFC0" CssClass="Buttonhand1hover"
                    AutoPostBack="False">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
                    <asp:ListItem>02</asp:ListItem>
                    <asp:ListItem>03</asp:ListItem>
                    <asp:ListItem>04</asp:ListItem>
                    <asp:ListItem>05</asp:ListItem>
                    <asp:ListItem>06</asp:ListItem>
                    <asp:ListItem>07</asp:ListItem>
                    <asp:ListItem>08</asp:ListItem>
                    <asp:ListItem>09</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>11</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>13</asp:ListItem>
                    <asp:ListItem>14</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>17</asp:ListItem>
                    <asp:ListItem>18</asp:ListItem>
                    <asp:ListItem>19</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>21</asp:ListItem>
                    <asp:ListItem>22</asp:ListItem>
                    <asp:ListItem>23</asp:ListItem>
                    <asp:ListItem>24</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>26</asp:ListItem>
                    <asp:ListItem>27</asp:ListItem>
                    <asp:ListItem>28</asp:ListItem>
                    <asp:ListItem>29</asp:ListItem>
                    <asp:ListItem Value="30"></asp:ListItem>
                    <asp:ListItem>31</asp:ListItem>
                    <asp:ListItem>32</asp:ListItem>
                    <asp:ListItem>33</asp:ListItem>
                    <asp:ListItem>34</asp:ListItem>
                    <asp:ListItem>35</asp:ListItem>
                    <asp:ListItem>36</asp:ListItem>
                    <asp:ListItem>37</asp:ListItem>
                    <asp:ListItem>38</asp:ListItem>
                    <asp:ListItem>39</asp:ListItem>
                    <asp:ListItem>40</asp:ListItem>
                    <asp:ListItem>41</asp:ListItem>
                    <asp:ListItem>42</asp:ListItem>
                    <asp:ListItem>43</asp:ListItem>
                    <asp:ListItem>44</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>46</asp:ListItem>
                    <asp:ListItem>47</asp:ListItem>
                    <asp:ListItem>48</asp:ListItem>
                    <asp:ListItem>49</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>51</asp:ListItem>
                    <asp:ListItem>52</asp:ListItem>
                    <asp:ListItem>53</asp:ListItem>
                    <asp:ListItem>54</asp:ListItem>
                    <asp:ListItem>55</asp:ListItem>
                    <asp:ListItem>56</asp:ListItem>
                    <asp:ListItem>57</asp:ListItem>
                    <asp:ListItem>58</asp:ListItem>
                    <asp:ListItem>59</asp:ListItem>
                </asp:ListBox>
                <asp:ListBox ID="ampm" runat="server" Rows="1" BackColor="#FFFFC0" CssClass="Buttonhand1hover"
                    AutoPostBack="false">
                    <asp:ListItem Value="AM">AM</asp:ListItem>
                    <asp:ListItem Value="PM">PM</asp:ListItem>
                    
                </asp:ListBox>
                &nbsp;&nbsp;
                <strong>Log Type:&nbsp;&nbsp;&nbsp; </strong>
                <asp:DropDownList ID="tran_type" runat="server" Font-Size="8pt" Width="70px" BackColor="#FFFFC0"
                    CssClass="Buttonhand1hover">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="I">LOG-IN</asp:ListItem>
                    <asp:ListItem Value="O">LOG-OUT</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredClassificationValidator" runat="server" ControlToValidate="tran_type"
                    Font-Size="Large" SetFocusOnError="True" ErrorMessage="Log type is blank."
                    ValidationGroup="ValG">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 230px" valign="top">
                <strong>Reason :&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="reason" Font-Size="Large" SetFocusOnError="True" ValidationGroup="ValG"
                    ErrorMessage="Reason is blank.">*</asp:RequiredFieldValidator></strong>
            </td>
            <td style="vertical-align: top; width: 362px;">
                <asp:TextBox ID="reason" BackColor="#FFFFC0" runat="server" Font-Size="8pt" Height="70px" TextMode="MultiLine"
                    Width="290px"></asp:TextBox>
            </td>
            <td colspan="1" style="vertical-align: bottom; width: 531px; text-align: right">
                <asp:Label ID="Labelextend" runat="server" BackColor="Yellow" BorderColor="Red" BorderStyle="Inset"
                    Font-Bold="False" Font-Size="Large" ForeColor="DarkRed" Text="Is this for a continuous OT/ESD?"
                    Visible="False" Width="186px"></asp:Label>&nbsp;&nbsp;<br />
                &nbsp; &nbsp;<asp:Button ID="BtnSaveExtend" runat="server" BackColor="Red" BorderColor="DarkGray"
                    BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Add w/ Extended OT/ESD"
                    ValidationGroup="ValG" Width="149px" ToolTip="Add" Visible="False" CssClass="Buttonhand1hover" />
                <asp:Button ID="BtnYES" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                    Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Yes" ValidationGroup="ValG"
                    Width="87px" ToolTip="Add" Visible="False" CssClass="Buttonhand1hover" />
                <asp:Button ID="BtnNO" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                    Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="No" ValidationGroup="ValG"
                    Width="87px" ToolTip="Add" Visible="False" CssClass="Buttonhand1hover" />
                <asp:Button ID="btnSave" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                    Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Add" ValidationGroup="ValG"
                    Width="87px" ToolTip="Add" CssClass="Buttonhand1hover" />
            </td>
        </tr>
        <tr style="color: #000000">
            <td colspan="2" style="color: white; border-bottom: gray 1px solid; height: 16px;
                text-align: right;">
                &nbsp;
            </td>
            <td colspan="1" style="color: white; border-bottom: gray 1px solid; height: 16px;
                text-align: right; width: 531px;">
            </td>
        </tr>
    </table>
    
    <br />
    <div>
    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966"
        BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%" AutoGenerateColumns="False">
        <RowStyle ForeColor="DimGray" Wrap="True" />
        <Columns>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete_index" ForeColor="Red">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="employee_id" HeaderText="Personnel No."></asp:BoundField>
            <asp:BoundField DataField="employee_name" HeaderText="Employee Name"></asp:BoundField>
            <asp:BoundField DataField="tran_date" HeaderText="Date/Time"></asp:BoundField>
            <asp:BoundField DataField="tran_type" HeaderText="Transation Type" />
            <asp:BoundField DataField="reason" HeaderText="Reason"></asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
        <AlternatingRowStyle BackColor="#E0E0E0" />
    </asp:GridView>
    </div>
    <br />
    <asp:Button ID="btnSubmit" runat="server" BackColor="Red" BorderColor="DarkGray"
        BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Submit"
        Width="112px" Visible="False" CssClass="Buttonhand1hover" style="height: 20px" />&nbsp;
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="True"
        ConfirmText="Are you sure you want to submit this application?" TargetControlID="btnSubmit">
    </cc1:ConfirmButtonExtender>
</asp:Content>