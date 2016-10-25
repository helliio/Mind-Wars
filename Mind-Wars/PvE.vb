Imports System.ComponentModel

Public Class PvEGame
    Dim CursorX As Integer, CursorY As Integer
    Dim DragForm As Boolean = False
    Dim ShowHolesCounter As Integer = 0
    Dim easy As New EasyComputer

    Private Sub PvE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeGameModeProgress = 0
        solution = GenerateSolution()
        Debug.Print("Solution: " & ArrayToInt(solution))
        SelectedColor = 2
        SelectedChooseCodeColor = 2
        Me.BackgroundImage = Theme_FormBackground
        BWPanel.Visible = True
        GamePanel.Visible = True
        Me.Width = 60 + 32 * holes
        Me.Height = 38 * (tries + 1) + 74
        Call GenerateBoard(1, GamePanel, BWPanel, ChooseCodePanel)
        InitializeDelay.Enabled = True
        With PicInitialLoadProgress
            .Parent = Me
            .Left = Me.ClientRectangle.Width / 2 - PicInitialLoadProgress.Width / 2
            .Top = Me.ClientRectangle.Height / 2 - PicInitialLoadProgress.Height / 2
            .BringToFront()
        End With
        InitializeGMPRect = PicInitialLoadProgress.DisplayRectangle
        InitializeGMPRect.Inflate(-2, -2)
        ChooseCodePanel.Left = 0
        ChooseCodePanel.Top = 0
        ChooseCodePanel.Size = Me.ClientRectangle.Size
    End Sub
    Private Sub InitializeBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles InitializeBackgroundWorker.DoWork
        Call PopulateLists(1, InitializeBackgroundWorker)
    End Sub
    Private Sub InitializeBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles InitializeBackgroundWorker.RunWorkerCompleted
        LoadCompleteTimer.Enabled = True
        PicInitialLoadProgress.Invalidate()
    End Sub

    Private Sub InitializeBackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles InitializeBackgroundWorker.ProgressChanged
        Me.Invoke(Sub()
                      InitializeGameModeProgress = Convert.ToSingle(e.ProgressPercentage * 3.6)
                      PicInitialLoadProgress.Invalidate()
                  End Sub)
    End Sub

    Private Sub PicInitialLoadProgress_Paint(sender As Object, e As PaintEventArgs) Handles PicInitialLoadProgress.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.DrawArc(InitializeGMPPen, InitializeGMPRect, 90, InitializeGameModeProgress)
    End Sub

    Private Sub InitializeDelay_Tick(sender As Object, e As EventArgs) Handles InitializeDelay.Tick
        Call InitializeGameMode(1)
        InitializeDelay.Enabled = False
    End Sub

    Private Sub LoadCompleteTimer_Tick(sender As Object, e As EventArgs) Handles LoadCompleteTimer.Tick
        If InitializeGMPPen.Color.A > 20 Then
            InitializeGMPPen.Color = Color.FromArgb(InitializeGMPPen.Color.A - 20, InitializeGMPPen.Color)
            PicInitialLoadProgress.Refresh()
        Else
            PicInitialLoadProgress.Hide()
            'InitializeGMPPen.Dispose()
            LoadCompleteTimer.Enabled = False
            BWPanel.Visible = True
            GamePanel.Visible = True
            ShowHolesTimer.Enabled = True
            Debug.Print("Elements in initial list: " & InitiallyPossibleSolutions.Count & ", current list: " & CurrentlyPossibleSolutions.Count)
        End If
    End Sub

    Private Sub PvEGame_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        'Debug.Print("YAY im closed")
        CurrentlyPossibleSolutions.Clear()
        InitiallyPossibleSolutions.Clear()
        StartScreen.Show()

        SelectedColor = 0
        SelectedChooseCodeColor = 0
        BWPanel.Visible = False
        GamePanel.Visible = False

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

        InitializeDelay.Enabled = False

    End Sub
    'starts the ai
    Private Sub AIBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles AIBackgroundWorker.DoWork
        If UseLightMinimax = True Then
            Debug.Print("Using MinimaxLight...")
            Dim LightMinimax As New MinimaxLight(InitiallyPossibleSolutions, CurrentlyPossibleSolutions)
            Dim LightMinimaxThread As New System.Threading.Thread(AddressOf LightMinimax.FindBestMove)
            LightMinimaxThread.Priority = Threading.ThreadPriority.Highest
            LightMinimaxThread.IsBackground = True
            LightMinimaxThread.Start()
            LightMinimaxThread.Join()
            Dim bestindexlight = FourBestIndices(0)
            Dim AIGuessLight() As Integer = InitiallyPossibleSolutions.Item(bestindexlight)
            Debug.Print("AI guesses " & ArrayToInt(AIGuessLight))
            AIAttempts += 1
            CurrentBW = verify(solution, AIGuessLight)
            Debug.Print("CurrentBW: " & ArrayToInt(CurrentBW) & ". Should be: " & ArrayToInt(GetBW(solution, AIGuessLight)) & ". Solution is " & ArrayToInt(solution))
            Debug.Print("This returns " & ArrayToInt(CurrentBW))
            Debug.Print("Before elimination: " & CurrentlyPossibleSolutions.Count)
            Eliminate(AIGuessLight, CurrentBW)
            Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
            If CurrentlyPossibleSolutions.Count = 1 Then
                Debug.Print("AI's solution: " & ArrayToInt(InitiallyPossibleSolutions.Item(0)) & ", real solution: " & ArrayToInt(solution))
            Else
                Debug.Print("There's " & CurrentlyPossibleSolutions.Count & " items left. Going again.")
            End If
        Else
            Debug.Print("Starting quadruple thread")
            Dim Minimax1of4 As New Minimax(InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 1)
            Dim Minimax2of4 As New Minimax(InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 2)
            Dim Minimax3of4 As New Minimax(InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 3)
            Dim Minimax4of4 As New Minimax(InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 4)
            Dim MinimaxThread1 As New System.Threading.Thread(AddressOf Minimax1of4.FindBestMove)
            MinimaxThread1.Priority = Threading.ThreadPriority.Highest
            MinimaxThread1.IsBackground = True
            Dim MinimaxThread2 As New System.Threading.Thread(AddressOf Minimax2of4.FindBestMove)
            MinimaxThread2.Priority = Threading.ThreadPriority.Highest
            MinimaxThread2.IsBackground = True
            Dim MinimaxThread3 As New System.Threading.Thread(AddressOf Minimax3of4.FindBestMove)
            MinimaxThread3.Priority = Threading.ThreadPriority.Highest
            MinimaxThread3.IsBackground = True
            Dim MinimaxThread4 As New System.Threading.Thread(AddressOf Minimax4of4.FindBestMove)
            MinimaxThread4.Priority = Threading.ThreadPriority.Highest
            MinimaxThread4.IsBackground = True
            MinimaxThread1.Start()
            MinimaxThread2.Start()
            MinimaxThread3.Start()
            MinimaxThread4.Start()
            MinimaxThread1.Join()
            MinimaxThread2.Join()
            MinimaxThread3.Join()
            MinimaxThread4.Join()
            Dim i As Integer = 0
            Dim bestscore As Integer = 0
            Dim bestindex As Integer = 0
            Do Until i = 4
                If FourBestScores(i) > bestscore Then
                    bestscore = FourBestScores(i)
                    bestindex = FourBestIndices(i)
                End If
                i += 1
            Loop
            Debug.Print("Quadruple thread finished.")

            Dim AIGuess() As Integer = InitiallyPossibleSolutions.Item(bestindex)
            Debug.Print("AI guesses " & ArrayToInt(AIGuess))
            AIAttempts += 1
            CurrentBW = verify(solution, AIGuess)
            Debug.Print("CurrentBW: " & ArrayToInt(CurrentBW) & ". Should be: " & ArrayToInt(GetBW(solution, AIGuess)) & ". Solution is " & ArrayToInt(solution))
            Debug.Print("This returns " & ArrayToInt(CurrentBW))
            Debug.Print("Before elimination: " & CurrentlyPossibleSolutions.Count)
            Eliminate(AIGuess, CurrentBW)
            Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
            If CurrentlyPossibleSolutions.Count = 1 Then
                Debug.Print("AI's solution: " & ArrayToInt(InitiallyPossibleSolutions.Item(0)) & ", real solution: " & ArrayToInt(solution))
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.TextLength = holes And IsNumeric(TextBox1.Text) Then
            Dim Input As Integer = Convert.ToInt32(TextBox1.Text)
            If CheckArrRange(Input, 1, colours) Then
                solution = IntToArr(Input)
                Debug.Print("Solution is " & Input)
                Button1.Enabled = False
                Call TestRepeatedly(solution)
            Else
                MsgBox("Maximum " & colours)
            End If
        Else
            MsgBox(holes & " holes")
        End If
    End Sub

    Public Sub TestRepeatedly(ByVal code() As Integer)
        solution = code
        Dim AIGuess() As Integer = GenerateSolution()
        Debug.Print("AI guesses " & ArrayToInt(AIGuess))
        AIAttempts += 1
        CurrentBW = verify(solution, AIGuess)
        Debug.Print("This returns " & ArrayToInt(CurrentBW))
        Debug.Print("Number of possible solutions before elimination: " & CurrentlyPossibleSolutions.Count)
        Eliminate(AIGuess, CurrentBW)
        Debug.Print("Number of possible solutions after elimination: " & CurrentlyPossibleSolutions.Count)
        If CurrentlyPossibleSolutions.Count > 40 Then
            UseLightMinimax = False
        Else
            UseLightMinimax = True
        End If
        Debug.Print("AI uses Minimax...")
        AIBackgroundWorker.RunWorkerAsync()
    End Sub
    Private Sub AIBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles AIBackgroundWorker.RunWorkerCompleted
        If CurrentlyPossibleSolutions.Count > 40 Then
            Debug.Print("Running again: " & CurrentlyPossibleSolutions.Count & " left.")
            AIBackgroundWorker.RunWorkerAsync()
            UseLightMinimax = False ' SET THIS TO FALSE
        ElseIf CurrentlyPossibleSolutions.Count > 1 Then
            UseLightMinimax = True
            AIBackgroundWorker.RunWorkerAsync()
        ElseIf CurrentlyPossibleSolutions.Count = 1 Then
            AIAttempts += 1
            Debug.Print("FINISHED IN " & AIAttempts & " MOVES")
            AIAttempts = 0
            'UseLightMinimax = False
            'InitializeBackgroundWorker.Dispose()
            Dim PopulateListsClass As New ListPopulate
            'InitializeBackgroundWorker = New BackgroundWorker
            'InitializeBackgroundWorker.WorkerReportsProgress = True
            PopulateListsClass.Operation = 1
            'PopulateListsClass.Sender = InitializeBackgroundWorker
            Dim PopulateListsThread As New System.Threading.Thread(AddressOf PopulateListsClass.PopulateLists)
            PopulateListsThread.IsBackground = True
            PopulateListsThread.Start()
            PopulateListsThread.Join()
            Button1.Enabled = True
        Else
            Debug.Print("Error: " & CurrentlyPossibleSolutions.Count & " remaining.")
        End If
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
    Private Sub PicFormHeader_MouseMove(sender As Object, e As MouseEventArgs) Handles PicFormHeader.MouseMove
        If DragForm = True Then
            Me.Left = Cursor.Position.X - CursorX
            Me.Top = Cursor.Position.Y - CursorY
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

    Private Sub ColorTimer_Tick(sender As Object, e As EventArgs) Handles ColorTimer.Tick

        Dim ChangeRect As Rectangle
        If ChooseCodePanel.Visible = False Then
            If ChoiceRectangleList.Item(SelectedColor).Width > 16 Then
                ChangeRect = ChoiceRectangleList.Item(SelectedColor)
                ChangeRect.Inflate(-1, -1)
                ChoiceRectangleList.Item(SelectedColor) = ChangeRect
                ChoiceList.Item(SelectedColor).Invalidate()
                If ChoiceRectangleList.Item(SelectedColor).Width < 20 Then
                    SelectedSpinning = True
                Else
                    SelectedSpinning = False
                End If
            End If

            For Each ChoicePic As PictureBox In ChoiceList
                If ChoiceRectangleList.Item(ChoicePic.Tag).Width < 24 AndAlso Not ChoicePic.Tag = SelectedColor Then
                    Dim GrowRect As Rectangle = ChoiceRectangleList.Item(ChoicePic.Tag)
                    GrowRect.Inflate(1, 1)
                    ChoiceRectangleList.Item(ChoicePic.Tag) = GrowRect
                    ChoicePic.Invalidate()
                End If
            Next
        Else
            If ChooseCodeRectangleList.Item(SelectedChooseCodeColor).Width > 16 Then
                ChangeRect = ChooseCodeRectangleList.Item(SelectedChooseCodeColor)
                ChangeRect.Inflate(-1, -1)
                ChooseCodeRectangleList.Item(SelectedChooseCodeColor) = ChangeRect
                ChooseCodeList.Item(SelectedChooseCodeColor).Invalidate()
                If ChooseCodeRectangleList.Item(SelectedChooseCodeColor).Width < 20 Then
                    SelectedSpinning = True
                Else
                    SelectedSpinning = False
                End If
            End If

            For Each ChoicePic As PictureBox In ChooseCodeList
                If ChooseCodeRectangleList.Item(ChoicePic.Tag).Width < 24 AndAlso Not ChoicePic.Tag = SelectedChooseCodeColor Then
                    Dim GrowRect As Rectangle = ChooseCodeRectangleList.Item(ChoicePic.Tag)
                    GrowRect.Inflate(1, 1)
                    ChooseCodeRectangleList.Item(ChoicePic.Tag) = GrowRect
                    ChoicePic.Invalidate()
                End If
            Next
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.TextLength = holes And IsNumeric(TextBox1.Text) Then
            Dim Input As Integer = Convert.ToInt32(TextBox1.Text)
            If CheckArrRange(Input, 1, colours) Then
                solution = IntToArr(Input)
                Debug.Print("Solution is " & Input)
                Button2.Enabled = False
                AIBackgroundWorkerEasy.RunWorkerAsync()
            Else
                MsgBox("Maximum " & colours)
            End If
        Else
            MsgBox(holes & " holes")
        End If
    End Sub

    Private Sub AIBackgroundWorkerEasy_DoWork(sender As Object, e As DoWorkEventArgs) Handles AIBackgroundWorkerEasy.DoWork
        easy.EasyGuess()
        AIAttempts += 1
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
        If GuessList.Count < holes * tries AndAlso VerifyRowTimer.Enabled = False Then
            HolesList.Item(GuessList.Count).Invalidate()
        End If
    End Sub

    Private Sub PvEGame_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Left And Not Keys.Right
                If Not SelectedColor = 0 AndAlso Not SelectedColor = 4 Then
                    SelectedColor -= 1
                    SelectedSpinning = False
                End If
            Case Keys.Right And Not Keys.Left
                If Not SelectedColor = 3 AndAlso Not SelectedColor = 7 AndAlso Not SelectedColor = colours - 1 Then
                    SelectedColor += 1
                    SelectedSpinning = False
                End If
            Case Keys.Down And Not Keys.Up
                If Not SelectedColor + 4 > colours - 1 Then
                    SelectedColor += 4
                    SelectedSpinning = False
                ElseIf Not SelectedColor >= 4 Then
                    SelectedColor = colours - 1
                    SelectedSpinning = False
                End If
            Case Keys.Up And Not Keys.Down
                If Not SelectedColor - 4 < 0 Then
                    SelectedColor -= 4
                    SelectedSpinning = False
                End If
            Case Keys.Space, Keys.Enter
                If VerifyRowTimer.Enabled = False Then
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
                    VerifyRowTimer.Enabled = False
                    For i = 0 To holes - 1
                        HolesList.Item(i + Attempt * holes).Invalidate()
                    Next
                    If GuessList.Count <= tries * holes - 1 Then
                        HoleGraphicsTimer.Enabled = True
                        'HolesList.Item(GuessList.Count).Invalidate()
                        If GuessList.Count - Attempt * holes = holes Then
                            Call verify_guess()
                        End If
                    End If
                End If
            Case Keys.Back
                If VerifyRowTimer.Enabled = True Then
                    VerifyRowTimer.Enabled = False
                    HoleGraphicsTimer.Enabled = True
                    For i = 0 To holes - 1
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
        End Select
        SelectedChooseCodeColor = SelectedColor
    End Sub

    Private Sub verify_guess()
        HoleGraphicsTimer.Enabled = False
        FillBWTimer.Enabled = True
        Dim g(holes - 1) As Integer
        For i As Integer = 0 To TestGuess.Count - 1
            g(i) = TestGuess(i)
        Next
        TestGuess.Clear()
        Dim verifiedguess() = verify(solution, g)
        BlackCount = verifiedguess(0)
        For i As Integer = 0 To holes - 1
            If verifiedguess(0) > 0 Then
                BWCountList.Add(2)
                verifiedguess(0) -= 1
            ElseIf verifiedguess(1) > 0 Then
                BWCountList.Add(1)
                verifiedguess(1) -= 1
            Else
                BWCountList.Add(0)
            End If
        Next
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
        For i = 0 To holes - 1
            HolesList.Item(Attempt * holes + i).Invalidate()
        Next
    End Sub

    Dim BWStep As Integer = 0
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

    Private Sub SwitchSides()
        Attempt = 0
        AIAttempts = 0
        BWCountList.Clear()
        GuessList.Clear()
        BWCountList.TrimToSize()
        GuessList.TrimToSize()
        If UsersTurn = True Then
            ChooseCodePanel.Visible = True
            UsersTurn = False
        Else
            UsersTurn = True
        End If
        Call ClearBoard()
    End Sub

    Private Sub AIBackgroundWorkerEasy_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles AIBackgroundWorkerEasy.RunWorkerCompleted
        If CurrentlyPossibleSolutions.Count > 1 Then
            AIBackgroundWorkerEasy.RunWorkerAsync()
        ElseIf CurrentlyPossibleSolutions.Count = 1 Then
            AIAttempts += 1
            Debug.Print("FINISHED IN " & AIAttempts & " MOVES")
            Debug.Print("AI's solution: " & ArrayToInt(InitiallyPossibleSolutions.Item(0)) & ", real solution: " & ArrayToInt(solution))
            AIAttempts = 0
            Button2.Enabled = True
            'InitializeBackgroundWorker.Dispose()
            Dim PopulateListsClass As New ListPopulate
            PopulateListsClass.Operation = 1
            Dim PopulateListsThread As New System.Threading.Thread(AddressOf PopulateListsClass.PopulateLists)
            PopulateListsThread.Start()
            PopulateListsThread.Join()
            Call SwitchSides()
        End If
    End Sub
End Class