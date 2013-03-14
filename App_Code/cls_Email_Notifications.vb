Imports Microsoft.VisualBasic
Imports System.Net.Mail
Imports System
Imports System.Configuration
Imports System.Text
Imports System.Web.UI.HtmlControls
Imports System.Web.UI
Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient
Public Class cls_Email_Notifications
    Inherits System.Web.UI.Page

    Private SMPTServer As String = System.Configuration.ConfigurationManager.AppSettings("SMPTServer")
    Private DefURL As String = ConfigurationManager.AppSettings("DefaultURL").ToString

    Public Shared Sub Insert_Email_Summary_Notification(ByVal recipient As String, _
                                       ByVal body As String, _
                                       ByVal sender As String)
        Dim ds As New DataSet
        Dim sqlParam(2) As SqlParameter

        sqlParam(0) = New SqlParameter("@recipient", SqlDbType.VarChar, 150)
        sqlParam(0).Value = recipient

        sqlParam(1) = New SqlParameter("@body", SqlDbType.Text)
        sqlParam(1).Value = body

        sqlParam(2) = New SqlParameter("@sender", SqlDbType.VarChar, 150)
        sqlParam(2).Value = sender

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Insert_Email_Summary_Notification", sqlParam)

    End Sub

    Public Sub SendEmail(ByVal recipient As String, ByVal body As String, ByVal SenderName As String)
        Try
            Dim HtmlBody As String
            Dim Mailmsg As New MailMessage("Time&Pay-PHL@holcim.com", recipient)
            Mailmsg.Subject = "Time and Pay Notice - " & SenderName
            HtmlBody = "BodyPart<br><br><br><br>You can access this application at the intranet or through this Link:<a href=""httpaddress"">Time and Pay</a>"
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
        Catch ex As Exception
            Dim exmsg As String
            exmsg = ex.Message.ToString
            UserMsgBox(exmsg)
        End Try
    End Sub

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
End Class
