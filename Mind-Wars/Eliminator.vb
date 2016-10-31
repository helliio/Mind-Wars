Public Class Eliminator
    Inherits MinimaxFunctions
    Public RealGuess(), RealBW() As Integer

    Public Sub Eliminate()
        Dim q As Integer = CurrentlyPossibleSolutions.Count - 1
        Do Until q = -1
            Dim CheckBW() As Integer = MiniGetBW(CurrentlyPossibleSolutions.Item(q), RealGuess)
            If Not CheckBW(1) = RealBW(1) OrElse Not CheckBW(0) = RealBW(0) Then
                CurrentlyPossibleSolutions.RemoveAt(q)
            End If
            q -= 1
        Loop
    End Sub
End Class
