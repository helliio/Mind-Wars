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
        SolutionSet = True
        If GameFinished = False Then
            PvPHTTP.LoadGuessTimer.Enabled = True
        End If
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
                ResultString = UpdateClient.DownloadString(ServerBaseURI & "updategame.php" & "?code=" & HTTPGameCode & "&action=updateguess&guesslist=" & SeriesString)
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
        Debug.Print("LoadGuess running")
        Dim GetSeriesClient As New WebClient
        Dim ResultString As String = GetSeriesClient.DownloadString(ServerBaseURI & "/getseries.php?code=" & CStr(HTTPGameCode))
        GetSeriesClient.Dispose()
        Debug.Print("RESULTSTRING: " & ResultString)
        If Not ResultString = LatestSeriesString AndAlso ResultString.Length > 0 Then
            LatestSeriesString = ResultString
            Debug.Print("Previous: " & LatestSeriesString & vbNewLine & "New: " & ResultString)
            Debug.Print("AIGuessList.Count = " & CStr(AIGuessList.Count))
        Else
            Debug.Print("LatestSeriesString: " & LatestSeriesString & ", ResultString.Length = " & ResultString.Length)
        End If
        'Debug.Print("<Starting SOG>")
        'PvPHTTP.ShowOpponentGuessTimer.Enabled = True
        Debug.Print("Finished loading")
        IsLoading = False
    End Sub
End Class
