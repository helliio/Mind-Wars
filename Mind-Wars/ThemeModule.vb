Module ThemeModule
    Public Theme_FormBackground As Image
    Public Sub ChangeTheme(ByVal Theme As Integer)

        Select Case Theme
            Case 0
                Theme_FormBackground = My.Resources.StartScreenBG
            Case 1
                ThemeResources(0) = My.Resources.BGtema1
            Case 2
            Case 3

        End Select


    End Sub
End Module
