Imports System.ComponentModel

Public Class PvPHTTP
    Dim CursorX As Integer, CursorY As Integer
    Dim DragForm As Boolean = False
    Dim ShowHolesCounter As Integer = 0
    Dim BWStep As Integer = 0

    Private Sub PvPHTTP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '''' Remove ''''
        holes = 4
        tries = 10
        colours = 8

        SelectedColor = 0
        SelectedChooseCodeColor = 0
        Me.BackgroundImage = Theme_FormBackground
        BWPanel.Visible = True
        GamePanel.Visible = True
        ChooseCodePanel.Visible = False
        Me.Width = 60 + 32 * holes
        Me.Height = 38 * (tries + 1) + 74
        Call GenerateBoard(2, GamePanel, BWPanel, ChooseCodePanel)
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
        If HTTPConnectClient.IsBusy = False Then
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
            solution = GenerateSolution()
            GamePanel.Visible = True
            BWPanel.Visible = True
            GameCodePanel.Visible = False
            ShowHolesTimer.Enabled = True
            If IsGameStarter = 2 Then
                ' 0 = not set, 1 = false, 2 = true
                Debug.Print("IS GAME STARTER. STARTER CHOOSES CODE.")
                UsersTurn = True
                Call SwitchSides()
            End If
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

    End Sub
    Private Sub PvPHTTP_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If (GameCodePanel.Visible = False AndAlso UsersTurn = True) Or (GameCodePanel.Visible = False AndAlso ChooseCodePanel.Visible = True) Then
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
                                Debug.Print("GuessList count: " & GuessList.Count & ", TestGuess count: " & TestGuess.Count & ", HolesList count: " & HolesList.Count)
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
                            For Each Pic As PictureBox In ChoiceList
                                Pic.Visible = False
                            Next
                            With InfoPanel
                                .Width = 32 * holes
                                .Height = PicInfoLeft.BackgroundImage.Height
                                .Left = HolesList.Item(0).Left
                                .Top = HolesList.Item(0).Top + 44
                                .Visible = True
                            End With

                            Dim UpdateSolutionString As String = ArrayToString(solution)
                            Dim UpdateGame As New UpdateGameClass
                            UpdateGame.ParametersString = "?code=" & HTTPGameCode & "&action=setsolution&solution=" & UpdateSolutionString
                            Dim UpdateGameString As New System.Threading.Thread(AddressOf UpdateGame.Update)
                            UpdateGameString.IsBackground = True
                            UpdateGameString.Start()

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
        End If
    End Sub
    Private Sub SelectedColorTimer_Tick(sender As Object, e As EventArgs) Handles SelectedColorTimer.Tick
        SelectedArcRotation += 2
        If ChooseCodePanel.Visible = False Then
            ChoiceList.Item(SelectedColor).Invalidate()
        Else
            ChooseCodeList.Item(SelectedChooseCodeColor).Invalidate()
        End If
        If SelectedArcRotation = 360 Then
            SelectedArcRotation = 0
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
    Private Sub ColorTimer_Tick(sender As Object, e As EventArgs) Handles ColorTimer.Tick
        Dim ChangeRect As Rectangle
        If ChooseCodePanel.Visible = False Then
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

            For Each ChoicePic As PictureBox In ChoiceList
                If ChoiceRectangleList.Item(CInt(ChoicePic.Tag)).Width < 24 AndAlso Not CInt(ChoicePic.Tag) = SelectedColor Then
                    Dim GrowRect As Rectangle = ChoiceRectangleList.Item(CInt(ChoicePic.Tag))
                    GrowRect.Inflate(1, 1)
                    ChoiceRectangleList.Item(CInt(ChoicePic.Tag)) = GrowRect
                    ChoicePic.Invalidate()
                End If
            Next
        Else
            If ChooseCodeRectangleList.Item(SelectedChooseCodeColor).Width > 16 Then
                ChangeRect = ChooseCodeRectangleList.Item(SelectedChooseCodeColor)
                ChangeRect.Inflate(-1, -1)
                ChooseCodeRectangleList.Item(SelectedChooseCodeColor) = ChangeRect
                ChooseCodeList.Item(SelectedChooseCodeColor).Invalidate()
            End If
            If ChooseCodeRectangleList.Item(SelectedChooseCodeColor).Width < 20 Then
                SelectedSpinning = True
            Else
                SelectedSpinning = False
            End If

            For Each ChoicePic As PictureBox In ChooseCodeList
                If ChooseCodeRectangleList.Item(CInt(ChoicePic.Tag)).Width < 24 AndAlso Not CInt(ChoicePic.Tag) = SelectedChooseCodeColor Then
                    Dim GrowRect As Rectangle = ChooseCodeRectangleList.Item(CInt(ChoicePic.Tag))
                    GrowRect.Inflate(1, 1)
                    ChooseCodeRectangleList.Item(CInt(ChoicePic.Tag)) = GrowRect
                    ChoicePic.Invalidate()
                End If
            Next
        End If
    End Sub
    Private Sub SwitchSides()
        Attempt = 0
        BWCountList.Clear()
        GuessList.Clear()
        BWCountList.TrimToSize()
        GuessList.TrimToSize()
        If UsersTurn = True Then
            VerifyRowTimer.Enabled = False
            HoleGraphicsTimer.Enabled = True
            ChooseCodePanel.Visible = True
            UsersTurn = False
        Else
            UsersTurn = True
        End If
        Call ClearBoard()
    End Sub
    Private Sub FillBWTimer_Tick(sender As Object, e As EventArgs) Handles FillBWTimer.Tick
        If UsersTurn = True Then
            BWHolesList.Item(Attempt * holes + BWStep).Invalidate()
            BWStep += 1
            If BWStep = holes Then
                FillBWTimer.Enabled = False
                BWStep = 0
                If BlackCount = holes Then
                    MsgBox("You won")
                    If HoleGraphicsTimer.Enabled = True Then
                        MsgBox("Enabled")
                    End If
                    Call SwitchSides()
                Else
                    HoleGraphicsTimer.Enabled = True
                    Attempt += 1
                End If
            End If
        Else
            BWHolesList.Item(tries * holes - AIAttempts * holes - BWStep).Invalidate()
            BWStep += 1
            If BWStep = holes Then
                FillBWTimer.Enabled = False
                BWStep = 0
                If BlackCount = holes Then
                    HoleGraphicsTimer.Enabled = False
                    TestGuess.Clear()
                    TestGuess.TrimToSize()
                    MsgBox("AI won")
                    Call SwitchSides()
                Else
                    HoleGraphicsTimer.Enabled = True
                    Attempt += 1
                End If
            End If
        End If
    End Sub

    Private Sub CheckStatusBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles CheckStatusBackgroundWorker.DoWork
        SolutionSet = False
        Dim ResultString As String = HTTPCheckStatusClient.DownloadString(ServerBaseURI & "/getsolution.php?code=" & HTTPGameCode)
        Debug.Print("Result: " & ResultString)
        Select Case ResultString
            Case "Error 1"
                Debug.Print("Error 1")
            Case "NotSet"
                Debug.Print("Code not set")
                SolutionSet = False
            Case Else
                If IsNumeric(ResultString) AndAlso ResultString.Length = holes Then
                    SolutionSet = True
                    Debug.Print("SOLUTION FOUND: " & ResultString)
                    solution = IntToArr(CInt(ResultString))
                    Debug.Print("SOLUTION VERIFICATION: " & ArrayToString(solution))
                Else
                    MsgBox(ResultString)
                End If
        End Select
    End Sub

    Private Sub PvPHTTP_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        Me.Visible = False
        ChooseCodePanel.Visible = False

        StartScreen.Show()

        CurrentlyPossibleSolutions.Clear()
        InitiallyPossibleSolutions.Clear()
        GuessList.Clear()
        ChosenCodeList.Clear()
        TestGuess.Clear()
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
        If IsGameStarter = 2 Then
            Dim DeleteThisGame As New DeleteGameClass
            DeleteThisGame.DeleteGameCode = HTTPGameCode
            Dim DeleteGameThread As New System.Threading.Thread(AddressOf DeleteThisGame.DeleteGame)
            DeleteGameThread.Start()
        End If
        IsGameStarter = 0
    End Sub

    Private Sub InfoPanel_Paint(sender As Object, e As PaintEventArgs) Handles InfoPanel.Paint
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
            .Text = "Your opponent is breaking your code."
        End With
    End Sub

    Private Sub CheckStatusBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles CheckStatusBackgroundWorker.RunWorkerCompleted
        If SolutionSet = True Then
            Debug.Print("Ready to play")
        Else
            CheckStatusBackgroundWorker.RunWorkerAsync()
        End If
    End Sub

    Private Sub PvPHTTP_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub
End Class