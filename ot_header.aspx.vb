
Partial Class ot_header
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

        Dpsort.Visible = False
        btnsort.Visible = False
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("Employee_ID") = ViewState("Employee_ID")
        Response.Redirect("ot_apply.aspx")
    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging

        GridView1.DataSource = ws.Get_OT_List(Current_User.Employee_ID)
        GridView1.PageIndex = e.NewPageIndex
        GridView1.DataBind()

        get_approvers()
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)
            Dim lnk_ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
            Dim isedit As LinkButton = CType(row.FindControl("lnk_edit_ot"), LinkButton)
            If e.CommandName = "cmd_ref_no" Then
                'OpenModalPage(lnk_ref_no.Text)

            ElseIf e.CommandName = "cmd_edit_ot" Then
                If isedit.Text = "EDIT" Then
                    Session("RefNo") = lnk_ref_no.Text
                    Session("Return_OT") = "ot_header.aspx"

                    Dim sector As String = Session("Branch_ID")

                    Select Case sector
                        Case "P100"
                            Response.Redirect("ot_edit_form.aspx")
                        Case "PLG0"
                            Response.Redirect("ot_edit_form_sector.aspx")
                        Case "PDV0"
                            Response.Redirect("ot_edit_form_sector.aspx")
                        Case "PBL0"
                            Response.Redirect("ot_edit_form_sector.aspx")
                        Case "PLN0"
                            Response.Redirect("ot_edit_form_sector.aspx")
                    End Select
                    
                End If
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no As LinkButton = CType(e.Row.FindControl("lnk_ref_no"), LinkButton)
            lnk_ref_no.CommandArgument = e.Row.RowIndex.ToString
            Dim lnk_edit_ot As LinkButton = CType(e.Row.FindControl("lnk_edit_ot"), LinkButton)
            lnk_edit_ot.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        get_ot_list()
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

    Private Sub get_ot_list()
        GridView1.DataSource = ws.Get_OT_List(Current_User.Employee_ID)
        GridView1.DataBind()
        If GridView1.Rows.Count > 0 Then
            Dpsort.Enabled = True
            btnsort.Enabled = True
            Dpsort.Visible = True
            btnsort.Visible = True
        Else
            Dpsort.Enabled = False
            btnsort.Enabled = False
            Dpsort.Visible = False
            btnsort.Visible = False
        End If
    End Sub

    Protected Sub btnsort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsort.Click
        'UserMsgBox(Dpsort.Text)
        'Exit Sub
        GridView1.DataSource = ws.Get_OT_List_Sort(Current_User.Employee_ID, Dpsort.Text)
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

    Private Sub OpenModalPage(ByVal RefNo As String)
        Session("RefNo") = RefNo
        Dim strScript As String = "<script language=javascript>"
        'strScript &= "if (true == my_window.opened) { my_window.close();} else {my_window.open();}"
        strScript &= "my_window = self.open('overtime_details.aspx','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=480,width=500,left=1,top=1');"
        'strScript &= ""
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

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lb As LinkButton = e.Row.FindControl("lnk_ref_no")

            lb.Attributes.Add("onclick", "javascript:mywin=self.open('overtime_details.aspx?ref_no=" & lb.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=500,width=500,left=1,top=1');mywin.focus();")
            'lb.Attributes.Add("onclick", String.Format("javascript:return confirm('Are you sure you want to delete Marketing Approver {0} ?')", e.Row.Cells(2).Text))

            'e.Row.Cells(1).Style("padding-left") = "10px"
        End If
    End Sub

    
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        GridView1.DataSource = ws.Get_OT_List_Sort(Current_User.Employee_ID, e.SortExpression)
        GridView1.DataBind()
        If GridView1.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>freezeheader();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

        get_approvers()
    End Sub
End Class
