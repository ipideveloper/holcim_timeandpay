<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="oncall_apply.aspx.vb" Inherits="oncall_apply" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <table style="width: 740px; text-align: left">
                <tr style="color: #000000">
                    <td colspan="3" style="font-weight: bold; color: gray; border-bottom: gray 1px solid;
                        height: 16px">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 154px">
                        File On Call</td>
                                <td style="color: red; text-align: right">
                                    Please fill up all required fields in yellow.
                                </td>
                            </tr>
                        </table>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Incomplete On call information.  Cannot process"
                            ShowMessageBox="True" ValidationGroup="ValG" />
                    </td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 118px; height: 16px">
                        Employee Name :</td>
                    <td colspan="2" style="font-size: 15pt; color: red; height: 16px; width: 619px;">
                        <asp:DropDownList id="dplEmployee" runat="server" Font-Size="8pt" Width="290px" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                        </asp:DropDownList>&nbsp;
                        <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server"
                            ControlToValidate="dplEmployee" Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator></td>
                </tr>
                <tr style="font-weight: bold; color: #000000">
                    <td style="width: 118px; height: 24px;">
                        Date :</td>
                    <td colspan="2" style="font-weight: normal; height: 24px; width: 619px;">
                        <asp:TextBox id="txt_date_from" runat="server" Font-Size="8pt" tabIndex="2" Width="75px" BackColor="#FFFFC0" CssClass="Buttonhand1hover"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_date_from"
                            ErrorMessage="On Call Date cannot be blank.  Please enter date" Font-Size="Large"
                            ValidationGroup="ValG">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="txt_date_to" runat="server" Font-Size="8pt" TabIndex="2" Width="75px" BackColor="#FFFFC0" Enabled="False" Visible="False" CssClass="Buttonhand1hover"></asp:TextBox></td>
                </tr>
                <tr style="font-weight: bold; color: #000000">
                    <td style="width: 118px">
                        Time From :</td>
                    <td colspan="2" style="font-weight: normal; height: 16px; width: 619px;">
                        <asp:DropDownList id="dplFrom" runat="server" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
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
                        </asp:DropDownList><asp:ListBox ID="lstFrom" runat="server" BackColor="#FFFFC0" Rows="1" CssClass="Buttonhand1hover">
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
                        </asp:ListBox><asp:ListBox id="ampmFrom" runat="server" Rows="1" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                            <asp:ListItem Value="AM">AM</asp:ListItem>
                        </asp:ListBox>(hh:mm)<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server"
                            ControlToValidate="txt_date_from" Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator></td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 118px; height: 16px">
                        <strong> Time To :</strong></td>
                    <td colspan="2" style="height: 16px; width: 619px;">
                        <asp:DropDownList id="dplTo" runat="server" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
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
                        </asp:DropDownList><asp:ListBox ID="lstTo" runat="server" BackColor="#FFFFC0" Rows="1" CssClass="Buttonhand1hover">
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
                        </asp:ListBox><asp:ListBox id="ampmTo" runat="server" Rows="1" BackColor="#FFFFC0" CssClass="Buttonhand1hover">
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                            <asp:ListItem>AM</asp:ListItem>
                        </asp:ListBox>(hh:mm)<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ControlToValidate="txt_date_to" Font-Bold="False" Font-Size="1pt" ValidationGroup="Val"></asp:RequiredFieldValidator></td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 118px; height: 16px">
                        <asp:TextBox ID="TextBoxexfrom" runat="server" BorderStyle="None" Font-Bold="True"
                            Font-Size="8pt" Visible="False" BorderColor="Transparent">Extended Time From:</asp:TextBox></td>
                    <td colspan="2" style="width: 619px; height: 16px">
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
                        </asp:DropDownList><asp:ListBox ID="lstFrom1" runat="server" BackColor="#FFFFC0"
                            Enabled="False" Rows="1" Visible="False" CssClass="Buttonhand1hover">
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
                        </asp:ListBox><asp:ListBox ID="ampmFrom1" runat="server" BackColor="#FFFFC0" Enabled="False"
                            Rows="1" Visible="False" CssClass="Buttonhand1hover">
                            <asp:ListItem Value="AM">AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:ListBox><asp:TextBox ID="TextBoxexhhmmfrom" runat="server" BorderStyle="None"
                            Font-Size="8pt" Visible="False" BorderColor="Transparent">(hh:mm)</asp:TextBox></td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 118px; height: 16px">
                        <asp:TextBox ID="TextBoxexto" runat="server" BorderStyle="None" Font-Bold="True"
                            Font-Size="8pt" Visible="False" BorderColor="Transparent">Extended Time To:</asp:TextBox></td>
                    <td colspan="2" style="width: 619px; height: 16px">
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
                        </asp:DropDownList><asp:ListBox ID="lstTo1" runat="server" BackColor="#FFFFC0" Rows="1" Enabled="False" Visible="False" CssClass="Buttonhand1hover">
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
                        </asp:ListBox><asp:ListBox ID="ampmTo1" runat="server" BackColor="#FFFFC0" Rows="1" Enabled="False" Visible="False" CssClass="Buttonhand1hover">
                            <asp:ListItem>AM</asp:ListItem>
                            <asp:ListItem Value="PM">PM</asp:ListItem>
                        </asp:ListBox><asp:TextBox ID="TextBoxexhhmmto" runat="server" BorderStyle="None"
                            Font-Size="8pt" Visible="False" BorderColor="Transparent">(hh:mm)</asp:TextBox></td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 118px; height: 16px">
                        <strong>Contacted by : </strong>
                    </td>
                    <td colspan="2" style="height: 16px; width: 619px;"><asp:TextBox id="txt_contact_by" runat="server" Font-Size="8pt" tabIndex="2" Width="355px" MaxLength="50" BackColor="#FFFFC0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_contact_by"
                            ErrorMessage="Contacted by cannot be blank.  Please enter contacted by." Font-Size="Large"
                            ValidationGroup="ValG">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr style="color: #000000">
                    <td style="width: 118px">
                        <strong>Details :&nbsp;</strong></td>
                    <td colspan="2" style="vertical-align: top; width: 619px;">
                        <asp:TextBox id="txt_details" runat="server" Font-Size="8pt" Height="45px" TextMode="MultiLine"
                            Width="355px" BackColor="#FFFFC0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_details"
                            ErrorMessage="Details cannot be blank.  Please enter details." Font-Size="Large"
                            ValidationGroup="ValG">*</asp:RequiredFieldValidator></td>
                </tr>
                <tr style="color: #000000">
                    <td colspan="1" style="color: white; border-bottom: gray 1px solid; height: 16px">
                    </td>
                    <td colspan="3" style="color: white; border-bottom: gray 1px solid; height: 16px">
                        <asp:Label ID="Labelextend" runat="server" BackColor="Yellow" BorderColor="Red" BorderStyle="Inset"
                            Font-Bold="False" Font-Size="Large" ForeColor="DarkRed" Text="Is this for a continuous On Call?"
                            Visible="False" Width="186px"></asp:Label><br />
                        <asp:Button ID="BtnYES" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Yes" ToolTip="Add"
                            UseSubmitBehavior="False" ValidationGroup="ValG" Visible="False" Width="87px" CssClass="Buttonhand1hover" /><asp:Button
                                ID="BtnNO" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                                Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="No" ToolTip="Add"
                                ValidationGroup="ValG" Visible="False" Width="87px" CssClass="Buttonhand1hover" /></td>
                </tr>
                <tr style="color: #000000">
                    <td colspan="1" style="color: white; border-bottom: gray 1px solid; height: 16px">
                    </td>
                    <td colspan="3" style="color: white; border-bottom: gray 1px solid; height: 16px">
                        .<br />
                        <asp:Button id="btnSave" runat="server" BackColor="Red" BorderColor="DarkGray" BorderWidth="1px"
                            Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Add" ValidationGroup="ValG"
                            Width="87px" UseSubmitBehavior="False" CssClass="Buttonhand1hover"  />
                        <asp:Button ID="BtnSaveExtend" runat="server" BackColor="Red" BorderColor="DarkGray"
                            BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Add w/ Extended On Call"
                            ToolTip="Add" ValidationGroup="ValG" Visible="False" Width="149px" CssClass="Buttonhand1hover" /></td>
                </tr>
            </table>
    &nbsp;
    <asp:Panel id="Panel1" runat="server" Height="149px" ScrollBars="Vertical" Width="100%">
        <asp:GridView id="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="97%">
            <rowstyle forecolor="DimGray" wrap="True" />
            <columns>
