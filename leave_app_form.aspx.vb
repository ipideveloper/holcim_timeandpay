Imports System.Data
Imports cls_Email_Notifications

Partial Class leaveapplications
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
    'Private DefURL As String = ConfigurationManager.AppSettings("DefaultURL").ToString
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
        'MsgBox(Current_User.Sex)
        Try
            If Not Page.IsPostBack Then

                Initialize()
            End If
        Catch ex As Exception
        End Try
        'MsgBox(DefURL)

        'Dim body As String
        'Dim recipients As String = ""
        'body = Current_User.LastName & " " & Current_User.FirstName & " is applying for " & UCase(dplType.SelectedItem.Text) & " . For your approval, please see Ref.  <br><br><br><br> This is a system-generated message.  Do not reply to this message. <br><br><br><br> " & DefURL
        'MsgBox(body)
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If ViewState("pressed") = "" Then
                ViewState("pressed") = "True"
                btnSubmit.Enabled = False
                For i As Integer = 0 To gv_dates.Rows.Count - 1
                    Dim row As GridViewRow = gv_dates.Rows(i)
                    Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
                    Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
                    Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
                    If ws.Validate_Duplicate_Leave("", Current_User.Employee_ID, gv_dates.Rows(i).Cells(1).Text, half.Checked, first_half.Checked, second_half.Checked) = True Then
                        UserMsgBox("You have filed the same date with your previous application.")
                        btnSubmit.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    End If
                Next


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
                        If Current_User.Employee_Status <> "0" Then
                            If CType(lbl_days_hidden.Text, Double) > CType(txt_credit.Text, Double) Then
                                'If Not CType(lbl_days_hidden.Text, Double) = 60 And Not CType(lbl_days_hidden.Text, Double) = 78 Then
                                'UserMsgBox("Total days applied must be equal to 60 or 78 calendar days!")
                                UserMsgBox("Your application exceeds the maximum Maternity Leave credits of 60 or 78 days.")
                                btnSubmit.Enabled = True
                                ViewState("pressed") = ""
                                Exit Sub
                            End If
                        End If
                    ElseIf dplType.SelectedItem.Text = "Paternity Leave" Then
                        If Current_User.Employee_Status <> "0" Then

                            If CType(lbl_days_hidden.Text, Double) > 7 Then
                                UserMsgBox("Your application exceeds the maximum Paternity Leave credits of 7 days.")
                                btnSubmit.Enabled = True
                                ViewState("pressed") = ""
                                Exit Sub
                            End If
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

                Dim ref_no As String = Insert_Leave_Header()

                'Added by Andrew Start
                If dplType.SelectedItem.Text = "Maternity Leave without pay" Then
                    Insert_leave_Details_Drew(ref_no)
                    ws.Insert_Employees_Approvers(Current_User.Employee_ID, ref_no)
                    ws.Update_Approve_Applications(ref_no, Current_User.Employee_ID, "LV")
                    send_notifications(ref_no)
                    Session("Employee_ID") = ViewState("Employee_ID")
                    ViewState("pressed") = ""
                    btnSubmit.Enabled = True
                    Response.Redirect("leave_app_header.aspx")
                Else
                    'Original Code start
                    Insert_Leave_Details(ref_no)
                    ws.Insert_Employees_Approvers(Current_User.Employee_ID, ref_no)
                    ws.Update_Approve_Applications(ref_no, Current_User.Employee_ID, "LV")
                    send_notifications(ref_no)
                    Session("Employee_ID") = ViewState("Employee_ID")
                    ViewState("pressed") = ""
                    btnSubmit.Enabled = True
                    Response.Redirect("leave_app_header.aspx")
                    'Orginal Code end
                End If
                'Added By Andrew End
            End If
        Catch ex As Exception
            UserMsgBox(ex.Message)
            btnSubmit.Enabled = True
            ViewState("pressed") = ""
        End Try
    End Sub

    Protected Sub dplType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dplType.SelectedIndexChanged
        Try
            get_leave_available()
            check_classification()
            Session("Calendar") = Calendar
            'MsgBox(Session("Calendar"))
            'Added code by Andrew 11-08-2011 Start
            If dplType.SelectedItem.Text = "Maternity Leave without pay" Then
                For i As Integer = 0 To gv_dates.Rows.Count - 1
                    Dim row As GridViewRow = gv_dates.Rows(i)
                    Dim checkwithoutpay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)
                    checkwithoutpay.Checked = True
                Next
                'dito ung code Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)
            End If
            'Added code by Andrew 11-08-2011 End
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btn_bind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_bind.Click
        Try
            Dim v_from As String = txtStart.Text
            Dim v_to As String = txtTo.Text

            If CType(v_to, Date) < CType(v_from, Date) Then
                UserMsgBox("Invalid Entry! Date To should not be earlier than Date From.")
                Exit Sub
            End If

            bind_dates()
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

    Protected Sub chk_half_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim total_days As Double = 0.0
        Dim daysCounter As Double = 1
        Dim daysAvailble As Double = CType(txt_credit.Text, Double)

        For i As Integer = 0 To gv_dates.Rows.Count - 1
            Dim row As GridViewRow = gv_dates.Rows(i)
            'gv_dates.Rows(i).Cells(1).Text

        Next


        For i As Integer = 0 To gv_dates.Rows.Count - 1
            Dim row As GridViewRow = gv_dates.Rows(i)

            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

            'If half.Enabled = True Then
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
            
            'End If

            Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)

            If gv_dates.Rows(i).Cells(4).Text <> "Restday" And holiday.Checked = False Then
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
    End Sub

