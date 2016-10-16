Module VerifyModule
    'Returns a Array of {black,white}
    Function verify(ByVal solution() As Integer, ByVal verifyguess() As Integer) As Integer()
        Dim BW(1) As Integer
        BW = {0, 0}
        Dim s() As Integer = solution
        Dim g() As Integer = verifyguess
        For i As Integer = 0 To SystemModule.holes - 1
            If g(i) = s(i) Then
                BW(0) += 1
                g(i) = -1
                s(i) = -1
            End If
        Next
        For i As Integer = 0 To SystemModule.holes - 1
            For j As Integer = 0 To SystemModule.holes - 1
                If g(j) = s(i) And g(i) <> s(i) And s(i) <> -1 Then
                    BW(1) += 1
                    g(j) = -1
                    s(i) = -1
                End If
            Next
        Next
        Return BW
    End Function

    Function verifyFixTest(ByVal AgainstSolution() As Integer, ByVal VerifyGuess() As Integer) As Integer()
        Dim givenSolution(holes - 1) As Integer
        AgainstSolution.CopyTo(givenSolution, 0)
        Dim checkGuess(holes - 1) As Integer
        VerifyGuess.CopyTo(checkGuess, 0)
        Dim BWPegs() As Integer = {0, 0}
        For i As Integer = 0 To holes - 1
            If checkGuess(i) = givenSolution(i) Then
                BWPegs(0) += 1
                checkGuess(i) = -1
                givenSolution(i) = -1
            End If
        Next
        For i As Integer = 0 To holes - 1
            For j As Integer = 0 To holes - 1
                If checkGuess(j) = givenSolution(i) AndAlso checkGuess(i) <> givenSolution(i) AndAlso Not givenSolution(i) = -1 Then
                    BWPegs(1) += 1
                    checkGuess(j) = -1
                    givenSolution(i) = -1
                End If
            Next
        Next
        Return BWPegs
    End Function


    'Only used for testing purposes.
    Public Function GetBW(ByVal ail() As Integer, ByVal g() As Integer) As Integer()
        Dim whitepegs As Integer
        Dim blackpegs As Integer
        Dim counted(holes - 1) As Integer
        Dim correct(holes - 1) As Integer

        Dim checkcorrectindex As Integer = 0
        Do
            If ail(checkcorrectindex) = g(checkcorrectindex) Then
                blackpegs += 1
                counted(checkcorrectindex) = 1
                correct(checkcorrectindex) = 1
            End If
            checkcorrectindex += 1
        Loop Until checkcorrectindex = holes

        Dim currentstep As Integer = 0
        Do
            If correct(currentstep) = 0 AndAlso ail.Contains(g(currentstep)) Then
                Dim countedindex As Integer = 0
                Dim foundmatch As Boolean = False
                Do
                    If countedindex = currentstep Then
                        countedindex += 1
                        Continue Do
                    ElseIf ail(countedindex) = g(currentstep) AndAlso counted(countedindex) = 0 Then
                        counted(countedindex) = 1
                        whitepegs += 1
                        foundmatch = True
                    End If
                    countedindex += 1
                Loop Until countedindex = holes Or foundmatch = True
            End If
            currentstep += 1
        Loop Until currentstep = holes

        Dim AntallBW(1) As Integer
        AntallBW = {blackpegs, whitepegs}
        Return AntallBW
    End Function

End Module