<asp:TemplateField><EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    
</EditItemTemplate>
<ItemTemplate>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete_index" ForeColor="Red">Delete</asp:LinkButton>
                    
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="employee_id" HeaderText="Personnel No."></asp:BoundField>
<asp:BoundField DataField="employee_name" HeaderText="Employee Name"></asp:BoundField>
<asp:BoundField DataField="time_from" HeaderText="From"></asp:BoundField>
<asp:BoundField DataField="time_to" HeaderText="To"></asp:BoundField>
<asp:BoundField DataField="contacted_by" HeaderText="Contacted by"></asp:BoundField>
<asp:BoundField DataField="details" HeaderText="Details"></asp:BoundField>
</columns>
            <footerstyle backcolor="#FFFFCC" forecolor="#330099" />
            <pagerstyle backcolor="#FFFFCC" forecolor="#330099" horizontalalign="Center" />
            <selectedrowstyle backcolor="#FFCC66" font-bold="True" forecolor="#663399" />
            <headerstyle backcolor="#990000" font-bold="True" forecolor="#FFFFCC" />
            <alternatingrowstyle backcolor="#E0E0E0" />
        </asp:GridView>
    </asp:Panel>
    &nbsp; &nbsp;
    &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
    <asp:Button id="btnSubmit" runat="server" BackColor="Red" BorderColor="DarkGray"
        BorderWidth="1px" Font-Names="arial" Font-Size="8pt" ForeColor="White" Text="Submit"
        Visible="False" Width="112px" OnClick="btnSubmit_Click" CssClass="Buttonhand1hover" />
    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txt_date_from"
        TargetControlID="txt_date_from" Format="dd-MMM-yyyy">
    </cc1:CalendarExtender>
    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
        PopupButtonID="txt_date_to" TargetControlID="txt_date_to">
    </cc1:CalendarExtender>
    &nbsp;
    <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="True"
        ConfirmText="Are you sure you want to submit this application?" TargetControlID="btnSubmit">
    </cc1:ConfirmButtonExtender>
</asp:Content>

