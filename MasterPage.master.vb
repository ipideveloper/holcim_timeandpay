Imports System.Data
Imports HolcimDbClass
Imports System.Data.SqlClient


Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Public Function Validate_FinalApprover(ByVal employee_id As String) As Boolean
        Dim ds As New DataSet
        Dim sqlParam(0) As SqlParameter

        sqlParam(0) = New SqlParameter("@employee_id", SqlDbType.VarChar, 15)
        sqlParam(0).Value = employee_id

        ds = ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Validate_FinalApprover", sqlParam)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


#Region "Variables"

    Private ws As New localhost.Service

#End Region

#Region "Properties"

    Public ReadOnly Property Current_User() As Cls_UserProperty
        Get
            Return New Cls_UserProperty(ViewState("Employee_ID"))
        End Get
    End Property

#End Region

#Region "Events"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Initialize()
                lblHeaderTitle.Text = IIf(Session("HeaderTitle") = Nothing, "My Requests Status", Session("HeaderTitle"))
                manage_btn_font(Session("btn_name"))

                If DateTime.Now() >= DateValue("2011/11/26") And DateTime.Now() <= DateValue("2011/11/30") Then
                    If Session("Branch_ID") = "P100" Then
                        btnleaveapplications.Visible = False 'original code
                        'btnleaveapplications.Visible = True
                    Else
                        btnleaveapplications.Visible = True
                    End If
                End If

                'Label5.Text = CheckIsConcur(Session("Organization_ID")).ToString

                'Concur
                

            End If
        Catch ex As Exception
        End Try

        If CheckIsConcur(Session("Organization_ID")) = "Yes" Then
            btnob.Visible = False

        End If

        'DTR Application Button
        btnDTRapplication.Visible = True

    End Sub

    Private Sub ListControl(ByVal oControl As Control)
        Dim xarray As New ArrayList
        Dim Ctrl As Control
        Dim sPath As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
        Dim oInfo As System.IO.FileInfo = New System.IO.FileInfo(sPath)
        Dim sRet As String = oInfo.Name
        For Each Ctrl In oControl.Controls

            If TypeOf Ctrl Is Button Then
                If Not IsDBNull(Ctrl.ID) Then
                    ws.Insert_Module(Ctrl.ID, sRet)
                End If
            End If
            If Ctrl.HasControls Then
                ListControl(Ctrl)
            End If
        Next

    End Sub
    Protected Sub btnleaveapplications_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnleaveapplications.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("Leave Application", "leave_app_header.aspx", "btnleaveapplications")

    End Sub

    Protected Sub btnlogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnlogout.Click
        ws.ActivateSession(Current_User.Employee_ID, False)
        Response.Redirect("Default.aspx")
    End Sub

    Protected Sub btnovertime_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnovertime.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("Overtime/ESD Application", "ot_header.aspx", "btnovertime")
    End Sub

    Protected Sub btnmydtr_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("My DTR", "my_dtr.aspx", "btnmydtr")
    End Sub

    Protected Sub btnapprovals_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("My Approvals", "my_approvals.aspx", "btnapprovals")
    End Sub

    Protected Sub btnmypassword_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("Change My Password", "change_password.aspx", "btnmypassword")
    End Sub

    Protected Sub btnempdtr_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("My Employee's DTR", "my_employee_dtr.aspx", "btnempdtr")
    End Sub

    Protected Sub btntimecorrections_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btntimecorrections.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("On Call Application", "oncall.aspx", "btntimecorrections")
    End Sub

    Protected Sub btnob_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnob.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("Travel Order Application", "ob_header.aspx", "btnob")
    End Sub

    'Protected Sub btnShiftSchedules_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShiftSchedules.Click
    '    GeT_module("Shift Schedules Application", "shift_apply.aspx", "btnShiftSchedules")
    'End Sub

    Protected Sub btnDTRapplication_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDTRapplication.Click
        GeT_module("DTR Application", "dtr_header.aspx", "btnDTRapplication")
    End Sub

    Protected Sub btnPayslip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPayslip.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("My Payslip", "payslip.aspx", "btnPayslip")
    End Sub

    Protected Sub btnuseraccess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnuseraccess.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("User Access Maintenance", "access_maintenance.aspx", "btnuseraccess")
    End Sub

    Protected Sub btn_schedules_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("My Schedules", "my_schedules.aspx", "btn_schedules")
    End Sub

    Protected Sub btnMyAppStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMyAppStatus.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        Session("MyButton") = "gv_ob"
        GeT_module("My Requests Status", "my_app_status.aspx", "btnMyAppStatus")
    End Sub

    Protected Sub btn_leave_balances_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_leave_balances.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("My Leave Balances", "leave_balances.aspx", "btn_leave_balances")
    End Sub

    Protected Sub btn_cancel_app_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_cancel_app.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("Cancel Application", "cancel_app.aspx", "btn_cancel_app")
    End Sub


    Protected Sub btn_shift_approval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_shift_approval.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("My Shift Sched Approval", "my_shift_approval_header.aspx", "btn_shift_approval")
    End Sub

    Protected Sub btnShiftUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShiftUpload.Click
        ws.ActivateSession(Current_User.Employee_ID, True)
        GeT_module("Shift Schedule Update", "UploadShift3.aspx", "btnShiftUpload")
    End Sub