#End Region

#Region "Validation"

    Private Function validate_applied_days() As Boolean


    End Function

#End Region

#Region "Insert"

    Private Function Insert_Leave_Header() As String

        Dim numInterval As Double = 0

        For i As Integer = 0 To gv_dates.Rows.Count - 1
            Dim row As GridViewRow = gv_dates.Rows(i)
            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

            If gv_dates.Rows(i).Cells(4).Text = "-" Then
                If holiday.Checked = False Then
                    If half.Checked = True Then
                        numInterval = numInterval + 0.5
                    Else
                        numInterval = numInterval + 1
                    End If
                End If
            End If
        Next

        Dim start_date As Date = gv_dates.Rows(0).Cells(1).Text
        Dim to_date As Date = gv_dates.Rows(gv_dates.Rows.Count - 1).Cells(1).Text

        Dim leave_type As String = ""

        leave_type = dplType.SelectedValue

        'Select Case dplType.SelectedItem.Text
        '    Case "Vacation Leave"
        '        leave_type = "01"
        '    Case "Buffer-Vacation Leave"
        '        leave_type = "57"
        '    Case "Sick Leave"
        '        leave_type = "02"
        '    Case "Buffer-Sick Leave"
        '        leave_type = "59"
        '    Case "Union Leave"
        '        leave_type = "78"
        '    Case "Bereavement Leave"
        '        leave_type = "54"
        '    Case "Calamity/Emergency Leave"
        '        leave_type = "56"
        '    Case "Blood Donor Leave"
        '        leave_type = "63"
        '    Case "Paternity Leave"
        '        leave_type = "53"
        '    Case "Maternity Leave without pay"
        '        leave_type = "52"
        '    Case "Cash-Vacation"
        '        leave_type = "58"
        '    Case "Cash-Sick"
        '        leave_type = "60"
        '    Case "Forfeit-Vacation Leave"
        '        leave_type = "61"
        '    Case "Forfeit-Sick Leave"
        '        leave_type = "62"
        '    Case "Incentive Leave"
        '        leave_type = "77"
        'End Select

        Dim ref_no As String = ws.Insert_Leave_Header(leave_type, start_date, to_date, numInterval, txtReason.Text, Current_User.Employee_ID)

        Return ref_no

    End Function

    Private Sub Insert_leave_Details_Drew(ByVal pref_no As String)
        For i As Integer = 0 To gv_dates.Rows.Count - 1
            Dim row As GridViewRow = gv_dates.Rows(i)
            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)
            Dim without_payx As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)

            Dim without_pay As Boolean = True
            'Added by Andrew 11-25-2011 start
            If gv_dates.Rows(i).Cells(4).Text <> "Restday" Then
                ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, half.Checked, IIf(first_half.Checked = True, True, False), IIf(second_half.Checked = True, True, False), without_pay)
            End If
            'Added by Andrew 11-25-2011 End
        Next

    End Sub

    Private Sub Insert_Leave_Details(ByVal pref_no As String)
        Dim Noofleave As Double = 0.0
        For i As Integer = 0 To gv_dates.Rows.Count - 1
            Dim row As GridViewRow = gv_dates.Rows(i)
            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)
            Dim without_payx As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)

            Dim without_pay As Boolean = without_payx.Checked

            'Added by Andrew 11-25-2011 start
            If gv_dates.Rows(i).Cells(4).Text <> "Restday" Then
                ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, half.Checked, IIf(first_half.Checked = True, True, False), IIf(second_half.Checked = True, True, False), without_pay)
            End If
            'Added by Andrew 11-25-2011 End

            'ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, half.Checked, IIf(first_half.Checked = True, True, False), IIf(second_half.Checked = True, True, False), without_pay)

            'Dim without_pay As Boolean = False

            'If holiday.Checked = False Then
            '    If dplType.SelectedItem.Text = "Vacation Leave" Then
            '        If ws.Get_Leave_Available(Current_User.Employee_ID, "01") <= 0 Then
            '            without_pay = True
            '        End If
            '    ElseIf dplType.SelectedItem.Text = "Sick Leave" Then
            '        If ws.Get_Leave_Available(Current_User.Employee_ID, "02") <= 0 Then
            '            without_pay = True
            '        End If
            '    End If



            'End If

            'If gv_dates.Rows(i).Cells(4).Text <> "Restday" Then
            '    Dim vlsl_balance As Decimal = 0
            '    If dplType.SelectedItem.Text = "Vacation Leave" Then
            '        If ws.Get_Leave_Available(Current_User.Employee_ID, "01") = 0.5 And Not half.Checked Then
            '            ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, True, True, False, False)
            '            ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, True, False, True, True)
            '        Else
            '            ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, half.Checked, IIf(first_half.Checked = True, True, False), IIf(second_half.Checked = True, True, False), without_pay)
            '        End If

            '    ElseIf dplType.SelectedItem.Text = "Sick Leave" Then
            '        If ws.Get_Leave_Available(Current_User.Employee_ID, "02") = 0.5 And Not half.Checked Then
            '            ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, True, True, False, False)
            '            ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, True, False, True, True)
            '        Else
            '            ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, half.Checked, IIf(first_half.Checked = True, True, False), IIf(second_half.Checked = True, True, False), without_pay)
            '        End If

            '    Else
            '        ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, half.Checked, IIf(first_half.Checked = True, True, False), IIf(second_half.Checked = True, True, False), without_pay)
            '    End If
            'End If

            'ws.Insert_Leave_Details(pref_no, gv_dates.Rows(i).Cells(1).Text, half.Checked, IIf(first_half.Checked = True, True, False), IIf(second_half.Checked = True, True, False), without_pay)

        Next
    End Sub

