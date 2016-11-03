Option Strict On

Imports System.ComponentModel
Imports System.Threading

Public Class PvEGame
    Dim CursorX As Integer, CursorY As Integer
    Dim DragForm As Boolean = False
    Dim ShowHolesCounter As Integer = 0

    Private Sub PvE_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ChosenCodeList.Capacity = holes
        GuessList.Capacity = holes * tries
        BWCountList.Capacity = holes * tries
        TestGuess.Capacity = holes
        AIGuessList.Capacity = holes * tries

        Me.Visible = False
        InitializeGameModeProgress = 0
        solution = GenerateSolution()
        SelectedColor = 0
        SelectedChooseCodeColor = 0
        'Change when theme changes instead:
        'Me.BackgroundImage = Theme_FormBackground
        'GamePanel.Visible = True
        Me.Width = 60 + 32 * holes
        Me.Height = 38 * (tries + 1) + 74
        Call GenerateBoard(1, Me, BWPanel, ChooseCodePanel)
        InfoPanel.Visible = False
        InitializeDelay.Enabled = True
        With PicInitialLoadProgress
            .Visible = False
            .BackColor = Color.Transparent
            .Parent = Me
            .Left = CInt(Me.ClientRectangle.Width / 2 - PicInitialLoadProgress.Width / 2)
            .Top = CInt(Me.ClientRectangle.Height / 2 - PicInitialLoadProgress.Height / 2)
            .BringToFront()
        End With
        InitializeGMPRect = PicInitialLoadProgress.DisplayRectangle
        InitializeGMPRect.Inflate(-2, -2)

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
    Private Sub InitializeBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles InitializeBackgroundWorker.DoWork
        Threading.Thread.Sleep(100)
        Call PopulateLists(1, InitializeBackgroundWorker)
    End Sub
    Private Sub InitializeBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles InitializeBackgroundWorker.RunWorkerCompleted
        'PicInitialLoadProgress.Visible = False
        LoadCompleteTimer.Enabled = True
    End Sub
    Private Sub InitializeBackgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles InitializeBackgroundWorker.ProgressChanged
        Me.Invoke(Sub()
                      InitializeGameModeProgress = CInt(e.ProgressPercentage * 3.6)
                      PicInitialLoadProgress.Invalidate()
                  End Sub)
    End Sub
    Private Sub PicInitialLoadProgress_Paint(sender As Object, e As PaintEventArgs) Handles PicInitialLoadProgress.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.DrawArc(InitializeGMPPen, InitializeGMPRect, 90, InitializeGameModeProgress)
    End Sub
    Private Sub InitializeDelay_Tick(sender As Object, e As EventArgs) Handles InitializeDelay.Tick
        Call InitializeGameMode(1)
        Me.Visible = True
        InitializeDelay.Enabled = False
    End Sub
    Private Sub LoadCompleteTimer_Tick(sender As Object, e As EventArgs) Handles LoadCompleteTimer.Tick
        If InitializeGMPPen.Color.A > 20 Then
            InitializeGMPPen.Color = Color.FromArgb(InitializeGMPPen.Color.A - 20, InitializeGMPPen.Color)
            PicInitialLoadProgress.Invalidate()
        ElseIf InitializeGMPPen.Color.A > 0 Then
            InitializeGMPPen.Color = Color.FromArgb(0, InitializeGMPPen.Color)
            PicInitialLoadProgress.Invalidate()
        Else
            PicInitialLoadProgress.Hide()
            LoadCompleteTimer.Enabled = False
            ShowHolesTimer.Enabled = True
        End If
    End Sub
    Private Sub PvEGame_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        StartScreen.Show()
        Me.Visible = False

        InitializeDelay.Enabled = False
        ChooseCodePanel.Visible = False

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
    End Sub
    Private Sub AIBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles AIBackgroundWorker.DoWork
        If UseLightMinimax = True Then
            Debug.Print("Using MinimaxLight...")

            Dim LightMinimax As New MinimaxLight(InitiallyPossibleSolutions.ToArray, CurrentlyPossibleSolutions.ToArray)
            Dim LightMinimaxThread As New System.Threading.Thread(AddressOf LightMinimax.FindBestMove)
            With LightMinimaxThread
                .Priority = Threading.ThreadPriority.AboveNormal
                .Start()
                .Join()
            End With

            Dim bestindexlight As Integer = FourBestIndices(0)
            ' IF ERROR, REMOVE CTYPE AND TURN OPTION STRICT OFF '
            Dim AIGuessLight() As Integer = InitiallyPossibleSolutions.Item(bestindexlight)


            ' WE NEED TO REMOVE THE GUESS FROM THE LIST SO THAT THE AI DOESN'T GET STUCK ON PLAYING IT.
            Debug.Print("AI guesses " & ArrayToString(AIGuessLight))
            AIAttempts += 1
            CurrentBW = verify(solution, AIGuessLight)
            Debug.Print("CurrentBW: " & ArrayToString(CurrentBW) & ". Should be: " & ArrayToString(verify(solution, AIGuessLight)) & ". Solution is " & ArrayToInt(solution))

            Debug.Print("This returns " & ArrayToInt(CurrentBW))
            Debug.Print("Before elimination: " & CurrentlyPossibleSolutions.Count)

            'Call Eliminate(AIGuessLight, CurrentBW)
            Dim EliminateClass As New Eliminator
            EliminateClass.RealGuess = AIGuessLight
            EliminateClass.RealBW = CurrentBW
            Dim EliminateThread As New System.Threading.Thread(AddressOf EliminateClass.Eliminate)
            'EliminateThread.IsBackground = True
            EliminateThread.Start()
            EliminateThread.Join()
            Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
            If CurrentlyPossibleSolutions.Count = 1 Then
                Debug.Print("AI's solution: " & ArrayToString(InitiallyPossibleSolutions.Item(0)) & ", real solution: " & ArrayToString(solution))
            Else
                Debug.Print("There's " & CurrentlyPossibleSolutions.Count & " items left. Going again.")
            End If
        Else
            Debug.Print("Starting quadruple thread")

            ' <UNCOMMENT> '

            'Dim Minimax1of4 As New Minimax(InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 1)
            'Dim MinimaxThread1 As New System.Threading.Thread(AddressOf Minimax1of4.FindBestMove)
            'MinimaxThread1.Priority = Threading.ThreadPriority.AboveNormal
            ''MinimaxThread1.IsBackground = True
            'MinimaxThread1.Start()

            'Dim Minimax2of4 As New Minimax(InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 2)
            'Dim MinimaxThread2 As New System.Threading.Thread(AddressOf Minimax2of4.FindBestMove)
            'MinimaxThread2.Priority = Threading.ThreadPriority.AboveNormal
            ''MinimaxThread2.IsBackground = True
            'MinimaxThread2.Start()

            'Dim Minimax3of4 As New Minimax(InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 3)
            'Dim MinimaxThread3 As New System.Threading.Thread(AddressOf Minimax3of4.FindBestMove)
            'MinimaxThread3.Priority = Threading.ThreadPriority.AboveNormal
            ''MinimaxThread3.IsBackground = True
            'MinimaxThread3.Start()

            'Dim Minimax4of4 As New Minimax(InitiallyPossibleSolutions, CurrentlyPossibleSolutions, 4)
            'Dim MinimaxThread4 As New System.Threading.Thread(AddressOf Minimax4of4.FindBestMove)
            'MinimaxThread4.Priority = Threading.ThreadPriority.AboveNormal
            ''MinimaxThread4.IsBackground = True
            'MinimaxThread4.Start()

            'MinimaxThread1.Join()
            'MinimaxThread2.Join()
            'MinimaxThread3.Join()
            'MinimaxThread4.Join()
            'Debug.Print("Quadruple thread finished.")

            'Dim bestscore As Integer = 0
            'Dim bestindex As Integer = 0

            '</UNCOMMENT>'

            Dim newthread As New Thread(AddressOf RunMinimaxTask)
            newthread.Start()
            newthread.Join()

            '<UNCOMMENT>'
            'For i = 0 To 3
            '    If FourBestScores(i) > bestscore Then
            '        bestscore = FourBestScores(i)
            '        bestindex = FourBestIndices(i)
            '    End If
            'Next
            '</UNCOMMENT>'

            Dim AIGuess() As Integer = InitiallyPossibleSolutions.Item(TESTBestIndex)
            Debug.Print("AI guesses " & ArrayToString(AIGuess) & ". Solution is " & ArrayToString(solution))

            AIAttempts += 1
            CurrentBW = verify(solution, AIGuess)

            If CurrentBW(0) = holes Then
                Debug.Print("AI won;" & AIAttempts.ToString & "moves")
            Else
                Debug.Print("CurrentBW: " & ArrayToString(CurrentBW) & ". Should be: " & ArrayToString(verify(solution, AIGuess)) & ". Solution is " & ArrayToString(solution))
                Debug.Print("This returns " & ArrayToString(CurrentBW))
                Debug.Print("Before elimination: " & CurrentlyPossibleSolutions.Count)
                If Not CurrentlyPossibleSolutions.Contains(solution) Then
                    MsgBox("False: " & CheckArrRange(ArrayToInt(solution), 0, colours - 1).ToString)
                End If

                Dim EliminateClass As New Eliminator
                EliminateClass.RealGuess = AIGuess
                EliminateClass.RealBW = CurrentBW
                Dim EliminateThread As New System.Threading.Thread(AddressOf EliminateClass.Eliminate)
                'EliminateThread.IsBackground = True
                EliminateThread.Start()
                EliminateThread.Join()
                Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
                If CurrentlyPossibleSolutions.Count = 1 Then
                    Debug.Print("AI's solution: " & ArrayToInt(InitiallyPossibleSolutions.Item(0)) & ", real solution: " & ArrayToInt(solution))
                End If
            End If

        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.TextLength = holes And IsNumeric(TextBox1.Text) Then
            Dim Input As Integer = CInt(TextBox1.Text)
            If CheckArrRange(Input, 0, colours - 1) Then
                solution = SolutionIntToArray(Input)
                Debug.Print("Solution is " & ArrayToString(solution))
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
        ' IF THE NEW AI DOESN'T WORK, UNCOMMENT. RIGHT NOW, IT'S WORKING SWIMMINGLY. '
        'Dim AIGuess() As Integer = GenerateSolution()
        'Debug.Print("AI guesses " & ArrayToInt(AIGuess))
        'AIAttempts += 1
        'CurrentBW = verify(solution, AIGuess)
        'Debug.Print("This returns " & ArrayToInt(CurrentBW))
        'Debug.Print("Number of possible solutions before elimination: " & CurrentlyPossibleSolutions.Count)
        'Call Eliminate(AIGuess, CurrentBW)
        'Debug.Print("Number of possible solutions after elimination: " & CurrentlyPossibleSolutions.Count)
        If CurrentlyPossibleSolutions.Count > 40 Then
            UseLightMinimax = False
        Else
            UseLightMinimax = True
        End If
        Debug.Print("AI uses Minimax...")
        AIBackgroundWorker.RunWorkerAsync()
    End Sub
    Private Sub AIBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles AIBackgroundWorker.RunWorkerCompleted
        'Only starts the timer'
        If CurrentlyPossibleSolutions.Count > 40 Then
            Debug.Print("Running again: " & CurrentlyPossibleSolutions.Count & " left.")
            UseLightMinimax = False
            AIBackgroundWorker.RunWorkerAsync()
            ' Instead of RunWorkerAsync, fill holes and BW holes with colors in a timer.
            ' When timer is done, THEN RunWorkerAsync.
            ' Try above and below '
        ElseIf CurrentlyPossibleSolutions.Count > 1 Then
            UseLightMinimax = True
            AIBackgroundWorker.RunWorkerAsync()
        ElseIf CurrentlyPossibleSolutions.Count = 1 Then
            AIAttempts += 1
            Debug.Print("FINISHED IN " & AIAttempts & " MOVES")
            AIAttempts = 0
            StealthyPopulateBackgroundWorker.RunWorkerAsync()
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
        ' Try moving into SelectedColorTimer

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

            'For Each ChoicePic As PictureBox In ChoiceList
            '    If ChoiceRectangleList.Item(CInt(ChoicePic.Tag)).Width < 24 AndAlso Not CInt(ChoicePic.Tag) = SelectedColor Then
            '        Dim GrowRect As Rectangle = ChoiceRectangleList.Item(CInt(ChoicePic.Tag))
            '        GrowRect.Inflate(1, 1)
            '        ChoiceRectangleList.Item(CInt(ChoicePic.Tag)) = GrowRect
            '        ChoicePic.Invalidate()
            '    End If
            'Next

            For i = 0 To ChoiceList.Count - 1
                If ChoiceRectangleList.Item(i).Width < 24 AndAlso i <> SelectedColor Then
                    Dim GrowRect As Rectangle = ChoiceRectangleList.Item(i)
                    GrowRect.Inflate(1, 1)
                    ChoiceRectangleList.Item(i) = GrowRect
                    ChoiceList.Item(i).Invalidate()
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

            'For Each ChoicePic As PictureBox In ChooseCodeList
            For i As Integer = 0 To ChooseCodeList.Count
                If ChooseCodeRectangleList.Item(i).Width < 24 AndAlso i <> SelectedChooseCodeColor Then
                    Dim GrowRect As Rectangle = ChooseCodeRectangleList.Item(i)
                    GrowRect.Inflate(1, 1)
                    ChooseCodeRectangleList.Item(i) = GrowRect
                    ChooseCodeList.Item(i).Invalidate()
                End If
            Next
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.TextLength = holes And IsNumeric(TextBox1.Text) Then
            Dim Input As Integer = Convert.ToInt32(TextBox1.Text)
            If CheckArrRange(Input, 0, colours - 1) Then
                solution = SolutionIntToArray(Input)
                Debug.Print("Solution is " & ArrayToString(solution))
                Button2.Enabled = False
                AIBackgroundWorkerEasy.RunWorkerAsync()
            Else
                MsgBox("Maximum " & colours - 1)
            End If
        Else
            MsgBox(holes & " holes")
        End If
    End Sub
    Private Sub AIBackgroundWorkerEasy_DoWork(sender As Object, e As DoWorkEventArgs) Handles AIBackgroundWorkerEasy.DoWork
        ' MOVED TO HERE FROM DECLARATIONS. MIGHT NEED CHANGES IN ORDER TO PASS RESULTS.
        Dim easy As New EasyComputer
        easy.EasyGuess()
        AIAttempts += 1
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
    Private Sub PvEGame_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        'If SelectedSpinning = True Then
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
                If FillBWTimer.Enabled = False Then
                    If VerifyRowTimer.Enabled = False Then
                        If ChooseCodePanel.Visible = False Then
                            If GuessList.Count < holes * tries AndAlso HoleGraphicsTimer.Enabled = True Then
                                GuessList.Add(SelectedColor)
                                TestGuess.Add(SelectedColor)
                                HolesList.Item(GuessList.Count - 1).Invalidate()

                                If GuessList.Count = (Attempt + 1) * holes AndAlso UsersTurn = True Then
                                    VerifyRowTimer.Enabled = True
                                    HoleGraphicsTimer.Enabled = False
                                    LabInfo.Text = "[enter] to guess, [backspace] to modify."

                                    InfoPanel.Show()
                                Else
                                    HolesList.Item(GuessList.Count).Invalidate()
                                End If
                            End If
                        Else
                            If ChosenCodeList.Count < holes Then 'AndAlso HoleGraphicsTimer.Enabled = True
                                ChosenCodeList.Add(SelectedChooseCodeColor)
                                ChooseCodeHolesList.Item(ChosenCodeList.Count - 1).Invalidate()
                            End If

                            If ChosenCodeList.Count = holes Then
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
                            InfoPanel.Hide()
                        Else
                            VerifyRowTimer.Enabled = False
                            For i As Integer = 0 To holes - 1
                                solution(i) = ChosenCodeList.Item(i)
                            Next
                            ChosenCodeList.Clear()
                            ChooseCodePanel.Hide()
                            LabInfo.Text = "The computer is breaking your code."

                            InfoPanel.Show()

                            Debug.Print("SOLUTION IS " & ArrayToInt(solution))
                        End If
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
        'End If
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

    Dim AIStep As Integer = 0
    Private Sub AITimer_Tick(sender As Object, e As EventArgs) Handles AITimer.Tick
        If AIStep < holes Then
            'fill color
        ElseIf AIStep < holes * 2 Then
            'fill bw
            If UsersTurn = True Then
                BWHolesList.Item(Attempt * holes + AIStep).Invalidate()
                AIStep += 1
                If AIStep = holes Then
                    AITimer.Enabled = False
                    AIStep = 0
                    If BlackCount = holes Then
                        MsgBox("Ai won")
                        If HoleGraphicsTimer.Enabled = True Then
                            MsgBox("Enabled")
                        End If
                        Call SwitchSides()
                    Else
                        HoleGraphicsTimer.Enabled = True
                    End If
                End If
            Else
                BWHolesList.Item(tries * holes - AIAttempts * holes - AIStep).Invalidate()
                AIStep += 1
                If AIStep = holes Then
                    AITimer.Enabled = False
                    AIStep = 0
                    If BlackCount = holes Then
                        HoleGraphicsTimer.Enabled = False
                        TestGuess.Clear()
                        MsgBox("Player Won")
                        Call SwitchSides()
                    Else
                        HoleGraphicsTimer.Enabled = True
                    End If
                End If
            End If
        Else
            'run ai
        End If
    End Sub

    Dim BWStep As Integer = 0
    Private Sub FillBWTimer_Tick(sender As Object, e As EventArgs) Handles FillBWTimer.Tick
        If UsersTurn = True Then
            BWHolesList.Item(Attempt * holes + BWStep).Invalidate()
            BWStep += 1
            If BWStep = holes Then
                FillBWTimer.Enabled = False
                BWStep = 0
                InfoPanel.Hide()
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
        If UsersTurn = True Then
            VerifyRowTimer.Enabled = False
            HoleGraphicsTimer.Enabled = True
            ChooseCodePanel.Visible = True
            UsersTurn = False
        Else
            InfoPanel.Visible = False
            UsersTurn = True
        End If
        Call ClearBoard()
    End Sub
    Private Sub DebugTimer_Tick(sender As Object, e As EventArgs) Handles DebugTimer.Tick

    End Sub

    Private Sub PicMinimizeForm_Click(sender As Object, e As EventArgs) Handles PicMinimizeForm.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub



    Private Sub AIBackgroundWorkerEasy_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles AIBackgroundWorkerEasy.RunWorkerCompleted
        Debug.Print("EASY AI STARTED")
        If CurrentlyPossibleSolutions.Count > 1 Then
            AIBackgroundWorkerEasy.RunWorkerAsync()
        ElseIf CurrentlyPossibleSolutions.Count = 1 Then
            AIAttempts += 1
            Debug.Print("FINISHED IN " & AIAttempts & " MOVES")
            Debug.Print("AI's solution: " & ArrayToInt(InitiallyPossibleSolutions.Item(0)) & ", real solution: " & ArrayToInt(solution))
            AIAttempts = 0
            Button2.Enabled = True
            ' USE BACKGROUNDWORKER INSTEAD
            Dim PopulateListsClass As New ListPopulate
            Dim PopulateListsThread As New System.Threading.Thread(AddressOf PopulateListsClass.PopulateLists)
            PopulateListsThread.IsBackground = True
            PopulateListsThread.Start()
            PopulateListsThread.Join()
            Call SwitchSides()
        End If
    End Sub

    Private Sub PicMinimizeForm_MouseEnter(sender As Object, e As EventArgs) Handles PicMinimizeForm.MouseEnter
        PicMinimizeForm.BackgroundImage = My.Resources.MinimizeHover
    End Sub
    Private Sub PicCloseForm_Click(sender As Object, e As EventArgs) Handles PicCloseForm.Click
        Me.Close()
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

    Private Sub StealthyPopulateBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles StealthyPopulateBackgroundWorker.DoWork
        Dim PopulateListsClass As New ListPopulate
        Dim PopulateListsThread As New System.Threading.Thread(AddressOf PopulateListsClass.PopulateLists)
        PopulateListsThread.IsBackground = True
        PopulateListsThread.Start()
        PopulateListsThread.Join()

    End Sub

    Private Sub PvEGame_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim newthread As New Thread(AddressOf RunMinimaxTask)
        newthread.Start()
    End Sub

    Private Sub StealthyPopulateBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles StealthyPopulateBackgroundWorker.RunWorkerCompleted
        Button1.Enabled = True
    End Sub
End Class