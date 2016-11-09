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
        UpdateClient.Dispose()
        If Not ResultString = ArrayToString(solution) Then
            MsgBox(ResultString & " in Update()")
        End If
        PvPHTTP.LoadGuessTimer.Enabled = True
    End Sub

    Public Sub UpdateGuess()
        Dim SeriesString As String = ""
        For i As Integer = 0 To GuessList.Count - 1
            SeriesString &= CStr(GuessList(i))
        Next
        Debug.Print("SeriesString to upload: " & SeriesString)
        If Not SeriesString = LatestSeriesString Then
            Dim UpdateClient As New WebClient
            Dim ResultString As String
            Try
                ResultString = UpdateClient.DownloadString(ServerBaseURI & "/updategame.php" & "?code=" & HTTPGameCode & "&action=udpateguess&guesslist=" & SeriesString)
                LatestSeriesString = SeriesString
            Catch
                Debug.Print("Time out")
                ResultString = LatestSeriesString
            End Try
            Debug.Print("Updating...")
            Debug.Print("Result: " & ResultString & ", latest: " & LatestSeriesString)
            UpdateClient.Dispose()
        End If
    End Sub

    Public Sub LoadGuess()
        Dim GetSeriesClient As New WebClient
        Dim ResultString As String = GetSeriesClient.DownloadString(ServerBaseURI & "/getseries.php?code=" & CStr(HTTPGameCode))
        GetSeriesClient.Dispose()
        If ResultString.Length > 0 AndAlso Not ResultString = LatestSeriesString Then
            LatestSeriesString = ResultString
            Debug.Print("Previous: " & LatestSeriesString & vbNewLine & "New: " & ResultString)
            AIGuessList.Clear()
            Dim GuessArray() As Char = ResultString.ToCharArray
            For i As Integer = 0 To GuessArray.GetUpperBound(0)
                AIGuessList.Add(CInt(GuessArray(i).ToString))
            Next
            Debug.Print("Starting SOG timer...")
        End If
        PvPHTTP.ShowOpponentGuessTimer.Enabled = True
    End Sub
End Class
