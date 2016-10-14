Module VerifyModule
    'Returns a Array of {black,white}
    Function verify(ByVal solution() As Integer, ByVal guess() As Integer)
        Dim bw(1) As Integer
        bw = {0, 0}
        Dim s = solution
        Dim g = guess
        For i As Integer = 0 To SystemModule.holes - 1
            If g(i) = s(i) Then
                bw(0) += 1
                g(i) = -1
                s(i) = -1
            End If
        Next
        For i As Integer = 0 To SystemModule.holes - 1
            For j As Integer = 0 To SystemModule.holes - 1
                If g(j) = s(i) And g(i) <> s(i) And s(i) <> -1 Then
                    bw(1) += 1
                    g(j) = -1
                    s(i) = -1
                End If
            Next
        Next
        Return bw
    End Function
End Module
