Option Strict Off

Class Minimax
    Implements IDisposable
    Private FourthNumber As Integer = 0
    Dim LocalInitialList, LocalPossibleList As New ArrayList

    Sub New(ByVal InitialList As ArrayList, ByVal PossibleList As ArrayList, ByVal Fourth As Integer)
        FourthNumber = Fourth
        LocalInitialList.Clear()
        LocalPossibleList.Clear()
        LocalInitialList = InitialList.GetRange(0, InitialList.Count)
        LocalPossibleList = PossibleList.GetRange(0, InitialList.Count)
    End Sub

    Public Sub FindBestMove()
        Dim SleepCounter As Integer = 0
        Do Until (LocalPossibleList.Count > 20 AndAlso LocalInitialList.Count > 20 AndAlso Not FourthNumber = 0) OrElse SleepCounter >= 100
            Threading.Thread.Sleep(50)
            SleepCounter += 1
            If SleepCounter >= 100 Then
                Debug.Print("!!!!!!!! - Thread #" & FourthNumber & " is stuck.")
            End If
        Loop

        Dim BWCount(1), i, iMax As Integer
        Dim HighestMinScoreIndex As Integer = 0
        Dim ScoreForSolution As Integer = 0
        Dim quarter As Integer = CInt(Math.Round(LocalInitialList.Count / 4))
        Dim SolutionCount As Integer = LocalPossibleList.Count
        Dim InitialCount As Integer = LocalInitialList.Count
        Dim BWList As New ArrayList

        Select Case FourthNumber
            Case 1
                iMax = quarter
                i = 0
            Case 2
                iMax = quarter * 2
                i = quarter - 1
            Case 3
                iMax = quarter * 3
                i = quarter * 2 - 1
            Case 4
                iMax = LocalInitialList.Count
                i = quarter * 3 - 1
            Case > 4
                MsgBox("You need to set FourthNumber (1 to 4).")
        End Select

        Debug.Print("Thread #" & FourthNumber & " starts calculating.")
        Do Until i = iMax
            Dim q As Integer = 0
            Dim score As Integer = Integer.MaxValue
            Dim InitialItemiArray() As Integer = LocalInitialList.Item(i)
            Do
                BWCount = MiniGetBW(LocalPossibleList.Item(q), InitialItemiArray)
                Dim BWint As Integer = BWCount(0) * 10 + BWCount(1)
                If Not BWList.Contains(BWint) Then
                    Dim tempscore As Integer = MiniCalculateEliminated(BWCount(0), BWCount(1), InitialItemiArray)
                    If score > tempscore Then
                        score = tempscore
                    End If
                    BWList.Add(BWint)
                End If
                q += 1
            Loop Until q = SolutionCount
            BWList.Clear()
            q = 0
            If score > ScoreForSolution AndAlso score < Integer.MaxValue Then
                ScoreForSolution = score
                HighestMinScoreIndex = i
            End If
            i += 1
        Loop

        ' Thread locking didn't work out as expected.
        FourBestIndices(FourthNumber - 1) = HighestMinScoreIndex
        FourBestScores(FourthNumber - 1) = ScoreForSolution

        Debug.Print("Thread #" & FourthNumber & " finished.")
    End Sub

    Private Function MiniCalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer) As Integer
        Dim SolutionsEliminated As Integer = 0
        Dim q As Integer = 0
        Dim ListCount As Integer = LocalPossibleList.Count - 1
        Do Until q = ListCount
            AI_BW_Check = MiniGetBW(LocalPossibleList.Item(q), HypotheticalGuess)
            If Not AI_BW_Check(0) = B OrElse Not AI_BW_Check(1) = W Then
                SolutionsEliminated += 1
            End If
            q += 1
        Loop
        Return SolutionsEliminated
    End Function

    Public Function MiniGetBW(ByVal ail() As Integer, ByVal ges() As Integer) As Integer()
        Dim bw(1) As Integer
        bw = {0, 0}
        Dim s() As Integer = ail.Clone
        Dim g() As Integer = ges.Clone
        For i As Integer = 0 To SystemModule.holes - 1
            If g(i) = s(i) Then
                bw(0) += 1
                g(i) = -1
                s(i) = -1
            End If
        Next
        For i As Integer = 0 To SystemModule.holes - 1
            For j As Integer = 0 To SystemModule.holes - 1
                If g(j) = s(i) And g(i) <> s(i) And s(i) <> -1 Then
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

        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
