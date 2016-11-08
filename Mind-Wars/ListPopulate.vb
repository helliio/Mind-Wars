Option Strict On

Public Class ListPopulate
    Public Sub PopulateLists()
        Dim ExpectedCount As Integer = CInt(colours ^ holes)


        CurrentlyPossibleSolutions.Clear()
        InitiallyPossibleSolutions.Clear()

        Dim LowestListValue As Integer = 0
        Dim q(holes - 1) As Integer
        For i As Integer = 0 To holes - 1
            q(i) = colours - 1
        Next
        Dim HighestListValue As Integer = ArrayToInt(q)
        For i As Integer = LowestListValue To HighestListValue
            If CheckArrRange(i, 0, colours - 1) = True Then
                CurrentlyPossibleSolutions.Add(SolutionIntToArray(i))
                InitiallyPossibleSolutions.Add(SolutionIntToArray(i))
            End If
        Next

        'Debug.Print("The InitiallyPossibleSolutions list contains " & InitiallyPossibleSolutions.Count.ToString & " integers.")
        'Debug.Print("If " & InitiallyPossibleSolutions.Count.ToString & " = colours^holes = " & ExpectedCount.ToString & ", this checks out.")
    End Sub

End Class
