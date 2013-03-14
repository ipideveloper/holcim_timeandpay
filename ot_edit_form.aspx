<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ot_edit_form.aspx.vb" Inherits="ot_edit_form" title="Holcim - OT/ESD Application" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <table style="width: 739px; text-align: left">
                <tr style="color: #000000">
                    <td colspan="4" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                        height: 16px">
                        <table style="width: 100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="height: 14px">
                                    Edit Overtime/Extra Shift</td>
                                <td style="color: red; text-align: right; height: 14px;">
                                    Please fill up all required fields in yellow.
                                </td>
                            </tr>
                        </table>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Incomplete Overtime data.  Cannot process"
                            ShowMessageBox="True" ValidationGroup="ValG" />
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td colspan="4" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                        height: 16px">
                        <span style="color: #000000">Employee Name: </span><span style="color: #000080">&nbsp;
                        </span>
                        <asp:Label ID="Lbl_name" runat="server" ForeColor="Black" Text="Label" Width="266px"></asp:Label><br />
                        <span style="color: black">Personnel No.: &nbsp; &nbsp; &nbsp; </span>
                        <asp:Label ID="lbl_emp_id" runat="server" ForeColor="Black" Text="Label" Width="124px"></asp:Label><span
                            style="color: black"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            &nbsp;&nbsp; Reference Number:&nbsp; </span>
                        <asp:Label ID="lbl_ref_no" runat="server" ForeColor="Black" Text="Label" Width="136px"></asp:Label><br />
                        <span style="color: black">Date Filed: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        </span>
                        <asp:Label ID="lbl_date_filed" runat="server" ForeColor="Black" Text="Label" Width="123px"></asp:Label><span
                            style="color: black"> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            &nbsp; Status: &nbsp; &nbsp;</span> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Label ID="lbl_status" runat="server" ForeColor="Black" Text="Label" Width="136px"></asp:Label></td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 230px;">
                        <strong> Employee Name : </strong></td>
                    <td colspan="2" style="font-size: 15pt; color: red; width: 362px;">
                        <asp:DropDownList ID="dplEmployee" runat="server" Font-Size="8pt" Width="290px" AutoPostBack="True" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                        </asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dplEmployee" Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator></td>
                    <td colspan="1" style="font-size: 15pt; width: 538px; color: red">
                    </td>
                </tr>
                <tr style="font-weight: bold; color: #000000">
                    <td style="width: 230px">
                        Date :</td>
                    <td colspan="2" style="font-weight: normal; width: 362px;">
                        <asp:TextBox ID="txt_date_from" runat="server" Font-Size="8pt" TabIndex="2" Width="75px" BackColor="#FFFFC0" CssClass="Buttonhand1hover"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_date_from"
                            ErrorMessage="Overtime Date cannot be blank.  Please enter date" Font-Size="Large"
                            SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_date_from" Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator></td>
                    <td colspan="1" style="font-weight: normal; width: 538px">
                    </td>
                </tr>
                <tr style="font-weight: bold; color: #000000">
                    <td style="width: 230px;">
                        Time From : </td>
                    <td colspan="2" style="font-weight: normal; width: 362px;">
                        <strong>
                        </strong>
                        <asp:DropDownList ID="dplFrom" AutoPostBack="True" runat="server" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
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
                        </asp:DropDownList><asp:ListBox ID="lstFrom" AutoPostBack="True" runat="server" Rows="1" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
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
                        </asp:ListBox><asp:ListBox ID="ampmFrom" AutoPostBack="True" runat="server" Rows="1" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                            <asp:ListItem Value="AM">AM</asp:ListItem>
                        </asp:ListBox>(hh:mm)</td>
                    <td colspan="1" style="font-weight: normal; width: 538px">
                     <asp:Label ID="Label_NoOfHrs" runat="server" Text="Total hours applied = " 
                            Visible="False"></asp:Label>
                        <asp:Label ID="Labelnoofhours" runat="server"></asp:Label></td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 230px;">
                        <strong> Time To : </strong></td>
                    <td colspan="2" style="width: 362px">
                        <asp:DropDownList ID="dplTo" runat="server" AutoPostBack="True" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
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
                        </asp:DropDownList><asp:ListBox ID="lstTo" AutoPostBack="True" runat="server" Rows="1" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
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
                        </asp:ListBox><asp:ListBox ID="ampmTo" runat="server" AutoPostBack="True" Rows="1" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                            <asp:ListItem>AM</asp:ListItem>
                        </asp:ListBox>(hh:mm)</td>
                    <td colspan="1" style="width: 538px">
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 230px; height: 23px">
                        &nbsp;</td>
                    <td style="width: 362px; height: 23px">
                        <asp:CheckBox ID="NextDayCheckBox" runat="server" Text="Next Day Time Out" 
                            AutoPostBack="True" /></td>
                    <td colspan="1" style="width: 531px; height: 23px">
                       
                       </td>
                </tr>
                <!--<tr style="color: #000000">
                    <td style="width: 230px; height: 23px">
                        <asp:TextBox ID="TextBoxexfrom" runat="server" BorderStyle="None" Font-Bold="True"
                            Font-Size="8pt" Visible="False" BorderColor="Transparent">Extended Time From:</asp:TextBox></td>
                    <td colspan="2" style="width: 362px; height: 23px">
                        <asp:DropDownList ID="dplFrom1" runat="server" BackColor="#FFFFC0" Enabled="False" Visible="False" CssClass="Buttonhand1hover">
                            <asp:ListItem Value="12"></asp:ListItem>
                            <asp:ListItem Value="11"></asp:ListItem>
                            <asp:ListItem Value="10"></asp:ListItem>
                            <asp:ListItem Value="09"></asp:ListItem>
                            <asp:ListItem Value="08"></asp:ListItem>
                            <asp:ListItem Value="07"></asp:ListItem>
                            <asp:ListItem Value="06"></asp:ListItem>
                            <asp:ListItem Value="05"></asp:ListItem>
                            <asp:ListItem Value="04"></asp:ListItem>
                            <asp:ListItem Value="03"></asp:ListItem>
                            <asp:ListItem>02</asp:ListItem>
                            <asp:ListItem Value="01"></asp:ListItem>
                        </asp:DropDownList><asp:ListBox ID="lstFrom1" runat="server" Rows="1" BackColor="#FFFFC0" Enabled="False" Visible="False" CssClass="Buttonhand1hover">
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
                        </asp:ListBox><asp:ListBox ID="ampmFrom1" runat="server" Rows="1" BackColor="#FFFFC0" Enabled="False" Visible="False" CssClass="Buttonhand1hover">
                            <asp:ListItem Value="AM">AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:ListBox><asp:TextBox ID="TextBoxexhhmmfrom" runat="server" BorderStyle="None"
                            Font-Size="8pt" Visible="False" BorderColor="Transparent">(hh:mm)</asp:TextBox></td>
                    <td colspan="1" style="width: 538px; height: 23px">
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 230px; height: 24px">
                        <strong>
                            <asp:TextBox ID="TextBoxexto" runat="server" BorderStyle="None" Font-Bold="True"
                                Font-Size="8pt" Visible="False" BorderColor="Transparent">Extended Time To:</asp:TextBox></strong></td>
                    <td colspan="2" style="width: 362px; height: 24px">
                        <asp:DropDownList ID="dplTo1" runat="server" BackColor="#FFFFC0" Enabled="False" Visible="False" CssClass="Buttonhand1hover">
                            <asp:ListItem Value="12"></asp:ListItem>
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
                        </asp:DropDownList><asp:ListBox ID="lstTo1" runat="server" Rows="1" BackColor="#FFFFC0" Enabled="False" Visible="False" CssClass="Buttonhand1hover">
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
                        </asp:ListBox><asp:ListBox ID="ampmTo1" runat="server" Rows="1" BackColor="#FFFFC0" Enabled="False" Visible="False" CssClass="Buttonhand1hover">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:ListBox><asp:TextBox ID="TextBoxexhhmmto" runat="server" BorderStyle="None"
                            Font-Size="8pt" Visible="False" BorderColor="Transparent">(hh:mm)</asp:TextBox></td>
                    <td colspan="1" style="width: 538px; height: 24px">
                    </td>
                </tr>-->
                <tr style="color: #000000">
                    <td style="width: 230px;">
                        Rea<strong>on:</strong></td>
                    <td colspan="2" style="width: 362px">
                        <asp:DropDownList ID="dpl_reason" runat="server" Font-Size="8pt" Width="290px" 
                            BackColor="#FFFFC0" CssClass="Buttonhand1hover" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dpl_reason"
                            ErrorMessage="Reason cannot be blank.  Please enter your reasons" Font-Size="Large"
                            SetFocusOnError="True" ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="dpl_reason"
                            Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator></td>
                    <td colspan="1" style="width: 538px">
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 230px;">
                        Classificati<strong>on : </strong>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="dpl_classifications" runat="server" Font-Size="8pt" Width="290px" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem>With Warm Body Replacement</asp:ListItem>
                            <asp:ListItem>With Out Warm Body Replacement</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredClassificationValidator" runat="server" ControlToValidate="dpl_classifications"
                            Font-Size="Large" SetFocusOnError="True" Visible="False">*</asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dpl_classifications"
                            Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator></td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 230px">
                    </td>
                    <td colspan="2" style="vertical-align: top; width: 362px;">
                        <asp:CheckBox ID="chkOncall" runat="server" Font-Bold="True" Text="On call" CssClass="Buttonhand1hover" /></td>
                    <td colspan="1" style="vertical-align: top; width: 538px">
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 230px">
                        <strong>Remarks :&nbsp;</strong></td>
                    <td colspan="2" style="vertical-align: top; width: 362px;">
                        <asp:TextBox ID="txtRemarks" runat="server" Font-Size="8pt" Height="93px" TextMode="MultiLine"
                            Width="355px"></asp:TextBox></td>
                    <td colspan="1" style="vertical-align: bottom; width: 538px; text-align: right">
                        <asp:Label ID="Labelextend" runat="server" BackColor="Yellow" BorderColor="Red" BorderStyle="Inset"
                            Font-Bold="False" Font-Size="Large" ForeColor="DarkRed" Text="Is this for a continuous OT/ESD?"
                            Visible="False" Width="186px"></asp:Label>&nbsp;&nbsp;<br />
                        &nbsp; &nbsp;<asp:Button ID="BtnSaveExtend" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Update w/ Extended OT/ESD" ValidationGroup="ValG"
                            Width="149px" ToolTip="Add" Visible="False" CssClass="Buttonhand1hover" />
                        <asp:Button ID="BtnYES" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Yes" ValidationGroup="ValG"
                            Width="87px" ToolTip="Add" Visible="False" CssClass="Buttonhand1hover" />
                        <asp:Button ID="BtnNO" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="No" ValidationGroup="ValG"
                            Width="87px" ToolTip="Add" Visible="False" CssClass="Buttonhand1hover" />
                        <asp:Button ID="btnSave" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Update" ValidationGroup="ValG"
                            Width="87px" ToolTip="Add" CssClass="Buttonhand1hover" /></td>
                </tr>
                <tr style="color: #000000">
                    <td colspan="3" style="color: white; border-bottom: gray 1px solid; height: 16px; text-align: right;">
                        &nbsp;
                    </td>
                    <td colspan="1" style="color: white; border-bottom: gray 1px solid; height: 16px;
                        text-align: right; width: 538px;">
                    </td>
                </tr>
            </table>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_date_from" Format="dd-MMM-yyyy" PopupButtonID="txt_date_from">
                        </cc1:CalendarExtender>
    <br />
        <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#CC9966"
            BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="100%" AutoGenerateColumns="False">
            <RowStyle ForeColor="DimGray" Wrap="True" />
            <Columns>
