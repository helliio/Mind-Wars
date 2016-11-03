Option Strict On
Option Explicit On
Option Infer Off
Module TaskModule
    Public TESTBestIndex As Integer

    Public Sub RunMinimaxTask()
        Dim highestscore As Integer = -1
        Dim highestindex As Integer = -1

        'If CurrentlyPossibleSolutions.Count > 40 Then   UNCOMMENT!!!! COMPARE SPEEDS
        If CurrentlyPossibleSolutions.Count = 0 Then

            Dim taskArray() As Task(Of Integer()) = {Task(Of Integer()).Factory.StartNew(Function() MinimaxTask(holes, InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 1)),
                                Task(Of Integer()).Factory.StartNew(Function() MinimaxTask(holes, InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 2)),
                                Task(Of Integer()).Factory.StartNew(Function() MinimaxTask(holes, InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 3))}
            Dim results(taskArray.Length - 1)() As Integer

            For i As Integer = 0 To taskArray.Length - 1
                results(i) = taskArray(i).Result
                If results(i)(1) > highestscore Then
                    highestscore = results(i)(1)
                    highestindex = results(i)(0)
                ElseIf results(i)(1) = highestscore AndAlso CurrentlyPossibleSolutions.Exists(Function(obj As Integer()) As Boolean
                                                                                                  For h As Integer = 0 To holes - 1
                                                                                                      If obj(h) <> InitiallyPossibleSolutions(highestindex)(h) Then Return False
                                                                                                  Next
                                                                                                  Return True
                                                                                              End Function) Then
                    highestscore = results(i)(1)
                    highestindex = results(i)(0)
                End If
            Next
        Else
            Dim singleTask As Task(Of Integer()) = Task(Of Integer()).Factory.StartNew(Function() MinimaxTask(holes, InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 0))
            Dim result() As Integer = singleTask.Result
            highestscore = result(1)
            highestindex = result(0)
        End If
        Debug.Print("HIGHEST INDEX: " & highestindex & " WITH " & highestscore & vbNewLine)

        If highestscore = 0 Then
            Debug.Print("ZERO!")
            TESTBestIndex = InitiallyPossibleSolutions.FindIndex(Function(obj As Integer()) As Boolean
                                                                     Return ArrayToInt(CurrentlyPossibleSolutions(0)) = ArrayToInt(obj)
                                                                 End Function)
        Else
            TESTBestIndex = highestindex
        End If
    End Sub

    Private Function MinimaxTask(ByVal holecount As Integer, IL As List(Of Integer()), CL As List(Of Integer()), ByVal Fourth As Integer) As Integer()
        Dim InitialList()() As Integer = IL.ToArray
        Dim CurrentList()() As Integer = CL.ToArray

        Dim HighestMinScoreIndex As Integer = Integer.MinValue
        Dim ScoreForSolution As Integer = Integer.MinValue
        Dim SolutionCount As Integer = InitialList.Count

        Dim Capacity As Integer = -1
        For x As Integer = holecount + 1 To 1 Step -1
            Capacity += x
        Next

        Dim BWList As New List(Of Integer)(Capacity)

        Dim i, iMax As Integer

        Dim quarter As Integer = CInt(Math.Round(InitialList.Count / 4))
        Select Case Fourth
            Case 1
                i = 0
                iMax = quarter
            Case 2
                i = quarter - 1
                iMax = quarter * 2
            Case 3
                i = quarter * 2 - 1
                iMax = InitialList.Count
            Case 0
                i = 0
                iMax = InitialList.Count
        End Select

        Dim LocalFunctions As New MinimaxFunctions

        Dim TopScore As Integer = 0

        Dim BWCount(1) As Integer
        Do Until i = iMax
            Dim score As Integer = Integer.MaxValue
            Dim InitialItemiArray() As Integer = InitialList(i)

            Dim q As Integer = 0
            Do Until q = CurrentList.Count
                Dim Check() As Integer = CurrentList(q)
                BWCount = LocalFunctions.MiniGetBW(Check, InitialItemiArray)

                Dim BWint As Integer = BWCount(0) * 10 + BWCount(1)
                If Not BWList.Exists(Function(obj As Integer) As Boolean
                                         If BWint = obj Then
                                             Return True
                                         End If
                                         Return False
                                     End Function) Then
                    BWList.Add(BWint)
                    Dim tempscore As Integer = LocalFunctions.NewCalculateEliminated(BWCount(0), BWCount(1), InitialItemiArray, CurrentList)
                    If tempscore < score Then
                        score = tempscore
                    End If
                    If score < ScoreForSolution Then
                        Exit Do
                    End If
                End If
                q += 1
            Loop
            BWList.Clear()

            If score > ScoreForSolution AndAlso score < Integer.MaxValue Then
                ScoreForSolution = score
                HighestMinScoreIndex = i
            ElseIf score = ScoreForSolution AndAlso CurrentlyPossibleSolutions.Exists(Function(obj As Integer()) As Boolean
                                                                                          For h As Integer = 0 To holes - 1
                                                                                              If obj(h) <> InitialItemiArray(h) Then Return False
                                                                                          Next
                                                                                          Return True
                                                                                      End Function) = True Then
                ScoreForSolution = score
                HighestMinScoreIndex = i

            End If
            i += 1
        Loop
        'BWList.TrimExcess()
        'BWList = Nothing
        Dim ReturnArray() As Integer = {HighestMinScoreIndex, ScoreForSolution}
        Return ReturnArray
    End Function

End Module
