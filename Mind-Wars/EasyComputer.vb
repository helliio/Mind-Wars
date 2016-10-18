Public Class EasyComputer
    Private rdm As New Random()

    Sub EasyGuess()
        Dim guess() As Integer
        guess = IntToArr(CurrentlyPossibleSolutions.Item(rdm.Next(0, CurrentlyPossibleSolutions.Count)))
        CurrentBW = verify(solution, guess)
        Eliminate(guess, CurrentBW)
    End Sub
End Class
