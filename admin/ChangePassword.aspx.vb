Imports cls_GlobalFunction
Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient

Partial Class admin_ChangePassword
    Inherits System.Web.UI.Page


    Protected Shared Sub UpdateSystemManagerUserPassword(ByVal Username As String, _
                                       ByVal Password As String)
        Dim ds As New DataSet
        Dim sqlParam(1) As SqlParameter

        sqlParam(0) = New SqlParameter("@Username", SqlDbType.VarChar, 20)
        sqlParam(0).Value = Username

        sqlParam(1) = New SqlParameter("@Password", SqlDbType.VarChar, 255)
        sqlParam(1).Value = encrypt(Password, "GECOFYFRDEY")

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Update_SysManager_UserPassword", sqlParam)
    End Sub


    Protected Function CurrentPasswordIsValid(ByVal Username As String, ByVal Password As String) As Boolean
        Dim nResult As Boolean = False
        Dim nPassword As String = ""

        Dim Conn As New SqlConnection(ConnectionString)
        Dim Cmd As New SqlCommand("spWeb_Get_SysManagerUserInfo", Conn)
        Cmd.CommandType = CommandType.StoredProcedure

        Dim objParam1 As SqlParameter
        objParam1 = Cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 20)
        objParam1.Direction = ParameterDirection.Input
        objParam1.Value = Username

        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If

            Dim objReader As SqlDataReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If objReader.HasRows Then
                While objReader.Read()
                    nPassword = objReader.GetString(1)
                End While
            End If

        Catch ex As Exception
            'Response.Redirect("Error.aspx?message=" & ex.Message)
        End Try

        If Password = decrypt(nPassword, "GECOFYFRDEY") Then
            nResult = True
        Else
            nResult = False
        End If
        Return nResult
    End Function


    Protected Sub Button_Cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_Cancel.Click
        Response.Redirect("Main.aspx")
    End Sub

    Protected Sub Button_ChangePassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_ChangePassword.Click

        If CurrentPasswordIsValid(Session("User"), CurrentPassword.Text) Then
            Label_CurrentPassword.Style("margin-left") = ""
            Label_CurrentPassword.Style("color") = ""
            Label_CurrentPassword.Text = ""

            Try
                InsertSystemLogs("Users", "Change Password", 1, "User '" & Session("User") & "' changed password", Session("User"))
                UpdateSystemManagerUserPassword(Session("User"), NewPassword.Text)
                SuccessMessage.Visible = True
            Catch ex As Exception
                InsertSystemLogs("Users", "Change Password", 2, ex.Message.ToString(), Session("User"))
            End Try

        Else
            Label_CurrentPassword.Style("margin-left") = "10px"
            Label_CurrentPassword.Style("color") = "#FC1002"
            Label_CurrentPassword.Text = "Password is invalid!"
        End If



    End Sub
End Class
