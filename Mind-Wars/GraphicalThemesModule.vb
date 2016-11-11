Option Strict On
Option Infer Off
Option Explicit On
Module GraphicalThemesModule

    Public ButtonPvEList, PvEColorList, ButtonSettingsList, PvPColorList As New List(Of PictureBox)
    Public PvEDifficultyList, ButtonLabList, SettingsLabList, PvPLabList As New List(Of Label)
    Public DefaultLabelColor As Color = Color.FromArgb(255, 192, 255, 255)
    Public DefaultSelectedLabelColor As Color = Color.LightCyan
    Public ImageList As New List(Of System.Drawing.Bitmap)

    Public Sub PopulateImageList(ByVal theme As Integer)
        ImageList.Clear()
        Select Case theme
            Case 0
                ImageList.Add(My.Resources.StartScreenBG)
                ImageList.Add(My.Resources.ButtonBorderActive1)
                ImageList.Add(My.Resources.ButtonBorderInactive)
                ImageList.Add(My.Resources.ButtonBackActive1)
                ImageList.Add(My.Resources.ButtonBackInactive)
                ImageList.Add(My.Resources.SettingsButtonActive)
                ImageList.Add(My.Resources.SettingsButtonInactive)
                ImageList.Add(My.Resources.NumberSettings00)
                ImageList.Add(My.Resources.NumberSettings10)
                ImageList.Add(My.Resources.NumberSettings11)
        End Select
    End Sub

    Public Sub InitializeTheme()
        For Each MainLab As Label In ButtonLabList
            Dim ParentPic As PictureBox = DirectCast(MainLab.Parent, PictureBox)
            ParentPic.Image = ImageList(2)
            MainLab.ForeColor = DefaultLabelColor
        Next
        StartScreen.PicClosePvE.Image = ImageList(3)
        StartScreen.PicPvEChooseColors.Image = ImageList(6)
        StartScreen.PicPvEChooseHoles.Image = ImageList(7)
        StartScreen.PicPvEChooseAttempts.Image = ImageList(7)
        StartScreen.PicPvEStartGame.Image = ImageList(6)

        StartScreen.PicHTTPClose.Image = ImageList(3)
        For Each HTTPLab As Label In PvPLabList
            HTTPLab.Image = ImageList(6)
            HTTPLab.ForeColor = DefaultLabelColor
        Next
        StartScreen.LabHTTPHolesCaption.ForeColor = Color.Gray
        StartScreen.LabHTTPAttempts.ForeColor = DefaultLabelColor
        StartScreen.LabHTTPHoles.ForeColor = Color.Gray
        StartScreen.PicHTTPHoles.Image = ImageList(7)
        StartScreen.PicHTTPAttempts.Image = ImageList(7)
    End Sub

    'Public Sub ChangeTheme(ByVal Theme As Integer)

    '    Select Case Theme
    '        Case 0
    '            Theme_FormBackground = My.Resources.StartScreenBG
    '        Case 1
    '            Theme_FormBackground = My.Resources.BGtema1
    '        Case 2
    '            Theme_FormBackground = My.Resources.BGtema2
    '        Case 3
    '            Theme_FormBackground = My.Resources.BGtema3

    '        Case 4
    '        Case 5
    '    End Select
    '    StartScreen.BackgroundImage = Theme_FormBackground
    'End Sub

    'Public Sub UpdateTheme()
    '    StartScreen.BackgroundImage = Theme_FormBackground
    'End Sub

End Module
