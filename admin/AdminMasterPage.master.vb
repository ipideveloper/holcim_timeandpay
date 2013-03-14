
Partial Class admin_AdminMasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("User") = "" Then
            Session.RemoveAll()
            Response.Redirect("Default.aspx?page=expired")
        End If

        If Not Page.IsPostBack Then
            Label_Username.Style("color") = "#F4F4F4"
            Label_Username.Style("text-decoration") = "underline"
            Label_Username.Text = Session("User").ToString
        End If
    End Sub
End Class

