<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Singleplayer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ChooseCodePanel = New System.Windows.Forms.PictureBox()
        Me.NewAIBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.StealthyPopulateBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.DebugTimer = New System.Windows.Forms.Timer(Me.components)
        Me.FillBWTimer = New System.Windows.Forms.Timer(Me.components)
        Me.VerifyRowTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HoleGraphicsTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ColorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SelectedColorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.ShowHolesTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HeaderTransparencyRight = New System.Windows.Forms.PictureBox()
        Me.InitializeBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.PicInitialLoadProgress = New System.Windows.Forms.PictureBox()
        Me.LoadCompleteTimer = New System.Windows.Forms.Timer(Me.components)
        Me.HeaderTransparencyLeft = New System.Windows.Forms.PictureBox()
        Me.BWPanel = New System.Windows.Forms.Panel()
        Me.InfoPanel = New System.Windows.Forms.Panel()
        Me.LabInfo = New System.Windows.Forms.Label()
        Me.PicInfoRight = New System.Windows.Forms.PictureBox()
        Me.PicInfoMiddle = New System.Windows.Forms.PictureBox()
        Me.PicInfoLeft = New System.Windows.Forms.PictureBox()
        Me.PicCloseForm = New System.Windows.Forms.PictureBox()
        Me.PicMinimizeForm = New System.Windows.Forms.PictureBox()
        Me.PicFormHeader = New System.Windows.Forms.PictureBox()
        CType(Me.ChooseCodePanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HeaderTransparencyRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicInitialLoadProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HeaderTransparencyLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.InfoPanel.SuspendLayout()
        CType(Me.PicInfoRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicInfoMiddle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicInfoLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicCloseForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicMinimizeForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ChooseCodePanel
        '
        Me.ChooseCodePanel.BackColor = System.Drawing.Color.Transparent
        Me.ChooseCodePanel.Location = New System.Drawing.Point(70, 103)
        Me.ChooseCodePanel.Name = "ChooseCodePanel"
        Me.ChooseCodePanel.Size = New System.Drawing.Size(100, 50)
        Me.ChooseCodePanel.TabIndex = 25
        Me.ChooseCodePanel.TabStop = False
        Me.ChooseCodePanel.Visible = False
        '
        'DebugTimer
        '
        Me.DebugTimer.Enabled = True
        Me.DebugTimer.Interval = 5000
        '
        'FillBWTimer
        '
        Me.FillBWTimer.Interval = 250
        '
        'VerifyRowTimer
        '
        Me.VerifyRowTimer.Interval = 30
        '
        'HoleGraphicsTimer
        '
        Me.HoleGraphicsTimer.Interval = 80
        '
        'ColorTimer
        '
        Me.ColorTimer.Interval = 30
        '
        'SelectedColorTimer
        '
        Me.SelectedColorTimer.Interval = 40
        '
        'ShowHolesTimer
        '
        Me.ShowHolesTimer.Interval = 50
        '
        'HeaderTransparencyRight
        '
        Me.HeaderTransparencyRight.BackColor = System.Drawing.Color.Maroon
        Me.HeaderTransparencyRight.Image = Global.Mind_Wars.My.Resources.Resources.HeaderTransparencyRight
        Me.HeaderTransparencyRight.Location = New System.Drawing.Point(84, 38)
        Me.HeaderTransparencyRight.Name = "HeaderTransparencyRight"
        Me.HeaderTransparencyRight.Size = New System.Drawing.Size(66, 59)
        Me.HeaderTransparencyRight.TabIndex = 32
        Me.HeaderTransparencyRight.TabStop = False
        '
        'InitializeBackgroundWorker
        '
        Me.InitializeBackgroundWorker.WorkerReportsProgress = True
        '
        'PicInitialLoadProgress
        '
        Me.PicInitialLoadProgress.BackColor = System.Drawing.Color.Transparent
        Me.PicInitialLoadProgress.Location = New System.Drawing.Point(48, 224)
        Me.PicInitialLoadProgress.Name = "PicInitialLoadProgress"
        Me.PicInitialLoadProgress.Size = New System.Drawing.Size(80, 80)
        Me.PicInitialLoadProgress.TabIndex = 27
        Me.PicInitialLoadProgress.TabStop = False
        '
        'LoadCompleteTimer
        '
        Me.LoadCompleteTimer.Interval = 30
        '
        'HeaderTransparencyLeft
        '
        Me.HeaderTransparencyLeft.BackColor = System.Drawing.Color.Maroon
        Me.HeaderTransparencyLeft.Image = Global.Mind_Wars.My.Resources.Resources.HeaderTransparencyLeft
        Me.HeaderTransparencyLeft.Location = New System.Drawing.Point(84, 159)
        Me.HeaderTransparencyLeft.Name = "HeaderTransparencyLeft"
        Me.HeaderTransparencyLeft.Size = New System.Drawing.Size(36, 59)
        Me.HeaderTransparencyLeft.TabIndex = 30
        Me.HeaderTransparencyLeft.TabStop = False
        '
        'BWPanel
        '
        Me.BWPanel.BackColor = System.Drawing.Color.Transparent
        Me.BWPanel.CausesValidation = False
        Me.BWPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.BWPanel.Location = New System.Drawing.Point(0, 0)
        Me.BWPanel.Name = "BWPanel"
        Me.BWPanel.Size = New System.Drawing.Size(42, 712)
        Me.BWPanel.TabIndex = 28
        Me.BWPanel.Visible = False
        '
        'InfoPanel
        '
        Me.InfoPanel.BackColor = System.Drawing.Color.Transparent
        Me.InfoPanel.Controls.Add(Me.LabInfo)
        Me.InfoPanel.Controls.Add(Me.PicInfoRight)
        Me.InfoPanel.Controls.Add(Me.PicInfoMiddle)
        Me.InfoPanel.Controls.Add(Me.PicInfoLeft)
        Me.InfoPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.InfoPanel.Location = New System.Drawing.Point(60, 402)
        Me.InfoPanel.Name = "InfoPanel"
        Me.InfoPanel.Size = New System.Drawing.Size(193, 85)
        Me.InfoPanel.TabIndex = 38
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
        Me.PicCloseForm.TabIndex = 41
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
        Me.PicMinimizeForm.TabIndex = 40
        Me.PicMinimizeForm.TabStop = False
        '
        'PicFormHeader
        '
        Me.PicFormHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(2, Byte), Integer))
        Me.PicFormHeader.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.HeaderBG
        Me.PicFormHeader.Location = New System.Drawing.Point(0, 0)
        Me.PicFormHeader.Name = "PicFormHeader"
        Me.PicFormHeader.Size = New System.Drawing.Size(251, 32)
        Me.PicFormHeader.TabIndex = 39
        Me.PicFormHeader.TabStop = False
        Me.PicFormHeader.WaitOnLoad = True
        '
        'Singleplayer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.StartScreenBG1
        Me.ClientSize = New System.Drawing.Size(251, 712)
        Me.Controls.Add(Me.PicCloseForm)
        Me.Controls.Add(Me.PicMinimizeForm)
        Me.Controls.Add(Me.PicFormHeader)
        Me.Controls.Add(Me.InfoPanel)
        Me.Controls.Add(Me.ChooseCodePanel)
        Me.Controls.Add(Me.HeaderTransparencyRight)
        Me.Controls.Add(Me.PicInitialLoadProgress)
        Me.Controls.Add(Me.HeaderTransparencyLeft)
        Me.Controls.Add(Me.BWPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Singleplayer"
        Me.Text = "Singleplayer"
        CType(Me.ChooseCodePanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HeaderTransparencyRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicInitialLoadProgress, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HeaderTransparencyLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.InfoPanel.ResumeLayout(False)
        CType(Me.PicInfoRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicInfoMiddle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicInfoLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicCloseForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicMinimizeForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ChooseCodePanel As PictureBox
    Friend WithEvents NewAIBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents StealthyPopulateBackgroundWorker As System.ComponentModel.BackgroundWorker
    Friend WithEvents DebugTimer As Timer
    Friend WithEvents FillBWTimer As Timer
    Friend WithEvents VerifyRowTimer As Timer
    Friend WithEvents HoleGraphicsTimer As Timer
    Friend WithEvents ColorTimer As Timer
    Public WithEvents SelectedColorTimer As Timer
    Friend WithEvents ShowHolesTimer As Timer
    Friend WithEvents HeaderTransparencyRight As PictureBox
    Public WithEvents InitializeBackgroundWorker As System.ComponentModel.BackgroundWorker
    Public WithEvents PicInitialLoadProgress As PictureBox
    Friend WithEvents LoadCompleteTimer As Timer
    Friend WithEvents HeaderTransparencyLeft As PictureBox
    Friend WithEvents BWPanel As Panel
    Friend WithEvents InfoPanel As Panel
    Friend WithEvents LabInfo As Label
    Friend WithEvents PicInfoRight As PictureBox
    Friend WithEvents PicInfoMiddle As PictureBox
    Friend WithEvents PicInfoLeft As PictureBox
    Friend WithEvents PicCloseForm As PictureBox
    Friend WithEvents PicMinimizeForm As PictureBox
    Friend WithEvents PicFormHeader As PictureBox
End Class
