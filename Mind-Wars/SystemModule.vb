Option Strict On

Module SystemModule
    Public holes, colours, tries As Integer
    Public solution(), guess() As Integer
    Public CurrentBW(1) As Integer
    Public TestGuess, ChosenCodeList As New ArrayList

    Public SelectedColor As Integer = 0
    Public SelectedChooseCodeColor As Integer = 0


    Public HolesList, BWHolesList, ChoiceList, ChooseCodeList, ChooseCodeHolesList As New List(Of PictureBox)

    Public ChoiceRectangleList, ChooseCodeRectangleList As New List(Of Rectangle)
    Public GuessList, BWCountList As New ArrayList
    Public Attempt As Integer = 0
    Public BlackCount As Integer
    Public UsersTurn As Boolean = True

    Public Sub GameSetup(ByVal h As Integer, ByVal c As Integer, ByVal t As Integer)
        holes = h
        colours = c
        tries = t
    End Sub

    Public Sub verify_guess()
        Dim g(holes - 1) As Integer
        For i As Integer = 0 To TestGuess.Count - 1
            g(i) = CInt(TestGuess(i))
        Next
        TestGuess.Clear()
        Dim verifiedguess() = verify(solution, g)
        BlackCount = verifiedguess(0)
        For i As Integer = 0 To holes - 1
            If verifiedguess(0) > 0 Then
                BWCountList.Add(2)
                verifiedguess(0) -= 1
            ElseIf verifiedguess(1) > 0 Then
                BWCountList.Add(1)
                verifiedguess(1) -= 1
            Else
                BWCountList.Add(0)
            End If
        Next
    End Sub

    Public Sub InitializeGameMode(ByVal GameMode As Integer)
        Select Case GameMode
            Case 1 'PvE
                PvEGame.InitializeBackgroundWorker.RunWorkerAsync()

        End Select
    End Sub

    Public Function IntToArr(ByVal int As Integer) As Integer()
        Dim str As String = int.ToString
        Dim arr(str.Length - 1) As Integer
        Dim l As Integer = str.Length - 1
        For i As Integer = 0 To l
            arr(i) = CInt(str.Chars(i).ToString)
        Next
        Return arr
    End Function

    Public Function ArrayToInt(ByVal array() As Integer) As Integer
        Dim int As Integer
        Dim l As Integer = array.Length - 1
        For i As Integer = 0 To l
            int += CInt(array(i) * 10 ^ (l - i))
        Next
        Return int
    End Function

    Public Function CheckArrRange(ByVal int As Integer, ByVal min As Integer, ByVal max As Integer) As Boolean
        Dim InRange As Boolean = True
        Dim digits() As Integer = IntToArr(int)
        Dim l As Integer = digits.Length - 1
        For m As Integer = 0 To l
            If digits(m) < min OrElse digits(m) > max Then
                InRange = False
                Exit For
            End If
        Next
        Return InRange
    End Function

End Module
