Imports System.Data
Imports cls_Email_Notifications

Partial Class ob_edit_form
    Inherits System.Web.UI.Page

#Region "Properties"

    Public ReadOnly Property Current_User() As Cls_UserProperty
        Get
            Return New Cls_UserProperty(ViewState("Employee_ID"))
        End Get
    End Property

#End Region

#Region "Variables"

    Private ws As New localhost.Service
    Private cls_email As New cls_Email_Notifications

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Request.IsAuthenticated Then
        '    Response.Redirect("Default.aspx")
        'End If
        If Session.IsNewSession Then
            UserMsgBox("Session Expired!")
            Response.Redirect("Default.aspx")
        End If
        Try
            If Not Page.IsPostBack Then
                Initialize()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Session("Employee_ID") = ViewState("Employee_ID")
        Response.Redirect(Session("Return_OB")) ' "my_app_status.aspx")
    End Sub

    Protected Sub btn_add_transpo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add_transpo.Click
        Try
            add_tranportation()
            clear_fields()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Delete_index" Then
            delete_gridview_row(index)
        End If
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            lnkDelete.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub gv_lodging_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_lodging.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Delete_index" Then
            delete_gv_lodging_row(index)
        End If
    End Sub

    Protected Sub gv_lodging_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_lodging.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            lnkDelete.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub gv_cash_advance_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_cash_advance.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Delete_index" Then
            delete_gridview_row_cash_advance(index)
        End If
    End Sub

    Protected Sub gv_cash_advance_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_cash_advance.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            lnkDelete.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If ViewState("pressed") = "" Then
                ViewState("pressed") = "True"
                btnSave.Enabled = False

                If IsNumeric(txtcontactno.Text) = False Then
                    txtcontactno.Focus()
                    UserMsgBox("Invalid Entry! Contact Number should be numeric.")
                    btnSave.Enabled = True
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                If r_dependent_yes.Checked And ((Trim(txtdependentname1.Text) = "" Or _
                       Trim(txtdepage1.Text) = "" Or Trim(txtdeppass1.Text) = "") _
                        ) Then
                    txtdependentname1.Focus()
                    UserMsgBox("Incomplete data! Please provide dependent's information.")
                    btnSave.Enabled = True
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                If CType(txtdateto.Text, Date) < CType(txtdatefrom.Text, Date) Then
                    UserMsgBox("Invalid Entry! Date To should not be earlier than Date From.")
                    btnSave.Enabled = True
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                If ws.Validate_Duplicate_DateTime(Session("RefNo"), IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), CType(txtdatefrom.Text, Date), CType(txtdateto.Text, Date), "OB") = True Then
                    UserMsgBox("You have filed the same date with your previous application.")
                    btnSave.Enabled = True
                    ViewState("pressed") = ""
                    Exit Sub
                End If
                'Added by Andrew 11-3-2011 Start
                If dpltravel_type.SelectedItem.Text = "Foreign" Then
                    If txtcontactemergency.Text = "" Then
                        UserMsgBox("Please Fill-up Contact Person in case of Emergency.")
                        btnSave.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    End If
                    If txtpassport.Text = "" Then
                        UserMsgBox("Please Fill-up Passport Field.")
                        btnSave.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    End If
                    If txtphone.Text = "" Then
                        UserMsgBox("Please Fill-up telephone No. Field.")
                        btnSave.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    End If
                    If r_dependent_yes.Checked = True Then
                        If txtdependentname1.Text = "" And txtdepage1.Text = "" And txtdeppass1.Text = "" Then
                            UserMsgBox("Please Enter Atleast 1 dependent Information.")
                            btnSave.Enabled = True
                            ViewState("pressed") = ""
                            Exit Sub
                        End If
                    End If

                    'Else
                    '    Dim ref_no As String = insert_ob_header()
                    '    insert_ob_details(ref_no)
                    '    insert_ob_transportation(ref_no)
                    '    insert_ob_cash_advance(ref_no)
                    '    insert_ob_lodging(ref_no)
                    '    ws.Insert_Employees_Approvers(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), ref_no)
                    '    ws.Update_Approve_Applications(ref_no, Current_User.Employee_ID, "OB")
                    '    send_notifications(ref_no, dplEmployee.SelectedItem.Text)
                    '    ViewState("pressed") = ""
                    '    Session("Employee_ID") = ViewState("Employee_ID")
                    '    btnSave.Enabled = True
                    '    Response.Redirect("ob_header.aspx")
                End If
                'Added by Andrew 11-3-2011 End
                Dim ref_no As String = edit_ob_header()
                insert_ob_details(ref_no)
                insert_ob_transportation(ref_no)
                insert_ob_cash_advance(ref_no)
                insert_ob_lodging(ref_no)
                'ws.Insert_Employees_Approvers(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), ref_no)
                'ws.Update_Approve_Applications(ref_no, Current_User.Employee_ID, "OB")
                'send_notifications(ref_no, dplEmployee.SelectedItem.Text)
                ViewState("pressed") = ""
                Session("Employee_ID") = ViewState("Employee_ID")
                btnSave.Enabled = True
                Response.Redirect(Session("Return_OB")) '"my_app_status.aspx")
            End If
        Catch ex As Exception
            txtpurposeoftravel.Text = ex.Message
            ViewState("pressed") = ""
            'UserMsgBox(ex.Message)
            btnSave.Enabled = True

        End Try
    End Sub

    Protected Sub btn_add_allowance_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add_allowance.Click
        Try
            If IsNumeric(txt_cash_amount.Text) = False Then
                txt_cash_amount.Focus()
                UserMsgBox("Invalid Entry! Cash amount should be numeric.")
                btnSave.Enabled = True
                Exit Sub
            End If

            add_cash_advance()
            clear_fields()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub dplEmployee_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dplEmployee.SelectedIndexChanged
        Try
            get_some_field(dplEmployee.SelectedValue)
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnAddLodging_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddLodging.Click
        Try

            If dplaccomodationtype.SelectedItem.Text = "Hotel" And Trim(txtpreferredhotel.Text) = "" Then
                txtpreferredhotel.Focus()
                UserMsgBox("Please specify preferred hotel")
                Exit Sub
            End If

            Dim checkin As String = txtcheckin.Text & " " & dplCheckIN.Text & ":" & lstCheckIN.Text & " " & ampmCheckIN.Text
            Dim checkout As String = txtcheckout.Text & " " & dplCheckOUT.Text & ":" & lstCheckOUT.Text & " " & ampmCheckOUT.Text

            If CType(checkout, DateTime) < CType(checkin, DateTime) Then
                UserMsgBox("Invalid Entry! Check OUT should not be earlier than Check IN.")
            Else
                add_lodging()
                clear_fields()
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        get_employee_list()
        get_ob_details(Session("RefNo"))
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"

    Private Sub add_tranportation()
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("vehicle_type"))
        dt.Columns.Add(New DataColumn("date"))
        dt.Columns.Add(New DataColumn("destination"))
        dt.Columns.Add(New DataColumn("airline_code"))
        dt.Columns.Add(New DataColumn("flight_vessel"))
        dt.Columns.Add(New DataColumn("etd"))
        dt.Columns.Add(New DataColumn("eta"))
        dt.Columns.Add(New DataColumn("pickup_time"))

        If GridView1.Rows.Count > 0 Then
            For i As Integer = 0 To GridView1.Rows.Count - 1
                dr = dt.NewRow()
                dr("vehicle_type") = IIf(GridView1.Rows(i).Cells(1).Text = "", "-", GridView1.Rows(i).Cells(1).Text)
                dr("date") = IIf(GridView1.Rows(i).Cells(2).Text = "", "-", GridView1.Rows(i).Cells(2).Text)
                dr("destination") = IIf(GridView1.Rows(i).Cells(3).Text = "", "-", GridView1.Rows(i).Cells(3).Text)
                dr("airline_code") = IIf(GridView1.Rows(i).Cells(4).Text = "&nbsp;", "-", GridView1.Rows(i).Cells(4).Text)
                dr("flight_vessel") = IIf(GridView1.Rows(i).Cells(5).Text = "", "-", GridView1.Rows(i).Cells(5).Text)
                dr("etd") = IIf(GridView1.Rows(i).Cells(6).Text = "", "-", GridView1.Rows(i).Cells(6).Text)
                dr("eta") = IIf(GridView1.Rows(i).Cells(7).Text = "", "-", GridView1.Rows(i).Cells(7).Text)
                dr("pickup_time") = IIf(GridView1.Rows(i).Cells(8).Text = "", "-", GridView1.Rows(i).Cells(8).Text)
                dt.Rows.Add(dr)
            Next
        End If

        dr = dt.NewRow()
        dr("vehicle_type") = IIf(dpl_vehicle.SelectedIndex = 0, "-", dpl_vehicle.SelectedItem.Text)
        dr("date") = IIf(txttranspodate.Text = "", "-", txttranspodate.Text)
        dr("destination") = txtdestinationfrom.Text & " - " & txtdestinationto.Text

        If dpl_vehicle.SelectedIndex = 4 Or dpl_vehicle.SelectedIndex = 5 Then
            dr("airline_code") = airline_code.SelectedValue.ToString
        Else
            dr("airline_code") = ""
        End If

        dr("flight_vessel") = IIf(txtvessel.Text = "", "-", txtvessel.Text)
        dr("etd") = IIf(txtetd.Text = "", "-", txtetd.Text)
        dr("eta") = IIf(txteta.Text = "", "-", txteta.Text)
        dr("pickup_time") = IIf(txtpickup.Text = "", "-", txtpickup.Text)
        dt.Rows.Add(dr)

        GridView1.DataSource = dt
        GridView1.DataBind()

        airline_code.Visible = False
    End Sub

    Private Sub add_lodging()
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("accommodation_type"))
        dt.Columns.Add(New DataColumn("preferred_hotel"))
        dt.Columns.Add(New DataColumn("check_in"))
        dt.Columns.Add(New DataColumn("check_out"))

        Dim total As Double = 0.0

        If gv_lodging.Rows.Count > 0 Then
            For i As Integer = 0 To gv_lodging.Rows.Count - 1
                dr = dt.NewRow()
                dr("accommodation_type") = gv_lodging.Rows(i).Cells(1).Text
                dr("preferred_hotel") = gv_lodging.Rows(i).Cells(2).Text
                dr("check_in") = gv_lodging.Rows(i).Cells(3).Text
                dr("check_out") = gv_lodging.Rows(i).Cells(4).Text
                dt.Rows.Add(dr)
            Next
        End If

        dr = dt.NewRow()
        dr("accommodation_type") = dplaccomodationtype.Text
        dr("preferred_hotel") = txtpreferredhotel.Text
        dr("check_in") = txtcheckin.Text & " " & dplCheckIN.Text & ":" & lstCheckIN.Text & " " & ampmCheckIN.Text
        dr("check_out") = txtcheckout.Text & " " & dplCheckOUT.Text & ":" & lstCheckOUT.Text & " " & ampmCheckOUT.Text

        dt.Rows.Add(dr)

        gv_lodging.DataSource = dt
        gv_lodging.DataBind()
    End Sub

    Private Sub add_cash_advance()
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("purpose"))
        dt.Columns.Add(New DataColumn("amount"))

        Dim total As Double = 0.0

        If gv_cash_advance.Rows.Count > 0 Then
            For i As Integer = 0 To gv_cash_advance.Rows.Count - 1
                dr = dt.NewRow()
                dr("purpose") = gv_cash_advance.Rows(i).Cells(1).Text
                dr("amount") = gv_cash_advance.Rows(i).Cells(2).Text
                dt.Rows.Add(dr)
                total = total + CType(gv_cash_advance.Rows(i).Cells(2).Text, Double)
            Next
        End If

        dr = dt.NewRow()
        dr("purpose") = txt_cash_purpose.Text
        dr("amount") = txt_cash_amount.Text
        dt.Rows.Add(dr)
        total = total + CType(txt_cash_amount.Text, Double)

        gv_cash_advance.DataSource = dt
        gv_cash_advance.DataBind()

        gv_cash_advance.FooterRow.Cells(1).Text = "Total"
        gv_cash_advance.FooterRow.Cells(1).HorizontalAlign = HorizontalAlign.Right
        gv_cash_advance.FooterRow.Cells(2).Style("font-weight") = "bold"
        gv_cash_advance.FooterRow.Cells(2).Text = String.Format("{0:N2}", total)
    End Sub

    Private Function edit_ob_header() As String
        Dim ob_type As Boolean

        If RadioButton_RegularOB.Checked = True Then
            ob_type = "1"
        End If
        If RadioButton_CrossPostingOB.Checked = True Then
            ob_type = "0"
        End If

        Dim ref_no As String = ws.Update_OB_Header(Session("RefNo"), IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), _
            dpltravel_type.SelectedItem.Text, txtpurposeoftravel.Text, _
                txtdatefrom.Text, txtdateto.Text, txtdestination.Text, txtcontactno.Text, txtcostcenter.Text, txtposition.Text, _
                dplaccomodationtype.SelectedItem.Text, txtpreferredhotel.Text, txtcheckin.Text, txtcheckout.Text, _
                IIf(r_visa_no.Checked = True, "No", "Yes"), txtcontactemergency.Text, txtpassport.Text, txtphone.Text, IIf(r_dependent_no.Checked = True, False, True), _
                txtdependentname1.Text, txtdepage1.Text, txtdeppass1.Text, txtdependentname2.Text, txtdepage2.Text, txtdeppass2.Text, Current_User.Employee_ID, ob_type)
        Return ref_no
    End Function

    Private Sub insert_ob_details(ByVal pref_no As String)
        Dim numInterval As Long = DateDiff(DateInterval.Day, Convert.ToDateTime(txtdatefrom.Text), Convert.ToDateTime(txtdateto.Text))
        Dim vDate As Date = Convert.ToDateTime(txtdatefrom.Text)
        ws.Insert_OB_Details(pref_no, vDate)
        For i As Integer = 1 To numInterval
            Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDate)
            ws.Insert_OB_Details(pref_no, vNewDate)
            vDate = vNewDate
        Next
    End Sub

    Private Sub insert_ob_transportation(ByVal pref_no As String)
        For i As Integer = 0 To GridView1.Rows.Count - 1
            ws.Insert_OB_Transportation(pref_no, _
                                        GridView1.Rows(i).Cells(1).Text, _
                                        GridView1.Rows(i).Cells(2).Text, _
                                        GridView1.Rows(i).Cells(3).Text, _
                                        GridView1.Rows(i).Cells(5).Text, _
                                        GridView1.Rows(i).Cells(6).Text, _
                                        GridView1.Rows(i).Cells(7).Text, _
                                        GridView1.Rows(i).Cells(8).Text, _
                                        GridView1.Rows(i).Cells(4).Text)
        Next
    End Sub

    Private Sub insert_ob_lodging(ByVal pref_no As String)
        For i As Integer = 0 To gv_lodging.Rows.Count - 1
            ws.Insert_OB_Lodging(pref_no, _
                                        gv_lodging.Rows(i).Cells(1).Text, _
                                        gv_lodging.Rows(i).Cells(2).Text, _
                                        gv_lodging.Rows(i).Cells(3).Text, _
                                        gv_lodging.Rows(i).Cells(4).Text)
        Next
    End Sub

    Private Sub insert_ob_cash_advance(ByVal pref_no As String)
        For i As Integer = 0 To gv_cash_advance.Rows.Count - 1
            ws.Insert_OB_CashAdvance(pref_no, gv_cash_advance.Rows(i).Cells(1).Text, gv_cash_advance.Rows(i).Cells(2).Text)
        Next
    End Sub

