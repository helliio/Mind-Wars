Module DrawingModule
    Public ColorCodes() As Color = {Color.Transparent, Color.Red, Color.Green, Color.Yellow, Color.Blue, Color.Cyan, Color.Orange, Color.DeepPink, Color.Purple}
    Public ChoiceBrush As New SolidBrush(Color.Gray)
    Public DisabledColorBrush As New SolidBrush(Color.FromArgb(255, 20, 20, 20))
    Public SelectedColorPen As New Pen(Color.LimeGreen, 2)
    Public VerifyRowPen As New Pen(Color.Cyan)
    Public VerifyRowAlpha As Integer = 100
    Public VerifyRowAlphaIncreasing As Boolean = True

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

    Public Testrect1 As New Rectangle
    Public Testrect2 As New Rectangle
    Public Testrect3 As New Rectangle
    Public Testrect4 As New Rectangle
    Public Testrect5 As New Rectangle
    Public Testrect6 As New Rectangle
    Public Testrect7 As New Rectangle
    Public Testrect8 As New Rectangle

    Public SelectedSpinning As Boolean = True

    Public GuessBrush As New SolidBrush(Color.Gray)
    Public FocusedHolePen As New Pen(Color.HotPink)

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
                    End With
                Next
        End Select
        FinishedGeneratingBoard = True
        PvEGame.SelectedColorTimer.Enabled = True

        Testrect1.Location = ChoiceList.Item(0).ClientRectangle.Location
        Testrect1.Size = ChoiceList.Item(0).ClientRectangle.Size
        Testrect1.Inflate(-5, -5)

        Testrect2.Location = ChoiceList.Item(1).ClientRectangle.Location
        Testrect2.Size = ChoiceList.Item(1).ClientRectangle.Size
        Testrect2.Inflate(-5, -5)

        Testrect3.Location = ChoiceList.Item(2).ClientRectangle.Location
        Testrect3.Size = ChoiceList.Item(2).ClientRectangle.Size
        Testrect3.Inflate(-5, -5)

        Testrect4.Location = ChoiceList.Item(3).ClientRectangle.Location
        Testrect4.Size = ChoiceList.Item(3).ClientRectangle.Size
        Testrect4.Inflate(-5, -5)

        Testrect5.Location = ChoiceList.Item(4).ClientRectangle.Location
        Testrect5.Size = ChoiceList.Item(4).ClientRectangle.Size
        Testrect5.Inflate(-5, -5)

        Testrect6.Location = ChoiceList.Item(5).ClientRectangle.Location
        Testrect6.Size = ChoiceList.Item(5).ClientRectangle.Size
        Testrect6.Inflate(-5, -5)

        Testrect7.Location = ChoiceList.Item(6).ClientRectangle.Location
        Testrect7.Size = ChoiceList.Item(6).ClientRectangle.Size
        Testrect7.Inflate(-5, -5)

        Testrect8.Location = ChoiceList.Item(7).ClientRectangle.Location
        Testrect8.Size = ChoiceList.Item(7).ClientRectangle.Size
        Testrect8.Inflate(-5, -5)

        ChoiceRectangleList.Add(testrect1)
        ChoiceRectangleList.Add(Testrect2)
        ChoiceRectangleList.Add(Testrect3)
        ChoiceRectangleList.Add(Testrect4)
        ChoiceRectangleList.Add(Testrect5)
        ChoiceRectangleList.Add(Testrect6)
        ChoiceRectangleList.Add(Testrect7)
        ChoiceRectangleList.Add(Testrect8)

        SelectedColor = 0
        PvEGame.ColorTimer.Enabled = True

    End Sub

    Public Sub PaintHole(sender As PictureBox, e As PaintEventArgs)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        HoleRectangle = sender.ClientRectangle
        HoleRectangle.Inflate(-2, -2)
        If PvEGame.VerifyRowTimer.Enabled = False Then
            If sender.Tag = GuessList.Count Then
                e.Graphics.DrawEllipse(FocusedHolePen, HoleRectangle)
            Else
                e.Graphics.DrawEllipse(Pens.AliceBlue, HoleRectangle)
                If CInt(sender.Tag) < GuessList.Count Then
                    HoleRectangle.Inflate(-6, -6)
                    GuessBrush.Color = ColorCodes(GuessList.Item(sender.Tag) + 1)
                    e.Graphics.FillEllipse(GuessBrush, HoleRectangle)
                End If
            End If
        ElseIf sender.Tag >= Attempt * holes AndAlso sender.Tag < (Attempt + 1) * holes Then
            Debug.Print(Attempt * holes)
            Debug.Print((Attempt + 1) * holes)
            e.Graphics.DrawEllipse(VerifyRowPen, HoleRectangle)
            HoleRectangle.Inflate(-6, -6)
            GuessBrush.Color = ColorCodes(GuessList.Item(sender.Tag) + 1)
            e.Graphics.FillEllipse(GuessBrush, HoleRectangle)
        End If
    End Sub
    Public Sub PaintBWHole(sender As PictureBox, e As PaintEventArgs)
        BWRectangle = sender.ClientRectangle
        BWRectangle.Inflate(-2, -2)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        e.Graphics.FillEllipse(Brushes.Red, BWRectangle)
    End Sub
    Public Sub PaintChoice(sender As PictureBox, e As PaintEventArgs)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        ChoiceRectangle = sender.ClientRectangle
        ChoiceRectangle.Inflate(-2, -2)
        If sender.Tag < colours Then
            ChoiceBrush.Color = ColorCodes(sender.Tag + 1)
            e.Graphics.FillEllipse(ChoiceBrush, ChoiceRectangleList.Item(sender.Tag))
        Else
            e.Graphics.FillEllipse(DisabledColorBrush, ChoiceRectangleList.Item(sender.Tag))
        End If

        If SelectedColor = sender.Tag AndAlso SelectedSpinning = True Then
            e.Graphics.DrawArc(SelectedColorPen, ChoiceRectangle, SelectedArcRotation, 45)
            e.Graphics.DrawArc(SelectedColorPen, ChoiceRectangle, SelectedArcRotation + 90, 45)
            e.Graphics.DrawArc(SelectedColorPen, ChoiceRectangle, SelectedArcRotation + 180, 45)
            e.Graphics.DrawArc(SelectedColorPen, ChoiceRectangle, SelectedArcRotation + 270, 45)
        End If

    End Sub

End Module
