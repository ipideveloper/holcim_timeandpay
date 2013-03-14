Imports System.Data
Imports cls_Email_Notifications

Partial Class ot_edit_form
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

        Dim sector As String = Session("Branch_ID")

        'For Sector Restriction
        Select Case sector
            Case "PLG0"
                Response.Redirect("ot_edit_form.aspx")
            Case "PDV0"
                Response.Redirect("ot_edit_form.aspx")
            Case "PBL0"
                Response.Redirect("ot_edit_form.aspx")
            Case "PLN0"
                Response.Redirect("ot_edit_form.aspx")
        End Select

        Try
            If Not Page.IsPostBack Then
                Initialize()


            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub BtnSaveExtend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSaveExtend.Click
        Try
            If ViewState("pressed") = "" Then
                ViewState("pressed") = "True"
                BtnSaveExtend.Enabled = False
                If dpl_classifications.Enabled = False Then
                    RequiredClassificationValidator.Visible = False
                Else
                    If dpl_classifications.Text = "" Then
                        RequiredClassificationValidator.Visible = True
                        UserMsgBox("Classifications cannot be blank.  Please select one")
                    Else
                        RequiredClassificationValidator.Visible = False
                    End If

                End If

                'Dim v_from As String = txt_date_from.Text & " " & dplFrom1.SelectedItem.Text & ":" & lstFrom1.SelectedItem.Text & " " & ampmFrom1.SelectedItem.Text
                Dim v_from As String = txt_date_from.Text & " 12:00 AM"
                Dim v_to As String = txt_date_from.Text & " " & dplTo1.SelectedItem.Text & ":" & lstTo1.SelectedItem.Text & " " & ampmTo1.SelectedItem.Text

                If CType(v_to, Date) < CType(v_from, Date) Then
                    UserMsgBox("Invalid Entry! Time To should not be earlier than Time From.")
                    BtnSaveExtend.Enabled = True
                    ViewState("pressed") = ""
                    Exit Sub
                ElseIf CType(v_to, Date) = CType(v_from, Date) Then
                    UserMsgBox("Invalid Entry! Time To should not be equal to Time From.")
                    BtnSaveExtend.Enabled = True
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                If ws.Validate_Duplicate_DateTime(Session("RefNo"), IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), CType(v_from, Date), CType(v_to, Date), "OT") = True Then
                    UserMsgBox("You have filed the same date with your previous application.")
                    BtnSaveExtend.Enabled = True
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                Dim v_count As Decimal
                Dim det_1159 As String = dplTo1.SelectedItem.Text & ":" & lstTo1.SelectedItem.Text & " " & ampmTo1.SelectedItem.Text

                v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
                v_to = CType(DateAdd(DateInterval.Day, 1, CType(txt_date_from.Text, Date)), String) & " " & dplTo1.SelectedItem.Text & ":" & lstTo1.SelectedItem.Text & " " & ampmTo1.SelectedItem.Text

                If det_1159 = "11:59 PM" Then
                    v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(CType(DateAdd(DateInterval.Day, 1, CType(txt_date_from.Text, Date)), String) & " 11:59:59 PM", Date)) / 60.0 / 60.0
                    v_count += 0.01
                Else
                    v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
                End If

                v_count = Decimal.Round(v_count, 2)

                If ws.Get_User_Info(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)).Tables(0).Rows(0).Item("employee_type") = "A" Then
                    If v_count < 1 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Minimum of 1 hour is allowed to file overtime for Associate Employee")
                        BtnSaveExtend.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    End If
                Else
                    If v_count < 4 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Less than 4 hours is not allowed to file ESD.")
                        BtnSaveExtend.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    ElseIf v_count < 8 And v_count > 4 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Only 4 hours will be paid to this filed ESD.")
                    ElseIf v_count < 12 And v_count > 8 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Only 8 hours will be paid to this filed ESD.")
                    ElseIf v_count > 12 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Only 12 hours will be paid to this filed ESD.")
                    End If
                End If

                add_employee_to_list()
                manage_lookups(Current_User.Employee_Type)
                '  clear_fields()
                insert_ot_applications()
                ViewState("pressed") = ""
                Session("Employee_ID") = ViewState("Employee_ID")
                BtnSaveExtend.Enabled = True
                Session("btn_name") = "btnMyAppStatus"
                Response.Redirect(Session("Return_OT")) '"my_app_status.aspx")
            End If
        Catch ex As Exception
            UserMsgBox(ex.Message)
            ViewState("pressed") = ""
        End Try
        BtnSaveExtend.Enabled = True
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If ViewState("pressed") = "" Then
                ViewState("pressed") = "True"
                btnSave.Enabled = False

                If dpl_classifications.Enabled = False Then
                    RequiredClassificationValidator.Visible = False
                Else
                    If dpl_classifications.Text = "" Then
                        RequiredClassificationValidator.Visible = True
                        UserMsgBox("Classifications cannot be blank.  Please select one")
                    Else
                        RequiredClassificationValidator.Visible = False
                    End If

                End If

                'Dim v_from As String = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
                'Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

                Dim v_from As String = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
                Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

                Dim time_from As String = dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
                Dim time_to As String = dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text



                'If CType(v_to, Date) < CType(v_from, Date) Then
                '    UserMsgBox("Invalid Entry! Time To should not be earlier than Time From.")
                '    btnSave.Enabled = True
                '    ViewState("pressed") = ""
                '    Exit Sub
                'ElseIf CType(v_to, Date) = CType(v_from, Date) Then
                '    UserMsgBox("Invalid Entry! Time To should not be equal to Time From.")
                '    btnSave.Enabled = True
                '    ViewState("pressed") = ""
                '    Exit Sub
                'End If

                If NextDayCheckBox.Checked = False Then
                    If CType(v_to, Date) < CType(v_to, Date) Then
                        UserMsgBox("Invalid Entry! Time To should not be earlier than Time From.")
                        btnSave.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    ElseIf CType(v_to, Date) = CType(v_from, Date) Then
                        UserMsgBox("Invalid Entry! Time To should not be equal to Time From.")
                        btnSave.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    End If
                End If

                If CType(v_to, Date) < CType(v_from, Date) Then
                    v_to = DateAdd(DateInterval.Day, 1, CDate(v_to))
                End If

                If ws.Validate_Duplicate_DateTime(Session("RefNo"), IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), CType(v_from, Date), CType(v_to, Date), "OT") = True Then
                    UserMsgBox("You have an overlap date and time entry.")
                    btnSave.Enabled = True
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                'Dim v_count As Double

                'v_count = DateDiff(DateInterval.Hour, CType(v_from, Date), CType(v_to, Date))


                'REMOVE BY JAN
                'Dim v_count As Decimal
                'Dim det_1159 As String
                'det_1159 = dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

                If time_to = "11:59 PM" Then
                    'v_count = CInt(DateDiff(DateInterval.Second, CType(v_from, Date), CType(txt_date_from.Text & " 11:59:59 PM", Date)) / 60.0 / 60.0)
                    Labelextend.Visible = True
                    dplFrom.Enabled = False
                    dplTo.Enabled = False
                    lstFrom.Enabled = False
                    lstTo.Enabled = False
                    ampmFrom.Enabled = False
                    ampmTo.Enabled = False
                    BtnYES.Visible = True
                    BtnNO.Visible = True
                    dplTo1.Focus()
                    btnSave.Visible = False
                    btnSubmit.Visible = False
                    txt_date_from.Enabled = False
                    btnSave.Enabled = True
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                'Else
                '    v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
                'End If

                'v_count = Decimal.Round(v_count, 2)

                Dim v_count As Decimal
                Dim det_1159 As String = dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

                Dim v_from1 As String = ""
                Dim v_to1 As String = ""

                If NextDayCheckBox.Checked = True Then
                    Dim fromdatetime As DateTime = txt_date_from.Text & " 12:00 AM"
                    Dim todatetime As DateTime = txt_date_from.Text & " 12:00 AM"
                    v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
                    v_from1 = DateAdd(DateInterval.Day, 1, fromdatetime)
                    v_to1 = DateAdd(DateInterval.Day, 1, todatetime)
                    v_to = v_to1 & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
                Else
                    v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
                End If

                If NextDayCheckBox.Checked = True Then
                    Dim count1 As Decimal
                    Dim count2 As Decimal
                    count1 = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_from1, Date)) / 60.0 / 60.0
                    count2 = DateDiff(DateInterval.Second, CType(v_to1, Date), CType(v_to, Date)) / 60.0 / 60.0
                    v_count = count1 + count2
                Else
                    If det_1159 = "11:59 PM" Then
                        'v_count = CInt(DateDiff(DateInterval.Second, CType(v_from, Date), CType(txt_date_from.Text & " 11:59:59 PM", Date)) / 60.0 / 60.0)
                        Labelextend.Visible = True
                        dplFrom.Enabled = False
                        dplTo.Enabled = False
                        lstFrom.Enabled = False
                        lstTo.Enabled = False
                        ampmFrom.Enabled = False
                        ampmTo.Enabled = False
                        BtnYES.Visible = True
                        BtnNO.Visible = True
                        dplTo1.Focus()
                        btnSave.Visible = False
                        btnSubmit.Visible = False
                        txt_date_from.Enabled = False
                        Exit Sub

                    Else
                        v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
                    End If
                End If

                v_count = Decimal.Round(v_count, 2)

                If ws.Get_User_Info(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)).Tables(0).Rows(0).Item("employee_type") = "A" Then
                    If v_count < 1 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Minimum of 1 hour is allowed to file overtime for Associate Employee")
                        btnSave.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    End If
                Else
                    If v_count < 4 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Less than 4 hours is not allowed to file ESD.")
                        btnSave.Enabled = True
                        ViewState("pressed") = ""
                        Exit Sub
                    ElseIf v_count < 8 And v_count > 4 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Only 4 hours will be paid to this filed ESD.")
                    ElseIf v_count < 12 And v_count > 8 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Only 8 hours will be paid to this filed ESD.")
                    ElseIf v_count > 12 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Only 12 hours will be paid to this filed ESD.")
                    End If
                End If
                Labelnoofhours.Visible = True
                Labelnoofhours.Text = CType(v_count, String)
                add_employee_to_list()
                manage_lookups(Current_User.Employee_Type)
                clear_fields()
                insert_ot_applications()
                ViewState("pressed") = ""
                Session("Employee_ID") = ViewState("Employee_ID")
                btnSave.Enabled = True
                Response.Redirect(Session("Return_OT")) '"my_app_status.aspx")
            End If
        Catch ex As Exception
            UserMsgBox(ex.Message)
            ViewState("pressed") = ""
        End Try
        btnSave.Enabled = True
    End Sub

    Protected Sub BtnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnYES.Click
        Try
            txt_date_from.Enabled = False
            dplFrom1.Visible = True
            lstFrom1.Visible = True
            ampmFrom1.Visible = True
            dplTo1.Visible = True
            lstTo1.Visible = True
            ampmTo1.Visible = True
            dplTo1.Enabled = True
            lstTo1.Enabled = True
            ampmTo1.Enabled = True
            TextBoxexfrom.Visible = True
            TextBoxexto.Visible = True
            TextBoxexhhmmfrom.Visible = True
            TextBoxexhhmmto.Visible = True
            BtnYES.Visible = False
            BtnNO.Visible = False
            Labelextend.Visible = False
            'btnSaveExtended.Visible = True
            BtnSaveExtend.Visible = True
            dplTo1.Focus()
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub BtnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNO.Click
        Try
            If ViewState("pressed") = "" Then
                ViewState("pressed") = "True"
                txt_date_from.Enabled = True
                dplFrom.Enabled = True
                dplTo.Enabled = True
                lstFrom.Enabled = True
                lstTo.Enabled = True
                ampmFrom.Enabled = True
                ampmTo.Enabled = True
                btnSave.Visible = True
                dplTo1.Visible = False
                lstTo1.Visible = False
                ampmTo1.Visible = False
                dplFrom1.Visible = False
                lstFrom1.Visible = False
                ampmFrom1.Visible = False
                Labelextend.Visible = False
                BtnYES.Visible = False
                BtnNO.Visible = False

                If dpl_classifications.Enabled = False Then
                    RequiredClassificationValidator.Visible = False
                Else
                    If dpl_classifications.Text = "" Then
                        RequiredClassificationValidator.Visible = True
                        UserMsgBox("Classifications cannot be blank.  Please select one")
                    Else
                        RequiredClassificationValidator.Visible = False
                    End If

                End If

                Dim v_from As String = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
                Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

                If CType(v_to, Date) < CType(v_from, Date) Then
                    UserMsgBox("Invalid Entry! Time To should not be earlier than Time From.")
                    ViewState("pressed") = ""
                    Exit Sub
                ElseIf CType(v_to, Date) = CType(v_from, Date) Then
                    UserMsgBox("Invalid Entry! Time To should not be equal to Time From.")
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                If ws.Validate_Duplicate_DateTime(Session("RefNo"), IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), CType(v_from, Date), CType(v_to, Date), "OT") = True Then
                    UserMsgBox("You have filed the same date with your previous application.")
                    ViewState("pressed") = ""
                    Exit Sub
                End If

                Dim v_count As Decimal
                Dim det_1159 As String = dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

                If det_1159 = "11:59 PM" Then
                    v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(txt_date_from.Text & " 11:59:59 PM", Date)) / 60.0 / 60.0
                    v_count += 0.01
                Else
                    v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
                End If

                v_count = Decimal.Round(v_count, 2)

                If ws.Get_User_Info(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)).Tables(0).Rows(0).Item("employee_type") = "A" Then
                    If v_count < 1 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Minimum of 1 hour is allowed to file overtime for Associate Employee")
                        ViewState("pressed") = ""
                        Exit Sub
                    End If
                Else
                    If v_count < 4 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Less than 4 hours is not allowed to file ESD.")
                        ViewState("pressed") = ""
                        Exit Sub
                    ElseIf v_count < 8 And v_count > 4 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Only 4 hours will be paid to this filed ESD.")
                    ElseIf v_count < 12 And v_count > 8 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Only 8 hours will be paid to this filed ESD.")
                    ElseIf v_count > 12 Then
                        UserMsgBox("OT hours = " & CType(v_count, String) & "; Only 12 hours will be paid to this filed ESD.")
                    End If
                End If
                add_employee_to_list()
                manage_lookups(Current_User.Employee_Type)
                clear_fields()
                insert_ot_applications()
                ViewState("pressed") = ""
                Session("Employee_ID") = ViewState("Employee_ID")
                btnSave.Enabled = True
                Response.Redirect(Session("Return_OT")) '"my_app_status.aspx")
            End If
        Catch ex As Exception
            txtRemarks.Text = ex.Message
            btnSave.Enabled = True
            ViewState("pressed") = ""
            UserMsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Delete_index" Then
            delete_gridview_row(index)
            dpl_classifications.Enabled = True
            manage_lookups(Current_User.Employee_Type)
            If GridView1.Rows.Count < 1 Then
                btnSubmit.Visible = False
            End If
        End If
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            lnkDelete.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If ViewState("pressed") = "" Then
                ViewState("pressed") = "True"
                insert_ot_applications()
                ViewState("pressed") = ""
                Session("Employee_ID") = ViewState("Employee_ID")
                Response.Redirect("ot_header.aspx")
            End If
        Catch ex As Exception
            ViewState("pressed") = ""
        End Try
    End Sub

    Protected Sub dplEmployee_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dplEmployee.SelectedIndexChanged
        Try
            manage_lookups(ws.Get_User_Info(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)).Tables(0).Rows(0).Item("employee_type"))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub triggerselectionchanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dplEmployee.SelectedIndexChanged
        Try
            manage_lookups(ws.Get_User_Info(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)).Tables(0).Rows(0).Item("employee_type"))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub dpl_reason_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dpl_reason.SelectedIndexChanged
        lookup_esd_classifications(dpl_reason.SelectedValue)
    End Sub

    Protected Sub dplFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dplFrom.SelectedIndexChanged
        Dim v_from As String = txt_date_from.Text & " 12:00 AM"
        Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Dim v_count As Decimal

        Dim v_from1 As String = ""
        Dim v_to1 As String = ""

        'BEGIN - Added by JAN 07-24-2012
        Dim myDate As String = txt_date_from.Text
        If myDate = "" Then
            myDate = "01-Jan-1900"
        Else
            myDate = txt_date_from.Text

        End If

        Dim fromdate As Date = myDate & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        Dim todate As Date = myDate & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        'UserMsgBox(fromdate + todate)

        If fromdate < todate Then
            NextDayCheckBox.Checked = False
        Else
            NextDayCheckBox.Checked = True
        End If
        'END - Added by JAN 07-24-2012



        If NextDayCheckBox.Checked = True Then
            Dim fromdatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            Dim todatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            v_from1 = DateAdd(DateInterval.Day, 1, fromdatetime)
            v_to1 = DateAdd(DateInterval.Day, 1, todatetime)
            v_to = v_to1 & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Else
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        End If

        If NextDayCheckBox.Checked = True Then
            Dim count1 As Decimal
            Dim count2 As Decimal
            count1 = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_from1, Date)) / 60.0 / 60.0
            count2 = DateDiff(DateInterval.Second, CType(v_to1, Date), CType(v_to, Date)) / 60.0 / 60.0
            v_count = count1 + count2
        Else
            v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
        End If

        v_count = Decimal.Round(v_count, 2)

        Label_NoOfHrs.Visible = True
        Labelnoofhours.Visible = True
        Labelnoofhours.Text = CType(v_count, String)

    End Sub

    Protected Sub lstFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstFrom.SelectedIndexChanged
        Dim v_from As String = txt_date_from.Text & " 12:00 AM"
        Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

        Dim v_count As Decimal

        Dim v_from1 As String = ""
        Dim v_to1 As String = ""

        'BEGIN - Added by JAN 07-24-2012
        Dim myDate As String = txt_date_from.Text
        If myDate = "" Then
            myDate = "01-Jan-1900"
        Else
            myDate = txt_date_from.Text

        End If

        Dim fromdate As Date = myDate & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        Dim todate As Date = myDate & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        'UserMsgBox(fromdate + todate)

        If fromdate < todate Then
            NextDayCheckBox.Checked = False
        Else
            NextDayCheckBox.Checked = True
        End If
        'END - Added by JAN 07-24-2012


        If NextDayCheckBox.Checked = True Then
            Dim fromdatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            Dim todatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            v_from1 = DateAdd(DateInterval.Day, 1, fromdatetime)
            v_to1 = DateAdd(DateInterval.Day, 1, todatetime)
            v_to = v_to1 & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Else
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        End If

        If NextDayCheckBox.Checked = True Then
            Dim count1 As Decimal
            Dim count2 As Decimal
            count1 = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_from1, Date)) / 60.0 / 60.0
            count2 = DateDiff(DateInterval.Second, CType(v_to1, Date), CType(v_to, Date)) / 60.0 / 60.0
            v_count = count1 + count2
        Else
            v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
        End If

        v_count = Decimal.Round(v_count, 2)

        Label_NoOfHrs.Visible = True
        Labelnoofhours.Visible = True
        Labelnoofhours.Text = CType(v_count, String)
    End Sub

    Protected Sub ampmFrom_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ampmFrom.SelectedIndexChanged
        Dim v_from As String = txt_date_from.Text & " 12:00 AM"
        Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

        Dim v_count As Decimal

        Dim v_from1 As String = ""
        Dim v_to1 As String = ""

        'BEGIN - Added by JAN 07-24-2012
        Dim myDate As String = txt_date_from.Text
        If myDate = "" Then
            myDate = "01-Jan-1900"
        Else
            myDate = txt_date_from.Text

        End If

        Dim fromdate As Date = myDate & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        Dim todate As Date = myDate & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        'UserMsgBox(fromdate + todate)

        If fromdate < todate Then
            NextDayCheckBox.Checked = False
        Else
            NextDayCheckBox.Checked = True
        End If
        'END - Added by JAN 07-24-2012


        If NextDayCheckBox.Checked = True Then
            Dim fromdatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            Dim todatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            v_from1 = DateAdd(DateInterval.Day, 1, fromdatetime)
            v_to1 = DateAdd(DateInterval.Day, 1, todatetime)
            v_to = v_to1 & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Else
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        End If

        If NextDayCheckBox.Checked = True Then
            Dim count1 As Decimal
            Dim count2 As Decimal
            count1 = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_from1, Date)) / 60.0 / 60.0
            count2 = DateDiff(DateInterval.Second, CType(v_to1, Date), CType(v_to, Date)) / 60.0 / 60.0
            v_count = count1 + count2
        Else
            v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
        End If

        v_count = Decimal.Round(v_count, 2)

        Label_NoOfHrs.Visible = True
        Labelnoofhours.Visible = True
        Labelnoofhours.Text = CType(v_count, String)
    End Sub

    Protected Sub dplTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dplTo.SelectedIndexChanged
        Dim v_from As String = txt_date_from.Text & " 12:00 AM"
        Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

        Dim v_count As Decimal

        Dim v_from1 As String = ""
        Dim v_to1 As String = ""

        'BEGIN - Added by JAN 07-24-2012
        Dim myDate As String = txt_date_from.Text
        If myDate = "" Then
            myDate = "01-Jan-1900"
        Else
            myDate = txt_date_from.Text

        End If

        Dim fromdate As Date = myDate & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        Dim todate As Date = myDate & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        'UserMsgBox(fromdate + todate)

        If fromdate < todate Then
            NextDayCheckBox.Checked = False
        Else
            NextDayCheckBox.Checked = True
        End If
        'END - Added by JAN 07-24-2012


        If NextDayCheckBox.Checked = True Then
            Dim fromdatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            Dim todatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            v_from1 = DateAdd(DateInterval.Day, 1, fromdatetime)
            v_to1 = DateAdd(DateInterval.Day, 1, todatetime)
            v_to = v_to1 & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Else
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        End If

        If NextDayCheckBox.Checked = True Then
            Dim count1 As Decimal
            Dim count2 As Decimal
            count1 = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_from1, Date)) / 60.0 / 60.0
            count2 = DateDiff(DateInterval.Second, CType(v_to1, Date), CType(v_to, Date)) / 60.0 / 60.0
            v_count = count1 + count2
        Else
            v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
        End If

        v_count = Decimal.Round(v_count, 2)

        Label_NoOfHrs.Visible = True
        Labelnoofhours.Visible = True
        Labelnoofhours.Text = CType(v_count, String)
    End Sub

    Protected Sub lstTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTo.SelectedIndexChanged
        Dim v_from As String = txt_date_from.Text & " 12:00 AM"
        Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

        Dim v_count As Decimal

        Dim v_from1 As String = ""
        Dim v_to1 As String = ""

        'BEGIN - Added by JAN 07-24-2012
        Dim myDate As String = txt_date_from.Text
        If myDate = "" Then
            myDate = "01-Jan-1900"
        Else
            myDate = txt_date_from.Text

        End If

        Dim fromdate As Date = myDate & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        Dim todate As Date = myDate & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        'UserMsgBox(fromdate + todate)

        If fromdate < todate Then
            NextDayCheckBox.Checked = False
        Else
            NextDayCheckBox.Checked = True
        End If
        'END - Added by JAN 07-24-2012


        If NextDayCheckBox.Checked = True Then
            Dim fromdatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            Dim todatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            v_from1 = DateAdd(DateInterval.Day, 1, fromdatetime)
            v_to1 = DateAdd(DateInterval.Day, 1, todatetime)
            v_to = v_to1 & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Else
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        End If

        If NextDayCheckBox.Checked = True Then
            Dim count1 As Decimal
            Dim count2 As Decimal
            count1 = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_from1, Date)) / 60.0 / 60.0
            count2 = DateDiff(DateInterval.Second, CType(v_to1, Date), CType(v_to, Date)) / 60.0 / 60.0
            v_count = count1 + count2
        Else
            v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
        End If

        v_count = Decimal.Round(v_count, 2)

        Label_NoOfHrs.Visible = True
        Labelnoofhours.Visible = True
        Labelnoofhours.Text = CType(v_count, String)
    End Sub

    Protected Sub ampmTo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ampmTo.SelectedIndexChanged
        Dim v_from As String = txt_date_from.Text & " 12:00 AM"
        Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

        Dim v_count As Decimal

        Dim v_from1 As String = ""
        Dim v_to1 As String = ""

        'BEGIN - Added by JAN 07-24-2012
        Dim myDate As String = txt_date_from.Text
        If myDate = "" Then
            myDate = "01-Jan-1900"
        Else
            myDate = txt_date_from.Text

        End If

        Dim fromdate As Date = myDate & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        Dim todate As Date = myDate & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        'UserMsgBox(fromdate + todate)

        If fromdate < todate Then
            NextDayCheckBox.Checked = False
        Else
            NextDayCheckBox.Checked = True
        End If
        'END - Added by JAN 07-24-2012


        If NextDayCheckBox.Checked = True Then
            Dim fromdatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            Dim todatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            v_from1 = DateAdd(DateInterval.Day, 1, fromdatetime)
            v_to1 = DateAdd(DateInterval.Day, 1, todatetime)
            v_to = v_to1 & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Else
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        End If

        If NextDayCheckBox.Checked = True Then
            Dim count1 As Decimal
            Dim count2 As Decimal
            count1 = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_from1, Date)) / 60.0 / 60.0
            count2 = DateDiff(DateInterval.Second, CType(v_to1, Date), CType(v_to, Date)) / 60.0 / 60.0
            v_count = count1 + count2
        Else
            v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
        End If

        v_count = Decimal.Round(v_count, 2)

        Label_NoOfHrs.Visible = True
        Labelnoofhours.Visible = True
        Labelnoofhours.Text = CType(v_count, String)
    End Sub

    Protected Sub NextDayCheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NextDayCheckBox.CheckedChanged
        Dim v_from As String = txt_date_from.Text & " 12:00 AM"
        Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

        Dim v_count As Decimal

        Dim v_from1 As String = ""
        Dim v_to1 As String = ""

        If NextDayCheckBox.Checked = True Then
            Dim fromdatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            Dim todatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            v_from1 = DateAdd(DateInterval.Day, 1, fromdatetime)
            v_to1 = DateAdd(DateInterval.Day, 1, todatetime)
            v_to = v_to1 & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Else
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        End If

        If NextDayCheckBox.Checked = True Then
            Dim count1 As Decimal
            Dim count2 As Decimal
            count1 = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_from1, Date)) / 60.0 / 60.0
            count2 = DateDiff(DateInterval.Second, CType(v_to1, Date), CType(v_to, Date)) / 60.0 / 60.0
            v_count = count1 + count2
        Else
            v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
        End If

        v_count = Decimal.Round(v_count, 2)

        Label_NoOfHrs.Visible = True
        Labelnoofhours.Visible = True
        Labelnoofhours.Text = CType(v_count, String)
    End Sub

    Protected Sub GetNumberOfHours()
        Dim v_from As String = txt_date_from.Text & " 12:00 AM"
        Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Dim v_count As Decimal

        Dim v_from1 As String = ""
        Dim v_to1 As String = ""

        'BEGIN - Added by JAN 07-24-2012
        Dim myDate As String = txt_date_from.Text
        If myDate = "" Then
            myDate = "01-Jan-1900"
        Else
            myDate = txt_date_from.Text

        End If

        Dim fromdate As Date = myDate & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        Dim todate As Date = myDate & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        'UserMsgBox(fromdate + todate)

        If fromdate < todate Then
            NextDayCheckBox.Checked = False
        Else
            NextDayCheckBox.Checked = True
        End If
        'END - Added by JAN 07-24-2012



        If NextDayCheckBox.Checked = True Then
            Dim fromdatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            Dim todatetime As DateTime = txt_date_from.Text & " 12:00 AM"
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            v_from1 = DateAdd(DateInterval.Day, 1, fromdatetime)
            v_to1 = DateAdd(DateInterval.Day, 1, todatetime)
            v_to = v_to1 & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        Else
            v_from = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        End If

        If NextDayCheckBox.Checked = True Then
            Dim count1 As Decimal
            Dim count2 As Decimal
            count1 = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_from1, Date)) / 60.0 / 60.0
            count2 = DateDiff(DateInterval.Second, CType(v_to1, Date), CType(v_to, Date)) / 60.0 / 60.0
            v_count = count1 + count2
        Else
            v_count = DateDiff(DateInterval.Second, CType(v_from, Date), CType(v_to, Date)) / 60.0 / 60.0
        End If

        v_count = Decimal.Round(v_count, 2)

        Label_NoOfHrs.Visible = True
        Labelnoofhours.Visible = True
        Labelnoofhours.Text = CType(v_count, String)
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        get_employee_list()
        manage_lookups(Current_User.Employee_Type)
        get_overtime_details(Session("RefNo"))
        GetNumberOfHours()
        NextDayCheckBox.Enabled = False
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"

    Private Sub insert_ot_applications()
        Dim ref_no As String
        Dim li_count As Integer = GridView1.Rows.Count

        'Dim v_from As String
        'Dim v_to As String

        For i As Integer = 0 To GridView1.Rows.Count - 1
            Try
                'v_from = GridView1.Rows(i).Cells(3).Text & " " & GridView1.Rows(i).Cells(4).Text
                'v_to = GridView1.Rows(i).Cells(3).Text & " " & GridView1.Rows(i).Cells(5).Text

                'Dim GridDateFrom As Date = GridView1.Rows(i).Cells(3).Text & " " & GridView1.Rows(i).Cells(4).Text
                'Dim GridDateTo As Date = GridView1.Rows(i).Cells(3).Text & " " & GridView1.Rows(i).Cells(5).Text

                'If GridDateTo < GridDateFrom Then
                '    v_to = DateAdd(DateInterval.Day, 1, CType(GridView1.Rows(i).Cells(3).Text, Date)) & " " & GridView1.Rows(i).Cells(5).Text
                'End If

                'If ws.Validate_Duplicate_DateTime("", GridView1.Rows(i).Cells(1).Text, CType(v_from, Date), CType(v_to, Date), "OT") = True Then
                '    UserMsgBox("You have filed the same date with your previous application.")
                '    Exit Sub
                'End If

                'If i = 0 Then
                ref_no = ws.Update_OT_Applications(Session("RefNo"), GridView1.Rows(i).Cells(1).Text, _
                                                                            CType(GridView1.Rows(i).Cells(3).Text, Date), _
                                                                           GridView1.Rows(i).Cells(4).Text, _
                                                                           GridView1.Rows(i).Cells(5).Text, _
                                                                           IIf(GridView1.Rows(i).Cells(7).Text = "Yes", True, False), _
                                                                           GridView1.Rows(i).Cells(8).Text, _
                                                                           GridView1.Rows(i).Cells(9).Text, _
                                                                           GridView1.Rows(i).Cells(10).Text, _
                                                                           Current_User.Employee_ID)
                ws.Insert_Employees_Approvers(GridView1.Rows(i).Cells(1).Text, ref_no)
                ws.Update_Approve_Applications(ref_no, Current_User.Employee_ID, "OT")
                send_notifications(ref_no, GridView1.Rows(i).Cells(1).Text, GridView1.Rows(i).Cells(2).Text)

                'Else
                'ref_no = ws.Insert_OT_Applications(GridView1.Rows(i).Cells(1).Text, _
                '                                                            CType(GridView1.Rows(i).Cells(3).Text, Date), _
                '                                                           GridView1.Rows(i).Cells(4).Text, _
                '                                                           GridView1.Rows(i).Cells(5).Text, _
                '                                                           IIf(GridView1.Rows(i).Cells(6).Text = "Yes", True, False), _
                '                                                           GridView1.Rows(i).Cells(7).Text, _
                '                                                           GridView1.Rows(i).Cells(8).Text, _
                '                                                           GridView1.Rows(i).Cells(9).Text, _
                '                                                           Current_User.Employee_ID)
                'ws.Insert_Employees_Approvers(GridView1.Rows(i).Cells(1).Text, ref_no)
                'ws.Update_Approve_Applications(ref_no, Current_User.Employee_ID, "OT")
                'send_notifications(ref_no, GridView1.Rows(i).Cells(1).Text, GridView1.Rows(i).Cells(2).Text)

                'End If


            Catch ex As Exception
                UserMsgBox(ex.Message)
            End Try

        Next
    End Sub

