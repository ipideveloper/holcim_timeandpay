Imports System.Data

Partial Class cancel_app
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
    Private cls_email As New cls_Email_Notifications

#End Region

#Region "Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Request.IsAuthenticated Then
        '    Response.Redirect("Default.aspx")
        'End If
        If Session.IsNewSession Then
            UserMsgBox("Session Expired!")
            Response.Redirect("Default.aspx")
        End If

        Try
            If Not Page.IsPostBack Then
                Initialize()
            End If
            btn_cancel.BackColor = Drawing.Color.Aqua
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Try
            If Left(TextBox1.Text, 2) = "91" Then
                UserMsgBox("Cannot cancel DTR Applications!")
                Exit Sub
            End If

            If TextBox1.Text = "" Then
                UserMsgBox("Enter Ref. No.")
                Exit Sub
            Else


                Dim n_result As Integer = ws.Cancel_Application(TextBox1.Text, Current_User.Employee_ID)

                If n_result = 1 Then
                    send_notifications(TextBox1.Text, ws.Get_users_ID_byRefno(TextBox1.Text), Current_User.LastName & ", " & Current_User.FirstName & " " & Current_User.Middle_Name, " has cancelled")
                    Dim ds As DataSet = ws.Get_Application_Details(TextBox1.Text)
                    'GridView1.DataSource = Nothing
                    GridView1.DataSource = ds
                    GridView1.DataBind()
                    UserMsgBox("Successfully Cancelled!")
                ElseIf n_result = 2 Then
                    UserMsgBox("Application is already cancelled!")
                ElseIf n_result = 3 Then
                    UserMsgBox("Cannot cancel this application because employee is not from this sector!")
                ElseIf n_result = 5 Then
                    UserMsgBox("Cannot cancel this application because it is already processed!")
                Else
                    UserMsgBox("Invalid Reference No.")
                End If
            End If
            btn_cancel.Enabled = False
            btn_cancel.BackColor = Drawing.Color.Aqua
            btn_view.BackColor = Drawing.Color.Red



        Catch ex As Exception
            UserMsgBox(ex.Message)
        End Try
    End Sub

#End Region

#Region "Initialization"

    Private Sub Initialize()
        ViewState("Employee_ID") = Session("Employee_ID")
        btn_cancel.Focus()

    End Sub

#End Region

#Region "Validation"



#End Region

#Region "Insert"



#End Region

#Region "Update"



#End Region

#Region "Retrieve"


    Protected Sub btnview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_view.Click
        
        btn_cancel.Enabled = False
        Label5.Visible = False
        'Try
        If TextBox1.Text = "" Then
            UserMsgBox("Enter Ref. No.")
            Exit Sub
        Else
            'MsgBox(Current_User.Employee_Position)
            'Exit Sub
            If Current_User.Employee_Position = "HR SPECIALIST" Or Current_User.Employee_Position = "HR OFFICER - OTHER BUSINESSES" Or Current_User.Employee_Position = "HR OFFICER - RMX" Or Current_User.Employee_Position = "HR OFFICER - LUGAIT" Or Current_User.Employee_Position = "HR OFFICER - LUGAIT" Or Current_User.Employee_Position = "HR OFFICER - LA UNION" Or Current_User.Employee_Position = "HR OFFICER - HEAD OFFICE" Or Current_User.Employee_Position = "HR OFFICER - DAVAO" Or Current_User.Employee_Position = "HR OFFICER - BULACAN" Or Current_User.Employee_Position = "HR SPECIALIST - PAYROLL SERVICES" Or Current_User.Employee_Position = "HR PAYROLL SPECIALIST (NO TO)" Then
                Dim n_result As Integer = ws.Check_If_Available_For_Cancellation(TextBox1.Text, Current_User.Employee_ID)
                If n_result > 3 Then
                    GridView1.DataSource = Nothing
                    GridView1.DataBind()
                    UserMsgBox("Invalid Reference No.")
                Else
                    Dim ds As DataSet = ws.Get_Application_Details(TextBox1.Text)
                    GridView1.DataSource = ds
                    GridView1.DataBind()
                End If

                If n_result = 1 Then
                    btn_cancel.Enabled = True
                    Label5.Visible = True
                    btn_cancel.BackColor = Drawing.Color.Red
                ElseIf n_result = 2 Then
                    UserMsgBox("Application is already cancelled!")
                ElseIf n_result = 3 Then
                    UserMsgBox("Cannot cancel this application because employee is not from this sector!")
                ElseIf n_result = 5 Then
                    UserMsgBox("Cannot cancel this application because it is already processed!")
                End If
            Else
                'Added by Andrew 11-17-2011 Start
                Dim n_result As Integer = ws.Check_If_Available_For_Cancellation2(TextBox1.Text, Current_User.Employee_ID)
                If n_result > 3 Then
                    GridView1.DataSource = Nothing
                    GridView1.DataBind()
                    UserMsgBox("Invalid Reference No.")
                Else
                    Dim ds As DataSet = ws.Get_Application_Details(TextBox1.Text)
                    GridView1.DataSource = ds
                    GridView1.DataBind()
                End If

                If n_result = 1 Then
                    btn_cancel.Enabled = True
                    Label5.Visible = True
                    btn_cancel.BackColor = Drawing.Color.Red
                ElseIf n_result = 2 Then
                    UserMsgBox("Application is already cancelled!")
                ElseIf n_result = 3 Then
                    UserMsgBox("Cannot cancel this application because employee is not from this sector!")
                ElseIf n_result = 5 Then
                    UserMsgBox("Cannot cancel this application because it is already processed!")
                End If
                'Added by Andrew 11-17-2011 End
            End If
        End If

            'Catch ex As Exception
            '    UserMsgBox(ex.Message)

            'End Try


    End Sub

#End Region

#Region "Delete"

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

#End Region

#Region "Manage"

    Private Sub send_notifications(ByVal ref_no As String, ByVal employee_id As String, ByVal employee_name As String, ByVal msg As String)
        Dim body As String
        Dim recipients As String = "", user_email As String
        body = employee_name & msg & " the application, please see Ref. " & ref_no & " <br><br><br><br> This is a system-generated message.  Do not reply to this message."
        Dim ds As DataSet
        ds = ws.Get_Approvers_Email(employee_id, "%")
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim dr As DataRow = ds.Tables(0).Rows(i)
            If recipients = "" Then
                recipients = dr("email")
            Else
                recipients = recipients & ", " & dr("email")
            End If
        Next
        user_email = ws.Get_users_Email_byRefno(ref_no)
        If recipients = "" Then
            recipients = user_email
        Else
            recipients = recipients & ", " & user_email
        End If

        cls_email.SendEmail(recipients, body, employee_name)
    End Sub


#End Region


End Class
