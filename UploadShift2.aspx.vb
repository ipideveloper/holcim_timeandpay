Imports System.Object
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports VB = Microsoft.VisualBasic
Imports System.Data.OleDb

Partial Class Default2
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

#Region "Properties"

    Public ReadOnly Property Current_User() As Cls_UserProperty
        Get
            Return New Cls_UserProperty(ViewState("Employee_ID"))
        End Get
    End Property

#End Region

#Region "Variables"

    Private ws As New localhost.Service
    Private cls_email As New cls_Email_Notifications

#End Region


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Request.IsAuthenticated Then
        '    Response.Redirect("Default.aspx")
        'End If
        If Session.IsNewSession Then
            UserMsgBox("Session Expired!")
            Response.Redirect("Default.aspx")
        End If

        'TabContainer1.ActiveTabIndex = Session("myIndex")

        If Not IsPostBack() Then
            Initialize()
            For i As Integer = Year(Now()) - 1 To Year(Now()) + 1
                DDL_Year_SS.Items.Add(i)
                DDL_Year_Update.Items.Add(i)
            Next

            

            DDL_Year_SS.SelectedValue = Year(Now())
            DDL_Year_Update.SelectedValue = Year(Now())
            DDL_Month_SS.SelectedValue = Month(Now())
            DDL_Month_Update.SelectedValue = Month(Now())
            FileUpload1.Visible = True
            TabContainer1.ActiveTabIndex = 0
            Session("myIndex") = 0
        End If



    End Sub

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        GridView6.Dispose()
        GridView6.DataSource = ws.Get_Shift_Approval(Current_User.Employee_ID)
        GridView6.DataBind()
        get_approvers()
    End Sub

    Private Sub get_approvers()

        For i As Integer = 0 To GridView6.Rows.Count - 1
            Dim row As GridViewRow = GridView6.Rows(i)
            Dim gv_approvers_shift As GridView = CType(row.FindControl("gv_approvers_shift"), GridView)
            Dim lnk_ref_no_shift As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
            'gv_approvers_shift.DataSource = ws.Get_Shift_Application_Approver(lnk_ref_no_shift.Text)
            gv_approvers_shift.Dispose()
            gv_approvers_shift.DataSource = ws.Get_Shift_Approver(lnk_ref_no_shift.Text)
            gv_approvers_shift.DataBind()
        Next

    End Sub

