<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PvEGame
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
        Me.AIBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.InitializeBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.PicInitialLoadProgress = New System.Windows.Forms.PictureBox()
        Me.InitializeDelay = New System.Windows.Forms.Timer(Me.components)
        Me.LoadCompleteTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.InfoPanel = New System.Windows.Forms.Panel()
        Me.LabInfo = New System.Windows.Forms.Label()
        Me.PicInfoRight = New System.Windows.Forms.PictureBox()
        Me.PicInfoMiddle = New System.Windows.Forms.PictureBox()
        Me.PicInfoLeft = New System.Windows.Forms.PictureBox()
        Me.PicCloseForm = New System.Windows.Forms.PictureBox()
        Me.PicMinimizeForm = New System.Windows.Forms.PictureBox()
        Me.HeaderTransparencyRight = New System.Windows.Forms.PictureBox()
        Me.ChooseCodePanel = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.BWPanel = New System.Windows.Forms.Panel()
        Me.HeaderTransparencyLeft = New System.Windows.Forms.PictureBox()
        Me.ShowHolesTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SelectedColorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ColorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HoleGraphicsTimer = New System.Windows.Forms.Timer(Me.components)
        Me.AIBackgroundWorkerEasy = New System.ComponentModel.BackgroundWorker()
        Me.VerifyRowTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FillBWTimer = New System.Windows.Forms.Timer(Me.components)
        Me.DebugTimer = New System.Windows.Forms.Timer(Me.components)
        Me.AITimer = New System.Windows.Forms.Timer(Me.components)
        Me.StealthyPopulateBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicInitialLoadProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.InfoPanel.SuspendLayout()
        CType(Me.PicInfoRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicInfoMiddle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicInfoLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicCloseForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicMinimizeForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HeaderTransparencyRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.BWPanel.SuspendLayout()
        CType(Me.HeaderTransparencyLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PicFormHeader
        '
        Me.PicFormHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(2, Byte), Integer))
        Me.PicFormHeader.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.HeaderBG
        Me.PicFormHeader.Location = New System.Drawing.Point(0, 0)
        Me.PicFormHeader.Name = "PicFormHeader"
        Me.PicFormHeader.Size = New System.Drawing.Size(251, 32)
        Me.PicFormHeader.TabIndex = 15
        Me.PicFormHeader.TabStop = False
        Me.PicFormHeader.WaitOnLoad = True
        '
        'AIBackgroundWorker
        '
        Me.AIBackgroundWorker.WorkerReportsProgress = True
        '
        'InitializeBackgroundWorker
        '
        Me.InitializeBackgroundWorker.WorkerReportsProgress = True
        '
        'PicInitialLoadProgress
        '
        Me.PicInitialLoadProgress.BackColor = System.Drawing.Color.Transparent
        Me.PicInitialLoadProgress.Location = New System.Drawing.Point(49, 290)
        Me.PicInitialLoadProgress.Name = "PicInitialLoadProgress"
        Me.PicInitialLoadProgress.Size = New System.Drawing.Size(80, 80)
        Me.PicInitialLoadProgress.TabIndex = 16
        Me.PicInitialLoadProgress.TabStop = False
        '
        'InitializeDelay
        '
        '
        'LoadCompleteTimer
        '
        Me.LoadCompleteTimer.Interval = 30
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(3, 182)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(36, 20)
        Me.TextBox1.TabIndex = 17
        Me.TextBox1.Visible = False
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(3, 124)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(36, 23)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Test"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'InfoPanel
        '
        Me.InfoPanel.Controls.Add(Me.LabInfo)
        Me.InfoPanel.Controls.Add(Me.PicInfoRight)
        Me.InfoPanel.Controls.Add(Me.PicInfoMiddle)
        Me.InfoPanel.Controls.Add(Me.PicInfoLeft)
        Me.InfoPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.InfoPanel.Location = New System.Drawing.Point(48, 393)
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
        'PicCloseForm
        '
        Me.PicCloseForm.BackColor = System.Drawing.Color.Transparent
        Me.PicCloseForm.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.Exit1
        Me.PicCloseForm.Location = New System.Drawing.Point(226, 8)
        Me.PicCloseForm.Name = "PicCloseForm"
        Me.PicCloseForm.Size = New System.Drawing.Size(16, 16)
        Me.PicCloseForm.TabIndex = 21
        Me.PicCloseForm.TabStop = False
        '
        'PicMinimizeForm
        '
        Me.PicMinimizeForm.BackColor = System.Drawing.Color.Transparent
        Me.PicMinimizeForm.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.Minimize
        Me.PicMinimizeForm.Location = New System.Drawing.Point(207, 8)
        Me.PicMinimizeForm.Margin = New System.Windows.Forms.Padding(0)
        Me.PicMinimizeForm.Name = "PicMinimizeForm"
        Me.PicMinimizeForm.Size = New System.Drawing.Size(16, 16)
        Me.PicMinimizeForm.TabIndex = 20
        Me.PicMinimizeForm.TabStop = False
        '
        'HeaderTransparencyRight
        '
        Me.HeaderTransparencyRight.BackColor = System.Drawing.Color.Maroon
        Me.HeaderTransparencyRight.Image = Global.Mind_Wars.My.Resources.Resources.HeaderTransparencyRight
        Me.HeaderTransparencyRight.Location = New System.Drawing.Point(63, 38)
        Me.HeaderTransparencyRight.Name = "HeaderTransparencyRight"
        Me.HeaderTransparencyRight.Size = New System.Drawing.Size(66, 59)
        Me.HeaderTransparencyRight.TabIndex = 21
        Me.HeaderTransparencyRight.TabStop = False
        '
        'ChooseCodePanel
        '
        Me.ChooseCodePanel.BackColor = System.Drawing.Color.Transparent
        Me.ChooseCodePanel.Location = New System.Drawing.Point(154, 37)
        Me.ChooseCodePanel.Name = "ChooseCodePanel"
        Me.ChooseCodePanel.Size = New System.Drawing.Size(200, 100)
        Me.ChooseCodePanel.TabIndex = 20
        Me.ChooseCodePanel.Visible = False
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(113, 198)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 20
        Me.Button2.Text = "TestEasy"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'BWPanel
        '
        Me.BWPanel.BackColor = System.Drawing.Color.Transparent
        Me.BWPanel.CausesValidation = False
        Me.BWPanel.Controls.Add(Me.Button1)
        Me.BWPanel.Controls.Add(Me.TextBox1)
        Me.BWPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.BWPanel.Location = New System.Drawing.Point(0, 0)
        Me.BWPanel.Name = "BWPanel"
        Me.BWPanel.Size = New System.Drawing.Size(42, 712)
        Me.BWPanel.TabIndex = 17
        Me.BWPanel.Visible = False
        '
        'HeaderTransparencyLeft
        '
        Me.HeaderTransparencyLeft.BackColor = System.Drawing.Color.Maroon
        Me.HeaderTransparencyLeft.Image = Global.Mind_Wars.My.Resources.Resources.HeaderTransparencyLeft
        Me.HeaderTransparencyLeft.Location = New System.Drawing.Point(71, 143)
        Me.HeaderTransparencyLeft.Name = "HeaderTransparencyLeft"
        Me.HeaderTransparencyLeft.Size = New System.Drawing.Size(36, 59)
        Me.HeaderTransparencyLeft.TabIndex = 20
        Me.HeaderTransparencyLeft.TabStop = False
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
        'AIBackgroundWorkerEasy
        '
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
        'AITimer
        '
        Me.AITimer.Interval = 250
        'StealthyPopulateBackgroundWorker
        '
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(89, 227)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 23
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'PvEGame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.StartScreenBG1
        Me.ClientSize = New System.Drawing.Size(251, 712)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.InfoPanel)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PicCloseForm)
        Me.Controls.Add(Me.PicMinimizeForm)
        Me.Controls.Add(Me.PicFormHeader)
        Me.Controls.Add(Me.HeaderTransparencyLeft)
        Me.Controls.Add(Me.ChooseCodePanel)
        Me.Controls.Add(Me.HeaderTransparencyRight)
        Me.Controls.Add(Me.PicInitialLoadProgress)
        Me.Controls.Add(Me.BWPanel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.Name = "PvEGame"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mind Wars"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(2, Byte), Integer))
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicInitialLoadProgress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.InfoPanel.ResumeLayout(False)
        CType(Me.PicInfoRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicInfoMiddle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicInfoLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicCloseForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicMinimizeForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HeaderTransparencyRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.BWPanel.ResumeLayout(False)
        Me.BWPanel.PerformLayout()
        CType(Me.HeaderTransparencyLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PicFormHeader As PictureBox
    Friend WithEvents AIBackgroundWorker As System.ComponentModel.BackgroundWorker
    Public WithEvents InitializeBackgroundWorker As System.ComponentModel.BackgroundWorker
    Public WithEvents PicInitialLoadProgress As PictureBox
    Friend WithEvents InitializeDelay As Timer
    Friend WithEvents LoadCompleteTimer As Timer
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents BWPanel As Panel
    Friend WithEvents ShowHolesTimer As Timer
    Friend WithEvents ColorTimer As Timer
    Friend WithEvents HoleGraphicsTimer As Timer
    Friend WithEvents Button2 As Button
    Friend WithEvents AIBackgroundWorkerEasy As System.ComponentModel.BackgroundWorker
    Friend WithEvents VerifyRowTimer As Timer
    Friend WithEvents FillBWTimer As Timer
    Friend WithEvents ChooseCodePanel As Panel
    Friend WithEvents DebugTimer As Timer
    Friend WithEvents HeaderTransparencyLeft As PictureBox
    Friend WithEvents HeaderTransparencyRight As PictureBox
    Friend WithEvents AITimer As Timer
    Friend WithEvents PicCloseForm As PictureBox
    Friend WithEvents PicMinimizeForm As PictureBox
    Friend WithEvents LabInfo As Label
    Friend WithEvents PicInfoRight As PictureBox
    Friend WithEvents PicInfoMiddle As PictureBox
    Friend WithEvents PicInfoLeft As PictureBox
    Friend WithEvents InfoPanel As Panel
    Friend WithEvents StealthyPopulateBackgroundWorker As System.ComponentModel.BackgroundWorker

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public WithEvents SelectedColorTimer As Timer
    Friend WithEvents Button3 As Button
End Class
