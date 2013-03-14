Imports System.Data

Partial Class leave_details
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
        get_leave_header_details(Request.QueryString("ref_no"))
        get_leave_detail_dates(Request.QueryString("ref_no"))
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub get_leave_header_details(ByVal ref_no As String)
        Dim dr As DataRow = ws.Get_Leave_Details(ref_no).Tables(0).Rows(0)
        With dr
            txt_name.Text = dr("employee_name")
            txt_emp_id.Text = dr("employee_id")
            txt_ref_no.Text = dr("ref_no")
            txt_leave_type.Text = dr("application_type")
            txt_date_covered.Text = dr("date_covered")
            txt_total_days.Text = CType(dr("days"), Decimal)
            txt_date_filed.Text = dr("date_created")
            txt_reason.Text = dr("reason")
            txt_status.Text = dr("status")
        End With
    End Sub

    Private Sub get_leave_detail_dates(ByVal ref_no As String)
        gv_dates.DataSource = ws.Get_Leave_Details_Dates(ref_no)
        gv_dates.DataBind()
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
