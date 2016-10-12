Public Class StartScreen
    Private Sub StartScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Changes the label's parent to the picturebox behind it, so that the transparent background will display the picturebox, rather than the form itself
        Call ButtonsGUI()
    End Sub

    Private Sub ButtonsGUI()
        With LabSettings
            .Parent = PicStartButton_Settings
            .Parent.BackColor = Color.DarkGreen
            .Height = PicStartButton_Settings.Height
            .Left = 0
            .Top = 0
        End With
        With LabPvE
            .Parent = PicStartButton_PvE
            .Parent.BackColor = Color.DarkGreen
            .Height = PicStartButton_PvE.Height
            .Left = 0
            .Top = 0
        End With
    End Sub

    ' Handle the MouseEnter and MouseLeave events for the buttons without excessive code
    Sub ButtonMouseEnter(sender As Object, e As EventArgs) Handles LabPvE.MouseEnter, LabSettings.MouseEnter
        sender.Parent.BackColor = Color.LimeGreen
    End Sub
    Sub ButtonMouseLeave(sender As Object, e As EventArgs) Handles LabPvE.MouseLeave, LabSettings.MouseLeave
        sender.Parent.BackColor = Color.DarkGreen
    End Sub

End Class