#End Region

#Region "Initialization"

    Private Sub Initialize()
        If Session("Log") = Nothing Then
            Session("expired") = "Yes"
            Session("Employee_ID") = Nothing
            Response.Redirect("Default.aspx")
        Else
            ViewState("Employee_ID") = Session("Employee_ID")
            get_current_userlog(ViewState("Employee_ID"))
            validate_module_access(ViewState("Employee_ID"))

        End If
        Disable_My_Control(Me, ViewState("Employee_ID"))
    End Sub


#End Region

#Region "Insert"

#End Region

#Region "Update"

#End Region

#Region "Retrieve"

    Private Sub get_current_userlog(ByVal pemployee_id As String)
        Label1.Text = Current_User.Employee_ID
        Label2.Text = Current_User.FirstName
        Label3.Text = Current_User.Middle_Name
        Label4.Text = Current_User.LastName
        Label5.Text = Current_User.Branch_id

    End Sub

#End Region

#Region "Delete"


#End Region

#Region "Manage"

    Private Sub GeT_module(ByVal title As String, ByVal strmodule As String, ByVal btn_name As String)
        Session("btn_name") = btn_name
        Session("Employee_ID") = ViewState("Employee_ID")
        Session("HeaderTitle") = title
        Response.Redirect(strmodule)
    End Sub

    Private Sub Manage_buttons()
        btnleaveapplications.Font.Bold = False
        btnovertime.Font.Bold = False
        btnob.Font.Bold = False
        btnShiftUpload.Font.Bold = False
        btntimecorrections.Font.Bold = False
        btnMyAppStatus.Font.Bold = False
        btnapprovals.Font.Bold = False
        btnmydtr.Font.Bold = False
        btnempdtr.Font.Bold = False
        btnPayslip.Font.Bold = False
        btnmypassword.Font.Bold = False
        btnuseraccess.Font.Bold = False
        btn_schedules.Font.Bold = False
        btn_leave_balances.Font.Bold = False

        btn_leave_balances.ForeColor = Drawing.Color.Black
        btnleaveapplications.ForeColor = Drawing.Color.Black
        btnovertime.ForeColor = Drawing.Color.Black
        btnob.ForeColor = Drawing.Color.Black
        btnShiftUpload.ForeColor = Drawing.Color.Black
        btntimecorrections.ForeColor = Drawing.Color.Black
        btnMyAppStatus.ForeColor = Drawing.Color.Black
        btnapprovals.ForeColor = Drawing.Color.Black
        btnmydtr.ForeColor = Drawing.Color.Black
        btnempdtr.ForeColor = Drawing.Color.Black
        btnPayslip.ForeColor = Drawing.Color.Black
        btnmypassword.ForeColor = Drawing.Color.Black
        btnuseraccess.ForeColor = Drawing.Color.Black
        btn_schedules.ForeColor = Drawing.Color.Black
    End Sub

    Private Sub manage_btn_font(ByVal str_btn As String)
        If str_btn = "" Then
            str_btn = "btnMyAppStatus"
        End If
        Dim btn As Button = CType(Me.FindControl(str_btn), Button)
        btn.BackColor = Drawing.Color.Red
        btn.ForeColor = Drawing.Color.White
    End Sub

    Private Sub validate_module_access(ByVal employee_id As String)
        If ws.Validate_PlannerOrFinal(employee_id) = False Then
            btnovertime.Visible = False
            btnob.Visible = False
            btnShiftUpload.Visible = False
            btntimecorrections.Visible = False
            btnapprovals.Visible = False

            If CType(ws.Validate_UserAccess(employee_id).Tables(0).Rows(0).Item("ot"), Boolean) = True Then
                btnovertime.Visible = True
            End If
            If CType(ws.Validate_UserAccess(employee_id).Tables(0).Rows(0).Item("ob"), Boolean) = True Then
                btnob.Visible = True
            End If
        Else
            btn_shift_approval.Visible = True

            btntimecorrections.Visible = True
            btnShiftUpload.Visible = True
            If Validate_FinalApprover(employee_id) = True Then
                btn_cancel_app.Visible = True
            Else
                btn_cancel_app.Visible = False
                'btnShiftUpload.Visible = False
            End If
        End If


        If ws.Validate_ESD(employee_id) = True Then
            btnovertime.Visible = ws.Validate_withESD(employee_id)
        End If


        If CType(ws.Validate_UserAccess(employee_id).Tables(0).Rows(0).Item("sa"), Boolean) = True Then
            btnuseraccess.Visible = True
            ListControl(Me)
        End If

        'Show User Access/Cancel Application if HR Specialist
        If ws.Validate_HR_Specialist(Current_User.Employee_ID) = True Then
            btnuseraccess.Visible = True
            btn_cancel_app.Visible = True
        End If

        If Validate_FinalApprover(employee_id) = True Then
            btn_cancel_app.Visible = True
        Else
            'btn_cancel_app.Visible = False
            ''btnShiftUpload.Visible = False
        End If


    End Sub

