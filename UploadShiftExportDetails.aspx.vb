Imports System.Data
Imports System.Data.SqlClient

Partial Class UploadShiftExportDetails
    Inherits System.Web.UI.Page
    Private ws As New localhost.Service
    Private cls_email As New cls_Email_Notifications

    Public ReadOnly Property Current_User() As Cls_UserProperty
        Get
            Return New Cls_UserProperty(ViewState("Employee_ID"))
        End Get
    End Property
    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Session.IsNewSession Then
            UserMsgBox("Session Expired!")
            Response.Redirect("Default.aspx")
        End If
        Try

        
            If Not IsPostBack() Then
                ViewState("Employee_ID") = Session("Employee_ID")
                GridView5.Dispose()
                GridView5.DataSource = ws.Get_Shift_History2(Session("EmpID"), Session("workdate"))
                GridView5.DataBind()


                If GridView5.Rows.Count = 0 Then
                    UserMsgBox("No changes made on the selected shift.")
                Else
                    For i As Integer = 0 To GridView5.Columns.Count - 1
                        GridView5.Columns(0).ItemStyle.Width = 50
                        GridView5.Columns(0).ItemStyle.Width = 130
                        GridView5.Columns(0).ItemStyle.Width = 50
                        GridView5.Columns(0).ItemStyle.Width = 50
                        GridView5.Columns(0).ItemStyle.Width = 120
                    Next
                End If
            End If

        Catch ex As Exception

        End Try
       
    End Sub
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

    Private Sub OpenModalPage(ByVal PageName As String, ByVal vheight As Integer, ByVal vwidth As Integer)

        Dim strScript As String = "<script language=JavaScript>"

        strScript &= "self.open('" & PageName & "','EmailPopup','fullscreen=no,status=no,scrollbars=no,address=no,resizable=no,height=" & vheight & ",width=" & vwidth & ",left=1,top=1')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If

    End Sub

    Private Sub OpenModalPage2(ByVal PageName As String, ByVal vheight As Integer, ByVal vwidth As Integer)

        Dim strScript As String = "<script language=JavaScript>"

        strScript &= "window.open('" & PageName & "','EmailPopup','fullscreen=no,status=no,scrollbars=no,address=no,resizable=no,height=" & vheight & ",width=" & vwidth & ",left=1,top=1')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If

    End Sub
#End Region
End Class
