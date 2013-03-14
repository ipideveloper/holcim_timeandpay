Imports System.Data
Imports cls_Email_Notifications
Imports HolcimDbClass
Imports System.Data.SqlClient

Partial Class leaveeditapplications
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
    Private Calendar As Boolean
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

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If ViewState("pressed") = "" Then
                ViewState("pressed") = "True"

                btnSubmit.Enabled = False
                If txt_credit.Text = "" Then
                    txt_credit.Text = "0"
                End If

                If lbl_days_hidden.Text = "" Then
                    lbl_days_hidden.Text = "0"
                End If

                For i As Integer = 0 To gv_dates.Rows.Count - 1
                    Dim row As GridViewRow = gv_dates.Rows(i)
                    Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
                    Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
                    Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
                    If ws.Validate_Duplicate_Leave(Session("RefNo"), Current_User.Employee_ID, gv_dates.Rows(i).Cells(1).Text, half.Checked, first_half.Checked, second_half.Checked) = True Then
                        btnSubmit.Enabled = True
                        UserMsgBox("You have filed the same date with your previous application.")
                        ViewState("pressed") = ""
                        Exit Sub
                    End If
                Next

                If dplType.SelectedValue = "" Then
                    UserMsgBox("Please select leave type")
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                If dplType.SelectedItem.Text = "Vacation Leave" Or dplType.SelectedItem.Text = "Sick Leave" Then
                    If Not CType(txt_credit.Text, Double) <= 0 Then
                        If CType(lbl_days_hidden.Text, Double) > CType(txt_credit.Text, Double) Then
                            Dim wopay As Double = CType(lbl_days_hidden.Text, Double) - CType(txt_credit.Text, Double)

                            UserMsgBox("Your application exceeds the maximum number of credits. " & CType(wopay, String) & " without pay")
                            'Exit Sub
                            '     MsgBox("Your application exceeds the maximum number of credits. " & CType(wopay, String) & " without pay",MsgBoxStyle.OkOnly)
                        End If
                    Else
                        Dim wopay As Double
                        Dim sMsg As String
                        wopay = CType(lbl_days_hidden.Text, Double) - CType(txt_credit.Text, Double)
                        sMsg = "Your application exceeds the maximum number of credits. (" & wopay & ") without pay"
                        'sMsg = "test"
                        UserMsgBox(sMsg)
                    End If
                Else
                    If dplType.SelectedItem.Text = "Maternity Leave without pay" Then
                        If CType(lbl_days_hidden.Text, Double) > CType(txt_credit.Text, Double) Then
                            'If Not CType(lbl_days_hidden.Text, Double) = 60 And Not CType(lbl_days_hidden.Text, Double) = 78 Then
                            'UserMsgBox("Total days applied must be equal to 60 or 78 calendar days!")
                            UserMsgBox("Your application exceeds the maximum Maternity Leave credits of 60 or 78 days.")
                            btnSubmit.Enabled = True
                            ViewState("pressed") = ""
                            Exit Sub
                        End If
                    ElseIf dplType.SelectedItem.Text = "Paternity Leave" Then
                        If CType(lbl_days_hidden.Text, Double) > 7 Then
                            UserMsgBox("Your application exceeds the maximum Paternity Leave credits of 7 days.")
                            btnSubmit.Enabled = True
                            ViewState("pressed") = ""
                            Exit Sub
                        End If
                    ElseIf dplType.SelectedItem.Text = "Bereavement Leave" Then
                        If CType(lbl_days_hidden.Text, Double) > 7 Then
                            UserMsgBox("Your application exceeds the maximum Bereavement Leave credits of 7 days.")
                            btnSubmit.Enabled = True
                            ViewState("pressed") = ""
                            Exit Sub
                        End If
                    ElseIf dplType.SelectedItem.Text = "Blood Donor Leave" Then
                        If CType(lbl_days_hidden.Text, Double) > 1 Then
                            UserMsgBox("Your application exceeds the maximum Blood Donor Leave credits of 1 day.")
                            btnSubmit.Enabled = True
                            ViewState("pressed") = ""
                            Exit Sub
                        End If
                    ElseIf dplType.SelectedItem.Text = "Calamity/Emergency Leave" Then
                        If CType(lbl_days_hidden.Text, Double) > 3 Then
                            UserMsgBox("Your application exceeds the maximum Calamity Leave credits of 3 days.")
                            btnSubmit.Enabled = True
                            ViewState("pressed") = ""
                            Exit Sub
                        End If

                    Else
                        If Not txt_credit.Text = "N/A" Then
                            If CType(lbl_days_hidden.Text, Double) > CType(txt_credit.Text, Double) Then
                                '    Dim wopay As Double = CType(lbl_days_hidden.Text, Double) - CType(txt_credit.Text, Double)
                                UserMsgBox("Your application exceeds the maximum number of credits. ")  '& CType(wopay, String) & " without pay")
                            End If
                        End If
                    End If
                End If

                Dim ref_no As String = Edit_Leave_Header()

                Insert_Leave_Details(ref_no)
                ws.Insert_Employees_Approvers(Current_User.Employee_ID, ref_no)
                ws.Update_Approve_Applications(ref_no, Current_User.Employee_ID, "LV")
                send_notifications(ref_no)
                ViewState("pressed") = ""
                Session("Employee_ID") = ViewState("Employee_ID")
                btnSubmit.Enabled = True
                Response.Redirect(Session("Return_Leave")) '"my_app_status.aspx")
            End If
        Catch ex As Exception
            btnSubmit.Enabled = True
            UserMsgBox(ex.Message)
            ViewState("pressed") = ""
        End Try
    End Sub

    Protected Sub dplType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            get_leave_available()
            check_classification()
            Session("Calendar") = Calendar
        Catch ex As Exception
        End Try
    End Sub

    Private Sub check_classification()
        Calendar = ws.Get_Leave_Classification(dplType.SelectedValue)

        btnSubmit.Visible = False
    End Sub

    Protected Sub btn_bind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_bind.Click
        clickeditLabel.Visible = False
        Try
            Dim v_from As String = txtStart.Text
            Dim v_to As String = txtTo.Text

            If CType(v_to, Date) < CType(v_from, Date) Then
                UserMsgBox("Invalid Entry! Date To should not be earlier than Date From.")
                Exit Sub
            End If

            bind_dates2()
            sortgrid()

        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub gv_dates_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_dates.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "cmd_delete" Then
            delete_dates(gv_dates.Rows(index).Cells(1).Text)
        End If
    End Sub

    Protected Sub gv_dates_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_dates.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnk_delete"), LinkButton)
            lnkDelete.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub chk_half_CheckedChanged2(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim total_days As Double = 0.0

        For i As Integer = 0 To gv_dates.Rows.Count - 1
            Dim row As GridViewRow = gv_dates.Rows(i)

            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

            If gv_dates.Rows(i).Cells(4).Text <> "Restday" Then
                If half.Checked = True Then
                    If first_half.Checked = False And second_half.Checked = False Then
                        first_half.Checked = True
                        second_half.Checked = False
                    Else
                        If first_half.Checked = True Then
                            first_half.Checked = True
                        Else
                            second_half.Checked = True
                        End If
                    End If

                    first_half.Enabled = True
                    second_half.Enabled = True
                    If holiday.Checked = False Then
                        total_days = total_days + 0.5
                    End If
                Else
                    first_half.Checked = False
                    second_half.Checked = False
                    first_half.Enabled = False
                    second_half.Enabled = False
                    If holiday.Checked = False Then
                        total_days = total_days + 1.0
                    End If
                End If
            End If
        Next
        lbl_total_days.Text = "Total Days Applied = " & total_days
        lbl_days_hidden.Text = total_days
    End Sub

    Protected Sub chk_half_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim total_days As Double = 0.0
        Dim daysCounter As Double = 1
        Dim daysAvailble As Double = CType(txt_credit.Text, Double)

        For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
            Dim row As GridViewRow = gv_dates_edit.Rows(i)
            'gv_dates.Rows(i).Cells(1).Text
        Next

        For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
            Dim row As GridViewRow = gv_dates_edit.Rows(i)

            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

            'If half.Enabled = True Then
            If gv_dates_edit.Rows(i).Cells(4).Text <> "Restday" Then
                If half.Checked = True Then
                    If first_half.Checked = False And second_half.Checked = False Then
                        first_half.Checked = True
                        second_half.Checked = False
                    Else
                        If first_half.Checked = True Then
                            first_half.Checked = True
                        Else
                            second_half.Checked = True
                        End If
                    End If

                    first_half.Enabled = True
                    second_half.Enabled = True
                    If holiday.Checked = False Then
                        total_days = total_days + 0.5
                    End If
                Else
                    first_half.Checked = False
                    second_half.Checked = False
                    first_half.Enabled = False
                    second_half.Enabled = False
                    If holiday.Checked = False Then
                        total_days = total_days + 1.0
                    End If
                End If
            End If

            'End If

            Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)

            If gv_dates_edit.Rows(i).Cells(4).Text <> "Restday" And holiday.Checked = False Then
                If half.Checked = True Then
                    daysCounter = daysCounter + 0.5
                    'total_days = total_days + 0.5
                Else
                    daysCounter = daysCounter + 1
                    'total_days = total_days + 1.0
                End If
            End If

            without_pay.Checked = False
            If holiday.Checked = False Then
                If dplType.SelectedItem.Text = "Vacation Leave" Then
                    If ws.Get_Leave_Available(Current_User.Employee_ID, "01") <= 0 Then
                        without_pay.Checked = True
                    Else
                        If daysCounter > daysAvailble Then
                            without_pay.Checked = True
                        End If
                    End If
                ElseIf dplType.SelectedItem.Text = "Sick Leave" Then
                    If ws.Get_Leave_Available(Current_User.Employee_ID, "02") <= 0 Then
                        without_pay.Checked = True
                    Else
                        If daysCounter > daysAvailble Then
                            without_pay.Checked = True
                        End If
                    End If
                End If
            End If


            'If gv_dates.Rows(0).Cells(1).Text = "04-Dec-2012" And half.Checked = True Then

            'End If



        Next



        lbl_total_days.Text = "Total Days Applied = " & total_days
        lbl_days_hidden.Text = total_days


    End Sub


