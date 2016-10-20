Public Class EasyComputer
    Private rdm As New Random()
    Sub EasyGuess()
        Dim guess() As Integer
        guess = CurrentlyPossibleSolutions.Item(rdm.Next(0, CurrentlyPossibleSolutions.Count))
        Debug.Print("AI guesses " & ArrayToInt(guess))
        CurrentBW = verify(solution, guess)
        Debug.Print("This returns " & ArrayToInt(CurrentBW))
        Debug.Print("Number of possible solutions before elimination: " & CurrentlyPossibleSolutions.Count)
        Eliminate(guess, CurrentBW)
        Debug.Print("Number of possible solutions after elimination: " & CurrentlyPossibleSolutions.Count)
    End Sub
End Class
