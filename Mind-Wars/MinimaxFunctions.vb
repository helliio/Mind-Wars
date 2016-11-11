Option Strict On
Option Explicit On
Option Infer Off
Public Class MinimaxFunctions

    'Function MiniCalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer, ByRef L As Integer()()) As Integer
    '    Dim SolutionsEliminated As Integer = 0
    '    Dim ListCount As Integer = L.Count - 1
    '    For q = 0 To ListCount
    '        Dim Check() As Integer = MiniGetBW(L(q), HypotheticalGuess)
    '        If Not Check(0) = B OrElse Not Check(1) = W Then
    '            SolutionsEliminated += 1
    '        End If
    '    Next
    '    Return SolutionsEliminated
    'End Function

    Function NewCalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer, ByRef CLi As Integer()()) As Integer
        ' BTW
        ' We can check if the returned amount of eliminated is less than the current maximum, and skip a lot of work.


        Dim SolutionsEliminated As Integer = 0, HolesLocal As Integer = holes, ListCount As Integer = CLi.Count, CLiArray()() As Integer = CLi

        Parallel.For(0, ListCount, Sub(q)
                                       Dim bw(1) As Integer
                                       bw = {0, 0}

                                       Dim s(HolesLocal - 1) As Integer
                                       CLiArray(q).CopyTo(s, 0)

                                       Dim g(HolesLocal - 1) As Integer
                                       HypotheticalGuess.CopyTo(g, 0)

                                       For i As Integer = 0 To HolesLocal - 1
                                           If g(i) = s(i) Then
                                               bw(0) += 1
                                               g(i) = -1
                                               s(i) = -1
                                           End If
                                       Next
                                       If bw(0) <> B Then
                                           SolutionsEliminated += 1
                                       Else
                                           For i As Integer = 0 To holes - 1
                                               For j As Integer = 0 To holes - 1
                                                   If g(j) = s(i) AndAlso s(i) <> -1 Then
                                                       bw(1) += 1
                                                       g(j) = -1
                                                       s(i) = -1
                                                   End If
                                               Next
                                           Next
                                           If bw(1) <> W Then
                                               SolutionsEliminated += 1

                                           End If
                                       End If
                                   End Sub)
        Return SolutionsEliminated
    End Function

    Function MiniArrayToInt(ByVal array() As Integer) As Integer
        Dim int As Integer, l As Integer = array.Length - 1
        For i As Integer = 0 To l
            int += CInt(array(i) * 10 ^ (l - i))
        Next
        Return int
    End Function

    'Public Function MiniGetBW(ByVal ail() As Integer, ByVal g() As Integer) As Integer()
    Function MiniGetBW(ByVal verifysolution() As Integer, ByVal verifyguess() As Integer) As Integer()
        Dim bw(1) As Integer
        bw = {0, 0}

        Dim s() As Integer = DirectCast(verifysolution.Clone, Integer())
        Dim g() As Integer = DirectCast(verifyguess.Clone, Integer())
        For i As Integer = 0 To holes - 1
            If g(i) = s(i) Then
                bw(0) += 1
                g(i) = -1
                s(i) = -1
            End If
        Next
        For i As Integer = 0 To holes - 1
            For j As Integer = 0 To holes - 1
                'If g(j) = s(i) AndAlso g(i) <> s(i) AndAlso s(i) <> -1 Then
                If g(j) = s(i) AndAlso s(i) <> -1 Then
                    bw(1) += 1
                    g(j) = -1
                    s(i) = -1
                End If
            Next
        Next
        Return bw
    End Function
End Class
