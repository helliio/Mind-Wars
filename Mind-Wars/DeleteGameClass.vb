Option Strict On
Option Explicit On
Option Infer Off
Imports System
Imports System.Net
Imports System.IO



Public Class DeleteGameClass
    Public DeleteGameCode As Integer = 0

    Public Sub DeleteGame()
        Dim HTTPDeleteClient As New WebClient
        Try
            Dim ResultString As String = HTTPDeleteClient.DownloadString(ServerBaseURI & "/deletegame.php?code=" & DeleteGameCode)
            Select Case ResultString
                Case "deleted"
                    Debug.Print("Deleted game " & DeleteGameCode)
                Case Else
                    MsgBox(ResultString)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            HTTPDeleteClient.Dispose()
            Threading.Thread.CurrentThread.Abort()
        End Try
    End Sub

End Class
