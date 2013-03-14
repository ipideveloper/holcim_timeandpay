Imports System.Data
Imports System
Imports cls_Email_Notifications

Partial Class shift_details
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

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            get_shift_schedules(Session("shift_month"), Session("shift_year"))
            lookups_shift_code()
            Timer1.Enabled = False
            Dim v_status As String = ws.Get_ShiftSchedule_Status(Current_User.Employee_ID, Session("shift_month"), Session("shift_year"))
            If Not v_status = "" Then
                lbl_status.Text = "Status = " & v_status
            End If
            If v_status = "DRAFT" Or v_status = "PLEASE REVISE" Then
                btn_submit.Enabled = True
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridView2_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Delete_index" Then
            delete_temp_shift(GridView2.Rows(index).Cells(2).Text & GridView2.Rows(index).Cells(3).Text)
            If GridView2.Rows.Count < 1 Then
                btn_save.Enabled = False
                btn_submit.Enabled = False
            End If
        End If
    End Sub

    Protected Sub GridView2_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            lnkDelete.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        get_shift_schedules(lbl_month.Text, lbl_year.Text)
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            If e.CommandName = "cmd1" Then
                get_selected_row(index, "1")
            ElseIf e.CommandName = "cmd2" Then
                get_selected_row(index, "2")
            ElseIf e.CommandName = "cmd3" Then
                get_selected_row(index, "3")
            ElseIf e.CommandName = "cmd4" Then
                get_selected_row(index, "4")
            ElseIf e.CommandName = "cmd5" Then
                get_selected_row(index, "5")
            ElseIf e.CommandName = "cmd6" Then
                get_selected_row(index, "6")
            ElseIf e.CommandName = "cmd7" Then
                get_selected_row(index, "7")
            ElseIf e.CommandName = "cmd8" Then
                get_selected_row(index, "8")
            ElseIf e.CommandName = "cmd9" Then
                get_selected_row(index, "9")
            ElseIf e.CommandName = "cmd10" Then
                get_selected_row(index, "10")
            ElseIf e.CommandName = "cmd11" Then
                get_selected_row(index, "11")
            ElseIf e.CommandName = "cmd12" Then
                get_selected_row(index, "12")
            ElseIf e.CommandName = "cmd13" Then
                get_selected_row(index, "13")
            ElseIf e.CommandName = "cmd14" Then
                get_selected_row(index, "14")
            ElseIf e.CommandName = "cmd15" Then
                get_selected_row(index, "15")
            ElseIf e.CommandName = "cmd16" Then
                get_selected_row(index, "16")
            ElseIf e.CommandName = "cmd17" Then
                get_selected_row(index, "17")
            ElseIf e.CommandName = "cmd18" Then
                get_selected_row(index, "18")
            ElseIf e.CommandName = "cmd19" Then
                get_selected_row(index, "19")
            ElseIf e.CommandName = "cmd20" Then
                get_selected_row(index, "20")
            ElseIf e.CommandName = "cmd21" Then
                get_selected_row(index, "21")
            ElseIf e.CommandName = "cmd22" Then
                get_selected_row(index, "22")
            ElseIf e.CommandName = "cmd23" Then
                get_selected_row(index, "23")
            ElseIf e.CommandName = "cmd24" Then
                get_selected_row(index, "24")
            ElseIf e.CommandName = "cmd25" Then
                get_selected_row(index, "25")
            ElseIf e.CommandName = "cmd26" Then
                get_selected_row(index, "26")
            ElseIf e.CommandName = "cmd27" Then
                get_selected_row(index, "27")
            ElseIf e.CommandName = "cmd28" Then
                get_selected_row(index, "28")
            ElseIf e.CommandName = "cmd29" Then
                get_selected_row(index, "29")
            ElseIf e.CommandName = "cmd30" Then
                get_selected_row(index, "30")
            ElseIf e.CommandName = "cmd31" Then
                get_selected_row(index, "31")
            End If

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim lnk_1 As LinkButton = CType(e.Row.FindControl("lnk_1"), LinkButton)
            lnk_1.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_2 As LinkButton = CType(e.Row.FindControl("lnk_2"), LinkButton)
            lnk_2.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_3 As LinkButton = CType(e.Row.FindControl("lnk_3"), LinkButton)
            lnk_3.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_4 As LinkButton = CType(e.Row.FindControl("lnk_4"), LinkButton)
            lnk_4.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_5 As LinkButton = CType(e.Row.FindControl("lnk_5"), LinkButton)
            lnk_5.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_6 As LinkButton = CType(e.Row.FindControl("lnk_6"), LinkButton)
            lnk_6.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_7 As LinkButton = CType(e.Row.FindControl("lnk_7"), LinkButton)
            lnk_7.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_8 As LinkButton = CType(e.Row.FindControl("lnk_8"), LinkButton)
            lnk_8.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_9 As LinkButton = CType(e.Row.FindControl("lnk_9"), LinkButton)
            lnk_9.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_10 As LinkButton = CType(e.Row.FindControl("lnk_10"), LinkButton)
            lnk_10.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_11 As LinkButton = CType(e.Row.FindControl("lnk_11"), LinkButton)
            lnk_11.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_12 As LinkButton = CType(e.Row.FindControl("lnk_12"), LinkButton)
            lnk_12.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_13 As LinkButton = CType(e.Row.FindControl("lnk_13"), LinkButton)
            lnk_13.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_14 As LinkButton = CType(e.Row.FindControl("lnk_14"), LinkButton)
            lnk_14.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_15 As LinkButton = CType(e.Row.FindControl("lnk_15"), LinkButton)
            lnk_15.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_16 As LinkButton = CType(e.Row.FindControl("lnk_16"), LinkButton)
            lnk_16.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_17 As LinkButton = CType(e.Row.FindControl("lnk_17"), LinkButton)
            lnk_17.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_18 As LinkButton = CType(e.Row.FindControl("lnk_18"), LinkButton)
            lnk_18.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_19 As LinkButton = CType(e.Row.FindControl("lnk_19"), LinkButton)
            lnk_19.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_20 As LinkButton = CType(e.Row.FindControl("lnk_20"), LinkButton)
            lnk_20.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_21 As LinkButton = CType(e.Row.FindControl("lnk_21"), LinkButton)
            lnk_21.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_22 As LinkButton = CType(e.Row.FindControl("lnk_22"), LinkButton)
            lnk_22.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_23 As LinkButton = CType(e.Row.FindControl("lnk_23"), LinkButton)
            lnk_23.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_24 As LinkButton = CType(e.Row.FindControl("lnk_24"), LinkButton)
            lnk_24.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_25 As LinkButton = CType(e.Row.FindControl("lnk_25"), LinkButton)
            lnk_25.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_26 As LinkButton = CType(e.Row.FindControl("lnk_26"), LinkButton)
            lnk_26.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_27 As LinkButton = CType(e.Row.FindControl("lnk_27"), LinkButton)
            lnk_27.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_28 As LinkButton = CType(e.Row.FindControl("lnk_28"), LinkButton)
            lnk_28.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_29 As LinkButton = CType(e.Row.FindControl("lnk_29"), LinkButton)
            lnk_29.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_30 As LinkButton = CType(e.Row.FindControl("lnk_30"), LinkButton)
            lnk_30.CommandArgument = e.Row.RowIndex.ToString

            Dim lnk_31 As LinkButton = CType(e.Row.FindControl("lnk_31"), LinkButton)
            lnk_31.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub btn_save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_save.Click
        Try
            '    If ViewState("pressed") = "" Then
            'ViewState("pressed") = "True"
            save_shedules(0)
            get_shift_schedules(lbl_month.Text, lbl_year.Text)
            insert_shift_for_approval(0)
            Dim v_status As String = ws.Get_ShiftSchedule_Status(Current_User.Employee_ID, lbl_month.Text, lbl_year.Text)
            If Not v_status = "" Then
                lbl_status.Text = "Status = " & v_status
            End If

            btn_save.Enabled = False
            txt_employee_id.Text = ""
            'txt_employee_name.Text = ""
            txt_day.Text = ""
            txt_current_shift.Text = ""
            dpl_new_shift.SelectedIndex = 0
            'ViewState("pressed") = ""
            'End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btn_submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_submit.Click
        Try
            'If ViewState("pressed") = "" Then
            'ViewState("pressed") = "True"
            save_shedules(1)
            get_shift_schedules(lbl_month.Text, lbl_year.Text)


            insert_shift_for_approval(1)



            clear_controls()
            Dim v_status As String = ws.Get_ShiftSchedule_Status(Current_User.Employee_ID, lbl_month.Text, lbl_year.Text)
            If Not v_status = "" Then
                lbl_status.Text = "Status = " & v_status
            End If
            send_notifications(Current_User.Employee_ID, LCase(Current_User.FirstName) & " " & UCase(Current_User.LastName))
            'ViewState("pressed") = ""
            'End If
            GridView2.DataSource = ws.Get_Change_Shift_Schedules(Current_User.Employee_ID, Session("shift_month"), Session("shift_year"))
            GridView2.DataBind()
            If GridView2.Rows.Count > 0 Then
                btn_save.Enabled = True
                btn_submit.Enabled = True
            Else
                btn_save.Enabled = False
                btn_submit.Enabled = False
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        Try

        
            ViewState("Employee_ID") = Session("Employee_ID")
            '  txt_employee_id.Text = Current_User.Employee_ID & "|" & Session("shift_month") & "|" & Session("shift_year")

            GridView2.DataSource = ws.Get_Change_Shift_Schedules(Current_User.Employee_ID, Session("shift_month"), Session("shift_year"))
            GridView2.DataBind()
            If GridView2.Rows.Count > 0 Then
                btn_save.Enabled = True
                btn_submit.Enabled = True
            Else
                btn_save.Enabled = False
                btn_submit.Enabled = False
            End If
        Catch ex As Exception
            'usermsgbox(ex.Message)
        End Try

    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"

    Private Sub add_temp_shift()
        Dim dt As New DataTable
        Dim dr As DataRow

        dt.Columns.Add(New DataColumn("employee_name"))
        dt.Columns.Add(New DataColumn("employee_id"))
        dt.Columns.Add(New DataColumn("day"))
        dt.Columns.Add(New DataColumn("shift_from"))
        dt.Columns.Add(New DataColumn("shift_to"))

        If GridView2.Rows.Count > 0 Then
            For i As Integer = 0 To GridView2.Rows.Count - 1

                If GridView2.Rows(i).Cells(2).Text = txt_employee_id.Text And GridView2.Rows(i).Cells(3).Text = txt_day.Text Then
                    Exit Sub
                End If
                Dim varName As String = ""
                dr = dt.NewRow()
                varName = GridView2.Rows(i).Cells(1).Text
                varName = Replace(varName, "&#209;", Chr(209))
                varName = Replace(varName, "&#227;", Chr(227))
                varName = Replace(varName, "&#241;", Chr(241))
                dr("employee_name") = varName
                dr("employee_id") = GridView2.Rows(i).Cells(2).Text
                dr("day") = GridView2.Rows(i).Cells(3).Text
                dr("shift_from") = GridView2.Rows(i).Cells(4).Text
                dr("shift_to") = GridView2.Rows(i).Cells(5).Text
                dt.Rows.Add(dr)
            Next
        End If
        dr = dt.NewRow()
        '        dr("employee_name") = txt_employee_name.Text
        dr("employee_id") = txt_employee_id.Text
        dr("day") = txt_day.Text
        dr("shift_from") = txt_current_shift.Text
        dr("shift_to") = dpl_new_shift.SelectedItem.Text
        dt.Rows.Add(dr)

        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If dpl_new_shift.SelectedItem.Text = "" Then
                Exit Sub
            ElseIf txt_current_shift.Text = dpl_new_shift.SelectedItem.Text Then
                Exit Sub
            End If
            add_temp_shift()
            btn_save.Enabled = True
            btn_submit.Enabled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub save_shedules(ByVal status As Integer)
        For i As Integer = 0 To GridView2.Rows.Count - 1
            ws.Insert_ShiftSched_Header(GridView2.Rows(i).Cells(5).Text, lbl_month.Text, GridView2.Rows(i).Cells(3).Text, GridView2.Rows(i).Cells(2).Text, Current_User.Employee_ID, status, lbl_year.Text, Current_User.Employee_ID)
        Next
    End Sub

    Private Sub insert_shift_for_approval(ByVal status As Integer)
        ws.Insert_Shift_Approval2(Current_User.Employee_ID, lbl_month.Text, status, lbl_year.Text, Current_User.Employee_ID)
    End Sub

