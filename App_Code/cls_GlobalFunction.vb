Imports Microsoft.VisualBasic

Public Class cls_GlobalFunction

    Public Shared Function encrypt(ByVal txt As String, ByVal cryptkey As String) As String
        If (txt = "") Then
            Exit Function
        End If
        If (cryptkey = "") Then
            Exit Function
        End If
        Dim keyval As String
        Dim kv() As String
        Dim estr As String
        Dim enc As String
        Dim i As Integer
        Dim e As String
        Dim rndval As Integer
        keyval = keyvalue(cryptkey)
        kv = Split(keyval, "/")
        For i = 0 To Len(txt)
            e = Mid(txt, i + 1)
            e = Microsoft.VisualBasic.Left(e, 1)
            If (e <> "") Then
                e = Asc(e)
                e = Int(Int(e) + Int(kv(0)))
                e = Int(Int(e) * Int(kv(1)))
                Randomize()
                rndval = Int((90 - 65 + 1) * Rnd() + 65)
                estr = estr & Chr(rndval) & e
            End If
        Next
        Return estr
    End Function

    Public Shared Function decrypt(ByVal txt As String, ByVal cryptkey As String) As String
        Dim keyval As String
        Dim kv() As String
        Dim estr As String
        Dim tmp As String
        Dim e As String
        Dim i As Integer
        keyval = keyvalue(cryptkey)
        kv = Split(keyval, "/")
        estr = ""
        tmp = ""
        For i = 1 To Len(txt)
            If (Microsoft.VisualBasic.Left(Mid(txt, i), 1) <> "") Then
                If (Asc(Microsoft.VisualBasic.Left(Mid(txt, i), 1)) > 64) And (Asc(Microsoft.VisualBasic.Left(Mid(txt, i), 1)) < 91) Then
                    If (tmp <> "") Then
                        tmp = Int(tmp / Int(kv(1)))
                        tmp = Int(tmp - Int(kv(0)))
                        estr = estr & Chr(tmp)
                        tmp = ""
                    End If
                Else
                    tmp = tmp & Microsoft.VisualBasic.Left(Mid(txt, i), 1)
                End If
            End If
        Next

        tmp = Int(tmp / Int(kv(1)))
        tmp = Int(tmp - Int(kv(0)))
        estr = estr & Chr(tmp)
        Return estr
    End Function

    Public Shared Function keyvalue(ByVal cryptkey As String) As String
        Dim keyval1 As Integer
        Dim keyval2 As Integer
        keyval1 = 0
        keyval2 = 0
        Dim i As Integer
        Dim curchr As String
        i = 1
        For i = 1 To Len(cryptkey)
            curchr = Mid(cryptkey, i + 1)
            curchr = Microsoft.VisualBasic.Left(curchr, 1)
            If curchr <> "" Then
                curchr = Asc(curchr)
                keyval1 = Int(keyval1 + curchr)
                keyval2 = Len(cryptkey)
            End If
        Next
        Return (keyval1 & "/" & keyval2)
    End Function

End Class
