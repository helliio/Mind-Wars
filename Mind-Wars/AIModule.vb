Module AIModule
    Dim rdm As New Random()
    'returns an array of random integers of colours for each hole(array element)
    Function generate()
        Dim ret(SystemModule.holes - 1) As Integer
        For n As Integer = 0 To SystemModule.holes - 1
            ret(n) = rdm.Next(1, SystemModule.colours)
        Next
        Return ret
    End Function
End Module
