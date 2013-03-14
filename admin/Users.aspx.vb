Imports cls_GlobalFunction
Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient



Partial Class admin_Users
    Inherits System.Web.UI.Page
    Protected Shared Sub InsertSystemManagerUsers(ByVal Username As String, _
                                       ByVal Password As String, _
                                       ByVal EmployeeID As String, _
                                       ByVal UserRoleID As Integer)
        Dim ds As New DataSet
        Dim sqlParam(3) As SqlParameter

        sqlParam(0) = New SqlParameter("@Username", SqlDbType.VarChar, 20)
        sqlParam(0).Value = Username

        sqlParam(1) = New SqlParameter("@Password", SqlDbType.VarChar, 255)
        sqlParam(1).Value = encrypt(Password, "GECOFYFRDEY")

        sqlParam(2) = New SqlParameter("@EmployeeID", SqlDbType.VarChar, 15)
        sqlParam(2).Value = EmployeeID

        sqlParam(3) = New SqlParameter("@UserRoleID", SqlDbType.Int)
        sqlParam(3).Value = UserRoleID

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Insert_SystemManagerUsers", sqlParam)
    End Sub

    Protected Sub Button_AddNewUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_AddNewUser.Click
        Dim vUserRoleID As Integer = DropDownList_UserType.SelectedValue
        Dim vUsername As String = username.Text
        Dim vEmployeeID As String = employeeid.Text

        Try
            InsertSystemLogs("Users", "Add", 1, "Add New User '" & vUsername & "'", "admin")
            InsertSystemManagerUsers(vUsername, "abc123", vEmployeeID, vUserRoleID)

        Catch ex As Exception
            InsertSystemLogs("Users", "Add", 2, ex.Message.ToString(), "admin")
        End Try

        Response.Redirect("Users.aspx")
    End Sub
End Class