#End Region



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        'Try
        'If Button1.Text = "Upload" Then
        If FileUpload1.HasFile Then

            FileInfo = New FileInfo(FileUpload1.PostedFile.FileName)
            Dim strCsvFilePath As String = Server.MapPath("MyCSVFolder") + "\\" + FileInfo.Name
            'Dim strCsvFilePath As String = "~/MyCSVFolder/Exported/" + FileInfo.Name
            FileUpload1.SaveAs(strCsvFilePath)
            Dim strFilePath As String = Server.MapPath("MyCSVFolder") + "\\"
            MyFile = strFilePath + FileInfo.Name

            If InStr(FileInfo.Name, "-") <= 0 Then
                UserMsgBox("Invalid file, please locate valid file.")
                Exit Sub
            End If

            If Mid(FileInfo.Name, 1, InStr(FileInfo.Name, "-") - 1) <> Current_User.Employee_ID Then
                UserMsgBox("Invalid file, please locate valid file.")
                Exit Sub
            End If

            If Mid(FileInfo.Name, 1, InStr(FileInfo.Name, "-") - 1) <> Current_User.Employee_ID Then
                UserMsgBox("Invalid file, please locate valid file.")
                Exit Sub
            End If


            If Val(Mid(FileInfo.Name, InStr(FileInfo.Name, ".") - 4, 4)) <> DDL_Year_Update.SelectedValue Then
                UserMsgBox("Month and Year does not match with the content of the file, please verify.")
                Exit Sub
            End If

            If Val(Mid(FileInfo.Name, InStr(FileInfo.Name, "-") + 1, Len(FileInfo.Name) - 8 - InStr(FileInfo.Name, "-"))) <> DDL_Month_Update.SelectedValue Then
                UserMsgBox("Month and Year does not match with the content of the file, please verify.")
                Exit Sub
            End If


            xlapp = Microsoft.VisualBasic.Interaction.CreateObject("Excel.Application")
            'MsgBox(MyFile)
            'MsgBox(FileUpload1.FileName)
            Label5.Text = FileUpload1.PostedFile.FileName

            Books = xlapp.Workbooks
            Dim FileToOpen As String = MyFile
            Books.open(FileToOpen)

            xlBook = Books.Item(1)        'get Book reference
            Sheets = xlBook.Worksheets  'get Sheets reference

            Label5.Text = MyFile
            xlapp.DisplayAlerts = False    'This will prevent any message prompts from Excel (IE.."Do you want to save before closing?")
            xlapp.Visible = False    'We don't want the app visible while we are populating it.

            xlSheet = Sheets.item("Sheet1")   'get Sheet reference

            Dim dt As New DataTable
            Dim dr As DataRow
            Dim varDay As String
            dt.Columns.Add(New DataColumn("Employee_Name"))
            dt.Columns.Add(New DataColumn("Employee_ID"))
            Dim varDate As DateTime = Trim(Str(DDL_Month_Update.SelectedValue)) + "/" + "01/" + Trim(Str(DDL_Year_Update.SelectedValue))
            Dim FirstDate As DateTime = varDate

            Dim jValue As Integer = xlSheet.UsedRange.Cells.Columns.Count
            Dim iValue As Integer = xlSheet.UsedRange.Cells.Rows.Count

            Do While Month(varDate) = DDL_Month_Update.SelectedValue
                varDay = varDate.ToString("dd-MMM-yyyy") + "  " + Mid(varDate.DayOfWeek.ToString, 1, 3)
                'varDay = Day(varDate).ToString + " - " + Mid(varDate.DayOfWeek.ToString, 1, 3)
                dt.Columns.Add(New DataColumn(varDay))
                varDate = DateAdd(DateInterval.Day, 1, varDate)
            Loop


            'For x As Integer = 1 To 30
            '    varDay = "Day " + Trim(Str(x))
            '    dt.Columns.Add(New DataColumn(varDay))
            'Next


            'If Month(xlSheet.cells(1, 3).value) = DDL_Month_Update.SelectedValue And Year(xlSheet.cells(1, 3).value) = DDL_Year_Update.SelectedValue Then
            Dim rowctr As Integer = 3
            ws.Shift_Upload_Error_Delete(Current_User.Employee_ID)

            Dim Blank_Counter As Integer = 0
            Do While True
                If Len(xlSheet.cells(rowctr, 1).value) = 0 And Len(xlSheet.cells(rowctr, 2).value) = 0 And Len(xlSheet.cells(rowctr, 3).value) = 0 Then
                    Blank_Counter = Blank_Counter + 1
                    If Blank_Counter > 10 Then
                        Exit Do
                    End If
                Else

                    Dim EmployeeNumber As String = xlSheet.cells(rowctr, 2).value
                    Dim EmployeeName As String = xlSheet.cells(rowctr, 1).value

                    ' Do While Len(xlSheet.cells(rowctr, 1).value) <> 0
                    If Len(xlSheet.cells(rowctr, 2).value) <= 0 Then
                        EmployeeNumber = ""
                    End If

                    If Len(xlSheet.cells(rowctr, 1).value) <= 0 Then
                        EmployeeName = ""
                    End If
                    EmployeeName.Replace("&nbsp;", "no data")
                    'If ws.Validate_Employee(xlSheet.cells(rowctr, 2).value, xlSheet.cells(rowctr, 1).value) Then
                    dr = dt.NewRow()
                    'dr("Employee_Name") = xlSheet.cells(rowctr, 1).value
                    dr("Employee_Name") = EmployeeName.Replace("&#241;", Chr(241))
                    dr("Employee_ID") = EmployeeNumber
                    varDate = FirstDate
                    Dim y As Integer = 1
                    Do While Month(varDate) = DDL_Month_Update.SelectedValue
                        varDay = varDate.ToString("dd-MMM-yyyy") + "  " + Mid(varDate.DayOfWeek.ToString, 1, 3)
                        'varDay = Day(varDate).ToString + " - " + Mid(varDate.DayOfWeek.ToString, 1, 3)
                        dr(varDay) = xlSheet.cells(rowctr, 2 + y).value
                        varDate = DateAdd(DateInterval.Day, 1, varDate)
                        y = y + 1
                    Loop
                    dt.Rows.Add(dr)
                    '    Else
                    '    ws.Shift_Upload_Error(Current_User.Employee_ID, "Invalid Employee ID or Name at Row " + Trim(rowctr.ToString))
                    'End If
                End If
                rowctr = rowctr + 1
            Loop


            GridView3.Dispose()
            GridView3.DataSource = ws.Get_Shift_Upload_Error(Current_User.Employee_ID)
            GridView3.DataBind()

            If GridView3.Rows.Count > 0 Then
                UserMsgBox("Cannot continue, an error has encountered on the excel file. Please review details below.")
                Exit Sub

            End If


            GridView1.Dispose()
            GridView1.DataSource = dt
            GridView1.DataBind()

            If GridView1.Rows.Count > 0 Then

                ' GridView1.Columns(1).ItemStyle.HorizontalAlign = HorizontalAlign.Left
                'Button1.Text = "New Upload"
                FileUpload1.Visible = False
                btn_Capture.Visible = True
                btn_cancel.Visible = True
                Label14.Visible = True
                Button1.Visible = False
                Label_Month.Visible = False
                Label_Year.Visible = False
                DDL_Month_Update.Visible = False
                DDL_Year_Update.Visible = False
                'GridView1.Columns.Item("Employee Name").ItemStyle.HorizontalAlign = HorizontalAlign.Left
            End If
            'Else
            '    UserMsgBox("Month and Year does not match with the content of the file, please verify.")
            '    xlBook.close()

            '    xlapp.quit()

            '    Sheets = Nothing
            '    Books = Nothing
            '    FileUpload1.Dispose()
            '    Exit Sub
            'End If
            xlBook.close()
            xlapp.quit()

            Sheets = Nothing
            Books = Nothing

            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlapp)
            xlapp = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            FileUpload1.Dispose()
            GC.Collect()
            TabContainer1.ActiveTabIndex = 1

        End If


        'Else
        '    Button1.Text = "Upload"
        '    FileUpload1.Visible = True
        '    btn_Capture.Visible = False
        '    btn_cancel.Visible = False
        '    Label14.Visible = False

        'End If

        'Catch ex As Exception
        '    UserMsgBox(ex.Message.ToString)

        'End Try

        '641-7471 641-7528

    End Sub

