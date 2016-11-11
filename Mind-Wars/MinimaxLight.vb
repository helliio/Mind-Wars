Option Strict On
Option Explicit On
Option Infer Off

Class MinimaxLight
    Private LocalPossibleList As Integer()()
    Sub New(ByVal PossibleList As Integer()())

        LocalPossibleList = PossibleList
        'LocalPossibleList = PossibleList.GetRange(0, InitialList.Count)
    End Sub

    Public Sub FindBestMove()
        Dim LocalFunctions As New MinimaxFunctions
        Dim BWCount(1) As Integer
        Dim HighestMinScoreIndex As Integer = 0
        Dim ScoreForSolution As Integer = Integer.MinValue
        Dim i As Integer = 0
        Dim iMax As Integer = LocalPossibleList.Count

        Dim SolutionCount As Integer = LocalPossibleList.Count
        Dim BWList As New List(Of Integer)(SolutionCount)

        Do Until i = iMax
            Dim q As Integer = 0
            Dim score As Integer = Integer.MaxValue
            Dim PossibleAttempt() As Integer = LocalPossibleList(i)
            Do Until q = LocalPossibleList.Count
                BWCount = LocalFunctions.MiniGetBW(LocalPossibleList(q), PossibleAttempt)
                Dim BWint As Integer = BWCount(0) * 10 + BWCount(1)
                If Not BWList.Exists(Function(obj As Integer) As Boolean
                                         If BWint = obj Then
                                             Return True
                                         End If
                                         Return False
                                     End Function) Then
                    BWList.Add(BWint)
                    Dim tempscore As Integer = CalculateEliminated(BWCount(0), BWCount(1), PossibleAttempt)
                    If score > tempscore Then
                        score = tempscore
                    End If
                End If
                q += 1
            Loop
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




    'Private Function MiniCalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer) As Integer
    '    Dim SolutionsEliminated As Integer = 0
    '    Dim ListCount As Integer = LocalPossibleList.Count - 1
    '    Dim Check() As Integer
    '    For q = 0 To ListCount
    '        Check = MiniGetBW(LocalPossibleList(q), HypotheticalGuess)
    '        If Not Check(0) = B OrElse Not Check(1) = W Then
    '            SolutionsEliminated += 1
    '        End If
    '    Next
    '    Return SolutionsEliminated
    'End Function

End Class
