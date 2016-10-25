Module SystemModule
    Public holes As Integer = 4 'Leave blank; for testing purposes
    Public colours As Integer = 8 'Leave blank; for testing purposes
    Public tries As Integer
    Public solution() As Integer
    Public guess() As Integer

    Public TestGuess As New ArrayList

    Public CurrentBW(1) As Integer

    Public SelectedColor As Integer = 0
    Public SelectedChooseCodeColor As Integer = 0

    Public HolesList As New List(Of PictureBox)
    Public BWHolesList As New List(Of PictureBox)
    Public ChoiceList As New List(Of PictureBox)
    Public ChooseCodeList As New List(Of PictureBox)
    Public ChoiceRectangleList As New List(Of Rectangle)
    Public ChooseCodeRectangleList As New List(Of Rectangle)
    Public GuessList As New ArrayList
    Public Attempt As Integer = 0
    Public BWCountList As New ArrayList
    Public BlackCount As Integer
    Public UsersTurn As Boolean = True

    Public Sub GameSetup(ByVal h As Integer, ByVal c As Integer, ByVal t As Integer)
        holes = h
        colours = c
        tries = t
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
            arr(i) = str.Chars(i).ToString
        Next
        Return arr
    End Function

    Public Function ArrayToInt(ByVal array() As Integer) As Integer
        Dim int As Integer
        Dim l As Integer = array.Length - 1
        For i As Integer = 0 To l
            int += array(i) * 10 ^ (l - i)
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