#Region "Insert"

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Capture.Click
        Dim varDate As DateTime = Trim(Str(DDL_Month_Update.SelectedValue)) + "/" + "01/" + Trim(Str(DDL_Year_Update.SelectedValue))
        Dim varDay As Integer
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim ShiftCode As String
        Dim NewShiftCode As String
        Dim EmployeeNumber As String
        Dim EmployeeName As String
        dt.Columns.Add(New DataColumn("employee_name"))
        dt.Columns.Add(New DataColumn("employee_id"))
        dt.Columns.Add(New DataColumn("date"))
        dt.Columns.Add(New DataColumn("shift_from"))
        dt.Columns.Add(New DataColumn("shift_to"))
        ws.Shift_Upload_Error_Delete(Current_User.Employee_ID)
        For i As Integer = 0 To GridView1.Rows.Count - 1
            varDate = Trim(Str(DDL_Month_Update.SelectedValue)) + "/" + "01/" + Trim(Str(DDL_Year_Update.SelectedValue))
            EmployeeNumber = GridView1.Rows(i).Cells(1).Text
            'EmployeeNumber = EmployeeNumber.Replace("&#241;", Chr(241))
            EmployeeName = GridView1.Rows(i).Cells(0).Text
            EmployeeName = EmployeeName.Replace("&amp;#241;", Chr(241))
            EmployeeName = EmployeeName.Replace("&#241;", Chr(241))

            'If EmployeeNumber = "63000345" Then
            '    MsgBox(EmployeeName + " " + EmployeeNumber)
            'End If
            If ws.Validate_Employee(EmployeeNumber, EmployeeName) Then

                If ws.CheckIfValidApprover(Current_User.Employee_ID, EmployeeNumber) Then
                    Do While Month(varDate) = DDL_Month_Update.SelectedValue
                        varDay = Day(varDate)

                        NewShiftCode = GridView1.Rows(i).Cells(varDay + 1).Text
                        If Not IsDBNull(ws.Shift_Upload_Equivalent(NewShiftCode, Current_User.Branch_id, 1)) Then
                            NewShiftCode = ws.Shift_Upload_Equivalent(NewShiftCode, Current_User.Branch_id, 1)
                        End If

                        If ws.CheckIfValidShift(EmployeeNumber, NewShiftCode) Then

                            ShiftCode = ws.Get_Shift_Date(EmployeeNumber, varDate)

                            If (ShiftCode <> NewShiftCode) Then

                                dr = dt.NewRow()
                                dr("employee_name") = GridView1.Rows(i).Cells(0).Text
                                dr("employee_id") = GridView1.Rows(i).Cells(1).Text
                                dr("date") = varDate.ToString("dd-MMM-yyyy")
                                dr("shift_from") = ws.Get_Shift_Date(GridView1.Rows(i).Cells(1).Text, varDate)
                                dr("shift_to") = NewShiftCode
                                dt.Rows.Add(dr)
                            End If
                        Else
                            NewShiftCode = GridView1.Rows(i).Cells(varDay + 1).Text.Replace("&nbsp;", "no data")
                            If NewShiftCode = "no data" Then
                                ws.Shift_Upload_Error(Current_User.Employee_ID, "Employee : " + EmployeeNumber + " " + EmployeeName + " : " + varDate.ToString("dd-MMM-yyyy") + " shift : " + NewShiftCode)
                            Else
                                ws.Shift_Upload_Error(Current_User.Employee_ID, "Employee : " + EmployeeNumber + " " + EmployeeName + " : " + varDate.ToString("dd-MMM-yyyy") + " shift : " + NewShiftCode + " is not valid")
                            End If

                        End If
                        varDate = DateAdd(DateInterval.Day, 1, varDate)
                    Loop
                Else
                    ' Log Upload error
                    ws.Shift_Upload_Error(Current_User.Employee_ID, "Employee " + EmployeeNumber + " is not under your jurisdiction!")
                End If

            Else
                ws.Shift_Upload_Error(Current_User.Employee_ID, "Invalid Employee ID and/or Name at Line # " + Trim((i + 1).ToString) + " -> Employee ID : " + EmployeeNumber + " Employee Name : " + EmployeeName)
            End If

        Next

        GridView2.DataSource = dt
        GridView2.DataBind()


        GridView3.Dispose()
        GridView3.DataSource = ws.Get_Shift_Upload_Error(Current_User.Employee_ID)
        GridView3.DataBind()

        If GridView3.Rows.Count > 0 Then
            btn_Capture.Visible = False
            UserMsgBox("Cannot capture  the shift schedule due to error/s found in your excel file. Please check the details at the bottom part of this page.")
            Exit Sub
        End If

        If GridView2.Rows.Count > 0 Then
            btn_Save.Visible = True
            Label8.Visible = True
            btn_Capture.Visible = False
            Button1.Visible = False
            FileUpload1.Visible = False

        Else
            UserMsgBox("No changes made on the selected shift.")

        End If

        TabContainer1.ActiveTabIndex = 1
    End Sub

