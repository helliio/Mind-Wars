Imports System.ComponentModel

Public Class PvPHTTP
    Dim CursorX As Integer, CursorY As Integer
    Dim DragForm As Boolean = False
    Dim ShowHolesCounter As Integer = 0

    Private Sub PvPHTTP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '''' Remove ''''
        holes = 4
        tries = 10
        colours = 8

        SelectedColor = 1
        SelectedChooseCodeColor = 1
        Me.BackgroundImage = Theme_FormBackground
        BWPanel.Visible = True
        GamePanel.Visible = True
        ChooseCodePanel.Visible = False
        Me.Width = 60 + 32 * holes
        Me.Height = 38 * (tries + 1) + 74
        Call GenerateBoard(1, GamePanel, BWPanel, ChooseCodePanel)
        With ChooseCodePanel
            .Left = 0
            .Top = 0
            .Size = Me.ClientRectangle.Size
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
    End Sub

    Private Sub ConnectionBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles ConnectionBackgroundWorker.DoWork
        If HTTPClient.IsBusy = False Then
            Dim result As Integer = CheckOpponentConnection(HTTPGameCode)
            Debug.Print(result)
            If result > 0 Then
                ConnectionEstablished = True
            Else
                ConnectionEstablished = False
                Threading.Thread.Sleep(2000)
            End If
        Else
            ConnectionEstablished = False
        End If
    End Sub

    Private Sub ConnectionBackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles ConnectionBackgroundWorker.ProgressChanged

    End Sub

    Private Sub ConnectionBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles ConnectionBackgroundWorker.RunWorkerCompleted
        If ConnectionEstablished = True Then
            GamePanel.Visible = True
            BWPanel.Visible = True
            ShowHolesTimer.Enabled = True
            GameCodePanel.Hide()
        Else
            ConnectionBackgroundWorker.RunWorkerAsync()
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

    Private Sub ShowHolesTimer_Tick(sender As Object, e As EventArgs) Handles ShowHolesTimer.Tick
        If ShowHolesCounter < HolesList.Count Then
            HolesList.Item(ShowHolesCounter).Visible = True
            ShowHolesCounter += 1
        ElseIf ShowHolesCounter < HolesList.Count + BWHolesList.Count Then
            ShowHolesTimer.Interval = 80
            BWHolesList.Item(ShowHolesCounter - HolesList.Count).Visible = True
            BWHolesList.Item(ShowHolesCounter + 1 - HolesList.Count).Visible = True
            BWHolesList.Item(ShowHolesCounter + 2 - HolesList.Count).Visible = True
            BWHolesList.Item(ShowHolesCounter + 3 - HolesList.Count).Visible = True
            ShowHolesCounter += 4
        Else
            ShowHolesTimer.Enabled = False
            ShowHolesCounter = 0
            HoleGraphicsTimer.Enabled = True
        End If
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

        If ChooseCodePanel.Visible = False Then
            If GuessList.Count < holes * tries AndAlso VerifyRowTimer.Enabled = False Then
                HolesList.Item(GuessList.Count).Invalidate()
            End If
        Else
            If ChosenCodeList.Count < holes AndAlso VerifyRowTimer.Enabled = False Then
                ChooseCodeHolesList.Item(ChosenCodeList.Count).Invalidate()
            End If
        End If
    End Sub

    Private Sub PicFormHeader_MouseUp(sender As Object, e As MouseEventArgs) Handles PicFormHeader.MouseUp
        DragForm = False
    End Sub

    Private Sub DebugTimer_Tick(sender As Object, e As EventArgs) Handles DebugTimer.Tick
        If ChooseCodePanel.Visible = True Then
            Debug.Print("VISIBLE")
        Else
            Debug.Print("INVISIBLE")
        End If
    End Sub

    Private Sub PvPHTTP_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
                If VerifyRowTimer.Enabled = False Then
                    If ChooseCodePanel.Visible = False Then
                        If GuessList.Count < holes * tries AndAlso HoleGraphicsTimer.Enabled = True Then
                            GuessList.Add(SelectedColor)
                            TestGuess.Add(SelectedColor)
                            HolesList.Item(GuessList.Count - 1).Invalidate()
                        End If

                        If GuessList.Count = (Attempt + 1) * holes AndAlso UsersTurn = True Then
                            VerifyRowTimer.Enabled = True
                            HoleGraphicsTimer.Enabled = False
                        ElseIf HoleGraphicsTimer.Enabled = True Then
                            HolesList.Item(GuessList.Count).Invalidate()
                        End If
                    Else
                        If ChosenCodeList.Count < holes Then 'AndAlso HoleGraphicsTimer.Enabled = True
                            ChosenCodeList.Add(SelectedChooseCodeColor)
                            ChooseCodeHolesList.Item(ChosenCodeList.Count - 1).Invalidate()
                        End If

                        If ChosenCodeList.Count = holes Then
                            Debug.Print("!!! Holes-1 = " & holes - 1 & ", chosencodelist.count = " & ChosenCodeList.Count)
                            VerifyRowTimer.Enabled = True
                            HoleGraphicsTimer.Enabled = False
                        ElseIf HoleGraphicsTimer.Enabled = True Then
                            ChooseCodeHolesList.Item(ChosenCodeList.Count).Invalidate()
                            Debug.Print(ChosenCodeList.Count & "/" & holes)
                        End If
                    End If
                Else
                    If ChooseCodePanel.Visible = False Then
                        VerifyRowTimer.Enabled = False
                        For i As Integer = 0 To holes - 1
                            HolesList.Item(i + Attempt * holes).Invalidate()
                        Next
                        If GuessList.Count <= tries * holes - 1 Then
                            HoleGraphicsTimer.Enabled = True
                            'HolesList.Item(GuessList.Count).Invalidate()
                            If GuessList.Count - Attempt * holes = holes Then
                                HoleGraphicsTimer.Enabled = False
                                FillBWTimer.Enabled = True
                                Call verify_guess()
                            End If
                        End If
                    Else
                        VerifyRowTimer.Enabled = False
                        For i As Integer = 0 To holes - 1
                            solution(i) = CInt(ChosenCodeList.Item(i))
                        Next
                        ChosenCodeList.Clear()
                        ChooseCodePanel.Hide()
                        Debug.Print("SOLUTION IS " & ArrayToInt(solution))
                    End If
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
                End If
                If ChooseCodePanel.Visible = False Then
                    If Not GuessList.Count - Attempt * holes = 0 Then
                        GuessList.RemoveAt(GuessList.Count - 1)
                        TestGuess.RemoveAt(TestGuess.Count - 1)
                        GuessList.TrimToSize()
                        TestGuess.TrimToSize()

                        If GuessList.Count < holes * tries - 1 Then
                            HolesList.Item(GuessList.Count + 1).Invalidate()
                        Else
                            HolesList.Item(GuessList.Count).Invalidate()
                        End If
                    End If
                Else
                    If Not ChosenCodeList.Count = 0 Then
                        ChosenCodeList.RemoveAt(ChosenCodeList.Count - 1)
                        GuessList.TrimToSize()
                        If ChosenCodeList.Count < holes - 1 Then
                            ChooseCodeHolesList.Item(ChosenCodeList.Count + 1).Invalidate()
                        Else
                            ChooseCodeHolesList.Item(ChosenCodeList.Count).Invalidate()

                        End If
                    End If
                End If
            Case Keys.Back
                If VerifyRowTimer.Enabled = True Then
                    VerifyRowTimer.Enabled = False
                    HoleGraphicsTimer.Enabled = True
                    For i As Integer = 0 To holes - 1
                        HolesList.Item(i + Attempt * holes).Invalidate()
                    Next
                End If
                If Not GuessList.Count - Attempt * holes = 0 Then
                    GuessList.RemoveAt(GuessList.Count - 1)
                    TestGuess.RemoveAt(TestGuess.Count - 1)
                    GuessList.TrimToSize()
                    TestGuess.TrimToSize()
                    If GuessList.Count < holes * tries - 1 Then
                        HolesList.Item(GuessList.Count + 1).Invalidate()
                    Else
                        HolesList.Item(GuessList.Count).Invalidate()
                    End If
                End If
            Case Keys.Escape
                Me.Close()
        End Select
        SelectedChooseCodeColor = SelectedColor
    End Sub
End Class