#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Protected Sub search_employee_type(ByVal empname As String)
        For Each li As ListItem In dplEmployee.Items
            If li.Value = empname Then
                Me.dplEmployee.SelectedValue = li.Value
            End If
        Next
    End Sub

    Private Sub get_overtime_details(ByVal ref_no As String)
        Try

        Dim dr As DataRow = ws.Get_Overtime_Details(ref_no).Tables(0).Rows(0)
            With dr
                ' txt_name.Text = dr("employee_name")
                'txt_emp_id.Text = dr("employee_id")
                'txt_ref_no.Text = dr("ref_no")
                'txt_planner.Text = dr("planner")
                'txt_date_time.Text = dr("work_date")
                'txt_reasons.Text = dr("reason")
                'txt_on_call.Text = IIf(CType(dr("on_call"), Boolean) = True, "Yes", "No")
                'txt_date_filed.Text = dr("date_created")
                'txt_remarks.Text = dr("remarks")
                'txt_status.Text = dr("status")
                Lbl_name.Text = dr("employee_name")
                lbl_emp_id.Text = dr("employee_id")
                lbl_ref_no.Text = dr("ref_no")
                lbl_date_filed.Text = dr("date_created")
                lbl_status.Text = dr("status")
                dpl_classifications.SelectedValue = dr("classification_desc")
                dpl_reason.SelectedItem.Text = Trim(dr("reason"))

                dplEmployee.SelectedItem.Text = dr("employee_name")
                dplEmployee.Enabled = False

                txt_date_from.Text = dr("ot_date")
                txtRemarks.Text = dr("remarks")
                'dplFrom.SelectedItem.Text = Left(Trim(dr("time_from")), 2)
                'dplTo.SelectedItem.Text = Left(Trim(dr("time_to")), 2)

                dplFrom.Items.Insert(0, Left(Trim(dr("time_from")), 2)) '  = IIf(Hour(CType(dr("time_from"), DateTime)) > 12, Hour(CType(dr("time_from"), DateTime)) - 12 - 1, IIf(Hour(CType(dr("time_from"), DateTime)) = 12, Hour(CType(dr("time_from"), DateTime)), Hour(CType(dr("time_from"), DateTime)) - 1))
                dplTo.Items.Insert(0, Left(Trim(dr("time_to")), 2)) 'SelectedIndex = IIf(Hour(CType(dr("time_to"), Date)) > 12, Hour(CType(dr("time_to"), Date)) - 12 - 1, Hour(CType(dr("time_to"), Date)) - 1)

                lstFrom.Text = Right(Left(dr("time_from"), 5), 2)
                lstTo.Text = Right(Left(dr("time_to"), 5), 2)
                ampmFrom.Text = Right(dr("time_from"), 2)
                ampmTo.Text = Right(dr("time_to"), 2)
                chkOncall.Checked = dr("on_call")
                'search_employee_type(dr("employee_name"))
                dplEmployee.SelectedValue = dr("employee_id")
            End With
            manage_lookups(ws.Get_User_Info(lbl_emp_id.Text).Tables(0).Rows(0).Item("employee_type"))
            dpl_reason.SelectedValue = Trim(dr("reason"))
            dpl_reason.SelectedItem.Text = Trim(dr("reason"))
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try

    End Sub

    Private Sub get_employee_list()
        dplEmployee.Focus()
        dplEmployee.DataSource = ws.Get_User_List(Current_User.Employee_ID)
        dplEmployee.DataValueField = "employee_id"
        dplEmployee.DataTextField = "employee_name"
        dplEmployee.DataBind()
        dplEmployee.Items.Insert(0, Current_User.LastName & " " & Current_User.FirstName)
        If ws.Validate_Planner(Current_User.Employee_ID) = False Then
            dplEmployee.Enabled = False
        End If
    End Sub

    Private Sub lookup_esd_classifications(ByVal ESDReason As String)
        Dim ds As New DataSet
        ds = ws.LookUp_ESD_Classifications(ESDReason)
        If ds.Tables(0).Rows.Count > 0 Then
            'dpl_classifications.DataSource = ws.LookUp_ESD_Classifications(ESDReason)
            dpl_classifications.DataSource = ds.Tables(0)
            dpl_classifications.DataValueField = "classification"
            dpl_classifications.DataTextField = "classification"
            dpl_classifications.DataBind()
            If ds.Tables(0).Rows.Count = 2 Then
                dpl_classifications.Items.Insert(0, "")
            End If
        Else
            dpl_classifications.Items.Insert(0, "")
        End If

        'dpl_classifications.DataSource = ws.LookUp_ESD_Classifications(ESDReason)
        'dpl_classifications.DataValueField = "classification"
        'dpl_classifications.DataTextField = "classification"
        'dpl_classifications.DataBind()
        'dpl_classifications.Items.Insert(0, "")
    End Sub

    Private Sub lookup_esd_reasons(ByVal employee_id As String)
        dpl_reason.DataSource = ws.LookUp_ESD_Reasons()
        dpl_reason.DataValueField = "code"
        dpl_reason.DataTextField = "reason_desc"
        dpl_reason.DataBind()
        dpl_reason.Items.Insert(0, "")
    End Sub

    Private Sub lookup_assoc_reasons()
        dpl_reason.DataSource = ws.LookUp_ASSOC_Reasons()
        dpl_reason.DataValueField = "reason_desc"
        dpl_reason.DataTextField = "reason_desc"
        dpl_reason.DataBind()
        dpl_reason.Items.Insert(0, "")
    End Sub