#End Region

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Save.Click
        If GridView3.Rows.Count = 0 Then

            Dim varDate As DateTime = Trim(Str(DDL_Month_Update.SelectedValue)) + "/" + "01/" + Trim(Str(DDL_Year_Update.SelectedValue))
            Dim ShiftCode As String
            Dim NewShiftCode As String
            Dim EmployeeNumber As String

            Dim var_ref_no As String = ws.Insert_Shift_Application(Current_User.Employee_ID, DDL_Month_Update.SelectedValue, DDL_Year_Update.SelectedValue, 1)

            For xy As Integer = 0 To GridView2.Rows.Count - 1
                EmployeeNumber = GridView2.Rows(xy).Cells(1).Text
                'varDate = Trim(Str(DDL_Month_Update.SelectedValue)) + "/" + Trim(GridView2.Rows(xy).Cells(3).Text) + "/" + Trim(Str(DDL_Year_Update.SelectedValue))
                varDate = Trim(GridView2.Rows(xy).Cells(2).Text)
                ShiftCode = GridView2.Rows(xy).Cells(3).Text
                NewShiftCode = GridView2.Rows(xy).Cells(4).Text
                ws.Insert_Shift_Header(var_ref_no, varDate, NewShiftCode, ShiftCode, EmployeeNumber, Current_User.Employee_ID, 1)

            Next
            ws.Insert_Shift_Approvers(var_ref_no, Current_User.Employee_ID)
            GridView6.Dispose()
            GridView6.DataSource = ws.Get_Shift_Approval(Current_User.Employee_ID)
            GridView6.DataBind()
            get_approvers()
            send_notifications(Current_User.Employee_ID, LCase(Current_User.FirstName) & " " & UCase(Current_User.LastName))
            UserMsgBox("Schedule has been submitted under Reference No. " + var_ref_no)
            ResetSettings()

            'Label8.Visible = True
            'Label8.Text = IIf(ws.Get_Shift_Request_Status(var_ref_no) = 1, "APPROVED", "PENDING")
        Else
            UserMsgBox("Cannot submit the shift schedule due to error/s on data encountered. Please check the details at the bottom part of this page.")
        End If
        TabContainer1.ActiveTabIndex = 1
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


    Private Sub OpenModalPage(ByVal PageName As String)

        Dim strScript As String = "<script language=JavaScript>"
        strScript &= "self.open('" & PageName & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=yes,height=100%,width=100%,left=200,top=100')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If
    End Sub

    Private Sub OpenModalPage2(ByVal VMonth As Integer, ByVal VYear As Integer, ByVal page_name As String, ByVal vheight As String, ByVal vwidth As String)
        Session("Monght") = VMonth
        Session("Year") = VYear
        Dim strScript As String = "<script language=JavaScript>"

        strScript &= "self.open('" & page_name & "','EmailPopup','fullscreen=no,status=no,scrollbars=no,address=no,resizable=no,height=" & vheight & ",width=" & vwidth & ",left=1,top=1')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If
    End Sub
    Private Sub OpenModalPage3(ByVal RefNo As String, ByVal page_name As String, ByVal vheight As String, ByVal vwidth As String)
        Session("RefNo") = RefNo
        Dim strScript As String = "<script language=JavaScript>"

        strScript &= "self.open('" & page_name & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=" & vheight & ",width=" & vwidth & ",left=1,top=1')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If
    End Sub
    Private Sub OpenModalPage4(ByVal page_name As String, ByVal vheight As String, ByVal vwidth As String)
        Dim strScript As String = "<script language=JavaScript>"

        strScript &= "self.open('" & page_name & "','EmailPopup','fullscreen=no,status=no,scrollbars=yes,address=no,resizable=no,height=" & vheight & ",width=" & vwidth & ",left=1,top=1')"
        strScript &= "</script>"

        If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
            ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
        End If
    End Sub
