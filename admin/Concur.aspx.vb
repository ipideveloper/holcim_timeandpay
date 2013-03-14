
Imports System.Data
Imports System.Data.SqlClient
Imports HolcimDbClass
Imports Telerik.Web.UI


Partial Class admin_Concur
    Inherits System.Web.UI.Page

    Protected Sub UpdateConcurTag(ByVal ID As String)
        Dim Conn As New SqlConnection(ConnectionString)
        Dim Cmd As New SqlCommand("spWeb_Update_ConcurTag", Conn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim par1 As SqlParameter
        par1 = Cmd.Parameters.AddWithValue("@ID", ID)
        par1.Direction = ParameterDirection.Input
        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            Cmd.ExecuteNonQuery()
            Conn.Close()
        Catch ex As Exception
            Conn.Close()
        End Try
    End Sub

    Protected Sub ShowCheckedNodes()

        Dim Conn As New SqlConnection(ConnectionString)
        Dim Cmd As New SqlCommand("DELETE FROM z_ob_concur", Conn)
        Cmd.CommandType = CommandType.Text

        Try
            If Conn.State = ConnectionState.Closed Then
                Conn.Open()
            End If
            Cmd.ExecuteNonQuery()
            ' InsertSystemLogs("Concur Travel Order Settings", "Save Settings", 1, "Travel Order module enabled to all", "admin")

            Conn.Close()
        Catch ex As Exception
            Label1.Text = ex.Message.ToString
            Conn.Close()
        End Try


    End Sub

    Protected Sub RadTreeView1_NodeDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles RadTreeView1.NodeDataBound
        Dim IsCheck As Boolean = (TryCast(e.Node.DataItem, DataRowView))("concur_tag").ToString()
        If IsCheck = True Then
            e.Node.Checked = True
        Else
            e.Node.Checked = False
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        ShowCheckedNodes()
        Dim str As String = ""
        Dim pstr As String = ""
        For Each node As RadTreeNode In RadTreeView1.CheckedNodes
            Dim ID As String = node.Value.ToString
            Try
                pstr = pstr + "" & ID & " "
                UpdateConcurTag(ID)
            Catch ex As Exception
                str = str + ex.Message
                InsertSystemLogs("Concur Travel Order Settings", "Save Settings", 2, str, Session("User"))
            End Try

        Next
        'Label1.Text = str
        If RTrim(pstr) <> "" Then
            InsertSystemLogs("Concur Travel Order Settings", "Save Settings", 1, "Travel Order module disabled to (" & pstr & ")", Session("User"))
            SuccessMessage.Visible = True
            'MessageLabel.Text = "Settings Saved!"
        Else
            InsertSystemLogs("Concur Travel Order Settings", "Save Settings", 1, "Travel Order module enabled to all.", Session("User"))
            SuccessMessage.Visible = True
            ' MessageLabel.Text = "Settings Saved!"
        End If

        'Response.Redirect("Concur.aspx")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Response.Redirect("Concur.aspx")
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        If LinkButton1.Text = "Expand All" Then
            RadTreeView1.ExpandAllNodes()
            LinkButton1.Text = "Collapse"
        Else
            Response.Redirect("Concur.aspx")
        End If

    End Sub

    

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = Session("organization")
    End Sub
End Class
