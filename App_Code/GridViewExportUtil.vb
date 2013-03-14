Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.IO
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls



Public Class GridViewExportUtil


    Public Shared Sub Export(ByVal fileName As String, ByVal gv As GridView, ByVal Title1 As String, ByVal Title2 As String)
        'MsgBox(gv.RowStyle.BackColor.ToString)
        'MsgBox(gv.RowStyle.ForeColor.ToString)

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))
        HttpContext.Current.Response.ContentType = "application/ms-excel"
        'HttpContext.Current.Response.ContentType = "application/vnd.sun.xml.calc"
        'HttpContext.Current.Response.ContentType = "application/vnd.oasis.opendocument.spreadsheet"

        Dim sw As StringWriter = New StringWriter
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)

        '  Create a form to contain the grid
        Dim Form As HtmlForm = New HtmlForm

        Dim table As Table = New Table
        table.GridLines = gv.GridLines

        '  add the header row to the table

        If (Not (gv.HeaderRow) Is Nothing) Then
            GridViewExportUtil.PrepareControlForExport(gv.HeaderRow)
            table.Rows.Add(gv.HeaderRow)
            'table.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#0067a2")
            'table.Rows(0).ForeColor = System.Drawing.Color.White
            table.Rows(0).BackColor = gv.HeaderStyle.BackColor
            table.Rows(0).ForeColor = gv.HeaderStyle.ForeColor
            table.Rows(0).BorderWidth = gv.HeaderStyle.BorderWidth
        End If
        '  add each of the data rows to the table
        For Each row As GridViewRow In gv.Rows
            GridViewExportUtil.PrepareControlForExport(row)
            table.Rows.Add(row)
            table.Rows(0).BackColor = gv.RowStyle.BackColor
            table.Rows(0).ForeColor = gv.RowStyle.ForeColor
            table.Rows(0).BorderWidth = gv.RowStyle.BorderWidth
        Next
        '  add the footer row to the table
        If (Not (gv.FooterRow) Is Nothing) Then
            GridViewExportUtil.PrepareControlForExport(gv.FooterRow)
            table.Rows.Add(gv.FooterRow)
        End If
        '---------------------------------------

        ' Add the Header Title for the exported excel
        Dim lblHeader As Label = New Label()
        lblHeader.ID = "lblHeader"
        lblHeader.Text = Title1
        lblHeader.Font.Size = FontUnit.Large

        ' Go to next line
        Dim br As HtmlGenericControl = New HtmlGenericControl("BR")

        ' Add the created date to the excel
        Dim lblDateTimeStamp As Label = New Label()
        lblDateTimeStamp.ID = "lblDateTimeStamp"
        lblDateTimeStamp.Text = Title2
        lblDateTimeStamp.Font.Size = FontUnit.Medium

        ' Add all the controls to the form

        Form.Controls.Add(lblHeader)
        Form.Controls.Add(br)
        Form.Controls.Add(lblDateTimeStamp)
        Form.Controls.Add(table)
        Dim CurrentPage As Page = New Page
        CurrentPage.Controls.Add(Form)
        '  Render the table into the HtmlTextWriter
        Form.RenderControl(htw)

        '--------------------------------------

        '  render the table into the htmlwriter

        'table.RenderControl(htw)


        '  render the htmlwriter into the response
        HttpContext.Current.Response.Write(sw.ToString)
        HttpContext.Current.Response.End()
    End Sub

    Public Shared Sub ExportCalc(ByVal fileName As String, ByVal gv As GridView, ByVal Title1 As String, ByVal Title2 As String)
        'MsgBox(gv.RowStyle.BackColor.ToString)
        'MsgBox(gv.RowStyle.ForeColor.ToString)

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))
        'HttpContext.Current.Response.ContentType = "application/ms-excel"
        'HttpContext.Current.Response.ContentType = "application/vnd.sun.xml.calc"
        HttpContext.Current.Response.ContentType = "application/vnd.oasis.opendocument.spreadsheet"

        Dim sw As StringWriter = New StringWriter
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)

        '  Create a form to contain the grid
        Dim Form As HtmlForm = New HtmlForm

        Dim table As Table = New Table
        table.GridLines = gv.GridLines

        '  add the header row to the table
        If (Not (gv.HeaderRow) Is Nothing) Then
            GridViewExportUtil.PrepareControlForExport(gv.HeaderRow)
            table.Rows.Add(gv.HeaderRow)
            'table.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#0067a2")
            'table.Rows(0).ForeColor = System.Drawing.Color.White
            table.Rows(0).BackColor = gv.HeaderStyle.BackColor
            table.Rows(0).ForeColor = gv.HeaderStyle.ForeColor
            table.Rows(0).BorderWidth = gv.HeaderStyle.BorderWidth
        End If
        '  add each of the data rows to the table
        For Each row As GridViewRow In gv.Rows
            GridViewExportUtil.PrepareControlForExport(row)
            table.Rows.Add(row)
            table.Rows(0).BackColor = gv.RowStyle.BackColor
            table.Rows(0).ForeColor = gv.RowStyle.ForeColor
            table.Rows(0).BorderWidth = gv.RowStyle.BorderWidth
        Next
        '  add the footer row to the table
        If (Not (gv.FooterRow) Is Nothing) Then
            GridViewExportUtil.PrepareControlForExport(gv.FooterRow)
            table.Rows.Add(gv.FooterRow)
        End If
        '---------------------------------------

        ' Add the Header Title for the exported excel
        Dim lblHeader As Label = New Label()
        lblHeader.ID = "lblHeader"
        lblHeader.Text = Title1
        lblHeader.Font.Size = FontUnit.Large

        ' Go to next line
        Dim br As HtmlGenericControl = New HtmlGenericControl("BR")

        ' Add the created date to the excel
        Dim lblDateTimeStamp As Label = New Label()
        lblDateTimeStamp.ID = "lblDateTimeStamp"
        lblDateTimeStamp.Text = Title2
        lblDateTimeStamp.Font.Size = FontUnit.Medium

        ' Add all the controls to the form
        Form.Controls.Add(lblHeader)
        Form.Controls.Add(br)
        Form.Controls.Add(lblDateTimeStamp)
        Form.Controls.Add(table)
        Dim CurrentPage As Page = New Page
        CurrentPage.Controls.Add(Form)
        '  Render the table into the HtmlTextWriter
        Form.RenderControl(htw)

        '--------------------------------------

        '  render the table into the htmlwriter

        'table.RenderControl(htw)


        '  render the htmlwriter into the response
        HttpContext.Current.Response.Write(sw.ToString)
        HttpContext.Current.Response.End()
    End Sub

    Public Shared Sub ExportExcel(ByVal fileName As String, ByVal gv As GridView, ByVal Title1 As String, ByVal Title2 As String)
        'MsgBox(gv.RowStyle.BackColor.ToString)
        'MsgBox(gv.RowStyle.ForeColor.ToString)

        HttpContext.Current.Response.Clear()
        HttpContext.Current.Response.AddHeader("content-disposition", String.Format("attachment; filename={0}", fileName))
        HttpContext.Current.Response.ContentType = "application/ms-excel"
        'HttpContext.Current.Response.ContentType = "application/vnd.sun.xml.calc"
        'HttpContext.Current.Response.ContentType = "application/vnd.oasis.opendocument.spreadsheet"

        Dim sw As StringWriter = New StringWriter
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)

        '  Create a form to contain the grid
        Dim Form As HtmlForm = New HtmlForm

        Dim table As Table = New Table
        table.GridLines = gv.GridLines

        '  add the header row to the table
        If (Not (gv.HeaderRow) Is Nothing) Then
            GridViewExportUtil.PrepareControlForExport(gv.HeaderRow)
            table.Rows.Add(gv.HeaderRow)
            'table.Rows(0).BackColor = System.Drawing.ColorTranslator.FromHtml("#0067a2")
            'table.Rows(0).ForeColor = System.Drawing.Color.White
            table.Rows(0).BackColor = gv.HeaderStyle.BackColor
            table.Rows(0).ForeColor = gv.HeaderStyle.ForeColor
            table.Rows(0).BorderWidth = gv.HeaderStyle.BorderWidth
        End If
        '  add each of the data rows to the table
        For Each row As GridViewRow In gv.Rows
            GridViewExportUtil.PrepareControlForExport(row)
            table.Rows.Add(row)
            table.Rows(0).BackColor = gv.RowStyle.BackColor
            table.Rows(0).ForeColor = gv.RowStyle.ForeColor
            table.Rows(0).BorderWidth = gv.RowStyle.BorderWidth
        Next
        '  add the footer row to the table
        If (Not (gv.FooterRow) Is Nothing) Then
            GridViewExportUtil.PrepareControlForExport(gv.FooterRow)
            table.Rows.Add(gv.FooterRow)
        End If
        '---------------------------------------

        ' Add the Header Title for the exported excel
        Dim lblHeader As Label = New Label()
        lblHeader.ID = "lblHeader"
        lblHeader.Text = Title1
        lblHeader.Font.Size = FontUnit.Large

        ' Go to next line
        Dim br As HtmlGenericControl = New HtmlGenericControl("BR")

        ' Add the created date to the excel
        Dim lblDateTimeStamp As Label = New Label()
        lblDateTimeStamp.ID = "lblDateTimeStamp"
        lblDateTimeStamp.Text = Title2
        lblDateTimeStamp.Font.Size = FontUnit.Medium

        ' Add all the controls to the form
        Form.Controls.Add(lblHeader)
        Form.Controls.Add(br)
        Form.Controls.Add(lblDateTimeStamp)
        Form.Controls.Add(table)
        Dim CurrentPage As Page = New Page
        CurrentPage.Controls.Add(Form)
        '  Render the table into the HtmlTextWriter
        Form.RenderControl(htw)

        '--------------------------------------

        '  render the table into the htmlwriter

        'table.RenderControl(htw)


        '  render the htmlwriter into the response
        HttpContext.Current.Response.Write(sw.ToString)
        HttpContext.Current.Response.End()
    End Sub

    ' Replace any of the contained controls with literals
    Private Shared Sub PrepareControlForExport(ByVal control As Control)
        Dim i As Integer = 0
        Do While (i < control.Controls.Count)
            Dim current As Control = control.Controls(i)
            If (TypeOf current Is LinkButton) Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(CType(current, LinkButton).Text))
            ElseIf (TypeOf current Is ImageButton) Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(CType(current, ImageButton).AlternateText))
            ElseIf (TypeOf current Is HyperLink) Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(CType(current, HyperLink).Text))
            ElseIf (TypeOf current Is DropDownList) Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(CType(current, DropDownList).SelectedItem.Text))
            ElseIf (TypeOf current Is CheckBox) Then
                control.Controls.Remove(current)
                control.Controls.AddAt(i, New LiteralControl(CType(current, CheckBox).Checked))
                'TODO: Warning!!!, inline IF is not supported ?
            End If
            If current.HasControls Then
                GridViewExportUtil.PrepareControlForExport(current)
            End If
            i = (i + 1)
        Loop
    End Sub

End Class