#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub get_ob_details(ByVal ref_no As String)
        Try

        Dim ds As DataSet
            ds = ws.Get_OB_Details(ref_no)
            Dim drHeader As DataRow = ds.Tables(0).Rows(0)

            GridView1.DataSource = ds.Tables(1)
            GridView1.DataBind()

            gv_lodging.DataSource = ds.Tables(2)
            gv_lodging.DataBind()

            gv_cash_advance.DataSource = ds.Tables(3)
            gv_cash_advance.DataBind()


            With drHeader
                Lbl_name.Text = drHeader("employee_name")
                lbl_emp_id.Text = drHeader("employee_id")
                lbl_ref_no.Text = drHeader("ref_no")
                lbl_date_filed.Text = drHeader("date_created")
                lbl_status.Text = drHeader("status")

                RadioButton_RegularOB.Checked = IIf(drHeader("ob_type") = True, True, False)
                RadioButton_CrossPostingOB.Checked = IIf(drHeader("ob_type") = False, True, False)

                dplEmployee.SelectedItem.Text = drHeader("employee_name")
                dplEmployee.SelectedValue = drHeader("employee_id")

                dplEmployee.Enabled = False

                dpltravel_type.SelectedValue = drHeader("travel_type")
                txtpurposeoftravel.Text = drHeader("purpose_of_travel")
                txtdatefrom.Text = drHeader("datefrom")
                txtdateto.Text = drHeader("dateto")
                txtdestination.Text = drHeader("destination")

                txtcontactno.Text = drHeader("contact_no")
                txtcostcenter.Text = drHeader("cost_center")
                txtposition.Text = drHeader("position")

                r_visa_yes.Checked = IIf(drHeader("visa_required") = "Yes", True, False)
                r_visa_no.Checked = IIf(drHeader("visa_required") = "Yes", False, True)
                txtcontactemergency.Text = drHeader("emergency_contact")
                txtpassport.Text = drHeader("passport_no")
                txtphone.Text = drHeader("phone_no")
                r_dependent_no.Checked = IIf(drHeader("accompanying_dependents") = "Yes", False, True)
                r_dependent_yes.Checked = IIf(drHeader("accompanying_dependents") = "Yes", True, False)
                txtdependentname1.Text = drHeader("dependent_name1")
                txtdepage1.Text = drHeader("dependent_age1")
                txtdeppass1.Text = drHeader("dependent_passport1")
                txtdependentname2.Text = drHeader("dependent_name2")
                txtdepage2.Text = drHeader("dependent_age2")
                txtdeppass2.Text = drHeader("dependent_passport2")
            End With


            If gv_cash_advance.Rows.Count > 0 Then
                Dim total As Decimal = 0.0

                For i As Integer = 0 To gv_cash_advance.Rows.Count - 1
                    total = total + CType(gv_cash_advance.Rows(i).Cells(2).Text, Decimal)
                Next

                gv_cash_advance.FooterRow.Cells(1).Text = "Total"
                gv_cash_advance.FooterRow.Cells(1).HorizontalAlign = HorizontalAlign.Right
                gv_cash_advance.FooterRow.Cells(2).Style("font-weight") = "bold"
                gv_cash_advance.FooterRow.Cells(2).Text = String.Format("{0:N2}", total)

            End If


        Catch ex As Exception
            usermsgbox(ex.Message)
        End Try


    End Sub

    Private Sub get_some_field(ByVal employee_id As String)
        Dim dr As DataRow
        dr = ws.Get_User_Info(employee_id).Tables(0).Rows(0)
        With dr
            txtcontactno.Text = dr("contactno")
            txtcostcenter.Text = dr("cost_center")
            txtposition.Text = dr("position")
        End With
    End Sub

    Private Sub get_employee_list()
        dplEmployee.DataSource = ws.Get_User_List(Current_User.Employee_ID)
        dplEmployee.DataValueField = "employee_id"
        dplEmployee.DataTextField = "employee_name"
        dplEmployee.DataBind()
        dplEmployee.Items.Insert(0, Current_User.LastName & " " & Current_User.FirstName)
        If ws.Validate_Planner(Current_User.Employee_ID) = False Then
            dplEmployee.Enabled = False
        End If
        get_some_field(Current_User.Employee_ID)
    End Sub

