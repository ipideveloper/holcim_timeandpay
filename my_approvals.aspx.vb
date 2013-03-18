Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient
Imports cls_Email_Notifications
Imports System.Net.Mail


Partial Class my_approvals
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

        DpMyApprovals.Visible = False
        Btnmyappsort.Visible = False

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1))
        Response.Cache.SetNoStore()




    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)
            Dim lnk_ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
            Dim h_app_type As HiddenField = CType(row.FindControl("h_app_type"), HiddenField)

            'Code from original code use by andrew 10-10-2011 start
            If e.CommandName = "cmd_ref_no" Then
                If h_app_type.Value = "LV" Then
                    lnk_ref_no.Attributes.Add("onclick", "javascript:mywin=self.open('leave_details.aspx?ref_no=" & lnk_ref_no.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=480,width=735,left=1,top=1');mywin.focus();return false;")
                    '    OpenModalPage(lnk_ref_no.Text, "leave_details.aspx", "480", "735")
                ElseIf h_app_type.Value = "OB" Then
                    lnk_ref_no.Attributes.Add("onclick", "javascript:mywin=self.open('ob_details.aspx?ref_no=" & lnk_ref_no.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=600,width=800,left=1,top=1');mywin.focus();return false;")

                    '    OpenModalPage(lnk_ref_no.Text, "ob_details.aspx", "600", "800")
                ElseIf h_app_type.Value = "OC" Then
                    lnk_ref_no.Attributes.Add("onclick", "javascript:mywin=self.open('oncall_details.aspx?ref_no=" & lnk_ref_no.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=450,width=450,left=1,top=1');mywin.focus();return false;")
                    '   OpenModalPage(lnk_ref_no.Text, "oncall_details.aspx", "450", "450")
                ElseIf h_app_type.Value = "OT" Then
                    lnk_ref_no.Attributes.Add("onclick", "javascript:mywin=self.open('overtime_details.aspx?ref_no=" & lnk_ref_no.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=480,width=500,left=1,top=1');mywin.focus();return false;")
                    '  OpenModalPage(lnk_ref_no.Text, "overtime_details.aspx", "480", "500")
                ElseIf h_app_type.Value = "DT" Then
                    lnk_ref_no.Attributes.Add("onclick", "javascript:mywin=self.open('dtr_details.aspx?ref_no=" & lnk_ref_no.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=480,width=500,left=1,top=1');mywin.focus();return false;")
                    '  OpenModalPage(lnk_ref_no.Text, "overtime_details.aspx", "480", "500")

                End If
            End If
            'code from original code use by andrew 10-10-2011 end
        Catch ex As Exception
        End Try
    End Sub

    'Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
    '    Try
    '        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
    '        Dim row As GridViewRow = GridView1.Rows(index)
    '        Dim ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
    '        Dim h_app_type As HiddenField = CType(row.FindControl("h_app_type"), HiddenField)
    '        If e.CommandName = "cmd_approve" Then
    '            Dim result As Integer

    '            result = ws.Validate_Applied_Dates(ref_no.Text)

    '            If result = 1 Then
    '                ws.Update_Approve_Applications(ref_no.Text, Current_User.Employee_ID, h_app_type.Value)
    '                send_notifications(ref_no.Text, ws.Get_users_ID_byRefno(ref_no.Text), Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has already approved")
    '                bind_approvals()
    '                UserMsgBox("Successfully Approved!")
    '            ElseIf result = 0 Then
    '                UserMsgBox("Approval not allowed at this time since payroll processing is on going.  You may resume approval of this transaction after payroll processing date.")
    '            ElseIf result = 3 Then
    '                UserMsgBox("Error Select payroll period")
    '            ElseIf result = 4 Then
    '                UserMsgBox("Error Select area code")
    '            ElseIf result = 5 Then
    '                UserMsgBox("No payarea code")
    '            ElseIf result = 6 Then
    '                UserMsgBox("Error Select on refno")
    '            ElseIf result = 7 Then
    '                UserMsgBox("Employee should have atleast 1 Final approver")
    '            Else
    '                UserMsgBox("Unable to approve, no message")
    '            End If

    '        ElseIf e.CommandName = "cmd_ref_no" Then
    '            If h_app_type.Value = "LV" Then
    '                OpenModalPage(ref_no.Text, "leave_details.aspx", "480", "735")
    '            ElseIf h_app_type.Value = "OB" Then
    '                OpenModalPage(ref_no.Text, "ob_details.aspx", "600", "800")
    '            ElseIf h_app_type.Value = "OC" Then
    '                OpenModalPage(ref_no.Text, "oncall_details.aspx", "450", "450")
    '            Else
    '                OpenModalPage(ref_no.Text, "overtime_details.aspx", "480", "500")
    '            End If
    '        Else
    '            ws.Update_Disapproved_Applications(ref_no.Text, Current_User.Employee_ID, h_app_type.Value)
    '            send_notifications(ref_no.Text, ws.Get_users_ID_byRefno(ref_no.Text), Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has disapproved")
    '            bind_approvals()
    '            UserMsgBox("Successfully Disapproved!")
    '        End If
    '    Catch ex As Exception
    '        UserMsgBox(ex.Message)
    '    End Try
    'End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            'Dim lnk_approve As LinkButton = CType(e.Row.FindControl("lnk_approve"), LinkButton)
            'lnk_approve.CommandArgument = e.Row.RowIndex.ToString
            'Dim lnk_dis_approve As LinkButton = CType(e.Row.FindControl("lnk_dis_approve"), LinkButton)
            'lnk_dis_approve.CommandArgument = e.Row.RowIndex.ToString
            Dim lnk_ref_no As LinkButton = CType(e.Row.FindControl("lnk_ref_no"), LinkButton)
            lnk_ref_no.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub


#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        bind_approvals()
    End Sub

#End Region




#Region "Retrieve"

    Private Sub bind_approvals()
        Try
            GridView1.DataSource = ws.Get_My_Approvals(Current_User.Employee_ID)
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                Btnmyappsort.Enabled = True
                DpMyApprovals.Enabled = True
                Btnmyappsort.Visible = True
                DpMyApprovals.Visible = True
            Else
                Btnmyappsort.Enabled = False
                DpMyApprovals.Enabled = False
                Btnmyappsort.Visible = False
                DpMyApprovals.Visible = False
            End If
            If GridView1.Rows.Count = 0 Then
                UserMsgBox("No Pending Application for Approval")
                btn_SelectAll.Visible = False
                btn_Approved.Visible = False
                btn_Disapprove.Visible = False
            Else
                btn_SelectAll.Visible = True
                btn_Approved.Visible = True
                btn_Disapprove.Visible = True
            End If
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try

    End Sub

#End Region



#Region "Manage"
    Private Sub SendEmail(ByVal recipient As String, ByVal body As String, ByVal SenderName As String)
        Try
            Dim SMPTServer As String = System.Configuration.ConfigurationManager.AppSettings("SMPTServer")
            Dim DefURL As String = ConfigurationManager.AppSettings("DefaultURL").ToString
            Dim HtmlBody As String
            Dim Mailmsg As New MailMessage("Time&Pay-PHL@holcim.com", recipient)
            Mailmsg.Subject = "Time and Pay Notice - " & SenderName
            HtmlBody = "BodyPart<br><br><br><br>You can access this application at the intranet or through this Link:<a href=""httpaddress"">Time and Pay</a>"
            HtmlBody = Replace(HtmlBody, "BodyPart", body)

            HtmlBody = Replace(HtmlBody, "httpaddress", DefURL)
            Mailmsg.Body = HtmlBody
            Mailmsg.Priority = MailPriority.High
            Mailmsg.IsBodyHtml = True
            Dim smtpMail As New SmtpClient
            Dim strHost As String = SMPTServer
            smtpMail.Host = strHost
            smtpMail.Send(Mailmsg)
        Catch ex As Exception
            Dim exmsg As String
            exmsg = ex.Message.ToString
            UserMsgBox(exmsg)
        End Try
    End Sub

    Private Sub Update_Approve_Applications(ByVal ref_no As String, ByVal approver_id As String, ByVal app_type As String)

        Dim sqlParam(2) As SqlParameter

        sqlParam(0) = New SqlParameter("@ref_no", SqlDbType.VarChar, 15)
        sqlParam(0).Value = ref_no

        sqlParam(1) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(1).Value = approver_id

        sqlParam(2) = New SqlParameter("@app_type", SqlDbType.Char, 2)
        sqlParam(2).Value = app_type

        ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Update_Approve_App", sqlParam)

    End Sub

    Private Sub send_notifications(ByVal ref_no As String, ByVal employee_id As String, ByVal employee_name As String, ByVal msg As String, ByVal applicant As String, ByVal apptype As String)

        Dim body As String
        Dim body_initial As String = ""
        Dim body_final As String = ""

        Dim recipients As String
        Dim user_email As String

        body = employee_name & " has already approved the application, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        ' body_initial = employee_name & " (Initial Approver) has already approved the application, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        body_final = employee_name & " (Initial Approver) has already approved the application, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        body_initial = applicant & " is applying for " & UCase(apptype) & ", please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."

        Dim ds As DataSet
        Dim r_level As String

        Dim currentEmployeeID As String = ViewState("Employee_ID")

        r_level = Get_Approvers_Level(employee_id, currentEmployeeID)

        If r_level = "" Then
            SendEmail("jsd@ipiphil.com", "ERROR: NO VALUE FOR R_LEVEL='' Employee ID: " & employee_id & " Approver ID: " & currentEmployeeID, employee_name)
        End If

        If r_level = "FINAL" Then
            'Send Email to Employee
            user_email = ws.Get_users_Email_byRefno(ref_no)
            recipients = user_email

            If recipients = "" Then
                SendEmail("jsd@ipiphil.com", "ERROR: NO VALUE FOR RECIPIENT='' Reference No.: " & ref_no, employee_name)
            End If

            'NOTE FOR TESTING: UNCOMMENT EMAIL SUMMARY NOTIFICATION AND COMMENT SENDMAIL
            'Insert_Email_Summary_Notification(recipients, body, employee_name)
            cls_email.SendEmail(recipients, body, employee_name)

        ElseIf r_level = "INITIAL" Then

            'Send Email to FINAL Approver
            ds = ws.Get_Approvers_Email(employee_id, "FINAL")

            Dim ds_itself As DataSet = ws.Get_Users_Email(currentEmployeeID)
            Dim dr_itself As DataRow = ds_itself.Tables(0).Rows(0)

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim dr As DataRow = ds.Tables(0).Rows(i)
                If dr("email") <> dr_itself("email") Then
                    Insert_Email_Summary_Notification(dr("email"), body_initial, employee_name)
                End If
            Next


        End If

    End Sub

    Private Sub send_notifications_disapprove(ByVal ref_no As String, ByVal employee_id As String, ByVal employee_name As String, ByVal msg As String, ByVal applicant As String, ByVal apptype As String)

        Dim body As String
        Dim body_initial As String = ""
        Dim body_final As String = ""

        Dim recipients As String = "", user_email As String
        body = employee_name & " has disapproved the application, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        ' body_initial = employee_name & " (Initial Approver) has already approved the application, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        body_final = employee_name & " (Initial Approver) has disapproved the application, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        body_initial = applicant & " is applying for " & UCase(apptype) & ", please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."

        Dim r_level As String
        Dim currentEmployeeID As String = ViewState("Employee_ID")

        r_level = Get_Approvers_Level(employee_id, Current_User.Employee_ID)

        user_email = ws.Get_users_Email_byRefno(ref_no)
        recipients = user_email

        'NOTE FOR TESTING: UNCOMMENT EMAIL SUMMARY NOTIFICATION AND COMMENT SENDMAIL
        'Insert_Email_Summary_Notification(recipients, body, employee_name) 'FOR TESTING: COMMENT FOR LIVE
        cls_email.SendEmail(recipients, body, employee_name)

    End Sub

#End Region


    Protected Sub btn_SelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_SelectAll.Click
        Dim cbCell As CheckBox
        Dim i As Integer = 0
        While (i < GridView1.Rows.Count)

            cbCell = GridView1.Rows(i).FindControl("chk_select")
            If btn_SelectAll.Text = "Select All" Then
                cbCell.Checked = True
            Else
                cbCell.Checked = False
            End If

            'If (cbCell.Checked) Then
            '    Dim DocNo = GridView1.Rows(i).Cells(0).Text.ToString
            '    MsgBox(DocNo)
            'End If
            i = i + 1
        End While
        If btn_SelectAll.Text = "Select All" Then
            btn_SelectAll.Text = "Unselect All"
        Else
            btn_SelectAll.Text = "Select All"
        End If
    End Sub


    Protected Sub Insert_DTR_Logs(ByVal refno As String)
        Dim sqlParam(0) As SqlParameter

        sqlParam(0) = New SqlParameter("@refno", SqlDbType.VarChar, 30)
        sqlParam(0).Value = refno
        ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_InsertDTR_ApplicationLogs", sqlParam)

    End Sub


    Function Get_Approvers_Level(ByVal employee_id As String, ByVal approver_id As String) As String

        'EXEC([dbo].[spWeb_Get_approvers_level])
        '@employee_id = @employee,
        '@approver_id = @employee_id,
        '@approver_level = @approver_level OUTPUT

        Dim sqlParam(2) As SqlParameter
        sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(0).Value = employee_id
        sqlParam(1) = New SqlParameter("@approver_id", SqlDbType.VarChar, 15)
        sqlParam(1).Value = approver_id
        sqlParam(2) = New SqlParameter("@approver_level", SqlDbType.VarChar, 15)
        sqlParam(2).Direction = ParameterDirection.Output

        ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_approvers_level", sqlParam)
        Return sqlParam(2).Value

    End Function

    Protected Sub btn_Approved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Approved.Click
        Dim cbCell As CheckBox
        Dim i As Integer = 0
        Dim Approved As Boolean
        LabelDisApproved.Visible = False
        ListBox1.Visible = False
        'UserMsgBox(GridView1.Rows.Count)
        Dim NoRemarks As Boolean = False
        ListBox1.Items.Clear()

        While (i < GridView1.Rows.Count)

            cbCell = GridView1.Rows(i).FindControl("chk_select")

            If (cbCell.Checked) = True Then
                Try
                    Dim index As Integer = i
                    Dim row As GridViewRow = GridView1.Rows(index)
                    Dim ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
                    Dim h_app_type As HiddenField = CType(row.FindControl("h_app_type"), HiddenField)

                    Dim result As Integer
                    result = ws.Validate_Applied_Dates(ref_no.Text) 'validation of payroll date "Locking"

                    Dim app_type As String = h_app_type.Value.ToString

                    'UserMsgBox(h_app_type.Value)
                    ' Label1.Text = h_app_type.Value
                    'Label1.Text = app_type

                    If result = 1 Then

                        Dim level As String = Get_Approvers_Level(GridView1.Rows(i).Cells(2).Text, Current_User.Employee_ID).ToString

                        If app_type = "DT" Then
                            If level = "FINAL" Then
                                Try
                                    Insert_DTR_Logs(ref_no.Text)
                                Catch ex As Exception

                                End Try
                            End If
                        End If

                        Update_Approve_Applications(ref_no.Text, Current_User.Employee_ID, h_app_type.Value)

                        send_notifications(ref_no.Text, ws.Get_users_ID_byRefno(ref_no.Text), Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has already approved", Server.HtmlDecode(GridView1.Rows(i).Cells(3).Text), GridView1.Rows(i).Cells(4).Text) 'commented for testing only by Andrew 11-13-2011


                        Approved = True


                    ElseIf result = 0 Then
                        UserMsgBox("Approval not allowed at this time since payroll processing is on going.  You may resume approval of this transaction after payroll processing date.")
                        Approved = False
                    ElseIf result = 3 Then
                        UserMsgBox("Error Select payroll period")
                        Approved = False
                    ElseIf result = 4 Then
                        UserMsgBox("Error Select area code")
                        Approved = False
                    ElseIf result = 5 Then
                        UserMsgBox("No payarea code")
                        Approved = False
                    ElseIf result = 6 Then
                        UserMsgBox("Error Select on refno")
                        Approved = False
                    ElseIf result = 7 Then
                        UserMsgBox("Employee should have atleast 1 Final approver")
                        Approved = False
                    Else
                        UserMsgBox("Unable to approve, no message")
                        Approved = False
                    End If
                    'Approved = True
                Catch ex As Exception
                    UserMsgBox(ex.Message)
                    Approved = False
                End Try

            End If
            i = i + 1
        End While



        If Approved = True Then
            UserMsgBox("Successfully Approved!")
        End If


        bind_approvals()
        btn_SelectAll.Text = "Select All"
        DpMyApprovals.Visible = False
        Btnmyappsort.Visible = False
    End Sub

    Protected Sub btn_Disapprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Disapprove.Click
        Dim cbCell As CheckBox
        Dim i As Integer = 0
        LabelDisApproved.Visible = False
        ListBox1.Visible = False
        'UserMsgBox(GridView1.Rows.Count)
        Dim Disapproved As Boolean
        Dim NoRemarks As Boolean = False
        ListBox1.Items.Clear()


        While (i < GridView1.Rows.Count)

            cbCell = GridView1.Rows(i).FindControl("chk_select")
            If (cbCell.Checked) = True Then
                Try
                    Dim index As Integer = i
                    Dim row As GridViewRow = GridView1.Rows(index)
                    Dim ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
                    Dim h_app_type As HiddenField = CType(row.FindControl("h_app_type"), HiddenField)

                    Dim DisAppRemarks As TextBox = CType(row.FindControl("DisApprovedRemarks"), TextBox)


                    If DisAppRemarks.Text.Length > 0 Then

                        ws.Update_Disapproved_Applications(ref_no.Text, Current_User.Employee_ID, h_app_type.Value, DisAppRemarks.Text)
                        send_notifications_disapprove(ref_no.Text, ws.Get_users_ID_byRefno(ref_no.Text), Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has disapproved", Server.HtmlDecode(GridView1.Rows(i).Cells(3).Text), GridView1.Rows(i).Cells(4).Text)
                        'bind_approvals()
                        NoRemarks = False
                        Disapproved = True
                    Else
                        NoRemarks = True
                        Disapproved = False
                        ListBox1.Items.Add(ref_no.Text)
                    End If


                Catch ex As Exception
                    UserMsgBox(ex.Message)
                End Try

            End If
            i = i + 1
        End While


        If NoRemarks = True Then
            'UserMsgBox("An application with no remarks has not been disapproved, please see details below.")
            'LabelDisApproved.Visible = True
            'ListBox1.Visible = True
            UserMsgBox("Please add reason for disapproval")
            'Response.Write("<script language=javascript>alert(""There are blank remarks on disapproved applications!"")</script>")
        Else
            UserMsgBox("Successfully Disapproved!")

        End If

        'If Disapproved = True Then
        '    UserMsgBox("Successfully Disapproved!")
        'End If

        bind_approvals()
        btn_SelectAll.Text = "Select All"

        DpMyApprovals.Visible = False
        Btnmyappsort.Visible = False
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

    Private Sub OpenModalPage2(ByVal RefNo As String)
        Session("RefNo") = RefNo
        Dim strScript As String = "<script language=JavaScript>"

        strScript &= "self.open('overtime_details.aspx','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=480,width=500,left=1,top=1')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If

    End Sub

    Private Sub OpenModalPage(ByVal RefNo As String, ByVal page_name As String, ByVal vheight As String, ByVal vwidth As String)
        Session("RefNo") = RefNo
        Dim strScript As String = "<script language=JavaScript>"

        strScript &= "self.open('" & page_name & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=" & vheight & ",width=" & vwidth & ",left=1,top=1')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If
    End Sub

#End Region

    Protected Sub Btnmyappsort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnmyappsort.Click
        Dim sorttextval As String = Trim(DpMyApprovals.Text)
        GridView1.DataSource = ws.Get_My_Approvals_Sort(Current_User.Employee_ID, sorttextval)
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        '----------------------------------------
        ' Added by Jan Dizon                    '
        ' Date Added/Modified: Jan (13, 2012)   '
        '-----------------------------------------
        GridView1.Dispose()
        GridView1.DataSource = ws.Get_My_Approvals_Sort(Current_User.Employee_ID, e.SortExpression)
        GridView1.DataBind()
    End Sub
End Class
