<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PvEGame
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
        Me.PicFormHeader = New System.Windows.Forms.PictureBox()
        Me.AIBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.InitializeBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.PicInitialLoadProgress = New System.Windows.Forms.PictureBox()
        Me.InitializeDelay = New System.Windows.Forms.Timer(Me.components)
        Me.LoadCompleteTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GamePanel = New System.Windows.Forms.Panel()
        Me.BWPanel = New System.Windows.Forms.Panel()
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicInitialLoadProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GamePanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'PicFormHeader
        '
        Me.PicFormHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(2, Byte), Integer))
        Me.PicFormHeader.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.FormHeader
        Me.PicFormHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicFormHeader.Dock = System.Windows.Forms.DockStyle.Top
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
        Me.PicInitialLoadProgress.Location = New System.Drawing.Point(12, 136)
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
        Me.TextBox1.Location = New System.Drawing.Point(12, 363)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 17
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 389)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Test"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GamePanel
        '
        Me.GamePanel.AutoSize = True
        Me.GamePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GamePanel.BackColor = System.Drawing.Color.Transparent
        Me.GamePanel.Controls.Add(Me.PicInitialLoadProgress)
        Me.GamePanel.Controls.Add(Me.BWPanel)
        Me.GamePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GamePanel.Location = New System.Drawing.Point(0, 0)
        Me.GamePanel.Name = "GamePanel"
        Me.GamePanel.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.GamePanel.Size = New System.Drawing.Size(251, 712)
        Me.GamePanel.TabIndex = 19
        Me.GamePanel.Visible = False
        '
        'BWPanel
        '
        Me.BWPanel.BackColor = System.Drawing.Color.Transparent
        Me.BWPanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.BWPanel.Location = New System.Drawing.Point(0, 0)
        Me.BWPanel.Name = "BWPanel"
        Me.BWPanel.Size = New System.Drawing.Size(42, 712)
        Me.BWPanel.TabIndex = 17
        Me.BWPanel.Visible = False
        '
        'PvEGame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.StartScreenBG1
        Me.ClientSize = New System.Drawing.Size(251, 712)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.PicFormHeader)
        Me.Controls.Add(Me.GamePanel)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "PvEGame"
        Me.Text = "PvE"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(2, Byte), Integer))
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicInitialLoadProgress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GamePanel.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PicFormHeader As PictureBox
    Friend WithEvents AIBackgroundWorker As System.ComponentModel.BackgroundWorker
    Public WithEvents InitializeBackgroundWorker As System.ComponentModel.BackgroundWorker
    Public WithEvents PicInitialLoadProgress As PictureBox
    Friend WithEvents InitializeDelay As Timer
    Friend WithEvents LoadCompleteTimer As Timer
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents GamePanel As Panel
    Friend WithEvents BWPanel As Panel
End Class
