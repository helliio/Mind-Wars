Module VerifyModule
    'Returns a Array of {black,white}
    Function verify(ByVal verifysolution() As Integer, ByVal verifyguess() As Integer) As Integer()
        Dim bw(1) As Integer
        bw = {0, 0}
        Dim s() As Integer = verifysolution.Clone
        Dim g() As Integer = verifyguess.Clone
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
