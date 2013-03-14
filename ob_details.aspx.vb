Imports System.Data
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
'Imports CrystalDecisions.ReportAppServer.ClientDoc
'Imports CrystalDecisions.Enterprise.InfoStore

Partial Class ob_details
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
        If Session.IsNewSession Then
            UserMsgBox("Session Expired!")
            Response.Redirect("Default.aspx")
        End If


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
        get_ob_details(Request.QueryString("Ref_No"))
    End Sub

#End Region


#Region "Retrieve"

    Private Sub get_ob_details(ByVal ref_no As String)
        Try

        Dim ds As DataSet, status As String
            ds = ws.Get_OB_Details(ref_no)
            Dim drHeader As DataRow = ds.Tables(0).Rows(0)
            With drHeader
                txt_ref_no.Text = ref_no
                txt_employee_name.Text = drHeader("employee_name")
                txt_travel_type.Text = drHeader("travel_type")
                txtpurposeoftravel.Text = drHeader("purpose_of_travel")
                txt_travel_dates.Text = drHeader("travel_date")
                txtdestination.Text = drHeader("destination")
                txt_employee_id.Text = drHeader("employee_id")
                txtcontactno.Text = drHeader("contact_no")
                txtcostcenter.Text = drHeader("cost_center")
                txtposition.Text = drHeader("position")
                ' txt_accomodation.Text = drHeader("accommodation_type")
                'txtcheckin.Text = drHeader("check_in")
                'txtpreferredhotel.Text = drHeader("preferred_hotel")
                'txtcheckout.Text = drHeader("check_out")
                txt_visa_req.Text = drHeader("visa_required")
                txtcontactemergency.Text = drHeader("emergency_contact")
                txtpassport.Text = drHeader("passport_no")
                txtphone.Text = drHeader("phone_no")
                txt_accompanying.Text = drHeader("accompanying_dependents")
                txtdependentname1.Text = drHeader("dependent_name1")
                txtdepage1.Text = drHeader("dependent_age1")
                txtdeppass1.Text = drHeader("dependent_passport1")
                txtdependentname2.Text = drHeader("dependent_name2")
                txtdepage2.Text = drHeader("dependent_age2")
                txtdeppass2.Text = drHeader("dependent_passport2")

                If drHeader("ob_type") Then
                    OBType_Label.Text = "Regular OB"
                Else
                    OBType_Label.Text = "Cross Posting OB"
                End If

            End With

            GridView1.DataSource = ds.Tables(1)
            GridView1.DataBind()

            gv_lodging.DataSource = ds.Tables(2)
            gv_lodging.DataBind()

            gv_cash_advance.DataSource = ds.Tables(3)
            gv_cash_advance.DataBind()

            If gv_cash_advance.Rows.Count > 0 Then
                Dim total As Double = 0.0

                For i As Integer = 0 To gv_cash_advance.Rows.Count - 1
                    total = total + CType(gv_cash_advance.Rows(i).Cells(1).Text, Double)
                Next

                gv_cash_advance.FooterRow.Cells(0).Text = "Total = "
                gv_cash_advance.FooterRow.Cells(0).HorizontalAlign = HorizontalAlign.Right
                gv_cash_advance.FooterRow.Cells(1).Text = total

            End If

            status = drHeader("status_code")

            If status <> "2" Then
                lnk_print.Visible = False
            Else
                lnk_print.Visible = True

            End If
            Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

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

    Protected Sub lnk_print_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_print.Click
        View_PDF()
    End Sub

    Private Sub View_PDF()
        Try
            Dim strPDF As String = String.Format("{0:MMddyyhhmmss}", DateTime.Now) & ".pdf"

            ExportToPDF(Server.MapPath("rpt\rpt_ob_details.rpt"), Server.MapPath("temp\" & strPDF))

            Dim strScript As String = "<script language=JavaScript>"
            strScript &= "var wid = screen.width - 50;"
            strScript &= "var hgt = screen.height - 150;"
            strScript &= "var x = (screen.width / 2) - (wid / 2);"
            strScript &= "var y = 25;"
            strScript &= "self.open('temp/" & strPDF & " ','ReportPopup','fullscreen=no,status=yes,scrollbars=yes,resizable=yes,height=' + hgt + ',width='+ wid + ',left=' + x + ',top=25');"
            strScript &= "</script>"

            If (Not ClientScript.IsStartupScriptRegistered("clientScript")) Then
                ClientScript.RegisterStartupScript(ClientScript.GetType, "clientScript", strScript)
                'ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert ('hello')", True)
            End If
        Catch ex As Exception
            'txtdestination.Text = ex.Message
            UserMsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ExportToPDF(ByVal strReportFilePath As String, ByVal strPdfFilePath As String)
        Try
            Dim doc As New ReportDocument
            Dim filename As String = strReportFilePath

            doc.Load(filename)
            doc.SetDataSource(ws.Get_OB_Report(txt_ref_no.Text))

            'doc.ReportDefinition.ReportObjects

            Dim exportOpts As ExportOptions = doc.ExportOptions

            exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat
            exportOpts.ExportDestinationType = ExportDestinationType.DiskFile
            exportOpts.DestinationOptions = New DiskFileDestinationOptions

            Dim diskOpts As DiskFileDestinationOptions = New DiskFileDestinationOptions
            CType(doc.ExportOptions.DestinationOptions, DiskFileDestinationOptions).DiskFileName = strPdfFilePath
            doc.Export()
            doc.Close()
            doc = Nothing

        Catch ex As Exception
            '    ;txtdestination.Text = ex.Message
            UserMsgBox(ex.Message)
        End Try
    End Sub

End Class
