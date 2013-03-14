
Partial Class change_password
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

        Try
            If Not Page.IsPostBack Then
                Initialize()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click
        Try
            If Session("btn_name") = "btnChange" Then
                If validate_old_password(txtold.Text) = False Then
                    UserMsgBox("Old password is incorrect")
                    Session("btn_name") = "btnChange"
                Else
                    update_password()
                    UserMsgBox("Password has been changed, please login again.")
                    Session("btn_name") = "btncancel"
                    btncancel.Text = "Logout"
                    btnChange.Visible = False
                    'Response.Redirect("Default.aspx")
                End If
            End If
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub btncancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncancel.Click
        If btncancel.Text = "cancel" Then
            Session("btn_name") = "btnMyAppStatus"
            Response.Redirect("my_app_status.aspx")
        Else
            Response.Redirect("Default.aspx")
        End If
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        Session("btn_name") = "btnChange"
        btnChange.Focus()
    End Sub

#End Region

#Region "Validation"

    Private Function validate_old_password(ByVal pold_password As String) As Boolean
        Dim strPass As String
        strPass = Current_User.Password
        If strPass = pold_password Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Insert"



#End Region

#Region "Update"

    Private Sub update_password()
        Try
            ws.Update_Password(Current_User.Employee_ID, cls_GlobalFunction.encrypt(Trim(txtnew.Text), "GECOFYFRDEY"))
        Catch ex As Exception
            usermsgbox(ex.Message)
        End Try

    End Sub

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
