Public Class ListPopulate
    Public Operation As Integer = 1
    Public Sender As Object = Nothing
    Public Sub PopulateLists()
        Dim ExpectedCount As Integer = colours ^ holes
        Select Case Operation
            Case 1
                InitiallyPossibleSolutions.Clear()
                CurrentlyPossibleSolutions.Clear()
                Dim q(holes - 1) As Integer
                For i As Integer = 0 To holes - 1
                    q(i) = 1
                Next
                Dim LowestListValue As Integer = ArrayToInt(q)
                For i As Integer = 0 To holes - 1
                    q(i) = colours
                Next
                Dim HighestListValue As Integer = ArrayToInt(q)
                Dim ReportProgressCounter As Integer = 0

                For i As Integer = LowestListValue To HighestListValue
                    If CheckArrRange(i, 1, colours) = True Then
                        If CheckArrRange(i, 1, colours) = True Then
                            CurrentlyPossibleSolutions.Add(i)
                            ReportProgressCounter += 0.2
                        End If
                        If CurrentlyPossibleSolutions.Count < 5000 Then

                            End If
                        End If
                    If ReportProgressCounter = 10 AndAlso Not IsNothing(Sender) Then
                        Sender.ReportProgress((CurrentlyPossibleSolutions.Count / ExpectedCount) * 400)
                        ReportProgressCounter = 0

                    End If

                Next
                ' For impractically slow but more intelligent decisions, replace with:
                ' InitiallyPossibleSolutions = CurrentlyPossibleSolutions.GetRange(0, CurrentlyPossibleSolutions.Count)
                ' or something like that. These lists are equal now, as discriminating between the two would result in
                ' calculations several minutes long in the upper range of holes and colors, though it gives the computer
                ' the ability to strategically pick an incorrect code in order to gain the maximum amount of information.
                InitiallyPossibleSolutions = CurrentlyPossibleSolutions

                Debug.Print("The InitiallyPossibleSolutions list contains " & InitiallyPossibleSolutions.Count & " integers.")
                Debug.Print("If " & InitiallyPossibleSolutions.Count & " = colours^holes = " & ExpectedCount & ", this checks out.")
        End Select
    End Sub

End Class
