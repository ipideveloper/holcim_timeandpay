Imports System.Data
Imports cls_Email_Notifications


Partial Class forgot_password
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
    Private cls_email As New cls_Email_Notifications

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
        If Not Page.IsPostBack Then
            If Session("expired") = "Yes" Then
                Session("expired") = Nothing
                UserMsgBox("Session Expired!")
            End If
            Session.Clear()
        End If
        TextBox1.Focus()
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            Dim vUsername As String = Trim(TextBox1.Text)


            If validate_userlogin_only(vUsername) = False Then

                UserMsgBox("Personnel No. doesn't exist")
                TextBox1.Focus()

            End If



        Catch ex As Exception

        End Try

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Back.Click
        Response.Redirect("Default.aspx")

    End Sub

#End Region

#Region "Validation"

    Private Function validate_userlogin_only(ByVal vUsername As String) As Boolean
        Dim nResult As Boolean = False
        If Not ws.Get_User_Info(vUsername).Tables(0).Rows.Count <= 0 Then
            'Dim strPass As String = cls_GlobalFunction.decrypt(ws.Get_User_Info(vUsername).Tables(0).Rows(0).Item("emp_password"), "GECOFYFRDEY")
            'If strPass = vPassword Then
            Dim vPassword As String

            vPassword = cls_GlobalFunction.decrypt(ws.Get_User_Info(vUsername).Tables(0).Rows(0).Item("emp_password"), "GECOFYFRDEY")

            send_notifications(vUsername, vPassword)
            'UserMsgBox(vPassword)

            nResult = True
            'End If
        End If

        Return nResult

    End Function

#End Region

#Region "Manage"

    Private Sub send_notifications(ByVal vUsername As String, ByVal vPassword As String)
        Dim body As String
        Dim recipients As String = ""
        Dim UserInfo As DataSet

        UserInfo = ws.Get_User_Info(vUsername)

        Dim varName As String
        Dim dr1 As DataRow = UserInfo.Tables(0).Rows(0)
        varName = Trim(dr1("last_name")) + ", " + Trim(dr1("first_name")) + " " + Trim(dr1("middle_name"))



        body = vUsername + " " + varName + " " + "your password is " & vPassword & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."

        'Current_User.LastName & " " & Current_User.FirstName & " is applying for " & UCase(dplType.SelectedItem.Text) & " LEAVE. For your approval, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."

        Dim ds As DataSet

        ds = ws.Get_Users_Email(vUsername)

        'For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
        'Dim dr As DataRow = ds.Tables(0).Rows(i)
        'If recipients = "" Then
        'recipients = dr("email")
        'Else
        'recipients = recipients & ", " & dr("email")
        'End If

        'Next
        ' UserMsgBox(ctype(ds.Tables(0).Rows.Count,String))

        Dim dr As DataRow = ds.Tables(0).Rows(0)

        recipients = dr("email")


        If recipients = "" Then
            UserMsgBox("Email address needs to be updated. Please contact your HR specialist")
            Try
                'Insert_Email_Summary_Notification(recipients, body, varName)
            Catch ex As Exception
                UserMsgBox(ex.Message)
            End Try

        Else

            cls_email.SendEmail(recipients, body, varName)
            Try
                'Insert_Email_Summary_Notification(recipients, body, varName)
                btnLogin.Enabled = False
                UserMsgBox("Your password has been sent to your email: " & recipients)
            Catch ex As Exception
                UserMsgBox(ex.Message)

            End Try

        End If



    End Sub

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