#End Region


    Protected Sub Button2_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "Process" Then

            ws.Get_Employee_Shift_Schedules(Current_User.Employee_ID, Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue)
            Session("Month") = Me.DDL_Month_SS.SelectedValue
            Session("Year") = Me.DDL_Year_SS.SelectedValue

            'OpenModalPage3("98-2011-002554D", "ob_details.aspx", 600, 800)

            Dim v_status As String = ws.Get_ShiftSchedule_Status(Current_User.Employee_ID, Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue)
            If Not v_status = "" Then
                lbl_stat.Visible = True
                lbl_stat.Text = "Status = " & v_status

                DDL_Month_SS.Enabled = False
                DDL_Year_SS.Enabled = False

                Button2.Visible = True
                Button2.Text = "Cancel"
                Button4.Visible = True
            Else
                lbl_stat.Visible = True
                lbl_stat.Text = "Status = No Data Found"

                Button4.Visible = False
            End If

            







            'GridView4.DataSource = ws.Get_Employee_Shift_Schedules(Current_User.Employee_ID, Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue)
            'GridView4.DataBind()

            'If GridView4.Rows.Count > 0 Then

            '    Dim varDate As DateTime = Trim(Str(DDL_Month_SS.SelectedValue)) + "/" + "01/" + Trim(Str(DDL_Year_SS.SelectedValue))
            '    Dim varday As String



            '    Do While Month(varDate) = DDL_Month_SS.SelectedValue
            '        varday = varDate.ToString("dd-MMM-yyyy") + "  " + Mid(varDate.DayOfWeek.ToString, 1, 3)
            '        GridView4.HeaderRow.Cells(Day(varDate) + 1).Text = varday
            '        GridView4.Columns(Day(varDate) + 1).ItemStyle.Width = Unit.Pixel(400)
            '        varDate = DateAdd(DateInterval.Day, 1, varDate)
            '    Loop

            '    For i As Integer = 0 To GridView4.Rows.Count - 1
            '        varDate = Trim(Str(DDL_Month_SS.SelectedValue)) + "/" + "01/" + Trim(Str(DDL_Year_SS.SelectedValue))
            '        Do While Month(varDate) = DDL_Month_SS.SelectedValue

            '            If shift_changed(i, Day(varDate)) Then

            '                GridView4.Rows(i).Cells(Day(varDate) + 1).BackColor = Drawing.Color.Orange
            '            End If

            '            'varday = Day(varDate).ToString + " - " + Mid(varDate.DayOfWeek.ToString, 1, 3)


            '            varDate = DateAdd(DateInterval.Day, 1, varDate)
            '        Loop

            '        'GridView4.Rows(i).Cells(10).BackColor = Drawing.Color.Orange
            '        'GridView4.Rows(4).Cells(10).BackColor = Drawing.Color.Orange
            '    Next



            '    'Label9.Visible = True
            '    'Label9.Text = "Shift schedule for :" + DDL_Month_SS.SelectedItem.ToString + " " + Trim(Str(DDL_Year_SS.SelectedValue))
            '    'btn_ExportToExcel.Visible = True
            '    'btn_ExportToExcel0.Visible = True

            '    Label15.Visible = True
            '    GridView9.DataSource = ws.Get_Employee_Shift_Schedules(Current_User.Employee_ID, Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue)
            '    GridView9.DataBind()

            '    Dim v_status As String = ws.Get_ShiftSchedule_Status(Current_User.Employee_ID, DDL_Month_SS.SelectedValue, DDL_Year_SS.SelectedValue)
            '    If Not v_status = "" Then
            '        lbl_stat.Visible = True
            '        lbl_stat.Text = "Status = " & v_status
            '    End If
            '    DDL_Month_SS.Enabled = False
            '    DDL_Year_SS.Enabled = False
            '    Button2.Text = "Cancel"
            '    btn_ExportToExcel.Visible = True



            'End If
        Else
            ResetShiftSchedule()

        End If

        'GridView4.Rows(3).Cells(10).BackColor = Drawing.Color.Orange
        'GridView4.Rows(4).Cells(10).BackColor = Drawing.Color.Orange
    End Sub

    Private Sub ResetShiftSchedule()
        Button2.Text = "Process"
        Button4.Visible = False
        DDL_Month_SS.Enabled = True
        DDL_Year_SS.Enabled = True

        GridView4.Dispose()
        GridView4.DataSource = Nothing
        GridView4.DataBind()

        GridView9.Dispose()
        GridView9.DataSource = Nothing
        GridView9.DataBind()

        lbl_stat.Visible = False
        'Label9.Visible = False
        btn_ExportToExcel.Visible = False
        Label15.Visible = False
        HyperLink1.Visible = False
    End Sub



    'Protected Sub GridView4_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView4.PageIndexChanging
    '    GridView4.PageIndex = e.NewPageIndex
    '    GridView4.DataSource = ws.Get_Employee_Shift_Schedules(Current_User.Employee_ID, Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue)
    '    GridView4.DataBind()

    '    Dim varDate As DateTime = Trim(Str(DDL_Month_SS.SelectedValue)) + "/" + "01/" + Trim(Str(DDL_Year_SS.SelectedValue))
    '    Dim varday As String

    '    Do While Month(varDate) = DDL_Month_SS.SelectedValue
    '        varday = varDate.ToString("dd-MMM-yyyy") + "  " + Mid(varDate.DayOfWeek.ToString, 1, 3)
    '        GridView4.HeaderRow.Cells(Day(varDate) + 1).Text = varday
    '        varDate = DateAdd(DateInterval.Day, 1, varDate)
    '    Loop
    'End Sub

    Protected Sub GridView6_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView6.PageIndexChanging
        GridView6.PageIndex = e.NewPageIndex
        GridView6.DataSource = ws.Get_Shift_Approval(Current_User.Employee_ID)
        GridView6.DataBind()
        TabContainer1.ActiveTabIndex = 2
    End Sub

    Private Sub get_selected_row(ByVal gvrows_count As Integer, ByVal count As String)
        Dim row As GridViewRow = GridView4.Rows(gvrows_count)
        Dim lnk_ref As LinkButton = CType(row.FindControl("lnk_" & count), LinkButton)
        Dim EmpID As String = GridView4.Rows(gvrows_count).Cells(1).Text
        Dim workdate As Date = Trim(Str(DDL_Month_SS.SelectedValue)) + "/" + Trim(count) + "/" + Trim(Str(DDL_Year_SS.SelectedValue))
        GridView5.Dispose()
        GridView5.DataSource = ws.Get_Shift_History(EmpID, workdate)
        GridView5.DataBind()
        If GridView5.Rows.Count = 0 Then
            UserMsgBox("No changes made on the selected shift.")
        End If
    End Sub



    Function shift_changed(ByVal gvrows_count As Integer, ByVal count As String) As Boolean
        Dim row As GridViewRow = GridView4.Rows(gvrows_count)

        Dim EmpID As String = GridView4.Rows(gvrows_count).Cells(1).Text
        Dim workdate As Date = Trim(Str(DDL_Month_SS.SelectedValue)) + "/" + Trim(count) + "/" + Trim(Str(DDL_Year_SS.SelectedValue))
        Dim shiftchange As New DataSet
        shiftchange = Nothing
        shiftchange = ws.Get_Shift_History(EmpID, workdate)
        If shiftchange.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
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
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub GridView4_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView4.RowCreated
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
    End Sub


    Protected Sub btn_cancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        ResetSettings()
    End Sub

    Private Sub ResetSettings()
        FileUpload1.Visible = True
        'UploadCSVFile.Visible = True
        Button1.Text = "Upload"
        Button1.Visible = True
        btn_cancel.Visible = False
        btn_Capture.Visible = False
        btn_Save.Visible = False

        Label_Month.Visible = True
        Label_Year.Visible = True
        DDL_Month_Update.Visible = True
        DDL_Year_Update.Visible = True

        GridView1.Dispose()
        GridView1.DataSource = Nothing
        GridView1.DataBind()

        GridView2.Dispose()
        GridView2.DataSource = Nothing
        GridView2.DataBind()

        GridView3.Dispose()
        GridView3.DataSource = Nothing
        GridView3.DataBind()

        Label8.Visible = False
        Label14.Visible = False
        TabContainer1.ActiveTabIndex = 1
    End Sub

    Protected Sub GridView6_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView6.RowCommand
        If e.CommandName = "cmd_ref_no" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim refno As String

            Dim row As GridViewRow = GridView6.Rows(index)
            Dim lnk_ref_no As LinkButton = CType(row.FindControl("lnk_ref_no"), LinkButton)
            refno = lnk_ref_no.Text
            Session("RefNo") = refno
            OpenModalPage4("UploadShift_details.aspx", 600, 1024)
            
            'lblDetailsOfReference.Text = (refno)
            'lblDetailsOfReference.Visible = True
            'GridView8.Dispose()
            'GridView8.DataSource = ws.Get_Shift_request_Details(refno)
            'GridView8.DataBind()
            TabContainer1.ActiveTabIndex = 2

            Dim strScript As String = "<script language='javascript' id='myClientScript'>gridtemp();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)

            strScript = "<script language='javascript' id='myClientScript'>tab3();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)


        End If

        If e.CommandName = "cmd_Status" Then
            TabContainer1.ActiveTabIndex = 2
        End If
    End Sub

    Protected Sub GridView6_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView6.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no As LinkButton = CType(e.Row.FindControl("lnk_ref_no"), LinkButton)
            lnk_ref_no.CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub


    Protected Sub btn_ExportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ExportToExcel.Click
        Try


            'GridView9.AllowPaging = False
            'GridView9.DataSource = ws.Get_Employee_Shift_Schedules(Current_User.Employee_ID, Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue)
            'GridView9.DataBind()
            xlapp = Microsoft.VisualBasic.Interaction.CreateObject("Excel.Application")
            Books = xlapp.Workbooks

            'Dim FileToOpen As String = "C:\inetpub\wwwroot\holcim_app_shift\MyCSVFolder\Exported\" + Current_User.Employee_ID + "-" + Trim(DDL_Month_SS.SelectedValue.ToString) + Trim(DDL_Year_SS.SelectedValue) + ".xls"
            Dim FileToOpen As String = Server.MapPath("MyCSVFolder\Exported") + "\" + Current_User.Employee_ID + "-" + Trim(DDL_Month_SS.SelectedValue.ToString) + Trim(DDL_Year_SS.SelectedValue) + ".xls"
            Books.Add()
            'Books.open(FileToOpen)

            xlBook = Books.Item(1)        'get Book reference
            Sheets = xlBook.Worksheets  'get Sheets reference

            Label5.Text = MyFile
            xlapp.DisplayAlerts = False    'This will prevent any message prompts from Excel (IE.."Do you want to save before closing?")
            xlapp.Visible = False    'We don't want the app visible while we are populating it.
            xlapp.cutcopymode = False
            'xlSheet = Sheets.item("Shift Sched Batch Upload (SSBU)")   'get Sheet reference
            xlSheet = Sheets.item("Sheet1")

            Dim varDate As DateTime = Trim(Str(DDL_Month_SS.SelectedValue)) + "/" + "01/" + Trim(Str(DDL_Year_SS.SelectedValue))
            Dim varDay As String

            xlSheet.cells(1, 1).value() = "Employee Name"
            xlSheet.cells(1, 2).value() = "Personnel Number"

            Dim colCtr As Integer = 3
            Do While Month(varDate) = DDL_Month_SS.SelectedValue

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

                        varFormula = "=$DD$" & Trim(rowCtr.ToString) & ":$DS$" & Trim(rowCtr.ToString)
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
            Dim ExcelFile As String = Current_User.Employee_ID + "-" + Trim(DDL_Month_SS.SelectedValue.ToString) + Trim(DDL_Year_SS.SelectedValue) + ".xls"
            Session("DownloadExcel") = ExcelFile
            'OpenModalPage("download.aspx")
            'Response.Redirect("download.aspx")
            HyperLink1.Visible = True
            Dim FileToDownload As String = "~/MyCSVFolder/Exported/" + Session("DownloadExcel")
            HyperLink1.Text = "Download " + Session("DownloadExcel")
            HyperLink1.NavigateUrl = FileToDownload
            TabContainer1.ActiveTabIndex = 0
        Catch ex As Exception
            ws.Shift_Upload_Error(Current_User.Employee_ID, "Error Export to excel: " + ex.Message.ToString)
            Label_Error_Message.Visible = True
            Label_Error_Message.Text = "Error Export to excel: " + ex.Message.ToString
        End Try
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UploadCSVFile.Click

        'Label4.Visible = True
        'Try

        If FileUpload1.HasFile Then
            FileInfo = New FileInfo(FileUpload1.PostedFile.FileName)
            Dim strCsvFilePath As String = Server.MapPath("MyCSVFolder") + "\\" + FileInfo.Name

            Filename = FileInfo.Name
            FilePath = FileInfo.DirectoryName + "\"
            MyFile = FileInfo.DirectoryName + "\" + FileInfo.Name

            If Mid(FileInfo.Name, 1, InStr(FileInfo.Name, "-") - 1) <> Current_User.Employee_ID Then
                UserMsgBox("Invalid file, please locate valid file.")
                Exit Sub
            End If

            If Mid(FileInfo.Name, 1, InStr(FileInfo.Name, "-") - 1) <> Current_User.Employee_ID Then
                UserMsgBox("Invalid file, please locate valid file.")
                Exit Sub
            End If


            If Val(Mid(FileInfo.Name, InStr(FileInfo.Name, ".") - 4, 4)) <> DDL_Year_Update.SelectedValue Then
                UserMsgBox("Month and Year does not match with the content of the file, please verify.")
                Exit Sub
            End If

            If Val(Mid(FileInfo.Name, InStr(FileInfo.Name, "-") + 1, Len(FileInfo.Name) - 8 - InStr(FileInfo.Name, "-"))) <> DDL_Month_Update.SelectedValue Then
                UserMsgBox("Month and Year does not match with the content of the file, please verify.")
                Exit Sub
            End If



            'Label4.Text = FileUpload1.PostedFile.FileName
            Dim isValidFile As Boolean = False
            'Dim validFileTypes As String() = {"bmp", "gif", "png", "jpg", "jpeg", "doc", "xls"}

            Dim ext As String = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName)



            'For i As Integer = 0 To validFileTypes.Length - 1

            '    If ext = "." & validFileTypes(i) Then

            '        isValidFile = True

            '        Exit For

            '    End If

            'Next

            If ext = ".csv" Then
                isValidFile = True
            End If
            isValidFile = True
            If isValidFile Then

                'Save the CSV file in the Server inside 'MyCSVFolder' 
                ws.Shift_Upload_Error_Delete(Current_User.Employee_ID)


                FileUpload1.SaveAs(strCsvFilePath)

                'Fetch the location of CSV file 
                Dim strFilePath As String = Server.MapPath("MyCSVFolder") + "\\"

                Dim filestr As FileStream = New FileStream(strFilePath + "\\schemaApr.ini", FileMode.Create, FileAccess.Write)
                Dim writer As StreamWriter = New StreamWriter(filestr)

                writer.WriteLine("[" + FileInfo.Name + "]")
                writer.WriteLine("ColNameHeader=True")
                writer.WriteLine("Format=CSVDelimited")
                writer.WriteLine("DateTimeFormat=dd-MM-yyyy")
                writer.WriteLine("Col1=A Text Width 250") ' Employee Number
                writer.WriteLine("Col2=B Text Width 250") ' Employee Name
                writer.WriteLine("Col3=C Text Width 250") ' Approver
                writer.WriteLine("Col4=D Text Width 250") ' Approver Name
                writer.WriteLine("Col5=E Text Width 250") ' Level of Authority
                writer.WriteLine("Col6=F Text Width 250") ' Level of Authority
                writer.WriteLine("Col7=G Text Width 250") ' Level of Authority
                writer.WriteLine("Col8=H Text Width 250") ' Level of Authority
                writer.WriteLine("Col9=I Text Width 250") ' Level of Authority
                writer.WriteLine("Col10=J Text Width 250") ' Level of Authority
                writer.WriteLine("Col11=K Text Width 250") ' Level of Authority
                writer.WriteLine("Col12=L Text Width 250") ' Level of Authority
                writer.WriteLine("Col13=M Text Width 250") ' Level of Authority
                writer.WriteLine("Col14=N Text Width 250") ' Level of Authority

                writer.WriteLine("Col15=O Text Width 250") ' Level of Authority
                writer.WriteLine("Col16=P Text Width 250") ' Level of Authority
                writer.WriteLine("Col17=Q Text Width 250") ' Level of Authority
                writer.WriteLine("Col18=R Text Width 250") ' Level of Authority
                writer.WriteLine("Col19=S Text Width 250") ' Level of Authority
                writer.WriteLine("Col20=T Text Width 250") ' Level of Authority
                writer.WriteLine("Col21=U Text Width 250") ' Level of Authority
                writer.WriteLine("Col22=V Text Width 250") ' Level of Authority
                writer.WriteLine("Col23=W Text Width 250") ' Level of Authority
                writer.WriteLine("Col24=X Text Width 250") ' Level of Authority
                writer.WriteLine("Col25=Y Text Width 250") ' Level of Authority
                writer.WriteLine("Col26=Z Text Width 250") ' Level of Authority
                writer.WriteLine("Col27=AA Text Width 250") ' Level of Authority
                writer.WriteLine("Col28=AB Text Width 250") ' Level of Authority
                writer.WriteLine("Col29=AC Text Width 250") ' Level of Authority
                writer.WriteLine("Col30=AD Text Width 250") ' Level of Authority
                writer.WriteLine("Col31=AE Text Width 250") ' Level of Authority
                writer.WriteLine("Col32=AF Text Width 250") ' Level of Authority
                writer.WriteLine("Col33=AG Text Width 250") ' Level of Authority

                writer.Close()
                writer.Dispose()

                filestr.Close()
                filestr.Dispose()


                'Dim strSql As String = "SELECT * FROM [" + FileInfo.Name + "]"
                Dim strSql As String
                'Dim strCSVConnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + ";" + "Extended Properties='text;HDR=YES;'"
                Dim strCSVConnString As String
                If ext = ".csv" Then
                    strSql = "SELECT * FROM [" + FileInfo.Name + "]"
                    strCSVConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + ";" + "Extended Properties='text;HDR=YES;'"
                Else
                    'strSql = "SELECT * FROM Sheet1$"
                    strSql = "SELECT * FROM Batch Upload template for Shift$"
                    strCSVConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFilePath + FileInfo.Name + ";" + "Extended Properties='Excel 12.0;HDR=YES;'"
                End If
                'load the data from CSV to DataTable 

                Dim oleda As OleDbDataAdapter = New OleDbDataAdapter(strSql, strCSVConnString)

                Dim dtbCSV As DataTable = New DataTable()
                oleda.Fill(dtbCSV)

                ' Display data in a GridView control 
                GridView1.Dispose()
                GridView1.DataSource = dtbCSV
                GridView1.DataBind()



                'Label3.ForeColor = System.Drawing.Color.Green

                'Label3.Text = "File uploaded successfully."

                If GridView1.Rows.Count > 0 Then


                    'Button1.Text = "New Upload"
                    FileUpload1.Visible = False
                    UploadCSVFile.Visible = False
                    btn_Capture.Visible = True
                    btn_cancel.Visible = True
                    Label14.Visible = True
                    Button1.Visible = False
                End If
            Else
                UserMsgBox("Invalid File. Please upload a File with extension CSV")


            End If

        End If
        'Catch ex As Exception
        '    UserMsgBox(ex.Message.ToString)
        'End Try
    End Sub




    Protected Sub btn_ExportToExcel0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ExportToExcel0.Click
        'GridView9.DataSource = ws.Get_Employee_Shift_Schedules(Current_User.Employee_ID, Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue)
        'GridView9.DataBind()
        'Dim ExcelFile As String = Current_User.Employee_ID + "-" + Trim(DDL_Month_SS.SelectedValue.ToString) + Trim(DDL_Year_SS.SelectedValue) + ".xls"
        'GridViewExportUtil.Export(ExcelFile, Me.GridView9, "", "")

    End Sub




    Protected Sub TabContainer1_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabContainer1.ActiveTabChanged
        Session("OldIndex") = Session("myIndex")
        Session("myIndex") = TabContainer1.ActiveTabIndex
        If Session("myIndex") = 0 Then

        End If
        If Session("myIndex") = 1 Then

            
        End If
        If Session("myIndex") = 2 Then

            GridView6.DataSource = ws.Get_Shift_Approval(Current_User.Employee_ID)
            GridView6.DataBind()
            get_approvers()

            If Session("OldIndex") <> Session("myIndex") Then
                Dim strScript As String = "<script language='javascript' id='myClientScript'>tab3();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
            End If
            'Dim strScript As String = "<script language='javascript' id='myClientScript'>gridtemp();</script>"
            'ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)

            'strScript = "<script language='javascript' id='myClientScript'>tab3();</script>"
            'ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)


        End If
        If Session("myIndex") = 3 Then

        End If
    End Sub

    Protected Sub Button3_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Button3.Text = "View History" Then
            GridView7.DataSource = ws.Get_All_Shift_History(Current_User.Employee_ID, DateFrom.Text, DateTo.Text)
            GridView7.DataBind()
            TabContainer1.ActiveTabIndex = 3
            btn_HistoryToExcel.Visible = False
            If GridView7.Rows.Count > 0 Then
                btn_HistoryToExcel.Visible = True
                Button3.Text = "Refresh"
                DateFrom.Enabled = False
                DateTo.Enabled = False
            Else
                Button3.Text = "View History"
                DateFrom.Enabled = True
                DateTo.Enabled = True
                btn_HistoryToExcel.Visible = False
            End If
        Else
            Button3.Text = "View History"
            DateFrom.Enabled = True
            DateTo.Enabled = True
            btn_HistoryToExcel.Visible = False
            GridView7.Dispose()
            GridView7.DataSource = Nothing
            GridView7.DataBind()
        End If

    End Sub


    Protected Sub btn_HistoryToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_HistoryToExcel.Click
        Dim ExcelFile As String = Current_User.Employee_ID + "-" + Trim(DDL_Month_SS.SelectedValue.ToString) + Trim(DDL_Year_SS.SelectedValue) + ".xls"
        GridViewExportUtil.Export("Shift Schedule History.xls", Me.GridView7, "Shift Schedule History", "From : " & DateFrom.Text & " To " & DateTo.Text)
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


    Protected Sub HyperLink1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles HyperLink1.Load
        'HyperLink1.Visible = False
    End Sub


    'Public Sub AddSortDirectionImage(ByVal GridViewID As GridView, ByVal ItemRow As GridViewRow)
    '    If GridViewID.AllowSorting = False Then
    '        Return
    '    End If
    '    Dim SortImage As Image = New Image
    '    Dim LabelSpace As Label = New Label
    '    SortImage.ImageUrl = (GridViewID.SortDirection = SortDirection.Ascending  @"~/sort_asc.gif" : @"~/sort_desc.gif")
    '    SortImage.Visible = True
    '    LabelSpace.Text = ""
    '    For i As Integer = 0 To GridViewID.Columns.Count
    '        Dim colExpr As String = GridViewID.Columns(i).SortExpression
    '        If colExpr <> "" And colExpr = GridViewID.SortExpression Then
    '            ItemRow.Cells(i).Controls.Add(LabelSpace)
    '            ItemRow.Cells(i).Controls.Add(SortImage)

    '        End If
    '    Next

    'End Sub

    Private Sub FreezeGridViewHeader(ByVal _gvl As GridView, ByVal _tbl As Table, ByVal _pcl As Panel)
        Page.EnableViewState = False
        _tbl.Rows.Add(_gvl.HeaderRow)
        _tbl.Rows(0).ControlStyle.CopyFrom(_gvl.HeaderStyle)
        _tbl.CellPadding = _gvl.CellPadding
        _tbl.CellSpacing = _gvl.CellSpacing
        _tbl.BorderWidth = _gvl.BorderWidth

        Dim Count As Integer = 0
        _pcl.Width = Unit.Pixel(100)
        For Count = 0 To _gvl.HeaderRow.Cells.Count - 1
            _tbl.Rows(0).Cells(Count).Width = _gvl.Columns(Count).ItemStyle.Width

        Next
    End Sub



    Protected Sub GridView4_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView4.RowDataBound

        'If e.Row.RowType = DataControlRowType.Header Then
        'e.Row.Cells(0).CssClass = "locked"
        'e.Row.Cells(1).CssClass = "locked"
        'End If
    End Sub


    Private Sub send_notifications(ByVal planner_id As String, ByVal planner_name As String)
        Dim body As String
        Dim recipients As String = ""
        body = planner_name & " is sending you a SHIFT SCHEDULE. For your review and approval. <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        Dim ds As DataSet
        ds = ws.Get_Approvers_Email(GridView2.Rows(0).Cells(1).Text, "FINAL")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim dr As DataRow = ds.Tables(0).Rows(i)
            If recipients = "" Then
                recipients = dr("email")
            Else
                recipients = recipients & ", " & dr("email")
            End If
        Next
        cls_email.SendEmail(recipients, body, planner_name)
    End Sub
    
    'Protected Sub Button4(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
    '    Button4.Visible = False
    '    Response.Redirect("UploadShiftExport.aspx")
    'End Sub

    
    Protected Sub Button4_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try
            'Button4.Visible = False
            'Response.Write("<script language='javascript'> { alert('Please Finish The Pending Dispute First'); }</script>")
            'Response.Redirect("UploadShiftExport.aspx")
            'Response.Write("<script language='javascript'> { dispPDF=window.close;} </script>")
            'Response.Write("<script language='javascript'> { dispPDF=window.open('UploadShiftExport.aspx','disputer','fullscreen=no,status=no,address=no,height=700,width=825,toolbar=no');}</script>")
            OpenModalPage2(Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue, "UploadShiftExport.aspx", 700, 825)
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        
    End Sub


End Class

