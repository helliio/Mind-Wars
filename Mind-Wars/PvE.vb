﻿Imports System.ComponentModel

Public Class PvEGame
    Dim CursorX As Integer, CursorY As Integer
    Dim DragForm As Boolean = False
    Dim ShowHolesCounter As Integer = 0

    Private Sub PvE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SelectedColor = 2
        Me.BackgroundImage = Theme_FormBackground
        BWPanel.Visible = True
        GamePanel.Visible = True
        Me.Width = 60 + 32 * holes
        Me.Height = 38 * (tries + 1) + 74
        Call GenerateBoard(1, GamePanel, BWPanel)
        InitializeDelay.Enabled = True
        With PicInitialLoadProgress
            .Parent = Me
            .Left = Me.ClientRectangle.Width / 2 - PicInitialLoadProgress.Width / 2
            .Top = Me.ClientRectangle.Height / 2 - PicInitialLoadProgress.Height / 2
            .BringToFront()
        End With
        InitializeGMPRect = PicInitialLoadProgress.DisplayRectangle
        InitializeGMPRect.Inflate(-2, -2)
    End Sub
    Private Sub InitializeBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles InitializeBackgroundWorker.DoWork
        Call PopulateLists(1, InitializeBackgroundWorker)
    End Sub
    Private Sub InitializeBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles InitializeBackgroundWorker.RunWorkerCompleted
        LoadCompleteTimer.Enabled = True
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
            InitializeGMPPen.Dispose()
            LoadCompleteTimer.Enabled = False
            BWPanel.Visible = True
            GamePanel.Visible = True
            ShowHolesTimer.Enabled = True
            Debug.Print("Elements in initial list: " & InitiallyPossibleSolutions.Count & ", current list: " & CurrentlyPossibleSolutions.Count)
        End If
    End Sub

    Private Sub PvEGame_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        StartScreen.Show()
    End Sub

    Private Sub AIBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles AIBackgroundWorker.DoWork
        If UseLightMinimax = True Then
            Debug.Print("Using MinimaxLight...")
            Dim LightMinimax As New MinimaxLight(InitiallyPossibleSolutions, CurrentlyPossibleSolutions)
            Dim LightMinimaxThread As New System.Threading.Thread(AddressOf LightMinimax.FindBestMove)
            LightMinimaxThread.Priority = Threading.ThreadPriority.Highest
            LightMinimaxThread.Start()
            LightMinimaxThread.Join()
            Dim bestindexlight = FourBestIndices(0)
            Dim AIGuessLight() As Integer = InitiallyPossibleSolutions.Item(bestindexlight)
            Debug.Print("AI guesses " & ArrayToInt(AIGuessLight))
            AIAttempts += 1
            CurrentBW = verifyFixTest(solution, AIGuessLight)
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
            Dim MinimaxThread2 As New System.Threading.Thread(AddressOf Minimax2of4.FindBestMove)
            MinimaxThread2.Priority = Threading.ThreadPriority.Highest
            Dim MinimaxThread3 As New System.Threading.Thread(AddressOf Minimax3of4.FindBestMove)
            MinimaxThread3.Priority = Threading.ThreadPriority.Highest
            Dim MinimaxThread4 As New System.Threading.Thread(AddressOf Minimax4of4.FindBestMove)
            MinimaxThread4.Priority = Threading.ThreadPriority.Highest
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
            CurrentBW = verifyFixTest(solution, AIGuess)
            Debug.Print("CurrentBW: " & ArrayToInt(CurrentBW) & ". Should be: " & ArrayToInt(GetBW(solution, AIGuess)) & ". Solution is " & ArrayToInt(solution))
            Debug.Print("This returns " & ArrayToInt(CurrentBW))
            Debug.Print("Before elimination: " & CurrentlyPossibleSolutions.Count)
            Eliminate(AIGuess, CurrentBW)
            Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
            If CurrentlyPossibleSolutions.Count = 1 Then
                Debug.Print("AI's solution: " & InitiallyPossibleSolutions.Item(0) & ", real solution: " & ArrayToInt(solution))
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
        CurrentBW = verifyFixTest(solution, AIGuess)
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
            InitializeBackgroundWorker.Dispose()
            Dim PopulateListsClass As New ListPopulate
            'InitializeBackgroundWorker = New BackgroundWorker
            'InitializeBackgroundWorker.WorkerReportsProgress = True
            PopulateListsClass.Operation = 1
            'PopulateListsClass.Sender = InitializeBackgroundWorker
            Dim PopulateListsThread As New System.Threading.Thread(AddressOf PopulateListsClass.PopulateLists)
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
        ChoiceList.Item(SelectedColor).Invalidate()
        If SelectedArcRotation = 360 Then
            SelectedArcRotation = 0
        End If
    End Sub

    Private Sub ColorTimer_Tick(sender As Object, e As EventArgs) Handles ColorTimer.Tick
        If ChoiceRectangleList.Item(2).Width > 16 Then
            Dim testrect As Rectangle = ChoiceRectangleList.Item(SelectedColor)
            testrect.Inflate(1, 1)
            ChoiceList.Item(SelectedColor).Invalidate()
            Debug.Print(ChoiceRectangleList.Item(SelectedColor).Width)
        End If
        For Each Choice As PictureBox In ChoiceList
            If Choice.ClientRectangle.Width < 30 AndAlso Not Choice.Tag = SelectedColor Then
                Choice.ClientRectangle.Inflate(1, 1)
                Choice.Invalidate()
            End If
        Next
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
        End If
    End Sub



End Class