#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        bind_leaves()
        get_leave_header_details(Session("RefNo"))
        get_leave_detail_dates(Session("RefNo"))
        get_leave_available()
        'bind_dates()

    End Sub

#End Region

#Region "Validation"

    Private Function validate_applied_days() As Boolean


    End Function

#End Region

#Region "Edit"

    Private Function Edit_Leave_Header() As String

        Dim numInterval As Decimal = 0.0
        For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
            Dim row As GridViewRow = gv_dates_edit.Rows(i)
            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

            If gv_dates_edit.Rows(i).Cells(4).Text = "-" Then
                If holiday.Checked = False Then
                    If half.Checked = True Then
                        numInterval = numInterval + 0.5
                    Else
                        numInterval = numInterval + 1.0
                    End If
                End If
            End If
        Next

        Dim start_date As Date = txtStart.Text 'gv_dates.Rows(0).Cells(1).Text
        Dim to_date As Date = txtTo.Text  'gv_dates.Rows(gv_dates.Rows.Count - 1).Cells(1).Text

        Dim leave_type As String = ""

        leave_type = dplType.SelectedValue

        Return ws.Edit_Leave_Header(Session("RefNo"), leave_type, start_date, to_date, numInterval, txtReason.Text, Current_User.Employee_ID, Current_User.Employee_ID)


    End Function

    Private Sub Insert_Leave_Details(ByVal pref_no As String)
        For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
            Dim row As GridViewRow = gv_dates_edit.Rows(i)
            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

            Dim without_pay As Boolean = False

            If holiday.Checked = False Then
                If dplType.SelectedItem.Text = "Vacation Leave" Then
                    If ws.Get_Leave_Available(Current_User.Employee_ID, "01") <= 0 Then
                        without_pay = True
                    End If
                ElseIf dplType.SelectedItem.Text = "Sick Leave" Then
                    If ws.Get_Leave_Available(Current_User.Employee_ID, "02") <= 0 Then
                        without_pay = True
                    End If
                End If
            End If

            If gv_dates_edit.Rows(i).Cells(4).Text <> "Restday" Then
                ws.Insert_Leave_Details(pref_no, gv_dates_edit.Rows(i).Cells(1).Text, half.Checked, IIf(first_half.Checked = True, True, False), IIf(second_half.Checked = True, True, False), without_pay)
            End If

        Next
    End Sub