#End Region

#Region "Delete"

    Private Sub delete_gridview_row(ByVal intRow As Integer)
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("vehicle_type"))
        dt.Columns.Add(New DataColumn("date"))
        dt.Columns.Add(New DataColumn("destination"))
        dt.Columns.Add(New DataColumn("airline_code"))
        dt.Columns.Add(New DataColumn("flight_vessel"))
        dt.Columns.Add(New DataColumn("etd"))
        dt.Columns.Add(New DataColumn("eta"))
        dt.Columns.Add(New DataColumn("pickup_time"))
        For i As Integer = 0 To GridView1.Rows.Count - 1
            If Not i = intRow Then
                dr = dt.NewRow()
                dr("vehicle_type") = GridView1.Rows(i).Cells(1).Text
                dr("date") = GridView1.Rows(i).Cells(2).Text
                dr("destination") = GridView1.Rows(i).Cells(3).Text
                dr("airline_code") = GridView1.Rows(i).Cells(4).Text
                dr("flight_vessel") = GridView1.Rows(i).Cells(5).Text
                dr("etd") = GridView1.Rows(i).Cells(6).Text
                dr("eta") = GridView1.Rows(i).Cells(7).Text
                dr("pickup_time") = GridView1.Rows(i).Cells(8).Text
                dt.Rows.Add(dr)
            End If
        Next
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Private Sub delete_gridview_row_cash_advance(ByVal intRow As Integer)
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("purpose"))
        dt.Columns.Add(New DataColumn("amount"))

        Dim total As Double = 0.0

        For i As Integer = 0 To gv_cash_advance.Rows.Count - 1
            If Not i = intRow Then
                dr = dt.NewRow()
                dr("purpose") = gv_cash_advance.Rows(i).Cells(1).Text
                dr("amount") = gv_cash_advance.Rows(i).Cells(2).Text
                dt.Rows.Add(dr)
                total = total + CType(gv_cash_advance.Rows(i).Cells(2).Text, Double)
            End If
        Next
        gv_cash_advance.DataSource = dt
        gv_cash_advance.DataBind()
        gv_cash_advance.FooterRow.Cells(1).Text = "Total"
        gv_cash_advance.FooterRow.Cells(1).HorizontalAlign = HorizontalAlign.Right
        gv_cash_advance.FooterRow.Cells(2).Style("font-weight") = "bold"
        gv_cash_advance.FooterRow.Cells(2).Text = String.Format("{0:N2}", total)
    End Sub

    Private Sub delete_gv_lodging_row(ByVal intRow As Integer)
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("accommodation_type"))
        dt.Columns.Add(New DataColumn("preferred_hotel"))
        dt.Columns.Add(New DataColumn("check_in"))
        dt.Columns.Add(New DataColumn("check_out"))
        For i As Integer = 0 To gv_lodging.Rows.Count - 1
            If Not i = intRow Then
                dr = dt.NewRow()
                dr("accommodation_type") = gv_lodging.Rows(i).Cells(1).Text
                dr("preferred_hotel") = gv_lodging.Rows(i).Cells(2).Text
                dr("check_in") = gv_lodging.Rows(i).Cells(3).Text
                dr("check_out") = gv_lodging.Rows(i).Cells(4).Text
                dt.Rows.Add(dr)
            End If
        Next
        gv_lodging.DataSource = dt
        gv_lodging.DataBind()
    End Sub

