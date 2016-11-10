Option Strict On
Option Explicit On
Option Infer Off

Imports System.ComponentModel
Imports System.Net

Public Class StartScreen

    Dim PanelList As New List(Of Panel)

    Dim PvEHoles As Integer = 4, PvEColors As Integer = 6, PvEAttempts As Integer = 10, FocusedPvEColorListIndex As Integer = 5, SelectedPvEDifficulty As Integer = 1
    Dim SelectedButtonListIndex, SelectedSettingsListIndex, VisiblePanel, CursorX, CursorY, FocusedLabelAddColor, PvEFocusedCategory, HTTPFocusedCategory, HTTPFocusedSubCategory, HTTPSelectedMode, SelectedHTTPListIndex, SelectedPvEListIndex As Integer

    Dim DragForm As Boolean = False, FocusedLabelColorIncreasing As Boolean = True
    Dim FocusedLabel As Label
    Dim ConnectionCode As String



    Private Sub StartScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Call PopulateImageList(0)
        With PanelList
            .Add(PanelSettings)
            .Add(PanelPvE)
            .Add(PanelPvP2)
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
        With PvPColorList
            .Add(HTTPCol1)
            .Add(HTTPCol2)
            .Add(HTTPCol3)
            .Add(HTTPCol4)
            .Add(HTTPCol5)
            .Add(HTTPCol6)
            .Add(HTTPCol7)
            .Add(HTTPCol8)
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
        With PvPLabList
            .Add(LabHTTPJoin2)
            .Add(LabCode2)
            .Add(LabHTTPConnect2)
            .Add(LabHTTPNewGame)
            .Add(LabHTTPColors2)
            .Add(LabHTTPCreate2)
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
        txtCode2.Top = -100
        txtCode2.Text = "CODE"

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
        For Each ColPal As PictureBox In PvPColorList
            ColPal.Parent = HTTPColorsPanel
        Next

        'PanelPvE.Dock = DockStyle.Fill
        'PanelPvE.Parent = Me
        'PanelPvE.BringToFront()
        'PanelPvPLan.Dock = DockStyle.Fill
        'PanelPvPLan.BringToFront()
        'PanelPvPHTTP.Dock = DockStyle.Fill
        'PanelPvPHTTP.BringToFront()
        'PanelPvP2.Dock = DockStyle.Fill

        'PanelTutorial.Dock = DockStyle.Fill
        'PanelTutorial.BringToFront()
        For Each P As Panel In PanelList
            P.Dock = DockStyle.Fill
            P.Parent = Me
            P.BringToFront()
        Next
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
        With LabPvENumberOfHoles
            .Parent = PicPvEChooseHoles
            .BringToFront()
            .Width = 50
            .Dock = DockStyle.Right
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        With LabPvENumberOfHolesButton
            .Parent = PicPvEChooseHoles
            .BringToFront()
            .Width = .Parent.ClientRectangle.Width - 52
            .Dock = DockStyle.Left
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        With LabHTTPHoles
            .Parent = PicHTTPHoles
            .BringToFront()
            .Width = 60
            .Dock = DockStyle.Right
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        With LabHTTPHolesCaption
            .Parent = PicHTTPHoles
            .BringToFront()
            .Width = .Parent.ClientRectangle.Width - 52
            .Dock = DockStyle.Left
            .TextAlign = ContentAlignment.MiddleRight
        End With
        With LabPvENumberOfAttemptsButton
            .Parent = PicPvEChooseAttempts
            .BringToFront()
            .Width = .Parent.ClientRectangle.Width - 52
            .Dock = DockStyle.Left
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        With LabPvENumberOfAttempts
            .Parent = PicPvEChooseAttempts
            .BringToFront()
            .Width = 50
            .Dock = DockStyle.Right
            .TextAlign = ContentAlignment.MiddleCenter
        End With
        With LabHTTPAttemptsCaption
            .Parent = PicHTTPAttempts
            .BringToFront()
            .Width = .Parent.ClientRectangle.Width - 52
            .Dock = DockStyle.Left
            .TextAlign = ContentAlignment.MiddleRight
        End With
        With LabHTTPAttempts
            .Parent = PicHTTPAttempts
            .BringToFront()
            .Width = 60
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

    Dim HoveredControlIndex As Integer = 0
    Sub SelectButton(ByVal deselect As Boolean, Optional ByVal HoverMode As Boolean = False)
        Dim ChangeIndex As Integer = 0
        Select Case VisiblePanel
            Case 0
                ChangeIndex = SelectedButtonListIndex
                Dim Selection As Label = ButtonLabList.Item(ChangeIndex)
                Dim ParentPic As PictureBox = DirectCast(Selection.Parent, PictureBox)
                If deselect = True Then
                    With Selection
                        ParentPic.Image = ImageList(2)
                        .ForeColor = DefaultLabelColor
                    End With
                Else
                    With Selection
                        ParentPic.Image = ImageList(1)
                        .ForeColor = DefaultSelectedLabelColor
                    End With
                End If
                'Selection.Invalidate()
            Case 1
                If HoverMode = True Then
                    ChangeIndex = HoveredControlIndex
                Else
                    ChangeIndex = SelectedSettingsListIndex
                End If

                Dim Selection As PictureBox = ButtonSettingsList.Item(ChangeIndex)
                If deselect = True Then
                    With Selection
                        If ChangeIndex = 0 Then
                            .Image = ImageList(4)
                        Else
                            .Image = ImageList(3)
                            .ForeColor = DefaultLabelColor
                        End If
                    End With
                Else
                    With Selection
                        If ChangeIndex = 0 Then
                            .Image = ImageList(1)
                        Else
                            .ForeColor = DefaultSelectedLabelColor
                        End If
                    End With
                End If
            Case 2
                If HoverMode = True Then
                    ChangeIndex = HoveredControlIndex
                Else
                    ChangeIndex = SelectedPvEListIndex
                End If
                Dim Selection As PictureBox = ButtonPvEList.Item(ChangeIndex)
                If ChangeIndex = 1 Or ChangeIndex = 2 Or ChangeIndex = 3 Then
                    Selection.Invalidate()
                ElseIf ChangeIndex = 0 Then
                    If deselect = True Then
                        Selection.Image = ImageList(4)
                    Else
                        Selection.Image = ImageList(3)
                    End If
                ElseIf ChangeIndex = 4 OrElse ChangeIndex = 7 Then
                    If deselect = True Then
                        Selection.Image = ImageList(6)
                    Else
                        Selection.Image = ImageList(5)
                    End If
                ElseIf ChangeIndex = 5 Then
                    If deselect = True Then
                        Selection.Image = ImageList(7)
                        LabPvENumberOfHolesButton.ForeColor = DefaultLabelColor
                    Else
                        LabPvENumberOfHolesButton.ForeColor = DefaultSelectedLabelColor
                        Selection.Image = ImageList(8)
                    End If
                ElseIf ChangeIndex = 6 Then
                    If deselect = True Then
                        LabPvENumberOfAttemptsButton.ForeColor = DefaultLabelColor
                        Selection.Image = ImageList(7)
                    Else
                        LabPvENumberOfAttemptsButton.ForeColor = DefaultSelectedLabelColor
                        Selection.Image = ImageList(8)
                    End If
                End If
            Case 4
                If HoverMode = True Then
                    ChangeIndex = HoveredControlIndex
                Else
                    ChangeIndex = SelectedHTTPListIndex
                End If
                Select Case HTTPFocusedCategory
                    Case 0
                        Select Case ChangeIndex
                            Case 0
                                If deselect = True Then
                                    PicHTTPClose.Image = ImageList(4)
                                Else
                                    PicHTTPClose.Image = ImageList(3)
                                End If
                            Case 1, 4
                                If deselect = True Then
                                    PvPLabList(ChangeIndex - 1).Image = ImageList(6)
                                Else
                                    PvPLabList(ChangeIndex - 1).Image = ImageList(5)
                                End If
                        End Select
                    Case 1
                        Select Case ChangeIndex
                            Case 1
                                If deselect = True Then
                                    LabHTTPJoin2.Image = ImageList(4)
                                Else
                                    LabHTTPJoin2.Image = ImageList(3)
                                End If
                            Case 2, 3
                                If deselect = True Then
                                    PvPLabList(ChangeIndex - 1).Image = ImageList(6)
                                Else
                                    PvPLabList(ChangeIndex - 1).Image = ImageList(5)
                                End If
                        End Select
                    Case 2
                        Select Case ChangeIndex
                            Case 4
                                If deselect = True Then
                                    LabHTTPNewGame.Image = ImageList(4)
                                Else
                                    LabHTTPNewGame.Image = ImageList(3)
                                End If
                            Case 5
                                If deselect = True Then
                                    LabHTTPColors2.Image = ImageList(6)
                                Else
                                    LabHTTPColors2.Image = ImageList(5)
                                End If
                            Case 6
                                If deselect = True Then
                                    PicHTTPHoles.Image = ImageList(7)
                                Else
                                    PicHTTPHoles.Image = ImageList(8)
                                End If
                            Case 7
                                If deselect = True Then
                                    PicHTTPAttempts.Image = ImageList(7)
                                Else
                                    PicHTTPAttempts.Image = ImageList(8)
                                End If
                            Case 8
                                If deselect = True Then
                                    LabHTTPCreate2.Image = ImageList(6)
                                Else
                                    LabHTTPCreate2.Image = ImageList(5)
                                End If
                        End Select
                End Select
        End Select
    End Sub

    Sub ButtonMouseEnter(sender As Object, e As EventArgs) Handles LabSettings.MouseEnter, LabPvE.MouseEnter, LabPvPLan.MouseEnter, LabPvPHTTP.MouseEnter, LabHTTPJoin2.MouseEnter, LabCode2.MouseEnter, LabHTTPConnect2.MouseEnter, LabHTTPNewGame.MouseEnter, LabHTTPColors2.MouseEnter, LabHTTPCreate2.MouseEnter, LabHTTPHoles.MouseEnter, LabHTTPHolesCaption.MouseEnter, LabHTTPAttempts.MouseEnter, LabHTTPAttemptsCaption.MouseEnter
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
                    If Not HoveredControlIndex = SelectedSettingsListIndex Then
                        Call SelectButton(True, True)
                    End If
                    HoveredControlIndex = CInt(SenderLab.Tag)
                    Call SelectButton(False, True)
                End If
                    Case 4
                If Not CInt(SenderLab.Tag) = 0 Then
                    If Not CInt(SenderLab.Tag) = SelectedHTTPListIndex Then
                        If Not HoveredControlIndex = SelectedHTTPListIndex Then
                            Call SelectButton(True, True)
                        End If
                        HoveredControlIndex = CInt(SenderLab.Tag)
                        Call SelectButton(False, True)
                    End If
                Else
                    Dim ParentPic As PictureBox = DirectCast(SenderLab.Parent, PictureBox)
                    If Not CInt(ParentPic.Tag) = SelectedHTTPListIndex Then
                        If Not HoveredControlIndex = SelectedHTTPListIndex Then
                            Call SelectButton(True, True)
                        End If
                        HoveredControlIndex = CInt(ParentPic.Tag)
                        Call SelectButton(False, True)
                    End If
                End If
        End Select
    End Sub

    Sub PicMouseEnter(sender As Object, e As EventArgs) Handles PicHTTPClose.MouseEnter, PicHTTPHoles.MouseEnter, PicHTTPAttempts.MouseEnter
        Dim SenderPic As PictureBox = DirectCast(sender, PictureBox)
        Select Case VisiblePanel
            Case 1
                If Not CInt(SenderPic.Tag) = SelectedSettingsListIndex Then
                    Call SelectButton(True)
                    SelectedSettingsListIndex = CInt(SenderPic.Tag)
                    Call SelectButton(False)
                End If
            Case 4
                If Not CInt(SenderPic.Tag) = SelectedHTTPListIndex Then
                    Call SelectButton(True)
                    SelectedHTTPListIndex = CInt(SenderPic.Tag)
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
            Case 4
                Debug.Print(CStr(SelectedHTTPListIndex))
                Select Case e.KeyCode
                    Case Keys.Down
                        Select Case HTTPFocusedCategory
                            Case 0
                                Select Case SelectedHTTPListIndex
                                    Case 0
                                        Call SelectButton(True)
                                        SelectedHTTPListIndex = 1
                                        Call SelectButton(False)
                                    Case 1
                                        Call SelectButton(True)
                                        SelectedHTTPListIndex = 4
                                        Call SelectButton(False)
                                End Select
                            Case 1
                                If SelectedHTTPListIndex < 3 Then
                                    Call SelectButton(True)
                                    SelectedHTTPListIndex += 1
                                    Call SelectButton(False)
                                End If
                            Case 2
                                Select Case HTTPFocusedSubCategory
                                    Case 0
                                        If Not SelectedHTTPListIndex = 8 Then
                                            Debug.Print("Deselecting " & SelectedHTTPListIndex)
                                            Call SelectButton(True)
                                            SelectedHTTPListIndex += 1
                                            Call SelectButton(False)
                                            Debug.Print("Selecting " & SelectedHTTPListIndex)
                                        End If
                                    Case 1
                                        Debug.Print("Entering/exiting " & SelectedHTTPListIndex)
                                        Call EnterSelected()
                                        Call SelectButton(True)
                                        SelectedHTTPListIndex += 1
                                        Call SelectButton(False)
                                    Case 2
                                        If PvEHoles > 3 Then
                                            PvEHoles -= 1
                                            LabHTTPHoles.Text = CStr(PvEHoles)
                                        End If
                                    Case 3
                                        If PvEAttempts > 4 Then
                                            PvEAttempts -= 1
                                            LabHTTPAttempts.Text = CStr(PvEAttempts)
                                        End If
                                End Select
                        End Select
                    Case Keys.Up
                        Select Case HTTPFocusedCategory
                            Case 0
                                Select Case SelectedHTTPListIndex
                                    Case 1
                                        Call SelectButton(True)
                                        SelectedHTTPListIndex = 0
                                        Call SelectButton(False)
                                    Case 4
                                        Call SelectButton(True)
                                        SelectedHTTPListIndex = 1
                                        Call SelectButton(False)
                                End Select
                            Case 1
                                If SelectedHTTPListIndex > 1 Then
                                    Call SelectButton(True)
                                    SelectedHTTPListIndex -= 1
                                    Call SelectButton(False)
                                End If
                            Case 2
                                Select Case HTTPFocusedSubCategory
                                    Case 0
                                        If Not SelectedHTTPListIndex = 4 Then
                                            Call SelectButton(True)
                                            SelectedHTTPListIndex -= 1
                                            Call SelectButton(False)
                                        End If
                                    Case 1
                                        Call EnterSelected()
                                    Case 2
                                        If PvEHoles < 8 Then
                                            PvEHoles += 1
                                            LabHTTPHoles.Text = CStr(PvEHoles)
                                        End If
                                    Case 3
                                        If PvEAttempts < 12 Then
                                            PvEAttempts += 1
                                            LabHTTPAttempts.Text = CStr(PvEAttempts)
                                        End If
                                End Select
                        End Select
                    Case Keys.Left
                        Select Case HTTPFocusedSubCategory
                            Case 0
                                Select Case SelectedHTTPListIndex
                                    Case 5
                                        Call EnterSelected()
                                        If FocusedPvEColorListIndex > 3 Then
                                            FocusedPvEColorListIndex -= 1
                                            Call SelectColor()
                                        End If
                                End Select
                            Case 1
                                If FocusedPvEColorListIndex > 3 Then
                                    FocusedPvEColorListIndex -= 1
                                    Call SelectColor()
                                End If
                            Case 2, 3
                                Call EnterSelected()
                        End Select
                    Case Keys.Right
                        Select Case HTTPFocusedSubCategory
                            Case 0
                                Select Case SelectedHTTPListIndex
                                    Case 5
                                        Call EnterSelected()
                                        If FocusedPvEColorListIndex < 7 Then
                                            FocusedPvEColorListIndex += 1
                                            Call SelectColor()
                                        End If
                                    Case 6, 7
                                        Call EnterSelected()
                                End Select
                            Case 1
                                If FocusedPvEColorListIndex < 7 Then
                                    FocusedPvEColorListIndex += 1
                                    Call SelectColor()
                                End If
                        End Select
                    Case Keys.Space, Keys.Enter
                        Call EnterSelected()
                End Select
        End Select
    End Sub

    Sub SelectColor()
        PvEColors = FocusedPvEColorListIndex + 1
        If VisiblePanel = 2 Then
            For Each ColPal As PictureBox In PvEColorList
                ColPal.Invalidate()
            Next
        ElseIf VisiblePanel = 4 Then
            For Each ColPal As PictureBox In PvPColorList
                ColPal.Invalidate()
            Next
        End If
    End Sub


    Sub EnterSelected()
        Select Case VisiblePanel
            Case 0
                TogglePanels(PanelList(SelectedButtonListIndex))
                Debug.Print("TOGGLING " & CStr(SelectedButtonListIndex))
                Debug.Print("VisiblePanel = " & VisiblePanel)
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
                    Case 1, 2, 3
                        SelectButton(True)
                        SelectedPvEListIndex = 7
                        SelectButton(False)
                    Case 4
                        If Not PvEFocusedCategory = 1 Then
                            PvEFocusedCategory = 1
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
            Case 4
                Select Case SelectedHTTPListIndex
                    Case 0
                        TogglePanels(PanelPvP2)
                    Case 1
                        If Not HTTPFocusedCategory = 1 Then
                            LabHTTPJoin2.Text = ""
                            LabHTTPJoin2.Image = ImageList(3)
                            LabHTTPNewGame.Hide()
                            HTTPJoinPanel.Show()
                            PicHTTPClose.Hide()
                            HTTPFocusedCategory = 1
                        Else
                            LabHTTPJoin2.Text = "Join existing game"
                            LabHTTPJoin2.Image = ImageList(5)
                            LabHTTPNewGame.Show()
                            HTTPJoinPanel.Hide()
                            PicHTTPClose.Show()
                            HTTPFocusedCategory = 0
                        End If
                    Case 2
                        txtCode2.Focus()
                    Case 3
                        Call JoinGame()
                    Case 4
                        If Not HTTPFocusedCategory = 2 Then
                            PicHTTPClose.Hide()
                            LabHTTPJoin2.Hide()
                            HTTPCreateGamePanel.Show()
                            LabHTTPNewGame.Text = ""
                            LabHTTPNewGame.Image = ImageList(3)
                            HTTPFocusedCategory = 2
                        Else
                            HTTPCreateGamePanel.Hide()
                            PicHTTPClose.Show()
                            LabHTTPJoin2.Show()
                            LabHTTPNewGame.Text = "Create new game"
                            LabHTTPNewGame.Image = ImageList(5)
                            HTTPFocusedCategory = 0
                        End If
                    Case 5
                        If Not HTTPFocusedSubCategory = 1 Then
                            HTTPFocusedSubCategory = 1
                            HTTPColorsPanel.Invalidate()
                        Else
                            HTTPFocusedSubCategory = 0
                            HTTPColorsPanel.Invalidate()
                        End If
                    Case 6
                        If Not HTTPFocusedSubCategory = 2 Then
                            HTTPFocusedSubCategory = 2
                            PicHTTPHoles.Image = ImageList(9)
                            LabHTTPHolesCaption.ForeColor = Color.LightSkyBlue
                            LabHTTPHoles.ForeColor = DefaultSelectedLabelColor
                        Else
                            HTTPFocusedSubCategory = 0
                            PicHTTPHoles.Image = ImageList(8)
                            LabHTTPHolesCaption.ForeColor = DefaultLabelColor
                            LabHTTPHoles.ForeColor = Color.LightSkyBlue
                        End If
                    Case 7
                        If Not HTTPFocusedSubCategory = 3 Then
                            HTTPFocusedSubCategory = 3
                            PicHTTPAttempts.Image = ImageList(9)
                            LabHTTPAttemptsCaption.ForeColor = Color.LightSkyBlue
                            LabHTTPAttempts.ForeColor = DefaultSelectedLabelColor
                        Else
                            HTTPFocusedSubCategory = 0
                            PicHTTPAttempts.Image = ImageList(8)
                            LabHTTPAttemptsCaption.ForeColor = DefaultLabelColor
                            LabHTTPAttempts.ForeColor = Color.LightSkyBlue
                        End If
                    Case 8
                        PvEAttempts = CInt(LabPvENumberOfAttempts.Text)
                        holes = PvEHoles
                        colours = PvEColors
                        tries = PvEAttempts
                        If HTTPBackgroundWorker.IsBusy = False Then
                            HTTPBackgroundWorker.RunWorkerAsync()
                        End If
                End Select
        End Select
    End Sub
    Sub ResetCurrentlyEntered()
        Select Case HTTPFocusedSubCategory
            Case 1
                HTTPColorsPanel.Invalidate()
            Case 2
                PicHTTPHoles.Image = ImageList(8)
                LabHTTPHolesCaption.ForeColor = DefaultLabelColor
                LabHTTPHoles.ForeColor = Color.LightSkyBlue
            Case 3
                PicHTTPAttempts.Image = ImageList(8)
                LabHTTPAttemptsCaption.ForeColor = DefaultLabelColor
                LabHTTPAttempts.ForeColor = Color.LightSkyBlue
        End Select
        HTTPFocusedSubCategory = 0
    End Sub

    Private Sub ButtonClick(sender As Object, e As EventArgs) Handles LabSettings.Click, LabPvE.Click, LabPvPLan.Click, LabPvPHTTP.Click, LabHTTPJoin2.Click, LabCode2.Click, LabHTTPConnect2.Click, LabHTTPNewGame.Click, LabHTTPColors2.Click, LabHTTPCreate2.Click, PicHTTPAttempts.Click, PicHTTPHoles.Click, LabHTTPAttempts.Click, LabHTTPAttemptsCaption.Click, LabHTTPHoles.Click, LabHTTPHolesCaption.Click
        Call ResetCurrentlyEntered()
        Call EnterSelected()
    End Sub

    Private Sub ClosePanel(senderX As Object, e As EventArgs) Handles PicClosePvE.Click, PicClosePvPLAN.Click, PicCloseTutorial.Click, PicClosePvPHTTP.Click
        Call TogglePanels(PanelList(VisiblePanel))
        'sender.Parent.Hide()
    End Sub

    Dim PanelInvalidated As Boolean = False, PanelControlsVisible As Boolean = True
    Private Sub ShowMainOnPaint(senderX As Object, e As PaintEventArgs) Handles PanelPvE.Paint, PanelPvPHTTP.Paint, PanelSettings.Paint, PanelTutorial.Paint, PanelPvPLan.Paint, PanelPvP2.Paint
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

            If Not VisiblePanel = 4 Then
                For Each obj As Control In SenderPanel.Controls
                    obj.Show()
                Next
            Else
                PicHTTPClose.Show()
                LabHTTPJoin2.Show()
                LabHTTPNewGame.Show()
            End If
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

    Sub JoinGame()
        If IsNumeric(txtCode2.Text) AndAlso txtCode2.Text.Length = 4 AndAlso Not JoinBackgroundWorker.IsBusy Then
            With PicLoading
                .Visible = False
                .BackColor = Color.Transparent
                .Parent = PanelPvP2
                .Left = 0
                .Top = 0
                .Dock = DockStyle.Bottom
                .Height = 120
                .BringToFront()
            End With
            With LoadingGraphicsRect
                .Width = 80
                .Height = 80
                .X = CInt(PanelPvP2.ClientRectangle.Width / 2 - 40)
                .Y = PicLoading.Top + 1
            End With
            ConnectionCode = txtCode2.Text
            LoadingSpinTimer.Enabled = True
            PicLoading.Show()
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

    Private Sub LoadingSpinTimer_Tick(sender As Object, e As EventArgs) Handles LoadingSpinTimer.Tick
        PicLoading.Invalidate()
        LoadingSpinRotation += 5
        If LoadingSpinRotation > 360 Then
            LoadingSpinRotation -= 360
        End If
    End Sub

    Private Sub PicLoading_Click(sender As Object, e As EventArgs) Handles PicLoading.Click

    End Sub

    Private Sub JoinBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles JoinBackgroundWorker.DoWork
        Dim JoinWebClient As New WebClient
        Dim ResultString As String = JoinWebClient.DownloadString(ServerBaseURI & "/joingame.php" & "?code=" & ConnectionCode)
        Select Case ResultString
            Case "Error 1", "Error 2", "Error 3"
                ConnectionErrorString = "Could not connect"
            Case "none"
                ConnectionErrorString = "Game not found"
            Case "occupied"
                ConnectionErrorString = "Game already started"
            Case "found"
                ConnectionErrorString = ""
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

    Private Sub PicClosePvPHTTP_Click(sender As Object, e As EventArgs) Handles PicClosePvPHTTP.Click

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
    Private Sub txtCode2_TextChanged(sender As Object, e As EventArgs) Handles txtCode2.TextChanged
        LabCode2.Text = txtCode2.Text
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

    Private Sub PicHTTPColorPalette_Paint(sender As Object, e As PaintEventArgs) Handles HTTPCol1.Paint, HTTPCol2.Paint, HTTPCol3.Paint, HTTPCol4.Paint, HTTPCol5.Paint, HTTPCol6.Paint, HTTPCol7.Paint, HTTPCol8.Paint
        Dim SenderPic As PictureBox = DirectCast(sender, PictureBox)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        ColorPaletteRect.Size = SenderPic.DisplayRectangle.Size
        ColorPaletteRect.Location = SenderPic.DisplayRectangle.Location
        Dim SenderTag As Integer = CInt(SenderPic.Tag)

        If HTTPSelectedMode = 2 Then
            If HTTPFocusedCategory = 1 Then
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
        Else
            If SenderTag > FocusedPvEColorListIndex + 1 Then
                ColorPaletteBrush.Color = Color.FromArgb(30, ColorCodes(SenderTag))
                ColorPaletteRect.Inflate(-4, -4)
            Else
                ColorPaletteBrush.Color = Color.FromArgb(120, ColorCodes(SenderTag))
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
            ConnectionFailureCounter = 0
            LoadingSpinTimer.Enabled = False
            PicLoading.Hide()
            Me.Hide()
            PvPHTTP.Show()
            HTTPGameCode = CInt(ConnectionCode)
            Call PvPHTTP.InitializePvPGame()
        Else
            If ConnectionErrorString = "Could not connect" Then
                JoinBackgroundWorker.RunWorkerAsync()
            End If
        End If

    End Sub

    Private Sub PicLoading_Paint(sender As Object, e As PaintEventArgs) Handles PicLoading.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.DrawArc(InitializeGMPPen, LoadingGraphicsRect, LoadingSpinRotation, 170)
        e.Graphics.DrawArc(InitializeGMPPen, LoadingGraphicsRect, LoadingSpinRotation + 180, 170)
    End Sub
End Class
