Module AIModule
    Public InitiallyPossibleSolutions As New ArrayList
    Public CurrentlyPossibleSolutions As New ArrayList

    Public UseLightMinimax As Boolean = False

    Public AI_BW_Check(1) As Integer
    Public AIAttempts As Integer = 0

    Public FourBestScores(3) As Integer
    Public FourBestIndices(3) As Integer

    Dim rdm As New Random()

    'Returns an array of random integers of colours for each hole(array element)
    Function GenerateSolution() As Integer()
        Dim ret(SystemModule.holes - 1) As Integer
        For n As Integer = 0 To SystemModule.holes - 1
            ret(n) = rdm.Next(0, SystemModule.colours)
        Next
        Return ret
    End Function

    Public Sub PopulateLists(ByVal Operation As Integer, Optional sender As Object = Nothing)
        Dim ExpectedCount As Integer = colours ^ holes

        If Operation = 1 Then
            CurrentlyPossibleSolutions = New ArrayList
            InitiallyPossibleSolutions = New ArrayList
            InitiallyPossibleSolutions.Clear()
            CurrentlyPossibleSolutions.Clear()
            InitiallyPossibleSolutions.TrimToSize()
            CurrentlyPossibleSolutions.TrimToSize()
            Dim q(holes - 1) As Integer
            For i As Integer = 0 To holes - 1
                q(i) = 0
            Next
            Dim LowestListValue As Integer = ArrayToInt(q)
            For i As Integer = 0 To holes - 1
                q(i) = colours - 1
            Next
            Dim HighestListValue As Integer = ArrayToInt(q)
            Dim ReportProgressCounter As Integer = 0
            For i As Integer = LowestListValue To HighestListValue
                If CheckArrRange(i, 0, colours - 1) = True Then
                    CurrentlyPossibleSolutions.Add(IntToArr(i))
                    ReportProgressCounter += 1
                    If ReportProgressCounter = 10 AndAlso Not IsNothing(sender) Then
                        sender.ReportProgress((CurrentlyPossibleSolutions.Count / ExpectedCount) * 100)
                        ReportProgressCounter = 0
                    End If
                End If
            Next
            ' For impractically slow but more intelligent decisions, replace with:
            ' InitiallyPossibleSolutions = CurrentlyPossibleSolutions.GetRange(0, CurrentlyPossibleSolutions.Count)
            ' or something like that. These lists are equal now, as discriminating between the two would result in
            ' calculations several minutes long in the upper range of holes and colors, though it gives the computer
            ' the ability to strategically pick an incorrect code in order to gain the maximum amount of information.
            InitiallyPossibleSolutions = CurrentlyPossibleSolutions

            Debug.Print("The InitiallyPossibleSolutions list contains " & InitiallyPossibleSolutions.Count & " integers.")
            Debug.Print("If " & InitiallyPossibleSolutions.Count & " = colours^holes = " & ExpectedCount & ", this checks out.")

        Else

        End If
    End Sub


    Public Sub Eliminate(ByVal RealGuess() As Integer, ByVal RealBW() As Integer)
        Dim CountBefore As Integer = CurrentlyPossibleSolutions.Count
        Dim q As Integer = CurrentlyPossibleSolutions.Count - 1

        Do
            Dim CheckBW() As Integer = verify(CurrentlyPossibleSolutions.Item(q), RealGuess)
            If Not CheckBW(1) = RealBW(1) OrElse Not CheckBW(0) = RealBW(0) Then
                If ArrayToInt(CurrentlyPossibleSolutions.Item(q)) = ArrayToInt(solution) Then
                    MsgBox("About to remove the actual solution. Solution: " & ArrayToInt(solution) & ", CheckBW = " & ArrayToInt(CheckBW) & ", RealBW = " & ArrayToInt(RealBW) & ", GetBW(" & CurrentlyPossibleSolutions.Item(q).ToString & ", " & ArrayToInt(RealGuess) & ") returns CheckBW")
                End If
                CurrentlyPossibleSolutions.RemoveAt(q)
            End If
            q -= 1
        Loop Until q = -1
        CurrentlyPossibleSolutions.TrimToSize()
        Debug.Print("Trimmed list of possible solutions: " & InitiallyPossibleSolutions.Count & " / " & CurrentlyPossibleSolutions.Count)
    End Sub

    Function CalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer) As Integer
        Dim SolutionsEliminated As Integer = 0
        Dim q As Integer = 0
        Dim ListCount As Integer = CurrentlyPossibleSolutions.Count - 1
        Do Until q = ListCount
            AI_BW_Check = verify(CurrentlyPossibleSolutions.Item(q), HypotheticalGuess)
            If Not AI_BW_Check(0) = B OrElse Not AI_BW_Check(1) = W Then
                SolutionsEliminated += 1
            End If
            q += 1
        Loop
        Return SolutionsEliminated
    End Function

End Module
