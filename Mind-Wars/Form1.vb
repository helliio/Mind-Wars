Public Class StartScreen
    ' Remarks:
    ' - When using the word 'button' in this code and these comments, I do not mean the common Windows form control Button.
    ' Button controls are not used in this project. By 'button', I mean 'a clickable element from the user's point of view'.
    ' The ButtonLabList, as you'll see, is a list of all the labels on this form, which, together with the PictureBox controls
    ' behind them, form a "button."

    ' We need a list in order to refer to each button using numbers instead of control names, which is necessary for reasonably
    ' simple code when navigating up and down with the arrow keys.
    Dim ButtonLabList As New List(Of Label)
    Dim ButtonSettingsList As New List(Of PictureBox)

    ' We need to be able to keep track of which button is selected. We're counting from the top, and 0 is the first button from
    ' the top. The upmost button ('Settings') will be selected initially, so we set the value of this variable to 0.
    Dim SelectedButtonListIndex As Integer = 0
    Dim SelectedSettingsListIndex As Integer = 0

    ' We need to know whether or not one of the panels shown by clicking a button is already open, so we can ignore the key
    ' events for the main buttons if it is. I've made this an integer instead of a boolean (we treat 0 as 'no panel is open')
    ' so we can determine which panel should be affected by the key events.
    Dim VisiblePanel As Integer = 0

    ' Our form is borderless, so we need a way to move it. We'll be using the currently gray PictureBox at the top of the form.
    ' We'll tell the form where to move by getting the position of the cursor:
    Dim CursorX As Integer, CursorY As Integer
    ' In addition, in order to prevent the form from following the cursor around after releasing the mouse button, we need to
    ' tell it if it's supposed to move.
    Dim DragForm As Boolean = False

    ' Variables for animation of the ForeColor of the selected button's label:
    Dim FocusedLabelAddColor As Integer = 0
    Dim FocusedLabelColorIncreasing As Boolean = True
    Dim FocusedLabel As Label

    Private Sub StartScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' If we simply placed a Label control in front of a PictureBox control, the PictureBox would be obscured. We could try
        ' to set the Label's BackColor property to 'Transparent', but this would show the Label's parent control instead of
        ' whatever control is behind it. Right now, that parent is the form. Therefore, we need to specify that each Label's
        ' parent is the PictureBox that it's placed in front of. For easy access to the PictureBox controls in the designer,
        ' the Labels are moved slightly, so we need to specify their locations and sizes at runtime. Might as well set every
        ' other non -default property value here as well. This is done within the sub procedure called below.
        ' This is where the labels are added to ButtonLabList, and other GUI properties are set.
        Call InitializeGUI()
        ' Next, we need to actually select the first button from the top. SelectedButtonListIndex is already 0 (zero-based,
        ' meaning the first button is button #0), and no other button is selected, so we call the SelectButton sub procedure
        ' without deselecting any other button first. Notice the '(ByVal deselect as Boolean)' for the SelectButton sub.
        ' If we wanted to deselect a button, we'd set this to 'True', but in this case we'll be setting it to 'False'.
        Call SelectButton(False)


    End Sub

    Private Sub InitializeGUI()
        With LabSettings
            .Parent = PicStartButton_Settings
            .Height = .Parent.Height
            .Left = 0
            .Top = 0
        End With
        With LabPvE
            .Parent = PicStartButton_PvE
            .Height = .Parent.Height
            .Left = 0
            .Top = 0
        End With
        With LabPvPLan
            .Parent = PicStartButton_PvPLan
            .Height = .Parent.Height
            .Left = 0
            .Top = 0
        End With
        With LabPvPHTTP
            .Parent = PicStartButton_PvPHTTP
            .Height = .Parent.Height
            .Left = 0
            .Top = 0
        End With
        With LabTutorial
            .Parent = PicStartButton_Tutorial
            .Height = .Parent.Height
            .Left = 0
            .Top = 0
        End With

        ' Adds the labels to the list, so that they can be accessed without knowing their names.
        With ButtonLabList
            .Add(LabSettings)
            .Add(LabPvE)
            .Add(LabPvPLan)
            .Add(LabPvPHTTP)
            .Add(LabTutorial)
        End With
        With ButtonSettingsList
            .Add(PicCloseSettings)
        End With

        With PicCloseForm
            .Parent = PicFormHeader
        End With
        With PicMinimizeForm
            .Parent = PicFormHeader
        End With

        PanelSettings.Dock = DockStyle.Fill
        PanelSettings.BringToFront()
        PanelPvE.Dock = DockStyle.Fill
        PanelPvE.BringToFront()
        PanelPvPLan.Dock = DockStyle.Fill
        PanelPvPLan.BringToFront()
        PanelPvPHTTP.Dock = DockStyle.Fill
        PanelPvPHTTP.BringToFront()
        PanelTutorial.Dock = DockStyle.Fill
        PanelTutorial.BringToFront()
        PicFormHeader.Dock = DockStyle.Top
        ButtonsPanel.SendToBack()
    End Sub

    Sub SelectButton(ByVal deselect As Boolean)
        ' If deselect is true, then we want to deselect the currently selected button (which we need to do before
        ' selecting a new one). If it set to true, we're selecting a new button. When changing the selection, we
        ' first need to deselect the currently selected button, change the value of SelectedButtonListIndex, then
        ' select the button whose index in the list of buttons is equal to the new value.
        Select Case VisiblePanel
            Case 0
                Dim Selection As Label = ButtonLabList.Item(SelectedButtonListIndex)
                If deselect = True Then
                    With Selection
                        .Parent.BackgroundImage = My.Resources.ButtonBorderInactive
                        .ForeColor = Color.SteelBlue
                    End With
                Else
                    With Selection
                        .Parent.BackgroundImage = My.Resources.ButtonBorderActive1
                        .ForeColor = Color.LightCyan
                    End With
                End If
            Case 1
                Dim Selection As PictureBox = ButtonSettingsList.Item(SelectedSettingsListIndex)
                If deselect = True Then
                    With Selection
                        If SelectedSettingsListIndex = 0 Then
                            .BackgroundImage = My.Resources.ButtonBackInactive
                        Else
                            .BackgroundImage = My.Resources.ButtonBorderInactive
                            .ForeColor = Color.SteelBlue
                        End If
                    End With
                Else
                    With Selection
                        If SelectedSettingsListIndex = 0 Then
                            .BackgroundImage = My.Resources.ButtonBackActive
                        Else
                            .ForeColor = Color.LightCyan
                        End If
                    End With
                End If
        End Select
    End Sub

    Sub ButtonMouseEnter(sender As Object, e As EventArgs) Handles LabSettings.MouseEnter, LabPvE.MouseEnter, LabPvPLan.MouseEnter, LabPvPHTTP.MouseEnter, PicCloseSettings.MouseEnter
        ' Handles the MouseEnter event of all the buttons on the form; no need to handle them separately, as 'sender' will be the hovered object.
        ' Because we are combining the use of the keyboard with the pointing of the cursor for selection of buttons, we should make sure that the
        ' hovered button is not already the selected one before we needlessly deselect it (with the SelectButton sub). Notice the importance of the TabIndex.
        ' In order for this to work, each label's TabIndex must correspond to its place on the form (from the top). We could use the Tag property
        ' instead, but setting the correct TabIndex is preferable anyway. If the label's TabIndex is not already equal to the SelectedButtonListIndex,
        ' we deselect the old selection (by setting the 'deselect' value of the SelectButton sub to 'True'), then select the hovered label. Notice also
        ' that this sub procedure handles the MouseEnter event for all labels on the form. There is no need to handle the MouseLeave event, as a
        ' button remains selected once selected, until explicitly deselected, when using the keyboard to navigate, and this should be consistent with that.

        ' When using the picture boxes in the panels, we cannot use the TabIndex property. We'll use the Tag property instead.

        Select Case VisiblePanel
            Case 0
                If Not sender.TabIndex = SelectedButtonListIndex Then
                    Call SelectButton(True)
                    SelectedButtonListIndex = sender.TabIndex
                    Call SelectButton(False)
                End If
            Case 1
                If Not sender.Tag = SelectedSettingsListIndex Then
                    Call SelectButton(True)
                    SelectedSettingsListIndex = sender.Tag
                    Call SelectButton(False)
                End If
        End Select
    End Sub

    Private Sub StartScreen_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Here, e contains information about which key was pressed.

        ' First, we check if the escape key was pressed.
        If e.KeyCode = Keys.Escape Then
            If Not VisiblePanel = 0 Then
                PanelSettings.Hide()
                PanelPvE.Hide()
                PanelPvPLan.Hide()
                PanelPvPHTTP.Hide()
                PanelTutorial.Hide()
                VisiblePanel = 0
                e.Handled = True
            Else
                ' We should avoid the need to use End() here, in case we implement a closing method in the future.
                ' We should also ask the user to confirm.
                Close()
            End If
        End If

        ' We determine which panel is open. I found it cleaner to check here.

        Select Case VisiblePanel
            Case 0
                ' If there is no panel open...
                Select Case e.KeyCode
                    ' If the 'up' key was pressed...
                    Case Keys.Up
                        ' If we're not already at the top of the list (we cannot select the button at the list index of -1, as it
                        ' does not exist)...
                        If Not SelectedButtonListIndex = 0 Then
                            Call SelectButton(True)
                            SelectedButtonListIndex -= 1
                            Call SelectButton(False)
                        End If

                    ' If the 'down' key was pressed...
                    Case Keys.Down
                        ' As with Keys.Up, we check if the last button is already selected.
                        If Not SelectedButtonListIndex = ButtonLabList.Count - 1 Then
                            Call SelectButton(True)
                            SelectedButtonListIndex += 1
                            Call SelectButton(False)
                        End If
                    Case Keys.Tab
                        ' With the tab key, we want to select the first button if the last button is selected.
                        ' The currently selected button will be deselected no matter the condition.
                        Call SelectButton(True)
                        If Not SelectedButtonListIndex = ButtonLabList.Count - 1 Then
                            ' Does what Keys.Down would do:
                            SelectedButtonListIndex += 1
                        Else
                            ' Doesn't stop at the bottom, skips to the top:
                            SelectedButtonListIndex = 0
                        End If
                        Call SelectButton(False)
                    Case Keys.Space, Keys.Enter
                        ' If the space/enter key was pressed, we proceed.
                        Call EnterSelected()
                End Select
            Case 1
                Select Case e.KeyCode
                    Case Keys.Space, Keys.Enter
                        Call EnterSelected()
                End Select
        End Select
    End Sub

    Sub EnterSelected()
        ' What we want to do next depends on which button is selected when space/enter is pressed or a button is clicked.
        Select Case VisiblePanel
            Case 0
                Select Case SelectedButtonListIndex
                    Case 0
                        VisiblePanel = 1
                        PanelSettings.Show()
                    Case 1
                        VisiblePanel = 2
                        PanelPvE.Show()
                    Case 2
                        VisiblePanel = 3
                        PanelPvPLan.Show()
                    Case 3
                        VisiblePanel = 4
                        PanelPvPHTTP.Show()
                    Case 4
                        VisiblePanel = 5
                        PanelTutorial.Show()
                End Select
            Case 1
                Select Case SelectedSettingsListIndex
                    Case 0
                        PanelSettings.Hide()
                        VisiblePanel = 0
                End Select
        End Select
    End Sub

    Private Sub ButtonClick(sender As Object, e As EventArgs) Handles LabSettings.Click, LabPvE.Click, LabPvPLan.Click, LabPvPHTTP.Click
        ' It does not matter which button is clicked per se. SelectedButtonListIndex was changed upon MouseEnter.
        Call EnterSelected()
    End Sub

    Private Sub PicFormHeader_Click(sender As Object, e As EventArgs) Handles PicFormHeader.Click

    End Sub

    Private Sub ClosePanel(sender As Object, e As EventArgs) Handles PicClosePvE.Click, PicClosePvPLAN.Click, PicClosePvPHTTP.Click, PicCloseTutorial.Click
        VisiblePanel = 0
        sender.Parent.Hide()
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
        FocusedLabel.ForeColor = Color.FromArgb(255, 150 + (FocusedLabelAddColor / 1.5), 230 + (FocusedLabelAddColor / 4), 255)
        If FocusedLabelColorIncreasing = True Then
            If FocusedLabelAddColor >= 100 Then
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
        ' Release the form drag
        DragForm = False
    End Sub
End Class
