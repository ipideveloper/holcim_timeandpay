Imports cls_GlobalFunction
Imports cls_Email_Notifications
Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail

Partial Class admin_ForgotPassword
    Inherits System.Web.UI.Page

    Private SMPTServer As String = System.Configuration.ConfigurationManager.AppSettings("SMPTServer")
    Private DefURL As String = ConfigurationManager.AppSettings("DefaultURL").ToString

    Protected Sub BackButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackButton.Click
        Response.Redirect("Default.aspx")

    End Sub

    Protected Sub RetrievePasswordButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RetrievePasswordButton.Click

        Dim v_username As String = username.Text
        Dim v_password As String = ""
        Dim v_email As String = ""

        Dim Conn As New SqlConnection(ConnectionString)
        Dim Cmd As New SqlCommand("spWeb_Get_SysManagerUserEmail", Conn)
        Cmd.CommandType = CommandType.StoredProcedure

        Dim objParam1 As SqlParameter
        objParam1 = Cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 20)
        objParam1.Direction = ParameterDirection.Input
        objParam1.Value = v_username

        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
        Dim objReader As SqlDataReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If objReader.HasRows Then
            While objReader.Read()
                v_password = decrypt(objReader.GetString(2), "GECOFYFRDEY")
                v_email = objReader.GetString(4)
            End While

            Dim body As String = ""
            body = v_username + " " + "your password is " & v_password & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."

            Dim recipients As String = v_email

            Dim HtmlBody As String
            Dim Mailmsg As New MailMessage("Time&Pay-PHL@holcim.com", v_email)
            Mailmsg.Subject = "Time and Pay Notice - " & v_username
            HtmlBody = "BodyPart<br><br><br><br>You can access this application at the intranet or through this Link:<a href=""httpaddressadmin"">Time and Pay</a>"
            HtmlBody = Replace(HtmlBody, "BodyPart", body)
            HtmlBody = Replace(HtmlBody, "httpaddress", DefURL)
            Mailmsg.Body = HtmlBody
            'Mailmsg.Body = body & " <br><br><br><br> " & DefURL
            Mailmsg.Priority = MailPriority.High
            Mailmsg.IsBodyHtml = True
            Dim smtpMail As New SmtpClient
            Dim strHost As String = SMPTServer
            smtpMail.Host = strHost
            smtpMail.Send(Mailmsg)


            ForgotPasswordMessage.Text = "Password has been sent to " & v_email
        Else
            ForgotPasswordMessage.Text = "Username doesn't exist!"
        End If



       
    End Sub
End Class
