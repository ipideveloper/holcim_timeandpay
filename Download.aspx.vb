
Partial Class Download
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim FileToDownload As String = "~/MyCSVFolder/Exported/" + Session("DownloadExcel")
        HyperLink1.Text = "Download " + Session("DownloadExcel")
        HyperLink1.NavigateUrl = FileToDownload


    End Sub
End Class
