Imports HolcimDbClass
Imports System.Data
Imports System.Data.SqlClient

Partial Class emailredirect
    Inherits System.Web.UI.Page


    Function GetSectorbyEmail(ByVal email As String) As String
        Dim ds As New DataSet

        Dim sqlParam(0) As SqlParameter
        sqlParam(0) = New SqlParameter("@email", SqlDbType.VarChar, 100)
        sqlParam(0).Value = email

        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Get_SectorByEmail", sqlParam)
        Return ds.Tables(0).Rows(0)("sector")

    End Function

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load

        Dim urllink As String = ""

        Select Case GetSectorbyEmail(Request.QueryString("email")).ToString
            Case "BULACAN"
                urllink = "http://phl-pbl0-s026/holcim_app/"
            Case "LA UNION"
                urllink = "http://phl-pln0-s022/holcim_app/"
            Case "DAVAO"
                urllink = "http://phl-pdv0-s028/holcim_app/"
            Case "LUGAIT"
                urllink = "http://phl-plg0-s020/holcim_app/"
            Case "HEAD OFFICE"
                urllink = "http://phl-p100-s095/holcim_app/"

        End Select

        Response.Redirect(urllink)
        'Label1.Text = urllink.ToString

    End Sub
End Class