#End Region

#Region "Retrieve"

    Private Sub bind_leaves()

        Dim ds As DataSet
        ds = ws.Leave_selection(Current_User.Employee_ID)
        dplType.DataSource = ds
        dplType.DataValueField = "leave_type"
        dplType.DataTextField = "description"
        dplType.DataBind()
        lbl_days_hidden.Text = ds.Tables(0).Rows.Count
        'dplType.Items.Add("")

        'If ws.Get_Leave_Available(Current_User.Employee_ID, "01") <= 0 Then
        '    If ws.Get_Leave_Available(Current_User.Employee_ID, "57") <= 0 Then
        '        dplType.Items.Add("Vacation Leave")
        '    Else
        '        If ws.Get_Employee_Group(Current_User.Employee_ID) = 1 And _
        '            ws.Get_Employee_Status(Current_User.Employee_ID) = 1 Then
        '            dplType.Items.Add("Buffer-Vacation Leave")
        '        Else
        '            dplType.Items.Add("Vacation Leave")
        '        End If
        '    End If
        'Else
        '    dplType.Items.Add("Vacation Leave")
        'End If

        'If ws.Get_Leave_Available(Current_User.Employee_ID, "02") <= 0 Then
        '    If ws.Get_Leave_Available(Current_User.Employee_ID, "59") <= 0 Then
        '        dplType.Items.Add("Sick Leave")
        '    Else
        '        If ws.Get_Employee_Group(Current_User.Employee_ID) = 1 And _
        '            ws.Get_Employee_Status(Current_User.Employee_ID) = 1 Then
        '            dplType.Items.Add("Buffer-Sick Leave")
        '        Else
        '            dplType.Items.Add("Sick Leave")
        '        End If
        '    End If
        'Else
        '    dplType.Items.Add("Sick Leave")
        'End If

        'If ws.Get_Employee_Group(Current_User.Employee_ID) = 1 And _
        '    ws.Get_Employee_Status(Current_User.Employee_ID) = 1 Then

        '    If Not Current_User.Union_Code = "" Then
        '        dplType.Items.Add("Union Leave")
        '    End If

        '    dplType.Items.Add("Bereavement Leave")
        '    dplType.Items.Add("Calamity/Emergency Leave")
        '    dplType.Items.Add("Blood Donor Leave")

        'End If

        'If Current_User.Sex = "Male" Then
        '    dplType.Items.Add("Paternity Leave")
        'Else
        '    dplType.Items.Add("Maternity Leave without pay")
        'End If

        'If ws.Get_Employee_Group(Current_User.Employee_ID) = 7 Then
        '    dplType.Items.Add("Incentive Leave")
        'End If

    End Sub

    Private Sub get_leave_available()
        Dim leaveavailable As Decimal = ws.Get_Leave_Available(Current_User.Employee_ID, dplType.SelectedValue)
        If leaveavailable = 9999 Then
            txt_credit.Text = "N/A"
        Else
            txt_credit.Text = leaveavailable
        End If
        'txt_credit.Text = ws.Get_Leave_Available(Current_User.Employee_ID, dplType.SelectedValue)

        'Select Case dplType.SelectedItem.Text
        '    Case "Vacation Leave"
        '        txt_credit.Text = ws.Get_Leave_Available(Current_User.Employee_ID, "01")
        '    Case "Sick Leave"
        '        txt_credit.Text = ws.Get_Leave_Available(Current_User.Employee_ID, "02")
        '    Case "Buffer-Sick Leave"
        '        txt_credit.Text = ws.Get_Leave_Available(Current_User.Employee_ID, "59")
        '    Case "Buffer-Vacation Leave"
        '        txt_credit.Text = ws.Get_Leave_Available(Current_User.Employee_ID, "57")
        '    Case "Incentive Leave"
        '        txt_credit.Text = ws.Get_Leave_Available(Current_User.Employee_ID, "77")
        '    Case "Bereavement Leave"
        '        txt_credit.Text = "7"
        '    Case "Calamity/Emergency Leave"
        '        txt_credit.Text = "3"
        '    Case "Blood Donor Leave"
        '        txt_credit.Text = "1"
        '    Case "Paternity Leave"
        '        txt_credit.Text = "7"
        '    Case "Maternity Leave without pay"
        '        txt_credit.Text = "78"
        '    Case "Union Leave"
        '        Dim union_available As Double = ws.Get_Union_Available(Current_User.Employee_ID, Current_User.Union_Code)
        '        If union_available = 9999 Then
        '            txt_credit.Text = "N/A"
        '        Else
        '            txt_credit.Text = union_available
        '        End If
        '    Case Else
        '        txt_credit.Text = ""
        'End Select
        btnSubmit.Visible = False
    End Sub


    Private Sub check_classification()
        Calendar = ws.Get_Leave_Classification(dplType.SelectedValue)
        'Select Case dplType.SelectedItem.Text
        '    Case "Vacation Leave"
        '        Calendar = ws.Get_Leave_Classification("01")
        '    Case "Buffer-Vacation Leave"
        '        Calendar = ws.Get_Leave_Classification("57")
        '    Case "Sick Leave"
        '        Calendar = ws.Get_Leave_Classification("02")
        '    Case "Buffer-Sick Leave"
        '        Calendar = ws.Get_Leave_Classification("59")
        '    Case "Union Leave"
        '        Calendar = ws.Get_Leave_Classification("78")
        '    Case "Bereavement Leave"
        '        Calendar = ws.Get_Leave_Classification("54")
        '    Case "Calamity/Emergency Leave"
        '        Calendar = ws.Get_Leave_Classification("56")
        '    Case "Blood Donor Leave"
        '        Calendar = ws.Get_Leave_Classification("63")
        '    Case "Paternity Leave"
        '        Calendar = ws.Get_Leave_Classification("53")
        '    Case "Maternity Leave without pay"
        '        Calendar = ws.Get_Leave_Classification("52")
        '    Case "Cash-Vacation"
        '        Calendar = ws.Get_Leave_Classification("58")
        '    Case "Cash-Sick"
        '        Calendar = ws.Get_Leave_Classification("60")
        '    Case "Forfeit-Vacation Leave"
        '        Calendar = ws.Get_Leave_Classification("61")
        '    Case "Forfeit-Sick Leave"
        '        Calendar = ws.Get_Leave_Classification("62")
        '    Case "Incentive Leave"
        '        Calendar = ws.Get_Leave_Classification("77")
        'End Select
        btnSubmit.Visible = False
    End Sub

