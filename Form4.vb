Public Class Form4
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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents OK_btn As System.Windows.Forms.Button
    Friend WithEvents Cancel_btn As System.Windows.Forms.Button
    Friend WithEvents Auto_Lockon_fld As System.Windows.Forms.CheckBox
    Friend WithEvents Save_Win_Pos_fld As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents wheel_move_setting As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CapturePath As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents IncludeMemo As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Offset_fld As System.Windows.Forms.NumericUpDown
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.OK_btn = New System.Windows.Forms.Button
        Me.Cancel_btn = New System.Windows.Forms.Button
        Me.Auto_Lockon_fld = New System.Windows.Forms.CheckBox
        Me.Save_Win_Pos_fld = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Offset_fld = New System.Windows.Forms.NumericUpDown
        Me.wheel_move_setting = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.CapturePath = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.IncludeMemo = New System.Windows.Forms.CheckBox
        Me.Label7 = New System.Windows.Forms.Label
        CType(Me.Offset_fld, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.wheel_move_setting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(72, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(192, 16)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "ジャンプ時、実際のタイムコードの(&O):"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(312, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 16)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "秒前から再生する"
        '
        'OK_btn
        '
        Me.OK_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OK_btn.Location = New System.Drawing.Point(416, 270)
        Me.OK_btn.Name = "OK_btn"
        Me.OK_btn.Size = New System.Drawing.Size(104, 24)
        Me.OK_btn.TabIndex = 15
        Me.OK_btn.Text = "保 存(&S)"
        '
        'Cancel_btn
        '
        Me.Cancel_btn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cancel_btn.Location = New System.Drawing.Point(296, 270)
        Me.Cancel_btn.Name = "Cancel_btn"
        Me.Cancel_btn.Size = New System.Drawing.Size(104, 24)
        Me.Cancel_btn.TabIndex = 16
        Me.Cancel_btn.Text = "キャンセル"
        '
        'Auto_Lockon_fld
        '
        Me.Auto_Lockon_fld.Enabled = False
        Me.Auto_Lockon_fld.Location = New System.Drawing.Point(72, 64)
        Me.Auto_Lockon_fld.Name = "Auto_Lockon_fld"
        Me.Auto_Lockon_fld.Size = New System.Drawing.Size(200, 24)
        Me.Auto_Lockon_fld.TabIndex = 18
        Me.Auto_Lockon_fld.Text = "メモ欄入力開始時に自動ロックオン"
        '
        'Save_Win_Pos_fld
        '
        Me.Save_Win_Pos_fld.Enabled = False
        Me.Save_Win_Pos_fld.Location = New System.Drawing.Point(72, 112)
        Me.Save_Win_Pos_fld.Name = "Save_Win_Pos_fld"
        Me.Save_Win_Pos_fld.Size = New System.Drawing.Size(144, 24)
        Me.Save_Win_Pos_fld.TabIndex = 19
        Me.Save_Win_Pos_fld.Text = "ウインドウの配置を記憶"
        '
        'Label1
        '
        Me.Label1.Enabled = False
        Me.Label1.Location = New System.Drawing.Point(120, 88)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(304, 24)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "（かな漢字変換中は検知されないので、あまり意味ないかも）"
        '
        'Offset_fld
        '
        Me.Offset_fld.Location = New System.Drawing.Point(256, 24)
        Me.Offset_fld.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.Offset_fld.Name = "Offset_fld"
        Me.Offset_fld.Size = New System.Drawing.Size(48, 19)
        Me.Offset_fld.TabIndex = 21
        Me.Offset_fld.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.Offset_fld.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'wheel_move_setting
        '
        Me.wheel_move_setting.Location = New System.Drawing.Point(160, 152)
        Me.wheel_move_setting.Name = "wheel_move_setting"
        Me.wheel_move_setting.Size = New System.Drawing.Size(36, 19)
        Me.wheel_move_setting.TabIndex = 22
        Me.wheel_move_setting.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.wheel_move_setting.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(70, 154)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 12)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "ホイール１ノッチで"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(202, 154)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 12)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "行スクロールする"
        '
        'CapturePath
        '
        Me.CapturePath.Location = New System.Drawing.Point(157, 190)
        Me.CapturePath.Name = "CapturePath"
        Me.CapturePath.Size = New System.Drawing.Size(259, 19)
        Me.CapturePath.TabIndex = 25
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(72, 193)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 12)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "静止画保存先:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(422, 188)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(60, 23)
        Me.Button1.TabIndex = 27
        Me.Button1.Text = "参照..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'IncludeMemo
        '
        Me.IncludeMemo.AutoSize = True
        Me.IncludeMemo.Checked = True
        Me.IncludeMemo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.IncludeMemo.Location = New System.Drawing.Point(157, 215)
        Me.IncludeMemo.Name = "IncludeMemo"
        Me.IncludeMemo.Size = New System.Drawing.Size(160, 16)
        Me.IncludeMemo.TabIndex = 28
        Me.IncludeMemo.Text = "ファイル名にメモ内容を含める"
        Me.IncludeMemo.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(168, 243)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(118, 12)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "(例: Movie1_00023.jpg)"
        '
        'Form4
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(536, 312)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.IncludeMemo)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.CapturePath)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.wheel_move_setting)
        Me.Controls.Add(Me.Offset_fld)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Save_Win_Pos_fld)
        Me.Controls.Add(Me.Auto_Lockon_fld)
        Me.Controls.Add(Me.Cancel_btn)
        Me.Controls.Add(Me.OK_btn)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label3)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(552, 350)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(552, 350)
        Me.Name = "Form4"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "環境設定"
        CType(Me.Offset_fld, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.wheel_move_setting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    ' 現在の値を各フィールドに反映する
    Private Sub Form4_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Offset_fld.Value = My.Settings.offset

        If My.Settings.auto_lockon = True Then
            Auto_Lockon_fld.Checked = True
        End If

        If My.Settings.save_pos = True Then
            Save_Win_Pos_fld.Checked = True
        End If

        If My.Settings.IncludeMemo = True Then
            IncludeMemo.Checked = True
            Label7.Text = "(例: Movie1_00023「ここわからないですね」.jpg)"
        Else
            IncludeMemo.Checked = False
            Label7.Text = "(例: Movie1_00023.jpg)"
        End If

        Offset_fld.Value = My.Settings.offset
        wheel_move_setting.Value = My.Settings.save_wheel
        CapturePath.Text = My.Settings.CapturePath
    End Sub


    'メモ欄を含めるチェックボックスが変更された時
    Private Sub IncludeMemo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IncludeMemo.CheckedChanged
        If IncludeMemo.Checked = True Then
            Label7.Text = "(例: Movie1_00023「ここわからないですね」.jpg)"
        Else
            Label7.Text = "(例: Movie1_00023.jpg)"
        End If

    End Sub

    ' 各フィールドの値をMy.settingsに反映、ファイルに保存して閉じる
    Sub save_changes()
        '設定をMy.Settingsに保存

        If Auto_Lockon_fld.Checked = True Then
            My.Settings.auto_lockon = True
        Else
            My.Settings.auto_lockon = False
        End If

        If Save_Win_Pos_fld.Checked = True Then
            My.Settings.save_pos = True
        Else
            My.Settings.save_pos = False
        End If

        If IncludeMemo.Checked = True Then
            My.Settings.IncludeMemo = True
        Else
            My.Settings.IncludeMemo = False
        End If

        My.Settings.save_wheel = wheel_move_setting.Value
        My.Settings.offset = Offset_fld.Value
        My.Settings.CapturePath = CapturePath.Text

        My.Settings.Save()
    End Sub

    ' 変更を保存しないで閉じる
    Private Sub Cancel_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_btn.Click
        Me.Close()
    End Sub

    ' 変更を保存して閉じる
    Private Sub OK_btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_btn.Click
        save_changes()
        Me.Close()
    End Sub


    ' 静止画保存フォルダ参照のダイアログ
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim fbd As New FolderBrowserDialog

        '上部に表示する説明テキストを指定する
        fbd.Description = "静止画を保存する場所を指定してください。"
        'ルートフォルダを指定する
        fbd.RootFolder = Environment.SpecialFolder.Desktop
        '最初に選択するフォルダを指定する
        'RootFolder以下にあるフォルダである必要がある
        If CapturePath.Text <> "" Then
            fbd.RootFolder = CapturePath.Text
        Else
            fbd.SelectedPath = "C:\"
        End If
        'ユーザーが新しいフォルダを作成できるようにする
        fbd.ShowNewFolderButton = True

        'ダイアログを表示する
        If fbd.ShowDialog(Me) = DialogResult.OK Then
            '選択されたフォルダを表示する
            CapturePath.Text = fbd.SelectedPath
        End If

    End Sub



End Class