#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub get_shift_schedules(ByVal month As Integer, ByVal pyear As Integer)

        ws.Get_Employee_Shift_Schedules(Current_User.Employee_ID, month, pyear)
        GridView1.DataSource = ws.Get_Shift_Details_Temp(Current_User.Employee_ID)
        GridView1.DataBind()
        lbl_month.Text = month
        lbl_year.Text = pyear
        Dim month_name As String = ""
        Select Case month
            Case 1
                month_name = "January"
            Case 2
                month_name = "February"
            Case 3
                month_name = "March"
            Case 4
                month_name = "April"
            Case 5
                month_name = "May"
            Case 6
                month_name = "June"
            Case 7
                month_name = "July"
            Case 8
                month_name = "August"
            Case 9
                month_name = "September"
            Case 10
                month_name = "October"
            Case 11
                month_name = "November"
            Case 12
                month_name = "December"
        End Select
        lbl_header.Text = "Shift Schedule for the month of " & month_name
    End Sub

    Private Sub lookups_shift_code()

        dpl_new_shift.DataSource = ws.LookUp_Shift_Code(Current_User.Employee_ID)
        dpl_new_shift.DataTextField = "shift_code"
        dpl_new_shift.DataValueField = "shift_code"
        dpl_new_shift.DataBind()
        dpl_new_shift.Items.Insert(0, "")
    End Sub

    Private Sub get_selected_row(ByVal gvrows_count As Integer, ByVal count As String)
        Dim row As GridViewRow = GridView1.Rows(gvrows_count)
        Dim lnk_ref As LinkButton = CType(row.FindControl("lnk_" & count), LinkButton)

        'txt_employee_name.Text = Replace(GridView1.Rows(gvrows_count).Cells(0).Text, "&#209;", Chr(209))
        'txt_employee_name.Text = Replace(txt_employee_name.Text, "&#227;", Chr(227))
        'txt_employee_name.Text = Replace(txt_employee_name.Text, "&#241;", Chr(241))
        txt_employee_id.Text = GridView1.Rows(gvrows_count).Cells(1).Text
        txt_day.Text = count
        txt_current_shift.Text = lnk_ref.Text
        dpl_new_shift.SelectedIndex = 0
        btnSave.Enabled = True
    End Sub

