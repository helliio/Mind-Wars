Option Strict Off

Imports System
Imports System.Net
Imports System.IO

Module WebModule
    Public ServerBaseURI As String = "http://www.mindwars.no/"
    Private TestDirectory As String = "/test.php"

    Public CreateGameSuccess As Boolean = False
    Public HTTPGameCode As Integer

    Public HTTPClient As New WebClient
    Public HTTPConnectClient As New WebClient
    Public HTTPCheckStatusClient As New WebClient

    Public ConnectionEstablished As Boolean = False

    Public SolutionSet As Boolean = False
    Public GuessListNeedsUpdating As Boolean = False
    Public LatestSeriesString As String


    Public Function CheckOpponentConnection(ByVal code As Integer) As Integer
        If HTTPConnectClient.IsBusy = False Then
            Return CInt(HTTPConnectClient.DownloadString(ServerBaseURI & "/checkconnection.php?code=" & code))
        Else
            Return 0
        End If
    End Function

    Public Sub DisplayCode(ByVal LAN As Boolean)
        If LAN = False Then
            PvPHTTP.GameCodePanel.Hide()
            PvPHTTP.LabGameCode.Hide()
            PvPHTTP.LabActualCode.Hide()
            PvPHTTP.LabExplanation.Hide()
            PvPHTTP.Show()
            With PvPHTTP.GameCodePanel
                .BackColor = Color.Transparent
                .Width = PvPHTTP.ClientRectangle.Width
                .Height = PvPHTTP.ClientRectangle.Height - PvPHTTP.PicFormHeader.Height
                .Top = PvPHTTP.PicFormHeader.Height
                .Left = 0
                .Parent = PvPHTTP
                .BringToFront()
            End With
            With PvPHTTP.LabGameCode
                .BackColor = Color.Transparent
                .Parent = PvPHTTP.GameCodePanel
                .Width = .Parent.ClientRectangle.Width
                .Height = .Parent.ClientRectangle.Height / 2 - PvPHTTP.LabActualCode.Height / 2 - 50
                .Top = 0
                .Left = 0
                .BringToFront()
            End With
            With PvPHTTP.LabActualCode
                .BackColor = Color.Transparent
                .Parent = PvPHTTP.GameCodePanel
                .Height = 50
                .Width = .Parent.Width
                .Top = PvPHTTP.LabGameCode.Height
                .Left = 0
                .BringToFront()
                .Text = HTTPGameCode
            End With
            With PvPHTTP.LabExplanation
                .BackColor = Color.Transparent
                .Parent = PvPHTTP.GameCodePanel
                .Height = .Parent.ClientRectangle.Height / 2 - PvPHTTP.LabActualCode.Height / 2
                .Width = .Parent.ClientRectangle.Width - 40
                .Top = PvPHTTP.LabGameCode.Height + PvPHTTP.LabActualCode.Height
                .Left = 20
                .BringToFront()
            End With
            PvPHTTP.GameCodePanel.Show()
            PvPHTTP.LabGameCode.Show()
            PvPHTTP.LabActualCode.Show()
            PvPHTTP.LabExplanation.Show()
            If PvPHTTP.ConnectionBackgroundWorker.IsBusy = False Then
                PvPHTTP.ConnectionBackgroundWorker.RunWorkerAsync()
            Else
                MsgBox("CBW Busy")
            End If
        Else
        End If
    End Sub

    Public Function ConnectToHTTP(ByVal ConnectionCode As String)
        Dim ResultString As String = HTTPClient.DownloadString(ServerBaseURI & "/joingame.php" & "?code=" & ConnectionCode)
        Return ResultString
    End Function

End Module
