
Partial Class _Default
    Inherits System.Web.UI.Page


#Region "Properties"


#End Region

#Region "Variables"

    Private ws As New localhost.Service

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Session("expired") = "Yes" Then
                Session("expired") = Nothing
                UserMsgBox("Session Expired! Kindly Log-in again")
            End If
            Session.Clear()
        End If
        TextBox1.Focus()

        If Request.QueryString("error") = "nonsector" Then
            UserMsgBox("Please login only on your sector Time and Pay site!")
        End If

    End Sub

    Protected Sub forgotpassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles forgotpassword.Click
        Try
            Dim id As String = Trim(TextBox1.Text)

            Session("Employee_ID") = id
            Session("Log") = id
            Response.Redirect("forgot_password.aspx")
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            Dim vUsername As String = Trim(TextBox1.Text)
            Dim vPassword As String = Trim(TextBox2.Text)
            If validate_login(vUsername, vPassword) = True Then
                Session("Employee_ID") = vUsername
                Session("Log") = vUsername

                If Not ws.CheckSession(vUsername) = True Then
                    ws.ActivateSession(vUsername, True)
                    If vPassword = "abc123" Then
                        Response.Redirect("change_initial_password.aspx")
                    Else
                        Response.Redirect("my_app_status.aspx")
                    End If
                Else
                    UserMsgBox("Username already in use..")
                End If
            Else
                UserMsgBox("Invalid Username/Password")
            End If
        Catch ex As Exception
            UserMsgBox("No Database Connection!")
            'UserMsgBox(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Initialization"



#End Region

#Region "Validation"

    Private Function validate_login(ByVal vUsername As String, ByVal vPassword As String) As Boolean
        Dim nResult As Boolean = False
        If Not ws.Get_User_Info(vUsername).Tables(0).Rows.Count <= 0 Then
            Dim strPass As String = cls_GlobalFunction.decrypt(ws.Get_User_Info(vUsername).Tables(0).Rows(0).Item("emp_password"), "GECOFYFRDEY")
            Session("Branch_ID") = ws.Get_User_Info(vUsername).Tables(0).Rows(0).Item("branch_id")
            Session("Organization_ID") = ws.Get_User_Info(vUsername).Tables(0).Rows(0).Item("organization")

            Dim sector As String = Session("Branch_ID")

            'For Sector Restriction
            'Select Case sector
            '    Case "PLG0"
            '        Response.Redirect("default.aspx?error=nonsector&sector=PLG0")
            '    Case "PDV0"
            '        Response.Redirect("default.aspx?error=nonsector&sector=PDV0")
            '    Case "PBL0"
            '        Response.Redirect("default.aspx?error=nonsector&sector=PBL0")
            '    Case "PLN0"
            '        Response.Redirect("default.aspx?error=nonsector&sector=PLN0")
            'End Select

            If strPass = vPassword Then
                ws.ActivateSession("63000618", False)
                nResult = True
            End If
        End If
        Return nResult
    End Function

#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"



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