Imports System.Data
Imports cls_Email_Notifications
Imports HolcimDbClass
Imports System.Data.SqlClient
Partial Class dtr_details
    Inherits System.Web.UI.Page

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

    Private Sub Initialize()
        'Get_DTR_Details(Request.QueryString("ref_no"))

        Dim dr As DataRow = Get_DTR_Details(Request.QueryString("ref_no")).Tables(0).Rows(0)
        With dr
            employee_name.Text = dr("employee_name")
            employee_id.Text = dr("employee_id")
            ref_no.Text = dr("ref_no")
            date_created.Text = dr("date_created")
        End With

        GridView1.DataSource = Get_DTR_Details_Details(Request.QueryString("ref_no"))
        GridView1.DataBind()

    End Sub


    Public Function Get_DTR_Details(ByVal ref_no As String) As DataSet
        Dim ds As New DataSet
        Dim sqlParam(0) As SqlParameter

        sqlParam(0) = New SqlParameter("@ref_no", SqlDbType.VarChar, 15)
        sqlParam(0).Value = ref_no

        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_DTR_Details", sqlParam)
        Return ds
    End Function

    Public Function Get_DTR_Details_Details(ByVal ref_no As String) As DataSet
        Dim ds As New DataSet
        Dim sqlParam(0) As SqlParameter

        sqlParam(0) = New SqlParameter("@ref_no", SqlDbType.VarChar, 15)
        sqlParam(0).Value = ref_no

        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_DTR_Details_Details", sqlParam)
        Return ds
    End Function

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
