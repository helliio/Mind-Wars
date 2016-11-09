Option Strict On
Option Explicit On
Option Infer Off

Imports System.ComponentModel
Imports System.Net

Public Class StartScreen

    Dim PanelList As New List(Of Panel)

    Dim PvEHoles As Integer = 4, PvEColors As Integer = 6, PvEAttempts As Integer = 10, FocusedPvEColorListIndex As Integer = 5, SelectedPvEDifficulty As Integer = 1
    Dim SelectedButtonListIndex, SelectedSettingsListIndex, VisiblePanel, CursorX, CursorY, FocusedLabelAddColor, PvEFocusedCategory, SelectedPvEListIndex As Integer

    Dim DragForm As Boolean = False, FocusedLabelColorIncreasing As Boolean = True
    Dim FocusedLabel As Label
    Dim ConnectionCode As String



    Private Sub StartScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Call PopulateImageList(0)
        With PanelList
            .Add(PanelSettings)
            .Add(PanelPvE)
            .Add(PanelPvPLan)
            .Add(PanelPvPHTTP)
            .Add(PanelTutorial)
        End With
        With PvEColorList
            .Add(PicPvEColor1)
            .Add(PicPvEColor2)
            .Add(PicPvEColor3)
            .Add(PicPvEColor4)
            .Add(PicPvEColor5)
            .Add(PicPvEColor6)
            .Add(PicPvEColor7)
            .Add(PicPvEColor8)
        End With
        With ButtonLabList
            .Add(LabSettings)
            .Add(LabPvE)
            .Add(LabPvPLan)
            .Add(LabPvPHTTP)
            .Add(LabTutorial)
        End With
        With ButtonPvEList
            .Add(PicClosePvE)
            .Add(PicDifficulty1)
            .Add(PicDifficulty2)
            .Add(PicDifficulty3)
            .Add(PicPvEChooseColors)
            .Add(PicPvEChooseHoles)
            .Add(PicPvEChooseAttempts)
            .Add(PicPvEStartGame)
        End With
        With PvEDifficultyList
            .Add(LabPvEEasy)
            .Add(LabPvEHard)
            .Add(LabPvEImpossible)
        End With
        With ButtonSettingsList
            .Add(PicCloseSettings)
        End With
        With SettingsLabList
            .Add(LabSettingsSound)
            .Add(LabSettingsTheme)
        End With
        Call InitializeGUI()
        Call PlayLoopingBackgroundSoundFile(1)
    End Sub

    Private Sub InitializeGUI()
        Me.Hide()
        Me.MinimumSize = ButtonsPanel.Size
        txtCode.Top = -100
        txtCode.Text = "CODE"

        LabSettings.Parent = PicStartButton_Settings
        LabPvE.Parent = PicStartButton_PvE
        LabPvPLan.Parent = PicStartButton_PvPLan
        LabPvPHTTP.Parent = PicStartButton_PvPHTTP
        LabTutorial.Parent = PicStartButton_Tutorial
        LabSettingsTheme.Parent = PicSettingsTheme
        LabSettingsSound.Parent = PicSettingSound
        For Each lab As Label In ButtonLabList
            With lab
                .Left = 0
                .Top = 0
                .Size = .Parent.ClientSize
            End With
        Next
        For Each lab As Label In SettingsLabList
            With lab
                .Left = 0
                .Top = 0
                .Size = .Parent.ClientSize
            End With
        Next

        PicCloseForm.Parent = PicFormHeader
        PicMinimizeForm.Parent = PicFormHeader

        For Each ColPal As PictureBox In PvEColorList
            ColPal.Parent = PanelPvEColors
        Next

        PanelSettings.Dock = DockStyle.Fill
        PanelSettings.BringToFront()

        PanelPvE.Dock = DockStyle.Fill
        PanelPvE.Parent = Me
        PanelPvE.BringToFront()
        PanelPvPLan.Dock = DockStyle.Fill
        PanelPvPLan.BringToFront()
        PanelPvPHTTP.Dock = DockStyle.Fill
        PanelPvPHTTP.BringToFront()
        PanelTutorial.Dock = DockStyle.Fill
        PanelTutorial.BringToFront()
        PicFormHeader.Dock = DockStyle.Top
        ButtonsPanel.SendToBack()

        PicDifficulty1.BringToFront()
        PicDifficulty1.Parent = PvEDifficultyPanel

        PicDifficulty2.BringToFront()
        PicDifficulty2.Parent = PvEDifficultyPanel

        PicDifficulty3.BringToFront()
        PicDifficulty3.Parent = PvEDifficultyPanel

        LabPvEEasy.Parent = PicDifficulty1
        LabPvEHard.Parent = PicDifficulty2
        LabPvEImpossible.Parent = PicDifficulty3
        For Each lab As Label In PvEDifficultyList
            With lab
                .ForeColor = Color.White
                .Location = .Parent.Location
                .BringToFront()
                .TextAlign = ContentAlignment.MiddleCenter
                .Dock = DockStyle.Fill
            End With
        Next
        With LabPvEColors
            .Parent = PicPvEChooseColors
            .BringToFront()
            .Location = .Parent.Location
            .TextAlign = ContentAlignment.MiddleCenter
            .Dock = DockStyle.Fill
        End With
        With LabPvENumberOfHolesButton
            .Parent = PicPvEChooseHoles
            .BringToFront()
            .Dock = DockStyle.Left
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        With LabPvENumberOfHoles
            .Parent = PicPvEChooseHoles
            .BringToFront()
            .Dock = DockStyle.Right
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        With LabPvENumberOfAttemptsButton
            .Parent = PicPvEChooseAttempts
            .BringToFront()
            .Dock = DockStyle.Left
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        With LabPvENumberOfAttempts
            .Parent = PicPvEChooseAttempts
            .BringToFront()
            .Dock = DockStyle.Right
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        With LabPvEStart
            .Parent = PicPvEStartGame
            .TextAlign = ContentAlignment.MiddleCenter
            .BringToFront()
            .Dock = DockStyle.Fill
        End With

        With LabCode
            .Parent = PanelPvPHTTP
            .Width = .Parent.ClientRectangle.Width
            .Height = My.Resources.SettingsButtonActive.Height
            .Top = 10
            .Left = 0
        End With
        With cmdConnectHTTP
            .Parent = PanelPvPHTTP
            .Width = .Parent.ClientRectangle.Width
            .Height = My.Resources.ButtonBorderInactive.Height
            .Top = LabCode.Height + LabCode.Top + 10
            .Left = 0
        End With
        With cmdNewPrivateGame
            .Parent = PanelPvPHTTP
            .Width = .Parent.ClientRectangle.Width
            .Height = My.Resources.ButtonBorderInactive.Height
            .Top = cmdConnectHTTP.Height + cmdConnectHTTP.Top + 10
            .Left = 0
        End With
        With cmdNewPublicGame
            .Parent = PanelPvPHTTP
            .Width = .Parent.ClientRectangle.Width
            .Height = My.Resources.ButtonBorderInactive.Height
            .Top = cmdNewPrivateGame.Height + cmdNewPrivateGame.Top + 10
            .Left = 0
        End With
        With HeaderTransparencyLeft
            .Parent = PicFormHeader
            .Left = 0
            .Top = 0
            .BringToFront()
            .Width = 12
            .Height = 12
            .BackColor = Color.Transparent
        End With
        With HeaderTransparencyRight
            .Parent = PicFormHeader
            .Left = Me.ClientRectangle.Width - 12
            .Top = 0
            .BringToFront()
            .Width = 12
            .Height = 12
            .BackColor = Color.Transparent
        End With
        Call SelectButton(False)
        Call InitializeTheme()
        GUITimer.Enabled = True
    End Sub

    Sub SelectButton(ByVal deselect As Boolean)
        Select Case VisiblePanel
            Case 0
                Dim Selection As Label = ButtonLabList.Item(SelectedButtonListIndex)
                Dim ParentPic As PictureBox = DirectCast(Selection.Parent, PictureBox)
                If deselect = True Then
                    With Selection
                        ParentPic.Image = ImageList(2)
                        .ForeColor = Color.SteelBlue
                    End With
                Else
                    With Selection
                        ParentPic.Image = ImageList(1)
                        .ForeColor = Color.LightCyan
                    End With
                End If
                'Selection.Invalidate()
            Case 1
                Dim Selection As PictureBox = ButtonSettingsList.Item(SelectedSettingsListIndex)
                If deselect = True Then
                    With Selection
                        If SelectedSettingsListIndex = 0 Then
                            .Image = ImageList(4)
                        Else
                            .Image = ImageList(3)
                            .ForeColor = Color.SteelBlue
                        End If
                    End With
                Else
                    With Selection
                        If SelectedSettingsListIndex = 0 Then
                            .Image = ImageList(1)
                        Else
                            .ForeColor = Color.LightCyan
                        End If
                    End With
                End If
            Case 2
                Dim Selection As PictureBox = ButtonPvEList.Item(SelectedPvEListIndex)
                If SelectedPvEListIndex = 1 Or SelectedPvEListIndex = 2 Or SelectedPvEListIndex = 3 Then
                    Selection.Invalidate()
                ElseIf SelectedPvEListIndex = 0 Then
                    If deselect = True Then
                        Selection.Image = ImageList(4)
                    Else
                        Selection.Image = ImageList(3)
                    End If
                ElseIf SelectedPvEListIndex = 4 OrElse SelectedPvEListIndex = 7 Then
                    If deselect = True Then
                        Selection.Image = ImageList(6)
                    Else
                        Selection.Image = ImageList(5)
                    End If
                ElseIf SelectedPvEListIndex = 5 Then
                    If deselect = True Then
                        Selection.Image = ImageList(7)
                        LabPvENumberOfHolesButton.ForeColor = Color.LightSkyBlue
                    Else
                        LabPvENumberOfHolesButton.ForeColor = Color.LightCyan
                        Selection.Image = ImageList(8)
                    End If
                ElseIf SelectedPvEListIndex = 6 Then
                    If deselect = True Then
                        LabPvENumberOfAttemptsButton.ForeColor = Color.LightSkyBlue
                        Selection.Image = ImageList(7)
                    Else
                        LabPvENumberOfAttemptsButton.ForeColor = Color.LightCyan
                        Selection.Image = ImageList(8)
                    End If
                End If
        End Select
    End Sub

    Sub ButtonMouseEnter(sender As Object, e As EventArgs) Handles LabSettings.MouseEnter, LabPvE.MouseEnter, LabPvPLan.MouseEnter, LabPvPHTTP.MouseEnter
        Dim SenderLab As Label = DirectCast(sender, Label)
        Select Case VisiblePanel
            Case 0
                If Not SenderLab.TabIndex = SelectedButtonListIndex Then
                    Call SelectButton(True)
                    SelectedButtonListIndex = SenderLab.TabIndex
                    Call SelectButton(False)
                End If
            Case 1
                If Not CInt(SenderLab.Tag) = SelectedSettingsListIndex Then
                    Call SelectButton(True)
                    SelectedSettingsListIndex = CInt(SenderLab.Tag)
                    Call SelectButton(False)
                End If
        End Select
    End Sub



    Private Sub StartScreen_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            If Not VisiblePanel = 0 Then
                TogglePanels(PanelList(VisiblePanel))
                VisiblePanel = 0
                e.Handled = True
            Else
                Close()
            End If
        End If

        Select Case VisiblePanel
            Case 0
                Select Case e.KeyCode
                    Case Keys.Up
                        If Not SelectedButtonListIndex = 0 Then
                            Call SelectButton(True)
                            SelectedButtonListIndex -= 1
                            Call SelectButton(False)
                        End If
                    Case Keys.Down
                        If Not SelectedButtonListIndex = ButtonLabList.Count - 1 Then
                            Call SelectButton(True)
                            SelectedButtonListIndex += 1
                            Call SelectButton(False)
                        End If
                    Case Keys.Tab
                        Call SelectButton(True)
                        If Not SelectedButtonListIndex = ButtonLabList.Count - 1 Then
                            SelectedButtonListIndex += 1
                        Else
                            SelectedButtonListIndex = 0
                        End If
                        Call SelectButton(False)
                    Case Keys.Space, Keys.Enter
                        Call EnterSelected()
                End Select
            Case 1
            Case 2
                Select Case e.KeyCode
                    Case Keys.Down
                        Select Case PvEFocusedCategory
                            Case 0

                                If Not SelectedPvEListIndex = ButtonPvEList.Count - 1 Then
                                    Call SelectButton(True)
                                    If SelectedPvEListIndex = 1 Then
                                        SelectedPvEListIndex += 3
                                    ElseIf SelectedPvEListIndex = 2 Then
                                        SelectedPvEListIndex += 2
                                    ElseIf SelectedPvEListIndex = 0 Then
                                        SelectedPvEListIndex = SelectedPvEDifficulty
                                    Else
                                        SelectedPvEListIndex += 1
                                    End If
                                    Call SelectButton(False)
                                End If
                            Case 1
                                Call EnterSelected()
                                Call SelectButton(True)
                                SelectedPvEListIndex += 1
                                Call SelectButton(False)
                            Case 2
                                If PvEHoles > 3 Then
                                    PvEHoles -= 1
                                    LabPvENumberOfHoles.Text = CStr(PvEHoles)
                                End If
                            Case 3
                                If PvEAttempts > 4 Then
                                    PvEAttempts -= 1
                                    LabPvENumberOfAttempts.Text = CStr(PvEAttempts)
                                End If
                        End Select
                    Case Keys.Up
                        If PvEFocusedCategory = 0 Then
                            If Not SelectedPvEListIndex = 0 Then
                                Call SelectButton(True)
                                If SelectedPvEListIndex = 2 Then
                                    SelectedPvEListIndex -= 2
                                ElseIf SelectedPvEListIndex = 3 Then
                                    SelectedPvEListIndex -= 3
                                ElseIf SelectedPvEListIndex = 4 Then
                                    SelectedPvEListIndex = SelectedPvEDifficulty
                                Else
                                    SelectedPvEListIndex -= 1
                                End If
                                Call SelectButton(False)
                            End If
                        ElseIf PvEFocusedCategory = 1 Then
                            Call EnterSelected()
                            Call SelectButton(True)
                            SelectedPvEListIndex = SelectedPvEDifficulty
                            Call SelectButton(False)
                        ElseIf PvEFocusedCategory = 2 Then
                            If PvEHoles < 8 Then
                                PvEHoles += 1
                                LabPvENumberOfHoles.Text = CStr(PvEHoles)
                            End If
                        ElseIf PvEFocusedCategory = 3 Then
                            If PvEAttempts < 14 Then
                                PvEAttempts += 1
                                LabPvENumberOfAttempts.Text = CStr(PvEAttempts)
                            End If
                        End If
                    Case Keys.Left
                        Select Case PvEFocusedCategory
                            Case 0
                                Select Case SelectedPvEListIndex
                                    Case 2, 3
                                        Call SelectButton(True)
                                        SelectedPvEDifficulty -= 1
                                        SelectedPvEListIndex -= 1
                                        Call SelectButton(False)
                                    Case 4
                                        Call EnterSelected()
                                        If FocusedPvEColorListIndex > 2 Then
                                            FocusedPvEColorListIndex -= 1
                                            Call SelectColor()
                                        End If
                                End Select
                            Case 1
                                If FocusedPvEColorListIndex > 2 Then
                                    FocusedPvEColorListIndex -= 1
                                    Call SelectColor()
                                End If
                            Case 2, 3
                                Call EnterSelected()
                        End Select
                    Case Keys.Right
                        Select Case PvEFocusedCategory
                            Case 0
                                Select Case SelectedPvEListIndex
                                    Case 1, 2
                                        Call SelectButton(True)
                                        SelectedPvEDifficulty += 1
                                        SelectedPvEListIndex += 1
                                        Call SelectButton(False)
                                    Case 4
                                        Call EnterSelected()
                                        If FocusedPvEColorListIndex < 7 Then
                                            FocusedPvEColorListIndex += 1
                                            Call SelectColor()
                                        End If
                                    Case 5, 6
                                        Call EnterSelected()
                                End Select
                            Case 1
                                If FocusedPvEColorListIndex < 7 Then
                                    FocusedPvEColorListIndex += 1
                                    Call SelectColor()
                                End If
                        End Select
                    Case Keys.Space, Keys.Enter
                        Select Case PvEFocusedCategory
                            Case 0
                                Call EnterSelected()
                                Debug.Print("Enter selected " & SelectedPvEListIndex)
                            Case 1, 2
                                Call EnterSelected()
                        End Select
                End Select
        End Select
    End Sub

    Sub SelectColor()
        PvEColors = FocusedPvEColorListIndex + 1
        For Each ColPal As PictureBox In PvEColorList
            ColPal.Invalidate()
        Next
    End Sub

    Sub EnterSelected()
        Select Case VisiblePanel
            Case 0
                TogglePanels(PanelList(SelectedButtonListIndex))
                Debug.Print("TOGGLING " & CStr(SelectedButtonListIndex))
                'Select Case SelectedButtonListIndex
                '    Case 0
                '        VisiblePanel = 1
                '        TogglePanels(PanelSettings)
                '        'PanelSettings.Show()
                '    Case 1
                '        VisiblePanel = 2
                '        TogglePanels(PanelPvE)
                '        'PanelPvE.Show()
                '    Case 2
                '        VisiblePanel = 3
                '        PanelPvPLan.Show()
                '    Case 3
                '        VisiblePanel = 4
                '        PanelPvPHTTP.Show()
                '    Case 4
                '        VisiblePanel = 5
                '        PanelTutorial.Show()
                'End Select
            Case 1
                Select Case SelectedSettingsListIndex
                    Case 0
                        PanelSettings.Hide()
                        VisiblePanel = 0
                End Select
            Case 2
                Select Case SelectedPvEListIndex
                    Case 0
                        TogglePanels(PanelPvE)
                        'PanelPvE.Hide()
                        'VisiblePanel = 0
                    Case 1, 2, 3
                        SelectButton(True)
                        SelectedPvEListIndex = 7
                        SelectButton(False)
                    Case 4
                        If Not PvEFocusedCategory = 1 Then
                            PvEFocusedCategory = 1
                            'For Each ColPal As PictureBox In PvEColorList
                            '    ColPal.Invalidate()
                            'Next
                            PanelPvEColors.Invalidate()
                        Else
                            PvEFocusedCategory = 0
                            For Each ColPal As PictureBox In PvEColorList
                                ColPal.Invalidate()
                            Next
                        End If
                    Case 5
                        If Not PvEFocusedCategory = 2 Then
                            PvEFocusedCategory = 2
                            PicPvEChooseHoles.Image = ImageList(9)
                            LabPvENumberOfHolesButton.ForeColor = Color.LightSkyBlue
                            LabPvENumberOfHoles.ForeColor = Color.LightCyan
                        Else
                            PvEFocusedCategory = 0
                            PicPvEChooseHoles.Image = ImageList(8)
                            LabPvENumberOfHolesButton.ForeColor = Color.LightCyan
                            LabPvENumberOfHoles.ForeColor = Color.LightSkyBlue
                        End If
                    Case 6
                        If Not PvEFocusedCategory = 3 Then
                            PvEFocusedCategory = 3
                            PicPvEChooseAttempts.Image = ImageList(9)
                            LabPvENumberOfAttemptsButton.ForeColor = Color.LightSkyBlue
                            LabPvENumberOfAttempts.ForeColor = Color.LightCyan
                        Else
                            PvEFocusedCategory = 0
                            PicPvEChooseAttempts.Image = ImageList(8)
                            LabPvENumberOfAttemptsButton.ForeColor = Color.LightCyan
                            LabPvENumberOfAttempts.ForeColor = Color.LightSkyBlue
                        End If
                    Case 7
                        PvEAttempts = CInt(LabPvENumberOfAttempts.Text)
                        Call GameSetup(PvEHoles, PvEColors, PvEAttempts)
                        Debug.Print("Holes: " & holes)
                        Me.Hide()
                        PanelPvE.Hide()
                        PvEFocusedCategory = 0
                        VisiblePanel = 0
                        PvEGame.Show()
                End Select

        End Select
    End Sub

    Private Sub ButtonClick(sender As Object, e As EventArgs) Handles LabSettings.Click, LabPvE.Click, LabPvPLan.Click, LabPvPHTTP.Click
        Call EnterSelected()
    End Sub

    Private Sub ClosePanel(senderX As Object, e As EventArgs) Handles PicClosePvE.Click, PicClosePvPLAN.Click, PicCloseTutorial.Click
        Dim sender As PictureBox = DirectCast(senderX, PictureBox)
        Dim sender_Panel As Panel = DirectCast(sender.Parent, Panel)
        Call TogglePanels(sender_Panel)
        'sender.Parent.Hide()
    End Sub

    Dim PanelInvalidated As Boolean = False, PanelControlsVisible As Boolean = True
    Private Sub ShowMainOnPaint(senderX As Object, e As PaintEventArgs) Handles PanelPvE.Paint, PanelPvPHTTP.Paint, PanelSettings.Paint, PanelTutorial.Paint, PanelPvPLan.Paint
        If PanelControlsVisible = False Then
            Dim sender As Panel = DirectCast(senderX, Panel)
            If PanelInvalidated = False Then
                'For Each obj As Control In ButtonsPanel.Controls
                '    obj.Show()
                'Next
                PanelInvalidated = True
                sender.Invalidate()
            Else
                VisiblePanel = 0
                sender.Hide()
                For Each obj As Control In ButtonsPanel.Controls
                    obj.Show()
                Next
                PanelInvalidated = False
            End If
        End If
    End Sub

    Private Sub TogglePanels(SenderPanel As Panel)
        If SenderPanel.Visible = False Then
            VisiblePanel = CInt(SenderPanel.Tag)
            ButtonsPanel.Hide()
            For Each obj As Control In ButtonsPanel.Controls
                obj.Hide()
            Next
            ButtonsPanel.Show()
            PanelControlsVisible = True
            SenderPanel.Hide()

            For Each obj As Control In SenderPanel.Controls
                obj.Show()
            Next
            SenderPanel.Show()
            'ButtonsPanel.Hide()
        Else
            ButtonsPanel.Show()
            SenderPanel.Hide()
            For Each obj As Control In SenderPanel.Controls
                obj.Hide()
            Next
            PanelControlsVisible = False
            SenderPanel.Show()
        End If
    End Sub

    Private Sub PicFormHeader_MouseDown(sender As Object, e As MouseEventArgs) Handles PicFormHeader.MouseDown
        If e.Button = MouseButtons.Left Then
            DragForm = True
            CursorX = Cursor.Position.X - Me.Left
            CursorY = Cursor.Position.Y - Me.Top
        End If
    End Sub

    Private Sub PicFormHeader_MouseMove(sender As Object, e As MouseEventArgs) Handles PicFormHeader.MouseMove
        If DragForm = True Then
            Me.Left = Cursor.Position.X - CursorX
            Me.Top = Cursor.Position.Y - CursorY
        End If
    End Sub

    Private Sub GUITimer_Tick(sender As Object, e As EventArgs) Handles GUITimer.Tick
        FocusedLabel = ButtonLabList.Item(SelectedButtonListIndex)
        FocusedLabel.ForeColor = Color.FromArgb(255, 150 + CInt((FocusedLabelAddColor / 1.5)), 230 + CInt((FocusedLabelAddColor / 4)), 255)
        If FocusedLabelColorIncreasing = True Then
            If FocusedLabelAddColor >= 100 Then
                FocusedLabelAddColor = 100
                FocusedLabelColorIncreasing = False
            Else
                FocusedLabelAddColor += 5
            End If
        Else
            If FocusedLabelAddColor <= 0 Then
                FocusedLabelAddColor = 0
                FocusedLabelColorIncreasing = True
            Else
                FocusedLabelAddColor -= 5
            End If
        End If
    End Sub

    Private Sub PicCloseForm_Click(sender As Object, e As EventArgs) Handles PicCloseForm.Click
        Me.Close()
    End Sub

    Private Sub PicFormHeader_MouseUp(sender As Object, e As MouseEventArgs) Handles PicFormHeader.MouseUp
        DragForm = False
    End Sub

    Private Sub PicMinimizeForm_MouseEnter(sender As Object, e As EventArgs) Handles PicMinimizeForm.MouseEnter
        PicMinimizeForm.Image = My.Resources.MinimizeHover
    End Sub

    Private Sub PicMinimizeForm_MouseLeave(sender As Object, e As EventArgs) Handles PicMinimizeForm.MouseLeave
        PicMinimizeForm.Image = My.Resources.Minimize
    End Sub

    Private Sub PicCloseForm_MouseEnter(sender As Object, e As EventArgs) Handles PicCloseForm.MouseEnter
        PicCloseForm.Image = My.Resources.ExitHover
    End Sub

    Private Sub PicCloseForm_MouseLeave(sender As Object, e As EventArgs) Handles PicCloseForm.MouseLeave
        PicCloseForm.Image = My.Resources.Exit1
    End Sub

    Private Sub PicDifficulty_Paint(senderX As Object, e As PaintEventArgs) Handles PicDifficulty1.Paint, PicDifficulty2.Paint, PicDifficulty3.Paint
        Dim sender As PictureBox = DirectCast(senderX, PictureBox)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        DifficultyDrawRect = sender.DisplayRectangle
        DifficultyDrawRect.Inflate(-1, -1)
        Dim SenderTag As Integer = CInt(sender.Tag)

        If CInt(SenderTag) = SelectedPvEListIndex Then
            EasyBrush.Color = Color.FromArgb(50, Color.Cyan)
            e.Graphics.FillRectangle(EasyBrush, DifficultyDrawRect)
        ElseIf CInt(SenderTag) = SelectedPvEDifficulty Then
            EasyBrush.Color = Color.FromArgb(30, Color.Cyan)
            e.Graphics.FillRectangle(EasyBrush, DifficultyDrawRect)
        End If
        ' What?

    End Sub

    Private Sub PicTheme_Paint(sender As Object, e As PaintEventArgs) Handles PicTheme1.Paint, PicTheme2.Paint, PicTheme3.Paint
        Dim SenderPic As PictureBox = DirectCast(sender, PictureBox)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        ThemeDrawRect = SenderPic.DisplayRectangle
        ThemeDrawRect.Inflate(-1, -1)
        If CStr(SenderPic.Tag) = "test" Then
            e.Graphics.DrawEllipse(Pens.Red, ThemeDrawRect)
        Else
            e.Graphics.DrawEllipse(Pens.LightCyan, ThemeDrawRect)
        End If
    End Sub

    Private Sub PicFormHeader_Click(sender As Object, e As EventArgs) Handles PicFormHeader.Click
        If HTTPBackgroundWorker.IsBusy = False Then
            HTTPBackgroundWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub cmdConnectHTTP_Click(sender As Object, e As EventArgs) Handles cmdConnectHTTP.Click
        ' SHOW "STAND BY" MESSAGE
        If IsNumeric(txtCode.Text) AndAlso txtCode.Text.Length = 4 AndAlso Not JoinBackgroundWorker.IsBusy Then
            ConnectionCode = txtCode.Text
            JoinBackgroundWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub HTTPBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles HTTPBackgroundWorker.DoWork
        Dim NewGame As New CreateHTTPGameClass
        Dim NewGameThread As New System.Threading.Thread(AddressOf NewGame.Create)
        NewGameThread.IsBackground = True
        NewGameThread.Start()
        NewGameThread.Join()
    End Sub

    Private Sub JoinBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles JoinBackgroundWorker.DoWork
        Dim JoinWebClient As New WebClient
        Dim ResultString As String = JoinWebClient.DownloadString(ServerBaseURI & "/joingame.php" & "?code=" & ConnectionCode)
        Select Case ResultString
            Case "Error 1", "Error 2", "Error 3"
                MsgBox("We're sorry; the server is experiencing problems right now. Please try again later.")
            Case "none"
                MsgBox("No game with that code.")
            Case "occupied"
                MsgBox("This game has already started.")
            Case "found"
                ConnectionEstablished = True
                IsGameStarter = 1
            Case Else
                MsgBox(ResultString & " !!! Form1")
        End Select

    End Sub

    Private Sub PicSound_Paint(senderX As Object, e As PaintEventArgs) Handles PicSound1.Paint, PicSound2.Paint, PicSound3.Paint
        Dim sender As PictureBox = DirectCast(senderX, PictureBox)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        SoundDrawRect = sender.DisplayRectangle
        SoundDrawRect.Inflate(-1, -1)
        If CStr(sender.Tag) = "test" Then
            e.Graphics.DrawEllipse(Pens.Red, SoundDrawRect)
        Else
            e.Graphics.DrawEllipse(Pens.LightCyan, SoundDrawRect)
        End If
    End Sub

    Private Sub PicMinimizeForm_Click(sender As Object, e As EventArgs) Handles PicMinimizeForm.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub cmdTestTheme_Click(sender As Object, e As EventArgs) Handles cmdTestTheme.Click
        Dim themeint As Integer = CInt(ThemeComboBox.SelectedValue)
        'Call ChangeTheme(0)
    End Sub

    Private Sub LabCode_Click(sender As Object, e As EventArgs) Handles LabCode.Click
        txtCode.Clear()
        txtCode.Focus()
    End Sub

    Private Sub txtCode_TextChanged(sender As Object, e As EventArgs) Handles txtCode.TextChanged
        LabCode.Text = txtCode.Text
    End Sub

    Private Sub PicPvEColorPalette_Paint(sender As Object, e As PaintEventArgs) Handles PicPvEColor1.Paint, PicPvEColor2.Paint, PicPvEColor3.Paint, PicPvEColor4.Paint, PicPvEColor5.Paint, PicPvEColor6.Paint, PicPvEColor7.Paint, PicPvEColor8.Paint
        Dim SenderPic As PictureBox = DirectCast(sender, PictureBox)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        ColorPaletteRect.Size = SenderPic.DisplayRectangle.Size
        ColorPaletteRect.Location = SenderPic.DisplayRectangle.Location
        Dim SenderTag As Integer = CInt(SenderPic.Tag)

        If PvEFocusedCategory = 1 Then
            If SenderTag > FocusedPvEColorListIndex + 1 Then
                ColorPaletteBrush.Color = Color.FromArgb(150, ColorCodes(SenderTag))
                ColorPaletteRect.Inflate(-4, -4)
            Else 'If SenderTag <= FocusedPvEColorListIndex + 1 Then
                ColorPaletteBrush.Color = ColorCodes(SenderTag)
                ColorPaletteRect.Inflate(-1, -1)
            End If
        Else
            If SenderTag > FocusedPvEColorListIndex + 1 Then
                ColorPaletteBrush.Color = Color.FromArgb(40, ColorCodes(SenderTag))
                ColorPaletteRect.Inflate(-4, -4)
            Else
                ColorPaletteBrush.Color = Color.FromArgb(170, ColorCodes(SenderTag))
                ColorPaletteRect.Inflate(-1, -1)
            End If
        End If
        e.Graphics.FillEllipse(ColorPaletteBrush, ColorPaletteRect)
    End Sub

    Private Sub StartScreen_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        End
    End Sub

    Private Sub HTTPBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles HTTPBackgroundWorker.RunWorkerCompleted
        If CreateGameSuccess = True Then
            Call DisplayCode(False)
            IsGameStarter = 2
        End If
    End Sub

    Private Sub StartScreen_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub txtCode_GotFocus(sender As Object, e As EventArgs) Handles txtCode.GotFocus
        LabCode.Image = My.Resources.SettingsButtonActive
    End Sub

    Private Sub txtCode_LostFocus(sender As Object, e As EventArgs) Handles txtCode.LostFocus
        LabCode.Image = My.Resources.SettingsButtonInactive
        If txtCode.Text = "" Then
            txtCode.Text = "CODE"
        End If
    End Sub
    Private Sub LabCode_MouseEnter(sender As Object, e As EventArgs) Handles LabCode.MouseEnter
        LabCode.Image = My.Resources.SettingsButtonActive
    End Sub

    Private Sub LabCode_MouseLeave(sender As Object, e As EventArgs) Handles LabCode.MouseLeave
        If txtCode.Focused = False Then
            LabCode.Image = My.Resources.SettingsButtonInactive
        End If
    End Sub

    Private Sub JoinBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles JoinBackgroundWorker.RunWorkerCompleted
        If ConnectionEstablished = True Then
            PvPHTTP.Show()
            HTTPGameCode = CInt(ConnectionCode)
            Call PvPHTTP.InitializePvPGame()
        Else
            JoinBackgroundWorker.RunWorkerAsync()
        End If

    End Sub
End Class
