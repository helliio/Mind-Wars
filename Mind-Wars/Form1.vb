Option Strict On
Option Explicit On
Option Infer Off

Imports System.ComponentModel
Imports System.Net

Public Class StartScreen

    Dim PanelList As New List(Of Panel)

    Dim PvEHoles As Integer = 4, PvEColors As Integer = 6, PvEAttempts As Integer = 10, FocusedPvEColorListIndex As Integer = 5, SelectedPvEDifficulty As Integer = 1
    Dim SelectedButtonListIndex, VisiblePanel, CursorX, CursorY, FocusedLabelAddColor, PvEFocusedCategory, HTTPFocusedCategory, HTTPFocusedSubCategory, HTTPSelectedMode, SelectedHTTPListIndex, SelectedPvEListIndex As Integer

    Dim DragForm As Boolean = False, FocusedLabelColorIncreasing As Boolean = True
    Dim FocusedLabel As Label
    Dim ConnectionCode As String

    Private Sub StartScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Call PopulateImageList(0)
        With PanelList
            .Add(PanelPvE)
            .Add(PanelPvP2)
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
        Call InitializeGUI()
    End Sub

    Private Sub InitializeGUI()
        Me.Hide()
        Me.MinimumSize = ButtonsPanel.Size
        LabCode2.Text = "CODE"
        LabSettings.Parent = PicStartButton_Settings
        LabPvE.Parent = PicStartButton_PvE
        LabPvPLan.Parent = PicStartButton_PvPLan
        LabPvPHTTP.Parent = PicStartButton_PvPHTTP
        LabTutorial.Parent = PicStartButton_Tutorial
        With LabStatusTitle
            .ForeColor = Color.LimeGreen
            .Hide()
        End With
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
    Sub SelectButton(ByVal deselect As Boolean)
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
            Case 2
                ChangeIndex = SelectedPvEListIndex
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
                ChangeIndex = SelectedHTTPListIndex
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
                            Case 2
                                If deselect = True Then
                                    LabCode2.Image = ImageList(6)
                                    If LabCode2.Text = "" Then
                                        LabCode2.Text = "CODE"
                                    End If
                                Else
                                    LabCode2.Image = ImageList(5)
                                End If
                            Case 3
                                If deselect = True Then
                                    LabHTTPConnect2.Image = ImageList(6)
                                Else
                                    LabHTTPConnect2.Image = ImageList(5)
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

    Sub ButtonMouseEnter(sender As Object, e As EventArgs) Handles LabSettings.MouseEnter, LabPvE.MouseEnter, LabPvPLan.MouseEnter, LabPvPHTTP.MouseEnter
        Dim SenderLab As Label = DirectCast(sender, Label)
        If Not SenderLab.TabIndex = SelectedButtonListIndex Then
            Call SelectButton(True)
            SelectedButtonListIndex = SenderLab.TabIndex
            Call SelectButton(False)
        End If
    End Sub

    Private Sub StartScreen_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Select Case VisiblePanel
                Case 0
                    Close()
                Case 2
                    Call TogglePanels(PanelPvE)
                Case 4
                    Call TogglePanels(PanelPvP2)
                Case 5
                    Call TogglePanels(PanelTutorial)
            End Select
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
                    Case Keys.Back
                        If SelectedHTTPListIndex = 2 AndAlso LabCode2.Text.Length > 0 Then
                            LabCode2.Text = LabCode2.Text.Remove(LabCode2.Text.Length - 1)
                        End If
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
                                Select Case SelectedHTTPListIndex
                                    Case 1
                                        Call SelectButton(True)
                                        SelectedHTTPListIndex = 2
                                        Call SelectButton(False)
                                        'LabCode2.Focus()
                                    Case 2
                                        Call SelectButton(True)
                                        SelectedHTTPListIndex = 3
                                        Call SelectButton(False)
                                        'LabHTTPConnect2.Focus()
                                End Select
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
                                Select Case SelectedHTTPListIndex
                                    Case 3
                                        Call SelectButton(True)
                                        SelectedHTTPListIndex = 2
                                        'LabCode2.Focus()
                                        Call SelectButton(False)
                                    Case 2
                                        Call SelectButton(True)
                                        SelectedHTTPListIndex = 1
                                        Call SelectButton(False)
                                        'LabHTTPJoin2.Focus()
                                End Select
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
                    Case Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9
                        If Not IsNumeric(LabCode2.Text) Then
                            LabCode2.Text = ""
                        End If
                        If SelectedHTTPListIndex = 2 AndAlso LabCode2.Text.Length < 4 Then
                            Select Case e.KeyCode
                                Case Keys.D0
                                    LabCode2.Text &= 0
                                Case Keys.D1
                                    LabCode2.Text &= 1
                                Case Keys.D2
                                    LabCode2.Text &= 2
                                Case Keys.D3
                                    LabCode2.Text &= 3
                                Case Keys.D4
                                    LabCode2.Text &= 4
                                Case Keys.D5
                                    LabCode2.Text &= 5
                                Case Keys.D6
                                    LabCode2.Text &= 6
                                Case Keys.D7
                                    LabCode2.Text &= 7
                                Case Keys.D8
                                    LabCode2.Text &= 8
                                Case Keys.D9
                                    LabCode2.Text &= 9
                            End Select
                        End If
                End Select
            Case 5
                If e.KeyCode = Keys.Space OrElse e.KeyCode = Keys.Enter Then
                    Call TogglePanels(PanelTutorial)
                End If
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
                If Not SelectedButtonListIndex = 0 AndAlso Not SelectedButtonListIndex = 4 Then
                    TogglePanels(PanelList(SelectedButtonListIndex))
                ElseIf SelectedButtonListIndex = 0 Then
                    Singleplayer.Show()
                End If
                Debug.Print("TOGGLING " & CStr(SelectedButtonListIndex))
                Debug.Print("VisiblePanel = " & VisiblePanel)
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
                        'LabCode2.Focus()
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
                        Else
                            HTTPFocusedSubCategory = 0
                            PicHTTPHoles.Image = ImageList(8)
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
                        'holes = PvEHoles
                        colours = PvEColors
                        tries = PvEAttempts
                        holes = 4
                        Debug.Print("STARTING GAME")
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

    Private Sub ButtonClick(sender As Object, e As EventArgs) Handles LabSettings.Click, LabPvE.Click, LabPvPLan.Click, LabPvPHTTP.Click, LabHTTPJoin2.Click, LabHTTPConnect2.Click, LabHTTPNewGame.Click, LabHTTPColors2.Click, LabHTTPCreate2.Click, LabHTTPAttempts.Click, LabHTTPAttemptsCaption.Click, LabHTTPHoles.Click, LabHTTPHolesCaption.Click
        Call ResetCurrentlyEntered()
        Select Case VisiblePanel
            Case 4
                SelectedHTTPListIndex = HoveredControlIndex
        End Select
        Call EnterSelected()
    End Sub

    Private Sub ClosePanel(senderX As Object, e As EventArgs) Handles PicClosePvE.Click
        Call TogglePanels(PanelList(VisiblePanel))
        'sender.Parent.Hide()
    End Sub

    Dim PanelInvalidated As Boolean = False, PanelControlsVisible As Boolean = True
    Private Sub ShowMainOnPaint(senderX As Object, e As PaintEventArgs) Handles PanelPvE.Paint, PanelTutorial.Paint, PanelPvP2.Paint
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


    Sub JoinGame()
        If IsNumeric(LabCode2.Text) AndAlso LabCode2.Text.Length = 4 AndAlso Not JoinBackgroundWorker.IsBusy Then
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
            With LabStatusTitle
                .Hide()
                .Dock = DockStyle.None
                .ForeColor = Color.LimeGreen
                .Parent = PicLoading
                .Width = PanelPvP2.Width
                .Left = 0
                .Height = 40
                .Top = 20
                .Text = "Connecting"
                .Show()
            End With
            ConnectionCode = LabCode2.Text
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

    Private Sub JoinBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles JoinBackgroundWorker.DoWork
        Dim JoinWebClient As New WebClient
        Dim ResultString As String = JoinWebClient.DownloadString(ServerBaseURI & "/joingame.php" & "?code=" & ConnectionCode)
        Select Case ResultString
            Case "Error 1", "Error 2", "Error 3"
                ConnectionErrorString = "Could not connect"
                ConnectionErrorDescription = "Please notify us about this error: " & ResultString
            Case "none"
                ConnectionErrorString = "Game not found"
                ConnectionErrorDescription = "There is no game with that code"
            Case "occupied"
                ConnectionErrorString = "Oops!"
                ConnectionErrorDescription = "This game has already started"
            Case "found"
                ConnectionErrorString = ""
                ConnectionEstablished = True
                IsGameStarter = 1
            Case Else
                Debug.Print(ResultString & " !!! Form1")
        End Select

    End Sub

    Private Sub PicMinimizeForm_Click(sender As Object, e As EventArgs) Handles PicMinimizeForm.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub LabCode2_Click(sender As Object, e As EventArgs) Handles LabCode2.Click
        'LabCode2.Text = ""
        'LabCode2.Focus()
        Call SelectButton(True)
        SelectedHTTPListIndex = 2
        Call SelectButton(False)
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

    Private Sub JoinBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles JoinBackgroundWorker.RunWorkerCompleted
        If ConnectionEstablished = True Then
            ConnectionFailureCounter = 0
            LoadingSpinTimer.Enabled = False
            PicLoading.Hide()
            Me.Hide()
            PvPHTTP.Show()
            PicLoading.Hide()
            LabStatusTitle.Hide()
            HTTPGameCode = CInt(ConnectionCode)
            Call PvPHTTP.InitializePvPGame()
        Else
            ConnectionFailureCounter += 1
            If ConnectionErrorString = "Could not connect" Then
                If ConnectionFailureCounter >= 3 Then
                    With LabStatusTitle
                        .Hide()
                        .Parent = PanelPvP2
                        '.Top = LoadingGraphicsRect.Y
                        .Height = LoadingGraphicsRect.Height
                        '.Width = 200
                        '.Left = CInt(.Parent.Width / 2 - 100)
                        .Dock = DockStyle.Bottom
                        .Text = "Could not connect: " & vbNewLine & "Please check your internet connection"
                        .BringToFront()
                        .ForeColor = Color.Red
                        .Show()
                    End With
                    LoadingSpinTimer.Enabled = False
                    PicLoading.Hide()
                Else
                    JoinBackgroundWorker.RunWorkerAsync()
                End If
            Else
                ConnectionFailureCounter = 0
                PicLoading.Hide()
                LoadingSpinTimer.Enabled = False
                With LabStatusTitle
                    .Hide()
                    .Parent = PanelPvP2
                    .Text = ConnectionErrorString & vbNewLine & ConnectionErrorDescription
                    .BringToFront()
                    .ForeColor = Color.Red
                    .Dock = DockStyle.Bottom
                    .Show()
                End With
            End If
        End If
    End Sub
    Private Sub PicLoading_Paint(sender As Object, e As PaintEventArgs) Handles PicLoading.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.DrawArc(InitializeGMPPen, LoadingGraphicsRect, LoadingSpinRotation, 170)
        e.Graphics.DrawArc(InitializeGMPPen, LoadingGraphicsRect, LoadingSpinRotation + 180, 170)
    End Sub
End Class
