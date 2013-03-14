Imports cls_GlobalFunction
Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient


Partial Class admin_MaintenanceUserType
    Inherits System.Web.UI.Page

    Protected Shared Sub InsertUserType(ByVal UserType As String)

        Dim ds As New DataSet
        Dim sqlParam(0) As SqlParameter

        sqlParam(0) = New SqlParameter("@UserRole", SqlDbType.VarChar, 50)
        sqlParam(0).Value = UserType

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Insert_UserRoles", sqlParam)
    End Sub

    Protected Sub Button_AddUserType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_AddUserType.Click


        Dim vUserType As String = UserType.Text

        Try
            InsertSystemLogs("Maintenance", "Add User Type", 1, "Add New User Type '" & vUserType & "'", Session("User"))
            InsertUserType(vUserType)

        Catch ex As Exception
            InsertSystemLogs("Maintenance", "Add User Type", 2, ex.Message.ToString(), Session("User"))
        End Try

        Response.Redirect("MaintenanceUserType.aspx")

    End Sub
End Class
