Option Strict On

Class Minimax
    Implements IDisposable
    Private FourthNumber As Integer = 0
    Private HolesLocal As Integer = holes
    Private LocalPossibleList, LocalInitialList As Integer()()
    Sub New(InitialList As List(Of Integer()), PossibleList As List(Of Integer()), ByVal Fourth As Integer)
        'Copy lists in populatelists instead
        LocalPossibleList = PossibleList.ToArray
        LocalInitialList = InitialList.ToArray

        FourthNumber = Fourth

        'LocalInitialList = InitialList.ToArray
        'LocalPossibleList = PossibleList.ToArray
        '' Improvement: Trim list here + set capacity to 1/4, then copy to the local lists (jagged arrays), remove iMax, increment a "real index" counter along with i to return.
    End Sub

    Private Function BWCapacity(ByVal holecount As Integer) As Integer
        Dim capacity As Integer = 0
        Dim blacks As Integer = 0
        While blacks <= holecount
            Dim whites As Integer = 0
            While whites <= holecount - blacks
                capacity += 1
                whites += 1
            End While
            blacks += 1
        End While
        Return capacity
    End Function

    Public Sub FindBestMove()

        Dim HighestMinScoreIndex As Integer = 0
        Dim ScoreForSolution As Integer = 0
        Dim SolutionCount As Integer = LocalPossibleList.Count

        Dim BWList As New List(Of Integer)(100)
        'If BWCapacity(HolesLocal) < SolutionCount Then
        '    BWList.Capacity = BWCapacity(HolesLocal)
        'Else
        '    BWList.Capacity = SolutionCount
        'End If

        'Dim initialcapacity As Integer = BWList.Capacity

        Dim i, iMax As Integer

        Dim quarter As Integer = CInt(Math.Round(LocalInitialList.Count / 4))
        Select Case FourthNumber
            Case 1
                i = 0
                iMax = quarter
            Case 2
                i = quarter - 1
                iMax = quarter * 2
            Case 3
                iMax = quarter * 3
                i = quarter * 2 - 1
            Case 4
                iMax = LocalInitialList.Count
                i = quarter * 3 - 1
            Case > 4
                MsgBox("You need to set FourthNumber (1 to 4).")
        End Select


        Dim BWCount(1) As Integer
        Debug.Print("MINIMAX STAGE 2")
        Do Until i = iMax
            Dim score As Integer = Integer.MaxValue
            Dim InitialItemiArray() As Integer = LocalInitialList(i)
            Dim q As Integer = 0
            Do Until q = SolutionCount
                Dim Check() As Integer = LocalPossibleList(q)
                BWCount = MiniGetBW(Check, InitialItemiArray)
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
            If score > ScoreForSolution AndAlso score < Integer.MaxValue Then
                ScoreForSolution = score
                HighestMinScoreIndex = i
            End If
            i += 1
        Loop
        BWList.Clear()
        BWList.TrimExcess()
        BWList = Nothing

        FourBestIndices(FourthNumber - 1) = HighestMinScoreIndex
        FourBestScores(FourthNumber - 1) = ScoreForSolution
        Debug.Print("---- THREAD " & FourthNumber.ToString & " FINISHED ---- " & vbNewLine & "imax = " & iMax.ToString & " highestminscoreindex = " & HighestMinScoreIndex & " (" & ArrayToString(LocalInitialList(HighestMinScoreIndex)) & " VS " & ArrayToString(InitiallyPossibleSolutions(HighestMinScoreIndex)) & ")")

    End Sub

    Private Function MiniCalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer) As Integer
        Dim SolutionsEliminated As Integer = 0
        Dim ListCount As Integer = LocalPossibleList.Count - 1
        For q = 0 To ListCount
            Dim Check() As Integer = MiniGetBW(LocalPossibleList(q), HypotheticalGuess)
            If Not Check(0) = B OrElse Not Check(1) = W Then
                SolutionsEliminated += 1
            End If
        Next
        Return SolutionsEliminated
    End Function

    Private Function MiniGetBW(ByVal ail() As Integer, ByVal ges() As Integer) As Integer()
        Dim bw(1) As Integer
        bw = {0, 0}
        Dim s(HolesLocal - 1) As Integer
        Dim g(HolesLocal - 1) As Integer
        For n As Integer = 0 To HolesLocal - 1
            s(n) = ail(n)
            g(n) = ges(n)
        Next

        For i As Integer = 0 To HolesLocal - 1
            If g(i) = s(i) Then
                bw(0) += 1
                g(i) = -1
                s(i) = -1
            End If
        Next
        For i As Integer = 0 To HolesLocal - 1
            For j As Integer = 0 To HolesLocal - 1
                If g(j) = s(i) AndAlso g(i) <> s(i) AndAlso s(i) <> -1 Then
                    bw(1) += 1
                    g(j) = -1
                    s(i) = -1
                End If
            Next
        Next
        Return bw
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).

            End If
            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
            LocalInitialList = Nothing
            LocalPossibleList = Nothing

            MsgBox(InitiallyPossibleSolutions.Count)
        End If
        disposedValue = True
    End Sub

    Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
