Imports System.Data
Imports System.Data.SqlClient
Imports HolcimDbClass


Partial Class my_app_status
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
                If gv_ob.Rows.Count > 10 Then
                    Dim strScript As String = "<script language='javascript' id='myClientScript'>button1();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
                End If

                'Concur Settings
                'If CheckIsConcur(Session("Organization_ID")) = "Yes" Then
                '    PN1.Visible = False
                '    Button1.Visible = False
                '    Button2.Enabled = False
                '    Button2.Style.Add("color", "#FFFFFF")
                '    Button2.Style.Add("background-color", "#FF0000")
                '    PN2.Visible = True
                '    PN3.Visible = False
                '    PN4.Visible = False
                '    PN5.Visible = False

                'Else
                '    Button1.Style.Add("color", "#FFFFFF")
                '    Button1.Style.Add("background-color", "#FF0000")
                '    PN1.Visible = True
                '    PN2.Visible = False
                '    PN3.Visible = False
                '    PN4.Visible = False
                '    PN5.Visible = False
                'End If

            End If

        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        get_application_status()
        get_approvers()
        Button1.Style.Add("color", "#FFFFFF")
        Button1.Style.Add("background-color", "#FF0000")
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub get_application_status()
        Try
            gv_ob.DataSource = ws.Get_My_OB_List(Current_User.Employee_ID)
            gv_ob.DataBind()

            'gv_overtime.DataSource = ws.Get_My_OT_List(Current_User.Employee_ID)
            'gv_overtime.DataBind()
            'gv_oncall.DataSource = ws.Get_My_OnCall_List(Current_User.Employee_ID)
            'gv_oncall.DataBind()
            'GridView1.DataSource = ws.Get_My_Leave_List(Current_User.Employee_ID)
            'GridView1.DataBind()
            'gv_shift.DataSource = ws.Get_My_ShiftSched_List(Current_User.Employee_ID)
            'gv_shift.DataBind()
        Catch ex As Exception
            UserMsgBox(ex.Message.ToString)
        End Try

    End Sub

    Private Sub get_approvers()
        For i As Integer = 0 To gv_ob.Rows.Count - 1
            Dim row As GridViewRow = gv_ob.Rows(i)
            Dim gv_approvers_ob As GridView = CType(row.FindControl("gv_approvers_ob"), GridView)
            Dim lnk_ref_no_ob As LinkButton = CType(row.FindControl("lnk_ref_no_ob"), LinkButton)
            gv_approvers_ob.DataSource = ws.Get_Application_Approver(lnk_ref_no_ob.Text)
            gv_approvers_ob.DataBind()
        Next

        'For i As Integer = 0 To gv_overtime.Rows.Count - 1
        '    Dim row As GridViewRow = gv_overtime.Rows(i)
        '    Dim gv_approvers_ot As GridView = CType(row.FindControl("gv_approvers_ot"), GridView)
        '    Dim lnk_ref_no_ot As LinkButton = CType(row.FindControl("lnk_ref_no_ot"), LinkButton)
        '    gv_approvers_ot.DataSource = ws.Get_Application_Approver(lnk_ref_no_ot.Text)
        '    gv_approvers_ot.DataBind()
        'Next

        'For i As Integer = 0 To gv_oncall.Rows.Count - 1
        '    Dim row As GridViewRow = gv_oncall.Rows(i)
        '    Dim gv_approvers_oncall As GridView = CType(row.FindControl("gv_approvers_oncall"), GridView)
        '    Dim lnk_ref_no_oncall As LinkButton = CType(row.FindControl("lnk_ref_no_oncall"), LinkButton)
        '    gv_approvers_oncall.DataSource = ws.Get_Application_Approver(lnk_ref_no_oncall.Text)
        '    gv_approvers_oncall.DataBind()
        'Next

        'For i As Integer = 0 To GridView1.Rows.Count - 1
        '    Dim row As GridViewRow = GridView1.Rows(i)
        '    Dim gv_approvers_leave As GridView = CType(row.FindControl("gv_approvers_leave"), GridView)
        '    Dim lnk_ref_no_leave As LinkButton = CType(row.FindControl("lnk_ref_no_leave"), LinkButton)
        '    gv_approvers_leave.DataSource = ws.Get_Application_Approver(lnk_ref_no_leave.Text)
        '    gv_approvers_leave.DataBind()
        'Next


        'For i As Integer = 0 To gv_shift.Rows.Count - 1
        '    Dim row As GridViewRow = gv_shift.Rows(i)
        '    Dim gv_approvers_shift As GridView = CType(row.FindControl("gv_approvers_shift"), GridView)
        '    Dim lnk_ref_no_shift As LinkButton = CType(row.FindControl("lnk_ref_no_shift"), LinkButton)
        '    gv_approvers_shift.DataSource = ws.Get_Shift_Approver(lnk_ref_no_shift.Text)
        '    gv_approvers_shift.DataBind()
        'Next

    End Sub

