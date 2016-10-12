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
        Me.PicStartButton_Settings = New System.Windows.Forms.PictureBox()
        Me.LabSettings = New System.Windows.Forms.Label()
        Me.LabPvE = New System.Windows.Forms.Label()
        Me.PicStartButton_PvE = New System.Windows.Forms.PictureBox()
        CType(Me.PicStartButton_Settings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicStartButton_PvE, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PicStartButton_Settings
        '
        Me.PicStartButton_Settings.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.PicStartButton_Settings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicStartButton_Settings.Location = New System.Drawing.Point(12, 12)
        Me.PicStartButton_Settings.Name = "PicStartButton_Settings"
        Me.PicStartButton_Settings.Size = New System.Drawing.Size(222, 41)
        Me.PicStartButton_Settings.TabIndex = 0
        Me.PicStartButton_Settings.TabStop = False
        '
        'LabSettings
        '
        Me.LabSettings.BackColor = System.Drawing.Color.Transparent
        Me.LabSettings.Location = New System.Drawing.Point(12, 25)
        Me.LabSettings.Name = "LabSettings"
        Me.LabSettings.Size = New System.Drawing.Size(222, 28)
        Me.LabSettings.TabIndex = 1
        Me.LabSettings.Text = "Settings..."
        Me.LabSettings.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LabPvE
        '
        Me.LabPvE.BackColor = System.Drawing.Color.Transparent
        Me.LabPvE.Location = New System.Drawing.Point(12, 69)
        Me.LabPvE.Name = "LabPvE"
        Me.LabPvE.Size = New System.Drawing.Size(222, 28)
        Me.LabPvE.TabIndex = 3
        Me.LabPvE.Text = "Play against AI"
        Me.LabPvE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PicStartButton_PvE
        '
        Me.PicStartButton_PvE.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.PicStartButton_PvE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PicStartButton_PvE.Location = New System.Drawing.Point(12, 56)
        Me.PicStartButton_PvE.Name = "PicStartButton_PvE"
        Me.PicStartButton_PvE.Size = New System.Drawing.Size(222, 41)
        Me.PicStartButton_PvE.TabIndex = 2
        Me.PicStartButton_PvE.TabStop = False
        '
        'StartScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(246, 261)
        Me.Controls.Add(Me.LabPvE)
        Me.Controls.Add(Me.PicStartButton_PvE)
        Me.Controls.Add(Me.LabSettings)
        Me.Controls.Add(Me.PicStartButton_Settings)
        Me.ForeColor = System.Drawing.Color.White
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "StartScreen"
        Me.Text = "Mind Wars"
        CType(Me.PicStartButton_Settings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicStartButton_PvE, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PicStartButton_Settings As PictureBox
    Friend WithEvents LabSettings As Label
    Friend WithEvents LabPvE As Label
    Friend WithEvents PicStartButton_PvE As PictureBox
End Class
