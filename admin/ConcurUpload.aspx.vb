Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Configuration
Imports HolcimDbClass
Imports System.Data.SqlClient


Partial Class admin_ConcurUpload
    Inherits System.Web.UI.Page

    Protected Sub spWeb_Insert_OB_Header_Concur(ByVal ref_no As String, _
                                                ByVal employee_id As String, _
                                                ByVal purpose_of_travel As String, _
                                                ByVal date_from As Date, _
                                                ByVal date_to As Date, _
                                                ByVal created_by As String, _
                                                ByVal status As String, _
                                                ByVal date_approved As Date)
        Dim sqlParam(7) As SqlParameter
        sqlParam(0) = New SqlParameter("@ref_no", SqlDbType.VarChar, 15)
        sqlParam(0).Value = ref_no
        sqlParam(1) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(1).Value = employee_id
        sqlParam(2) = New SqlParameter("@purpose_of_travel", SqlDbType.VarChar, 100)
        sqlParam(2).Value = purpose_of_travel
        sqlParam(3) = New SqlParameter("@date_from", SqlDbType.DateTime)
        sqlParam(3).Value = date_from
        sqlParam(4) = New SqlParameter("@date_to", SqlDbType.DateTime)
        sqlParam(4).Value = date_to
        sqlParam(5) = New SqlParameter("@created_by", SqlDbType.VarChar, 15)
        sqlParam(5).Value = created_by
        sqlParam(6) = New SqlParameter("@status", SqlDbType.VarChar, 15)
        sqlParam(6).Value = status
        sqlParam(7) = New SqlParameter("@date_approved", SqlDbType.DateTime)
        sqlParam(7).Value = date_approved

        HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Insert_OB_Header_Concur", sqlParam)
    End Sub


    Protected Shared Function CheckConcurRefNo(ByVal ref_no As String) As String
        Dim ds As New DataSet
        Dim sqlParam(1) As SqlParameter

        sqlParam(0) = New SqlParameter("@ref_no", SqlDbType.VarChar, 20)
        sqlParam(0).Value = ref_no

        sqlParam(1) = New SqlParameter("@IdExist", SqlDbType.VarChar, 5)
        sqlParam(1).Direction = ParameterDirection.Output

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_CheckConcurRefNo", sqlParam)
        Return sqlParam(1).Value
    End Function


    Protected Shared Function CheckDateExists(ByVal employeeID As String, ByVal FromDate As Date, ByVal ToDate As Date) As String
        Dim ds As New DataSet
        Dim sqlParam(3) As SqlParameter

        sqlParam(0) = New SqlParameter("@from", SqlDbType.DateTime)
        sqlParam(0).Value = FromDate

        sqlParam(1) = New SqlParameter("@to", SqlDbType.DateTime)
        sqlParam(1).Value = ToDate

        sqlParam(2) = New SqlParameter("@employeeID", SqlDbType.VarChar, 20)
        sqlParam(2).Value = employeeID

        sqlParam(3) = New SqlParameter("@DateExists", SqlDbType.Bit)
        sqlParam(3).Direction = ParameterDirection.Output

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_ConcurCheckDateExists", sqlParam)
        Return sqlParam(3).Value
    End Function


    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If FileUpload1.HasFile Then
            Dim FileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim Extension As String = Path.GetExtension(FileUpload1.PostedFile.FileName)
            Dim FolderPath As String = ConfigurationManager.AppSettings("ConcurImportDataFolderPath")

            Dim isHDR As String
            Dim pStr As String = ""
            If CheckBox1.Checked = True Then
                isHDR = "Yes"
            Else
                isHDR = "No"
            End If

            Dim FileName2 As String = "CONCUR_" + Date.Now.ToString("yyyy-MM-dd-HHmmssfff") + Extension

            Dim FilePath As String = Server.MapPath(FolderPath + FileName2)

            If Extension = ".xls" Or Extension = ".xlsx" Then

                FileUpload1.SaveAs(FilePath)

                Try
                    pStr = "Import_To_Grid('" & FileName & "', '" & Extension & "', '" & isHDR & "')"
                    InsertSystemLogs("Concur Data Import", "Load Data", 1, pStr, Session("User"))
                    Import_To_Grid(FilePath, Extension, isHDR)
                Catch ex As Exception
                    MessageLabel.Style("color") = "red"
                    MessageLabel.Text = ex.Message.ToString()
                    btnConfirmUpload.Visible = False
                    InsertSystemLogs("Concur Data Import", "Load Data", 2, ex.Message.ToString(), Session("User"))
                End Try

            Else
                MessageLabel.Style("color") = "red"
                MessageLabel.Text = "Invalid File Extension Uploaded (" & Extension & ")"
                InsertSystemLogs("Concur Data Import", "Load Data", 2, "Invalid file extension uploaded (" & Extension & ")", Session("User"))

            End If


        Else

            MessageLabel.Style("color") = "red"
            MessageLabel.Text = "No file to import!"
            InsertSystemLogs("Concur Data Import", "Load Data", 2, "No file to import!", Session("User"))
            btnConfirmUpload.Visible = False
            GridView1.Dispose()
            GridView1.DataSource = ""
            GridView1.DataBind()

            FunctionLabel.Visible = False
            MessagePanel.Visible = False

        End If
    End Sub

    Private Sub Import_To_Grid(ByVal FilePath As String, ByVal Extension As String, ByVal isHDR As String)
        Dim conStr As String = ""
        Select Case Extension
            Case ".xls"
                'Excel 97-03 
                conStr = ConfigurationManager.ConnectionStrings("Excel03ConString").ConnectionString
                Exit Select
            Case ".xlsx"
                'Excel 07 
                conStr = ConfigurationManager.ConnectionStrings("Excel07ConString").ConnectionString
                Exit Select
        End Select

        conStr = String.Format(conStr, FilePath, isHDR)

        Dim connExcel As New OleDbConnection(conStr)
        Dim cmdExcel As New OleDbCommand()
        Dim oda As New OleDbDataAdapter()
        Dim dt As New DataTable()

        cmdExcel.Connection = connExcel

        'Get the name of First Sheet 
        connExcel.Open()
        Dim dtExcelSchema As DataTable
        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
        Dim SheetName As String = dtExcelSchema.Rows(0)("TABLE_NAME").ToString()
        connExcel.Close()

        'Read Data from First Sheet 
        connExcel.Open()
        cmdExcel.CommandText = "SELECT * From [" & SheetName & "]"
        oda.SelectCommand = cmdExcel
        oda.Fill(dt)
        connExcel.Close()

        'Bind Data to GridView 
        GridView1.DataSource = dt
        GridView1.DataBind()

        MessageLabel.Style("color") = "green"
        MessageLabel.Text = "<strong>" & GridView1.Rows.Count.ToString & " record(s)</strong> loaded from excel file.<br /><br />Please verify data if there are no errors and click ""Import Concur Data"" button to import data to Travel Order module." 'from <strong>" & Path.GetFileName(FilePath) & "</strong>"
        Dim RecCount As String = GridView1.Rows.Count.ToString & " record(s) loaded from excel file"
        InsertSystemLogs("Concur Data Import", "Load Data", 1, RecCount, Session("User"))

        Try
            Dim ErrorCount As Integer = 0
            Dim addstr1 As String = ""
            Dim addstr2 As String = ""
            Dim addstr3 As String = ""
            Dim addstr4 As String = ""
            Dim addstr5 As String = ""

            For i As Integer = 0 To GridView1.Rows.Count - 1
                Dim row As GridViewRow = GridView1.Rows(i)
                Dim EmployeeID As String = GridView1.Rows(i).Cells(0).Text
                Dim Employee As String = GridView1.Rows(i).Cells(1).Text
                Dim RequestName As String = GridView1.Rows(i).Cells(2).Text
                Dim StartDate As String = GridView1.Rows(i).Cells(3).Text
                Dim EndDate As String = GridView1.Rows(i).Cells(4).Text
                Dim RequestID As String = GridView1.Rows(i).Cells(5).Text
                Dim ApprovalStatus As String = GridView1.Rows(i).Cells(6).Text
                Dim ApprovalDateTime As String = GridView1.Rows(i).Cells(7).Text
                Dim Purpose As String = GridView1.Rows(i).Cells(8).Text


                If EmployeeID = "&nbsp;" Then
                    ErrorCount = ErrorCount + 1
                End If

                If Employee = "&nbsp;" Then
                    ErrorCount = ErrorCount + 1
                End If

                If RequestName = "&nbsp;" Then
                    ErrorCount = ErrorCount + 1
                End If

                If StartDate = "&nbsp;" Then
                    ErrorCount = ErrorCount + 1
                End If

                If EndDate = "&nbsp;" Then
                    ErrorCount = ErrorCount + 1
                End If

                If StartDate <> "&nbsp;" And EndDate <> "&nbsp;" Then
                    If CDate(StartDate) > CDate(EndDate) Then
                        ErrorCount = ErrorCount + 1
                        addstr1 = "<div class=""alert warning"">Date Error: Start Date should not be greater than End Date</div>"
                    End If
                End If

                If ApprovalStatus <> "Approved" Then
                    ErrorCount = ErrorCount + 1
                    addstr2 = "<div class=""alert warning"">Approval Status Error: '" & ApprovalStatus & "' -  System only accepts Approved status!</div>"
                End If


                If RequestID = "&nbsp;" Then
                    ErrorCount = ErrorCount + 1
                End If

                If Purpose = "&nbsp;" Then
                    ErrorCount = ErrorCount + 1
                End If

                If CheckConcurRefNo(RequestID) = "Yes" Then
                    ErrorCount = ErrorCount + 1
                    addstr3 = "<div class=""alert warning"">Data Error: Reference No. exists in the database!</div>"
                End If

                If CheckDateExists(EmployeeID, StartDate, EndDate) = True Then
                    ErrorCount = ErrorCount + 1
                    addstr5 = "<div class=""alert warning"">Data Error: Employee has previous application filed with same date.</div>"
                End If

                If ApprovalStatus = "&nbsp;" Then
                    ErrorCount = ErrorCount + 1
                End If

                If ApprovalDateTime = "&nbsp;" Then
                    ErrorCount = ErrorCount + 1
                End If

            Next

            If GridView1.Rows.Count = 0 Then
                ErrorCount = ErrorCount + 1
                addstr4 = "<div class=""alert warning"">Data Error: No Data to Import!</div>"
            End If

            If ErrorCount > 0 Then
                FunctionLabel.Visible = True
                FunctionLabel.Style("color") = "Red"
                FunctionLabel.Text = "<div class=""alert warning"">Data Error: Blank or Invalid Data detected! Please cleanup data in excel file.</div>" & addstr1 & addstr2 & addstr3 & addstr4 & addstr5 & "<br>"
                btnConfirmUpload.Visible = False
                InsertSystemLogs("Concur Data Import", "Load Data", 2, "<li>Blank /invalid data detected!</li>" & addstr1 & addstr2 & addstr3 & addstr4 & addstr5, Session("User"))
            Else
                FunctionLabel.Text = ""
                FunctionLabel.Visible = False
                FunctionLabel.Style("color") = ""
                btnConfirmUpload.Visible = True
            End If


        Catch ex As Exception
            FunctionLabel.Style("color") = "Red"
            FunctionLabel.Text = "Error: Excel format invalid!" & ex.Message

            btnConfirmUpload.Visible = False
            InsertSystemLogs("Concur Data Import", "Load Data", 2, "Excel format invalid!", Session("User"))
        End Try


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnConfirmUpload.Visible = False
        End If
    End Sub

    Protected Sub btnConfirmUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConfirmUpload.Click

        Dim str As String = ""
        Dim pstr As String = "Data Imported: "
        Try
            For i As Integer = 0 To GridView1.Rows.Count - 1
                Dim row As GridViewRow = GridView1.Rows(i)
                Dim EmployeeID As String = GridView1.Rows(i).Cells(0).Text
                Dim Employee As String = GridView1.Rows(i).Cells(1).Text
                Dim RequestName As String = GridView1.Rows(i).Cells(2).Text
                Dim StartDate As Date = GridView1.Rows(i).Cells(3).Text
                Dim EndDate As Date = GridView1.Rows(i).Cells(4).Text
                Dim RequestID As String = GridView1.Rows(i).Cells(5).Text
                Dim ApprovalStatus As String = GridView1.Rows(i).Cells(6).Text
                Dim ApprovalDateTime As Date = GridView1.Rows(i).Cells(7).Text
                Dim Purpose As String = GridView1.Rows(i).Cells(8).Text

                pstr = pstr + "('" + EmployeeID + "','" + Employee + "','" + Purpose + "','" + StartDate.ToString + "','" + EndDate.ToString + "','" + RequestID + "','" + ApprovalStatus + "')<br/>"
                spWeb_Insert_OB_Header_Concur(RequestID, EmployeeID, Purpose, StartDate, EndDate, EmployeeID, ApprovalStatus, ApprovalDateTime)
            Next
            InsertSystemLogs("Concur Data Import", "Import Data", 1, pstr, Session("User"))
            MessageLabel.Text = "Concur Data Imported Successfully!"
        Catch ex As Exception
            str = ex.Message '& Err.Number

            InsertSystemLogs("Concur Data Import", "Import Data", 2, str, Session("User"))
            MessageLabel.ForeColor = Drawing.Color.Red
            If Err.Number = 5 Then
                MessageLabel.Style("Color") = "red"
                MessageLabel.Text = "Error Data Import: Concur Reference Number already exists in the database."
            Else
                MessageLabel.Style("Color") = "red"
                MessageLabel.Text = "Error Data Import: " & str & "(Please view system log)"
                FunctionLabel.Text = str
            End If

        End Try

        MessagePanel.Visible = True
        FunctionLabel.Visible = True
        'FunctionLabel.Text = str
        btnConfirmUpload.Visible = False
        GridView1.Dispose()
        GridView1.DataSource = ""
        GridView1.DataBind()


    End Sub



    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            
            If e.Row.Cells(0).Text = "&nbsp;" Then
                e.Row.Cells(0).Style("background-color") = "#DD6153"
            End If
            If e.Row.Cells(1).Text = "&nbsp;" Then
                e.Row.Cells(1).Style("background-color") = "#DD6153"
            End If
            If e.Row.Cells(2).Text = "&nbsp;" Then
                e.Row.Cells(2).Style("background-color") = "#DD6153"
            End If
            If e.Row.Cells(3).Text = "&nbsp;" Then
                e.Row.Cells(3).Style("background-color") = "#DD6153"
            End If
            If e.Row.Cells(4).Text = "&nbsp;" Then
                e.Row.Cells(4).Style("background-color") = "#DD6153"
            End If
            If e.Row.Cells(5).Text = "&nbsp;" Then
                e.Row.Cells(5).Style("background-color") = "#DD6153"
            End If

            If IsDate(e.Row.Cells(3).Text) And IsDate(e.Row.Cells(4).Text) Then
                If CDate(e.Row.Cells(3).Text) > CDate(e.Row.Cells(4).Text) Then
                    e.Row.Cells(3).ForeColor = Drawing.Color.Red
                    e.Row.Cells(4).ForeColor = Drawing.Color.Red
                End If
            End If

            If CheckConcurRefNo(e.Row.Cells(5).Text) = "Yes" Then
                e.Row.Cells(5).ForeColor = Drawing.Color.Red
            End If

            If CheckDateExists(e.Row.Cells(0).Text, e.Row.Cells(3).Text, e.Row.Cells(4).Text) = True Then
                e.Row.ForeColor = Drawing.Color.Red
            End If

            e.Row.Cells(3).Text = Convert.ToDateTime(e.Row.Cells(3).Text).ToString("d")
            e.Row.Cells(4).Text = Convert.ToDateTime(e.Row.Cells(4).Text).ToString("d")

            If e.Row.Cells(6).Text <> "Approved" Then
                e.Row.Cells(6).ForeColor = Drawing.Color.Red
            End If

            If e.Row.Cells(7).Text = "&nbsp;" Then
                e.Row.Cells(7).Style("background-color") = "#DD6153"
            End If

            If e.Row.Cells(8).Text = "&nbsp;" Then
                e.Row.Cells(8).Style("background-color") = "#DD6153"
            End If

        End If
    End Sub
End Class
