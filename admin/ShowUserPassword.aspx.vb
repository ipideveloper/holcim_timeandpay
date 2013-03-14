
Partial Class admin_ShowUserPassword
    Inherits System.Web.UI.Page

    Protected Sub RetrieveButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RetrieveButton.Click
        Web_Password.Text = "Password"

    End Sub

    Protected Sub RetrieveButton_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RetrieveButton.PreRender
        RetrieveButton.Style("padding-left") = "20px"
        RetrieveButton.Style("padding-right") = "20px"

    End Sub

    Protected Sub Web_Password_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Web_Password.PreRender
        Web_Password.Style("color") = "#FFFFFF"
        Web_Password.Enabled = False


    End Sub
End Class
