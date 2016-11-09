Option Strict On
Imports System.IO
Imports System.Xml.Serialization

Module SystemModule
    Public holes, colours, tries As Integer
    Public solution(), guess() As Integer
    Public CurrentBW(1) As Integer
    Public TestGuess, ChosenCodeList, GuessList, BWCountList As New List(Of Integer)
    Public GameFinished As Boolean = False

    Public SelectedColor As Integer = 0
    Public SelectedChooseCodeColor As Integer = 0


    Public HolesList, BWHolesList, ChoiceList, ChooseCodeList, ChooseCodeHolesList As New List(Of PictureBox)

    Public ChoiceRectangleList, ChooseCodeRectangleList As New List(Of Rectangle)

    Public Attempt As Integer = 0
    Public BlackCount As Integer
    Public UsersTurn As Boolean = True

    Public Sub GameSetup(ByVal h As Integer, ByVal c As Integer, ByVal t As Integer)
        holes = h
        colours = c
        tries = t
    End Sub

    Public Sub verify_guess()
        'Dim g(holes - 1) As Integer
        Dim g() As Integer = TestGuess.ToArray
        'For i As Integer = 0 To TestGuess.Count - 1
        '    g(i) = CInt(TestGuess(i))
        'Next
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

    Public Function SolutionIntToArray(ByVal int As Integer) As Integer()
        Dim str As String = int.ToString
        Dim arr(holes - 1) As Integer
        Dim LengthDifference As Integer = holes - str.Length
        Dim UpdatedDifference As Integer = LengthDifference


        For i As Integer = 0 To holes - 1
            If UpdatedDifference > 0 Then
                arr(i) = 0
                UpdatedDifference -= 1
            Else
                arr(i) = CInt(CStr(str.Chars(i - LengthDifference)))
            End If
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

    Public Function ArrayToString(ByVal array() As Integer) As String
        Dim str As String = ""
        Dim l As Integer = array.Length - 1
        For i As Integer = 0 To l
            str &= array(i).ToString
        Next
        Return str
    End Function

    Public Function CopyList(Of T)(oldList As List(Of T)) As List(Of T)
        'Serialize
        Dim xmlString As String = ""
        Dim string_writer As New StringWriter
        Dim xml_serializer As New XmlSerializer(GetType(List(Of T)))
        xml_serializer.Serialize(string_writer, oldList)
        xmlString = string_writer.ToString()

        'Deserialize
        Dim string_reader As New StringReader(xmlString)
        Dim newList As List(Of T)
        newList = DirectCast(xml_serializer.Deserialize(string_reader), List(Of T))
        string_reader.Close()
        Return newList
    End Function

    Public Function CopyArray(OldArray As Integer()) As Integer()
        'Serialize
        Dim xmlString As String = ""
        Dim string_writer As New StringWriter
        Dim xml_serializer As New XmlSerializer(GetType(Integer()))
        xml_serializer.Serialize(string_writer, OldArray)
        xmlString = string_writer.ToString()

        'Deserialize
        Dim string_reader As New StringReader(xmlString)
        Dim newArray As Integer()
        newArray = DirectCast(xml_serializer.Deserialize(string_reader), Integer())
        string_reader.Close()
        Return newArray
    End Function

    Public Function CheckArrRange(ByVal int As Integer, ByVal min As Integer, ByVal max As Integer) As Boolean
        Dim InRange As Boolean = True
        Dim digits() As Integer = SolutionIntToArray(int)
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