#End Region

#Region "Delete"

    Private Sub delete_dates(ByVal strdate As String)

        Dim total_days As Double = 0.0
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("date"))
        dt.Columns.Add(New DataColumn("half"))
        dt.Columns.Add(New DataColumn("first_half"))
        dt.Columns.Add(New DataColumn("second_half"))
        dt.Columns.Add(New DataColumn("date_status"))
        dt.Columns.Add(New DataColumn("holiday"))

        Dim allrestday As Boolean = True

        For i As Integer = 0 To gv_dates.Rows.Count - 1
            If Not strdate = gv_dates.Rows(i).Cells(1).Text Then
                Dim row As GridViewRow = gv_dates.Rows(i)
                Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
                Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
                Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
                Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

                dr = dt.NewRow()
                dr("date") = gv_dates.Rows(i).Cells(1).Text
                dr("half") = half.Checked
                dr("first_half") = first_half.Checked
                dr("second_half") = second_half.Checked
                dr("date_status") = gv_dates.Rows(i).Cells(4).Text
                dr("holiday") = holiday.Checked

                dt.Rows.Add(dr)

                If gv_dates.Rows(i).Cells(4).Text = "-" Then
                    If holiday.Checked = False Then
                        If half.Checked = True Then
                            total_days = total_days + 0.5
                        Else
                            total_days = total_days + 1.0
                        End If
                    End If
                    allrestday = False
                End If

            End If

        Next

        gv_dates.DataSource = dt
        gv_dates.DataBind()

        For i As Integer = 0 To gv_dates.Rows.Count - 1
            Dim row As GridViewRow = gv_dates.Rows(i)
            Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)
            Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

            half.Checked = dt.Rows(i).Item("half")
            first_half.Checked = dt.Rows(i).Item("first_half")
            second_half.Checked = dt.Rows(i).Item("second_half")
            holiday.Checked = dt.Rows(i).Item("holiday")
            gv_dates.Rows(i).Cells(4).Text = dt.Rows(i).Item("date_status")

            If half.Checked = True Then
                first_half.Enabled = True
                second_half.Enabled = True
                '  total_days = total_days + 0.5
            Else
                ' total_days = total_days + 1.0
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

