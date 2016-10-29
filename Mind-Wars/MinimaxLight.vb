Option Strict On
Class MinimaxLight
    Inherits MinimaxFunctions
    Private LocalInitialList, LocalPossibleList As Integer()()
    Sub New(ByVal InitialList As Integer()(), ByVal PossibleList As Integer()())
        LocalInitialList = InitialList
        LocalPossibleList = PossibleList
        'LocalPossibleList = PossibleList.GetRange(0, InitialList.Count)
    End Sub

    Public Sub FindBestMove()
        Do Until LocalPossibleList.Count > 0 AndAlso LocalInitialList.Count > 0
            System.Threading.Thread.Yield()
        Loop

        Dim BWCount(1) As Integer
        Dim HighestMinScoreIndex As Integer = 0
        Dim ScoreForSolution As Integer = 0
        Dim i As Integer = 0
        Dim iMax As Integer = LocalPossibleList.Count

        Dim SolutionCount As Integer = LocalPossibleList.Count
        Dim InitialCount As Integer = LocalInitialList.Count
        Dim BWList As New List(Of Integer)
        If CInt(3 ^ holes) <= LocalPossibleList.Count Then
            BWList.Capacity = CInt(3 ^ holes)
        Else
            BWList.Capacity = LocalPossibleList.Count
        End If

        Do Until i = iMax
            Dim q As Integer = 0
            Dim score As Integer = Integer.MaxValue
            Dim InitialItemiArray() As Integer = LocalInitialList(i)
            Do Until q = SolutionCount
                BWCount = MiniGetBW(LocalInitialList(q), InitialItemiArray)
                Dim BWint As Integer = BWCount(0) * 10 + BWCount(1)
                If Not BWList.Contains(BWint) Then
                    Dim tempscore As Integer = MiniCalculateEliminated(BWCount(0), BWCount(1), InitialItemiArray)
                    If score > tempscore Then
                        score = tempscore
                    End If
                    BWList.Add(BWint)
                End If
                q += 1
            Loop
            BWList.Clear()
            q = 0
            If score > ScoreForSolution AndAlso score < LocalInitialList.Count + 1 Then
                ScoreForSolution = score
                HighestMinScoreIndex = i
            End If
            i += 1
        Loop
        FourBestIndices = {HighestMinScoreIndex, HighestMinScoreIndex, HighestMinScoreIndex, HighestMinScoreIndex}
        FourBestScores = {ScoreForSolution, ScoreForSolution, ScoreForSolution, ScoreForSolution}
    End Sub

    Private Function MiniCalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer) As Integer
        Dim SolutionsEliminated As Integer = 0
        Dim ListCount As Integer = LocalPossibleList.Count - 1
        Dim Check() As Integer
        For q = 0 To ListCount
            Check = MiniGetBW(LocalPossibleList(q), HypotheticalGuess)
            If Not Check(0) = B OrElse Not Check(1) = W Then
                SolutionsEliminated += 1
            End If
        Next
        Return SolutionsEliminated
    End Function

End Class
