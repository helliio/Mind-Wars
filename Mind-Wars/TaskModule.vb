Option Strict On
Option Explicit On
Option Infer Off
Module TaskModule
    Public TESTBestIndex As Integer

    Public Sub RunMinimaxTask()
        Dim taskArray() As Task(Of Integer()) = {Task(Of Integer()).Factory.StartNew(Function() MinimaxTask(holes, InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 1)),
                            Task(Of Integer()).Factory.StartNew(Function() MinimaxTask(holes, InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 2)),
                            Task(Of Integer()).Factory.StartNew(Function() MinimaxTask(holes, InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 3))}

        Dim results(taskArray.Length - 1)() As Integer

        Dim highestscore As Integer = 0
        Dim highestindex As Integer = 0
        For i As Integer = 0 To taskArray.Length - 1
            results(i) = taskArray(i).Result
            If results(i)(1) > highestscore Then
                highestscore = results(i)(1)
                highestindex = results(i)(0)
            End If
        Next
        Debug.Print("HIGHEST INDEX: " & highestindex & " WITH " & highestscore & vbNewLine)
        TESTBestIndex = highestindex
    End Sub

    Private Function MinimaxTask(ByVal holecount As Integer, IL As List(Of Integer()), CL As List(Of Integer()), ByVal Fourth As Integer) As Integer()
        Dim InitialList()() As Integer = IL.ToArray
        Dim CurrentList()() As Integer = CL.ToArray

        Dim HighestMinScoreIndex As Integer = 0
        Dim ScoreForSolution As Integer = 0
        Dim SolutionCount As Integer = InitialList.Count

        Dim Capacity As Integer = 0
        For x As Integer = holecount + 1 To 1
            Capacity += x
            x -= 1
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
                If Not BWList.Contains(BWint) Then
                    BWList.Add(BWint)
                    Dim tempscore As Integer = LocalFunctions.NewCalculateEliminated(BWCount(0), BWCount(1), InitialItemiArray, CurrentList)
                    If score > tempscore Then
                        score = tempscore
                    End If
                    If score < ScoreForSolution Then
                        Exit Do
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
        'BWList.TrimExcess()
        'BWList = Nothing

        Dim ReturnArray() As Integer = {HighestMinScoreIndex, ScoreForSolution}
        Return ReturnArray
    End Function

End Module
