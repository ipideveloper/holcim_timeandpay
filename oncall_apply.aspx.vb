Imports System.Data
Imports cls_Email_Notifications

Partial Class oncall_apply
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


    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Delete_index" Then
            delete_gridview_row(index)
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
                btnSubmit.Enabled = False
                insert_oncall_applications()
                ViewState("pressed") = ""
                Session("Employee_ID") = ViewState("Employee_ID")
                btnSubmit.Enabled = True
                Response.Redirect("oncall.aspx")
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub BtnSaveExtend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSaveExtend.Click
        Try
            txt_date_to.Text = txt_date_from.Text

            Dim v_from As String = txt_date_from.Text & " 12:00 AM" ' & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            Dim v_to As String = txt_date_from.Text & " " & dplTo1.SelectedItem.Text & ":" & lstTo1.SelectedItem.Text & " " & ampmTo1.SelectedItem.Text

            If CType(v_to, Date) < CType(v_from, Date) Then
                UserMsgBox("Invalid Entry! Extended Date Time To should not be earlier than Date Time From.")
                Exit Sub
            ElseIf CType(v_to, Date) = CType(v_from, Date) Then
                UserMsgBox("Invalid Entry! Extended Time To should not be equal to Time From.")
                Exit Sub
            End If

            If ws.Validate_Duplicate_DateTime("", IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), CType(v_from, Date), CType(v_to, Date), "OC") = True Then
                UserMsgBox("Your Extended time have an overlap date and time entry.")
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
                    UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Minimum of 1 hour is allowed to file On Call for Associate Employee")
                    Exit Sub
                End If
            Else
                If v_count < 4 Then
                    UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Less than 4 hours is not allowed to file On Call.")
                    Exit Sub
                    '     ElseIf v_count < 8 And v_count > 4 Then
                    '        UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Only 4 hours will be paid to this filed On Call.")
                    '   ElseIf v_count < 12 And v_count > 8 Then
                    '      UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Only 8 hours will be paid to this filed On Call.")
                    ' ElseIf v_count > 12 Then
                    '    UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Only 12 hours will be paid to this filed On Call.")
                End If
            End If

            add_employee_to_list()
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
            TextBoxexfrom.Visible = False
            TextBoxexto.Visible = False
            TextBoxexhhmmfrom.Visible = False
            TextBoxexhhmmto.Visible = False
            clear_fields()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            txt_date_to.Text = txt_date_from.Text

            Dim v_from As String = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

            If CType(v_to, Date) < CType(v_from, Date) Then
                UserMsgBox("Invalid Entry! Date Time To should not be earlier than Date Time From.")
                Exit Sub
            ElseIf CType(v_to, Date) = CType(v_from, Date) Then
                UserMsgBox("Invalid Entry! Time To should not be equal to Time From.")
                Exit Sub
            End If

            If ws.Validate_Duplicate_DateTime("", IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), CType(v_from, Date), CType(v_to, Date), "OC") = True Then
                UserMsgBox("You have filed the same date with your previous application.")
                Exit Sub
            End If

            Dim v_count As Decimal
            Dim det_1159 As String = dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

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

            v_count = Decimal.Round(v_count, 2)

            If ws.Get_User_Info(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)).Tables(0).Rows(0).Item("employee_type") = "A" Then
                If v_count < 1 Then
                    UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Minimum of 1 hour is allowed to file On Call for Associate Employee")
                    Exit Sub
                End If
            Else
                If v_count < 4 Then
                    UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Less than 4 hours is not allowed to file On Call.")
                    Exit Sub
                    '  ElseIf v_count < 8 And v_count > 4 Then
                    '      UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Only 4 hours will be paid to this filed On Call.")
                    '  ElseIf v_count < 12 And v_count > 8 Then
                    '     UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Only 8 hours will be paid to this filed On Call.")
                    ' ElseIf v_count > 12 Then
                    '    UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Only 12 hours will be paid to this filed On Call.")
                End If
            End If

            add_employee_to_list()
            clear_fields()
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub BtnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNO.Click
        Try
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
            TextBoxexfrom.Visible = False
            TextBoxexto.Visible = False
            TextBoxexhhmmfrom.Visible = False
            TextBoxexhhmmto.Visible = False

            txt_date_to.Text = txt_date_from.Text

            Dim v_from As String = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text
            Dim v_to As String = txt_date_from.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

            If CType(v_to, Date) < CType(v_from, Date) Then
                UserMsgBox("Invalid Entry! Date Time To should not be earlier than Date Time From.")

                Exit Sub
            ElseIf CType(v_to, Date) = CType(v_from, Date) Then
                UserMsgBox("Invalid Entry! Time To should not be equal to Time From.")

                Exit Sub
            End If

            If ws.Validate_Duplicate_DateTime("", IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), CType(v_from, Date), CType(v_to, Date), "OC") = True Then
                UserMsgBox("You have filed the same date with your previous application.")

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
                    UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Minimum of 1 hour is allowed to file On Call for Associate Employee")

                    Exit Sub
                End If
            Else
                If v_count < 4 Then
                    UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Less than 4 hours is not allowed to file On Call.")

                    Exit Sub
                    '        ElseIf v_count < 8 And v_count > 4 Then
                    '           UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Only 4 hours will be paid to this filed On Call.")
                    '      ElseIf v_count < 12 And v_count > 8 Then
                    '         UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Only 8 hours will be paid to this filed On Call.")
                    '    ElseIf v_count > 12 Then
                    '       UserMsgBox("OnCall hours = " & CType(v_count, String) & "; Only 12 hours will be paid to this filed On Call.")
                End If
            End If

            add_employee_to_list()
            clear_fields()
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
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
            BtnSaveExtend.Visible = True
            dplTo1.Focus()
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub txt_date_from_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt_date_from.TextChanged
        txt_date_to.Text = txt_date_from.Text
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        get_employee_list()
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"

    Private Sub insert_oncall_applications()
        Try
            For i As Integer = 0 To GridView1.Rows.Count - 1
                Dim ref_no As String = ws.Insert_Oncall_Header(GridView1.Rows(i).Cells(1).Text, _
                                                 CType(GridView1.Rows(i).Cells(3).Text, Date), _
                                                GridView1.Rows(i).Cells(4).Text, _
                                                GridView1.Rows(i).Cells(5).Text, _
                                                GridView1.Rows(i).Cells(6).Text, _
                                                Current_User.Employee_ID)
                ws.Insert_Employees_Approvers(GridView1.Rows(i).Cells(1).Text, ref_no)
                ws.Update_Approve_Applications(ref_no, Current_User.Employee_ID, "OC")
                send_notifications(ref_no, GridView1.Rows(i).Cells(1).Text, Server.HtmlDecode(GridView1.Rows(i).Cells(2).Text))
            Next
        Catch ex As Exception
            ViewState("pressed") = ""
        End Try
    End Sub

    Private Sub add_employee_to_list()
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("employee_id"))
        dt.Columns.Add(New DataColumn("employee_name"))
        dt.Columns.Add(New DataColumn("time_from"))
        dt.Columns.Add(New DataColumn("time_to"))
        dt.Columns.Add(New DataColumn("contacted_by"))
        dt.Columns.Add(New DataColumn("details"))

        If GridView1.Rows.Count > 0 Then
            For i As Integer = 0 To GridView1.Rows.Count - 1
                dr = dt.NewRow()
                dr("employee_id") = GridView1.Rows(i).Cells(1).Text
                dr("employee_name") = GridView1.Rows(i).Cells(2).Text
                dr("time_from") = GridView1.Rows(i).Cells(3).Text
                dr("time_to") = GridView1.Rows(i).Cells(4).Text
                dr("contacted_by") = GridView1.Rows(i).Cells(5).Text
                dr("details") = GridView1.Rows(i).Cells(6).Text
                dt.Rows.Add(dr)
            Next
        End If

        dr = dt.NewRow()
        dr("employee_id") = IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)
        dr("employee_name") = dplEmployee.SelectedItem.Text

        dr("time_from") = txt_date_from.Text & " " & dplFrom.SelectedItem.Text & ":" & lstFrom.SelectedItem.Text & " " & ampmFrom.SelectedItem.Text

        dr("time_to") = txt_date_to.Text & " " & dplTo.SelectedItem.Text & ":" & lstTo.SelectedItem.Text & " " & ampmTo.SelectedItem.Text

        dr("contacted_by") = IIf(txt_contact_by.Text = "", "-", txt_contact_by.Text)

        dr("details") = IIf(txt_details.Text = "", "-", txt_details.Text)
        dt.Rows.Add(dr)

        If dplTo1.Enabled = True Then

            dr = dt.NewRow()
            dr("employee_id") = IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)
            dr("employee_name") = dplEmployee.SelectedItem.Text
            dr("time_from") = CType(DateAdd(DateInterval.Day, 1, CType(txt_date_from.Text, Date)), String) & " " & "12:00 AM"

            dr("time_to") = CType(DateAdd(DateInterval.Day, 1, CType(txt_date_from.Text, Date)), String) & " " & dplTo1.SelectedItem.Text & ":" & lstTo1.SelectedItem.Text & " " & ampmTo1.SelectedItem.Text

            dr("contacted_by") = IIf(txt_contact_by.Text = "", "-", txt_contact_by.Text)
            dr("details") = IIf(txt_details.Text = "", "-", txt_details.Text)
            dt.Rows.Add(dr)

        End If

        GridView1.DataSource = dt
        GridView1.DataBind()

        BtnSaveExtend.Visible = False
        btnSave.Visible = True
        dplTo1.Enabled = False
        lstTo1.Enabled = False
        ampmTo1.Enabled = False

        btnSubmit.Visible = True

    End Sub

