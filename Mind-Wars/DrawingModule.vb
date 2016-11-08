Option Strict On
Module DrawingModule
    Public BWStep As Integer = 0
    Public ColorCodes() As Color = {Color.Transparent, Color.Red, Color.Green, Color.Yellow, Color.Blue, Color.Cyan, Color.Orange, Color.DeepPink, Color.Purple}
    Public ChoiceBrush As New SolidBrush(Color.Gray)
    Public DisabledColorBrush As New SolidBrush(Color.FromArgb(255, 20, 20, 20))
    Public SelectedColorPen As New Pen(Color.LimeGreen, 2)
    Public VerifyRowPen As New Pen(Color.Cyan)
    Public VerifyRowAlpha As Integer = 100
    Public VerifyRowAlphaIncreasing As Boolean = True

    Public DifficultyDrawRect, ThemeDrawRect, SoundDrawRect, ColorPaletteRect As New Rectangle

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


    Public HoleRectangle, BWRectangle, ChoiceRectangle, ChooseCodeRectangle As New Rectangle

    Public SelectedArcRotation As Integer = 0

    Public Testrect1, Testrect2, TestRect3, Testrect4, Testrect5, Testrect6, Testrect7, Testrect8 As New Rectangle


    Public ChooseCodeRect1, ChooseCodeRect2, ChooseCodeRect3, ChooseCodeRect4, ChooseCodeRect5, ChooseCodeRect6, ChooseCodeRect7, ChooseCodeRect8 As New Rectangle

    Public SelectedSpinning As Boolean = True

    Public GuessBrush As New SolidBrush(Color.Gray)
    Public FocusedHolePen As New Pen(Color.Cyan)

    Public BlackPegBrush As New SolidBrush(Color.Blue)
    Public WhitePegBrush As New SolidBrush(Color.White)
    Public NothingBrush As New SolidBrush(Color.DarkGray)

    Public AIStep As Integer = 0
    Public InvalidatedSteps As Integer = 1

    Public Sub GenerateBoard(ByVal GameMode As Integer, SenderForm As Form, SenderBWPanel As Panel, SenderChoosePanel As Panel)
        With SenderChoosePanel
            .Height = SenderForm.ClientRectangle.Height
            .Width = SenderForm.ClientRectangle.Width
        End With
        HolesList.Clear()

        Select Case holes
            Case 2
            Case 3

            Case 4, 6
                Dim PegWidth As Integer
                If holes = 4 Then
                    PegWidth = 16
                Else
                    PegWidth = 10
                End If
                For m As Integer = 0 To tries - 1
                    For n As Integer = 0 To holes - 1
                        Dim BWPeg As New PictureBox
                        With BWPeg
                            .Visible = False
                            .Width = PegWidth
                            .Height = PegWidth
                            '.Name = "BWHole_" & m * holes + n
                            .Tag = m * holes + n
                            If n < CInt(holes / 2) Then
                                .Top = (38 * tries) - (38 * m)
                                .Left = 10 + n * PegWidth
                            Else
                                .Top = 16 + (38 * tries) - (38 * m)
                                .Left = n * PegWidth - 22
                            End If
                        End With
                        SenderBWPanel.Controls.Add(BWPeg)
                        AddHandler BWPeg.Paint, AddressOf PaintBWHole
                        BWHolesList.Add(BWPeg)
                    Next
                Next
            Case 5
            Case 6, 3
                
            Case 7
            Case 8
        End Select
        For y As Integer = 0 To tries - 1
            For x As Integer = 0 To holes - 1
                Dim Hole As New PictureBox
                With Hole
                    .Visible = False
                    .Width = 32
                    .Height = 32
                    .Top = 38 * tries - 38 * y
                    .Left = 50 + 32 * x
                    .BackColor = Color.Transparent
                    .Tag = y * holes + x
                End With
                AddHandler Hole.Paint, AddressOf PaintHole
                SenderForm.Controls.Add(Hole)
                HolesList.Add(Hole)
            Next
        Next

        For i As Integer = 0 To 7
            Dim Choice As New PictureBox
            With Choice
                .Visible = False
                .Width = 32
                .Height = 32
                If i < 4 Then
                    .Top = 38 * tries + 42
                    .Left = 50 + 32 * i
                Else
                    .Top = 38 * tries + 74
                    .Left = 50 + 32 * (i - 4)
                End If
                .BackColor = Color.Transparent
                .Tag = i
                AddHandler Choice.Paint, AddressOf PaintChoice
                SenderForm.Controls.Add(Choice)
                ChoiceList.Add(Choice)
                ' REMOVE AT A LATER POINT:
                .Visible = True
            End With
            Dim ChooseCode As New PictureBox
            With ChooseCode
                .Visible = False
                .Width = 32
                .Height = 32
                If i < 4 Then
                    .Top = CInt(SenderChoosePanel.ClientRectangle.Height / 2)
                    .Left = CInt(SenderForm.ClientRectangle.Width / 2 + i * 32 - 64)
                Else
                    .Top = CInt(SenderChoosePanel.ClientRectangle.Height / 2 + 32)
                    .Left = CInt(SenderForm.ClientRectangle.Width / 2 + (i - 4) * 32 - 64)
                End If
                .BackColor = Color.Transparent
                .Tag = i
                If i < colours Then
                    AddHandler ChooseCode.Paint, AddressOf PaintChooseCode
                    SenderChoosePanel.Controls.Add(ChooseCode)
                    ChooseCodeList.Add(ChooseCode)
                    .Visible = True
                End If
            End With

        Next
        For j As Integer = 0 To holes - 1
            Dim ChooseCodeHole As New PictureBox
            With ChooseCodeHole
                .Visible = False
                .Width = 32
                .Height = 32
                .Top = CInt(SenderChoosePanel.ClientRectangle.Height / 2) - 42
                .Left = CInt(SenderForm.ClientRectangle.Width / 2 - (holes * 32) / 2 + 32 * j)
                .BackColor = Color.Transparent
                .Tag = j
                .Visible = True
            End With
            SenderChoosePanel.Controls.Add(ChooseCodeHole)
            ChooseCodeHolesList.Add(ChooseCodeHole)
            AddHandler ChooseCodeHole.Paint, AddressOf PaintChooseCodeHole
        Next

        Select Case GameMode
            Case 1 'PvE
                PvEGame.SelectedColorTimer.Enabled = True
            Case 2 ' PvP HTTP
                PvPHTTP.SelectedColorTimer.Enabled = True
        End Select

        Testrect1.Location = ChoiceList.Item(0).ClientRectangle.Location
        Testrect1.Size = ChoiceList.Item(0).ClientRectangle.Size
        Testrect1.Inflate(-5, -5)

        Testrect2.Location = ChoiceList.Item(1).ClientRectangle.Location
        Testrect2.Size = ChoiceList.Item(1).ClientRectangle.Size
        Testrect2.Inflate(-5, -5)

        TestRect3.Location = ChoiceList.Item(2).ClientRectangle.Location
        TestRect3.Size = ChoiceList.Item(2).ClientRectangle.Size
        TestRect3.Inflate(-5, -5)

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

        ChoiceRectangleList.Add(Testrect1)
        ChoiceRectangleList.Add(Testrect2)
        ChoiceRectangleList.Add(TestRect3)
        ChoiceRectangleList.Add(Testrect4)
        ChoiceRectangleList.Add(Testrect5)
        ChoiceRectangleList.Add(Testrect6)
        ChoiceRectangleList.Add(Testrect7)
        ChoiceRectangleList.Add(Testrect8)

        ChooseCodeRect1.Location = ChooseCodeList.Item(0).ClientRectangle.Location
        ChooseCodeRect1.Size = ChooseCodeList.Item(0).ClientRectangle.Size
        ChooseCodeRect1.Inflate(-5, -5)

        ChooseCodeRect2.Location = ChooseCodeList.Item(1).ClientRectangle.Location
        ChooseCodeRect2.Size = ChooseCodeList.Item(1).ClientRectangle.Size
        ChooseCodeRect2.Inflate(-5, -5)

        ChooseCodeRect3.Location = ChooseCodeList.Item(2).ClientRectangle.Location
        ChooseCodeRect3.Size = ChooseCodeList.Item(2).ClientRectangle.Size
        ChooseCodeRect3.Inflate(-5, -5)

        ChooseCodeRect4.Location = ChooseCodeList.Item(3).ClientRectangle.Location
        ChooseCodeRect4.Size = ChooseCodeList.Item(3).ClientRectangle.Size
        ChooseCodeRect4.Inflate(-5, -5)

        ChooseCodeRect5.Location = ChooseCodeList.Item(4).ClientRectangle.Location
        ChooseCodeRect5.Size = ChooseCodeList.Item(4).ClientRectangle.Size
        ChooseCodeRect5.Inflate(-5, -5)

        ChooseCodeRect6.Location = ChooseCodeList.Item(5).ClientRectangle.Location
        ChooseCodeRect6.Size = ChooseCodeList.Item(5).ClientRectangle.Size
        ChooseCodeRect6.Inflate(-5, -5)

        ChooseCodeRect7.Location = ChooseCodeList.Item(6).ClientRectangle.Location
        ChooseCodeRect7.Size = ChooseCodeList.Item(6).ClientRectangle.Size
        ChooseCodeRect7.Inflate(-5, -5)

        ChooseCodeRect8.Location = ChooseCodeList.Item(7).ClientRectangle.Location
        ChooseCodeRect8.Size = ChooseCodeList.Item(7).ClientRectangle.Size
        ChooseCodeRect8.Inflate(-5, -5)

        With ChooseCodeRectangleList
            .Add(ChooseCodeRect1)
            .Add(ChooseCodeRect2)
            .Add(ChooseCodeRect3)
            .Add(ChooseCodeRect4)
            .Add(ChooseCodeRect5)
            .Add(ChooseCodeRect6)
            .Add(ChooseCodeRect7)
            .Add(ChooseCodeRect8)
        End With

        SelectedChooseCodeColor = 0
        SelectedColor = 0

        Select Case GameMode
            Case 1
                PvEGame.ColorTimer.Enabled = True
            Case 2
                PvPHTTP.ColorTimer.Enabled = True
        End Select
        SenderChoosePanel.BringToFront()
    End Sub

    Public Sub ClearBoard()
        Dim iMax As Integer = HolesList.Count - 1
        For i = 0 To iMax
            ' This is a test
            HolesList.Item(i).Invalidate()
            BWHolesList.Item(i).Invalidate()
        Next
    End Sub
    Public Sub PaintHole(senderX As Object, e As PaintEventArgs)
        Dim sender As PictureBox = DirectCast(senderX, PictureBox)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        HoleRectangle = sender.ClientRectangle
        HoleRectangle.Inflate(-2, -2)
        If PvEGame.VerifyRowTimer.Enabled = False AndAlso PvPHTTP.VerifyRowTimer.Enabled = False Then
            If CInt(sender.Tag) = GuessList.Count AndAlso UsersTurn = True AndAlso (PvEGame.HoleGraphicsTimer.Enabled = True OrElse PvPHTTP.HoleGraphicsTimer.Enabled = True) Then
                e.Graphics.DrawEllipse(FocusedHolePen, HoleRectangle)
            Else
                e.Graphics.DrawEllipse(Pens.AliceBlue, HoleRectangle)
                HoleRectangle.Inflate(-6, -6)
                If UsersTurn = True Then
                    If CInt(sender.Tag) < GuessList.Count Then
                        GuessBrush.Color = ColorCodes(GuessList.Item(CInt(sender.Tag)) + 1)
                        e.Graphics.FillEllipse(GuessBrush, HoleRectangle)
                    End If
                Else
                    Dim InversePosition As Integer = (holes * tries) - ((holes + CInt(sender.Tag)) - ((holes + CInt(sender.Tag)) Mod holes)) + (CInt(sender.Tag) Mod holes)
                    '16-((4+8)-((4+8) mod 4))+(8 mod 4)
                    If InversePosition <= (AIAttempts - 1) * holes + AIStep Then ' AndAlso InversePosition < AIGuessList.Count
                        GuessBrush.Color = ColorCodes(AIGuessList.Item(InversePosition) + 1)
                        e.Graphics.FillEllipse(GuessBrush, HoleRectangle)
                    End If
                End If
            End If
        ElseIf CInt(sender.Tag) >= Attempt * holes AndAlso CInt(sender.Tag) < (Attempt + 1) * holes Then
            e.Graphics.DrawEllipse(VerifyRowPen, HoleRectangle)
            HoleRectangle.Inflate(-6, -6)
            GuessBrush.Color = ColorCodes(GuessList.Item(CInt(sender.Tag)) + 1)
            e.Graphics.FillEllipse(GuessBrush, HoleRectangle)
        Else
            e.Graphics.DrawEllipse(Pens.AliceBlue, HoleRectangle)
            If CInt(sender.Tag) < GuessList.Count Then
                HoleRectangle.Inflate(-6, -6)
                GuessBrush.Color = ColorCodes(GuessList.Item(CInt(sender.Tag)) + 1)
                e.Graphics.FillEllipse(GuessBrush, HoleRectangle)
            End If
        End If
    End Sub
    Public Sub PaintBWHole(senderX As Object, e As PaintEventArgs)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        Dim sender As PictureBox = DirectCast(senderX, PictureBox)
        BWRectangle = sender.ClientRectangle
        BWRectangle.Inflate(-2, -2)

        If UsersTurn = True Then
            If CInt(sender.Tag) >= BWCountList.Count Then
                e.Graphics.FillEllipse(Brushes.Red, BWRectangle)
            Else
                Select Case BWCountList.Item(CInt(sender.Tag))
                    Case 0
                        e.Graphics.FillEllipse(NothingBrush, BWRectangle)
                    Case 1
                        e.Graphics.FillEllipse(WhitePegBrush, BWRectangle)
                    Case 2
                        e.Graphics.FillEllipse(BlackPegBrush, BWRectangle)
                End Select
            End If
        Else
            Dim InversePosition As Integer = (holes * tries) - ((holes + CInt(sender.Tag)) - ((holes + CInt(sender.Tag)) Mod holes)) + (CInt(sender.Tag) Mod holes)
            If InversePosition >= AIBWList.Count OrElse InversePosition >= (AIAttempts - 1) * holes + AIStep Then
                e.Graphics.FillEllipse(Brushes.Red, BWRectangle)
            Else
                Select Case AIBWList.Item(InversePosition)
                    Case 0
                        e.Graphics.FillEllipse(NothingBrush, BWRectangle)
                    Case 1
                        e.Graphics.FillEllipse(WhitePegBrush, BWRectangle)
                    Case 2
                        e.Graphics.FillEllipse(BlackPegBrush, BWRectangle)
                End Select
            End If
        End If
    End Sub
    Public Sub PaintChoice(senderX As Object, e As PaintEventArgs)
        Dim sender As PictureBox = DirectCast(senderX, PictureBox)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        ChoiceRectangle = sender.ClientRectangle
        ChoiceRectangle.Inflate(-2, -2)
        If CInt(sender.Tag) < colours Then
            ChoiceBrush.Color = ColorCodes(CInt(sender.Tag) + 1)
            e.Graphics.FillEllipse(ChoiceBrush, ChoiceRectangleList.Item(CInt(sender.Tag)))
        Else
            e.Graphics.FillEllipse(DisabledColorBrush, ChoiceRectangleList.Item(CInt(sender.Tag)))
        End If
        'Debug.Print("Selected spinnning: " & SelectedSpinning)
        If CInt(sender.Tag) = SelectedColor AndAlso SelectedSpinning = True Then
            e.Graphics.DrawArc(SelectedColorPen, ChoiceRectangle, SelectedArcRotation, 45)
            e.Graphics.DrawArc(SelectedColorPen, ChoiceRectangle, SelectedArcRotation + 90, 45)
            e.Graphics.DrawArc(SelectedColorPen, ChoiceRectangle, SelectedArcRotation + 180, 45)
            e.Graphics.DrawArc(SelectedColorPen, ChoiceRectangle, SelectedArcRotation + 270, 45)
        End If
    End Sub

    Public Sub PaintChooseCode(senderX As Object, e As PaintEventArgs)
        Dim sender As PictureBox = DirectCast(senderX, PictureBox)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        ChooseCodeRectangle = sender.ClientRectangle
        ChooseCodeRectangle.Inflate(-2, -2)

        If CInt(sender.Tag) < colours Then
            ChoiceBrush.Color = ColorCodes(CInt(sender.Tag) + 1)
            e.Graphics.FillEllipse(ChoiceBrush, ChooseCodeRectangleList.Item(CInt(sender.Tag)))
        Else
            e.Graphics.FillEllipse(DisabledColorBrush, ChooseCodeRectangleList.Item(CInt(sender.Tag)))
        End If
        If SelectedChooseCodeColor = CInt(sender.Tag) AndAlso SelectedSpinning = True Then
            e.Graphics.DrawArc(SelectedColorPen, ChooseCodeRectangle, SelectedArcRotation, 45)
            e.Graphics.DrawArc(SelectedColorPen, ChooseCodeRectangle, SelectedArcRotation + 90, 45)
            e.Graphics.DrawArc(SelectedColorPen, ChooseCodeRectangle, SelectedArcRotation + 180, 45)
            e.Graphics.DrawArc(SelectedColorPen, ChooseCodeRectangle, SelectedArcRotation + 270, 45)
        End If
    End Sub

    Public Sub PaintChooseCodeHole(senderX As Object, e As PaintEventArgs)
        Dim sender As PictureBox = DirectCast(senderX, PictureBox)
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        HoleRectangle = sender.ClientRectangle
        HoleRectangle.Inflate(-2, -2)
        If PvEGame.VerifyRowTimer.Enabled = False AndAlso PvPHTTP.VerifyRowTimer.Enabled = False Then
            If CInt(sender.Tag) = ChosenCodeList.Count Then
                e.Graphics.DrawEllipse(FocusedHolePen, HoleRectangle)
            Else
                e.Graphics.DrawEllipse(Pens.AliceBlue, HoleRectangle)
                If CInt(sender.Tag) < ChosenCodeList.Count Then
                    HoleRectangle.Inflate(-6, -6)
                    GuessBrush.Color = ColorCodes(ChosenCodeList.Item(CInt(sender.Tag)) + 1)
                    e.Graphics.FillEllipse(GuessBrush, HoleRectangle)
                End If
            End If
        Else 'If sender.Tag >= Attempt * holes AndAlso sender.Tag < (Attempt + 1) * holes AndAlso UsersTurn = True Then
            e.Graphics.DrawEllipse(VerifyRowPen, HoleRectangle)
            HoleRectangle.Inflate(-6, -6)
            GuessBrush.Color = ColorCodes(ChosenCodeList.Item(CInt(sender.Tag)) + 1)
            e.Graphics.FillEllipse(GuessBrush, HoleRectangle)
        End If
    End Sub
End Module
