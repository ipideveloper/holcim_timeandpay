
Partial Class leave_app_header
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
            'Initialize()
            If GridView1.Rows.Count > 10 Then
                Dim strScript As String = "<script language='javascript' id='myClientScript'>freezeheader();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
            End If
            If Not Page.IsPostBack Then
                Initialize()
            End If
        Catch ex As Exception
        End Try

        DpSortLeave.Visible = False
        LeaveSort.Visible = False
    End Sub

    Protected Sub btnApply_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Try
            Session("Employee_ID") = ViewState("Employee_ID")
            Response.Redirect("leave_app_form.aspx")
        Catch ex As Exception
            UserMsgBox(ex.Message.ToString)
        End Try

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)
            Dim lnk_ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
            If e.CommandName = "cmd_ref_no" Then
                'OpenModalPage(lnk_ref_no.Text)
                If GridView1.Rows.Count > 10 Then
                    Dim strScript As String = "<script language='javascript' id='myClientScript'>freezeheader();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no As LinkButton = CType(e.Row.FindControl("lnk_ref_no"), LinkButton)
            lnk_ref_no.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")

        Get_Leave_List()
        'get_approvers()
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub Get_Leave_List()
        ' MsgBox(Current_User.Employee_ID)
        Try
            GridView1.DataSource = ws.Get_Leave_List(Current_User.Employee_ID)
            GridView1.DataBind()
            If GridView1.Rows.Count > 0 Then
                DpSortLeave.Enabled = True
                LeaveSort.Enabled = True
                DpSortLeave.Visible = True
                LeaveSort.Visible = True
            Else
                DpSortLeave.Enabled = False
                LeaveSort.Enabled = False
                DpSortLeave.Visible = False
                LeaveSort.Visible = False
            End If
            If GridView1.Rows.Count > 10 Then
                Dim strScript As String = "<script language='javascript' id='myClientScript'>freezeheader();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub get_approvers()
        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim row As GridViewRow = GridView1.Rows(i)
            Dim gv_approvers As GridView = CType(row.FindControl("gv_approvers"), GridView)
            Dim lnk_ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
            gv_approvers.DataSource = ws.Get_Application_Approver(lnk_ref_no.Text)
            gv_approvers.DataBind()
        Next
    End Sub

#End Region

#Region "Delete"



#End Region

#Region "Manage"



#End Region

#Region "Javascripts"

    Private Sub OpenModalPage(ByVal RefNo As String)
        Session("RefNo") = RefNo
        Dim strScript As String = "<script language=JavaScript>"
        strScript &= "self.open('leave_details.aspx','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=480,width=735,left=1,top=1')"
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

    Protected Sub LeaveSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LeaveSort.Click
        Dim sortstr As String = Trim(DpSortLeave.Text)
        GridView1.DataSource = ws.Get_Leave_List_Sort(Current_User.Employee_ID, sortstr)
        GridView1.DataBind()
        If GridView1.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>freezeheader();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

        get_approvers()
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lb As LinkButton = e.Row.FindControl("lnk_ref_no")
            lb.Attributes.Add("onclick", "javascript:mywin=self.open('leave_details.aspx?ref_no=" & lb.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=480,width=735,left=1,top=1');mywin.focus();return false;")

            Dim lba As LinkButton = e.Row.FindControl("lnk_approver")
            lba.Attributes.Add("onclick", "javascript:targetWin = window.open('leave_approvers.aspx?ref_no=" & lb.Text & "', 'ApproverPopup', 'toolbar=no,location=no,directories=no,status=no,address=no,menubar=no,scrollbars=yes,resizable=no,copyhistory=no,width=500, height=350, top=150, left=200');targetWin.focus();return false;")

            If GridView1.Rows.Count > 10 Then
                Dim strScript As String = "<script language='javascript' id='myClientScript'>freezeheader();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
            End If
        End If
    End Sub

    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        GridView1.DataSource = ws.Get_Leave_List_Sort(Current_User.Employee_ID, e.SortExpression)
        GridView1.DataBind()
        If GridView1.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>freezeheader();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

        'get_approvers()
    End Sub
End Class
