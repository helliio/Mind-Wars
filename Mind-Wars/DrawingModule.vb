Module DrawingModule
    Public ColorCodes() As Color = {Color.Transparent, Color.Red, Color.Green, Color.Yellow, Color.Blue, Color.Cyan, Color.Orange, Color.DeepPink, Color.Purple}
    Public ChoiceBrush As New SolidBrush(Color.Gray)
    Public DisabledColorBrush As New SolidBrush(Color.FromArgb(255, 20, 20, 20))
    Public SelectedColorPen As New Pen(Color.LimeGreen, 2)

    Public DifficultyDrawRect As New Rectangle
    Public ThemeDrawRect As New Rectangle
    Public SoundDrawRect As New Rectangle
    Public ColorPaletteRect As New Rectangle

    Public EasyBrush As New SolidBrush(Color.LimeGreen)
    Public EasyPen As New Pen(EasyBrush)
    Public HardBrush As New SolidBrush(Color.DarkOrange)
    Public HardPen As New Pen(HardBrush)
    Public ImpossibleBrush As New SolidBrush(Color.Red)
    Public ImpossiblePen As New Pen(ImpossibleBrush)

    Public ColorPaletteBrush As New SolidBrush(Color.Transparent)

    Public InitializeGameModeProgress As Integer = 0
    Public InitializeGMPRect As Rectangle
    Public InitializeGMPPen As New Pen(Color.LimeGreen, 3)

    Public HoleRectangle As New Rectangle
    Public BWRectangle As New Rectangle
    Public ChoiceRectangle As New Rectangle

    Public FinishedGeneratingBoard As Boolean = False
    Public SelectedArcRotation As Integer = 0

    Public SelectedChoicePeg As PictureBox

    Public Sub ChangeSelection(ByVal NewSelection As Integer)
        SelectedColor = NewSelection
        SelectedChoicePeg = ChoiceList.Item(SelectedColor)
    End Sub

    Public Sub GenerateBoard(ByVal GameMode As Integer, SenderPanel As Panel, ByVal SenderBWPanel As Panel)
        FinishedGeneratingBoard = False
        HolesList.Clear()
        Select Case GameMode
            Case 1 'PvE
                Select Case holes
                    Case 2
                    Case 3
                    Case 4
                        For m = 0 To tries - 1
                            For n = 0 To holes - 1
                                Dim BWPeg As New PictureBox
                                With BWPeg
                                    .Visible = False
                                    .Width = 16
                                    .Height = 16
                                    .Name = "BWHole_" & m * holes + n
                                    .Tag = m * holes + n - 1
                                    If n < 2 Then
                                        .Top = (38 * tries) - (38 * m)
                                        .Left = 10 + n * 16
                                    Else
                                        .Top = 16 + (38 * tries) - (38 * m)
                                        .Left = n * 16 - 22
                                    End If
                                End With
                                SenderBWPanel.Controls.Add(BWPeg)
                                AddHandler BWPeg.Paint, AddressOf PaintBWHole
                                BWHolesList.Add(BWPeg)
                            Next
                        Next
                    Case 5
                    Case 6
                    Case 7
                    Case 8
                End Select
                For y As Integer = 0 To tries - 1
                    For x As Integer = 0 To holes - 1
                        Dim Hole As New PictureBox
                        With Hole
                            .Width = 32
                            .Height = 32
                            .Top = (38 * tries) - (38 * y)
                            .Left = 50 + 32 * x
                            .BorderStyle = BorderStyle.None
                            .BackColor = Color.Transparent
                            .Name = "Hole_" & y * holes + x
                            .Tag = y * holes + x
                            .Visible = False
                        End With
                        AddHandler Hole.Paint, AddressOf PaintHole
                        SenderPanel.Controls.Add(Hole)
                        HolesList.Add(Hole)
                    Next
                Next
                For i = 0 To 7
                    Dim Choice As New PictureBox
                    With Choice
                        .Width = 32
                        .Height = 32
                        If i < 4 Then
                            .Top = (38 * tries + 42)
                            .Left = 50 + 32 * i
                        Else
                            .Top = (38 * tries + 74)
                            .Left = 50 + 32 * (i - 4)
                        End If
                        .BackColor = Color.Transparent
                        .BorderStyle = BorderStyle.None
                        .Name = "Choice_" & i
                        .Tag = i
                        '.Visible = False
                        AddHandler Choice.Paint, AddressOf PaintChoice
                        SenderPanel.Controls.Add(Choice)
                        ChoiceList.Add(Choice)
                        Dim ChoiceRect As New Rectangle
                        ChoiceRect.Location = Choice.ClientRectangle.Location
                        ChoiceRect.Size = Choice.ClientRectangle.Size
                        ChoiceRect.Inflate(-2, -2)
                        ChoiceRectangleList.Add(ChoiceRect)
                    End With
                    'Dim ChoiceClass As New ColorChoice
                    'With ChoiceClass
                    '    .RepresentsTag = Choice.Tag
                    '    .RectangleLocation = Choice.ClientRectangle.Location
                    '    .varRectangle.Size = Choice.ClientRectangle.Size
                    '    .varRectangle.Inflate(-2, -2)
                    '    .SizeState = 0
                    '    .Selected = False
                    'End With
                    '.Add(ChoiceClass)

                Next
                'HolesList.Reverse()
        End Select
        FinishedGeneratingBoard = True
        PvEGame.SelectedColorTimer.Enabled = True

        Call ChangeSelection(3)
        PvEGame.ColorTimer.Enabled = True

    End Sub

    Public Sub PaintHole(sender As PictureBox, e As PaintEventArgs)
        HoleRectangle = sender.ClientRectangle
        HoleRectangle.Inflate(-2, -2)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.DrawEllipse(Pens.AliceBlue, HoleRectangle)
    End Sub
    Public Sub PaintBWHole(sender As PictureBox, e As PaintEventArgs)
        BWRectangle = sender.ClientRectangle
        BWRectangle.Inflate(-2, -2)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.FillEllipse(Brushes.Red, BWRectangle)
    End Sub
    Public Sub PaintChoice(sender As PictureBox, e As PaintEventArgs)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ChoiceBrush.Color = ColorCodes(sender.Tag + 1)
        e.Graphics.FillEllipse(ChoiceBrush, ChoiceRectangleList.Item(sender.Tag))

        If SelectedColor = sender.Tag Then
            e.Graphics.DrawArc(SelectedColorPen, sender.ClientRectangle, SelectedArcRotation, 150)
            e.Graphics.DrawArc(SelectedColorPen, sender.ClientRectangle, SelectedArcRotation + 180, 150)
        End If

    End Sub

End Module
