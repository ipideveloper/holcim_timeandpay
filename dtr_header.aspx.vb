Imports System.Data
Imports cls_Email_Notifications
Imports HolcimDbClass
Imports System.Data.SqlClient
Partial Class dtr_header
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
        If Session.IsNewSession Then
            UserMsgBox("Session Expired!")
            Response.Redirect("Default.aspx")
        End If

        Try
            If Not Page.IsPostBack Then
                Session("pageIsSorted") = ""
                Initialize()
            End If
        Catch ex As Exception
        End Try


    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Session("Employee_ID") = ViewState("Employee_ID")
        Response.Redirect("dtr_apply.aspx")
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        Get_DTR_List(Session("Employee_ID"))
        get_approvers()
      
    End Sub

    Private Sub get_approvers()
        For i As Integer = 0 To GridView1.Rows.Count - 1
            Dim row As GridViewRow = GridView1.Rows(i)
            Dim gv_approvers As GridView = CType(row.FindControl("gv_approvers"), GridView)
            Dim lnk_ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
            gv_approvers.DataSource = ws.Get_Application_Approver(lnk_ref_no.Text)
            gv_approvers.DataBind()
        Next
    End Sub

    Public Sub Get_DTR_List(ByVal employee_id As String) 'As DataSet
        Dim ds As New DataSet
        Dim sqlParam(0) As SqlParameter
        sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(0).Value = employee_id

        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_DTR_List", sqlParam)
        GridView1.DataSource = ds
        GridView1.DataBind()
    End Sub

#End Region

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        Dim ds As New DataSet
        If Session("pageIsSorted") = "" Then


            Dim sqlParam(0) As SqlParameter
            sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
            sqlParam(0).Value = Current_User.Employee_ID

            ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                                "spWeb_Get_DTR_List", sqlParam)
        Else

            Dim sqlParam(1) As SqlParameter
            sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
            sqlParam(0).Value = Session("Employee_ID")
            sqlParam(1) = New SqlParameter("@sortvalue", SqlDbType.VarChar, 80)
            sqlParam(1).Value = Session("pageIsSorted")

            ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                                "spWeb_Get_DTR_List_Sort", sqlParam)
        End If
        
        GridView1.DataSource = ds
        GridView1.PageIndex = e.NewPageIndex
        GridView1.DataBind()

        get_approvers()
    End Sub


    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lb As LinkButton = e.Row.FindControl("lnk_ref_no")

            lb.Attributes.Add("onclick", "javascript:mywin=self.open('dtr_details.aspx?ref_no=" & lb.Text & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=350,width=500,left=1,top=1');mywin.focus();")
            'lb.Attributes.Add("onclick", String.Format("javascript:return confirm('Are you sure you want to delete Marketing Approver {0} ?')", e.Row.Cells(2).Text))

            'e.Row.Cells(1).Style("padding-left") = "10px"
        End If
    End Sub
    Protected Sub GridView1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView1.Sorting
        Dim ds As New DataSet
        Dim sqlParam(1) As SqlParameter
        sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(0).Value = Session("Employee_ID")
        sqlParam(1) = New SqlParameter("@sortvalue", SqlDbType.VarChar, 80)
        sqlParam(1).Value = e.SortExpression

        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_DTR_List_Sort", sqlParam)
        GridView1.DataSource = ds
        GridView1.DataBind()
        If GridView1.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>freezeheader();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If
        get_approvers()

        Session("pageIsSorted") = e.SortExpression

    End Sub

#Region "Javascripts"

    Private Sub OpenModalPage(ByVal RefNo As String)
        Session("RefNo") = RefNo
        Dim strScript As String = "<script language=javascript>"
        'strScript &= "if (true == my_window.opened) { my_window.close();} else {my_window.open();}"
        strScript &= "my_window = self.open('overtime_details.aspx','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=480,width=500,left=1,top=1');"
        'strScript &= ""
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If
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


#End Region

End Class