#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub get_employee_list()
        dplEmployee.DataSource = ws.Get_User_List(Current_User.Employee_ID)
        dplEmployee.DataValueField = "employee_id"
        dplEmployee.DataTextField = "employee_name"
        dplEmployee.DataBind()
        dplEmployee.Items.Insert(0, Current_User.LastName & " " & Current_User.FirstName)
        If ws.Validate_Planner(Current_User.Employee_ID) = False Then
            dplEmployee.Enabled = False
        End If
    End Sub

#End Region

#Region "Delete"

    Private Sub delete_gridview_row(ByVal intRow As Integer)
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("employee_id"))
        dt.Columns.Add(New DataColumn("employee_name"))
        dt.Columns.Add(New DataColumn("time_from"))
        dt.Columns.Add(New DataColumn("time_to"))
        dt.Columns.Add(New DataColumn("contacted_by"))
        dt.Columns.Add(New DataColumn("details"))

        For i As Integer = 0 To GridView1.Rows.Count - 1
            If Not i = intRow Then
                dr = dt.NewRow()
                dr("employee_id") = GridView1.Rows(i).Cells(1).Text
                dr("employee_name") = GridView1.Rows(i).Cells(2).Text
                dr("time_from") = GridView1.Rows(i).Cells(3).Text
                dr("time_to") = GridView1.Rows(i).Cells(4).Text
                dr("contacted_by") = GridView1.Rows(i).Cells(5).Text
                dr("details") = GridView1.Rows(i).Cells(6).Text
                dt.Rows.Add(dr)
            End If
        Next
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

