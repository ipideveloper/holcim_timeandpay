
Partial Class my_schedules_print
    Inherits System.Web.UI.Page
    Private ws As New localhost.Service

    Public ReadOnly Property Current_User() As Cls_UserProperty
        Get
            Dim EmployeeID As String = Request.QueryString("employee_id")
            Return New Cls_UserProperty(EmployeeID)
        End Get
    End Property

    Private Sub get_current_userlog(ByVal pemployee_id As String)
        PeronnelNoLabel.Text = Current_User.Employee_ID
        FirstNameLabel.Text = Current_User.FirstName
        MiddleNameLabel.Text = Current_User.Middle_Name
        LastNameLabel.Text = Current_User.LastName
        SectorLabel.Text = Current_User.Branch_id
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Dim EmployeeID As String = Request.QueryString("employee_id")
                Dim pMonth As String = Request.QueryString("pMonth")
                Dim pYear As String = Request.QueryString("pYear")

                get_current_userlog(EmployeeID)


                Try
                    GridView1.DataSource = ws.Get_Employee_Schedules(EmployeeID, pMonth, CType(pYear, Integer))
                    GridView1.DataBind()

                Catch ex As Exception
                    UserMsgBox(ex.Message)
                End Try
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
#End Region

    Protected Sub btn_close_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_close.PreRender
        btn_close.Attributes.Add("onclick", "javascript:self.close();")
    End Sub


    Protected Sub btn_print_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_print.PreRender
        btn_print.Attributes.Add("onclick", "javascript:self.print();")
    End Sub
End Class
