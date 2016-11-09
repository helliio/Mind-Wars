Option Strict On
Option Explicit On
Option Infer Off

Imports System
Imports System.Net
Imports System.IO

Public Class UpdateGameClass

    Public ParametersString As String = ""

    Public Sub Update()
        Dim UpdateClient As New WebClient
        Dim ResultString As String = UpdateClient.DownloadString(ServerBaseURI & "/updategame.php" & ParametersString)
        If Not ResultString = ArrayToString(solution) Then
            MsgBox(ResultString & " in Update()")
        End If
        UpdateClient.Dispose()
    End Sub

    Public Sub UpdateGuess()
        If GuessListNeedsUpdating = True Then
            GuessListNeedsUpdating = False

            Dim SeriesString As String = CStr(GuessList(0))
            For i As Integer = 1 To GuessList.Count - 1
                SeriesString &= "." & CStr(GuessList(i))
            Next
            If SeriesString = LatestSeriesString Then
                Exit Sub
            Else
                LatestSeriesString = SeriesString
                Dim UpdateClient As New WebClient
                Dim ResultString As String = UpdateClient.DownloadString(ServerBaseURI & "/updateguess.php" & "?code=" & HTTPGameCode & "&action=udpateguess&guesslist=" & SeriesString)
                Debug.Print("Updating...")
                UpdateClient.Dispose()
            End If
        End If
        If UsersTurn = False Then
            PvPHTTP.UpdateGuessTimer.Enabled = False
            Debug.Print("Stopping UpdateTimer, UpdateGuess()")
        End If
    End Sub

    Public Sub LoadGuess()
        Dim GetSeriesClient As New WebClient
        Dim ResultString As String = GetSeriesClient.DownloadString(ServerBaseURI & "/getseries.php?code=" & CStr(HTTPGameCode))
        Debug.Print("Found series: " & ResultString)
        GetSeriesClient.Dispose()
        If Not IsNothing(ResultString) AndAlso Not ResultString = LatestSeriesString Then
            Debug.Print("Previous: " & LatestSeriesString & vbNewLine & "New: " & ResultString)
            LatestSeriesString = ResultString
            AIGuessList.Clear()
            Dim SeparatorString As String = "."
            Dim SeparatorChar As Char = SeparatorString.Chars(0)
            Dim GuessArray() As String = ResultString.Split(SeparatorChar)
            For i As Integer = 0 To GuessArray.GetUpperBound(0)
                AIGuessList.Add(i)
            Next
            PvPHTTP.ShowOpponentGuessTimer.Enabled = True
        End If
    End Sub
End Class
