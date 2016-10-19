Module GraphicalThemesModule
    Public Theme_FormBackground As Image = My.Resources.StartScreenBG
    Public Theme_ButtonBorderActive As Image = My.Resources.ButtonBorderActive1
    Public Theme_ButtonBorderInactive As Image = My.Resources.ButtonBorderInactive
    Public Theme_ButtonBackActive As Image = My.Resources.ButtonBackActive1
    Public Theme_ButtonBackInactive As Image = My.Resources.ButtonBackInactive
    Public Theme_SettingsButtonActive As Image = My.Resources.SettingsButtonActive
    Public Theme_SettingsButtonInactive As Image = My.Resources.SettingsButtonInactive

    Public Sub InitiateTheme()
        StartScreen.PicStartButton_Settings.Tag = "StartButton"
        StartScreen.PicStartButton_PvE.Tag = "StartButton"
        StartScreen.PicStartButton_PvPLan.Tag = "StartButton"
        StartScreen.PicStartButton_PvPHTTP.Tag = "StartButton"
        StartScreen.PicStartButton_Tutorial.Tag = "StartButton"

        StartScreen.PicClosePvE.Tag = "ClosePanel"
        StartScreen.PicClosePvPHTTP.Tag = "ClosePanel"
        StartScreen.PicClosePvPLAN.Tag = "ClosePanel"
        StartScreen.PicCloseSettings.Tag = "ClosePanel"
        StartScreen.PicCloseTutorial.Tag = "ClosePanel"

        StartScreen.PicSettingsTheme.Tag = "SettingsButton"
        StartScreen.PicPvEChooseColors.Tag = "SettingsButton"
        StartScreen.PicPvEStartGame.Tag = "SettingsButton"

        StartScreen.PicPvEChooseHoles.Tag = "SettingsButtonSplit"
        StartScreen.PicPvEChooseAttempts.Tag = "SettingsButtonSplit"
    End Sub


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

    Public Sub UpdateTheme()
        StartScreen.BackgroundImage = Theme_FormBackground
    End Sub

End Module
