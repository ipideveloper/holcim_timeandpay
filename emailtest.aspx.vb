Imports System.Net.Mail
Imports cls_Email_Notifications

Partial Class emailtest
    Inherits System.Web.UI.Page



    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load

        Dim recipient As String = "jsd@ipiphil.com"
        Dim SenderName As String = "Jan Dizon"
        Dim body As String = "TEST"

        Dim SMPTServer As String = System.Configuration.ConfigurationManager.AppSettings("SMPTServer")
        Dim DefURL As String = ConfigurationManager.AppSettings("DefaultURL").ToString

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
    End Sub
End Class