#End Region

#Region "Delete"

    Private Sub delete_temp_shift(ByVal row_delete_id As String)
        Dim dt As New DataTable
        Dim dr As DataRow

        dt.Columns.Add(New DataColumn("employee_name"))
        dt.Columns.Add(New DataColumn("employee_id"))
        dt.Columns.Add(New DataColumn("day"))
        dt.Columns.Add(New DataColumn("shift_from"))
        dt.Columns.Add(New DataColumn("shift_to"))

        For i As Integer = 0 To GridView2.Rows.Count - 1
            If Not row_delete_id = GridView2.Rows(i).Cells(2).Text & GridView2.Rows(i).Cells(3).Text Then
                Dim row As GridViewRow = GridView2.Rows(i)
                dr = dt.NewRow()
                dr("employee_name") = GridView2.Rows(i).Cells(1).Text
                dr("employee_id") = GridView2.Rows(i).Cells(2).Text
                dr("day") = GridView2.Rows(i).Cells(3).Text
                dr("shift_from") = GridView2.Rows(i).Cells(4).Text
                dr("shift_to") = GridView2.Rows(i).Cells(5).Text
                dt.Rows.Add(dr)
            End If
        Next
        GridView2.DataSource = dt
        GridView2.DataBind()
    End Sub

#End Region

