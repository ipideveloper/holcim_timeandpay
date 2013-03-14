
Partial Class admin_Logout
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        'Session.RemoveAll()
        Session("User") = Nothing
        Session("RoleID") = Nothing
        Session.Remove("User")
        Session.Remove("RoleID")

        Response.Redirect("Default.aspx")
    End Sub
End Class
