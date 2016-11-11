Option Strict On
Option Explicit On
Option Infer Off

Public Class Singleplayer
    Dim CursorX As Integer, CursorY As Integer
    Dim DragForm As Boolean = False
    Dim ShowHolesCounter As Integer = 0, AttemptSum As Integer = 0, Runs As Integer = 0

    Private Sub Singleplayer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Visible = False

        UsersTurn = True
        holes = 4
        colours = 8
        tries = 12
        GuessList.Capacity = holes * tries
        BWCountList.Capacity = holes * tries
        TestGuess.Capacity = holes

        Solution = GenerateSolution()
        Debug.Print("Solution is " & ArrayToString(Solution))
        SelectedColor = 0

        Me.Width = 60 + 32 * holes
        Me.Height = 38 * (tries + 1) + 74

        InfoPanel.Visible = False
        With PicInitialLoadProgress
            .Visible = False
            .BackColor = Color.Transparent
            .Parent = Me
            .Left = CInt(Me.ClientRectangle.Width / 2 - PicInitialLoadProgress.Width / 2)
            .Top = CInt(Me.ClientRectangle.Height / 2 - PicInitialLoadProgress.Height / 2)
            .BringToFront()
        End With

        With ChooseCodePanel
            .Visible = False
            .Left = 0
            .Top = 0
            .BackColor = Color.Transparent
            .Size = Me.ClientRectangle.Size
            .Parent = Me
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
        With PicMinimizeForm
            .Parent = PicFormHeader
            .Visible = True
            .BringToFront()
            .Left = 32 * holes + 20
            .Top = 10
        End With
        With PicCloseForm
            .Visible = True
            .Parent = PicFormHeader
            .BringToFront()
            .Left = 32 * holes + 36
            .Top = 10
        End With
        Call GenerateBoard(3, Me, BWPanel, ChooseCodePanel)
        Me.Visible = True
        SelectedColorTimer.Enabled = True
        ColorTimer.Enabled = True
        ShowHolesTimer.Enabled = True
    End Sub



    Private Sub PicFormHeader_MouseDown(sender As Object, e As MouseEventArgs) Handles PicFormHeader.MouseDown
        If e.Button = MouseButtons.Left Then
            DragForm = True
            CursorX = Cursor.Position.X - Me.Left
            CursorY = Cursor.Position.Y - Me.Top
        End If

    End Sub
    Private Sub PicFormHeader_MouseUp(sender As Object, e As MouseEventArgs) Handles PicFormHeader.MouseUp
        DragForm = False
    End Sub

    Private Sub ShowHolesTimer_Tick(sender As Object, e As EventArgs) Handles ShowHolesTimer.Tick
        If ShowHolesCounter < HolesList.Count Then
            HolesList.Item(ShowHolesCounter).Visible = True
            ShowHolesCounter += 1
            If BWPanel.Visible = False Then
                BWPanel.Visible = True
            End If
        ElseIf ShowHolesCounter < HolesList.Count + BWHolesList.Count Then
            ShowHolesTimer.Interval = 80
            BWHolesList.Item(ShowHolesCounter - HolesList.Count).Visible = True
            BWHolesList.Item(ShowHolesCounter + 1 - HolesList.Count).Visible = True
            BWHolesList.Item(ShowHolesCounter + 2 - HolesList.Count).Visible = True
            BWHolesList.Item(ShowHolesCounter + 3 - HolesList.Count).Visible = True
            ShowHolesCounter += 4
        Else
            With InfoPanel
                .Parent = Me
                .BackColor = Color.Transparent
                .BringToFront()
                .Width = 32 * holes
                .Height = PicInfoLeft.BackgroundImage.Height + 12
                .Left = HolesList.Item(0).Left
                .Top = HolesList.Item(0).Top + 42
            End With
            With PicInfoLeft
                .Parent = InfoPanel
                .Size = .BackgroundImage.Size
                .Left = 0
                .Top = 0
            End With
            With PicInfoMiddle
                .Parent = InfoPanel
                .Height = .BackgroundImage.Height
                .Width = .Parent.Width - PicInfoLeft.Width * 2
                .Left = PicInfoLeft.Width
                .Top = 0
            End With
            With PicInfoRight
                .Parent = InfoPanel
                .Size = .BackgroundImage.Size
                .Left = PicInfoLeft.Width + PicInfoMiddle.Width
                .Top = 0
            End With
            With LabInfo
                .Parent = PicInfoMiddle
                .Size = .Parent.Size
                .Left = 0
                .Top = 0
            End With
            ShowHolesTimer.Enabled = False
            ShowHolesCounter = 0
            HoleGraphicsTimer.Enabled = True
        End If
    End Sub

    Private Sub SelectedColorTimer_Tick(sender As Object, e As EventArgs) Handles SelectedColorTimer.Tick
        If SelectedSpinning = True Then
            SelectedArcRotation += 2
            If ChooseCodePanel.Visible = False Then
                ChoiceList.Item(SelectedColor).Invalidate()
            Else
                ChooseCodeList.Item(SelectedChooseCodeColor).Invalidate()
            End If
            If SelectedArcRotation = 360 Then
                SelectedArcRotation = 0
            End If
        End If
    End Sub

    Private Sub ColorTimer_Tick(sender As Object, e As EventArgs) Handles ColorTimer.Tick

        Dim ChangeRect As Rectangle
        If ChoiceRectangleList.Item(SelectedColor).Width > 16 Then
            ChangeRect = ChoiceRectangleList.Item(SelectedColor)
            ChangeRect.Inflate(-1, -1)
            ChoiceRectangleList.Item(SelectedColor) = ChangeRect
            ChoiceList.Item(SelectedColor).Invalidate()
        End If
        If ChoiceRectangleList.Item(SelectedColor).Width < 20 Then
            SelectedSpinning = True
        Else
            SelectedSpinning = False
        End If

        For i As Integer = 0 To ChoiceList.Count - 1
            If ChoiceRectangleList.Item(i).Width < 24 AndAlso i <> SelectedColor Then
                Dim GrowRect As Rectangle = ChoiceRectangleList.Item(i)
                GrowRect.Inflate(1, 1)
                ChoiceRectangleList.Item(i) = GrowRect
                ChoiceList.Item(i).Invalidate()
            End If
        Next
    End Sub

    Private Sub HoleGraphicsTimer_Tick(sender As Object, e As EventArgs) Handles HoleGraphicsTimer.Tick
        If VerifyRowAlphaIncreasing Then
            VerifyRowAlpha += 10
        Else
            VerifyRowAlpha -= 10
        End If
        If VerifyRowAlpha <= 155 Then
            VerifyRowAlphaIncreasing = True
        ElseIf VerifyRowAlpha >= 255 Then
            VerifyRowAlphaIncreasing = False
            VerifyRowAlpha = 255
        End If
        FocusedHolePen.Color = Color.FromArgb(VerifyRowAlpha, FocusedHolePen.Color)

        If GuessList.Count < holes * tries AndAlso VerifyRowTimer.Enabled = False Then
            HolesList.Item(GuessList.Count).Invalidate()
        End If

    End Sub

    Private Sub FillBWTimer_Tick(sender As Object, e As EventArgs) Handles FillBWTimer.Tick
        'If UsersTurn = True Then
        BWHolesList.Item(Attempt * holes + BWStep).Invalidate()
        BWStep += 1
        If BWStep = holes Then
            FillBWTimer.Enabled = False
            BWStep = 0
            'InfoPanel.Hide()
            If BlackCount = holes Then
                Attempt += 1
                UserAttemptCountList.Add(Attempt)
                Debug.Print("You won")
                AIAttempts = 0
                Attempt = 0
                LabInfo.Text = "You broke the code." & vbNewLine & "Press [space] to continue."
                InfoPanel.Show()
                GameFinished = True
                InvalidatedSteps = 1
            Else
                If Attempt = tries - 1 Then
                    AIAttempts = 0
                    Attempt = 0
                    LabInfo.Text = "The code was "
                    For i As Integer = 0 To holes - 1
                        Dim AndString As String = ""
                        Dim ColorInt As Integer = Solution(i)
                        Select Case ColorInt
                            Case 0
                                AndString = "red"
                            Case 1
                                AndString = "green"
                            Case 2
                                AndString = "yellow"
                            Case 3
                                AndString = "blue"
                            Case 4
                                AndString = "cyan"
                            Case 5
                                AndString = "orange"
                            Case 6
                                AndString = "pink"
                            Case 7
                                AndString = "purple"
                        End Select
                        LabInfo.Text &= AndString
                        If i < holes - 1 Then
                            LabInfo.Text &= ", "
                        End If
                    Next
                    LabInfo.Text = LabInfo.Text & "." & vbNewLine & "Press [space] To Continue."
                    InfoPanel.Show()
                    GameFinished = True
                    InvalidatedSteps = 1
                Else
                    Attempt += 1
                    HoleGraphicsTimer.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub VerifyRowTimer_Tick(sender As Object, e As EventArgs) Handles VerifyRowTimer.Tick
        VerifyRowPen.Color = Color.FromArgb(VerifyRowAlpha, VerifyRowPen.Color)
        If VerifyRowAlpha = 255 Then
            VerifyRowAlphaIncreasing = False
            VerifyRowAlpha -= 5
        ElseIf VerifyRowAlpha = 100 Then
            VerifyRowAlphaIncreasing = True
            VerifyRowAlpha += 5
        ElseIf VerifyRowAlphaIncreasing = True Then
            VerifyRowAlpha += 5
        Else
            VerifyRowAlpha -= 5
        End If
        If Not ChooseCodePanel.Visible = True Then
            For i As Integer = 0 To holes - 1
                HolesList.Item(Attempt * holes + i).Invalidate()
            Next
        Else
            For i As Integer = 0 To holes - 1
                ChooseCodeHolesList.Item(i).Invalidate()
            Next
        End If
    End Sub

    Private Sub PicMinimizeForm_Click(sender As Object, e As EventArgs) Handles PicMinimizeForm.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub PicFormHeader_MouseMove(sender As Object, e As MouseEventArgs) Handles PicFormHeader.MouseMove
        If DragForm = True Then
            Me.Left = Cursor.Position.X - CursorX
            Me.Top = Cursor.Position.Y - CursorY
        End If
    End Sub

    Sub PlayAgain()
        GameFinished = False
        BWCountList.Clear()
        GuessList.Clear()

        Attempt = 0
        Call ClearBoard()
        HoleGraphicsTimer.Enabled = True
        InfoPanel.Hide()
        Solution = GenerateSolution()
        Debug.Print("Solution Is " & ArrayToString(Solution))
    End Sub

    Private Sub PicCloseForm_Click(sender As Object, e As EventArgs) Handles PicCloseForm.Click
        Me.Close()
    End Sub

    Private Sub Singleplayer_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Left
                If Not SelectedColor = 0 AndAlso Not SelectedColor = 4 Then
                    SelectedColor -= 1
                    SelectedSpinning = False
                End If
            Case Keys.Right
                If Not SelectedColor = 3 AndAlso Not SelectedColor = 7 AndAlso Not SelectedColor = colours - 1 Then
                    SelectedColor += 1
                    SelectedSpinning = False
                End If
            Case Keys.Down
                If Not SelectedColor + 4 > colours - 1 Then
                    SelectedColor += 4
                    SelectedSpinning = False
                ElseIf Not SelectedColor >= 4 Then
                    SelectedColor = colours - 1
                    SelectedSpinning = False
                End If
            Case Keys.Up
                If Not SelectedColor - 4 < 0 Then
                    SelectedColor -= 4
                    SelectedSpinning = False
                End If
            Case Keys.Space, Keys.Enter
                If GameFinished = False Then
                    If FillBWTimer.Enabled = False Then
                        If VerifyRowTimer.Enabled = False Then
                            If GuessList.Count < holes * tries AndAlso HoleGraphicsTimer.Enabled = True Then
                                GuessList.Add(SelectedColor)
                                TestGuess.Add(SelectedColor)
                                HolesList.Item(GuessList.Count - 1).Invalidate()

                                If GuessList.Count = (Attempt + 1) * holes Then
                                    VerifyRowTimer.Enabled = True
                                    HoleGraphicsTimer.Enabled = False
                                    LabInfo.Text = "[enter] to guess, [backspace] to modify."
                                    InfoPanel.Show()
                                Else
                                    HolesList.Item(GuessList.Count).Invalidate()
                                End If
                            End If

                        Else
                            VerifyRowTimer.Enabled = False
                            For i As Integer = 0 To holes - 1
                                HolesList.Item(i + Attempt * holes).Invalidate()
                            Next
                            If GuessList.Count <= tries * holes - 1 Then
                                HoleGraphicsTimer.Enabled = True
                                'HolesList.Item(GuessList.Count).Invalidate()
                                If GuessList.Count - Attempt * holes = holes Then
                                    HoleGraphicsTimer.Enabled = False
                                    Call verify_guess()
                                    FillBWTimer.Enabled = True
                                End If
                            Else
                                HoleGraphicsTimer.Enabled = True
                                'HolesList.Item(GuessList.Count).Invalidate()
                                If GuessList.Count - Attempt * holes = holes Then
                                    HoleGraphicsTimer.Enabled = False
                                    Call verify_guess()
                                    FillBWTimer.Enabled = True
                                End If
                            End If
                            InfoPanel.Hide()
                        End If
                    End If
                Else
                    Call PlayAgain()
                End If
            Case Keys.Back
                If VerifyRowTimer.Enabled = True Then
                    VerifyRowTimer.Enabled = False
                    HoleGraphicsTimer.Enabled = True
                    If ChooseCodePanel.Visible = False Then
                        For i As Integer = 0 To holes - 1
                            HolesList.Item(i + Attempt * holes).Invalidate()
                        Next
                    Else
                        For i As Integer = 0 To holes - 1
                            ChooseCodeHolesList.Item(i).Invalidate()
                        Next
                    End If
                    InfoPanel.Hide()
                End If
                If ChooseCodePanel.Visible = False Then
                    If Not GuessList.Count - Attempt * holes = 0 AndAlso Not FillBWTimer.Enabled = True Then
                        GuessList.RemoveAt(GuessList.Count - 1)
                        TestGuess.RemoveAt(TestGuess.Count - 1)

                        If GuessList.Count < holes * tries - 1 Then
                            HolesList.Item(GuessList.Count + 1).Invalidate()
                        Else
                            HolesList.Item(GuessList.Count).Invalidate()
                        End If
                    End If
                Else
                    If Not ChosenCodeList.Count = 0 Then
                        ChosenCodeList.RemoveAt(ChosenCodeList.Count - 1)

                        If ChosenCodeList.Count < holes - 1 Then
                            ChooseCodeHolesList.Item(ChosenCodeList.Count + 1).Invalidate()
                        Else
                            ChooseCodeHolesList.Item(ChosenCodeList.Count).Invalidate()
                        End If
                    End If
                End If
            Case Keys.Escape
                Me.Close()
        End Select
        SelectedChooseCodeColor = SelectedColor
    End Sub

    Private Sub PicMinimizeForm_MouseEnter(sender As Object, e As EventArgs) Handles PicMinimizeForm.MouseEnter
        PicMinimizeForm.Image = My.Resources.MinimizeHover
    End Sub
    Private Sub PicMinimizeForm_MouseLeave(sender As Object, e As EventArgs) Handles PicMinimizeForm.MouseLeave
        PicMinimizeForm.BackgroundImage = My.Resources.Minimize
    End Sub
    Private Sub PicCloseForm_MouseEnter(sender As Object, e As EventArgs) Handles PicCloseForm.MouseEnter
        PicCloseForm.BackgroundImage = My.Resources.ExitHover
    End Sub
    Private Sub PicCloseForm_MouseLeave(sender As Object, e As EventArgs) Handles PicCloseForm.MouseLeave
        PicCloseForm.BackgroundImage = My.Resources.Exit1
    End Sub

    Private Sub Singleplayer_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Singleplayer_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Visible = False
        StartScreen.Show()
        GuessList.Clear()
        ChooseCodeRectangleList.Clear()
        ChoiceRectangleList.Clear()
        BWCountList.Clear()
        SelectedArcRotation = 0
        SelectedColor = 0
        SelectedChooseCodeColor = 0
        ChoiceList.Clear()
        ChooseCodeList.Clear()
        BWHolesList.Clear()
        HolesList.Clear()
        ChooseCodeHolesList.Clear()
        For Each pic As PictureBox In HolesList
            Dim myEventHandler As New PaintEventHandler(AddressOf PaintHole)
            RemoveHandler pic.Paint, myEventHandler
        Next
        For Each pic As PictureBox In BWHolesList
            Dim myEventHandler As New PaintEventHandler(AddressOf PaintBWHole)
            RemoveHandler pic.Paint, myEventHandler
        Next
        For Each pic As PictureBox In ChoiceList
            Dim myEventHandler As New PaintEventHandler(AddressOf PaintChoice)
            RemoveHandler pic.Paint, myEventHandler
        Next
        For Each pic As PictureBox In ChooseCodeList
            Dim myEventHandler As New PaintEventHandler(AddressOf PaintChooseCode)
            RemoveHandler pic.Paint, myEventHandler
        Next
        For Each pic As PictureBox In ChooseCodeHolesList
            Dim myEventHandler As New PaintEventHandler(AddressOf PaintChooseCodeHole)
            RemoveHandler pic.Paint, myEventHandler
        Next
    End Sub
End Class