#End Region

#Region "Delete"

    Private Sub delete_gridview_row(ByVal intRow As Integer)
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("employee_id"))
        dt.Columns.Add(New DataColumn("employee_name"))
        dt.Columns.Add(New DataColumn("date"))
        dt.Columns.Add(New DataColumn("time_from"))
        dt.Columns.Add(New DataColumn("time_to"))
        dt.Columns.Add(New DataColumn("otesd_hours"))
        dt.Columns.Add(New DataColumn("on_call"))
        dt.Columns.Add(New DataColumn("classification"))
        dt.Columns.Add(New DataColumn("reason"))
        dt.Columns.Add(New DataColumn("remarks"))
        For i As Integer = 0 To GridView1.Rows.Count - 1
            If Not i = intRow Then
                dr = dt.NewRow()
                dr("employee_id") = GridView1.Rows(i).Cells(1).Text
                dr("employee_name") = GridView1.Rows(i).Cells(2).Text
                dr("date") = GridView1.Rows(i).Cells(3).Text
                dr("time_from") = GridView1.Rows(i).Cells(4).Text
                dr("time_to") = GridView1.Rows(i).Cells(5).Text
                dr("otesd_hours") = GridView1.Rows(i).Cells(6).Text
                dr("on_call") = GridView1.Rows(i).Cells(7).Text
                dr("classification") = GridView1.Rows(i).Cells(8).Text
                dr("reason") = GridView1.Rows(i).Cells(9).Text
                dr("remarks") = GridView1.Rows(i).Cells(10).Text
                dt.Rows.Add(dr)
            End If
        Next
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