#End Region

    Public Function Disable_My_Control(ByVal oControl As Control, ByVal employee_id As String) As Boolean

        Dim sPath As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
        Dim oInfo As System.IO.FileInfo = New System.IO.FileInfo(sPath)
        Dim sRet As String = oInfo.Name
        Dim ds As New DataSet
        ds = ws.Get_Disable_Module(employee_id, sRet)
        Dim cmdButton As String
        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            cmdButton = ds.Tables(0).Rows(i).Item("module_id")
            DisableControl(oControl, cmdButton)
        Next
        Return True
    End Function


    Public Function DisableControl(ByVal oControl As Control, ByVal nControl As String) As Boolean

        Dim xarray As New ArrayList
        Dim Ctrl As Control
        Dim sPath As String = System.Web.HttpContext.Current.Request.Url.AbsolutePath
        Dim oInfo As System.IO.FileInfo = New System.IO.FileInfo(sPath)
        Dim sRet As String = oInfo.Name
        For Each Ctrl In oControl.Controls

            If TypeOf Ctrl Is Button Then
                If Not IsDBNull(Ctrl.ID) Then
                    If Ctrl.ID = nControl Then
                        Ctrl.Visible = False
                    End If
                End If
            End If
            If Ctrl.HasControls Then
                DisableControl(Ctrl, nControl)
            End If
        Next
        Return True
    End Function

  
End Class
