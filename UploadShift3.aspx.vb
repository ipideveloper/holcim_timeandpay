Imports System.Object
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports VB = Microsoft.VisualBasic
Imports System.Data.OleDb
Imports cls_Email_Notifications

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
        Server.ScriptTimeout = 10

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
            Session("myIndex") = 0
            btn1.Style.Add("color", "#FFFFFF")
            btn1.Style.Add("background-color", "#FF0000")
            PN1.Visible = True
            PN2.Visible = False
            PN3.Visible = False
            PN4.Visible = False

        End If
    End Sub


#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        GridView6.Dispose()
        GridView6.DataSource = ws.Get_Shift_Approval(Current_User.Employee_ID)
        GridView6.DataBind()
        get_approvers()
        If GridView6.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>tab3();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If
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

                If GridView1.Rows.Count > 10 Then
                    'Dim strScript As String = "<script language='javascript' id='myClientScript'>tab2();</script>"
                    'ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
                End If


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

        Else

            UserMsgBox("Please select excel file to upload before clicking this button")
            Exit Sub


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
        'If GridView1.Rows.Count > 10 Then
        '    Dim strScript As String = "<script language='javascript' id='myClientScript'>tab2();</script>"
        '    ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        'End If

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
        Session("Month") = VMonth
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
            Server.ScriptTimeout = 10
            Try

                ws.Get_Employee_Shift_Schedules(Current_User.Employee_ID, Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue)
                Session("Month") = Me.DDL_Month_SS.SelectedValue
                Session("Year") = Me.DDL_Year_SS.SelectedValue
            Catch ex As Exception
                UserMsgBox(ex.Message)
            End Try

            'OpenModalPage3("98-2011-002554D", "ob_details.aspx", 600, 800)

            Dim v_status As String = ws.Get_ShiftSchedule_Status(Current_User.Employee_ID, Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue)
            If Not v_status = "" Then
                lbl_stat.Visible = True
                lbl_stat.Text = "Status = " & v_status

                DDL_Month_SS.Enabled = False
                DDL_Year_SS.Enabled = False

                Session("Process") = "TRUE"
            Else
                lbl_stat.Visible = True
                lbl_stat.Text = "Status = No Current Shift Schedule for Approval"
                'Added Code By Andrew 11-9-2011 Start
                Session("Process") = "TRUE"
                'Added Code by Andrew 11-9-2011 End
            End If


        Else
            Session("Process") = "FALSE"
            ResetShiftSchedule()

        End If

    End Sub

    Private Sub ResetShiftSchedule()
        Button2.Text = "Process"
        Button4.Visible = False
        DDL_Month_SS.Enabled = True
        DDL_Year_SS.Enabled = True
        lbl_stat.Visible = False
        Label15.Visible = False

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

    End Sub

    Private Sub get_selected_row(ByVal gvrows_count As Integer, ByVal count As String)
        'Dim row As GridViewRow = GridView4.Rows(gvrows_count)
        'Dim lnk_ref As LinkButton = CType(row.FindControl("lnk_" & count), LinkButton)
        'Dim EmpID As String = GridView4.Rows(gvrows_count).Cells(1).Text
        'Dim workdate As Date = Trim(Str(DDL_Month_SS.SelectedValue)) + "/" + Trim(count) + "/" + Trim(Str(DDL_Year_SS.SelectedValue))
        'GridView5.Dispose()
        'GridView5.DataSource = ws.Get_Shift_History(EmpID, workdate)
        'GridView5.DataBind()
        'If GridView5.Rows.Count = 0 Then
        '    UserMsgBox("No changes made on the selected shift.")
        'End If
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

            Dim strScript As String = "<script language='javascript' id='myClientScript'>tab3();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)


        End If


    End Sub

    Protected Sub GridView6_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView6.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim lnk_ref_no As LinkButton = CType(e.Row.FindControl("lnk_ref_no"), LinkButton)
            lnk_ref_no.CommandArgument = e.Row.RowIndex.ToString
        End If
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


    Protected Sub Button3_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Button3.Text = "View History" Then
            GridView7.DataSource = ws.Get_All_Shift_History(Current_User.Employee_ID, DateFrom.Text, DateTo.Text)
            GridView7.DataBind()

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

            If GridView7.Rows.Count > 10 Then
                Dim strScript As String = "<script language='javascript' id='myClientScript'>tab4();</script>"
                ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
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

    Private Sub FreezeGridViewHeader(ByVal _gvl As GridView, ByVal _tbl As Table, ByVal _pcl As Panel)
        'Page.EnableViewState = False
        '_tbl.Rows.Add(_gvl.HeaderRow)
        '_tbl.Rows(0).ControlStyle.CopyFrom(_gvl.HeaderStyle)
        '_tbl.CellPadding = _gvl.CellPadding
        '_tbl.CellSpacing = _gvl.CellSpacing
        '_tbl.BorderWidth = _gvl.BorderWidth

        'Dim Count As Integer = 0
        '_pcl.Width = Unit.Pixel(100)
        'For Count = 0 To _gvl.HeaderRow.Cells.Count - 1
        '    _tbl.Rows(0).Cells(Count).Width = _gvl.Columns(Count).ItemStyle.Width

        'Next
    End Sub

    Private Sub send_notifications(ByVal planner_id As String, ByVal planner_name As String)
        Dim body As String
        Dim recipients As String = ""
        body = planner_name & " is sending you a SHIFT SCHEDULE. For your review and approval. <br><br><br><br> This is a system-generated message.  Do not reply to this message."

        Dim ds As DataSet
        ds = ws.Get_Approvers_Email(GridView2.Rows(0).Cells(1).Text, "FINAL")

        Dim ds_itself As DataSet = ws.Get_Users_Email(Current_User.Employee_ID)
        Dim dr_itself As DataRow = ds_itself.Tables(0).Rows(0)

        If dr_itself("email") = recipients Then
            recipients = ""
        End If

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim dr As DataRow = ds.Tables(0).Rows(i)
            If recipients = "" Then
                If dr("email") <> dr_itself("email") Then
                    recipients = dr("email")
                    Insert_Email_Summary_Notification(dr("email"), body, planner_name)
                Else

                End If
            Else
                If dr("email") <> dr_itself("email") Then
                    Insert_Email_Summary_Notification(dr("email"), body, planner_name)
                    recipients = recipients & ", " & dr("email")
                End If
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
            If Session("Process") = "TRUE" Then
                OpenModalPage2(Me.DDL_Month_SS.SelectedValue, Me.DDL_Year_SS.SelectedValue, "UploadShiftExport.aspx", 340, 825)
                Session("Process") = "FALSE"
                'Else
                'UserMsgBox("No Data Found")
            End If
            DDL_Month_SS.Enabled = True
            DDL_Year_SS.Enabled = True
            lbl_stat.Visible = False

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Protected Sub btn1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn1.Click
        btn1.Style.Add("color", "#FFFFFF")
        btn1.Style.Add("background-color", "#FF0000")

        btn2.Style.Add("color", "#696969")
        btn2.Style.Add("background-color", "#FFFFFF")

        btn3.Style.Add("color", "#696969")
        btn3.Style.Add("background-color", "#FFFFFF")

        btn4.Style.Add("color", "#696969")
        btn4.Style.Add("background-color", "#FFFFFF")



        PN1.Visible = True
        PN2.Visible = False
        PN3.Visible = False
        PN4.Visible = False
    End Sub

    Protected Sub btn2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn2.Click
        btn1.Style.Add("color", "#696969")
        btn1.Style.Add("background-color", "#FFFFFF")

        btn2.Style.Add("color", "#FFFFFF")
        btn2.Style.Add("background-color", "#FF0000")

        btn3.Style.Add("color", "#696969")
        btn3.Style.Add("background-color", "#FFFFFF")

        btn4.Style.Add("color", "#696969")
        btn4.Style.Add("background-color", "#FFFFFF")

        PN1.Visible = False
        PN2.Visible = True
        PN3.Visible = False
        PN4.Visible = False
    End Sub

    Protected Sub btn3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn3.Click
        btn1.Style.Add("color", "#696969")
        btn1.Style.Add("background-color", "#FFFFFF")

        btn2.Style.Add("color", "#696969")
        btn2.Style.Add("background-color", "#FFFFFF")

        btn3.Style.Add("color", "#FFFFFF")
        btn3.Style.Add("background-color", "#FF0000")

        btn4.Style.Add("color", "#696969")
        btn4.Style.Add("background-color", "#FFFFFF")

        PN1.Visible = False
        PN2.Visible = False
        PN3.Visible = True
        PN4.Visible = False

        If GridView6.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>tab3();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)

        End If


    End Sub

    Protected Sub btn4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn4.Click
        btn1.Style.Add("color", "#696969")
        btn1.Style.Add("background-color", "#FFFFFF")

        btn2.Style.Add("color", "#696969")
        btn2.Style.Add("background-color", "#FFFFFF")

        btn3.Style.Add("color", "#696969")
        btn3.Style.Add("background-color", "#FFFFFF")

        btn4.Style.Add("color", "#FFFFFF")
        btn4.Style.Add("background-color", "#FF0000")

        PN1.Visible = False
        PN2.Visible = False
        PN3.Visible = False
        PN4.Visible = True


    End Sub

    Protected Sub gridview6_sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridView6.Sorting
        'MsgBox(e.SortExpression)
        'Exit Sub
        GridView6.Dispose()
        GridView6.DataSource = ws.Get_Shift_Approval_Sort(Current_User.Employee_ID, e.SortExpression)
        GridView6.DataBind()
        'This is for freeze pane
        If GridView6.Rows.Count > 10 Then
            Dim strScript As String = "<script language='javascript' id='myClientScript'>tab3();</script>"
            ClientScript.RegisterStartupScript(Me.GetType(), "callTest", strScript)
        End If
    End Sub

End Class