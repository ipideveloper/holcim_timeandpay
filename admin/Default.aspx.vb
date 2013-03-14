Imports cls_GlobalFunction
Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient


Partial Class admin_Default
    Inherits System.Web.UI.Page

    Private Function UserLogin(ByVal Username As String, ByVal Password As String) As Boolean
        Dim nResult As Boolean = False
        Dim nPassword As String = ""
        Dim nRoleID As Integer = 0


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
                    nRoleID = objReader.GetInt32(3)
                End While
                If Password = decrypt(nPassword, "GECOFYFRDEY") Then
                    nResult = True
                    Session("User") = Username
                    Session("RoleID") = nRoleID
                Else
                    Session("User") = ""
                    Session("RoleID") = ""
                    nResult = False
                End If
            Else
                nResult = False
            End If

        Catch ex As Exception
            'Response.Redirect("Error.aspx?message=" & ex.Message)
        End Try

        
        Return nResult
    End Function


    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        Dim vUserName As String = username.Text
        Dim vPassword As String = password.Text


        If vUserName = "" Or vPassword = "" Then
            If vUserName = "" Then
                username.Style("background-color") = "#FFD8D4"
                LoginMessage.Style("color") = "Red"
                LoginMessage.Text = "Username/Password is blank!"
            Else
                username.Style("background-color") = ""
            End If

            If vPassword = "" Then
                password.Style("background-color") = "#FFD8D4"
                LoginMessage.Style("color") = "Red"
                LoginMessage.Text = "Username/Password is blank!"
            Else
                password.Style("background-color") = ""
            End If
        Else
            If UserLogin(vUserName, vPassword) Then
                Response.Redirect("Main.aspx")
            Else
                LoginMessage.Style("color") = "Red"
                LoginMessage.Text = "Invalid Username/Password!"
            End If
        End If
        
    End Sub

    Protected Sub LoginButton_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginButton.PreRender
        LoginButton.Style("width") = "100px"
        LoginButton.Style("padding-left") = "20px"
        LoginButton.Style("padding-right") = "20px"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("page") = "expired" Then
            LoginMessage.Style("color") = "Red"
            LoginMessage.Text = "Session expired. Please login to continue."
        End If
    End Sub
End Class