#End Region

#Region "Manage"

    Private Sub clear_fields()
        dpl_vehicle.SelectedIndex = 0
        txttranspodate.Text = ""
        txtdestinationfrom.Text = ""
        txtdestinationto.Text = ""
        txtvessel.Text = ""
        txtetd.Text = ""
        txteta.Text = ""
        txtpickup.Text = ""
        txt_cash_purpose.Text = ""
        txt_cash_amount.Text = ""
        dplaccomodationtype.SelectedIndex = 0
        txtcheckin.Text = ""
        txtcheckout.Text = ""
        txtpreferredhotel.Text = ""
    End Sub

    Private Sub send_notifications(ByVal ref_no As String, ByVal employee_name As String)
        Dim body As String
        Dim recipients As String = ""
        body = employee_name & " is applying for " & " OFFICIAL BUSINESS/TRAVEL ORDER. For your approval, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        Dim ds As DataSet, level As String, r_level As String

        r_level = ws.Get_approvers_level(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), Current_User.Employee_ID)

        If r_level = "FINAL" Or r_level = "INITIAL" Then
            level = "%"
            '        recipients = ws.Get_users_Email_byRefno(ref_no)
        ElseIf r_level = "USER_FINAL" Then
            level = "FINAL"
        Else
            level = "INITIAL"
        End If

        
        'ds = ws.Get_Approvers_Email(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), level)
        ''Dim ds_itself As DataSet = ws.Get_Users_Email(Current_User.Employee_ID)
        'Dim dr_itself As DataRow = ds_itself.Tables(0).Rows(0)

        'recipients = ws.Get_users_Email_byRefno(ref_no)
        'If dr_itself("email") = recipients Then
        '    recipients = ""
        'End If

        'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '    Dim dr As DataRow = ds.Tables(0).Rows(i)
        '    If recipients = "" Then
        '        If dr("email") <> dr_itself("email") Then
        '            recipients = dr("email")
        '            Insert_Email_Summary_Notification(dr("email"), body, employee_name)
        '        End If
        '    Else
        '        If dr("email") <> dr_itself("email") Then
        '            recipients = recipients & ", " & dr("email")
        '            Insert_Email_Summary_Notification(dr("email"), body, employee_name)
        '        End If
        '    End If
        'Next
        Dim ds1 As DataSet

        If r_level = "INITIAL" Then
            ds = ws.Get_Approvers_Email(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), "FINAL")
            Dim ds_itself As DataSet = ws.Get_Users_Email(Current_User.Employee_ID)
            Dim dr_itself As DataRow = ds_itself.Tables(0).Rows(0)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim dr As DataRow = ds.Tables(0).Rows(i)
                If dr("email") <> dr_itself("email") Then
                    Insert_Email_Summary_Notification(dr("email"), body, employee_name)
                End If
            Next
        Else
            If r_level = "" Then
                ds1 = ws.Get_Approvers_Email(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), "INITIAL")

                If ds1.Tables(0).Rows.Count > 0 Then
                    ds = ws.Get_Approvers_Email(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), "INITIAL")
                Else
                    ds = ws.Get_Approvers_Email(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), "FINAL")
                End If
            Else
                ds = ws.Get_Approvers_Email(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), level)
            End If


            Dim ds_itself As DataSet = ws.Get_Users_Email(Current_User.Employee_ID)
            Dim dr_itself As DataRow = ds_itself.Tables(0).Rows(0)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim dr As DataRow = ds.Tables(0).Rows(i)
                If dr("email") <> dr_itself("email") Then
                    Insert_Email_Summary_Notification(dr("email"), body, employee_name)
                End If
            Next
        End If
        '        txtdestination.Text = recipients
        'cls_email.SendEmail(recipients, body, employee_name)
    End Sub

