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
        Select Case ResultString
            Case "Error 1.1"
            Case "Error 1.2"
            Case Else
        End Select
        MsgBox(ResultString)
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
                Dim UpdateClient As New WebClient
                ParametersString = "?code=" & HTTPGameCode & "&action=udpateguess&guesslist=" & SeriesString
                Debug.Print(UpdateClient.DownloadString(ServerBaseURI & "/updategame.php" & ParametersString))
                UpdateClient.Dispose()
            End If
            If UsersTurn = False Then
                PvPHTTP.UpdateGuessTimer.Enabled = False
            End If
        End If
    End Sub

End Class
