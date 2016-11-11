Option Strict On
Option Explicit On
Option Infer Off

Imports System.ComponentModel
Imports System.Threading

Public Class PvEGame
    Dim CursorX As Integer, CursorY As Integer
    Dim DragForm As Boolean = False
    Dim ShowHolesCounter As Integer = 0

    Dim AttemptSum As Integer = 0
    Dim Runs As Integer = 0

    Private Sub PvE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChosenCodeList.Capacity = holes
        GuessList.Capacity = holes * tries
        BWCountList.Capacity = holes * tries
        TestGuess.Capacity = holes
        AIGuessList.Capacity = holes * tries

        Me.Visible = False
        InitializeGameModeProgress = 0
        solution = GenerateSolution()
        Debug.Print("Solution is " & ArrayToString(solution))
        SelectedColor = 0
        SelectedChooseCodeColor = 0

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
        InitializeGMPRect = PicInitialLoadProgress.DisplayRectangle
        InitializeGMPRect.Inflate(-2, -2)

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

        Call GenerateBoard(1, Me, BWPanel, ChooseCodePanel)

        InitializeDelay.Enabled = True
    End Sub
    Private Sub InitializeBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles InitializeBackgroundWorker.DoWork
        Threading.Thread.Sleep(100)
        Call PopulateLists(1, InitializeBackgroundWorker)
    End Sub
    Private Sub InitializeBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles InitializeBackgroundWorker.RunWorkerCompleted
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

        Me.Visible = True
        PicInitialLoadProgress.Visible = True
        InitializeDelay.Enabled = False
        Call InitializeGameMode(1)
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
        Call ShowHideChooseCodePanel(BWPanel, ChooseCodePanel)

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
        AIGuessList.Clear()
        AIBWList.Clear()
        UsersTurn = True
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


        If AIAttempts = 0 Then
            Debug.Print("AIAttempts = 0")
            AINewestGuess = AIBestFirstGuess()
            Debug.Print("AI guesses " & ArrayToString(AINewestGuess) & ". Before elimination: " & CurrentlyPossibleSolutions.Count)
            CurrentBW = verify(solution, AINewestGuess)
            Dim EliminateClass As New Eliminator
            EliminateClass.RealGuess = AINewestGuess
            EliminateClass.RealBW = CurrentBW
            Dim EliminateThread As New System.Threading.Thread(AddressOf EliminateClass.Eliminate)
            EliminateThread.Start()
            EliminateThread.Join()
            Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
        Else
            Debug.Print("AIAttempts > 0")
            Dim newthread As New Thread(AddressOf RunMinimaxWithAverage)
            newthread.Start()
            newthread.Join()
            AINewestGuess = InitiallyPossibleSolutions.Item(TESTBestIndex)
            CurrentBW = verify(solution, AINewestGuess)
            Debug.Print("AI guesses " & ArrayToString(AINewestGuess) & ". Before elimination: " & CurrentlyPossibleSolutions.Count)
            Dim EliminateClass As New Eliminator
            EliminateClass.RealGuess = AINewestGuess
            EliminateClass.RealBW = CurrentBW
            Dim EliminateThread As New System.Threading.Thread(AddressOf EliminateClass.Eliminate)
            EliminateThread.Start()
            EliminateThread.Join()
            Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
        End If
    End Sub

    Private Sub AIBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles AIBackgroundWorker.RunWorkerCompleted
        AIAttempts += 1
        Call AIPlayGuess(AINewestGuess, CurrentBW)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.TextLength = holes And IsNumeric(TextBox1.Text) Then
            Dim Input As Integer = CInt(TextBox1.Text)
            If CheckArrRange(Input, 0, colours - 1) Then
                AIAttempts = 0
                CurrentBW = {0, 0}
                Button1.Enabled = False
                solution = SolutionIntToArray(Input)
                Debug.Print("Solution is " & ArrayToString(solution))
                Debug.Print("Starting BackgroundWorker.")
                AIBackgroundWorker.RunWorkerAsync()
            Else
                MsgBox("Maximum " & colours)
            End If
        Else
            MsgBox(holes & " holes")
        End If
    End Sub
    Private Sub AverageTest()
        AIAttempts = 0
        CurrentBW = {0, 0}
        'Button1.Enabled = False
        solution = GenerateSolution()
        Debug.Print("Solution is " & ArrayToString(solution))
        Debug.Print("Starting BackgroundWorker.")
        AIBackgroundWorker.RunWorkerAsync()
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

            For i As Integer = 0 To ChoiceList.Count - 1
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
            For i As Integer = 0 To ChooseCodeList.Count - 1
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
                AIAttempts = 0
                CurrentBW = {0, 0}
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
                            Else
                                For i As Integer = 0 To holes - 1
                                    Solution(i) = ChosenCodeList.Item(i)
                                Next
                                VerifyRowTimer.Enabled = False
                                Call ShowHideChooseCodePanel(BWPanel, ChooseCodePanel)
                                ChosenCodeList.Clear()
                                LabInfo.Text = "The computer is breaking your code."
                                InfoPanel.Show()
                                AIDelayTimer.Enabled = True
                                Debug.Print("SOLUTION IS " & ArrayToInt(Solution))
                            End If
                        End If
                    End If
                Else
                    Call SwitchSides()
                    Debug.Print("Hole timer: " & HoleGraphicsTimer.Enabled.ToString & ", color timer: " & ColorTimer.Enabled.ToString)
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

    Private Sub AITimer_Tick(sender As Object, e As EventArgs) Handles AITimer.Tick
        Dim InvalidateRow As Integer = holes * tries - InvalidatedSteps * holes
        If AIStep < holes Then
            HolesList(InvalidateRow + AIStep).Invalidate()
            AIStep += 1
        ElseIf AIStep < holes * 2 Then
            BWHolesList.Item(InvalidateRow + AIStep - holes).Invalidate()
            AIStep += 1
        Else
            AIStep = 0
            Dim BWSum As Integer = 0
            For i As Integer = 0 To holes - 1
                BWSum += AIBWList(holes * (InvalidatedSteps - 1) + i)
            Next
            If BWSum = 2 * holes Then
                Debug.Print("AI won")

                Dim UserWins As Integer = 0
                Dim AIWins As Integer = 0
                For i As Integer = 0 To AttemptCountList.Count - 1
                    If AttemptCountList(i) < UserAttemptCountList(i) Then
                        AIWins += 1
                    ElseIf AttemptCountList(i) > UserAttemptCountList(i) Then
                        UserWins += 1
                    End If
                Next
                AIAttempts = 0
                Attempt = 0
                LabInfo.Text = "You: " & UserWins & ", AI: " & AIWins & vbNewLine & "Press [space] to continue playing."
                InfoPanel.Show()
                GameFinished = True
                InvalidatedSteps = 1
                Button4.Enabled = True
                AITimer.Enabled = False
            ElseIf InvalidatedSteps * holes < AIGuessList.Count Then
                InvalidatedSteps += 1
            Else
                InvalidatedSteps += 1
                AITimer.Enabled = False
                'NewAIBackgroundWorker.RunWorkerAsync()
            End If
        End If
    End Sub

    Dim BWStep As Integer = 0
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
                MsgBox("You won")
                If HoleGraphicsTimer.Enabled = True Then
                    MsgBox("Enabled")
                End If
                Call SwitchSides()
            Else
                If Attempt = tries - 1 Then
                    AIAttempts = 0
                    Attempt = 0

                    Dim UserWins As Integer = 0
                    Dim AIWins As Integer = 0
                    For i As Integer = 0 To AttemptCountList.Count - 1
                        If AttemptCountList(i) < UserAttemptCountList(i) Then
                            AIWins += 1
                        ElseIf AttemptCountList(i) > UserAttemptCountList(i) Then
                            UserWins += 1
                        End If
                    Next

                    LabInfo.Text = "You: " & UserWins & ", AI: " & AIWins & vbNewLine & "Press [space] to continue playing."
                    InfoPanel.Show()
                    GameFinished = True
                    InvalidatedSteps = 1
                    Button4.Enabled = True
                    AITimer.Enabled = False
                Else
                    Attempt += 1
                    HoleGraphicsTimer.Enabled = True
                End If
            End If
        End If
        'End If
    End Sub
    Private Sub SwitchSides()
        GameFinished = False
        Dim GuessCount As Integer = AIGuessList.Count
        BWCountList.Clear()
        GuessList.Clear()
        AIGuessList.Clear()
        AIBWList.Clear()
        If UsersTurn = True Then
            VerifyRowTimer.Enabled = False
            HoleGraphicsTimer.Enabled = True
            For Each pic As PictureBox In ChooseCodeList
                pic.Visible = True
            Next
            ChooseCodePanel.BringToFront()
            Call ShowHideChooseCodePanel(BWPanel, ChooseCodePanel)
            UsersTurn = False
            AIAttempts = 0
            CurrentBW = {0, 0}
            Call ClearBoard()
        Else
            Attempt = 0
            Call ClearBoard()
            HoleGraphicsTimer.Enabled = True
            InfoPanel.Hide()
            UsersTurn = True
            solution = GenerateSolution()
            Debug.Print("Solution is " & ArrayToString(solution))
        End If
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
            Debug.Print("AI's solution: " & ArrayToInt(CurrentlyPossibleSolutions.Item(0)) & ", real solution: " & ArrayToInt(solution))
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

        ' FOR TESTING PURPOSES
        Call AverageTest()
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Button4.Enabled = False
        'solution = GenerateSolution()
        'Dim heh As New ListPopulate
        'heh.PopulateLists()
        'NewAIBackgroundWorker.RunWorkerAsync()

        If TextBox1.TextLength = holes AndAlso IsNumeric(TextBox1.Text) Then
            Dim Input As Integer = CInt(TextBox1.Text)
            If CheckArrRange(Input, 0, colours - 1) Then
                Dim GuessCount As Integer = AIGuessList.Count
                Debug.Print("GUESSCOUNT: " & CStr(GuessCount))
                AIGuessList.Clear()
                AIBWList.Clear()
                AIAttempts = 0
                CurrentBW = {0, 0}
                Button4.Enabled = False
                solution = SolutionIntToArray(Input)
                For i As Integer = GuessCount To 0 Step -1
                    HolesList(holes * tries - (i + 1)).Invalidate()
                    BWHolesList(holes * tries - (i + 1)).Invalidate()
                Next
                Debug.Print("Solution is " & ArrayToString(solution))
                Debug.Print("Starting NewAIBackgroundWorker.")
                NewAIBackgroundWorker.RunWorkerAsync()
            Else
                MsgBox("Maximum " & colours)
            End If
        Else
            MsgBox(holes & " holes")
        End If


    End Sub

    Private Sub NewAIBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles NewAIBackgroundWorker.DoWork
        AISolvedCode = False

        If AIAttempts = 0 Then
            AIAttempts = 1
            AINewestGuess = AIBestFirstGuess()
            Debug.Print("AI guesses " & ArrayToString(AINewestGuess) & ". Before elimination: " & CurrentlyPossibleSolutions.Count)
            CurrentBW = verify(solution, AINewestGuess)
            For i As Integer = 0 To holes - 1
                AIGuessList.Add(AINewestGuess(i))
                If i < CurrentBW(0) Then
                    AIBWList.Add(2)
                ElseIf i - CurrentBW(0) < CurrentBW(1) Then
                    AIBWList.Add(1)
                Else
                    AIBWList.Add(0)
                End If
            Next
            If CurrentBW(0) = holes Then
                AISolvedCode = True
                Debug.Print("AI won on the first try")
            Else
                Dim EliminateClass As New Eliminator
                EliminateClass.RealGuess = AINewestGuess
                EliminateClass.RealBW = CurrentBW
                Dim EliminateThread As New System.Threading.Thread(AddressOf EliminateClass.Eliminate)
                EliminateThread.Start()
                EliminateThread.Join()
                Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
            End If
        Else

            Dim IndexOfLowestMaximum As Integer = 0
            Dim LowestMaximum As Integer = Integer.MaxValue ' Primary

            BWForGList.Clear()
            For i As Integer = 0 To InitiallyPossibleSolutions.Count - 1
                Dim arr((holes + 1) * holes) As Integer

                For Each s As Integer() In CurrentlyPossibleSolutions
                    Dim bwresult() As Integer = verify(s, InitiallyPossibleSolutions(i))
                    Dim ConvertToIndex As Integer = bwresult(0) * (holes + 1) + bwresult(1)
                    arr(ConvertToIndex) += 1
                    If arr(ConvertToIndex) > LowestMaximum Then
                        Exit For
                    End If
                Next
                BWForGList.Add(arr)
                If arr.Max > LowestMaximum Then
                    Continue For
                ElseIf arr.Max < LowestMaximum Then
                    LowestMaximum = arr.Max
                    IndexOfLowestMaximum = i
                Else
                    Dim Check() As Integer = InitiallyPossibleSolutions(i)
                    Dim CheckPrev() As Integer = InitiallyPossibleSolutions(IndexOfLowestMaximum)
                    Dim iPossible As Boolean = CurrentlyPossibleSolutions.Exists(Function(ByVal obj As Integer()) As Boolean
                                                                                     For x As Integer = 0 To holes - 1
                                                                                         If Check(x) <> obj(x) Then
                                                                                             Return False
                                                                                         End If
                                                                                     Next
                                                                                     Return True
                                                                                 End Function)
                    Dim prevPossible As Boolean = CurrentlyPossibleSolutions.Exists(Function(ByVal obj As Integer()) As Boolean
                                                                                        For x As Integer = 0 To holes - 1
                                                                                            If CheckPrev(x) <> obj(x) Then
                                                                                                Return False
                                                                                            End If
                                                                                        Next
                                                                                        Return True
                                                                                    End Function)
                    If iPossible = True AndAlso prevPossible = False Then
                        IndexOfLowestMaximum = i
                    ElseIf iPossible = prevPossible Then
                        ' TEST FOR STANDARD DEVIATION / VARIANCE
                    End If
                End If
            Next

            AIAttempts += 1

            Dim realtest() As Integer = verify(solution, InitiallyPossibleSolutions(IndexOfLowestMaximum))
            For i As Integer = 0 To holes - 1
                AIGuessList.Add(InitiallyPossibleSolutions(IndexOfLowestMaximum)(i))
                If i < realtest(0) Then
                    AIBWList.Add(2)
                ElseIf i - realtest(0) < realtest(1) Then
                    AIBWList.Add(1)
                Else
                    AIBWList.Add(0)
                End If
            Next

            ' I apologize for the less semantic variable and method names here.
            If realtest(0) = holes Then
                AISolvedCode = True
                Dim heh As New ListPopulate
                heh.PopulateLists()
            ElseIf CurrentlyPossibleSolutions.Count > 1 Then
                Eliminate(InitiallyPossibleSolutions(IndexOfLowestMaximum), realtest)
            End If

            If CurrentlyPossibleSolutions.Count = 1 Then
                AIAttempts += 1
                AISolvedCode = True
                For i As Integer = 0 To holes - 1
                    AIGuessList.Add(CurrentlyPossibleSolutions(0)(i))
                    AIBWList.Add(2)
                Next
                Dim heh As New ListPopulate
                heh.PopulateLists()
            ElseIf CurrentlyPossibleSolutions.Count = 0 Then
                MsgBox("error")
            End If
        End If
    End Sub

    Private Sub AIDelayTimer_Tick(sender As Object, e As EventArgs) Handles AIDelayTimer.Tick

        Debug.Print("Solution is " & ArrayToString(solution))
        Debug.Print("Starting NewAIBackgroundWorker.")
        NewAIBackgroundWorker.RunWorkerAsync()

        AIDelayTimer.Enabled = False
    End Sub

    Private Sub ChooseCodePanel_Click(sender As Object, e As EventArgs) Handles ChooseCodePanel.Click

    End Sub

    Private Sub NewAIBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles NewAIBackgroundWorker.RunWorkerCompleted
        If AITimer.Enabled = False Then
            AITimer.Enabled = True
        End If
        If AISolvedCode = False Then
            NewAIBackgroundWorker.RunWorkerAsync()
        Else
            AttemptCountList.Add(AIAttempts)
            Debug.Print("AI won; not going again. " & AIAttempts & " attempts.")
        End If
    End Sub

    Dim PanelInvalidated As Boolean = False
    Private Sub ChooseCodePanel_Paint(sender As Object, e As PaintEventArgs) Handles ChooseCodePanel.Paint
        If ChooseCodeHolesList(holes - 1).Visible = False Then
            If PanelInvalidated = False Then
                For Each pic As PictureBox In HolesList
                    pic.Show()
                Next
                Debug.Print("TEST")
                BWPanel.Show()
                PanelInvalidated = True
                ChooseCodePanel.Invalidate()
            Else
                ChooseCodePanel.Hide()
                PanelInvalidated = False
            End If
            'ElseIf BWPanel.Visible = True Then
            '    BWPanel.Hide()
            '    For Each pic As PictureBox In HolesList
            '        pic.Hide()
            '    Next
        End If
    End Sub

    Private Sub ChooseCodePanel_VisibleChanged(sender As Object, e As EventArgs) Handles ChooseCodePanel.VisibleChanged
        PicFormHeader.BringToFront()
    End Sub
End Class