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


    Public Function CheckOpponentConnection(ByVal code As Integer) As Integer
        If HTTPConnectClient.IsBusy = False Then
            Return CInt(HTTPConnectClient.DownloadString(ServerBaseURI & "/checkconnection.php?code=" & code))
        Else
            Return 0
        End If
    End Function

    Public Sub DisplayCode(ByVal LAN As Boolean)
        PvPHTTP.Show()
        If LAN = False Then
            With PvPHTTP.GameCodePanel
                .Width = PvPHTTP.ClientRectangle.Width
                .Height = PvPHTTP.ClientRectangle.Height - PvPHTTP.PicFormHeader.Height
                .Top = PvPHTTP.PicFormHeader.Height
                .Left = 0
                .Parent = PvPHTTP
                .BringToFront()
                .BackColor = Color.Transparent
            End With
            With PvPHTTP.LabGameCode
                .Parent = PvPHTTP.GameCodePanel
                .Width = .Parent.ClientRectangle.Width
                .Height = .Parent.ClientRectangle.Height / 2 - PvPHTTP.LabActualCode.Height / 2 - 50
                .Top = 0
                .Left = 0
                .BringToFront()
                .BackColor = Color.Transparent
            End With
            With PvPHTTP.LabActualCode
                .Parent = PvPHTTP.GameCodePanel
                .Height = 50
                .Width = .Parent.Width
                .Top = PvPHTTP.LabGameCode.Height
                .Left = 0
                .BringToFront()
                .BackColor = Color.Transparent
                .Text = HTTPGameCode
            End With
            With PvPHTTP.LabExplanation
                .Parent = PvPHTTP.GameCodePanel
                .Height = .Parent.ClientRectangle.Height / 2 - PvPHTTP.LabActualCode.Height / 2
                .Width = .Parent.ClientRectangle.Width
                .Top = PvPHTTP.LabGameCode.Height + PvPHTTP.LabActualCode.Height
                .Left = 0
                .BringToFront()
                .Width -= 40
                .Left += 20
                .BackColor = Color.Transparent
            End With
            If PvPHTTP.ConnectionBackgroundWorker.IsBusy = False Then
                PvPHTTP.ConnectionBackgroundWorker.RunWorkerAsync()
            End If
        Else
        End If
    End Sub

    Public Sub ConnectToHTTP(ByVal code As String)
        If IsNumeric(code) Then
            Dim ResultString As String = HTTPClient.DownloadString(ServerBaseURI & "/joingame.php" & "?code=" & code)
            Select Case ResultString
                Case "Error 1", "Error 2", "Error 3"
                    MsgBox("We're sorry; the server is experiencing problems right now. Please try again later.")
                Case "none"
                    MsgBox("No game with that code.")
                Case "occupied"
                    MsgBox("This game has already started.")
                Case "found"
                    'IsGameStarter = 1
                    'PvPHTTP.GamePanel.Show()
                    'PvPHTTP.BWPanel.Show()
                    'PvPHTTP.GameCodePanel.Hide()
                    'PvPHTTP.Show()
                    HTTPGameCode = CInt(code)
                    PvPHTTP.CheckStatusBackgroundWorker.RunWorkerAsync()
                Case Else
                    MsgBox(ResultString)
            End Select
        Else
            MsgBox("The code must be numeric.")
        End If
    End Sub

End Module