#Region "Manage"

    Private Sub clear_controls()
        GridView2.DataSource = Nothing
        GridView2.DataBind()
        btn_save.Enabled = False
        btn_submit.Enabled = False
        btnSave.Enabled = False
        txt_employee_id.Text = ""
        txt_day.Text = ""
        '        txt_employee_name.Text = ""
        txt_current_shift.Text = ""
        dpl_new_shift.SelectedIndex = 0
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


    Private Sub send_notifications(ByVal planner_id As String, ByVal planner_name As String)
        Dim body As String
        Dim recipients As String = ""
        body = planner_name & " is sending you a SHIFT SCHEDULE. For your review and approval. <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        Dim ds As DataSet
        ds = ws.Get_Approvers_Email(GridView1.Rows(0).Cells(1).Text, "FINAL")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim dr As DataRow = ds.Tables(0).Rows(i)
            If recipients = "" Then
                recipients = dr("email")
                Insert_Email_Summary_Notification(dr("email"), body, planner_name)
            Else
                Insert_Email_Summary_Notification(dr("email"), body, planner_name)
                recipients = recipients & ", " & dr("email")
            End If
        Next
        cls_email.SendEmail(recipients, body, planner_name)
    End Sub

#End Region

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Response.Redirect("UploadShift.aspx")

    End Sub
End Class
