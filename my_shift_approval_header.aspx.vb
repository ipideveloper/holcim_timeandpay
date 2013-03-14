Imports System.Data

Partial Class my_shift_approval_header
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

        DpMyShiftapp.Enabled = False
        Btnmyshiftapp.Enabled = False
        DpMyShiftapp.Visible = False
        Btnmyshiftapp.Visible = False



        'Try
        If Not Page.IsPostBack Then
            Initialize()
        End If
        'Catch ex As Exception
        'End Try
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        bind_approval()
        If GridView1.Rows.Count = 0 Then
            UserMsgBox("No Pending Shift Approval")
        End If
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub bind_approval()
        GridView1.DataSource = ws.Get_My_Shift_Approval(Current_User.Employee_ID)
        GridView1.DataBind()

        'Added by Andrew 11-27-2011 Start
        'Dim dsrefid As New DataSet
        'Dim refiddrew As String
        'dsrefid = ws.Get_My_Shift_Approval(Current_User.Employee_ID)
        ''
        'For i As Integer = 0 To dsrefid.Tables(0).Rows.Count - 1
        '    Dim dr As DataRow = dsrefid.Tables(0).Rows(i)
        '    refiddrew = dr("ref_no")
        'Next
        'Added by Andrew 11-27-2011 End

        'If GridView1.Rows.Count > 0 Then
        ' Btnmyshiftapp.Enabled = True
        'DpMyShiftapp.Enabled = True
        'Btnmyshiftapp.Visible = True
        'DpMyShiftapp.Visible = True
        'Else
        'Btnmyshiftapp.Enabled = False
        'DpMyShiftapp.Enabled = False
        'Btnmyshiftapp.Visible = False
        'DpMyShiftapp.Visible = False
        'End If
        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim month_name As String = ""
            'Added Code for Trace Andrew 11-12-2011 Start
            'MsgBox(GridView1.Rows(i).Cells(4).Text)
            'Exit Sub
            'Added Code for Trace Andrew 11-13-2011 End
            Select Case GridView1.Rows(i).Cells(4).Text

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
            GridView1.Rows(i).Cells(5).Text = month_name
            'MsgBox(GridView1.Rows(i).Cells(8).Text)
            'Exit Sub
        Next
      
    End Sub

#End Region

#Region "Delete"


#End Region

#Region "Manage"

    Private Sub send_notifications(ByVal month As Integer, ByVal year As Integer, ByVal planner_id As String, ByVal employee_name As String, ByVal msg As String)
        Dim body As String
        Dim recipients As String = ""
        body = employee_name & msg & " change shift schedule, month " & CType(month, String) & " and year " & CType(year, String) & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        Dim ds As DataSet
        ds = ws.Get_Users_Email(planner_id)
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim dr As DataRow = ds.Tables(0).Rows(i)
            If recipients = "" Then
                recipients = dr("email")
            Else
                recipients = recipients & ", " & dr("email")
            End If
        Next
        
        cls_email.SendEmail(recipients, body, employee_name)
    End Sub

#End Region

