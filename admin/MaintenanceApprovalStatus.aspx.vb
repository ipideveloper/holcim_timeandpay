Imports cls_GlobalFunction
Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient

Partial Class admin_MaintenanceApprovalStatus
    Inherits System.Web.UI.Page

    Protected Shared Sub InsertApprovalStatus(ByVal status As String)

        Dim ds As New DataSet
        Dim sqlParam(0) As SqlParameter

        sqlParam(0) = New SqlParameter("@status", SqlDbType.VarChar, 50)
        sqlParam(0).Value = status

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Insert_ApprovalStatus", sqlParam)
    End Sub

    Protected Sub Button_AddNewApprovalStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_AddNewApprovalStatus.Click
        Dim ApprovalStatus As String = Status.Text

        Try
            InsertSystemLogs("Maintenance", "Add Approval Status", 1, "Add New Approval Status '" & ApprovalStatus & "'", Session("User"))
            InsertApprovalStatus(ApprovalStatus)

        Catch ex As Exception
            InsertSystemLogs("Maintenance", "Add Approval Status", 2, ex.Message.ToString(), Session("User"))
        End Try

        Response.Redirect("MaintenanceApprovalStatus.aspx")
    End Sub
End Class
