Option Strict On
Option Explicit On
Option Infer Off
Module AIModule
    Public InitiallyPossibleSolutions, CurrentlyPossibleSolutions As New List(Of Integer())
    Public AIGuessList, AIBWList As New List(Of Integer)
    Public UseLightMinimax, AISolvedCode As Boolean

    Public AI_BW_Check(1), AINewestGuess() As Integer, FourBestScores(3) As Integer, FourBestIndices(3) As Integer
    Public AIAttempts As Integer

    Dim rdm As New Random()

    Dim AlreadyUsedIndices As New List(Of Integer)

    '' FOR TESTING PURPOSES ''
    Public BWForGList As New List(Of Integer())(InitiallyPossibleSolutions.Count)

    Public TestAttempts, TestRuns, TestSum As Integer

    'Returns an array of random integers of colours for each hole(array element)
    Function GenerateSolution() As Integer()
        Dim ret(SystemModule.holes - 1) As Integer
        For n As Integer = 0 To SystemModule.holes - 1
            ret(n) = rdm.Next(0, SystemModule.colours)
        Next
        Return ret
    End Function

    Public Sub AIPlayGuess(ByVal code() As Integer, ByVal BW() As Integer)
        For i As Integer = 0 To holes - 1
            AIGuessList.Add(code(i))

            'Dim forkortelse As Integer = antallforsøk * antallhull - antallhull * (AIturn + 1) + 1

            'Do
            '    AIpeg = testcollection.Item(forkortelse + AIFargeIndex)
            '    AIpeg.BackColor = fargekoder(NyesteForsøk(AIFargeIndex))
            '    AIFargeIndex += 1
            'Loop Until AIFargeIndex = antallhull

            If i < BW(0) Then
                AIBWList.Add(2)
            ElseIf i < BW(1) + BW(0) Then
                AIBWList.Add(1)
            Else
                AIBWList.Add(0)
            End If
        Next
        Debug.Print("AI guesses " & ArrayToString(code))
        PvEGame.AITimer.Enabled = True
    End Sub

    Public Function AIBestFirstGuess() As Integer()
        Dim counter As Integer = 0, FirstColor As Integer = rdm.Next(0, colours - 1), SecondColor As Integer
        Do Until SecondColor <> FirstColor
            SecondColor = rdm.Next(0, colours - 1)
        Loop
        Dim GuessArray(holes - 1) As Integer
        GuessArray(0) = FirstColor
        GuessArray(1) = FirstColor
        GuessArray(2) = SecondColor
        GuessArray(3) = SecondColor
        If holes > 4 Then
            For i As Integer = 4 To holes - 1
                GuessArray(i) = rdm.Next(0, holes - 1)
            Next
        End If

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

            Dim LowestListValue As Integer = 0, q(holes - 1) As Integer
            For i As Integer = 0 To holes - 1
                q(i) = colours - 1
            Next
            Dim HighestListValue As Integer = ArrayToInt(q), ReportProgressCounter As Integer = 0, ReportAt As Integer = CInt(HighestListValue / 100) - 1

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

        Dim q As Integer = CurrentlyPossibleSolutions.Count - 1

        Do Until q = -1
            Dim CheckBW(1) As Integer
            CheckBW = verify(CurrentlyPossibleSolutions.Item(q), RealGuess)

            If Not CheckBW(1) = RealBW(1) OrElse Not CheckBW(0) = RealBW(0) Then
                If ArrayToInt(CurrentlyPossibleSolutions.Item(q)) = ArrayToInt(solution) Then
                    MsgBox("About to remove the actual solution. Solution: " & ArrayToString(solution) & ", CheckBW = " & ArrayToString(CheckBW) & ", RealBW = " & ArrayToString(RealBW) & ", GetBW(" & ArrayToString(CurrentlyPossibleSolutions.Item(q)) & ", " & ArrayToString(RealGuess) & ") returns CheckBW")
                End If
                CurrentlyPossibleSolutions.RemoveAt(q)
            End If
            q -= 1
        Loop
        'Debug.Print("Trimmed list of possible solutions: " & InitiallyPossibleSolutions.Count.ToString & " / " & CurrentlyPossibleSolutions.Count.ToString)
    End Sub

    Function CalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer) As Integer
        Dim SolutionsEliminated As Integer = 0, ListCount As Integer = CurrentlyPossibleSolutions.Count - 1
        For q As Integer = 0 To ListCount
            AI_BW_Check = verify(CurrentlyPossibleSolutions.Item(q), HypotheticalGuess)
            If Not AI_BW_Check(0) = B OrElse Not AI_BW_Check(1) = W Then
                SolutionsEliminated += 1
            End If
        Next
        Return SolutionsEliminated
    End Function



    Public Sub PopulateBWList()

        ' RE-IMPLEMENT "LACKSCORE" BUT OPPOSITE; COUNT PARTITIONS

        Dim IndexOfLowestMaximum As Integer = 0, LowestMaximum As Integer = Integer.MaxValue, AverageForLowest As Double = 0
        BWForGList.Clear()

        For i As Integer = 0 To InitiallyPossibleSolutions.Count - 1
            Dim arr(16) As Integer
            For Each s As Integer() In CurrentlyPossibleSolutions
                Dim bwresult() As Integer = verify(s, InitiallyPossibleSolutions(i))
                Dim ConvertToIndex As Integer = bwresult(0) * (holes) + bwresult(1)
                arr(ConvertToIndex) += 1
                If arr.Max > LowestMaximum Then
                    Exit For
                End If
            Next
            If arr.Max > LowestMaximum Then
                Continue For
            End If

            BWForGList.Add(arr)
            If arr.Max < LowestMaximum Then
                LowestMaximum = arr.Max
                AverageForLowest = arr.Average
                IndexOfLowestMaximum = i
            ElseIf arr.Max = LowestMaximum Then
                Dim Check() As Integer = InitiallyPossibleSolutions(i)
                Dim CheckPrev() As Integer = InitiallyPossibleSolutions(IndexOfLowestMaximum)
                Dim iPossible As Boolean = CurrentlyPossibleSolutions.Exists(Function(ByVal obj As Integer()) As Boolean
                                                                                 For x As Integer = 0 To holes - 1
                                                                                     If Check(x) <> obj(x) Then
                                                                                         Return False
                                                                                     End If
                                                                                 Next
                                                                                 Return True
                                                                             End Function)
                Dim prevPossible As Boolean = CurrentlyPossibleSolutions.Exists(Function(ByVal obj As Integer()) As Boolean
                                                                                    For x As Integer = 0 To holes - 1
                                                                                        If CheckPrev(x) <> obj(x) Then
                                                                                            Return False
                                                                                        End If
                                                                                    Next
                                                                                    Return True
                                                                                End Function)

                If iPossible And Not prevPossible Then
                    LowestMaximum = arr.Max
                    AverageForLowest = arr.Average
                    IndexOfLowestMaximum = i
                ElseIf (iPossible AndAlso prevPossible) OrElse (Not iPossible AndAlso Not prevPossible) Then
                    If arr.Average < BWForGList(IndexOfLowestMaximum).Average Then
                        Debug.Print("AverageForLowest: " & AverageForLowest & " -> " & arr.Average)
                        LowestMaximum = arr.Max
                        AverageForLowest = arr.Average
                        IndexOfLowestMaximum = i
                    End If
                End If
            End If
        Next
        TestAttempts += 1
        Debug.Print("Lowest maximum: " & LowestMaximum & " at " & IndexOfLowestMaximum)
        Debug.Print("Before elimination: " & CurrentlyPossibleSolutions.Count)
        Dim real() As Integer = verify(solution, InitiallyPossibleSolutions(IndexOfLowestMaximum))
        If real(0) = 4 Then
            Debug.Print("HURRAY: " & TestAttempts & "; " & ArrayToString(InitiallyPossibleSolutions(IndexOfLowestMaximum)) & " vs " & ArrayToString(solution))
            TestAttempts = 0
            Dim heh As New ListPopulate
            heh.PopulateLists()
            PvEGame.Button4.Enabled = True
        Else
            Eliminate(InitiallyPossibleSolutions(IndexOfLowestMaximum), real)
            If CurrentlyPossibleSolutions.Count = 1 Then
                TestAttempts += 1
                Debug.Print("HURRAY: " & ArrayToString(CurrentlyPossibleSolutions(0)) & " vs " & ArrayToString(solution) & "; " & TestAttempts)
                TestAttempts = 0
                Dim heh As New ListPopulate
                heh.PopulateLists()
                PvEGame.Button4.Enabled = True
            Else
                Call PopulateBWList()
            End If
        End If

    End Sub












End Module
