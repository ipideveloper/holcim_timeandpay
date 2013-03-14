Imports System.Data

Partial Class shift_approval
    Inherits System.Web.UI.Page

#Region "Properties"

    Public ReadOnly Property Current_User() As Cls_UserProperty
        Get
            Return New Cls_UserProperty(ViewState("Employee_ID"))
        End Get
    End Property

#End Region

#Region "Variables"

    Private ws As New localhost.Service

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Request.IsAuthenticated Then
        '    Response.Redirect("Default.aspx")
        'End If
        'If Session.IsNewSession Then
        '    UserMsgBox("Session Expired!")
        '    Response.Redirect("Default.aspx")
        'End If

        Try
            If Not Page.IsPostBack Then
                Initialize()
            End If
        Catch ex As Exception
        End Try
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"

    Private Sub get_shift_schedules(ByVal month As Integer, ByVal planner_id As String, ByVal pyear As Integer)

        ws.Get_Employee_Shift_Schedules(planner_id, month, pyear)
        GridView1.DataSource = ws.Get_Shift_Details_Temp(planner_id)
        GridView1.DataBind()
        Dim varDate As DateTime
        For i As Integer = 0 To GridView1.Rows.Count - 1
            varDate = Trim(Str(month)) + "/" + "01/" + Trim(Str(pyear))
            Do While (varDate.Month) = month

                If shift_changed(i, Day(varDate)) Then

                    GridView1.Rows(i).Cells(Day(varDate) + 1).BackColor = Drawing.Color.Orange
                End If

                'varday = Day(varDate).ToString + " - " + Mid(varDate.DayOfWeek.ToString, 1, 3)


                varDate = DateAdd(DateInterval.Day, 1, varDate)
            Loop

            'GridView4.Rows(i).Cells(10).BackColor = Drawing.Color.Orange
            'GridView4.Rows(4).Cells(10).BackColor = Drawing.Color.Orange
        Next



        Dim month_name As String = ""
        Select Case month
            Case 1
                month_name = "January"
            Case 2
                month_name = "February"
            Case 3
                month_name = "March"
            Case 4
                month_name = "April"
            Case 5
                month_name = "May"
            Case 6
                month_name = "June"
            Case 7
                month_name = "July"
            Case 8
                month_name = "August"
            Case 9
                month_name = "September"
            Case 10
                month_name = "October"
            Case 11
                month_name = "November"
            Case 12
                month_name = "December"
        End Select
        lbl_header.Text = "Shift Schedule for the month of " & month_name
    End Sub


    Function shift_changed(ByVal gvrows_count As Integer, ByVal count As String) As Boolean
        Dim row As GridViewRow = GridView1.Rows(gvrows_count)


        Dim EmpID As String = GridView1.Rows(gvrows_count).Cells(1).Text
        Dim workdate As Date = Trim(Str(Session("month"))) + "/" + Trim(count) + "/" + Trim(Str(Session("year")))
        Dim shiftchange As New DataSet
        shiftchange = Nothing
        shiftchange = ws.Get_Shift_History(EmpID, workdate)
        If shiftchange.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Delete"



#End Region

#Region "Manage"



#End Region

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


#End Region

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs)
        get_shift_schedules(Session("month"), Session("planner_id"), Session("year"))
        Timer1.Enabled = False
    End Sub
End Class