#End Region

#Region "Manage"

    Private Sub clear_fields()
        dplEmployee.SelectedIndex = 0
        txt_date_from.Text = ""
        txt_date_to.Text = ""
        dplFrom.SelectedIndex = 0
        lstFrom.SelectedIndex = 0
        ampmFrom.SelectedIndex = 0
        dplTo.SelectedIndex = 0
        lstFrom.SelectedIndex = 0
        ampmFrom.SelectedIndex = 0
        txt_contact_by.Text = ""
        txt_details.Text = ""

        dplFrom1.SelectedIndex = 0
        lstFrom1.SelectedIndex = 0
        ampmFrom1.SelectedIndex = 0
        dplTo1.SelectedIndex = 0
        lstFrom1.SelectedIndex = 0
        ampmFrom1.SelectedIndex = 0

    End Sub

#End Region

#Region "Javascripts"

    Private Sub send_notifications(ByVal ref_no As String, ByVal employee_id As String, ByVal employee_name As String)
        Dim body As String
        Dim recipients As String = ""
        body = employee_name & " is applying for ON CALL. For your approval, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        Dim ds As DataSet, level As String, r_level As String

        r_level = ws.Get_approvers_level(employee_id, Current_User.Employee_ID)

        'If r_level = "FINAL" Or r_level = "INITIAL" Then
       If r_level = "FINAL" Or r_level = "INITIAL" Then
            level = "%"
            '    recipients = ws.Get_users_Email_byRefno(ref_no)
        ElseIf r_level = "USER_FINAL" Then
            level = "FINAL"
        Else
            level = "INITIAL"
        End If


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
        'txt_details.Text = recipients
        'cls_email.SendEmail(recipients, body, employee_name)

    End Sub

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
