Module CodeStatisticsModuleTemp
    Public CodeComparisonSwitch As Boolean = False
    Public ComparisonA, ComparisonB As New ArrayList
    Public ComparisonWatch As New Stopwatch

    Public Sub Compare()
        Dim CountA As Integer = ComparisonA.Count
        Dim CountB As Integer = ComparisonB.Count

        Dim SumA As Integer = 0
        Dim SumB As Integer = 0

        Dim i As Integer = 0
        Do Until i = CountA
            SumA += ComparisonA(i)
            i += 1
        Loop

        Dim j As Integer = 0
        Do Until j = CountB
            SumB += ComparisonB(j)
            j += 1
        Loop

        Dim AverageA As Integer = SumA / CountA
        Dim AverageB As Integer = SumB / CountB

        MsgBox("Sum elapsed milliseconds (A/B): " & SumA & "/" & SumB & vbNewLine & "Count (A/B): " & CountA & "/" & CountB & vbNewLine & "Average (A/B): " & AverageA & "/" & AverageB)
        ComparisonA.Clear()
        ComparisonB.Clear()
    End Sub

End Module
