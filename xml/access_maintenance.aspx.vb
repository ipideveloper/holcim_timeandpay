Imports System.Data

Partial Class access_maintenance
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

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)


            If e.CommandName = "cmdEdit" Then
                'Dim index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim row As GridViewRow = GridView1.Rows(index)
                Dim lnkEdit As LinkButton = CType(row.FindControl("LinkButton1"), LinkButton)
                Dim chk_ot As CheckBox = CType(row.FindControl("chkOT"), CheckBox)
                Dim chk_ob As CheckBox = CType(row.FindControl("chkOB"), CheckBox)
                Dim chk_sa As CheckBox = CType(row.FindControl("chkSA"), CheckBox)
                If lnkEdit.Text = "EDIT" Then
                    lnkEdit.Text = "Update"
                    lnkEdit.ForeColor = Drawing.Color.Blue
                    chk_ot.Enabled = True
                    chk_ob.Enabled = True
                    chk_sa.Enabled = True
                Else
                    ws.Update_User_Access_byEmployee(GridView1.Rows(index).Cells(0).Text, chk_ot.Checked, chk_ob.Checked, chk_sa.Checked)
                    lnkEdit.Text = "EDIT"
                    lnkEdit.ForeColor = Drawing.Color.Red
                    chk_ot.Enabled = False
                    chk_ob.Enabled = False
                    chk_sa.Enabled = False
                End If
            End If

            If e.CommandName = "cmdEditAll" Then
                Dim lnkEditAll As LinkButton = CType(GridView1.HeaderRow.FindControl("lnk_edit_all"), LinkButton)
                Dim chk_ot As CheckBox = CType(GridView1.HeaderRow.FindControl("chkHOT"), CheckBox)
                Dim chk_ob As CheckBox = CType(GridView1.HeaderRow.FindControl("chk_h_ob"), CheckBox)
                Dim chk_sa As CheckBox = CType(GridView1.HeaderRow.FindControl("chk_h_sysad"), CheckBox)
                If lnkEditAll.Text = "DISABLE" Then
                    lnkEditAll.Text = "EDIT"
                    '  lnkEditAll.ForeColor = Drawing.Color.Blue
                    chk_ot.Enabled = False
                    chk_ob.Enabled = False
                    chk_sa.Enabled = False
                    btnSaveAll.Visible = False
                Else
                    'ws.Update_User_Access_byEmployee(GridView1.Rows(index).Cells(0).Text, chk_ot.Checked, chk_ob.Checked, chk_sa.Checked)
                    lnkEditAll.Text = "DISABLE"
                    lnkEditAll.ForeColor = Drawing.Color.White
                    chk_ot.Enabled = True
                    chk_ob.Enabled = True
                    chk_sa.Enabled = True
                    btnSaveAll.Visible = True
                End If
            End If

            'Added Code of Andrew 11-08-2011 Start
            If e.CommandName = "cmdReset" Then
                Dim empname As String = GridView1.Rows(index).Cells(0).Text
                Dim row As GridViewRow = GridView1.Rows(index)
                'Dim kill As ButtonField = CType(GridView1.HeaderRow.FindControl("cmdReset"), ButtonField)
                ws.Update_Password(Trim(empname), cls_GlobalFunction.encrypt(Trim("abc123"), "GECOFYFRDEY"))

                'MsgBox("Reset Password Success!")
                '--------msgbox start
                Dim sMsg As String = "Resetting of password successfully done"
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
                MsgBox("Resetting of password successfully done", MsgBoxStyle.Information, "Employee Password Resetting")
                '--------msgbox end
                'Exit Sub
            End If
            'Added Code of Andrew 11-08-2011 End

        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnkEdit As LinkButton = CType(e.Row.FindControl("LinkButton1"), LinkButton)
            lnkEdit.CommandArgument = e.Row.RowIndex.ToString
            
        End If
        
    End Sub

    Protected Sub chkHOT_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        check_uncheck_all("chkHOT", "chkOT")
    End Sub

    Protected Sub chk_h_ob_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        check_uncheck_all("chk_h_ob", "chkOB")
    End Sub

    Protected Sub chk_h_shiftsched_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        check_uncheck_all("chk_h_shiftsched", "chkShift")
    End Sub

    Protected Sub chk_h_oncall_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        check_uncheck_all("chk_h_oncall", "chkOncall")
    End Sub

    Protected Sub chk_h_sysad_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        check_uncheck_all("chk_h_sysad", "chkSA")
    End Sub

    Protected Sub btn_search_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            bind_user_access()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnSaveAll_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            update_all_user_access()
            btnSaveAll.Visible = False
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        bind_lookups()
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"

    Private Sub update_all_user_access()
        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim row As GridViewRow = GridView1.Rows(i)
            Dim chk_ot As CheckBox = CType(row.FindControl("chkOT"), CheckBox)
            Dim chk_ob As CheckBox = CType(row.FindControl("chkOB"), CheckBox)
            Dim chk_sa As CheckBox = CType(row.FindControl("chkSA"), CheckBox)
            ws.Update_User_Access_byEmployee(GridView1.Rows(i).Cells(0).Text, chk_ot.Checked, chk_ob.Checked, chk_sa.Checked)
        Next
    End Sub

#End Region

#Region "Retrieve"

    Private Sub bind_user_access()
        Dim ds As DataSet = ws.Get_User_Access(dpl_plant.SelectedValue, dpl_organization.SelectedValue, txt_emp_id.Text, txt_emp_lastname.Text, txt_emp_firstname.Text)
        GridView1.DataSource = ds
        GridView1.DataBind()
        lblrecords.Text = "(" & ds.Tables(0).Rows.Count & ") Records"
    End Sub

    Private Sub bind_lookups()
        dpl_plant.DataSource = ws.LookUp_Branch()
        dpl_plant.DataValueField = "bcode"
        dpl_plant.DataTextField = "bname"
        dpl_plant.DataBind()
        dpl_plant.SelectedValue = Current_User.Branch_id

        dpl_organization.DataSource = ws.LookUp_Organization(Current_User.Branch_id)
        dpl_organization.DataValueField = "organ_id"
        dpl_organization.DataTextField = "organ_name"
        dpl_organization.DataBind()
        dpl_organization.Items.Insert(0, "")
    End Sub

#End Region

#Region "Delete"


#End Region

#Region "Manage"

    Private Sub check_uncheck_all(ByVal c_h_str As String, ByVal c_i_str As String)
        Dim h_Check As CheckBox = CType(GridView1.HeaderRow.FindControl(c_h_str), CheckBox)
        If h_Check.Checked = True Then
            For i As Integer = 0 To GridView1.Rows.Count - 1
                Dim row As GridViewRow = GridView1.Rows(i)
                Dim i_check As CheckBox = CType(row.FindControl(c_i_str), CheckBox)
                i_check.Checked = True
            Next
        Else
            For i As Integer = 0 To GridView1.Rows.Count - 1
                Dim row As GridViewRow = GridView1.Rows(i)
                Dim i_check As CheckBox = CType(row.FindControl(c_i_str), CheckBox)
                i_check.Checked = False
            Next
        End If
        btnSaveAll.Visible = True
    End Sub

#End Region

#Region "Javascripts"

    '-- start
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
