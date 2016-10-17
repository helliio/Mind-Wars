Class MinimaxLight

    Inherits MinimaxFunctions

    Private LocalInitialList As New ArrayList
    Private LocalPossibleList As New ArrayList
    Sub New(ByVal InitialList As ArrayList, ByVal PossibleList As ArrayList)
        LocalInitialList.Clear()
        LocalPossibleList.Clear()
        LocalInitialList = InitialList.GetRange(0, InitialList.Count)
        LocalPossibleList = PossibleList.GetRange(0, InitialList.Count)

    End Sub


    Public Sub FindBestMove()
        Dim SleepCounter As Integer = 0
        Do Until LocalPossibleList.Count > 0 AndAlso LocalInitialList.Count > 0
            SleepCounter += 1
            If SleepCounter >= 500 Then
                Debug.Print("Error: The list is empty.")
                MsgBox("Solution: " & ArrayToInt(solution) & ", empty")
                If Not LocalPossibleList.Contains(ArrayToInt(solution)) Then
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
        Dim iMax As Integer = LocalPossibleList.Count

        Dim SolutionCount As Integer = LocalPossibleList.Count
        Dim InitialCount As Integer = LocalInitialList.Count
        Dim BWList As New ArrayList

        Do Until i = iMax
            Dim q As Integer = 0
            Dim score As Integer = Integer.MaxValue
            Dim InitialItemi As Integer = LocalInitialList.Item(i)
            Dim InitialItemiArray() As Integer = IntToArr(InitialItemi)
            Do
                BWCount = MiniGetBW(MiniIntToArr(LocalInitialList.Item(q)), InitialItemiArray)
                Dim BWint As Integer = BWCount(0) * 10 + BWCount(1)
                If Not BWList.Contains(BWint) Then
                    Dim tempscore As Integer = MiniCalculateEliminated(BWCount(0), BWCount(1), InitialItemi, LocalPossibleList)
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
        FourBestIndices = {HighestMinScoreIndex, HighestMinScoreIndex, HighestMinScoreIndex, HighestMinScoreIndex}
        FourBestScores = {ScoreForSolution, ScoreForSolution, ScoreForSolution, ScoreForSolution}
    End Sub
End Class
