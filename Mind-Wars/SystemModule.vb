Module SystemModule
    Public holes As Integer = 4 '!!!!!!TEST
    Public colours As Integer
    Public tries As Integer
    Public solution() As Integer
    Public guess() As Integer
    Public Sub GameSetup(ByVal h As Integer, ByVal c As Integer, ByVal t As Integer)
        holes = h
        colours = c
        tries = t
    End Sub
End Module
