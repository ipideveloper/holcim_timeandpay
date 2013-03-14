Imports System.Object
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports VB = Microsoft.VisualBasic
Imports System.Data.OleDb
Imports Cls_UserProperty

Partial Class UploadShiftExport
    Inherits System.Web.UI.Page
    Public MM, DD, YY As String
    Dim FileInfo As FileInfo
    Public sFileName As String
    Public sFilePath As String
    Public MyFile As String
    Public sFileDir As String
    Dim xlapp As Object
    Dim Books As Object     'Excel.Workbooks
    Dim xlBook As Object    'Excel.Workbook
    Dim Sheets As Object    'Excel.Sheets
    Dim xlSheet As Object   'Excel.Worksheet
    Dim myCommand As New SqlCommand
    Public MyConn As New SqlConnection

    Dim MyFilename As String
    Dim Filename As String
    Dim FilePath As String
    Private ws As New localhost.Service
    Private cls_email As New cls_Email_Notifications
    Public ReadOnly Property Current_User() As Cls_UserProperty
        Get
            Return New Cls_UserProperty(ViewState("Employee_ID"))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.IsNewSession Then
            UserMsgBox("Session Expired!")
            Response.Redirect("Default.aspx")
        End If
        Try

        
            If Not IsPostBack() Then
                ViewState("Employee_ID") = Session("Employee_ID")
                GridView4.DataSource = ws.Get_Shift_Details_Temp(Session("Employee_ID"))
                GridView4.DataBind()


                If GridView4.Rows.Count > 0 Then

                    Dim varDate As DateTime = Trim(Str(Session("Month"))) + "/" + "01/" + Trim(Str(Session("Year")))
                    Dim varday As String

                    Do While Month(varDate) = Session("Month")
                        varday = varDate.ToString("dd-MMM-yyyy") + "  " + Mid(varDate.DayOfWeek.ToString, 1, 3)
                        GridView4.HeaderRow.Cells(Day(varDate) + 1).Text = varday
                        GridView4.Columns(Day(varDate) + 1).ItemStyle.Width = Unit.Pixel(400)
                        varDate = DateAdd(DateInterval.Day, 1, varDate)
                    Loop

                    For i As Integer = 0 To GridView4.Rows.Count - 1
                        varDate = Trim(Str(Session("Month"))) + "/" + "01/" + Trim(Str(Session("Year")))
                        Do While Month(varDate) = Session("Month")

                            If shift_changed(i, Day(varDate)) Then
                                GridView4.Rows(i).Cells(Day(varDate) + 1).BackColor = Drawing.Color.Orange 'Original Code
                            End If

                            varDate = DateAdd(DateInterval.Day, 1, varDate)
                        Loop

                        'GridView4.Rows(i).Cells(10).BackColor = Drawing.Color.Orange
                        'GridView4.Rows(4).Cells(10).BackColor = Drawing.Color.Orange
                    Next


                    GridView9.DataSource = ws.Get_Shift_Details_Temp(Session("Employee_ID"))
                    GridView9.DataBind()

                    btn_ExportToExcel.Visible = True
                Else
                    'btn_ExportToExcel.Visible = False

                End If

            End If
        Catch ex As Exception

        End Try
    End Sub
    Function shift_changed(ByVal gvrows_count As Integer, ByVal count As String) As Boolean
        Try

            Dim row As GridViewRow = GridView4.Rows(gvrows_count)

            Dim EmpID As String = GridView4.Rows(gvrows_count).Cells(1).Text
            Dim workdate As Date = Trim(Str(Session("Month"))) + "/" + Trim(count) + "/" + Trim(Str(Session("Year")))
            Dim shiftchange As New DataSet
            Dim pendingchecker As Integer 'Added by Andrew 11-18-2011 
            shiftchange = Nothing
            shiftchange = ws.Get_Shift_History(EmpID, workdate)
            If shiftchange.Tables(0).Rows.Count > 0 Then
                pendingchecker = shiftchange.Tables(0).Rows(0)(8) 'Added by Andrew 11-18-2011
                If pendingchecker = 1 Then                        'Added by Andrew 11-18-2011
                    Return True                                   'Added by Andrew 11-18-2011
                Else                                              'Added by Andrew 11-18-2011
                    Return False                                  'Added by Andrew 11-18-2011
                End If                                            'Added by Andrew 11-18-2011
            Else
                Return False
            End If
        Catch ex As Exception

        End Try
    End Function

    Protected Sub GridView4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView4.RowDataBound
        Try
            e.Row.Cells(0).CssClass = "locked"
            e.Row.Cells(1).CssClass = "locked"
        Catch ex As Exception

        End Try


    End Sub

    Protected Sub btn_ExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ExportToExcel.Click

        Dim FileToOpen As String = Server.MapPath("MyCSVFolder\Exported") + "\" + Session("Employee_ID") + "-" + Trim(Session("Month")) + Trim(Session("Year")) + ".xls"

        Try

            xlapp = Microsoft.VisualBasic.Interaction.CreateObject("Excel.Application")
            Books = xlapp.Workbooks

            'Dim FileToOpen As String = "C:\inetpub\wwwroot\holcim_app_shift\MyCSVFolder\Exported\" + Current_User.Employee_ID + "-" + Trim(DDL_Month_SS.SelectedValue.ToString) + Trim(DDL_Year_SS.SelectedValue) + ".xls"
            'Dim FileToOpen As String = Server.MapPath("MyCSVFolder\Exported") + "\" + Session("Employee_ID") + "-" + Trim(Session("Month")) + Trim(Session("Year")) + ".xls"
            'MsgBox(FileToOpen) 'This is for testing only by Andrew 11-2-2011
            'MsgBox(Session("Employee_ID")) 'This is for testing only by Andrew 11-3-2011

            Books.Add()
            'Books.open(FileToOpen)

            xlBook = Books.Item(1)      'get Book reference
            Sheets = xlBook.Worksheets  'get Sheets reference

            'Label5.Text = MyFile
            xlapp.DisplayAlerts = False    'This will prevent any message prompts from Excel (IE.."Do you want to save before closing?")
            xlapp.Visible = False    'We don't want the app visible while we are populating it.
            xlapp.cutcopymode = False
            'xlSheet = Sheets.item("Shift Sched Batch Upload (SSBU)")   'get Sheet reference
            xlSheet = Sheets.item("Sheet1")

            Dim varDate As DateTime = Trim(Str(Session("Month"))) + "/" + "01/" + Trim(Str(Session("Year")))
            Dim varDay As String

            xlSheet.cells(1, 1).value() = "Employee Name" 'eto ung check 11-4-2011
            xlSheet.cells(1, 2).value() = "Personnel Number"

            Dim colCtr As Integer = 3
            Do While Month(varDate) = Session("Month")

                varDay = Day(varDate).ToString + " - " + Mid(varDate.DayOfWeek.ToString, 1, 3)
                xlSheet.cells(1, colCtr).value() = varDate.ToString("dd-MMM-yyyy")
                xlSheet.cells(1 + 1, colCtr).value() = Mid(varDate.DayOfWeek.ToString, 1, 3)

                colCtr = colCtr + 1
                varDate = DateAdd(DateInterval.Day, 1, varDate)
            Loop

            Dim varString As String

            Dim rowCtr As Integer = 3
            Dim ShiftCode As String
            Dim varCell As Excel.Range
            Dim varFormula As String
            Dim ds As DataSet


            For i As Integer = 0 To GridView9.Rows.Count - 1

                ds = ws.Get_Available_Shift(GridView9.Rows(i).Cells(1).Text)
                For xi As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = ds.Tables(0).Rows(xi)
                    With dr
                        xlSheet.cells(rowCtr, 108 + xi).value() = ws.Shift_Upload_Equivalent(dr("shift_code"), Current_User.Branch_id, 2)
                    End With
                Next


                'For xy As Integer = 0 To GridView9.Columns.Count - 1
                'xlSheet.cells(rowCtr, 108).value() = "ADM"
                'xlSheet.cells(rowCtr, 109).value() = "1"
                'xlSheet.cells(rowCtr, 110).value() = "2"
                'xlSheet.cells(rowCtr, 111).value() = "3"
                'xlSheet.cells(rowCtr, 112).value() = "4"
                'xlSheet.cells(rowCtr, 113).value() = "OFF"


                For xy As Integer = 0 To Day(DateAdd(DateInterval.Day, -1, varDate)) + 1
                    ShiftCode = GridView9.Rows(i).Cells(xy).Text
                    If Not IsDBNull(ws.Shift_Upload_Equivalent(ShiftCode, Current_User.Branch_id, 2)) Then
                        ShiftCode = ws.Shift_Upload_Equivalent(ShiftCode, Current_User.Branch_id, 2)
                    End If
                    If ShiftCode <> "" Then

                        varCell = CType(xlSheet.range(getCell(rowCtr, xy + 1)), Excel.Range)

                        varFormula = "=$DD$" & Trim(rowCtr.ToString) & ":$EZ$" & Trim(rowCtr.ToString)
                        With varCell.Validation
                            .Add(Excel.XlDVType.xlValidateList, AlertStyle:=Excel.XlDVAlertStyle.xlValidAlertStop, Operator:=Excel.XlFormatConditionOperator.xlBetween, Formula1:=varFormula)
                            .IgnoreBlank = True
                            .InCellDropdown = True
                            .InputTitle = ""
                            .ErrorTitle = ""
                            .InputMessage = ""
                            .ErrorMessage = "Invalid Value"
                            .ShowInput = True
                            .ShowInput = True


                        End With
                        xlSheet.cells(rowCtr, xy + 1).value() = ShiftCode
                    Else
                        varString = GridView9.Rows(i).Cells(xy).Text
                        varString = varString.Replace("&#241;", Chr(241))
                        xlSheet.cells(rowCtr, xy + 1).value() = varString
                    End If
                Next
                rowCtr = rowCtr + 1
            Next
            Dim RangeToAutofit As String = "A1:AG" & Trim(rowCtr.ToString)
            Dim xlRange As Excel.Range = CType(xlSheet.range(RangeToAutofit), Excel.Range)
            CType(xlSheet.range(RangeToAutofit), Excel.Range).EntireColumn.AutoFit()


            Dim RangeToProtect As String = "A1:B" & Trim((rowCtr + 10000).ToString)


            'xlRange.Locked = False
            'xlSheet.cells.select()

            'CType(xlapp.selection, Excel.Range).Locked = False
            'CType(xlSheet.range(RangeToProtect), Excel.Range).Select()
            'CType(xlapp.selection, Excel.Range).Locked = True
            'xlSheet.protect()

            rowCtr = rowCtr - 1
            Select Case Day(DateAdd(DateInterval.Day, -1, varDate))
                Case Is = 31
                    RangeToProtect = "C3:AG" & Trim(rowCtr.ToString)
                Case Is = 30
                    RangeToProtect = "C3:AF" & Trim(rowCtr.ToString)
                Case Is = 29
                    RangeToProtect = "C3:AE" & Trim(rowCtr.ToString)
                Case Is = 28
                    RangeToProtect = "C3:AD" & Trim(rowCtr.ToString)
            End Select


            'RangeToProtect = "C3:AG" & Trim(rowCtr.ToString)
            xlSheet.cells.select()
            CType(xlSheet.range(RangeToProtect), Excel.Range).Select()
            CType(xlapp.selection, Excel.Range).Locked = False
            xlSheet.protect(DrawingObjects:=True, Contents:=True, Scenarios:=True, Password:="Secret")
            xlSheet = Sheets.item("Sheet2")

            'xlBook.selectedsheet.delete()

            xlBook.protect(Structure:=True, Password:="Secret")

            If Not Directory.Exists(Server.MapPath("MyCSVFolder\Exported")) Then
                Directory.CreateDirectory(Server.MapPath("MyCSVFolder\Exported"))
            End If

            xlBook.SaveAs(FileToOpen)
            xlBook.close()
            xlapp.quit()

            'xlapp.visible = True
            Sheets = Nothing
            Books = Nothing

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlapp)
            xlapp = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            GC.Collect()


            UserMsgBox("Schedule has been successfully exported!")
            Dim ExcelFile As String = Session("Employee_ID") + "-" + Trim(Session("Month")) + Trim(Session("Year")) + ".xls"
            Session("DownloadExcel") = ExcelFile
            'OpenModalPage("download.aspx")
            'Response.Redirect("download.aspx")
            HyperLink1.Visible = True
            Dim FileToDownload As String = "~/MyCSVFolder/Exported/" + Session("DownloadExcel")
            HyperLink1.Text = "Download " + Session("DownloadExcel")
            HyperLink1.NavigateUrl = FileToDownload

        Catch ex As Exception
            'MsgBox("Andrew Error!")
            'ws.Shift_Upload_Error(Session("Employee_ID"), "Error Export to excel: " + ex.Message.ToString)-- Original Comment
            ws.Shift_Upload_Error(Session("Employee_ID"), "Error Export to excel: " + ex.Message.ToString)
            Label_Error_Message.Visible = True
            Label_Error_Message.Text = "Error Export to excel: " + ex.Message.ToString
            'Dim xsting As String = Label_Error_Message.Text = "Error Export to excel: " + ex.Message.ToString --Original Comment
            'UserMs'Box(xsting) -- Original Comment
            'MsgBox("Gago may error!", FileToOpen) --Original Comment
        End Try
    End Sub
    Private Function getCell(ByVal RowCtr As Integer, ByVal ColCtr As Integer) As String
        Dim varCell As String = ""
        Select Case ColCtr
            Case Is = 1
                varCell = "A" & Trim(RowCtr.ToString) & ":" & "A" & Trim(RowCtr.ToString)
            Case Is = 2
                varCell = "B" & Trim(RowCtr.ToString) & ":" & "B" & Trim(RowCtr.ToString)
            Case Is = 3
                varCell = "C" & Trim(RowCtr.ToString) & ":" & "C" & Trim(RowCtr.ToString)
            Case Is = 4
                varCell = "D" & Trim(RowCtr.ToString) & ":" & "D" & Trim(RowCtr.ToString)
            Case Is = 5
                varCell = "E" & Trim(RowCtr.ToString) & ":" & "E" & Trim(RowCtr.ToString)
            Case Is = 6
                varCell = "F" & Trim(RowCtr.ToString) & ":" & "F" & Trim(RowCtr.ToString)
            Case Is = 7
                varCell = "G" & Trim(RowCtr.ToString) & ":" & "G" & Trim(RowCtr.ToString)
            Case Is = 8
                varCell = "H" & Trim(RowCtr.ToString) & ":" & "H" & Trim(RowCtr.ToString)
            Case Is = 9
                varCell = "I" & Trim(RowCtr.ToString) & ":" & "I" & Trim(RowCtr.ToString)
            Case Is = 10
                varCell = "J" & Trim(RowCtr.ToString) & ":" & "J" & Trim(RowCtr.ToString)
            Case Is = 11
                varCell = "K" & Trim(RowCtr.ToString) & ":" & "K" & Trim(RowCtr.ToString)

            Case Is = 12
                varCell = "L" & Trim(RowCtr.ToString) & ":" & "L" & Trim(RowCtr.ToString)
            Case Is = 13
                varCell = "M" & Trim(RowCtr.ToString) & ":" & "M" & Trim(RowCtr.ToString)
            Case Is = 14
                varCell = "N" & Trim(RowCtr.ToString) & ":" & "N" & Trim(RowCtr.ToString)
            Case Is = 15
                varCell = "O" & Trim(RowCtr.ToString) & ":" & "O" & Trim(RowCtr.ToString)
            Case Is = 16
                varCell = "P" & Trim(RowCtr.ToString) & ":" & "P" & Trim(RowCtr.ToString)
            Case Is = 17
                varCell = "Q" & Trim(RowCtr.ToString) & ":" & "Q" & Trim(RowCtr.ToString)
            Case Is = 18
                varCell = "R" & Trim(RowCtr.ToString) & ":" & "R" & Trim(RowCtr.ToString)
            Case Is = 19
                varCell = "S" & Trim(RowCtr.ToString) & ":" & "S" & Trim(RowCtr.ToString)
            Case Is = 20
                varCell = "T" & Trim(RowCtr.ToString) & ":" & "T" & Trim(RowCtr.ToString)
            Case Is = 21
                varCell = "U" & Trim(RowCtr.ToString) & ":" & "U" & Trim(RowCtr.ToString)
            Case Is = 22
                varCell = "V" & Trim(RowCtr.ToString) & ":" & "V" & Trim(RowCtr.ToString)
            Case Is = 23
                varCell = "W" & Trim(RowCtr.ToString) & ":" & "W" & Trim(RowCtr.ToString)
            Case Is = 24
                varCell = "X" & Trim(RowCtr.ToString) & ":" & "X" & Trim(RowCtr.ToString)
            Case Is = 25
                varCell = "Y" & Trim(RowCtr.ToString) & ":" & "Y" & Trim(RowCtr.ToString)
            Case Is = 26
                varCell = "Z" & Trim(RowCtr.ToString) & ":" & "Z" & Trim(RowCtr.ToString)
            Case Is = 27
                varCell = "AA" & Trim(RowCtr.ToString) & ":" & "AA" & Trim(RowCtr.ToString)
            Case Is = 28
                varCell = "AB" & Trim(RowCtr.ToString) & ":" & "AB" & Trim(RowCtr.ToString)
            Case Is = 29
                varCell = "AC" & Trim(RowCtr.ToString) & ":" & "AC" & Trim(RowCtr.ToString)
            Case Is = 30
                varCell = "AD" & Trim(RowCtr.ToString) & ":" & "AD" & Trim(RowCtr.ToString)
            Case Is = 31
                varCell = "AE" & Trim(RowCtr.ToString) & ":" & "AE" & Trim(RowCtr.ToString)
            Case Is = 32
                varCell = "AF" & Trim(RowCtr.ToString) & ":" & "AF" & Trim(RowCtr.ToString)
            Case Is = 33
                varCell = "AG" & Trim(RowCtr.ToString) & ":" & "AG" & Trim(RowCtr.ToString)
            Case Is = 34
                varCell = "AH" & Trim(RowCtr.ToString) & ":" & "AH" & Trim(RowCtr.ToString)
            Case Is = 35
                varCell = "AI" & Trim(RowCtr.ToString) & ":" & "AI" & Trim(RowCtr.ToString)
            Case Is = 36
                varCell = "AJ" & Trim(RowCtr.ToString) & ":" & "AJ" & Trim(RowCtr.ToString)
            Case Is = 37
                varCell = "AK" & Trim(RowCtr.ToString) & ":" & "AK" & Trim(RowCtr.ToString)
        End Select
        Return varCell
    End Function
    Protected Sub GridView4_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView4.RowCommand

        Try
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            If e.CommandName = "cmd1" Then
                get_selected_row(index, "1")
            ElseIf e.CommandName = "cmd2" Then
                get_selected_row(index, "2")
            ElseIf e.CommandName = "cmd3" Then
                get_selected_row(index, "3")
            ElseIf e.CommandName = "cmd4" Then
                get_selected_row(index, "4")
            ElseIf e.CommandName = "cmd5" Then
                get_selected_row(index, "5")
            ElseIf e.CommandName = "cmd6" Then
                get_selected_row(index, "6")
            ElseIf e.CommandName = "cmd7" Then
                get_selected_row(index, "7")
            ElseIf e.CommandName = "cmd8" Then
                get_selected_row(index, "8")
            ElseIf e.CommandName = "cmd9" Then
                get_selected_row(index, "9")
            ElseIf e.CommandName = "cmd10" Then
                get_selected_row(index, "10")
            ElseIf e.CommandName = "cmd11" Then
                get_selected_row(index, "11")
            ElseIf e.CommandName = "cmd12" Then
                get_selected_row(index, "12")
            ElseIf e.CommandName = "cmd13" Then
                get_selected_row(index, "13")
            ElseIf e.CommandName = "cmd14" Then
                get_selected_row(index, "14")
            ElseIf e.CommandName = "cmd15" Then
                get_selected_row(index, "15")
            ElseIf e.CommandName = "cmd16" Then
                get_selected_row(index, "16")
            ElseIf e.CommandName = "cmd17" Then
                get_selected_row(index, "17")
            ElseIf e.CommandName = "cmd18" Then
                get_selected_row(index, "18")
            ElseIf e.CommandName = "cmd19" Then
                get_selected_row(index, "19")
            ElseIf e.CommandName = "cmd20" Then
                get_selected_row(index, "20")
            ElseIf e.CommandName = "cmd21" Then
                get_selected_row(index, "21")
            ElseIf e.CommandName = "cmd22" Then
                get_selected_row(index, "22")
            ElseIf e.CommandName = "cmd23" Then
                get_selected_row(index, "23")
            ElseIf e.CommandName = "cmd24" Then
                get_selected_row(index, "24")
            ElseIf e.CommandName = "cmd25" Then
                get_selected_row(index, "25")
            ElseIf e.CommandName = "cmd26" Then
                get_selected_row(index, "26")
            ElseIf e.CommandName = "cmd27" Then
                get_selected_row(index, "27")
            ElseIf e.CommandName = "cmd28" Then
                get_selected_row(index, "28")
            ElseIf e.CommandName = "cmd29" Then
                get_selected_row(index, "29")
            ElseIf e.CommandName = "cmd30" Then
                get_selected_row(index, "30")
            ElseIf e.CommandName = "cmd31" Then
                get_selected_row(index, "31")
            End If

            
        Catch ex As Exception

        End Try
    End Sub
    Private Sub get_selected_row(ByVal gvrows_count As Integer, ByVal count As String)
        Try

        
            Dim row As GridViewRow = GridView4.Rows(gvrows_count)
            Dim lnk_ref As LinkButton = CType(row.FindControl("lnk_" & count), LinkButton)
            Dim EmpID As String = GridView4.Rows(gvrows_count).Cells(1).Text
            Dim workdate As Date = Trim(Str(Session("Month"))) + "/" + Trim(count) + "/" + Trim(Str(Session("Year")))


            Session("EmpID") = EmpID
            Session("workdate") = workdate

            GridView5.Dispose()
            GridView5.DataSource = ws.Get_Shift_History(EmpID, workdate)
            GridView5.DataBind()

            If GridView5.Rows.Count = 0 Then
                UserMsgBox("No changes made on the selected shift.")
            Else
                ClientScript.RegisterStartupScript(Me.GetType, "lolo", "<script language=JavaScript>window.open('UploadShiftExportDetails.aspx','EmailPopup2','fullscreen=no,status=no,scrollbars=no,address=no,resizable=no,height=250,width=825,left=1,top=410')</script>")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Protected Sub GridView4_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView4.RowCreated
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then

                Dim lnk_1 As LinkButton = CType(e.Row.FindControl("lnk_1"), LinkButton)
                lnk_1.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_2 As LinkButton = CType(e.Row.FindControl("lnk_2"), LinkButton)
                lnk_2.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_3 As LinkButton = CType(e.Row.FindControl("lnk_3"), LinkButton)
                lnk_3.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_4 As LinkButton = CType(e.Row.FindControl("lnk_4"), LinkButton)
                lnk_4.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_5 As LinkButton = CType(e.Row.FindControl("lnk_5"), LinkButton)
                lnk_5.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_6 As LinkButton = CType(e.Row.FindControl("lnk_6"), LinkButton)
                lnk_6.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_7 As LinkButton = CType(e.Row.FindControl("lnk_7"), LinkButton)
                lnk_7.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_8 As LinkButton = CType(e.Row.FindControl("lnk_8"), LinkButton)
                lnk_8.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_9 As LinkButton = CType(e.Row.FindControl("lnk_9"), LinkButton)
                lnk_9.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_10 As LinkButton = CType(e.Row.FindControl("lnk_10"), LinkButton)
                lnk_10.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_11 As LinkButton = CType(e.Row.FindControl("lnk_11"), LinkButton)
                lnk_11.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_12 As LinkButton = CType(e.Row.FindControl("lnk_12"), LinkButton)
                lnk_12.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_13 As LinkButton = CType(e.Row.FindControl("lnk_13"), LinkButton)
                lnk_13.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_14 As LinkButton = CType(e.Row.FindControl("lnk_14"), LinkButton)
                lnk_14.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_15 As LinkButton = CType(e.Row.FindControl("lnk_15"), LinkButton)
                lnk_15.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_16 As LinkButton = CType(e.Row.FindControl("lnk_16"), LinkButton)
                lnk_16.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_17 As LinkButton = CType(e.Row.FindControl("lnk_17"), LinkButton)
                lnk_17.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_18 As LinkButton = CType(e.Row.FindControl("lnk_18"), LinkButton)
                lnk_18.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_19 As LinkButton = CType(e.Row.FindControl("lnk_19"), LinkButton)
                lnk_19.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_20 As LinkButton = CType(e.Row.FindControl("lnk_20"), LinkButton)
                lnk_20.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_21 As LinkButton = CType(e.Row.FindControl("lnk_21"), LinkButton)
                lnk_21.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_22 As LinkButton = CType(e.Row.FindControl("lnk_22"), LinkButton)
                lnk_22.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_23 As LinkButton = CType(e.Row.FindControl("lnk_23"), LinkButton)
                lnk_23.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_24 As LinkButton = CType(e.Row.FindControl("lnk_24"), LinkButton)
                lnk_24.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_25 As LinkButton = CType(e.Row.FindControl("lnk_25"), LinkButton)
                lnk_25.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_26 As LinkButton = CType(e.Row.FindControl("lnk_26"), LinkButton)
                lnk_26.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_27 As LinkButton = CType(e.Row.FindControl("lnk_27"), LinkButton)
                lnk_27.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_28 As LinkButton = CType(e.Row.FindControl("lnk_28"), LinkButton)
                lnk_28.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_29 As LinkButton = CType(e.Row.FindControl("lnk_29"), LinkButton)
                lnk_29.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_30 As LinkButton = CType(e.Row.FindControl("lnk_30"), LinkButton)
                lnk_30.CommandArgument = e.Row.RowIndex.ToString

                Dim lnk_31 As LinkButton = CType(e.Row.FindControl("lnk_31"), LinkButton)
                lnk_31.CommandArgument = e.Row.RowIndex.ToString
            End If
        Catch ex As Exception

        End Try
    End Sub
#Region "Javascripts"

    Private Sub UserMsgBox(ByVal sMsg As String)
        Dim sb As New StringBuilder
        Dim oFormObject As New System.Web.UI.Control
        sMsg = sMsg.Replace("'", "\'")
        sMsg = sMsg.Replace(Chr(34), "\" & Chr(34))
        sMsg = sMsg.Replace(vbCrLf, "\n")
        sMsg = "<script language=javascript>alert(""" & sMsg & """)</script>"
        sb = New StringBuilder
        sb.Append(sMsg)
        For Each oFormObject In Me.Controls
            If TypeOf oFormObject Is HtmlForm Then
                Exit For
            End If
        Next
        oFormObject.Controls.AddAt(oFormObject.Controls.Count, New LiteralControl(sb.ToString()))
    End Sub


    Private Sub OpenModalPage(ByVal PageName As String, ByVal vheight As Integer, ByVal vwidth As Integer)

        Dim strScript As String = "<script language=JavaScript>"

        strScript &= "self.open('" & PageName & "','EmailPopup','fullscreen=no,status=no,scrollbars=no,address=no,resizable=no,height=" & vheight & ",width=" & vwidth & ",left=1,top=1')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If

    End Sub

    
#End Region
End Class