Public Class Form3
    Inherits System.Windows.Forms.Form

#Region " Windows フォーム デザイナで生成されたコード "

    Public Sub New()
        MyBase.New()

        ' この呼び出しは Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後に初期化を追加します。

    End Sub

    ' Form は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    ' メモ : 以下のプロシージャは、Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使って変更してください。  
    ' コード エディタを使って変更しないでください。
    Friend WithEvents play_btn As System.Windows.Forms.Button
    Friend WithEvents jump_btn As System.Windows.Forms.Button
    Friend WithEvents back_btn As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents FwSec As System.Windows.Forms.NumericUpDown
    Friend WithEvents BackSec As System.Windows.Forms.NumericUpDown
    Friend WithEvents Pos As System.Windows.Forms.NumericUpDown
    Friend WithEvents PosMin As System.Windows.Forms.NumericUpDown
    Friend WithEvents Btn_Rframe As System.Windows.Forms.Button
    Friend WithEvents SS_Button As System.Windows.Forms.Button
    Friend WithEvents Btn_Fframe As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.play_btn = New System.Windows.Forms.Button()
        Me.jump_btn = New System.Windows.Forms.Button()
        Me.back_btn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.FwSec = New System.Windows.Forms.NumericUpDown()
        Me.BackSec = New System.Windows.Forms.NumericUpDown()
        Me.PosMin = New System.Windows.Forms.NumericUpDown()
        Me.Pos = New System.Windows.Forms.NumericUpDown()
        Me.Btn_Rframe = New System.Windows.Forms.Button()
        Me.Btn_Fframe = New System.Windows.Forms.Button()
        Me.SS_Button = New System.Windows.Forms.Button()
        CType(Me.FwSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BackSec, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PosMin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'play_btn
        '
        Me.play_btn.Location = New System.Drawing.Point(8, 8)
        Me.play_btn.Name = "play_btn"
        Me.play_btn.Size = New System.Drawing.Size(128, 32)
        Me.play_btn.TabIndex = 2
        Me.play_btn.Text = "再生/一時停止(&S)"
        '
        'jump_btn
        '
        Me.jump_btn.Location = New System.Drawing.Point(261, 13)
        Me.jump_btn.Name = "jump_btn"
        Me.jump_btn.Size = New System.Drawing.Size(96, 23)
        Me.jump_btn.TabIndex = 5
        Me.jump_btn.Text = "秒目へ移動(&J)"
        '
        'back_btn
        '
        Me.back_btn.Location = New System.Drawing.Point(209, 45)
        Me.back_btn.Name = "back_btn"
        Me.back_btn.Size = New System.Drawing.Size(80, 24)
        Me.back_btn.TabIndex = 7
        Me.back_btn.Text = "秒戻す(&Q)"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(204, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(16, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "分"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(350, 45)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 24)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "秒進める(&W)"
        '
        'FwSec
        '
        Me.FwSec.Location = New System.Drawing.Point(299, 49)
        Me.FwSec.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.FwSec.Name = "FwSec"
        Me.FwSec.Size = New System.Drawing.Size(50, 19)
        Me.FwSec.TabIndex = 12
        Me.FwSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.FwSec.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'BackSec
        '
        Me.BackSec.Location = New System.Drawing.Point(160, 49)
        Me.BackSec.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.BackSec.Name = "BackSec"
        Me.BackSec.Size = New System.Drawing.Size(48, 19)
        Me.BackSec.TabIndex = 13
        Me.BackSec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.BackSec.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'PosMin
        '
        Me.PosMin.Location = New System.Drawing.Point(147, 16)
        Me.PosMin.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.PosMin.Name = "PosMin"
        Me.PosMin.Size = New System.Drawing.Size(57, 19)
        Me.PosMin.TabIndex = 14
        Me.PosMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Pos
        '
        Me.Pos.Location = New System.Drawing.Point(220, 16)
        Me.Pos.Name = "Pos"
        Me.Pos.Size = New System.Drawing.Size(40, 19)
        Me.Pos.TabIndex = 15
        Me.Pos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Btn_Rframe
        '
        Me.Btn_Rframe.Location = New System.Drawing.Point(62, 45)
        Me.Btn_Rframe.Name = "Btn_Rframe"
        Me.Btn_Rframe.Size = New System.Drawing.Size(32, 24)
        Me.Btn_Rframe.TabIndex = 16
        Me.Btn_Rframe.Text = "<|"
        '
        'Btn_Fframe
        '
        Me.Btn_Fframe.Location = New System.Drawing.Point(100, 45)
        Me.Btn_Fframe.Name = "Btn_Fframe"
        Me.Btn_Fframe.Size = New System.Drawing.Size(32, 24)
        Me.Btn_Fframe.TabIndex = 17
        Me.Btn_Fframe.Text = "|>"
        '
        'SS_Button
        '
        Me.SS_Button.Location = New System.Drawing.Point(362, 13)
        Me.SS_Button.Name = "SS_Button"
        Me.SS_Button.Size = New System.Drawing.Size(88, 23)
        Me.SS_Button.TabIndex = 18
        Me.SS_Button.Text = "画面写真(&C)"
        Me.SS_Button.UseVisualStyleBackColor = True
        '
        'Form3
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(456, 73)
        Me.Controls.Add(Me.SS_Button)
        Me.Controls.Add(Me.Btn_Fframe)
        Me.Controls.Add(Me.Btn_Rframe)
        Me.Controls.Add(Me.Pos)
        Me.Controls.Add(Me.PosMin)
        Me.Controls.Add(Me.BackSec)
        Me.Controls.Add(Me.FwSec)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.jump_btn)
        Me.Controls.Add(Me.back_btn)
        Me.Controls.Add(Me.play_btn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(472, 112)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(472, 112)
        Me.Name = "Form3"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "動画操作パネル"
        CType(Me.FwSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BackSec, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PosMin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub play_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles play_btn.Click
        Start_movie()
    End Sub

    '再生位置（分）フィールドに数字以外受け付けない。
    Private Sub PosMin_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        'リターンで実行
        If e.KeyChar = vbCr Then
            Jump_seconds(PosMin.Value * 60 + Pos.Value)
            main_frm.Memo.Focus()
        End If

        If e.KeyChar <> vbBack And (e.KeyChar < "0"c Or e.KeyChar > "9"c) Then
            e.Handled = True
        End If
    End Sub '再生位置（秒）フィールドに数字以外受け付けない。

    'Private Sub Pos_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    'リターンで実行
    '    If e.KeyChar = vbCr Then
    '        Jump_seconds(PosMin.Text * 60 + Pos.Text)
    '        main_frm.Memo.Focus()
    '    End If

    '    If e.KeyChar <> vbBack And (e.KeyChar < "0"c Or e.KeyChar > "9"c) Then
    '        e.Handled = True
    '    End If
    'End Sub

    '指定秒戻しフィールドに数字以外受け付けない。
    'Private Sub BackSec_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    'リターンで実行
    '    If e.KeyChar = vbCr Then
    '        Rew_seconds(BackSec.Text)
    '        main_frm.Memo.Focus()
    '    End If

    '    If e.KeyChar <> vbBack And (e.KeyChar < "0"c Or e.KeyChar > "9"c) Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub jump_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles jump_btn.Click
        Jump_seconds(PosMin.Value * 60 + Pos.Value)
        main_frm.Memo.Focus()
    End Sub

    Private Sub back_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles back_btn.Click
        Rew_seconds(BackSec.Value)
        main_frm.Memo.Focus()
    End Sub


    Private Sub Form3_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.MouseLeave
        main_frm.Memo.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Fw_seconds(FwSec.Value)
        main_frm.Memo.Focus()
    End Sub

    Private Sub Btn_Fframe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Fframe.Click
        If IsPlaying() Then
            If player_frm.Player1.playState = 2 Then
                Fw_seconds(0.1)
                player_frm.Player1.Ctlcontrols.play()
                player_frm.Player1.Ctlcontrols.pause()
            End If
        End If
        main_frm.Memo.Focus()
    End Sub

    Private Sub Btn_Rframe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Rframe.Click
        If IsPlaying() Then
            If player_frm.Player1.playState = 2 Then
                Rew_seconds(0.1)
                player_frm.Player1.Ctlcontrols.play()
                player_frm.Player1.Ctlcontrols.pause()
            End If
        End If
        main_frm.Memo.Focus()
    End Sub

    'Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    '    MsgBox(GrabFile)
    '    hBitmap = GrabFrame(player_frm.Player1.currentMedia.sourceURL, Int(player_frm.Player1.Ctlcontrols.currentPosition * 29.97))
    '    If hBitmap <> 0 Then
    '        Dim img As Image = Image.FromHbitmap(New IntPtr(hBitmap))
    '        Dim bmp As Bitmap = New Bitmap(img, img.Width, img.Height)
    '        Dim ss As String = GrabFile + ".png"
    '        MsgBox(ss)
    '        bmp.Save(ss, System.Drawing.Imaging.ImageFormat.Png)
    '        bmp.Dispose()
    '        img.Dispose()
    '        ReleaseGrabBuffer()
    '    End If
    'End Sub

    '画面写真ボタン
    Private Sub SS_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SS_Button.Click
        ScreenShot_Button()
    End Sub

    
   

End Class
