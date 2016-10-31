<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PvPHTTP
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PicFormHeader = New System.Windows.Forms.PictureBox()
        Me.HeaderTransparencyRight = New System.Windows.Forms.PictureBox()
        Me.ChooseCodePanel = New System.Windows.Forms.Panel()
        Me.GamePanel = New System.Windows.Forms.Panel()
        Me.InfoPanel = New System.Windows.Forms.Panel()
        Me.LabInfo = New System.Windows.Forms.Label()
        Me.PicInfoRight = New System.Windows.Forms.PictureBox()
        Me.PicInfoMiddle = New System.Windows.Forms.PictureBox()
        Me.PicInfoLeft = New System.Windows.Forms.PictureBox()
        Me.GameCodePanel = New System.Windows.Forms.Panel()
        Me.LabGameCode = New System.Windows.Forms.Label()
        Me.LabExplanation = New System.Windows.Forms.Label()
        Me.LabActualCode = New System.Windows.Forms.Label()
        Me.BWPanel = New System.Windows.Forms.Panel()
        Me.HeaderTransparencyLeft = New System.Windows.Forms.PictureBox()
        Me.ConnectionBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.ShowHolesTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SelectedColorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ColorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HoleGraphicsTimer = New System.Windows.Forms.Timer(Me.components)
        Me.VerifyRowTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FillBWTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DebugTimer = New System.Windows.Forms.Timer(Me.components)
        Me.CheckStatusBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.Button1 = New System.Windows.Forms.Button()
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HeaderTransparencyRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GamePanel.SuspendLayout()
        Me.InfoPanel.SuspendLayout()
        CType(Me.PicInfoRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicInfoMiddle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicInfoLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GameCodePanel.SuspendLayout()
        Me.BWPanel.SuspendLayout()
        CType(Me.HeaderTransparencyLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PicFormHeader
        '
        Me.PicFormHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(2, Byte), Integer))
        Me.PicFormHeader.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.HeaderBG
        Me.PicFormHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PicFormHeader.Location = New System.Drawing.Point(0, 0)
        Me.PicFormHeader.Name = "PicFormHeader"
        Me.PicFormHeader.Size = New System.Drawing.Size(264, 32)
        Me.PicFormHeader.TabIndex = 20
        Me.PicFormHeader.TabStop = False
        Me.PicFormHeader.WaitOnLoad = True
        '
        'HeaderTransparencyRight
        '
        Me.HeaderTransparencyRight.BackColor = System.Drawing.Color.Maroon
        Me.HeaderTransparencyRight.Image = Global.Mind_Wars.My.Resources.Resources.HeaderTransparencyRight
        Me.HeaderTransparencyRight.Location = New System.Drawing.Point(150, 226)
        Me.HeaderTransparencyRight.Name = "HeaderTransparencyRight"
        Me.HeaderTransparencyRight.Size = New System.Drawing.Size(66, 59)
        Me.HeaderTransparencyRight.TabIndex = 21
        Me.HeaderTransparencyRight.TabStop = False
        '
        'ChooseCodePanel
        '
        Me.ChooseCodePanel.BackColor = System.Drawing.Color.Transparent
        Me.ChooseCodePanel.Location = New System.Drawing.Point(39, 50)
        Me.ChooseCodePanel.Name = "ChooseCodePanel"
        Me.ChooseCodePanel.Size = New System.Drawing.Size(200, 100)
        Me.ChooseCodePanel.TabIndex = 20
        Me.ChooseCodePanel.Visible = False
        '
        'GamePanel
        '
        Me.GamePanel.AutoSize = True
        Me.GamePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GamePanel.BackColor = System.Drawing.Color.Transparent
        Me.GamePanel.Controls.Add(Me.Button1)
        Me.GamePanel.Controls.Add(Me.InfoPanel)
        Me.GamePanel.Controls.Add(Me.GameCodePanel)
        Me.GamePanel.Controls.Add(Me.HeaderTransparencyRight)
        Me.GamePanel.Controls.Add(Me.ChooseCodePanel)
        Me.GamePanel.Controls.Add(Me.BWPanel)
        Me.GamePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GamePanel.Location = New System.Drawing.Point(0, 0)
        Me.GamePanel.Name = "GamePanel"
        Me.GamePanel.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.GamePanel.Size = New System.Drawing.Size(264, 500)
        Me.GamePanel.TabIndex = 21
        Me.GamePanel.Visible = False
        '
        'InfoPanel
        '
        Me.InfoPanel.Controls.Add(Me.LabInfo)
        Me.InfoPanel.Controls.Add(Me.PicInfoRight)
        Me.InfoPanel.Controls.Add(Me.PicInfoMiddle)
        Me.InfoPanel.Controls.Add(Me.PicInfoLeft)
        Me.InfoPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.InfoPanel.Location = New System.Drawing.Point(54, 166)
        Me.InfoPanel.Name = "InfoPanel"
        Me.InfoPanel.Size = New System.Drawing.Size(200, 100)
        Me.InfoPanel.TabIndex = 22
        Me.InfoPanel.Visible = False
        '
        'LabInfo
        '
        Me.LabInfo.Location = New System.Drawing.Point(6, 9)
        Me.LabInfo.Name = "LabInfo"
        Me.LabInfo.Size = New System.Drawing.Size(119, 45)
        Me.LabInfo.TabIndex = 3
        Me.LabInfo.Text = "Your opponent is guessing your code"
        Me.LabInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicInfoRight
        '
        Me.PicInfoRight.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.InfoBoxRight
        Me.PicInfoRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicInfoRight.Location = New System.Drawing.Point(150, 0)
        Me.PicInfoRight.Name = "PicInfoRight"
        Me.PicInfoRight.Size = New System.Drawing.Size(15, 100)
        Me.PicInfoRight.TabIndex = 2
        Me.PicInfoRight.TabStop = False
        '
        'PicInfoMiddle
        '
        Me.PicInfoMiddle.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.InfoBoxMiddle
        Me.PicInfoMiddle.Location = New System.Drawing.Point(21, 0)
        Me.PicInfoMiddle.Name = "PicInfoMiddle"
        Me.PicInfoMiddle.Size = New System.Drawing.Size(95, 64)
        Me.PicInfoMiddle.TabIndex = 1
        Me.PicInfoMiddle.TabStop = False
        '
        'PicInfoLeft
        '
        Me.PicInfoLeft.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.InfoBoxLeft
        Me.PicInfoLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicInfoLeft.Location = New System.Drawing.Point(0, 0)
        Me.PicInfoLeft.Name = "PicInfoLeft"
        Me.PicInfoLeft.Size = New System.Drawing.Size(15, 100)
        Me.PicInfoLeft.TabIndex = 0
        Me.PicInfoLeft.TabStop = False
        '
        'GameCodePanel
        '
        Me.GameCodePanel.Controls.Add(Me.LabGameCode)
        Me.GameCodePanel.Controls.Add(Me.LabExplanation)
        Me.GameCodePanel.Controls.Add(Me.LabActualCode)
        Me.GameCodePanel.Location = New System.Drawing.Point(48, 323)
        Me.GameCodePanel.Name = "GameCodePanel"
        Me.GameCodePanel.Size = New System.Drawing.Size(312, 150)
        Me.GameCodePanel.TabIndex = 24
        '
        'LabGameCode
        '
        Me.LabGameCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LabGameCode.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabGameCode.ForeColor = System.Drawing.Color.White
        Me.LabGameCode.Location = New System.Drawing.Point(5, 10)
        Me.LabGameCode.Name = "LabGameCode"
        Me.LabGameCode.Size = New System.Drawing.Size(154, 50)
        Me.LabGameCode.TabIndex = 22
        Me.LabGameCode.Text = "Game Code"
        Me.LabGameCode.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'LabExplanation
        '
        Me.LabExplanation.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LabExplanation.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabExplanation.ForeColor = System.Drawing.Color.White
        Me.LabExplanation.Location = New System.Drawing.Point(3, 77)
        Me.LabExplanation.Name = "LabExplanation"
        Me.LabExplanation.Size = New System.Drawing.Size(216, 50)
        Me.LabExplanation.TabIndex = 23
        Me.LabExplanation.Text = "Tell your friend to use this number when prompted for a game code"
        Me.LabExplanation.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'LabActualCode
        '
        Me.LabActualCode.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.LabActualCode.Font = New System.Drawing.Font("Courier New", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabActualCode.ForeColor = System.Drawing.Color.White
        Me.LabActualCode.Location = New System.Drawing.Point(3, 40)
        Me.LabActualCode.Name = "LabActualCode"
        Me.LabActualCode.Size = New System.Drawing.Size(201, 37)
        Me.LabActualCode.TabIndex = 23
        Me.LabActualCode.Text = "1234"
        Me.LabActualCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'BWPanel
        '
        Me.BWPanel.BackColor = System.Drawing.Color.Transparent
        Me.BWPanel.Controls.Add(Me.HeaderTransparencyLeft)
        Me.BWPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.BWPanel.Location = New System.Drawing.Point(0, 0)
        Me.BWPanel.Name = "BWPanel"
        Me.BWPanel.Size = New System.Drawing.Size(42, 500)
        Me.BWPanel.TabIndex = 17
        Me.BWPanel.Visible = False
        '
        'HeaderTransparencyLeft
        '
        Me.HeaderTransparencyLeft.BackColor = System.Drawing.Color.Maroon
        Me.HeaderTransparencyLeft.Image = Global.Mind_Wars.My.Resources.Resources.HeaderTransparencyLeft
        Me.HeaderTransparencyLeft.Location = New System.Drawing.Point(0, 0)
        Me.HeaderTransparencyLeft.Name = "HeaderTransparencyLeft"
        Me.HeaderTransparencyLeft.Size = New System.Drawing.Size(66, 59)
        Me.HeaderTransparencyLeft.TabIndex = 20
        Me.HeaderTransparencyLeft.TabStop = False
        '
        'ConnectionBackgroundWorker
        '
        Me.ConnectionBackgroundWorker.WorkerReportsProgress = True
        '
        'ShowHolesTimer
        '
        Me.ShowHolesTimer.Interval = 50
        '
        'SelectedColorTimer
        '
        Me.SelectedColorTimer.Interval = 40
        '
        'ColorTimer
        '
        Me.ColorTimer.Interval = 30
        '
        'HoleGraphicsTimer
        '
        Me.HoleGraphicsTimer.Interval = 80
        '
        'VerifyRowTimer
        '
        Me.VerifyRowTimer.Interval = 30
        '
        'FillBWTimer
        '
        Me.FillBWTimer.Interval = 250
        '
        'DebugTimer
        '
        Me.DebugTimer.Enabled = True
        Me.DebugTimer.Interval = 5000
        '
        'CheckStatusBackgroundWorker
        '
        Me.CheckStatusBackgroundWorker.WorkerSupportsCancellation = True
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(179, 291)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 22
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PvPHTTP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.StartScreenBG1
        Me.ClientSize = New System.Drawing.Size(264, 500)
        Me.ControlBox = False
        Me.Controls.Add(Me.PicFormHeader)
        Me.Controls.Add(Me.GamePanel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "PvPHTTP"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mind Wars"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(2, Byte), Integer))
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HeaderTransparencyRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GamePanel.ResumeLayout(False)
        Me.InfoPanel.ResumeLayout(False)
        CType(Me.PicInfoRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicInfoMiddle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicInfoLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GameCodePanel.ResumeLayout(False)
        Me.BWPanel.ResumeLayout(False)
        CType(Me.HeaderTransparencyLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PicFormHeader As PictureBox
    Friend WithEvents HeaderTransparencyRight As PictureBox
    Friend WithEvents ChooseCodePanel As Panel
    Friend WithEvents GamePanel As Panel
    Friend WithEvents BWPanel As Panel
    Friend WithEvents HeaderTransparencyLeft As PictureBox
    Public WithEvents ConnectionBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents ShowHolesTimer As Timer
    Friend WithEvents SelectedColorTimer As Timer
    Friend WithEvents ColorTimer As Timer
    Friend WithEvents HoleGraphicsTimer As Timer
    Friend WithEvents VerifyRowTimer As Timer
    Friend WithEvents FillBWTimer As Timer
    Friend WithEvents DebugTimer As Timer
    Friend WithEvents GameCodePanel As Panel
    Friend WithEvents LabGameCode As Label
    Friend WithEvents LabExplanation As Label
    Friend WithEvents LabActualCode As Label
    Friend WithEvents CheckStatusBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents InfoPanel As Panel
    Friend WithEvents PicInfoLeft As PictureBox
    Friend WithEvents PicInfoRight As PictureBox
    Friend WithEvents PicInfoMiddle As PictureBox
    Friend WithEvents LabInfo As Label
    Friend WithEvents Button1 As Button
End Class
