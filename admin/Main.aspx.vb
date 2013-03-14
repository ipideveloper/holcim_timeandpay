
Partial Class admin_Main
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Label1.Text = Session("User") & Session("RoleID")


        If Session("RoleID") = 1 Then
            Panel_ConcurModule.Visible = True
            Panel_SystemAdmin.Visible = True
        End If

        If Session("RoleID") = 2 Then
            Panel_ConcurModule.Visible = True
            Panel_SystemAdmin.Visible = False
        End If

    End Sub
End Class