#End Region



#Region "Retrieve"

    Private Sub get_leave_header_details(ByVal ref_no As String)

        Dim dr As DataRow = ws.Get_Leave_Details(ref_no).Tables(0).Rows(0)
        With dr
            Lbl_name.Text = dr("employee_name")
            lbl_emp_id.Text = dr("employee_id")
            lbl_ref_no.Text = dr("ref_no")
            'dplType.SelectedItem.Text = dr("application_type")
            'dplType.SelectedItem.Value = dr("application_typeID").ToString
            'dplType.SelectedValue = dr("application_typeID").ToString
            dplType.Items.FindByText(dr("application_type")).Selected = True

            txtStart.Text = dr("date_from")
            txtTo.Text = dr("date_to")
            lbl_total_days.Text = "Total Days Applied = " & CType(dr("days"), Decimal)
            lbl_date_filed.Text = dr("date_created")
            txtReason.Text = dr("reason")
            lbl_status.Text = dr("status")
            'UserMsgBox(dr("application_typeID"))

        End With

    End Sub

    Private Sub get_leave_detail_dates(ByVal ref_no As String)
        gv_dates.DataSource = ws.Get_Leave_Details_Dates(ref_no)
        gv_dates.DataBind()

        Dim leaveavailable As Decimal = ws.Get_Leave_Available(Current_User.Employee_ID, dplType.SelectedValue)
        Dim dr As DataRow = ws.Get_Leave_Details(Session("RefNo")).Tables(0).Rows(0)

        Dim leaveapplied As Decimal = 0.0
        Dim leavetype As String = ""
        leavetype = dr("application_type")

        If leavetype = dplType.SelectedItem.Text Then
            leaveapplied = CType(dr("days"), Decimal)
        End If

        Dim leavecredit As Decimal
        leavecredit = leaveapplied + leaveavailable

        For i As Integer = 0 To gv_dates.Rows.Count - 1
            Dim row As GridViewRow = gv_dates.Rows(i)
            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)

            'If gv_dates.Rows(i).Cells(1).Text = gv_dates.Rows(i).Cells(1).Text Then


            If leavecredit = 0.5 Then
                If half.Checked = True Then
                    half.Enabled = False
                Else
                    half.Enabled = True
                End If
            Else

            End If

            half.Enabled = False
            'End If

        Next
    End Sub

    Private Sub bind_leaves()

        Dim ds As DataSet
        ds = ws.Leave_selection(Current_User.Employee_ID)
        dplType.DataSource = ds
        dplType.DataValueField = "leave_type"
        dplType.DataTextField = "description"
        dplType.DataBind()



    End Sub

    Private Sub get_leave_available()
        Dim leaveavailable As Decimal = ws.Get_Leave_Available(Current_User.Employee_ID, dplType.SelectedValue)

        Dim dr As DataRow = ws.Get_Leave_Details(Session("RefNo")).Tables(0).Rows(0)

        Dim leaveapplied As Decimal = 0
        Dim leavetype As String = ""

        leavetype = dr("application_type")
        If leavetype = dplType.SelectedItem.Text Then
            leaveapplied = CType(dr("days"), Decimal)
        End If

        'UserMsgBox(dplType.SelectedItem.Text)


        If leaveavailable = 9999 Then
            txt_credit.Text = "N/A"
        Else
            txt_credit.Text = leaveavailable + leaveapplied
        End If

        btnSubmit.Visible = False
    End Sub

#End Region