#End Region

#Region "Javascripts"

    Private Sub UserMsgBox(ByVal sMsg As String)
        Dim sb As New StringBuilder
        Dim oFormObject As New System.Web.UI.Control
        sMsg = sMsg.Replace("'", "\'")
        sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
        sMsg = sMsg.Replace(vbCrLf, "\n")
        sMsg = "<script language=javascript>alert(""" & sMsg & """)</script>"
        sb = New StringBuilder
        sb.Append(sMsg)
        For Each oFormObject In Me.Controls
            If TypeOf oFormObject Is HtmlForm Then
                Exit For
            End If
        Next
        oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
    End Sub

#End Region

    <System.Web.Services.WebMethodAttribute()> <System.Web.Script.Services.ScriptMethodAttribute()> Public Shared Function GetDynamicContent(ByVal contextKey As System.String) As System.String
        Return 1
    End Function

    Protected Sub dpltravel_type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpltravel_type.SelectedIndexChanged
        If dpltravel_type.SelectedValue = "Foreign" Then
            'RequiredFieldValidator30.Enabled = True
            'RequiredFieldValidator31.Enabled = True
            'RequiredFieldValidator32.Enabled = True
            'Added by Andrew 11-4-2011 Start
            txtcontactemergency.BackColor = Drawing.Color.Yellow
            txtpassport.BackColor = Drawing.Color.Yellow
            txtphone.BackColor = Drawing.Color.Yellow
            If r_dependent_yes.Checked = True Then
                txtdependentname1.BackColor = Drawing.Color.Yellow
                txtdepage1.BackColor = Drawing.Color.Yellow
                txtdeppass1.BackColor = Drawing.Color.Yellow
            Else
                txtdependentname1.BackColor = Drawing.Color.White
                txtdepage1.BackColor = Drawing.Color.White
                txtdeppass1.BackColor = Drawing.Color.White
            End If
            'Added by Andrew 11-4-2011 End
        Else
            'RequiredFieldValidator30.Enabled = False
            'RequiredFieldValidator31.Enabled = False
            'RequiredFieldValidator32.Enabled = False
            'Added by Andrew 11-4-2011 Start
            txtcontactemergency.BackColor = Drawing.Color.White
            txtpassport.BackColor = Drawing.Color.White
            txtphone.BackColor = Drawing.Color.White
            'Added by Andrew 11-4-2011 End
        End If
    End Sub

    Protected Sub dpl_vehicle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpl_vehicle.SelectedIndexChanged
        If dpl_vehicle.SelectedValue = "Air - Economy Class" Then
            airline_code.Visible = True
        ElseIf dpl_vehicle.SelectedValue = "Air - Business Class" Then
            airline_code.Visible = True
        Else
            airline_code.Visible = False
        End If
    End Sub

    Protected Sub dplaccomodationtype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dplaccomodationtype.SelectedIndexChanged
        If dplaccomodationtype.SelectedValue = "Hotel" Then
            CityCode_DropDownList.Enabled = True
            txtpreferredhotel.Visible = True
        Else
            CityCode_DropDownList.Enabled = False
            txtpreferredhotel.Text = ""
            txtpreferredhotel.Visible = False

        End If
    End Sub

    Protected Sub gv_cash_advance_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_cash_advance.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Cells(2).Text = String.Format("{0:N2}", Convert.ToDecimal(e.Row.Cells(2).Text))
        End If
    End Sub
End Class


