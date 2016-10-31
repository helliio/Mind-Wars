Option Strict On
Module AIModule
    Public InitiallyPossibleSolutions, CurrentlyPossibleSolutions As New List(Of Integer())
    Public AIGuessList As New List(Of Integer)
    Public UseLightMinimax As Boolean = False

    Public AI_BW_Check(1) As Integer
    Public FourBestScores(3), FourBestIndices(3) As Integer
    Public AIAttempts As Integer = 0

    Dim rdm As New Random()

    'Returns an array of random integers of colours for each hole(array element)
    Function GenerateSolution() As Integer()
        Dim ret(SystemModule.holes - 1) As Integer
        For n As Integer = 0 To SystemModule.holes - 1
            ret(n) = rdm.Next(0, SystemModule.colours)
        Next
        Return ret
    End Function

    Public Sub PopulateLists(ByVal Operation As Integer, Optional senderX As Object = Nothing)
        Dim sender As System.ComponentModel.BackgroundWorker = DirectCast(senderX, System.ComponentModel.BackgroundWorker)
        Dim ExpectedCount As Integer = CInt(colours ^ holes)

        If Operation = 1 Then
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
            Dim ReportProgressCounter As Integer = 0
            For i As Integer = LowestListValue To HighestListValue
                If CheckArrRange(i, 0, colours - 1) = True Then
                    CurrentlyPossibleSolutions.Add(SolutionIntToArray(i))
                    ReportProgressCounter += 1
                    If ReportProgressCounter = 10 AndAlso Not IsNothing(sender) Then
                        sender.ReportProgress(CInt(i / HighestListValue) * 100)
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

            Debug.Print("The InitiallyPossibleSolutions list contains " & InitiallyPossibleSolutions.Count.ToString & " integers.")
            Debug.Print("If " & InitiallyPossibleSolutions.Count.ToString & " = colours^holes = " & ExpectedCount.ToString & ", this checks out.")

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
                    MsgBox("About to remove the actual solution. Solution: " & ArrayToString(solution) & ", CheckBW = " & ArrayToString(CheckBW) & ", RealBW = " & ArrayToString(RealBW) & ", GetBW(" & ArrayToString(CurrentlyPossibleSolutions.Item(q)) & ", " & ArrayToString(RealGuess) & ") returns CheckBW")
                End If
                CurrentlyPossibleSolutions.RemoveAt(q)
            End If
            q -= 1
        Loop Until q = -1
        Debug.Print("Trimmed list of possible solutions: " & InitiallyPossibleSolutions.Count.ToString & " / " & CurrentlyPossibleSolutions.Count.ToString)
    End Sub

    Function CalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer) As Integer
        Dim SolutionsEliminated As Integer = 0
        Dim ListCount As Integer = CurrentlyPossibleSolutions.Count - 1
        For q = 0 To ListCount
            AI_BW_Check = verify(CurrentlyPossibleSolutions.Item(q), HypotheticalGuess)
            If Not AI_BW_Check(0) = B OrElse Not AI_BW_Check(1) = W Then
                SolutionsEliminated += 1
            End If
        Next
        Return SolutionsEliminated
    End Function

End Module