#Region "Delete"

    'Private Sub delete_dates(ByVal strdate As String)

    '    Dim total_days As Double = 0.0
    '    Dim dt As New DataTable
    '    Dim dr As DataRow
    '    dt.Columns.Add(New DataColumn("date"))
    '    dt.Columns.Add(New DataColumn("half_day"))
    '    dt.Columns.Add(New DataColumn("first_half"))
    '    dt.Columns.Add(New DataColumn("second_half"))
    '    dt.Columns.Add(New DataColumn("date_status"))
    '    dt.Columns.Add(New DataColumn("chk_holiday"))

    '    Dim allrestday As Boolean = True

    '    For i As Integer = 0 To gv_dates.Rows.Count - 1

    '        If Not strdate = gv_dates.Rows(i).Cells(1).Text Then
    '            Dim row As GridViewRow = gv_dates.Rows(i)

    '            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
    '            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
    '            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
    '            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

    '            dr = dt.NewRow()
    '            dr("date") = gv_dates.Rows(i).Cells(1).Text
    '            dr("half_day") = half.Checked
    '            dr("first_half") = first_half.Checked
    '            dr("second_half") = second_half.Checked
    '            dr("date_status") = gv_dates.Rows(i).Cells(4).Text
    '            dr("chk_holiday") = holiday.Checked

    '            dt.Rows.Add(dr)

    '            If gv_dates.Rows(i).Cells(4).Text = "-" Then
    '                If holiday.Checked = False Then
    '                    If half.Checked = True Then
    '                        total_days = total_days + 0.5
    '                    Else
    '                        total_days = total_days + 1.0
    '                    End If
    '                End If
    '                allrestday = False
    '            End If

    '        End If

    '    Next

    '    gv_dates.DataSource = dt
    '    gv_dates.DataBind()

    '    For i As Integer = 0 To gv_dates.Rows.Count - 1
    '        Dim row As GridViewRow = gv_dates.Rows(i)
    '        Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
    '        Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
    '        Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
    '        Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

    '        half.Checked = dt.Rows(i).Item("half_day")
    '        first_half.Checked = dt.Rows(i).Item("first_half")
    '        second_half.Checked = dt.Rows(i).Item("second_half")
    '        holiday.Checked = dt.Rows(i).Item("chk_holiday")
    '        gv_dates.Rows(i).Cells(4).Text = dt.Rows(i).Item("date_status")

    '        If half.Checked = True Then
    '            first_half.Enabled = True
    '            second_half.Enabled = True
    '            '  total_days = total_days + 0.5
    '        Else
    '            ' total_days = total_days + 1.0
    '        End If

    '        'If Not gv_dates.Rows(i).Cells(4).Text = "-" Then
    '        'total_days = total_days - 1
    '        'half.Enabled = False
    '        'allrestday = False
    '        'ElseIf _holiday = True Then
    '        'total_days = total_days - 1
    '        'half.Enabled = False
    '        'End If

    '    Next

    '    'Dim vDates As Date = txtStart.Text
    '    'vDates = DateAdd(DateInterval.Day, -1, vDates)



    '    'For i As Integer = 0 To gv_dates.Rows.Count - 1
    '    'Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDates)
    '    'Dim _holiday As Boolean = ws.Validate_Holiday(Current_User.Employee_ID, vNewDate)
    '    'Dim row As GridViewRow = gv_dates.Rows(i)
    '    'Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
    '    'Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

    '    'If _holiday = True Then
    '    '   holiday.Checked = True
    '    'Else
    '    '   holiday.Checked = False
    '    'End If

    '    'If Not gv_dates.Rows(i).Cells(4).Text = "-" Then
    '    '  total_days = total_days - 1
    '    ' half.Enabled = False
    '    '   allrestday = False
    '    'ElseIf _holiday = True Then
    '    '    total_days = total_days - 1
    '    '     half.Enabled = False
    '    '  End If
    '    '   vDates = vNewDate
    '    'Next

    '    lbl_total_days.Text = "Total Days Applied = " & total_days
    '    lbl_days_hidden.Text = total_days

    '    ' If total_days <= 0 Then
    '    'btnSubmit.Visible = False
    '    ' Else


    '    If allrestday = True Then
    '        btnSubmit.Visible = False
    '    Else
    '        btnSubmit.Visible = True
    '    End If

    '    'End If

    'End Sub

    Private Sub delete_dates(ByVal strdate As String)

        Dim total_days As Double = 0.0
        Dim dt1 As New DataTable
        Dim dr1 As DataRow
        dt1.Columns.Add(New DataColumn("date"))
        dt1.Columns.Add(New DataColumn("half"))
        dt1.Columns.Add(New DataColumn("first_half"))
        dt1.Columns.Add(New DataColumn("second_half"))
        dt1.Columns.Add(New DataColumn("date_status"))
        dt1.Columns.Add(New DataColumn("holiday"))
        dt1.Columns.Add(New DataColumn("without_pay"))

        Dim allrestday As Boolean = True

        For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
            If Not strdate = gv_dates_edit.Rows(i).Cells(1).Text Then
                Dim row As GridViewRow = gv_dates_edit.Rows(i)
                Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
                Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
                Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
                Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)
                Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)


                dr1 = dt1.NewRow()
                dr1("date") = gv_dates_edit.Rows(i).Cells(1).Text
                dr1("half") = half.Checked
                dr1("first_half") = first_half.Checked
                dr1("second_half") = second_half.Checked
                dr1("date_status") = gv_dates_edit.Rows(i).Cells(4).Text
                dr1("holiday") = holiday.Checked
                dr1("without_pay") = without_pay.Checked
                dt1.Rows.Add(dr1)

                If gv_dates_edit.Rows(i).Cells(4).Text = "-" Then
                    If holiday.Checked = False Then
                        If half.Checked = True Then
                            total_days = total_days + 0.5
                        Else
                            total_days = total_days + 1.0
                        End If
                    End If
                    If half.Enabled = False Then
                        half.Enabled = False
                    ElseIf half.Enabled = True Then
                        half.Enabled = True

                    End If
                    allrestday = False
                End If

            End If

        Next

        gv_dates_edit.DataSource = dt1
        gv_dates_edit.DataBind()

        For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
            Dim row As GridViewRow = gv_dates_edit.Rows(i)
            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)
            Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)

            half.Checked = dt1.Rows(i).Item("half")
            first_half.Checked = dt1.Rows(i).Item("first_half")
            second_half.Checked = dt1.Rows(i).Item("second_half")
            holiday.Checked = dt1.Rows(i).Item("holiday")
            gv_dates_edit.Rows(i).Cells(4).Text = dt1.Rows(i).Item("date_status")
            without_pay.Checked = dt1.Rows(i).Item("without_pay")

            If half.Checked = True Then
                first_half.Enabled = True
                second_half.Enabled = True
                '  total_days = total_days + 0.5
            Else
                ' total_days = total_days + 1.0
            End If

            If half.Enabled = False Then
                half.Enabled = False
            ElseIf half.Enabled = True Then
                half.Enabled = True

            End If

            'If Not gv_dates.Rows(i).Cells(4).Text = "-" Then
            'total_days = total_days - 1
            'half.Enabled = False
            'allrestday = False
            'ElseIf _holiday = True Then
            'total_days = total_days - 1
            'half.Enabled = False
            'End If

        Next

        'Dim vDates As Date = txtStart.Text
        'vDates = DateAdd(DateInterval.Day, -1, vDates)



        'For i As Integer = 0 To gv_dates.Rows.Count - 1
        'Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDates)
        'Dim _holiday As Boolean = ws.Validate_Holiday(Current_User.Employee_ID, vNewDate)
        'Dim row As GridViewRow = gv_dates.Rows(i)
        'Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
        'Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

        'If _holiday = True Then
        '   holiday.Checked = True
        'Else
        '   holiday.Checked = False
        'End If

        'If Not gv_dates.Rows(i).Cells(4).Text = "-" Then
        '  total_days = total_days - 1
        ' half.Enabled = False
        '   allrestday = False
        'ElseIf _holiday = True Then
        '    total_days = total_days - 1
        '     half.Enabled = False
        '  End If
        '   vDates = vNewDate
        'Next

        lbl_total_days.Text = "Total Days Applied = " & total_days
        lbl_days_hidden.Text = total_days

        ' If total_days <= 0 Then
        'btnSubmit.Visible = False
        ' Else


        If allrestday = True Then
            btnSubmit.Visible = False
        Else
            btnSubmit.Visible = True
        End If

        'End If

    End Sub

    Private Sub delete_dates_index(ByVal index As Integer)


        Dim total_days As Double = 0.0
        Dim dt1 As New DataTable
        Dim dr1 As DataRow
        dt1.Columns.Add(New DataColumn("date"))
        dt1.Columns.Add(New DataColumn("half"))
        dt1.Columns.Add(New DataColumn("first_half"))
        dt1.Columns.Add(New DataColumn("second_half"))
        dt1.Columns.Add(New DataColumn("date_status"))
        dt1.Columns.Add(New DataColumn("holiday"))
        dt1.Columns.Add(New DataColumn("without_pay"))

        Dim allrestday As Boolean = True

        For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
            If Not gv_dates_edit.Rows(i).RowIndex = index Then
                Dim row As GridViewRow = gv_dates_edit.Rows(i)
                Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
                Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
                Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
                Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)
                Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)


                dr1 = dt1.NewRow()
                dr1("date") = gv_dates_edit.Rows(i).Cells(1).Text
                dr1("half") = half.Checked
                dr1("first_half") = first_half.Checked
                dr1("second_half") = second_half.Checked
                dr1("date_status") = gv_dates_edit.Rows(i).Cells(4).Text
                dr1("holiday") = holiday.Checked
                dr1("without_pay") = without_pay.Checked
                dt1.Rows.Add(dr1)

                If gv_dates_edit.Rows(i).Cells(4).Text = "-" Then
                    If holiday.Checked = False Then
                        If half.Checked = True Then
                            total_days = total_days + 0.5
                        Else
                            total_days = total_days + 1.0
                        End If
                    End If
                    If half.Enabled = False Then
                        half.Enabled = False
                    ElseIf half.Enabled = True Then
                        half.Enabled = True

                    End If
                    allrestday = False
                End If

            End If

        Next

        gv_dates_edit.DataSource = dt1
        gv_dates_edit.DataBind()

        For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
            Dim row As GridViewRow = gv_dates_edit.Rows(i)
            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)
            Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)

            half.Checked = dt1.Rows(i).Item("half")
            first_half.Checked = dt1.Rows(i).Item("first_half")
            second_half.Checked = dt1.Rows(i).Item("second_half")
            holiday.Checked = dt1.Rows(i).Item("holiday")
            gv_dates_edit.Rows(i).Cells(4).Text = dt1.Rows(i).Item("date_status")
            without_pay.Checked = dt1.Rows(i).Item("without_pay")

            If half.Checked = True Then
                first_half.Enabled = True
                second_half.Enabled = True
                '  total_days = total_days + 0.5
            Else
                ' total_days = total_days + 1.0
            End If

            If half.Enabled = False Then
                half.Enabled = False
            ElseIf half.Enabled = True Then
                half.Enabled = True

            End If



        Next


        lbl_total_days.Text = "Total Days Applied = " & total_days
        lbl_days_hidden.Text = total_days

        If allrestday = True Then
            btnSubmit.Visible = False
        Else
            btnSubmit.Visible = True
        End If



    End Sub
