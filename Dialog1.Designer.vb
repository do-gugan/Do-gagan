<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dialog1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Cmt_Text = New System.Windows.Forms.TextBox
        Me.tc_hour = New System.Windows.Forms.NumericUpDown
        Me.tc_min = New System.Windows.Forms.NumericUpDown
        Me.tc_sec = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.tc_hour, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tc_min, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tc_sec, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(344, 44)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 27)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 21)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 21)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "キャンセル"
        '
        'Cmt_Text
        '
        Me.Cmt_Text.Location = New System.Drawing.Point(179, 11)
        Me.Cmt_Text.Name = "Cmt_Text"
        Me.Cmt_Text.Size = New System.Drawing.Size(304, 19)
        Me.Cmt_Text.TabIndex = 1
        '
        'tc_hour
        '
        Me.tc_hour.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.tc_hour.Location = New System.Drawing.Point(13, 12)
        Me.tc_hour.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.tc_hour.Name = "tc_hour"
        Me.tc_hour.Size = New System.Drawing.Size(36, 19)
        Me.tc_hour.TabIndex = 2
        Me.tc_hour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tc_min
        '
        Me.tc_min.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.tc_min.Location = New System.Drawing.Point(68, 12)
        Me.tc_min.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.tc_min.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.tc_min.Name = "tc_min"
        Me.tc_min.Size = New System.Drawing.Size(36, 19)
        Me.tc_min.TabIndex = 3
        Me.tc_min.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'tc_sec
        '
        Me.tc_sec.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.tc_sec.Location = New System.Drawing.Point(121, 13)
        Me.tc_sec.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.tc_sec.Minimum = New Decimal(New Integer() {1, 0, 0, -2147483648})
        Me.tc_sec.Name = "tc_sec"
        Me.tc_sec.Size = New System.Drawing.Size(36, 19)
        Me.tc_sec.TabIndex = 4
        Me.tc_sec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(55, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(7, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = ":"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(108, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(7, 12)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = ":"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "(時)"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(79, 34)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 12)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "(分)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(132, 34)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(25, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "(秒)"
        '
        'Dialog1
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(502, 82)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tc_sec)
        Me.Controls.Add(Me.tc_min)
        Me.Controls.Add(Me.tc_hour)
        Me.Controls.Add(Me.Cmt_Text)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Dialog1"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "コメント修正"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.tc_hour, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tc_min, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tc_sec, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Cmt_Text As System.Windows.Forms.TextBox
    Friend WithEvents tc_hour As System.Windows.Forms.NumericUpDown
    Friend WithEvents tc_min As System.Windows.Forms.NumericUpDown
    Friend WithEvents tc_sec As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class
