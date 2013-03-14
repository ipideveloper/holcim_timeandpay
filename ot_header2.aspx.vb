
Partial Class Default2
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
            If Not Page.IsPostBack Then
                Initialize()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Session("Employee_ID") = ViewState("Employee_ID")
        Response.Redirect("ob_apply.aspx")

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)
            Dim lnk_ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
            Dim isedit As LinkButton = CType(row.FindControl("lnk_edit_ob"), LinkButton)
            If e.CommandName = "cmd_ref_no" Then
                'OpenModalPage(lnk_ref_no.Text)
                'End If
                '              If e.CommandName = "cmd_ref_no_ob" Then
                OpenModalPage(lnk_ref_no.Text, "ob_details.aspx", "600", "800")
            ElseIf e.CommandName = "cmd_edit_ob" Then
                If isedit.Text = "EDIT" Then
                    Session("RefNo") = lnk_ref_no.Text
                    Session("Return_OB") = "ob_header.aspx"
                    Response.Redirect("ob_edit_form.aspx")
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no As LinkButton = CType(e.Row.FindControl("lnk_ref_no"), LinkButton)
            lnk_ref_no.CommandArgument = e.Row.RowIndex.ToString
            Dim lnk_edit_ob As LinkButton = CType(e.Row.FindControl("lnk_edit_ob"), LinkButton)
            lnk_edit_ob.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        get_ob_list()
        get_approvers()
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub get_ob_list()
        GridView1.DataSource = ws.Get_OB_List(Current_User.Employee_ID)
        GridView1.DataBind()
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

    
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged

    End Sub
End Class
