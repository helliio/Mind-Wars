Option Strict On
Option Explicit On
Option Infer Off

Public Class Singleplayer
    Dim CursorX As Integer, CursorY As Integer
    Dim DragForm As Boolean = False
    Dim ShowHolesCounter As Integer = 0, AttemptSum As Integer = 0, Runs As Integer = 0

    Private Sub Singleplayer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ChosenCodeList.Capacity = holes
        GuessList.Capacity = holes * tries
        BWCountList.Capacity = holes * tries
        TestGuess.Capacity = holes
        AIGuessList.Capacity = holes * tries

        Me.Visible = False
        InitializeGameModeProgress = 0
        Solution = GenerateSolution()
        Debug.Print("Solution is " & ArrayToString(Solution))
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

End Class