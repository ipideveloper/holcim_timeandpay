Imports Microsoft.VisualBasic
Imports System.Data

Public Class Cls_UserProperty

    Dim mEmployee_id As String
    Dim mFirstname As String
    Dim mLastname As String
    Dim mmiddle_name As String
    Dim mpassword As String
    Dim mBranch_id As String
    Dim mType As String
    Dim mSex As String
    Dim mUnionCode As String
    Dim mEmployeeStatus As String
    Dim mEmpposition As String 'Added by Andrew 11-17-2011 
    Private ws As New localhost.Service

    Public Property Employee_ID() As String
        Get
            Return CType(mEmployee_id, String)
        End Get
        Set(ByVal value As String)
            mEmployee_id = CType(value, String)
        End Set
    End Property

    Public Property Employee_Type() As String
        Get
            Return CType(mType, String)
        End Get
        Set(ByVal value As String)
            mType = CType(value, String)
        End Set
    End Property

    Public Property FirstName() As String
        Get
            Return CType(mFirstname, String)
        End Get
        Set(ByVal value As String)
            mFirstname = CType(value, String)
        End Set
    End Property

    Public Property LastName() As String
        Get
            Return CType(mLastname, String)
        End Get
        Set(ByVal value As String)
            mLastname = CType(value, String)
        End Set
    End Property

    Public Property Middle_Name() As String
        Get
            Return CType(mmiddle_name, String)
        End Get
        Set(ByVal value As String)
            mmiddle_name = CType(value, String)
        End Set
    End Property

    Public Property Password() As String
        Get
            Return CType(mpassword, String)
        End Get
        Set(ByVal value As String)
            mpassword = CType(value, String)
        End Set
    End Property

    Public Property Branch_id() As String
        Get
            Return CType(mBranch_id, String)
        End Get
        Set(ByVal value As String)
            mBranch_id = CType(value, String)
        End Set
    End Property

    Public Property Sex() As String
        Get
            Return CType(mSex, String)
        End Get
        Set(ByVal value As String)
            mSex = CType(value, String)
        End Set
    End Property

    Public Property Union_Code() As String
        Get
            Return CType(mUnionCode, String)
        End Get
        Set(ByVal value As String)
            mUnionCode = CType(value, String)
        End Set
    End Property

    Public Property Employee_Status() As String
        Get
            Return CType(mEmployeeStatus, String)
        End Get
        Set(ByVal value As String)
            mEmployeeStatus = CType(value, String)
        End Set
    End Property

    Public Property Employee_Position() As String
        Get
            Return CType(mEmpposition, String)
        End Get
        Set(ByVal value As String)
            mEmpposition = CType(value, String)
        End Set
    End Property

    Public Sub New(ByVal pemployee_id As String)
        Dim dr As DataRow = ws.Get_User_Info(pemployee_id).Tables(0).Rows(0)
        With dr
            mEmployee_id = .Item("employee_id")
            mFirstname = ArrangeWords(.Item("first_name"))
            mLastname = ArrangeWords(.Item("last_name"))
            mmiddle_name = ArrangeWords(.Item("middle_name"))
            mpassword = cls_GlobalFunction.decrypt(.Item("emp_password"), "GECOFYFRDEY")
            mBranch_id = .Item("branch_id")
            mType = .Item("employee_type")
            mSex = .Item("sex")
            mUnionCode = .Item("union_code")
            mEmployeeStatus = .Item("employee_status")
            mEmpposition = .Item("position")
        End With
    End Sub

    Public Function ArrangeWords(ByVal strInputText As String) As String
        Dim intCounter As Integer
        Dim strCompare As String
        Dim ArrangeWord As String = ""
        Dim vType As Boolean = True

        For intCounter = 1 To Len(strInputText)
            strCompare = Mid(strInputText, intCounter, 1)
            If strCompare = " " Then
                vType = False
            Else
                If vType = False Then
                    strCompare = UCase(strCompare)
                    vType = True
                Else
                    strCompare = strCompare
                End If
            End If
            ArrangeWord = ArrangeWord & strCompare
        Next intCounter
        Return ArrangeWord
    End Function

End Class
