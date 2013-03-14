Imports System.Data


Partial Class change_abc123_password
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
            Response.Redirect("Default.aspx")
        End If


        If Not Page.IsPostBack Then
            If Session("expired") = "Yes" Then
                Session("expired") = Nothing
                UserMsgBox("Session Expired!")
                Session.Clear()
            End If
        End If
        Session("btn_name") = "btnChange"
        ViewState("Employee_ID") = Session("Employee_ID")
        TextBox1.Text = Current_User.Employee_ID
        txtnew.Focus()
    End Sub

    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click, btnBack.Click
        Try
            If Session("btn_name") = "btnChange" Then
                update_password()
                Session("btn_name") = "btnBack"
            End If
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try

    End Sub
    Private Sub goto_default()
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        goto_default()

    End Sub

  
#End Region

#Region "Update"

    Private Sub update_password()
        Try
            ws.Update_Password(Current_User.Employee_ID, cls_GlobalFunction.encrypt(Trim(txtnew.Text), "GECOFYFRDEY"))
            btnChange.Visible = False
            'added by andrew 11-16-2011 start
            Session("expired") = Nothing
            'added by andrew 11-16-2011 end
            UserMsgBox("Your password has been changed, please log-in again")
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try

    End Sub

#End Region


#Region "Javascripts"

    Private Sub UserMsgBox(ByVal sMsg As String)
        Try

        
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
        Catch ex As Exception
            TextBox1.Text = ex.Message
        End Try
    End Sub

#End Region

End Class
