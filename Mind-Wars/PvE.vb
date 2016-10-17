Imports System.ComponentModel

Public Class PvEGame
    Private Sub PvE_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDelay.Enabled = True
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
        InitializeGameModeProgress = Convert.ToSingle(e.ProgressPercentage * 3.6)
        PicInitialLoadProgress.Refresh()
    End Sub

    Private Sub PicInitialLoadProgress_Click(sender As Object, e As EventArgs) Handles PicInitialLoadProgress.Click

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
            Debug.Print("Elements in initial list: " & InitiallyPossibleSolutions.Count & ", current list: " & CurrentlyPossibleSolutions.Count)
        End If
    End Sub

    Private Sub PvEGame_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        StartScreen.Show()
    End Sub

    Private Sub AIBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles AIBackgroundWorker.DoWork
        If UseLightMinimax = True Then
            Debug.Print("Using MinimaxLight...")
            Dim LightMinimax As New MinimaxLight
            Dim LightMinimaxThread As New System.Threading.Thread(AddressOf LightMinimax.FindBestMove)
            LightMinimaxThread.Start()
            LightMinimaxThread.Join()
            Dim bestindexlight = FourBestIndices(0)
            Dim AIGuessLight() As Integer = IntToArr(InitiallyPossibleSolutions.Item(bestindexlight))
            Debug.Print("AI guesses " & ArrayToInt(AIGuessLight))
            AIAttempts += 1
            CurrentBW = verify(solution, AIGuessLight)
            Debug.Print("CurrentBW: " & ArrayToInt(CurrentBW) & ". Should be: " & ArrayToInt(GetBW(solution, AIGuessLight)) & ". Solution is " & ArrayToInt(solution))
            Debug.Print("This returns " & ArrayToInt(CurrentBW))
            Debug.Print("Before elimination: " & CurrentlyPossibleSolutions.Count)
            Eliminate(AIGuessLight, CurrentBW)
            Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
            If CurrentlyPossibleSolutions.Count = 1 Then
                Debug.Print("AI's solution: " & InitiallyPossibleSolutions.Item(0) & ", real solution: " & ArrayToInt(solution))
            Else
                Debug.Print("There's " & CurrentlyPossibleSolutions.Count & " items left. Going again.")
            End If
        Else
            Debug.Print("Starting quadruple thread")
            Dim Minimax1of4 As New Minimax
            Dim Minimax2of4 As New Minimax
            Dim Minimax3of4 As New Minimax
            Dim Minimax4of4 As New Minimax
            Dim MinimaxThread1 As New System.Threading.Thread(AddressOf Minimax1of4.FindBestMove)
            Dim MinimaxThread2 As New System.Threading.Thread(AddressOf Minimax2of4.FindBestMove)
            Dim MinimaxThread3 As New System.Threading.Thread(AddressOf Minimax3of4.FindBestMove)
            Dim MinimaxThread4 As New System.Threading.Thread(AddressOf Minimax4of4.FindBestMove)
            Minimax1of4.FourthNumber = 1
            Minimax2of4.FourthNumber = 2
            Minimax3of4.FourthNumber = 3
            Minimax4of4.FourthNumber = 4
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

            Dim AIGuess() As Integer = IntToArr(InitiallyPossibleSolutions.Item(bestindex))
            Debug.Print("AI guesses " & ArrayToInt(AIGuess))
            AIAttempts += 1
            CurrentBW = verify(solution, AIGuess)
            Debug.Print("CurrentBW: " & ArrayToInt(CurrentBW) & ". Should be: " & ArrayToInt(GetBW(solution, AIGuess)) & ". Solution is " & ArrayToInt(solution))
            Debug.Print("This returns " & ArrayToInt(CurrentBW))
            Debug.Print("Before elimination: " & CurrentlyPossibleSolutions.Count)
            Eliminate(AIGuess, CurrentBW)
            Debug.Print("After elimination: " & CurrentlyPossibleSolutions.Count)
            If CurrentlyPossibleSolutions.Count = 1 Then
                Debug.Print("AI's solution: " & InitiallyPossibleSolutions.Item(0) & ", real solution: " & ArrayToInt(solution))
            End If
        End If

        'Dim i As Integer = 0
        'Dim BestScore As Integer = 0
        'Dim IndexOfBestScore As Integer = 0
        'Do Until i <= 4
        'If FireBesteScore(i) > bestescore Then
        'bestescore = FireBesteScore(i)
        'besteindex = FireBeste(i)
        'End If
        'i += 1
        'Loop

        'NyesteForsøk = StringTilArray(OrigS.Item(besteindex))

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
            Call PopulateLists(1)
            Button1.Enabled = True
        Else
            Debug.Print("Error: " & CurrentlyPossibleSolutions.Count & " remaining.")
        End If
    End Sub
End Class