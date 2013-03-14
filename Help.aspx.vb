
Partial Class Default2
    Inherits System.Web.UI.Page

    'Private Sub bind_dates2()
    '    Dim dt As New DataTable
    '    Dim dr As DataRow

    '    gv_dates.DataSource = ws.Get_Leave_Details_Dates("")
    '    gv_dates.DataBind()


    '    dt.Columns.Add(New DataColumn("date"))
    '    dt.Columns.Add(New DataColumn("date_status"))
    '    dt.Columns.Add(New DataColumn("half_day"))
    '    dt.Columns.Add(New DataColumn("first_half"))
    '    dt.Columns.Add(New DataColumn("second_half"))
    '    dt.Columns.Add(New DataColumn("chk_holiday"))

    '    Dim numInterval As Integer = DateDiff(DateInterval.Day, Convert.ToDateTime(txtStart.Text), Convert.ToDateTime(txtTo.Text))
    '    Dim vDate As Date = txtStart.Text
    '    dr = dt.NewRow()
    '    dr("date") = txtStart.Text
    '    dr("half_Day") = False
    '    dr("first_half") = False
    '    dr("second_half") = False
    '    dr("chk_holiday") = False


    '    If Calendar Then
    '        dr("date_status") = "-"
    '    Else
    '        If ws.Validate_Restday(Current_User.Employee_ID, txtStart.Text) = True Then
    '            dr("date_status") = "Restday"
    '        Else
    '            dr("date_status") = "-"
    '        End If
    '    End If


    '    dt.Rows.Add(dr)
    '    Dim allrestday As Boolean = True

    '    For i As Integer = 1 To numInterval

    '        Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDate)
    '        dr = dt.NewRow()
    '        dr("date") = Format(vNewDate, "dd-MMM-yyyy")
    '        dr("half_Day") = False
    '        dr("first_half") = False
    '        dr("second_half") = False
    '        dr("chk_holiday") = False

    '        If Calendar Then
    '            dr("date_status") = "-"
    '        Else
    '            If ws.Validate_Restday(Current_User.Employee_ID, vNewDate) = True Then
    '                dr("date_status") = "Restday"
    '            Else
    '                dr("date_status") = "-"
    '                allrestday = False
    '            End If
    '        End If


    '        dt.Rows.Add(dr)
    '        vDate = vNewDate
    '    Next


    '    gv_dates.DataSource = dt
    '    gv_dates.DataBind()


    '    Dim total_days As Double = numInterval + 1
    '    Dim vDates As Date = txtStart.Text
    '    vDates = DateAdd(DateInterval.Day, -1, vDates)
    '    Dim daysAvailable As Decimal = CType(txt_credit.Text, Decimal)

    '    For i As Integer = 0 To gv_dates.Rows.Count - 1

    '        Dim row As GridViewRow = gv_dates.Rows(i)
    '        Dim vNewDate As Date = DateAdd(DateInterval.Day, 1, vDates)

    '        Dim _holiday As Boolean = ws.Validate_Holiday(Current_User.Employee_ID, vNewDate)
    '        Dim holiday As CheckBox = CType(row.FindControl("check_holiday"), CheckBox)

    '        If _holiday = True Then
    '            holiday.Checked = True
    '        Else
    '            holiday.Checked = False
    '        End If

    '        Dim half As CheckBox = CType(row.FindControl("chk_half"), CheckBox)

    '        If Not gv_dates.Rows(i).Cells(4).Text = "-" Then
    '            total_days = total_days - 1
    '            half.Enabled = False
    '        ElseIf _holiday = True Then
    '            total_days = total_days - 1
    '            'half.Enabled = False
    '        End If


    '        If gv_dates.Rows(i).Cells(4).Text = "-" Then
    '            allrestday = False
    '        End If

    '        If daysAvailable = 0.5 Then
    '            half.Enabled = False
    '        End If

    '        vDates = vNewDate
    '    Next


    '    lbl_total_days.Text = "Total Days Applied = " & total_days
    '    lbl_days_hidden.Text = total_days

    '    If allrestday = True Then
    '        btnSubmit.Visible = False
    '    Else
    '        btnSubmit.Visible = True
    '    End If

    'End Sub
End Class
