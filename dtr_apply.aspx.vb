Imports System.Data
Imports cls_Email_Notifications
Imports HolcimDbClass
Imports System.Data.SqlClient
Partial Class dtr_apply
    Inherits System.Web.UI.Page
    Public ReadOnly Property Current_User() As Cls_UserProperty
        Get
            Return New Cls_UserProperty(ViewState("Employee_ID"))
        End Get
    End Property

    Private ws As New localhost.Service
    Private cls_email As New cls_Email_Notifications

    Private Sub get_employee_list()
        dplEmployee.DataSource = ws.Get_User_List(Current_User.Employee_ID)
        dplEmployee.DataValueField = "employee_id"
        dplEmployee.DataTextField = "employee_name"
        dplEmployee.DataBind()
        dplEmployee.Items.Insert(0, Current_User.LastName & " " & Current_User.FirstName)
        If ws.Validate_Planner(Current_User.Employee_ID) = False Then
            dplEmployee.Enabled = False
        Else
            dplEmployee.Enabled = True
        End If
    End Sub

    Private Function Get_New_RefNo(ByVal type As String) As String
        Dim sqlParam(1) As SqlParameter

        sqlParam(0) = New SqlParameter("@type", SqlDbType.VarChar, 15)
        sqlParam(0).Value = type

        sqlParam(1) = New SqlParameter("@nref_no", SqlDbType.VarChar, 15)
        sqlParam(1).Direction = ParameterDirection.Output

        ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_New_Application_Refno", sqlParam)
        Return sqlParam(1).Value
    End Function

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
            Else
                If GridView1.Rows.Count > 0 Then
                    dplEmployee.Enabled = False
                Else
                    dplEmployee.Enabled = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        get_employee_list()
        HiddenField_employeeid.Value = Current_User.Employee_ID
    End Sub

    Private Sub ClearFormFields()
        tran_date.Text = ""
        tran_type.SelectedIndex = 0
        reason.Text = ""
    End Sub
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

    Public Function Insert_DTR_Header(ByVal employeeID As String) As String

        Dim ds As New DataSet
        Dim sqlParam(2) As SqlParameter

        Dim pref_no As String = "91-" & Get_New_RefNo("91")

        sqlParam(0) = New SqlParameter("@ref_no", SqlDbType.VarChar, 15)
        sqlParam(0).Value = pref_no

        sqlParam(1) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(1).Value = employeeID

        sqlParam(2) = New SqlParameter("@planner_id", SqlDbType.VarChar, 15)
        sqlParam(2).Value = Current_User.Employee_ID

        ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Insert_DTR_Header", sqlParam)
        Return pref_no

    End Function

    Public Sub Insert_DTR_Details(ByVal ref_no As String, ByVal employee_id As String, _
                                        ByVal vtran_type As String, ByVal tran_date As Date, _
                                        ByVal reason As String, ByVal pay_basis As String, _
                                        ByVal payroll_frequency As String, ByVal branch_id As String)

        Dim ds As New DataSet
        Dim sqlParam(7) As SqlParameter

        sqlParam(0) = New SqlParameter("@ref_no", SqlDbType.VarChar, 15)
        sqlParam(0).Value = ref_no

        sqlParam(1) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(1).Value = employee_id

        Dim tran_type As String = ""

        If vtran_type = "LOG-IN" Then
            tran_type = "I"
        ElseIf vtran_type = "LOG-OUT" Then
            tran_type = "O"
        End If

        sqlParam(2) = New SqlParameter("@tran_type", SqlDbType.Char, 1)
        sqlParam(2).Value = tran_type

        sqlParam(3) = New SqlParameter("@tran_date", SqlDbType.DateTime)
        sqlParam(3).Value = tran_date

        sqlParam(4) = New SqlParameter("@reason", SqlDbType.Text)
        sqlParam(4).Value = reason

        sqlParam(5) = New SqlParameter("@pay_basis", SqlDbType.Char, 1)
        sqlParam(5).Value = pay_basis

        sqlParam(6) = New SqlParameter("@payroll_frequency", SqlDbType.Char, 1)
        sqlParam(6).Value = payroll_frequency

        sqlParam(7) = New SqlParameter("@branch_id", SqlDbType.VarChar, 15)
        sqlParam(7).Value = branch_id

        ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Insert_DTR_Details", sqlParam)
    End Sub


    Public Function Validate_LogboxParsedData(ByVal employee_id As String, ByVal tran_type As String, ByVal tran_date As Date) As Boolean
        Dim ds As New DataSet
        Dim sqlParam(3) As SqlParameter

        sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(0).Value = employee_id

        sqlParam(1) = New SqlParameter("@tran_type", SqlDbType.Char, 1)
        sqlParam(1).Value = tran_type

        sqlParam(2) = New SqlParameter("@tran_date", SqlDbType.DateTime)
        sqlParam(2).Value = tran_date

        sqlParam(3) = New SqlParameter("@result", SqlDbType.Bit)
        sqlParam(3).Direction = ParameterDirection.Output

        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Validate_LogboxParsedData", sqlParam)

        Return sqlParam(3).Value
    End Function


    'BTN_ADD
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If GridView1.Rows.Count > 0 Then
            For i As Integer = 0 To GridView1.Rows.Count - 1

                If (GridView1.Rows(i).Cells(1).Text = IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue) _
                    And CType(GridView1.Rows(i).Cells(3).Text, Date) = CType(tran_date.Text & " " & hh.SelectedItem.Text & ":" & mm.SelectedItem.Text & " " & ampm.SelectedItem.Text, Date) _
                    And GridView1.Rows(i).Cells(4).Text = tran_type.SelectedItem.ToString) Then
                    UserMsgBox("You have filed the same date, time and trasaction type!")
                    Exit Sub
                End If

            Next
        End If

        Dim EmployeeID As String = IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)
        Dim Transaction_Type As String = ""
        If tran_type.SelectedItem.Text = "LOG-IN" Then
            Transaction_Type = "I"
        ElseIf tran_type.SelectedItem.Text = "LOG-OUT" Then
            Transaction_Type = "O"
        End If

        Dim Transaction_Date As Date = CType(tran_date.Text & " " & hh.SelectedItem.Text & ":" & mm.SelectedItem.Text & " " & ampm.SelectedItem.Text, Date)


        If Validate_LogboxParsedData(EmployeeID, Transaction_Type, Transaction_Date) Then
            UserMsgBox("Time log and trasaction type already exist in your time log record!")
            Exit Sub
        End If

        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("employee_id"))
        dt.Columns.Add(New DataColumn("employee_name"))
        dt.Columns.Add(New DataColumn("tran_date"))
        dt.Columns.Add(New DataColumn("tran_type"))
        dt.Columns.Add(New DataColumn("reason"))

        If GridView1.Rows.Count > 0 Then
            For i As Integer = 0 To GridView1.Rows.Count - 1
                dr = dt.NewRow()
                dr("employee_id") = GridView1.Rows(i).Cells(1).Text
                dr("employee_name") = GridView1.Rows(i).Cells(2).Text
                dr("tran_date") = GridView1.Rows(i).Cells(3).Text
                dr("tran_type") = GridView1.Rows(i).Cells(4).Text
                dr("reason") = GridView1.Rows(i).Cells(5).Text
                dt.Rows.Add(dr)
            Next
        End If

        dr = dt.NewRow()
        dr("employee_id") = IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)
        dr("employee_name") = dplEmployee.SelectedItem.Text
        dr("tran_date") = tran_date.Text & " " & hh.SelectedItem.Text & ":" & mm.SelectedItem.Text & " " & ampm.SelectedItem.Text
        dr("tran_type") = tran_type.SelectedItem.ToString
        dr("reason") = reason.Text

        dt.Rows.Add(dr)
        GridView1.DataSource = dt
        GridView1.DataBind()


        If GridView1.Rows.Count > 0 Then
            dplEmployee.Enabled = False
        Else
            dplEmployee.Enabled = True
        End If

        ClearFormFields()
        btnSubmit.Visible = True
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Delete_index" Then
            delete_gridview_row(index)

            If GridView1.Rows.Count < 1 Then
                btnSubmit.Visible = False
            End If
        End If

        If GridView1.Rows.Count > 0 Then
            dplEmployee.Enabled = False
        Else
            dplEmployee.Enabled = True
        End If
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkDelete As LinkButton = CType(e.Row.FindControl("lnkDelete"), LinkButton)
            lnkDelete.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Private Sub delete_gridview_row(ByVal intRow As Integer)
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add(New DataColumn("employee_id"))
        dt.Columns.Add(New DataColumn("employee_name"))
        dt.Columns.Add(New DataColumn("tran_date"))
        dt.Columns.Add(New DataColumn("tran_type"))
        dt.Columns.Add(New DataColumn("reason"))
        For i As Integer = 0 To GridView1.Rows.Count - 1
            If Not i = intRow Then
                dr = dt.NewRow()
                dr("employee_id") = GridView1.Rows(i).Cells(1).Text
                dr("employee_name") = GridView1.Rows(i).Cells(2).Text
                dr("tran_date") = GridView1.Rows(i).Cells(3).Text
                dr("tran_type") = GridView1.Rows(i).Cells(4).Text
                dr("reason") = GridView1.Rows(i).Cells(5).Text
                dt.Rows.Add(dr)
            End If
        Next
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Protected Sub Insert_DTR_Logs(ByVal refno As String)
        Dim sqlParam(0) As SqlParameter

        sqlParam(0) = New SqlParameter("@refno", SqlDbType.VarChar, 30)
        sqlParam(0).Value = refno
        ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_InsertDTR_ApplicationLogs", sqlParam)

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim Refno As String = Insert_DTR_Header(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue))

        Dim ds As New DataSet
        Dim sqlParam(0) As SqlParameter
        sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 20)
        sqlParam(0).Value = IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue)
        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_EmployeePayDetails", sqlParam)

        Dim pay_basis As String = ds.Tables(0).Rows(0).Item("pay_basis")
        Dim payroll_frequency As String = ds.Tables(0).Rows(0).Item("pay_frequency")
        Dim branch_id As String = ds.Tables(0).Rows(0).Item("branch_id")


        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim row As GridViewRow = GridView1.Rows(i)
            Insert_DTR_Details(Refno, GridView1.Rows(i).Cells(1).Text, GridView1.Rows(i).Cells(4).Text, GridView1.Rows(i).Cells(3).Text, GridView1.Rows(i).Cells(5).Text, pay_basis, payroll_frequency, branch_id)
        Next

        Dim EmployeeName As String = IIf(dplEmployee.SelectedIndex = 0, Current_User.LastName & " " & Current_User.FirstName, dplEmployee.SelectedItem.Text)

        ws.Insert_Employees_Approvers(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), Refno)
        ws.Update_Approve_Applications(Refno, Current_User.Employee_ID, "DT")

        Dim r_level As String = ws.Get_approvers_level(IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), Current_User.Employee_ID)
        If r_level = "FINAL" Then
            Try
                Insert_DTR_Logs(Refno)
            Catch ex As Exception

            End Try
        End If

        send_notifications(Refno, IIf(dplEmployee.SelectedIndex = 0, Current_User.Employee_ID, dplEmployee.SelectedValue), EmployeeName)

        Response.Redirect("dtr_header.aspx")


    End Sub


    Private Sub send_notifications(ByVal ref_no As String, ByVal employee_id As String, ByVal employee_name As String)
        Dim body As String
        Dim recipients As String = ""
        body = employee_name & " is applying for DTR. For your approval, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        Dim ds As DataSet, level As String, r_level As String

        r_level = ws.Get_approvers_level(employee_id, Current_User.Employee_ID)

        If r_level = "FINAL" Or r_level = "INITIAL" Then
            level = "%"
        ElseIf r_level = "USER_FINAL" Then
            level = "FINAL"
        Else
            level = "INITIAL"
        End If


        'ds = ws.Get_Approvers_Email(employee_id, level)
        'Dim ds_itself As DataSet = ws.Get_Users_Email(employee_id)
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
        '        Else
        '            'FOR TESTING ONLY IN TEST ENVIRONMENT
        '            Insert_Email_Summary_Notification(dr("email"), body, employee_name)
        '        End If
        '    Else
        '        If dr("email") <> dr_itself("email") Then
        '            Insert_Email_Summary_Notification(dr("email"), body, employee_name)
        '            recipients = recipients & ", " & dr("email")
        '        Else
        '            'FOR TESTING ONLY IN TEST ENVIRONMENT
        '            Insert_Email_Summary_Notification(dr("email"), body, employee_name)
        '        End If
        '    End If
        'Next
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
        'txtRemarks.Text = recipients
        'cls_email.SendEmail(recipients, body, employee_name)
    End Sub
End Class
