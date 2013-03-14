
Partial Class my_dtr
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

    Protected Sub btn_bind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_bind.Click
        Try

            If Not dpl_month.SelectedIndex = 0 Then
                Bind_Schedules()

            End If
        Catch ex As Exception
        End Try
    End Sub

    

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        Dim i As Integer
        For i = 2010 To 2999
            dpl_year.Items.Add(CType(i, String))
        Next
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub Bind_Schedules()
        Try
            GridView1.DataSource = ws.Get_Employee_DTR(Current_User.Employee_ID, dpl_month.SelectedIndex, CType(dpl_year.Text, Integer))
            GridView1.DataBind()

            If GridView1.Rows.Count > 10 Then
                Dim strScript As String = "<script language='javascript' id='myClientScript'>freezeheader();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
            End If

            If GridView1.Rows.Count = 0 Then
                txtnolog.Visible = True
                btnPrintDTR.Enabled = False
            Else
                txtnolog.Visible = False
                btnPrintDTR.Enabled = True
            End If
            
        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
        
    End Sub

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

    Protected Sub btnPrintDTR_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintDTR.PreRender
        Dim EmployeeID As String = Current_User.Employee_ID
        Dim pMonth As String = dpl_month.SelectedIndex
        Dim pYear As String = dpl_year.Text

        If pMonth = "- - Select Month - -" Or pYear = "- - Select Year - -" Then
            btnPrintDTR.Enabled = False
        Else
            btnPrintDTR.Attributes.Add("onclick", "javascript:mywin=self.open('my_dtr_print.aspx?employee_id=" & EmployeeID & "&pmonth=" & pMonth & "&pyear=" & pYear & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=500,width=800,left=1,top=1');mywin.focus();return false;")
        End If
        
    End Sub
End Class