#Region "Javascripts"

    Private Sub OpenModalPage(ByVal planner_id As String, ByVal month As String, ByVal pyear As String)
        Session("Employee_ID") = ViewState("Employee_ID")
        Session("month") = month
        Session("year") = pyear
        Session("planner_id") = planner_id

        Dim strScript As String = "<script language=JavaScript>"
        strScript &= "self.open('shift_approval.aspx','EmailPopup','fullscreen=yes,status=no,scrollbars=yes,address=no,resizable=yes,height=100%,width=100%,left=200,top=100')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If
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

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)
            If e.CommandName = "cmd_view" Then
                Dim x As String = GridView1.Rows(index).Cells(3).Text.ToString
                Dim y As String = GridView1.Rows(index).Cells(4).Text.ToString
                Dim z As String = GridView1.Rows(index).Cells(6).Text.ToString
                OpenModalPage(GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(4).Text, GridView1.Rows(index).Cells(6).Text)
                'ElseIf e.CommandName = "cmd_approve" Then
                '    ws.Insert_Shift_Approval(GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, 2, GridView1.Rows(index).Cells(5).Text, Current_User.Employee_ID)
                '    ws.Update_Approve_Shift_Sched(GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, 2)
                '    ws.Update_Shift_Approvers(Current_User.Employee_ID, GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, 2)
                '    send_notifications(GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, GridView1.Rows(index).Cells(2).Text, Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has already approved")
                '    bind_approval()
                '    UserMsgBox("Shift schedule successfully approved")
                'Else
                '    ws.Insert_Shift_Approval(GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, 3, GridView1.Rows(index).Cells(5).Text, Current_User.Employee_ID)
                '    ws.Update_Approve_Shift_Sched(GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, 3)
                '    send_notifications(GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, GridView1.Rows(index).Cells(2).Text, Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has already cancelled")
                '    bind_approval()
                '    UserMsgBox("Done")
            End If
        Catch ex As Exception
            'UserMsgBox(ex.Message & "May Mali!")
        End Try
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_view As LinkButton = CType(e.Row.FindControl("lnk_view"), LinkButton)
            lnk_view.CommandArgument = e.Row.RowIndex.ToString
            'Dim lnk_approve As LinkButton = CType(e.Row.FindControl("lnk_approve"), LinkButton)
            'lnk_approve.CommandArgument = e.Row.RowIndex.ToString
            'Dim lnk_revise As LinkButton = CType(e.Row.FindControl("lnk_revise"), LinkButton)
            'lnk_revise.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

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

    Protected Sub btn_Approved_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Approved.Click
        Dim cbCell As CheckBox
        Dim i As Integer = 0
        'UserMsgBox(GridView1.Rows.Count)
        Dim Approved As Boolean

        While (i < GridView1.Rows.Count)

            cbCell = GridView1.Rows(i).FindControl("chk_select")
            If (cbCell.Checked) = True Then
                'Try
                Dim index As Integer = i
                Dim row As GridViewRow = GridView1.Rows(index)
                Dim disAppRemarks As TextBox = CType(row.FindControl("DisApprovedRemarks"), TextBox)
                If disAppRemarks.Text = "" Then
                    disAppRemarks.Text = "No Remarks"
                End If
                'MsgBox(GridView1.Rows(index).Cells(2).Text)
                'Exit Sub
                'ws.Insert_Shift_Approval(GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, 2, GridView1.Rows(index).Cells(5).Text, Current_User.Employee_ID)
                'ws.Update_Approve_Shift_Sched(GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, 2, "")
                'ws.Update_Shift_Approvers(Current_User.Employee_ID, GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, 2)
                'Revised Code by Andrew 11-13-2011 Start
                ws.Insert_Shift_Approval(GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, 2, GridView1.Rows(index).Cells(6).Text, Current_User.Employee_ID, GridView1.Rows(index).Cells(8).Text)
                ws.Update_Approve_Shift_Sched(GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, GridView1.Rows(index).Cells(6).Text, 2, disAppRemarks.Text, GridView1.Rows(index).Cells(8).Text)
                ws.Update_Shift_Approvers(Current_User.Employee_ID, GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, GridView1.Rows(index).Cells(6).Text, 2, GridView1.Rows(index).Cells(8).Text)
                ws.Update_shift_app(GridView1.Rows(index).Cells(8).Text, 2) 'Added by Andrew 11-27-2011
                'Revised Code by Andrew 11-13-2011 End
                'send_notifications(GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, GridView1.Rows(index).Cells(2).Text, Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has already approved")
                'Revised Code by Andrew 11-13-2011 Start
                'send_notifications(GridView1.Rows(index).Cells(4).Text, GridView1.Rows(index).Cells(6).Text, GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(2).Text, " has already approved")
                'Revised Code by Andrew 11-13-2011 End
                'Catch ex As Exception
                'UserMsgBox(ex.Message)
                'End Try
                Approved = True
            End If
            i = i + 1
        End While


        If Approved = True Then
            UserMsgBox("Shift schedule successfully approved")
        End If


        bind_approval()
        btn_SelectAll.Text = "Select All"

    End Sub

    Protected Sub btn_Disapprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Disapprove.Click
        Dim cbCell As CheckBox
        Dim i As Integer = 0
        LabelDisApproved.Visible = False
        ListBox1.Visible = False
        'UserMsgBox(GridView1.Rows.Count)
        Dim Disapproved As Boolean
        Dim NoRemarks As Boolean = False
        'ListBox1.Items.Clear()

        While (i < GridView1.Rows.Count)

            cbCell = GridView1.Rows(i).FindControl("chk_select")
            If (cbCell.Checked) = True Then
                'Try
                Dim index As Integer = i
                Dim row As GridViewRow = GridView1.Rows(index)
                Dim ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
                'Dim h_app_type As HiddenField = CType(row.FindControl("h_app_type"), HiddenField)
                Dim DisAppRemarks As TextBox = CType(row.FindControl("DisApprovedRemarks"), TextBox)

                If DisAppRemarks.Text.Length > 0 Then
                    'ws.Insert_Shift_Approval(GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, 3, GridView1.Rows(index).Cells(5).Text, Current_User.Employee_ID)
                    'ws.Update_Approve_Shift_Sched(GridView1.Rows(index).Cells(2).Text, GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, 3, DisAppRemarks.Text)
                    'send_notifications(GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, GridView1.Rows(index).Cells(2).Text, Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has already cancelled")
                    'Revise Code by Andrew 11-13-2011 Start
                    ws.Update_Approve_Shift_Sched(GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, GridView1.Rows(index).Cells(6).Text, 3, DisAppRemarks.Text, GridView1.Rows(index).Cells(8).Text) 'eto lng dapat ung naka uncomment
                    ws.Update_shift_app(GridView1.Rows(index).Cells(8).Text, 3) 'Added by Andrew 11-27-2011
                    'ws.Update_Shift_Approvers(Current_User.Employee_ID, GridView1.Rows(index).Cells(3).Text, GridView1.Rows(index).Cells(5).Text, GridView1.Rows(index).Cells(6).Text, 3)
                    'send_notifications(GridView1.Rows(index).Cells(4).Text, GridView1.Rows(index).Cells(6).Text, GridView1.Rows(index).Cells(3).Text, Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has already Disapproved")
                    'Revise Code by Andrew 11-13-2011 End
                    Disapproved = True
                Else
                    NoRemarks = True
                    Disapproved = False
                    'ListBox1.Items.Add(ref_no.Text)
                End If

                'Catch ex As Exception
                ' UserMsgBox(ex.Message)
                'End Try

            End If
            i = i + 1
        End While

        bind_approval()
        If NoRemarks = True Then
            'UserMsgBox("An application with no remarks has not been disapproved, please see details below.")
            UserMsgBox("Please add reason for disapproval")
            'LabelDisApproved.Visible = True
            'ListBox1.Visible = True
        End If

        If Disapproved = True Then
            UserMsgBox("Successfully Disapproved!")
        End If
        btn_SelectAll.Text = "Select All"
    End Sub

    Protected Sub Btnmyshiftapp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnmyshiftapp.Click
        Dim shiftapp As String = Trim(DpMyShiftapp.Text)
        GridView1.DataSource = ws.Get_My_Shift_Approval_Sort(Current_User.Employee_ID, shiftapp)
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        Try
            'Dim shiftapp As String = Trim(DpMyShiftapp.Text)

            Dim sortby As String = e.SortExpression
            GridView1.DataSource = ws.Get_My_Shift_Approval_Sort(Current_User.Employee_ID, sortby)
            GridView1.DataBind()

            For i As Integer = 0 To GridView1.Rows.Count - 1
                Dim month_name As String = ""
                'Added Code for Trace Andrew 11-12-2011 Start
                'MsgBox(GridView1.Rows(i).Cells(4).Text)
                'Exit Sub
                'Added Code for Trace Andrew 11-13-2011 End
                Select Case GridView1.Rows(i).Cells(4).Text

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
                GridView1.Rows(i).Cells(5).Text = month_name
                'MsgBox(GridView1.Rows(i).Cells(8).Text)
                'Exit Sub
            Next
           
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try

    End Sub
End Class
