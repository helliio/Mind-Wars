<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartScreen
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartScreen))
        Me.PicStartButton_Settings = New System.Windows.Forms.PictureBox()
        Me.LabSettings = New System.Windows.Forms.Label()
        Me.LabPvE = New System.Windows.Forms.Label()
        Me.PicStartButton_PvE = New System.Windows.Forms.PictureBox()
        Me.LabPvPLan = New System.Windows.Forms.Label()
        Me.PicStartButton_PvPLan = New System.Windows.Forms.PictureBox()
        Me.LabPvPHTTP = New System.Windows.Forms.Label()
        Me.PicStartButton_PvPHTTP = New System.Windows.Forms.PictureBox()
        Me.LabTutorial = New System.Windows.Forms.Label()
        Me.PicStartButton_Tutorial = New System.Windows.Forms.PictureBox()
        Me.PanelSettings = New System.Windows.Forms.Panel()
        Me.PicCloseSettings = New System.Windows.Forms.PictureBox()
        Me.PanelPvE = New System.Windows.Forms.Panel()
        Me.PicClosePvE = New System.Windows.Forms.PictureBox()
        Me.PanelPvPLan = New System.Windows.Forms.Panel()
        Me.PicClosePvPLAN = New System.Windows.Forms.PictureBox()
        Me.PanelPvPHTTP = New System.Windows.Forms.Panel()
        Me.PicClosePvPHTTP = New System.Windows.Forms.PictureBox()
        Me.PanelTutorial = New System.Windows.Forms.Panel()
        Me.PicCloseTutorial = New System.Windows.Forms.PictureBox()
        Me.PicFormHeader = New System.Windows.Forms.PictureBox()
        Me.ButtonsPanel = New System.Windows.Forms.Panel()
        Me.PicMinimizeForm = New System.Windows.Forms.PictureBox()
        Me.PicCloseForm = New System.Windows.Forms.PictureBox()
        Me.GUITimer = New System.Windows.Forms.Timer(Me.components)
        CType(Me.PicStartButton_Settings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicStartButton_PvE, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicStartButton_PvPLan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicStartButton_PvPHTTP, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicStartButton_Tutorial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelSettings.SuspendLayout()
        CType(Me.PicCloseSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelPvE.SuspendLayout()
        CType(Me.PicClosePvE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelPvPLan.SuspendLayout()
        CType(Me.PicClosePvPLAN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelPvPHTTP.SuspendLayout()
        CType(Me.PicClosePvPHTTP, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelTutorial.SuspendLayout()
        CType(Me.PicCloseTutorial, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ButtonsPanel.SuspendLayout()
        CType(Me.PicMinimizeForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicCloseForm, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PicStartButton_Settings
        '
        Me.PicStartButton_Settings.BackColor = System.Drawing.Color.Transparent
        Me.PicStartButton_Settings.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.ButtonBorderActive1
        Me.PicStartButton_Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PicStartButton_Settings.Location = New System.Drawing.Point(8, 8)
        Me.PicStartButton_Settings.Margin = New System.Windows.Forms.Padding(0)
        Me.PicStartButton_Settings.Name = "PicStartButton_Settings"
        Me.PicStartButton_Settings.Size = New System.Drawing.Size(222, 41)
        Me.PicStartButton_Settings.TabIndex = 0
        Me.PicStartButton_Settings.TabStop = False
        '
        'LabSettings
        '
        Me.LabSettings.BackColor = System.Drawing.Color.Transparent
        Me.LabSettings.ForeColor = System.Drawing.Color.LightCyan
        Me.LabSettings.Location = New System.Drawing.Point(8, 21)
        Me.LabSettings.Margin = New System.Windows.Forms.Padding(0)
        Me.LabSettings.Name = "LabSettings"
        Me.LabSettings.Size = New System.Drawing.Size(222, 28)
        Me.LabSettings.TabIndex = 0
        Me.LabSettings.Text = "Settings..."
        Me.LabSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabPvE
        '
        Me.LabPvE.BackColor = System.Drawing.Color.Transparent
        Me.LabPvE.ForeColor = System.Drawing.Color.SteelBlue
        Me.LabPvE.Location = New System.Drawing.Point(8, 71)
        Me.LabPvE.Margin = New System.Windows.Forms.Padding(0)
        Me.LabPvE.Name = "LabPvE"
        Me.LabPvE.Size = New System.Drawing.Size(222, 28)
        Me.LabPvE.TabIndex = 1
        Me.LabPvE.Text = "Play against AI"
        Me.LabPvE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicStartButton_PvE
        '
        Me.PicStartButton_PvE.BackColor = System.Drawing.Color.Transparent
        Me.PicStartButton_PvE.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.ButtonBorderInactive
        Me.PicStartButton_PvE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PicStartButton_PvE.Location = New System.Drawing.Point(8, 55)
        Me.PicStartButton_PvE.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.PicStartButton_PvE.Name = "PicStartButton_PvE"
        Me.PicStartButton_PvE.Size = New System.Drawing.Size(222, 41)
        Me.PicStartButton_PvE.TabIndex = 2
        Me.PicStartButton_PvE.TabStop = False
        '
        'LabPvPLan
        '
        Me.LabPvPLan.BackColor = System.Drawing.Color.Transparent
        Me.LabPvPLan.ForeColor = System.Drawing.Color.SteelBlue
        Me.LabPvPLan.Location = New System.Drawing.Point(8, 118)
        Me.LabPvPLan.Margin = New System.Windows.Forms.Padding(0)
        Me.LabPvPLan.Name = "LabPvPLan"
        Me.LabPvPLan.Size = New System.Drawing.Size(222, 28)
        Me.LabPvPLan.TabIndex = 2
        Me.LabPvPLan.Text = "PvP (LAN)"
        Me.LabPvPLan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicStartButton_PvPLan
        '
        Me.PicStartButton_PvPLan.BackColor = System.Drawing.Color.Transparent
        Me.PicStartButton_PvPLan.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.ButtonBorderInactive
        Me.PicStartButton_PvPLan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PicStartButton_PvPLan.Location = New System.Drawing.Point(8, 102)
        Me.PicStartButton_PvPLan.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.PicStartButton_PvPLan.Name = "PicStartButton_PvPLan"
        Me.PicStartButton_PvPLan.Size = New System.Drawing.Size(222, 41)
        Me.PicStartButton_PvPLan.TabIndex = 4
        Me.PicStartButton_PvPLan.TabStop = False
        '
        'LabPvPHTTP
        '
        Me.LabPvPHTTP.BackColor = System.Drawing.Color.Transparent
        Me.LabPvPHTTP.ForeColor = System.Drawing.Color.SteelBlue
        Me.LabPvPHTTP.Location = New System.Drawing.Point(8, 165)
        Me.LabPvPHTTP.Margin = New System.Windows.Forms.Padding(0)
        Me.LabPvPHTTP.Name = "LabPvPHTTP"
        Me.LabPvPHTTP.Size = New System.Drawing.Size(222, 28)
        Me.LabPvPHTTP.TabIndex = 3
        Me.LabPvPHTTP.Text = "PvP (internet)"
        Me.LabPvPHTTP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicStartButton_PvPHTTP
        '
        Me.PicStartButton_PvPHTTP.BackColor = System.Drawing.Color.Transparent
        Me.PicStartButton_PvPHTTP.BackgroundImage = CType(resources.GetObject("PicStartButton_PvPHTTP.BackgroundImage"), System.Drawing.Image)
        Me.PicStartButton_PvPHTTP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PicStartButton_PvPHTTP.Location = New System.Drawing.Point(8, 149)
        Me.PicStartButton_PvPHTTP.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.PicStartButton_PvPHTTP.Name = "PicStartButton_PvPHTTP"
        Me.PicStartButton_PvPHTTP.Size = New System.Drawing.Size(222, 41)
        Me.PicStartButton_PvPHTTP.TabIndex = 6
        Me.PicStartButton_PvPHTTP.TabStop = False
        '
        'LabTutorial
        '
        Me.LabTutorial.BackColor = System.Drawing.Color.Transparent
        Me.LabTutorial.ForeColor = System.Drawing.Color.SteelBlue
        Me.LabTutorial.Location = New System.Drawing.Point(8, 212)
        Me.LabTutorial.Margin = New System.Windows.Forms.Padding(0)
        Me.LabTutorial.Name = "LabTutorial"
        Me.LabTutorial.Size = New System.Drawing.Size(222, 28)
        Me.LabTutorial.TabIndex = 4
        Me.LabTutorial.Text = "Tutorial"
        Me.LabTutorial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicStartButton_Tutorial
        '
        Me.PicStartButton_Tutorial.BackColor = System.Drawing.Color.Transparent
        Me.PicStartButton_Tutorial.BackgroundImage = CType(resources.GetObject("PicStartButton_Tutorial.BackgroundImage"), System.Drawing.Image)
        Me.PicStartButton_Tutorial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PicStartButton_Tutorial.Location = New System.Drawing.Point(8, 196)
        Me.PicStartButton_Tutorial.Margin = New System.Windows.Forms.Padding(0, 8, 0, 0)
        Me.PicStartButton_Tutorial.Name = "PicStartButton_Tutorial"
        Me.PicStartButton_Tutorial.Size = New System.Drawing.Size(222, 41)
        Me.PicStartButton_Tutorial.TabIndex = 8
        Me.PicStartButton_Tutorial.TabStop = False
        '
        'PanelSettings
        '
        Me.PanelSettings.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.StartScreenBG
        Me.PanelSettings.Controls.Add(Me.PicCloseSettings)
        Me.PanelSettings.Location = New System.Drawing.Point(262, 57)
        Me.PanelSettings.Name = "PanelSettings"
        Me.PanelSettings.Size = New System.Drawing.Size(226, 260)
        Me.PanelSettings.TabIndex = 9
        Me.PanelSettings.Visible = False
        '
        'PicCloseSettings
        '
        Me.PicCloseSettings.BackColor = System.Drawing.Color.Transparent
        Me.PicCloseSettings.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.ButtonBackActive1
        Me.PicCloseSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PicCloseSettings.Dock = System.Windows.Forms.DockStyle.Top
        Me.PicCloseSettings.Location = New System.Drawing.Point(0, 0)
        Me.PicCloseSettings.Name = "PicCloseSettings"
        Me.PicCloseSettings.Size = New System.Drawing.Size(226, 53)
        Me.PicCloseSettings.TabIndex = 1
        Me.PicCloseSettings.TabStop = False
        Me.PicCloseSettings.Tag = "0"
        '
        'PanelPvE
        '
        Me.PanelPvE.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.StartScreenBG
        Me.PanelPvE.Controls.Add(Me.PicClosePvE)
        Me.PanelPvE.Location = New System.Drawing.Point(0, 315)
        Me.PanelPvE.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelPvE.Name = "PanelPvE"
        Me.PanelPvE.Size = New System.Drawing.Size(226, 84)
        Me.PanelPvE.TabIndex = 10
        Me.PanelPvE.Visible = False
        '
        'PicClosePvE
        '
        Me.PicClosePvE.BackColor = System.Drawing.Color.Transparent
        Me.PicClosePvE.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.ButtonBackActive1
        Me.PicClosePvE.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PicClosePvE.Dock = System.Windows.Forms.DockStyle.Top
        Me.PicClosePvE.Location = New System.Drawing.Point(0, 0)
        Me.PicClosePvE.Name = "PicClosePvE"
        Me.PicClosePvE.Size = New System.Drawing.Size(226, 53)
        Me.PicClosePvE.TabIndex = 0
        Me.PicClosePvE.TabStop = False
        '
        'PanelPvPLan
        '
        Me.PanelPvPLan.Controls.Add(Me.PicClosePvPLAN)
        Me.PanelPvPLan.Location = New System.Drawing.Point(12, 399)
        Me.PanelPvPLan.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelPvPLan.Name = "PanelPvPLan"
        Me.PanelPvPLan.Size = New System.Drawing.Size(226, 27)
        Me.PanelPvPLan.TabIndex = 11
        Me.PanelPvPLan.Visible = False
        '
        'PicClosePvPLAN
        '
        Me.PicClosePvPLAN.BackColor = System.Drawing.Color.DarkRed
        Me.PicClosePvPLAN.Location = New System.Drawing.Point(207, 11)
        Me.PicClosePvPLAN.Name = "PicClosePvPLAN"
        Me.PicClosePvPLAN.Size = New System.Drawing.Size(16, 16)
        Me.PicClosePvPLAN.TabIndex = 0
        Me.PicClosePvPLAN.TabStop = False
        '
        'PanelPvPHTTP
        '
        Me.PanelPvPHTTP.Controls.Add(Me.PicClosePvPHTTP)
        Me.PanelPvPHTTP.Location = New System.Drawing.Point(12, 432)
        Me.PanelPvPHTTP.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelPvPHTTP.Name = "PanelPvPHTTP"
        Me.PanelPvPHTTP.Size = New System.Drawing.Size(226, 27)
        Me.PanelPvPHTTP.TabIndex = 12
        Me.PanelPvPHTTP.Visible = False
        '
        'PicClosePvPHTTP
        '
        Me.PicClosePvPHTTP.BackColor = System.Drawing.Color.DarkRed
        Me.PicClosePvPHTTP.Location = New System.Drawing.Point(207, 11)
        Me.PicClosePvPHTTP.Name = "PicClosePvPHTTP"
        Me.PicClosePvPHTTP.Size = New System.Drawing.Size(16, 16)
        Me.PicClosePvPHTTP.TabIndex = 0
        Me.PicClosePvPHTTP.TabStop = False
        '
        'PanelTutorial
        '
        Me.PanelTutorial.Controls.Add(Me.PicCloseTutorial)
        Me.PanelTutorial.Location = New System.Drawing.Point(12, 465)
        Me.PanelTutorial.Margin = New System.Windows.Forms.Padding(0)
        Me.PanelTutorial.Name = "PanelTutorial"
        Me.PanelTutorial.Size = New System.Drawing.Size(226, 27)
        Me.PanelTutorial.TabIndex = 13
        Me.PanelTutorial.Visible = False
        '
        'PicCloseTutorial
        '
        Me.PicCloseTutorial.BackColor = System.Drawing.Color.DarkRed
        Me.PicCloseTutorial.Location = New System.Drawing.Point(207, 11)
        Me.PicCloseTutorial.Name = "PicCloseTutorial"
        Me.PicCloseTutorial.Size = New System.Drawing.Size(16, 16)
        Me.PicCloseTutorial.TabIndex = 0
        Me.PicCloseTutorial.TabStop = False
        '
        'PicFormHeader
        '
        Me.PicFormHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(2, Byte), Integer))
        Me.PicFormHeader.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.FormHeader
        Me.PicFormHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.PicFormHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PicFormHeader.Location = New System.Drawing.Point(0, 0)
        Me.PicFormHeader.Name = "PicFormHeader"
        Me.PicFormHeader.Size = New System.Drawing.Size(944, 32)
        Me.PicFormHeader.TabIndex = 14
        Me.PicFormHeader.TabStop = False
        '
        'ButtonsPanel
        '
        Me.ButtonsPanel.AutoSize = True
        Me.ButtonsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ButtonsPanel.BackColor = System.Drawing.Color.Transparent
        Me.ButtonsPanel.Controls.Add(Me.LabSettings)
        Me.ButtonsPanel.Controls.Add(Me.PicStartButton_Settings)
        Me.ButtonsPanel.Controls.Add(Me.LabPvE)
        Me.ButtonsPanel.Controls.Add(Me.LabPvPLan)
        Me.ButtonsPanel.Controls.Add(Me.LabTutorial)
        Me.ButtonsPanel.Controls.Add(Me.LabPvPHTTP)
        Me.ButtonsPanel.Controls.Add(Me.PicStartButton_Tutorial)
        Me.ButtonsPanel.Controls.Add(Me.PicStartButton_PvE)
        Me.ButtonsPanel.Controls.Add(Me.PicStartButton_PvPHTTP)
        Me.ButtonsPanel.Controls.Add(Me.PicStartButton_PvPLan)
        Me.ButtonsPanel.Location = New System.Drawing.Point(0, 35)
        Me.ButtonsPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonsPanel.Name = "ButtonsPanel"
        Me.ButtonsPanel.Padding = New System.Windows.Forms.Padding(8)
        Me.ButtonsPanel.Size = New System.Drawing.Size(238, 248)
        Me.ButtonsPanel.TabIndex = 15
        '
        'PicMinimizeForm
        '
        Me.PicMinimizeForm.BackColor = System.Drawing.Color.Transparent
        Me.PicMinimizeForm.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.Minimize
        Me.PicMinimizeForm.Location = New System.Drawing.Point(195, 8)
        Me.PicMinimizeForm.Margin = New System.Windows.Forms.Padding(0)
        Me.PicMinimizeForm.Name = "PicMinimizeForm"
        Me.PicMinimizeForm.Size = New System.Drawing.Size(16, 16)
        Me.PicMinimizeForm.TabIndex = 17
        Me.PicMinimizeForm.TabStop = False
        '
        'PicCloseForm
        '
        Me.PicCloseForm.BackColor = System.Drawing.Color.Transparent
        Me.PicCloseForm.BackgroundImage = Global.Mind_Wars.My.Resources.Resources._Exit
        Me.PicCloseForm.Location = New System.Drawing.Point(214, 8)
        Me.PicCloseForm.Name = "PicCloseForm"
        Me.PicCloseForm.Size = New System.Drawing.Size(16, 16)
        Me.PicCloseForm.TabIndex = 18
        Me.PicCloseForm.TabStop = False
        '
        'GUITimer
        '
        Me.GUITimer.Enabled = True
        Me.GUITimer.Interval = 40
        '
        'StartScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImage = Global.Mind_Wars.My.Resources.Resources.StartScreenBG
        Me.ClientSize = New System.Drawing.Size(944, 504)
        Me.ControlBox = False
        Me.Controls.Add(Me.PicCloseForm)
        Me.Controls.Add(Me.PicMinimizeForm)
        Me.Controls.Add(Me.ButtonsPanel)
        Me.Controls.Add(Me.PicFormHeader)
        Me.Controls.Add(Me.PanelTutorial)
        Me.Controls.Add(Me.PanelPvPHTTP)
        Me.Controls.Add(Me.PanelPvPLan)
        Me.Controls.Add(Me.PanelPvE)
        Me.Controls.Add(Me.PanelSettings)
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "StartScreen"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mind Wars"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(1, Byte), Integer), CType(CType(2, Byte), Integer))
        CType(Me.PicStartButton_Settings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicStartButton_PvE, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicStartButton_PvPLan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicStartButton_PvPHTTP, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicStartButton_Tutorial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelSettings.ResumeLayout(False)
        CType(Me.PicCloseSettings, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelPvE.ResumeLayout(False)
        CType(Me.PicClosePvE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelPvPLan.ResumeLayout(False)
        CType(Me.PicClosePvPLAN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelPvPHTTP.ResumeLayout(False)
        CType(Me.PicClosePvPHTTP, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelTutorial.ResumeLayout(False)
        CType(Me.PicCloseTutorial, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicFormHeader, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ButtonsPanel.ResumeLayout(False)
        CType(Me.PicMinimizeForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicCloseForm, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PicStartButton_Settings As PictureBox
    Friend WithEvents LabSettings As Label
    Friend WithEvents LabPvE As Label
    Friend WithEvents PicStartButton_PvE As PictureBox
    Friend WithEvents LabPvPLan As Label
    Friend WithEvents PicStartButton_PvPLan As PictureBox
    Friend WithEvents LabPvPHTTP As Label
    Friend WithEvents PicStartButton_PvPHTTP As PictureBox
    Friend WithEvents LabTutorial As Label
    Friend WithEvents PicStartButton_Tutorial As PictureBox
    Friend WithEvents PanelSettings As Panel
    Friend WithEvents PanelPvE As Panel
    Friend WithEvents PicClosePvE As PictureBox
    Friend WithEvents PanelPvPLan As Panel
    Friend WithEvents PicClosePvPLAN As PictureBox
    Friend WithEvents PanelPvPHTTP As Panel
    Friend WithEvents PicClosePvPHTTP As PictureBox
    Friend WithEvents PanelTutorial As Panel
    Friend WithEvents PicCloseTutorial As PictureBox
    Friend WithEvents PicFormHeader As PictureBox
    Friend WithEvents ButtonsPanel As Panel
    Friend WithEvents PicMinimizeForm As PictureBox
    Friend WithEvents PicCloseForm As PictureBox
    Friend WithEvents GUITimer As Timer
    Friend WithEvents PicCloseSettings As PictureBox
End Class
