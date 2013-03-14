Imports System.Data

Partial Class overtime_details
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

#End Region

#Region "Initialization"

    Private Sub Initialize()
        get_overtime_details(Request.QueryString("ref_no"))
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub get_overtime_details(ByVal ref_no As String)
        Dim dr As DataRow = ws.Get_Overtime_Details(ref_no).Tables(0).Rows(0)
        With dr
            txt_name.Text = dr("employee_name")
            txt_emp_id.Text = dr("employee_id")
            txt_ref_no.Text = dr("ref_no")
            txt_planner.Text = dr("planner")
            txt_date_time.Text = dr("work_date")
            txt_reasons.Text = dr("reason")
            txt_on_call.Text = IIf(CType(dr("on_call"), Boolean) = True, "Yes", "No")
            txt_date_filed.Text = dr("date_created")
            txt_remarks.Text = dr("remarks")
            txt_status.Text = dr("status")
            ddl_classification.SelectedValue = dr("classification")
        End With
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


#End Region

End Class