#End Region

#Region "Manage"

    Private Sub bind_dates()
        Dim dt As New DataTable
        Dim dr As DataRow
        Calendar = Session("Calendar")
        dt.Columns.Add(New DataColumn("date"))
        dt.Columns.Add(New DataColumn("date_status"))

        Dim numInterval As Integer = DateDiff(DateInterval.Day, Convert.ToDateTime(txtStart.Text), Convert.ToDateTime(txtTo.Text))
        Dim vDate As Date = txtStart.Text
        dr = dt.NewRow()
        dr("date") = txtStart.Text

        If Calendar Then
            dr("date_status") = "-"
        Else

            If ws.Validate_Restday(Current_User.Employee_ID, txtStart.Text) = True Then
                dr("date_status") = "Restday"
            Else
                dr("date_status") = "-"
            End If
        End If
        'If ws.Validate_Duplicate_Leave(Current_User.Employee_ID, txtStart.Text, True, False, False) = True Then
        '    dr("date_status") = "Duplicate"
        'Else
        '    dr("date_status") = "-"
        'End If
        dt.Rows.Add(dr)
        Dim allrestday As Boolean = True

        For i As Integer = 1 To numInterval
            Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDate)
            dr = dt.NewRow()
            dr("date") = Format(vNewDate, "dd-MMM-yyyy")

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
            'If ws.Validate_Duplicate_Leave(Current_User.Employee_ID, vNewDate, True, False, False) = True Then
            '    dr("date_status") = "Duplicate"
            'Else
            '    dr("date_status") = "-"
            'End If

            dt.Rows.Add(dr)
            vDate = vNewDate
        Next


        gv_dates.DataSource = dt
        gv_dates.DataBind()

        Dim total_days As Double = numInterval + 1
        Dim vDates As Date = txtStart.Text
        vDates = DateAdd(DateInterval.Day, -1, vDates)
        Dim daysCounter As Double = 0
        Dim daysAvailble As Double = CType(txt_credit.Text, Double)

        Dim NewDate As Date = txtStart.Text
        Dim NeedNewDate As Date = "01/01/1900"
        Dim NeedNewRow As Boolean = False


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
            Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
            Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)

            second_half.Checked = False
            first_half.Checked = False
            half.Checked = False


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

            '--------------- modify by Art 20110904
            Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)


            If gv_dates.Rows(i).Cells(4).Text <> "Restday" And holiday.Checked = False Then
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

        If NeedNewRow Then
            Dim newDT As New DataTable
            Dim newDR As DataRow

            newDT = gv_dates.DataSource
            newDR = newDT.NewRow()

            newDR("date") = Format(NeedNewDate, "dd-MMM-yyyy")
            newDR("date_status") = "-"
            newDT.Rows.Add(newDR)
            gv_dates.DataSource = Nothing
            gv_dates.DataBind()
            gv_dates.DataSource = newDT
            'gv_dates.Sort("date", SortDirection.Ascending)
            gv_dates.DataBind()

            '-------------------------------------------------------------

            total_days = numInterval + 1
            vDates = txtStart.Text
            vDates = DateAdd(DateInterval.Day, -1, vDates)
            daysCounter = 0
            daysAvailble = CType(txt_credit.Text, Double)


            Dim ctr As Integer = 1
            For i As Integer = 0 To gv_dates.Rows.Count - 1
                NewDate = gv_dates.Rows(i).Cells(1).Text
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
                Dim first_half As RadioButton = CType(row.FindControl("r_first"), RadioButton)
                Dim second_half As RadioButton = CType(row.FindControl("r_second"), RadioButton)

                second_half.Checked = False
                first_half.Checked = False
                half.Checked = False


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

                '--------------- modify by Art 20110904
                Dim without_pay As CheckBox = CType(row.FindControl("check_withoutpay"), CheckBox)


                If gv_dates.Rows(i).Cells(4).Text <> "Restday" And holiday.Checked = False Then
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

                If gv_dates.Rows(i).Cells(4).Text = "Restday" Or holiday.Checked = True Then
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

    Private Sub send_notifications(ByVal ref_no As String)
        Dim body As String
        Dim recipients As String = ""
        Dim SenderName As String = Current_User.LastName & " " & Current_User.FirstName
        body = Current_User.LastName & " " & Current_User.FirstName & " is applying for " & UCase(dplType.SelectedItem.Text) & " . For your approval, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message. <br><br><br><br> "
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


        'ADDED NOV 19 BY JAN
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

        'COMMENT BY JAN NOV 19
        'ds = ws.Get_Approvers_Email(Current_User.Employee_ID, level)
        'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        '    Dim dr As DataRow = ds.Tables(0).Rows(i)
        '    If recipients = "" Then
        '        recipients = dr("email")
        '        Insert_Email_Summary_Notification(dr("email"), body, SenderName)
        '    Else
        '        Insert_Email_Summary_Notification(dr("email"), body, SenderName)
        '        recipients = recipients & ", " & dr("email")
        '    End If
        '    'txtReason.Text = txtReason.Text & "|" & recipients
        'Next
        'DISABLE EMAIL NOTIFICATION - 09022012 - PHASE 2
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

End Class