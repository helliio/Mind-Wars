Option Strict On
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

    Function MiniArrayToInt(ByVal array() As Integer) As Integer
        Dim int As Integer
        Dim l As Integer = array.Length - 1
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
                If g(j) = s(i) AndAlso g(i) <> s(i) AndAlso s(i) <> -1 Then
                    bw(1) += 1
                    g(j) = -1
                    s(i) = -1
                End If
            Next
        Next
        Return bw

        'Dim whitepegs, blackpegs As Integer
        'Dim counted(holes - 1) As Integer
        'Dim correct(holes - 1) As Integer

        'Dim checkcorrectindex As Integer = 0
        'Do
        '    If ail(checkcorrectindex) = g(checkcorrectindex) Then
        '        blackpegs += 1
        '        counted(checkcorrectindex) = 1
        '        correct(checkcorrectindex) = 1
        '    End If
        '    checkcorrectindex += 1
        'Loop Until checkcorrectindex = holes

        'Dim currentstep As Integer = 0
        'Do
        '    If correct(currentstep) = 0 AndAlso ail.Contains(g(currentstep)) Then
        '        Dim countedindex As Integer = 0
        '        Dim foundmatch As Boolean = False
        '        Do
        '            If countedindex = currentstep Then
        '                countedindex += 1
        '                Continue Do
        '            ElseIf ail(countedindex) = g(currentstep) AndAlso counted(countedindex) = 0 Then
        '                counted(countedindex) = 1
        '                whitepegs += 1
        '                foundmatch = True
        '            End If
        '            countedindex += 1
        '        Loop Until countedindex = holes Or foundmatch = True
        '    End If
        '    currentstep += 1
        'Loop Until currentstep = holes

        'Dim AntallBW(1) As Integer
        'AntallBW = {blackpegs, whitepegs}
        'Return AntallBW
    End Function

End Class