#End Region

#Region "Delete"



#End Region

#Region "Manage"



#End Region

#Region "Javascripts"

    Private Sub OpenModalPage(ByVal RefNo As String, ByVal page_name As String, ByVal vheight As String, ByVal vwidth As String)
        Session("RefNo") = RefNo
        Dim strScript As String = "<script language=JavaScript>"

        strScript &= "self.open('" & page_name & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=" & vheight & ",width=" & vwidth & ",left=1,top=1')"
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




    Protected Sub gv_ob_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_ob.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gv_ob.Rows(index)
            Dim ref_no As LinkButton = CType(row.FindControl("lnk_ref_no_ob"), LinkButton)
            Dim isedit As LinkButton = CType(row.FindControl("lnk_ob"), LinkButton)

            If e.CommandName = "cmd_ref_no_ob" Then
                'OpenModalPage(ref_no.Text, "ob_details.aspx", "600", "800")
                If gv_ob.Rows.Count > 10 Then

                    Dim strScript As String = "<script language='javascript' id='myClientScript'>button1();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)

                End If
            ElseIf e.CommandName = "cmd_edit_ob" Then
                If isedit.Text = "EDIT" Then
                    Session("RefNo") = ref_no.Text
                    Session("Return_OB") = "my_app_status.aspx"
                    Response.Redirect("ob_edit_form.aspx")
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub gv_ob_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_ob.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no_ob As LinkButton = CType(e.Row.FindControl("lnk_ref_no_ob"), LinkButton)
            lnk_ref_no_ob.CommandArgument = e.Row.RowIndex.ToString
            Dim lnk_edit_ob As LinkButton = CType(e.Row.FindControl("lnk_ob"), LinkButton)
            lnk_edit_ob.CommandArgument = e.Row.RowIndex.ToString

            'lnk_ref_no_ob.Attributes.Add("onclick", "javascript:mywin=self.open('ob_details.aspx?ref_no=" & lnk_ref_no_ob.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=600,width=800,left=1,top=1');mywin.focus();")

        End If
    End Sub

    Protected Sub gv_overtime_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_overtime.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gv_overtime.Rows(index)
            Dim ref_no As LinkButton = CType(row.FindControl("lnk_ref_no_ot"), LinkButton)
            Dim isedit As LinkButton = CType(row.FindControl("lnk_ot"), LinkButton)

            If e.CommandName = "cmd_ref_no_ot" Then
                'OpenModalPage(ref_no.Text, "overtime_details.aspx", "480", "500")
                If gv_overtime.Rows.Count > 10 Then
                    Dim strScript As String = "<script language='javascript' id='myClientScript'>button2();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
                End If
            ElseIf e.CommandName = "cmd_edit_ot" Then
                If isedit.Text = "EDIT" Then
                    Session("RefNo") = ref_no.Text
                    Session("Return_OT") = "my_app_status.aspx"


                    Dim sector As String = Session("Branch_ID")

                    'For Sector Restriction
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

    Protected Sub gv_overtime_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_overtime.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no_ot As LinkButton = CType(e.Row.FindControl("lnk_ref_no_ot"), LinkButton)
            lnk_ref_no_ot.CommandArgument = e.Row.RowIndex.ToString
            Dim lnk_edit_ot As LinkButton = CType(e.Row.FindControl("lnk_ot"), LinkButton)
            lnk_edit_ot.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub gv_oncall_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_oncall.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gv_oncall.Rows(index)
            Dim ref_no As LinkButton = CType(row.FindControl("lnk_ref_no_oncall"), LinkButton)
            Dim isedit As LinkButton = CType(row.FindControl("lnk_oncall"), LinkButton)

            If e.CommandName = "cmd_ref_no_oncall" Then
                'OpenModalPage(ref_no.Text, "oncall_details.aspx", "450", "450")
                If gv_oncall.Rows.Count > 10 Then
                    Dim strScript As String = "<script language='javascript' id='myClientScript'>button3();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
                End If
            ElseIf e.CommandName = "cmd_edit_oncall" Then
                If isedit.Text = "EDIT" Then
                    Session("RefNo") = ref_no.Text
                    Session("Return_OnCall") = "my_app_status.aspx"
                    Response.Redirect("oncall_edit_form.aspx")
                End If
            End If


        Catch ex As Exception
        End Try
    End Sub

    Protected Sub gv_oncall_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_oncall.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no_oncall As LinkButton = CType(e.Row.FindControl("lnk_ref_no_oncall"), LinkButton)
            lnk_ref_no_oncall.CommandArgument = e.Row.RowIndex.ToString
            Dim lnk_edit_oncall As LinkButton = CType(e.Row.FindControl("lnk_oncall"), LinkButton)
            lnk_edit_oncall.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = GridView1.Rows(index)
            Dim ref_no As LinkButton = CType(row.FindControl("lnk_ref_no_leave"), LinkButton)
            Dim isedit As LinkButton = CType(row.FindControl("lnk_edit"), LinkButton)

            If e.CommandName = "cmd_ref_no_leave" Then
                'OpenModalPage(ref_no.Text, "leave_details.aspx", "480", "735")
                If GridView1.Rows.Count > 10 Then
                    Dim strScript As String = "<script language='javascript' id='myClientScript'>button4();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
                End If
            ElseIf e.CommandName = "cmd_edit_leave" Then
                If isedit.Text = "EDIT" Then
                    Session("RefNo") = ref_no.Text
                    Session("Return_Leave") = "my_app_status.aspx"
                    Response.Redirect("leave_edit_form.aspx")
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub


    Protected Sub GridView1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no_leave As LinkButton = CType(e.Row.FindControl("lnk_ref_no_leave"), LinkButton)
            lnk_ref_no_leave.CommandArgument = e.Row.RowIndex.ToString
            Dim lnk_edit_leave As LinkButton = CType(e.Row.FindControl("lnk_edit"), LinkButton)
            lnk_edit_leave.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub


    Protected Sub gv_shift_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_shift.RowCommand
        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim row As GridViewRow = gv_shift.Rows(index)
            Dim ref_no As LinkButton = CType(row.FindControl("lnk_ref_no_shift"), LinkButton)
            Session("RefNo") = ref_no

            If e.CommandName = "cmd_ref_no_shift" Then

                OpenModalPage(ref_no.Text, "UploadShift_My_Details.aspx", "480", "735")
                If gv_shift.Rows.Count > 10 Then
                    Dim strScript As String = "<script language='javascript' id='myClientScript'>button5();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub

    Protected Sub gv_Shift_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_shift.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no_shift As LinkButton = CType(e.Row.FindControl("lnk_ref_no_shift"), LinkButton)
            lnk_ref_no_shift.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub gv_shift_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gv_shift.Sorting
        gv_shift.Dispose()
        gv_shift.DataSource = ws.Get_My_ShiftSched_List_Sort(Current_User.Employee_ID, e.SortExpression)
        gv_shift.DataBind()
        For i As Integer = 0 To gv_shift.Rows.Count - 1
            Dim row As GridViewRow = gv_shift.Rows(i)
            Dim gv_approvers_shift As GridView = CType(row.FindControl("gv_approvers_shift"), GridView)
            Dim lnk_ref_no_shift As LinkButton = CType(row.FindControl("lnk_ref_no_shift"), LinkButton)
            gv_approvers_shift.DataSource = ws.Get_Shift_Approver(lnk_ref_no_shift.Text)
            gv_approvers_shift.DataBind()
        Next

        If gv_shift.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button5();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If
    End Sub

    Protected Sub gv_ob_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_ob.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lb As LinkButton = e.Row.FindControl("lnk_ref_no_ob")

            lb.Attributes.Add("onclick", "javascript:mywin=self.open('ob_details.aspx?ref_no=" & lb.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=600,width=800,left=1,top=1');mywin.focus();return false;")

        End If
    End Sub

    Protected Sub gv_ob_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gv_ob.Sorting
        gv_ob.Dispose()
        gv_ob.DataSource = ws.Get_My_OB_List_Sort(Current_User.Employee_ID, e.SortExpression)
        gv_ob.DataBind()
        For i As Integer = 0 To gv_ob.Rows.Count - 1
            Dim row As GridViewRow = gv_ob.Rows(i)
            Dim gv_approvers_ob As GridView = CType(row.FindControl("gv_approvers_ob"), GridView)
            Dim lnk_ref_no_ob As LinkButton = CType(row.FindControl("lnk_ref_no_ob"), LinkButton)
            gv_approvers_ob.DataSource = ws.Get_Application_Approver(lnk_ref_no_ob.Text)
            gv_approvers_ob.DataBind()
        Next
        If gv_ob.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button1();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If
    End Sub

    Protected Sub gv_overtime_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_overtime.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lb As LinkButton = e.Row.FindControl("lnk_ref_no_ot")

            lb.Attributes.Add("onclick", "javascript:mywin=self.open('overtime_details.aspx?ref_no=" & lb.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=500,width=500,left=1,top=1');mywin.focus();return false;")
            'lb.Attributes.Add("onclick", String.Format("javascript:return confirm('Are you sure you want to delete Marketing Approver {0} ?')", e.Row.Cells(2).Text))

            'e.Row.Cells(1).Style("padding-left") = "10px"
        End If
    End Sub

    Protected Sub gv_overtime_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gv_overtime.Sorting
        gv_overtime.Dispose()
        gv_overtime.DataSource = ws.Get_My_OT_List_Sort(Current_User.Employee_ID, e.SortExpression)
        gv_overtime.DataBind()
        For i As Integer = 0 To gv_overtime.Rows.Count - 1
            Dim row As GridViewRow = gv_overtime.Rows(i)
            Dim gv_approvers_ot As GridView = CType(row.FindControl("gv_approvers_ot"), GridView)
            Dim lnk_ref_no_ot As LinkButton = CType(row.FindControl("lnk_ref_no_ot"), LinkButton)
            gv_approvers_ot.DataSource = ws.Get_Application_Approver(lnk_ref_no_ot.Text)
            gv_approvers_ot.DataBind()
        Next

        If gv_overtime.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button2();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

    End Sub

    Protected Sub gv_oncall_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_oncall.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lb As LinkButton = e.Row.FindControl("lnk_ref_no_oncall")

            lb.Attributes.Add("onclick", "javascript:mywin=self.open('oncall_details.aspx?ref_no=" & lb.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=450,width=450,left=1,top=1');mywin.focus();return false;")

        End If
    End Sub

    Protected Sub gv_oncall_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gv_oncall.Sorting
        gv_oncall.Dispose()
        gv_oncall.DataSource = ws.Get_My_OnCall_List_Sort(Current_User.Employee_ID, e.SortExpression)
        gv_oncall.DataBind()
        For i As Integer = 0 To gv_oncall.Rows.Count - 1
            Dim row As GridViewRow = gv_oncall.Rows(i)
            Dim gv_approvers_oncall As GridView = CType(row.FindControl("gv_approvers_oncall"), GridView)
            Dim lnk_ref_no_oncall As LinkButton = CType(row.FindControl("lnk_ref_no_oncall"), LinkButton)
            gv_approvers_oncall.DataSource = ws.Get_Application_Approver(lnk_ref_no_oncall.Text)
            gv_approvers_oncall.DataBind()
        Next

        If gv_oncall.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button3();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lb As LinkButton = e.Row.FindControl("lnk_ref_no_leave")

            lb.Attributes.Add("onclick", "javascript:mywin=self.open('leave_details.aspx?ref_no=" & lb.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=480,width=735,left=1,top=1');mywin.focus();return false;")

        End If
    End Sub



    Protected Sub gridview1_sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        GridView1.Dispose()
        GridView1.DataSource = ws.Get_My_Leave_List_Sort(Current_User.Employee_ID, e.SortExpression)
        GridView1.DataBind()
        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim row As GridViewRow = GridView1.Rows(i)
            Dim gv_approvers_leave As GridView = CType(row.FindControl("gv_approvers_leave"), GridView)
            Dim lnk_ref_no_leave As LinkButton = CType(row.FindControl("lnk_ref_no_leave"), LinkButton)
            gv_approvers_leave.DataSource = ws.Get_Application_Approver(lnk_ref_no_leave.Text)
            gv_approvers_leave.DataBind()
        Next

        If GridView1.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button4();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If
    End Sub

    Protected Sub gv_dtr_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gv_dtr.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lb As LinkButton = e.Row.FindControl("lnk_ref_no_dtr")

            lb.Attributes.Add("onclick", "javascript:mywin=self.open('dtr_details.aspx?ref_no=" & lb.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,height=600,width=800,resizable=no,left=150,top=1');mywin.focus();return false;")

        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        gv_ob.DataSource = ws.Get_My_OB_List(Current_User.Employee_ID)
        gv_ob.DataBind()
        For i As Integer = 0 To gv_ob.Rows.Count - 1
            Dim row As GridViewRow = gv_ob.Rows(i)
            Dim gv_approvers_ob As GridView = CType(row.FindControl("gv_approvers_ob"), GridView)
            Dim lnk_ref_no_ob As LinkButton = CType(row.FindControl("lnk_ref_no_ob"), LinkButton)
            gv_approvers_ob.DataSource = ws.Get_Application_Approver(lnk_ref_no_ob.Text)
            gv_approvers_ob.DataBind()
        Next
        If gv_ob.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button1();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

        Button1.Style.Add("color", "#FFFFFF")
        Button1.Style.Add("background-color", "#FF0000")

        Button2.Style.Add("color", "#696969")
        Button2.Style.Add("background-color", "#FFFFFF")

        Button3.Style.Add("color", "#696969")
        Button3.Style.Add("background-color", "#FFFFFF")

        Button4.Style.Add("color", "#696969")
        Button4.Style.Add("background-color", "#FFFFFF")

        Button5.Style.Add("color", "#696969")
        Button5.Style.Add("background-color", "#FFFFFF")

        Button_DTR.Style.Add("color", "#696969")
        Button_DTR.Style.Add("background-color", "#FFFFFF")

        PN1.Visible = True
        PN2.Visible = False
        PN3.Visible = False
        PN4.Visible = False
        PN5.Visible = False
        PN6.Visible = False
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        gv_overtime.DataSource = ws.Get_My_OT_List(Current_User.Employee_ID)
        gv_overtime.DataBind()
        For i As Integer = 0 To gv_overtime.Rows.Count - 1
            Dim row As GridViewRow = gv_overtime.Rows(i)
            Dim gv_approvers_ot As GridView = CType(row.FindControl("gv_approvers_ot"), GridView)
            Dim lnk_ref_no_ot As LinkButton = CType(row.FindControl("lnk_ref_no_ot"), LinkButton)
            gv_approvers_ot.DataSource = ws.Get_Application_Approver(lnk_ref_no_ot.Text)
            gv_approvers_ot.DataBind()
        Next

        If gv_overtime.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button2();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

        Button1.Style.Add("color", "#696969")
        Button1.Style.Add("background-color", "#FFFFFF")

        Button2.Style.Add("color", "#FFFFFF")
        Button2.Style.Add("background-color", "#FF0000")

        Button3.Style.Add("color", "#696969")
        Button3.Style.Add("background-color", "#FFFFFF")

        Button4.Style.Add("color", "#696969")
        Button4.Style.Add("background-color", "#FFFFFF")

        Button5.Style.Add("color", "#696969")
        Button5.Style.Add("background-color", "#FFFFFF")

        Button_DTR.Style.Add("color", "#696969")
        Button_DTR.Style.Add("background-color", "#FFFFFF")

        PN1.Visible = False
        PN2.Visible = True
        PN3.Visible = False
        PN4.Visible = False
        PN5.Visible = False
        PN6.Visible = False
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        gv_oncall.DataSource = ws.Get_My_OnCall_List(Current_User.Employee_ID)
        gv_oncall.DataBind()

        For i As Integer = 0 To gv_oncall.Rows.Count - 1
            Dim row As GridViewRow = gv_oncall.Rows(i)
            Dim gv_approvers_oncall As GridView = CType(row.FindControl("gv_approvers_oncall"), GridView)
            Dim lnk_ref_no_oncall As LinkButton = CType(row.FindControl("lnk_ref_no_oncall"), LinkButton)
            gv_approvers_oncall.DataSource = ws.Get_Application_Approver(lnk_ref_no_oncall.Text)
            gv_approvers_oncall.DataBind()
        Next

        If gv_oncall.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button3();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

        Button1.Style.Add("color", "#696969")
        Button1.Style.Add("background-color", "#FFFFFF")

        Button2.Style.Add("color", "#696969")
        Button2.Style.Add("background-color", "#FFFFFF")

        Button3.Style.Add("color", "#FFFFFF")
        Button3.Style.Add("background-color", "#FF0000")

        Button4.Style.Add("color", "#696969")
        Button4.Style.Add("background-color", "#FFFFFF")

        Button5.Style.Add("color", "#696969")
        Button5.Style.Add("background-color", "#FFFFFF")

        Button_DTR.Style.Add("color", "#696969")
        Button_DTR.Style.Add("background-color", "#FFFFFF")

        PN1.Visible = False
        PN2.Visible = False
        PN3.Visible = True
        PN4.Visible = False
        PN5.Visible = False
        PN6.Visible = False
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        GridView1.DataSource = ws.Get_My_Leave_List(Current_User.Employee_ID)
        GridView1.DataBind()

        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim row As GridViewRow = GridView1.Rows(i)
            Dim gv_approvers_leave As GridView = CType(row.FindControl("gv_approvers_leave"), GridView)
            Dim lnk_ref_no_leave As LinkButton = CType(row.FindControl("lnk_ref_no_leave"), LinkButton)
            gv_approvers_leave.DataSource = ws.Get_Application_Approver(lnk_ref_no_leave.Text)
            gv_approvers_leave.DataBind()
        Next

        If GridView1.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button4();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

        Button1.Style.Add("color", "#696969")
        Button1.Style.Add("background-color", "#FFFFFF")

        Button2.Style.Add("color", "#696969")
        Button2.Style.Add("background-color", "#FFFFFF")

        Button3.Style.Add("color", "#696969")
        Button3.Style.Add("background-color", "#FFFFFF")

        Button4.Style.Add("color", "#FFFFFF")
        Button4.Style.Add("background-color", "#FF0000")

        Button5.Style.Add("color", "#696969")
        Button5.Style.Add("background-color", "#FFFFFF")

        Button_DTR.Style.Add("color", "#696969")
        Button_DTR.Style.Add("background-color", "#FFFFFF")

        PN1.Visible = False
        PN2.Visible = False
        PN3.Visible = False
        PN4.Visible = True
        PN5.Visible = False
        PN6.Visible = False
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        gv_shift.DataSource = ws.Get_My_ShiftSched_List(Current_User.Employee_ID)
        gv_shift.DataBind()
        For i As Integer = 0 To gv_shift.Rows.Count - 1
            Dim row As GridViewRow = gv_shift.Rows(i)
            Dim gv_approvers_shift As GridView = CType(row.FindControl("gv_approvers_shift"), GridView)
            Dim lnk_ref_no_shift As LinkButton = CType(row.FindControl("lnk_ref_no_shift"), LinkButton)
            gv_approvers_shift.DataSource = ws.Get_Shift_Approver(lnk_ref_no_shift.Text)
            gv_approvers_shift.DataBind()
        Next

        If gv_shift.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button5();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

        Button1.Style.Add("color", "#696969")
        Button1.Style.Add("background-color", "#FFFFFF")

        Button2.Style.Add("color", "#696969")
        Button2.Style.Add("background-color", "#FFFFFF")

        Button3.Style.Add("color", "#696969")
        Button3.Style.Add("background-color", "#FFFFFF")

        Button4.Style.Add("color", "#696969")
        Button4.Style.Add("background-color", "#FFFFFF")

        Button5.Style.Add("color", "#FFFFFF")
        Button5.Style.Add("background-color", "#FF0000")

        Button_DTR.Style.Add("color", "#696969")
        Button_DTR.Style.Add("background-color", "#FFFFFF")

        PN1.Visible = False
        PN2.Visible = False
        PN3.Visible = False
        PN4.Visible = False
        PN5.Visible = True
        PN6.Visible = False
    End Sub

    Protected Sub Button_DTR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_DTR.Click

        Dim ds As New DataSet
        Dim sqlParam(0) As SqlParameter
        sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(0).Value = Current_User.Employee_ID

        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_MyRequest_DTR_List", sqlParam)
        gv_dtr.DataSource = ds
        gv_dtr.DataBind()

        For i As Integer = 0 To gv_dtr.Rows.Count - 1
            Dim row As GridViewRow = gv_dtr.Rows(i)
            Dim gv_approvers_dtr As GridView = CType(row.FindControl("gv_approvers_dtr"), GridView)
            Dim lnk_ref_no_dtr As LinkButton = CType(row.FindControl("lnk_ref_no_dtr"), LinkButton)
            gv_approvers_dtr.DataSource = ws.Get_Application_Approver(lnk_ref_no_dtr.Text)
            gv_approvers_dtr.DataBind()
        Next

        If gv_dtr.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button_dtr();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If

        Button1.Style.Add("color", "#696969")
        Button1.Style.Add("background-color", "#FFFFFF")

        Button2.Style.Add("color", "#696969")
        Button2.Style.Add("background-color", "#FFFFFF")

        Button3.Style.Add("color", "#696969")
        Button3.Style.Add("background-color", "#FFFFFF")

        Button4.Style.Add("color", "#696969")
        Button4.Style.Add("background-color", "#FFFFFF")

        Button5.Style.Add("color", "#696969")
        Button5.Style.Add("background-color", "#FFFFFF")

        Button_DTR.Style.Add("color", "#FFFFFF")
        Button_DTR.Style.Add("background-color", "#FF0000")

        PN1.Visible = False
        PN2.Visible = False
        PN3.Visible = False
        PN4.Visible = False
        PN5.Visible = False
        PN6.Visible = True
    End Sub

    Protected Sub gv_dtr_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gv_dtr.Sorting
        Dim ds As New DataSet
        Dim sqlParam(1) As SqlParameter
        sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(0).Value = Session("Employee_ID")
        sqlParam(1) = New SqlParameter("@sortvalue", SqlDbType.VarChar, 80)
        sqlParam(1).Value = e.SortExpression

        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_MyRequest_DTR_List_Sort", sqlParam)
        gv_dtr.DataSource = ds
        gv_dtr.DataBind()


        For i As Integer = 0 To gv_dtr.Rows.Count - 1
            Dim row As GridViewRow = gv_dtr.Rows(i)
            Dim gv_approvers_dtr As GridView = CType(row.FindControl("gv_approvers_dtr"), GridView)
            Dim lnk_ref_no_dtr As LinkButton = CType(row.FindControl("lnk_ref_no_dtr"), LinkButton)
            gv_approvers_dtr.DataSource = ws.Get_Application_Approver(lnk_ref_no_dtr.Text)
            gv_approvers_dtr.DataBind()
        Next

        If gv_dtr.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>button_dtr();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If
    End Sub
End Class