<asp:TemplateField><EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    
</EditItemTemplate>
<ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete_index" ForeColor="Red">Delete</asp:LinkButton>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="employee_id" HeaderText="Personnel No."></asp:BoundField>
<asp:BoundField DataField="employee_name" HeaderText="Employee Name"></asp:BoundField>
                <asp:BoundField DataField="date" HeaderText="Date" />
<asp:BoundField DataField="time_from" HeaderText="From"></asp:BoundField>
<asp:BoundField DataField="time_to" HeaderText="To"></asp:BoundField>
<asp:BoundField DataField="otesd_hours" HeaderText="OT/ESD Hours"></asp:BoundField>
<asp:BoundField DataField="on_call" HeaderText="On Call?"></asp:BoundField>
                <asp:BoundField DataField="classification" HeaderText="Classification" />
<asp:BoundField DataField="reason" HeaderText="Reason"></asp:BoundField>
<asp:BoundField DataField="remarks" HeaderText="Remarks"></asp:BoundField>
</Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <AlternatingRowStyle BackColor="#E0E0E0" />
        </asp:GridView>
    <br />
    <asp:Button ID="btnSubmit" runat="server" BackColor="Red" BorderColor="DarkGray"
        BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Submit"
        Width="112px" Visible="False" CssClass="Buttonhand1hover" />&nbsp;
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="True"
        ConfirmText="Are you sure you want to submit this application?" TargetControlID="btnSubmit">
    </cc1:ConfirmButtonExtender>
    &nbsp;
    <br />
</asp:Content>

