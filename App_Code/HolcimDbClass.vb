Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections

Public Class HolcimDbClass

    Public Shared ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("DBConnectionString")

    Public Overloads Shared Function ExecuteDataset(ByVal connectionString As String, _
                                                        ByVal commandType As CommandType, _
                                                        ByVal commandText As String, _
                                                        ByVal ParamArray commandParameters() As SqlParameter) As DataSet
        Dim cn As New SqlConnection(connectionString)
        Try
            cn.Open()
            Return ExecuteDataset(cn, commandType, commandText, commandParameters)
        Finally
            cn.Dispose()
        End Try
    End Function

    Public Overloads Shared Function ExecuteDataset(ByVal connection As SqlConnection, _
                                                       ByVal commandType As CommandType, _
                                                       ByVal commandText As String, _
                                                       ByVal ParamArray commandParameters() As SqlParameter) As DataSet
        Dim cmd As New SqlCommand
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        PrepareCommand(cmd, connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters)
        da = New SqlDataAdapter(cmd)
        da.Fill(ds)
        cmd.Parameters.Clear()
        Return ds
    End Function

    Private Shared Sub PrepareCommand(ByVal command As SqlCommand, _
                                        ByVal connection As SqlConnection, _
                                        ByVal transaction As SqlTransaction, _
                                        ByVal commandType As CommandType, _
                                        ByVal commandText As String, _
                                        ByVal commandParameters() As SqlParameter)
        If connection.State <> ConnectionState.Open Then
            connection.Open()
        End If
        command.Connection = connection
        command.CommandTimeout = 60000
        command.CommandText = commandText
        If Not (transaction Is Nothing) Then
            command.Transaction = transaction
        End If
        command.CommandType = commandType
        If Not (commandParameters Is Nothing) Then
            AttachParameters(command, commandParameters)
        End If
        Return
    End Sub

    Private Shared Sub AttachParameters(ByVal command As SqlCommand, ByVal commandParameters() As SqlParameter)
        Dim p As SqlParameter
        For Each p In commandParameters
            If p.Direction = ParameterDirection.InputOutput And p.Value Is Nothing Then
                p.Value = Nothing
            End If
            command.Parameters.Add(p)
        Next p
    End Sub

    Public Shared Function CheckIsConcur(ByVal organization_id As String) As String
        Dim ds As New DataSet
        Dim sqlParam(1) As SqlParameter

        sqlParam(0) = New SqlParameter("@organization_id", SqlDbType.VarChar, 20)
        sqlParam(0).Value = organization_id

        sqlParam(1) = New SqlParameter("@IsConcur", SqlDbType.VarChar, 5)
        sqlParam(1).Direction = ParameterDirection.Output

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_CheckIsConcur", sqlParam)
        Return sqlParam(1).Value
    End Function

    Public Shared Sub InsertSystemLogs(ByVal vModule As String, _
                                       ByVal vEvent As String, _
                                       ByVal LogType As Integer, _
                                       ByVal LogMessage As String, _
                                       ByVal Username As String)
        Dim ds As New DataSet
        Dim sqlParam(4) As SqlParameter

        sqlParam(0) = New SqlParameter("@Module", SqlDbType.VarChar, 150)
        sqlParam(0).Value = vModule

        sqlParam(1) = New SqlParameter("@Event", SqlDbType.VarChar, 50)
        sqlParam(1).Value = vEvent

        sqlParam(2) = New SqlParameter("@LogType", SqlDbType.TinyInt)
        sqlParam(2).Value = LogType

        sqlParam(3) = New SqlParameter("@LogMessage", SqlDbType.Text)
        sqlParam(3).Value = LogMessage

        sqlParam(4) = New SqlParameter("@Username", SqlDbType.VarChar, 50)
        sqlParam(4).Value = Username

        ds = HolcimDbClass.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, _
                                            "spWeb_Insert_SysManager_Logs", sqlParam)
    End Sub
End Class
