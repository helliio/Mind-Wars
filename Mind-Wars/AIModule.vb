Option Strict On
Module AIModule
    Public InitiallyPossibleSolutions, CurrentlyPossibleSolutions As New List(Of Integer())
    Public AIGuessList As New List(Of Integer)
    Public UseLightMinimax As Boolean = False

    Public AI_BW_Check(1) As Integer
    Public FourBestScores(3), FourBestIndices(3) As Integer
    Public AIAttempts As Integer = 0

    Public PreviouslyGuessedList As New List(Of Integer())(5)
    Public PreviouslyGottenBW As New List(Of Integer)(5)

    Dim rdm As New Random()

    'Returns an array of random integers of colours for each hole(array element)
    Function GenerateSolution() As Integer()
        Dim ret(SystemModule.holes - 1) As Integer
        For n As Integer = 0 To SystemModule.holes - 1
            ret(n) = rdm.Next(0, SystemModule.colours)
        Next
        Return ret
    End Function

    Public Function AIBestFirstGuess() As Integer()
        Dim FirstColor As Integer = rdm.Next(0, colours)
        Dim SecondColor As Integer = -1

        Dim GuessArray(holes - 1) As Integer
        Do Until SecondColor <> -1 AndAlso SecondColor <> FirstColor
            SecondColor = rdm.Next(0, colours)
        Loop
        Dim Half As Integer = CInt(holes / 2)
        For i As Integer = 0 To Half - 1
            GuessArray(i) = FirstColor
        Next
        For i As Integer = Half To holes - 1
            GuessArray(i) = SecondColor
        Next
        Return GuessArray
    End Function

    Public Sub PopulateLists(ByVal Operation As Integer, senderX As Object)
        Dim sender As System.ComponentModel.BackgroundWorker = DirectCast(senderX, System.ComponentModel.BackgroundWorker)
        Dim ExpectedCount As Integer = CInt(colours ^ holes)

        If Operation = 1 Then

            InitiallyPossibleSolutions.Clear()
            CurrentlyPossibleSolutions.Clear()
            InitiallyPossibleSolutions.Capacity = ExpectedCount + 1
            CurrentlyPossibleSolutions.Capacity = ExpectedCount + 1

            Dim LowestListValue As Integer = 0

            Dim q(holes - 1) As Integer
            For i As Integer = 0 To holes - 1
                q(i) = colours - 1
            Next
            Dim HighestListValue As Integer = ArrayToInt(q)
            Dim ReportProgressCounter As Integer = 0

            Dim ReportAt As Integer = CInt(HighestListValue / 100) - 1

            For i As Integer = LowestListValue To HighestListValue
                If ReportProgressCounter = ReportAt Then
                    sender.ReportProgress(CInt((i / HighestListValue) * 100))
                    ReportProgressCounter = 0
                End If
                ReportProgressCounter += 1
                If CheckArrRange(i, 0, colours - 1) = True Then
                    CurrentlyPossibleSolutions.Add(SolutionIntToArray(i))
                    InitiallyPossibleSolutions.Add(SolutionIntToArray(i))
                End If
            Next

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
