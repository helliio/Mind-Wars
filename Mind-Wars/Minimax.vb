Class Minimax
    Public FourthNumber As Integer = 0

    Public Sub FindBestMove()
        Dim watch As New Stopwatch
        watch.Start()
        Dim SleepCounter As Integer = 0
        Do Until CurrentlyPossibleSolutions.Count > 20 AndAlso InitiallyPossibleSolutions.Count > 20 AndAlso Not FourthNumber = 0
            System.Threading.Thread.Sleep(50)
            SleepCounter += 1
            If SleepCounter >= 100 Then
                MsgBox("Error: Is " & InitiallyPossibleSolutions.Count & " > 20? How about " & CurrentlyPossibleSolutions.Count & "?")
                Me.Finalize()
            End If
        Loop

        Dim BWCount(1) As Integer
        Dim HighestMinScoreIndex As Integer = 0
        Dim ScoreForSolution As Integer = 0
        Dim i As Integer
        Dim iMax As Integer
        Dim quarter As Integer = InitiallyPossibleSolutions.Count / 4

        Dim remainder As Integer = InitiallyPossibleSolutions.Count Mod 4
        Dim SolutionCount As Integer = CurrentlyPossibleSolutions.Count
        Dim InitialCount As Integer = InitiallyPossibleSolutions.Count
        Dim BWList As New ArrayList

        If Not remainder = 0 Then
            If remainder = 1 Then
                quarter -= -0.5
            ElseIf remainder = 2 Then
                quarter -= -0.75
            Else
                quarter = Math.Round(InitiallyPossibleSolutions.Count / 4)
            End If
        End If

        If FourthNumber = 1 Then
            iMax = quarter
            i = 0
        ElseIf FourthNumber = 2 Then
            iMax = quarter * 2
            i = quarter - 1
        ElseIf FourthNumber = 3 Then
            iMax = quarter * 3
            i = quarter * 2 - 1
        ElseIf FourthNumber = 4 Then
            iMax = InitiallyPossibleSolutions.Count
            i = quarter * 3 - 1
        Else
            MsgBox("You need to set FourthNumber (1 to 4).")
        End If

        Do Until i = iMax
            Dim q As Integer = 0
            Dim score As Integer = Integer.MaxValue
            Dim InitialItemi As Integer = InitiallyPossibleSolutions.Item(i)
            Dim InitialItemiArray() As Integer = IntToArr(InitialItemi)
            Do
                BWCount = verifyFixTest(IntToArr(CurrentlyPossibleSolutions.Item(q)), InitialItemiArray)
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
        FourBestIndices(FourthNumber - 1) = HighestMinScoreIndex
        FourBestScores(FourthNumber - 1) = ScoreForSolution
        Debug.Print("Elapsed time QUAD: " & watch.Elapsed.Seconds & " for " & CurrentlyPossibleSolutions.Count & " items")
    End Sub
End Class
