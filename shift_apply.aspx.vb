Imports System.Data

Partial Class shift_apply
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

        Try
            If Not Page.IsPostBack Then
                Initialize()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btn_bind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_bind.Click
        If Not dpl_month.SelectedIndex = 0 Then
            OpenModalPage(dpl_month.SelectedIndex, dpl_year.Text)
        End If
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        Dim i As Integer

        For i = 2010 To 2999
            dpl_year.Items.Add(CType(i, String))
        Next
        '  dpl_year.Text = CType(Year(Now()), String)
    End Sub

#End Region

#Region "Validation"



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


    Private Sub OpenModalPage(ByVal month As Integer, ByVal _year As Integer)
        Session("shift_month") = month
        Session("shift_year") = _year
        Session("Employee_ID") = ViewState("Employee_ID")
        Dim strScript As String = "<script language=JavaScript>"
        strScript &= "self.open('shift_details.aspx','EmailPopup','fullscreen=yes,status=no,scrollbars=yes,address=no,resizable=yes,height=100%,width=100%,left=200,top=100')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If
    End Sub

#End Region

End Class