#End Region

#Region "Manage"
    Private Sub bind_dates2()
        gv_dates.Visible = False

        Dim dt1 As New DataTable
        Dim dr1 As DataRow
        Calendar = Session("Calendar")
        dt1.Columns.Add(New DataColumn("date"))
        dt1.Columns.Add(New DataColumn("date_status"))
        'dt1.Columns.Add(New DataColumn("ref_id"))

        Dim numInterval As Integer = DateDiff(DateInterval.Day, Convert.ToDateTime(txtStart.Text), Convert.ToDateTime(txtTo.Text))

        Dim vDate As Date = txtStart.Text
        dr1 = dt1.NewRow()
        dr1("date") = txtStart.Text

        If Calendar Then
            dr1("date_status") = "-"
        Else

            If ws.Validate_Restday(Current_User.Employee_ID, txtStart.Text) = True Then
                dr1("date_status") = "Restday"
            Else
                dr1("date_status") = "-"
            End If
        End If



        'If ws.Validate_Duplicate_Leave(Current_User.Employee_ID, txtStart.Text, True, False, False) = True Then
        '    dr("date_status") = "Duplicate"
        'Else
        '    dr("date_status") = "-"
        'End If
        dt1.Rows.Add(dr1)
        Dim allrestday As Boolean = True

        For i As Integer = 1 To numInterval
            Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDate)
            dr1 = dt1.NewRow()
            dr1("date") = Format(vNewDate, "dd-MMM-yyyy")

            If Calendar Then
                dr1("date_status") = "-"
            Else
                If ws.Validate_Restday(Current_User.Employee_ID, vNewDate) = True Then
                    dr1("date_status") = "Restday"
                Else
                    dr1("date_status") = "-"
                    allrestday = False
                End If
            End If
            'If ws.Validate_Duplicate_Leave(Current_User.Employee_ID, vNewDate, True, False, False) = True Then
            '    dr("date_status") = "Duplicate"
            'Else
            '    dr("date_status") = "-"
            'End If

            dt1.Rows.Add(dr1)
            vDate = vNewDate
        Next


        gv_dates_edit.DataSource = dt1
        gv_dates_edit.DataBind()

        Dim total_days As Double = numInterval + 1
        Dim vDates As Date = txtStart.Text
        vDates = DateAdd(DateInterval.Day, -1, vDates)

        Dim daysCounter As Double = 0
        Dim daysAvailble As Double = CType(txt_credit.Text, Double)

        Dim NewDate As Date = txtStart.Text
        Dim NeedNewDate As Date = "01/01/1900"
        Dim NeedNewRow As Boolean = False

        'UserMsgBox(daysAvailble)


        For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
            Dim count As Integer
            count = count + 1
            Dim row As GridViewRow = gv_dates_edit.Rows(i)
            Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDates)
            Dim _holiday As Boolean = ws.Validate_Holiday(Current_User.Employee_ID, vNewDate)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

            If _holiday = True Then
                holiday.Checked = True
            Else
                holiday.Checked = False
            End If

            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)

            second_half.Checked = False
            first_half.Checked = False
            half.Checked = False


            If Not gv_dates_edit.Rows(i).Cells(4).Text = "-" Then
                total_days = total_days - 1
                half.Enabled = False
            ElseIf _holiday = True Then
                total_days = total_days - 1
                'half.Enabled = False
            End If

            If gv_dates_edit.Rows(i).Cells(4).Text = "-" Then
                allrestday = False
            End If

            '--------------- modify by Art 20110904
            Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)





            If gv_dates_edit.Rows(i).Cells(4).Text <> "Restday" And holiday.Checked = False Then
                If half.Checked = True Then
                    daysCounter = daysCounter + 0.5
                Else
                    daysCounter = daysCounter + 1
                End If
            End If

            If holiday.Checked = False Then
                If dplType.SelectedItem.Text = "Vacation Leave" Then
                    If daysAvailble <= 0 Then
                        'If ws.Get_Leave_Available(Current_User.Employee_ID, "01") <= 0 Then
                        without_pay.Checked = True
                    Else
                        If daysCounter > daysAvailble Then
                            If (daysCounter - daysAvailble) = 0.5 Then
                                first_half.Checked = True
                                half.Checked = True
                                If NeedNewDate = "01/01/1900" Then
                                    NeedNewDate = NewDate
                                End If
                                without_pay.Checked = False
                                NeedNewRow = True
                            Else
                                without_pay.Checked = True
                            End If
                        End If
                    End If
                ElseIf dplType.SelectedItem.Text = "Sick Leave" Then
                    If ws.Get_Leave_Available(Current_User.Employee_ID, "02") <= 0 Then
                        without_pay.Checked = True
                    Else
                        If daysCounter > daysAvailble Then
                            If (daysCounter - daysAvailble) = 0.5 Then
                                half.Checked = True
                                first_half.Checked = True
                                without_pay.Checked = False
                                If NeedNewDate = "01/01/1900" Then
                                    NeedNewDate = NewDate
                                End If
                                NeedNewRow = True
                            Else
                                without_pay.Checked = True
                            End If

                        End If
                    End If
                End If
            Else
                daysAvailble = daysAvailble + 1 'added by Andrew 1-11-2011
            End If
            'Added code by Andrew 11-09-2011 Start
            If dplType.SelectedItem.Text = "Maternity Leave without pay" Then
                without_pay.Checked = True
            End If
            'Added code by Andrew 11-09-2011 End 
            '----------------------------------------------
            NewDate = DateAdd(DateInterval.Day, 1, NewDate)
            vDates = vNewDate


        Next

        'UserMsgBox(NeedNewRow.ToString)

        If NeedNewRow Then
            Dim newDT1 As New DataTable
            Dim newDR1 As DataRow

            newDT1 = gv_dates_edit.DataSource
            newDR1 = newDT1.NewRow()

            newDR1("date") = Format(NeedNewDate, "dd-MMM-yyyy")
            newDR1("date_status") = "-"
            newDT1.Rows.Add(newDR1)
            gv_dates_edit.DataSource = Nothing
            gv_dates_edit.DataBind()

            gv_dates_edit.DataSource = newDT1
            'gv_dates.Sort("date", SortDirection.Ascending)
            gv_dates_edit.DataBind()
            gv_dates.Sort("date", SortDirection.Ascending)
            '-------------------------------------------------------------

            total_days = numInterval + 1
            vDates = txtStart.Text
            vDates = DateAdd(DateInterval.Day, -1, vDates)
            daysCounter = 0
            daysAvailble = CType(txt_credit.Text, Double)


            Dim ctr As Integer = 1
            For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
                NewDate = gv_dates_edit.Rows(i).Cells(1).Text
                Dim row As GridViewRow = gv_dates_edit.Rows(i)
                Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDates)
                Dim _holiday As Boolean = ws.Validate_Holiday(Current_User.Employee_ID, vNewDate)
                Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

                If _holiday = True Then
                    holiday.Checked = True
                Else
                    holiday.Checked = False
                End If

                Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
                Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
                Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)

                second_half.Checked = False
                first_half.Checked = False
                half.Checked = False


                If Not gv_dates_edit.Rows(i).Cells(4).Text = "-" Then
                    total_days = total_days - 1
                    half.Enabled = False
                ElseIf _holiday = True Then
                    total_days = total_days - 1
                    'half.Enabled = False
                End If

                If gv_dates_edit.Rows(i).Cells(4).Text = "-" Then
                    allrestday = False
                End If

                '--------------- modify by Art 20110904
                Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)


                If gv_dates_edit.Rows(i).Cells(4).Text <> "Restday" And holiday.Checked = False Then
                    If half.Checked = True Then
                        daysCounter = daysCounter + 0.5
                    Else
                        daysCounter = daysCounter + 1
                    End If
                End If
                If holiday.Checked = False Then
                    If dplType.SelectedItem.Text = "Vacation Leave" Then
                        If ws.Get_Leave_Available(Current_User.Employee_ID, "01") <= 0 Then
                            without_pay.Checked = True
                        Else
                            If daysCounter > daysAvailble Then
                                If (daysCounter - daysAvailble) = 0.5 Then
                                    first_half.Checked = True
                                    half.Checked = True
                                    half.Enabled = False
                                    without_pay.Checked = False

                                Else
                                    without_pay.Checked = True
                                    'half.Enabled = False
                                End If


                            End If
                        End If
                    ElseIf dplType.SelectedItem.Text = "Sick Leave" Then
                        If ws.Get_Leave_Available(Current_User.Employee_ID, "02") <= 0 Then
                            without_pay.Checked = True
                        Else
                            If daysCounter > daysAvailble Then
                                If (daysCounter - daysAvailble) = 0.5 Then
                                    half.Checked = True
                                    first_half.Checked = True
                                    without_pay.Checked = False
                                    half.Enabled = False
                                Else
                                    without_pay.Checked = True
                                End If


                            End If
                        End If
                    End If
                End If

                If gv_dates_edit.Rows(i).Cells(4).Text = "Restday" Or holiday.Checked = True Then
                    half.Checked = False
                    first_half.Checked = False
                    second_half.Checked = False
                    without_pay.Checked = False
                End If

                If NewDate = NeedNewDate Then
                    If ctr = 1 Then
                        half.Checked = True
                        first_half.Checked = True
                        without_pay.Checked = False
                        ctr = ctr + 1
                    Else
                        half.Checked = True
                        first_half.Checked = False
                        second_half.Checked = True
                        without_pay.Checked = True
                    End If
                    half.Enabled = False
                End If

                '----------------------------------------------
                NewDate = DateAdd(DateInterval.Day, 1, NewDate)
                vDates = vNewDate
            Next

            '---------------------------------------------------------------

        End If


        
        'gv_dates_edit.Sort("Date", SortDirection.Descending)
        'gv_dates_edit.DataBind()





        lbl_total_days.Text = "Total Days Applied = " & total_days
        lbl_days_hidden.Text = total_days

        '' If total_days <= 0 Then
        ''btnSubmit.Visible = False
        ''Else
        'btnSubmit.Visible = True
        ''End If
        If allrestday = True Then
            btnSubmit.Visible = False
        Else
            btnSubmit.Visible = True
        End If

    End Sub

    Private Sub sortgrid()
        'gv_dates_edit.Sort("date", SortDirection.Ascending)
        ''Session["SortOrder"] = e.SortDirection.ToString();  // sort direction
        ''Session["SavedSort"] = e.SortExpression;            // field to sort by
        'gv_dates_edit.DataBind()


        'Dim dr As DataRow

        'dt.Columns.Add(New DataColumn("date"))
        'dt.Columns.Add(New DataColumn("half"))
        'dt.Columns.Add(New DataColumn("first_half"))
        'dt.Columns.Add(New DataColumn("second_half"))
        'dt.Columns.Add(New DataColumn("date_status"))
        'dt.Columns.Add(New DataColumn("holiday"))
        'dt.Columns.Add(New DataColumn("without_pay"))

        'Dim allrestday As Boolean = True

        'For i As Integer = 0 To gv_dates_edit.Rows.Count - 1
        '    Dim row As GridViewRow = gv_dates_edit.Rows(i)
        '    Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
        '    Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
        '    Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
        '    Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)
        '    Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)

        '    dr = dt.NewRow()
        '    dr("date") = gv_dates_edit.Rows(i).Cells(1).Text
        '    If half.Checked = True Then
        '        dr("first_half") = True
        '    End If
        '    If first_half.Checked = True Then
        '        dr("first_half") = True
        '    End If
        '    If second_half.Checked = True Then
        '        dr("second_half") = True

        '    End If
        '    dr("date_status") = gv_dates_edit.Rows(i).Cells(4).Text
        '    If holiday.Checked = True Then
        '        dr("holiday") = True
        '    End If
        '    If without_pay.Checked = True Then
        '        dr("without_pay") = True
        '    End If
        '    dt.Rows.Add(dr)
        'Next

        Dim dt As DataTable = gv_dates_edit.DataSource
        ' My datatable is being pulled in from ViewState
        Dim dv As DataView = dt.DefaultView               ' Copy the default view of your DataTable to the DataView
        dv.Sort = "Date Asc"                        ' e.SortExpression contains the column name
        dt = dv.ToTable                         ' Send the sorted table back to dt, our DataTable
        gv_dates_edit.DataSource = dt           ' Reset the GridView contol's DataSource property
        'gv_dates_edit.DataBind()
       



    End Sub

    Private Sub bind_dates()
        Dim dt As New DataTable
        Dim dr As DataRow

        gv_dates.DataSource = ws.Get_Leave_Details_Dates("")
        gv_dates.DataBind()


        dt.Columns.Add(New DataColumn("date"))
        dt.Columns.Add(New DataColumn("date_status"))
        dt.Columns.Add(New DataColumn("half_day"))
        dt.Columns.Add(New DataColumn("first_half"))
        dt.Columns.Add(New DataColumn("second_half"))
        dt.Columns.Add(New DataColumn("chk_holiday"))
        dt.Columns.Add(New DataColumn("without_pay"))
        dt.Columns.Add(New DataColumn("ref_id"))

        Dim numInterval As Integer = DateDiff(DateInterval.Day, Convert.ToDateTime(txtStart.Text), Convert.ToDateTime(txtTo.Text))
        Dim vDate As Date = txtStart.Text
        dr = dt.NewRow()
        dr("date") = txtStart.Text
        dr("half_Day") = False
        dr("first_half") = False
        dr("second_half") = False
        dr("chk_holiday") = False
        dr("without_pay") = False
        dr("ref_id") = ""


        If Calendar Then
            dr("date_status") = "-"
        Else
            If ws.Validate_Restday(Current_User.Employee_ID, txtStart.Text) = True Then
                dr("date_status") = "Restday"
            Else
                dr("date_status") = "-"
            End If
        End If


        dt.Rows.Add(dr)
        Dim allrestday As Boolean = True

        For i As Integer = 1 To numInterval


            Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDate)
            dr = dt.NewRow()
            dr("date") = Format(vNewDate, "dd-MMM-yyyy")
            dr("half_Day") = False
            dr("first_half") = False
            dr("second_half") = False
            dr("chk_holiday") = False

            If Calendar Then
                dr("date_status") = "-"
            Else
                If ws.Validate_Restday(Current_User.Employee_ID, vNewDate) = True Then
                    dr("date_status") = "Restday"
                Else
                    dr("date_status") = "-"
                    allrestday = False
                End If
            End If


            dt.Rows.Add(dr)
            vDate = vNewDate
        Next


        gv_dates.DataSource = dt
        gv_dates.DataBind()


        Dim total_days As Double = numInterval + 1
        Dim vDates As Date = txtStart.Text
        vDates = DateAdd(DateInterval.Day, -1, vDates)
        Dim daysAvailable As Decimal = CType(txt_credit.Text, Decimal)

        For i As Integer = 0 To gv_dates.Rows.Count - 1

            Dim row As GridViewRow = gv_dates.Rows(i)
            Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDates)

            Dim _holiday As Boolean = ws.Validate_Holiday(Current_User.Employee_ID, vNewDate)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

            If _holiday = True Then
                holiday.Checked = True
            Else
                holiday.Checked = False
            End If

            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)

            If Not gv_dates.Rows(i).Cells(4).Text = "-" Then
                total_days = total_days - 1
                half.Enabled = False
            ElseIf _holiday = True Then
                total_days = total_days - 1
                'half.Enabled = False
            End If


            If gv_dates.Rows(i).Cells(4).Text = "-" Then
                allrestday = False
            End If

            If daysAvailable = 0.5 Then
                half.Enabled = False
            End If

            vDates = vNewDate
        Next


        lbl_total_days.Text = "Total Days Applied = " & total_days
        lbl_days_hidden.Text = total_days

        If allrestday = True Then
            btnSubmit.Visible = False
        Else
            btnSubmit.Visible = True
        End If

    End Sub

    

    Private Sub send_notifications(ByVal ref_no As String)
        Dim body As String
        Dim recipients As String = ""
        Dim SenderName As String = Current_User.LastName & " " & Current_User.FirstName
        body = Current_User.LastName & " " & Current_User.FirstName & " is applying for " & UCase(dplType.SelectedItem.Text) & " (Edited). For your approval, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        Dim ds As DataSet, level As String, r_level As String

        r_level = ws.Get_approvers_level(Current_User.Employee_ID, Current_User.Employee_ID)

        If r_level = "FINAL" Then
            level = "%"
            recipients = ws.Get_users_Email_byRefno(ref_no)
        ElseIf r_level = "USER_FINAL" Then
            level = "FINAL"
        Else
            level = "INITIAL"
        End If

        'ds = ws.Get_Approvers_Email(Current_User.Employee_ID, level)
        'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '    Dim dr As DataRow = ds.Tables(0).Rows(i)
        '    If recipients = "" Then
        '        recipients = dr("email")
        '        Insert_Email_Summary_Notification(dr("email"), body, SenderName)
        '    Else
        '        recipients = recipients & ", " & dr("email")
        '        Insert_Email_Summary_Notification(dr("email"), body, SenderName)
        '    End If
        '    '   txtReason.Text = txtReason.Text & "|" & recipients
        'Next

        Dim ds1 As DataSet

        If r_level = "INITIAL" Then
            ds = ws.Get_Approvers_Email(Current_User.Employee_ID, "FINAL")
            Dim ds_itself As DataSet = ws.Get_Users_Email(Current_User.Employee_ID)
            Dim dr_itself As DataRow = ds_itself.Tables(0).Rows(0)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim dr As DataRow = ds.Tables(0).Rows(i)
                If dr("email") <> dr_itself("email") Then
                    Insert_Email_Summary_Notification(dr("email"), body, SenderName)
                End If
            Next
        Else
            If r_level = "" Then
                ds1 = ws.Get_Approvers_Email(Current_User.Employee_ID, "INITIAL")

                If ds1.Tables(0).Rows.Count > 0 Then
                    ds = ws.Get_Approvers_Email(Current_User.Employee_ID, "INITIAL")
                Else
                    ds = ws.Get_Approvers_Email(Current_User.Employee_ID, "FINAL")
                End If
            Else
                ds = ws.Get_Approvers_Email(Current_User.Employee_ID, level)
            End If

            Dim ds_itself As DataSet = ws.Get_Users_Email(Current_User.Employee_ID)
            Dim dr_itself As DataRow = ds_itself.Tables(0).Rows(0)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim dr As DataRow = ds.Tables(0).Rows(i)
                If dr("email") <> dr_itself("email") Then
                    Insert_Email_Summary_Notification(dr("email"), body, SenderName)
                End If
            Next
        End If
        'cls_email.SendEmail(recipients, body, SenderName)
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


    Protected Sub gv_dates_edit_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_dates_edit.RowCommand
        'Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "cmd_delete" Then
            'delete_dates(gv_dates_edit.Rows(index).Cells(1).Text)
            delete_dates_index(index)
            'UserMsgBox(index)
        End If
    End Sub

    Protected Sub gv_dates_edit_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_dates_edit.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnk_delete"), LinkButton)
            lnkDelete.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub gv_dates_edit_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gv_dates_edit.Sorting
        'gv_dates_edit.Sort("date", SortDirection.Ascending)
        'gv_dates_edit.DataBind()
        
    End Sub

    Protected Sub gv_dates_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gv_dates.Sorting

    End Sub

   
End Class
