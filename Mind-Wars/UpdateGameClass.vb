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

End Class
