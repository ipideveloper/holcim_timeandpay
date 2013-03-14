
Partial Class leave_approvers
    Inherits System.Web.UI.Page

    Private ws As New localhost.Service

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            gv_approvers.DataSource = ws.Get_Application_Approver(Request.QueryString("ref_no"))
            gv_approvers.DataBind()
        End If
    End Sub
End Class
