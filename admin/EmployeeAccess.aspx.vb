Imports HolcimDbClass
Imports System.Data
Imports System
Imports System.Data.SqlClient



Partial Class admin_EmployeeAccess
    Inherits System.Web.UI.Page

    'Protected Sub Button_ShowAccess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_ShowAccess.Click

    '    Dim ds As DataSet
    '    Dim sqlParam(0) As SqlParameter
    '    sqlParam(0) = New SqlParameter("@employeeid", SqlDbType.VarChar, 20)
    '    sqlParam(0).Value = employeeid.Text

    '    ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spWeb_GetEmployeeModuleAccess", sqlParam)

    '    If ds.Tables.Count > 0 Then
    '        GridView1.DataSource = ds
    '        GridView1.DataBind()
    '        GridView1.Visible = True
    '    Else
    '        GridView1.Dispose()
    '        GridView1.Visible = False

    '    End If

    '    RadGrid1.Visible = False

    'End Sub

    'Protected Sub Button_ShowAllAccess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_ShowAllAccess.Click

    '    RadGrid1.Visible = True
    '    GridView1.Visible = False
    'End Sub




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'GridView1.Visible = False
            'RadGrid1.Visible = False
            'spWeb_GetEmployeeUserAccess_All
            ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "spWeb_GetEmployeeUserAccess_All")

        End If

    End Sub


End Class