#End Region

#Region "Manage"

    Private Sub add_employee_to_list()

        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("employee_id"))
        dt.Columns.Add(New DataColumn("employee_name"))
        dt.Columns.Add(New DataColumn("date"))
        dt.Columns.Add(New DataColumn("time_from"))
        dt.Columns.Add(New DataColumn("time_to"))
        dt.Columns.Add(New DataColumn("otesd_hours"))
        dt.Columns.Add(New DataColumn("on_call"))
        dt.Columns.Add(New DataColumn("classification"))
        dt.Columns.Add(New DataColumn("reason"))
        dt.Columns.Add(New DataColumn("remarks"))

        If GridView1.Rows.Count > 0 Then
            For i As Integer = 0 To GridView1.Rows.Count - 1
                dr = dt.NewRow()
                dr("employee_id") = GridView1.Rows(i).Cells(1).Text
                dr("employee_name") = GridView1.Rows(i).Cells(2).Text
                dr("date") = GridView1.Rows(i).Cells(3).Text
                dr("time_from") = GridView1.Rows(i).Cells(4).Text
                dr("time_to") = GridView1.Rows(i).Cells(5).Text
                dr("otesd_hours") = GridView1.Rows(i).Cells(6).Text
                dr("on_call") = GridView1.Rows(i).Cells(7).Text
                dr("classification") = GridView1.Rows(i).Cells(8).Text
                dr("reason") = GridView1.Rows(i).Cells(9).Text
                dr("remarks") = GridView1.Rows(i).Cells(10).Text
                dt.Rows.Add(dr)
            Next
        End If

        dr = dt.NewRow()
        dr("employee_id") = IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)
        dr("employee_name") = dplEmployee.SelectedItem.Text
        dr("date") = txt_date_from.Text
        dr("time_from") = dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
        dr("time_to") = dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text
        dr("otesd_hours") = Labelnoofhours.Text
        dr("on_call") = IIf(chkOncall.Checked = True, "Yes", "No")
        dr("classification") = IIf(dpl_classifications.SelectedItem.Text = "", "-", dpl_classifications.SelectedItem.Text)
        dr("reason") = IIf(dpl_reason.SelectedItem.Text = "", "-", dpl_reason.SelectedItem.Text)
        dr("remarks") = IIf(txtRemarks.Text = "", "-", txtRemarks.Text)
        dt.Rows.Add(dr)

        If dplTo1.Enabled = True Then

            dr = dt.NewRow()
            dr("employee_id") = IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)
            dr("employee_name") = dplEmployee.SelectedItem.Text
            dr("date") = CType(DateAdd(DateInterval.Day, 1, CDate(txt_date_from.Text)), String)
            dr("time_from") = "12:00 AM"
            dr("time_to") = dplTo1.SelectedItem.Text & ":" & lstTo1.SelectedItem.Text & " " & ampmTo1.SelectedItem.Text
            dr("otesd_hours") = Labelnoofhours.Text
            dr("on_call") = IIf(chkOncall.Checked = True, "Yes", "No")
            dr("classification") = IIf(dpl_classifications.SelectedItem.Text = "", "-", dpl_classifications.SelectedItem.Text)
            dr("reason") = IIf(dpl_reason.SelectedItem.Text = "", "-", dpl_reason.SelectedItem.Text)
            dr("remarks") = IIf(txtRemarks.Text = "", "-", txtRemarks.Text)
            dt.Rows.Add(dr)

        End If


        GridView1.DataSource = dt
        GridView1.DataBind()

        BtnSaveExtend.Visible = False
        btnSave.Visible = True
        dplTo1.Enabled = False
        lstTo1.Enabled = False
        ampmTo1.Enabled = False

        btnSubmit.Visible = False
    End Sub

    Private Sub clear_fields()
        dplEmployee.SelectedIndex = 0
        txt_date_from.Text = ""
        dplFrom.SelectedIndex = 0
        lstFrom.SelectedIndex = 0
        ampmFrom.SelectedIndex = 0
        dplTo.SelectedIndex = 0
        lstFrom.SelectedIndex = 0
        ampmFrom.SelectedIndex = 0

        dplFrom1.SelectedIndex = 0
        lstFrom1.SelectedIndex = 0
        ampmFrom1.SelectedIndex = 0
        dplTo1.SelectedIndex = 0
        lstFrom1.SelectedIndex = 0
        ampmFrom1.SelectedIndex = 0

        chkOncall.Checked = False
        dpl_reason.SelectedIndex = 0
        dpl_classifications.SelectedIndex = 0
        txtRemarks.Text = ""
    End Sub

    Private Sub send_notifications(ByVal ref_no As String, ByVal employee_id As String, ByVal employee_name As String)
        Dim body As String
        Dim recipients As String = ""
        body = employee_name & " is applying for OVERTIME/ESD (Edited). For your approval, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        Dim ds As DataSet, level As String, r_level As String

        r_level = ws.Get_approvers_level(employee_id, Current_User.Employee_ID)

        If r_level = "FINAL" Or r_level = "INITIAL" Then
            level = "%"
        ElseIf r_level = "USER_FINAL" Then
            level = "FINAL"
        Else
            level = "INITIAL"
        End If

        'Added NOv 19 by Jan
        Dim ds1 As DataSet

        If r_level = "INITIAL" Then
            ds = ws.Get_Approvers_Email(employee_id, "FINAL")
            Dim ds_itself As DataSet = ws.Get_Users_Email(employee_id)
            Dim dr_itself As DataRow = ds_itself.Tables(0).Rows(0)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim dr As DataRow = ds.Tables(0).Rows(i)
                If dr("email") <> dr_itself("email") Then
                    Insert_Email_Summary_Notification(dr("email"), body, employee_name)
                End If
            Next
        Else
            'If r_level = "" Then
            '    ds = ws.Get_Approvers_Email(employee_id, "FINAL")
            'Else
            '    ds = ws.Get_Approvers_Email(employee_id, level)
            'End If

            If r_level = "" Then
                ds1 = ws.Get_Approvers_Email(employee_id, "INITIAL")

                If ds1.Tables(0).Rows.Count > 0 Then
                    ds = ws.Get_Approvers_Email(employee_id, "INITIAL")
                Else
                    ds = ws.Get_Approvers_Email(employee_id, "FINAL")
                End If
            Else
                ds = ws.Get_Approvers_Email(employee_id, level)
            End If

            Dim ds_itself As DataSet = ws.Get_Users_Email(employee_id)
            Dim dr_itself As DataRow = ds_itself.Tables(0).Rows(0)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim dr As DataRow = ds.Tables(0).Rows(i)
                If dr("email") <> dr_itself("email") Then
                    Insert_Email_Summary_Notification(dr("email"), body, employee_name)
                End If
            Next
        End If

        'COMMENT BY JAN NOV 19
        'ds = ws.Get_Approvers_Email(employee_id, level)
        'Dim ds_itself As DataSet = ws.Get_Users_Email(Current_User.Employee_ID)
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



        'txtRemarks.Text = recipients
        'cls_email.SendEmail(recipients, body, employee_name)
    End Sub

    Private Sub manage_lookups(ByVal ptype As String)
        If ptype = "M" Then
            dpl_classifications.Enabled = True
            'lookup_esd_classifications(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue))
            lookup_esd_reasons(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue))
            RequiredFieldValidator4.Enabled = True
        Else
            dpl_classifications.SelectedIndex = 0
            dpl_classifications.Enabled = False
            RequiredFieldValidator4.Enabled = False
            lookup_assoc_reasons()
        End If
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

    End Function



    
End Class
