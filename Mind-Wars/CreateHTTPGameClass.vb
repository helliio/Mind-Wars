Option Strict On
Option Explicit On
Option Infer Off
Public Class CreateHTTPGameClass
    Public Sub Create()
        Dim ResultString As String = HTTPClient.DownloadString(ServerBaseURI & "/creategame.php")
        Select Case ResultString
            Case "Error 1"
            Case "Error 2"
            Case Else
                If IsNumeric(ResultString) Then
                    HTTPGameCode = CInt(ResultString)
                    CreateGameSuccess = True
                Else
                    Debug.Print(ResultString)
                End If
        End Select
    End Sub
End Class
