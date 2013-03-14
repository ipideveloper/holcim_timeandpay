Imports System.Drawing
Imports Telerik.Web.UI
Imports Telerik.Web

Partial Class admin_TravelOrderData
    Inherits System.Web.UI.Page
    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If (TypeOf (e.Item) Is GridDataItem) Then
            Dim dataBoundItem As GridDataItem = e.Item
            'If dataBoundItem("LogType").Text = "Error" Then
            '    dataBoundItem.ForeColor = Color.Red

            '    dataBoundItem.BackColor = Color.LightGray



            'End If
        End If
    End Sub


    Protected Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs)
        If e.CommandName = "FilterRadGrid" Then
            RadFilter1.FireApplyCommand()
        End If
    End Sub


    Protected Sub RadToolBar1_ButtonClick(ByVal source As Object, ByVal e As RadToolBarEventArgs)

    End Sub

    Protected Function GetFilterIcon() As String
        Return SkinRegistrar.GetWebResourceUrl(Page, GetType(RadGrid), String.Format("Telerik.Web.UI.Skins.{0}.Grid.Filter.gif", "Transparent"))
    End Function
End Class
