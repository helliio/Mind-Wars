Option Strict On

Public Class ListPopulate
    Public Sub PopulateLists()
        Dim ExpectedCount As Integer = CInt(colours ^ holes)

        If InitiallyPossibleSolutions.Count > 0 OrElse CurrentlyPossibleSolutions.Count > 0 Then
            InitiallyPossibleSolutions.Clear()
            CurrentlyPossibleSolutions.Clear()
        End If
        InitiallyPossibleSolutions.Capacity = ExpectedCount
        CurrentlyPossibleSolutions.Capacity = ExpectedCount

        Dim LowestListValue As Integer = 0
        Dim q(holes - 1) As Integer
        For i As Integer = 0 To holes - 1
            q(i) = colours - 1
        Next
        Dim HighestListValue As Integer = ArrayToInt(q)
        For i As Integer = LowestListValue To HighestListValue
            If CheckArrRange(i, 0, colours - 1) = True Then
                CurrentlyPossibleSolutions.Add(SolutionIntToArray(i))
            End If
        Next
        ' For impractically slow but more intelligent decisions, replace with:
        ' InitiallyPossibleSolutions = CurrentlyPossibleSolutions.GetRange(0, CurrentlyPossibleSolutions.Count)
        ' or something like that. These lists are equal now, as discriminating between the two would result in
        ' calculations several minutes long in the upper range of holes and colors, though it gives the computer
        ' the ability to strategically pick an incorrect code in order to gain the maximum amount of information.
        InitiallyPossibleSolutions = CurrentlyPossibleSolutions

        Debug.Print("The InitiallyPossibleSolutions list contains " & InitiallyPossibleSolutions.Count.ToString & " integers.")
        Debug.Print("If " & InitiallyPossibleSolutions.Count.ToString & " = colours^holes = " & ExpectedCount.ToString & ", this checks out.")
    End Sub

End Class
