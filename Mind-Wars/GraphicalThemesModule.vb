Module GraphicalThemesModule
    Public Theme_FormBackground As Image

    Public Sub ChangeTheme(ByVal Theme As Integer)

        Select Case Theme
            Case 0
                Theme_FormBackground = My.Resources.StartScreenBG
            Case 1
                Theme_FormBackground = My.Resources.BGtema1
            Case 2

            Case 3
            Case 4
            Case 5
        End Select
        StartScreen.BackgroundImage = Theme_FormBackground


    End Sub
End Module
