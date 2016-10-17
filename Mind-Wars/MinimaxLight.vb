Class MinimaxLight
    Public Sub FindBestMove()
        Dim watch As New Stopwatch
        watch.Start()
        Dim SleepCounter As Integer = 0
        Do Until CurrentlyPossibleSolutions.Count > 0 AndAlso InitiallyPossibleSolutions.Count > 0
            SleepCounter += 1
            If SleepCounter >= 500 Then
                Debug.Print("Error: The list is empty.")
                MsgBox("Solution: " & ArrayToInt(solution) & ", empty")
                If Not CurrentlyPossibleSolutions.Contains(ArrayToInt(solution)) Then
                    MsgBox("List does not contain " & ArrayToInt(solution))
                End If
                Exit Sub
            End If
            System.Threading.Thread.Sleep(10)
        Loop

        Dim BWCount(1) As Integer
        Dim HighestMinScoreIndex As Integer = 0
        Dim ScoreForSolution As Integer = 0
        Dim i As Integer = 0
        Dim iMax As Integer = InitiallyPossibleSolutions.Count

        Dim SolutionCount As Integer = CurrentlyPossibleSolutions.Count
        Dim InitialCount As Integer = InitiallyPossibleSolutions.Count
        Dim BWList As New ArrayList

        Do Until i = iMax
            Dim q As Integer = 0
            Dim score As Integer = Integer.MaxValue
            Dim InitialItemi As Integer = InitiallyPossibleSolutions.Item(i)
            Dim InitialItemiArray() As Integer = IntToArr(InitialItemi)
            Do
                BWCount = verify(IntToArr(CurrentlyPossibleSolutions.Item(q)), InitialItemiArray)
                Dim BWint As Integer = BWCount(0) * 10 + BWCount(1)
                If Not BWList.Contains(BWint) Then
                    Dim tempscore As Integer = (CalculateEliminated(BWCount(0), BWCount(1), InitialItemi))
                    If score > tempscore Then
                        score = tempscore
                    End If
                    BWList.Add(BWint)
                End If
                q += 1
            Loop Until q = SolutionCount
            BWList.Clear()
            q = 0
            If score > ScoreForSolution AndAlso score < Integer.MaxValue Then
                ScoreForSolution = score
                HighestMinScoreIndex = i
            End If
            i += 1
        Loop
        FourBestIndices(0) = HighestMinScoreIndex
        FourBestIndices(1) = HighestMinScoreIndex
        FourBestIndices(2) = HighestMinScoreIndex
        FourBestIndices(3) = HighestMinScoreIndex
        FourBestScores(0) = ScoreForSolution
        FourBestScores(1) = ScoreForSolution
        FourBestScores(2) = ScoreForSolution
        FourBestScores(3) = ScoreForSolution
        Debug.Print("Elapsed time LIGHT: " & watch.Elapsed.Seconds & " for " & CurrentlyPossibleSolutions.Count & " items")
    End Sub
End Class
