Imports cls_GlobalFunction
Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient

Partial Class admin_MaintenanceOBType
    Inherits System.Web.UI.Page

    Protected Shared Sub InsertOBType(ByVal ID As Integer, ByVal ob_type As String)

        Dim ds As New DataSet
        Dim sqlParam(1) As SqlParameter

        sqlParam(0) = New SqlParameter("@ID", SqlDbType.Int)
        sqlParam(0).Value = ID

        sqlParam(1) = New SqlParameter("@ob_type", SqlDbType.VarChar, 50)
        sqlParam(1).Value = ob_type

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Insert_OBType", sqlParam)
    End Sub

    Protected Sub Button_AddOBType_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button_AddOBType.Click
        Dim ID As Integer = CInt(ob_type_ID.Text)
        Dim obtype As String = ob_type.Text

        Try
            InsertSystemLogs("Maintenance", "Add OB Type", 1, "Add New OB Type '" & ID.ToString & "," & obtype & "'", Session("User"))
            InsertOBType(ID,obtype)

        Catch ex As Exception
            InsertSystemLogs("Maintenance", "Add OB Type", 2, ex.Message.ToString(), Session("User"))
        End Try

        Response.Redirect("MaintenanceOBType.aspx")
    End Sub
End Class
