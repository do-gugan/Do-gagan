Public Class Form1
    Inherits System.Windows.Forms.Form

    'Form1内でアクセスできる変数の宣言
    '------------------------------------------------------------------------------------------------------------------------------------
    ' 変数の定義
    '------------------------------------------------------------------------------------------------------------------------------------

    Dim logfile, chapterfile As String           'ログファイルのパスとファイル名
    Dim dirty As Boolean = False    '最後に保存してからログが変更されたかどうかの真偽値（Trueなら更新有り）
    Dim white_line As Boolean = True    'ログ欄の最初の行が、例外防止の空行かどうか（Trueなら空行）
    Dim movfile, undobuffer As String
    Dim lastSearchWord As String
    Dim dpiScale As Double

    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem22 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem23 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem24 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem26 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem25 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem27 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem28 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem29 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem30 As MenuItem
    Friend WithEvents MenuItem31 As MenuItem
    Friend WithEvents MenuItem32 As MenuItem
    Friend WithEvents MenuItem33 As MenuItem
    '分析対象の動画ファイルのパスとファイル名
    Dim droped_file As String


    '------------------------------------------------------------------------------------------------------------------------------------
    ' Subプロシージャ、関数の定義
    '------------------------------------------------------------------------------------------------------------------------------------

    'フルパスからファイル名を抽出する関数
    Public Function GetFullPathToFileName(ByVal FullPas As String) As String
        Dim fname As String = ""
        Dim i As Integer
        For i = Len(FullPas) To 1 Step -1
            Select Case Mid$(FullPas, i, 1)
                Case "\", ":"
                    fname = Mid$(FullPas, i + 1)
                    Exit For
            End Select
        Next i
        Return fname
    End Function


    'ログ・フォームの中身を保存
    Sub SaveLog()
        If logfile = "" Then
            GetSaveFile()
        End If
        If logfile <> "" Then
            Dim i As Integer
            FileOpen(1, logfile, OpenMode.Output)
            For i = 0 To log_lb.Items.Count - 1
                Print(1, log_lb.Items(i) & vbCrLf)
            Next i
            FileClose()
            ' 未保存フラグをクリア
            dirty = False
            ' ウィンドウのタイトルをファイル名に変更
            Me.Text = logfile

            ''スクリーンショット保存先ディレクトリを設定
            'GrabFile = get_filename_body(logfile)
        End If
    End Sub

    '保存するログファイルを選択するダイアログを表示して取得
    Sub GetSaveFile()
        With SaveFileDialog1
            .Title = "名前をつけてログを保存"
            'デフォルトのファイル名は、動画ファイル名の拡張子を.txtに替えたもの
            If movfile <> "" Then
                .FileName = GetFullPathToFileName(movfile).Substring(0, GetFullPathToFileName(movfile).LastIndexOf(".")) & ".txt"
            Else
                .FileName = "新規ログファイル.txt"
                .FileName = player_frm.Text.Substring(0, player_frm.Text.LastIndexOf(".")) & ".txt"

            End If
            .Filter = "テキスト（タブ区切り）(*.txt)|*.txt|すべてのファイル(*.*)|*.*"
            .OverwritePrompt = True
            .RestoreDirectory = True
            If .ShowDialog = DialogResult.OK Then
                logfile = SaveFileDialog1.FileName
            End If
        End With
    End Sub

    '既存ファイル読み込み
    Sub LoadLog()
        With OpenFileDialog1
            .Title = "ログファイルを選択"
            .CheckFileExists = True
            .RestoreDirectory = True
            .Filter = "テキスト（タブ区切り）(*.txt)|*.txt|すべてのファイル(*.*)|*.*"
            .CheckFileExists = True
            If .ShowDialog = DialogResult.OK Then
                'ここでもう一度ログ欄をクリア（例外防止の空行を削除するため）
                'log_lb.Items.Clear()
                LoadLogFile(OpenFileDialog1.FileName)

                enable_savemenu()
            End If
        End With
    End Sub

    '指定されたファイル名で読み込みを実行
    Sub LoadLogFile(ByVal filename As String)
        'ウインドウタイトルを変更
        logfile = filename
        Me.Text = logfile

        ''スクリーンショット保存先ディレクトリを設定
        'GrabFile = get_filename_body(logfile)

        '読み込み中描画処理を中断し高速化
        log_lb.BeginUpdate()
        'ファイルから１行ずつ読み込んでログ欄に追加
        Dim file As New System.IO.StreamReader(filename, System.Text.Encoding.Default)
        Dim line As String = file.ReadLine
        Dim col(), rest As String
        While Not line Is Nothing
            col = Split(line, vbTab)

            'メモにタブが含まれている場合の対処（行全体からタイムコードを抜いたものを使用
            rest = line.Replace(col(0) & vbTab, "")

            'タイムコードのフォーマット変換
            If (InStr(col(0), ":") = 0) Then
                col(0) = sec_to_minsec(col(0))
            End If

            'ログの再合成と追加
            log_lb.Items.Add(col(0) & vbTab & rest)
            line = file.ReadLine
        End While
        file.Close()
        '描画処理を再開
        log_lb.EndUpdate()
        dirty = False
        white_line = False

    End Sub

    '新規ログ作成
    Sub new_document()
        If dirty = True Then
            Select Case MessageBox.Show("ログが保存されていません。保存しますか？", "未保存警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
                Case DialogResult.Yes
                    SaveLog()
                Case DialogResult.Cancel
                    Exit Sub
                Case DialogResult.No
                    main_frm.log_lb.Items.Clear()
                    'main_frm.log_lb.Items.Add(vbTab)
                    dirty = False
                    white_line = True
                    main_frm.Text = "新規ログファイル.txt"
            End Select
        Else
            main_frm.log_lb.Items.Clear()
            'main_frm.log_lb.Items.Add(vbTab)
            dirty = False
            white_line = True
            main_frm.Text = "新規ログファイル.txt"

        End If
    End Sub

    '渡された内容をログ欄に挿入
    Public Sub insert_memo()
        insert_memo_value(timecode.Text, Memo.Text)
        Memo.Text = ""
        timecode.Text = ""
    End Sub

    Sub insert_memo_value(ByVal tcode As String, ByVal memo_text As String)
        'アンドゥバッファ保存
        SaveUndoBuffer()
        If IsPlaying() Then
            If tcode = "now" Then
                tcode = sec_to_minsec(player_frm.Player1.Ctlcontrols.currentPosition() - My.Settings.offset)
            End If

            If tcode = "" Then
                timecode.Focus()
                tcode = timecode.Text
                '再生終了後にタイムコードが空欄になる件に対処
                If tcode = "" Then
                    tcode = "00:00:00"
                End If
            End If

            '例外防止の空行を消す
            If white_line = True Then
                log_lb.Items.Clear()
                white_line = False
            End If

            log_lb.Items.Add(tcode & vbTab & memo_text)

            '挿入した項目を選択
            Dim i As Integer
            For i = 0 To log_lb.Items.Count - 1
                If InStr(log_lb.Items(i), (tcode & vbTab & memo_text)) > 0 Then Exit For
            Next
            log_lb.ClearSelected()
            log_lb.SelectedIndex = i

            flag_dirty()
        End If

    End Sub
    Sub flag_dirty()
        '未保存フラグを立てる
        If dirty = False Then
            dirty = True
            Me.Text &= "*"
        End If
        enable_savemenu()

    End Sub

    Sub enable_savemenu()
        '保存系メニューを有効にする
        MenuItem3.Enabled = True
        MenuItem4.Enabled = True
        MenuItem27.Enabled = True

    End Sub


#Region " Windows フォーム デザイナで生成されたコード "

    Public Sub New()
        MyBase.New()

        ' この呼び出しは Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後に初期化を追加します。

    End Sub

    ' Form は dispose をオーバーライドしてコンポーネント一覧を消去します。
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
    ' Windows フォーム デザイナを使って変更してください。  
    ' コード エディタは使用しないでください。
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Memo As System.Windows.Forms.TextBox
    Friend WithEvents Done As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents timecode As System.Windows.Forms.TextBox
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem14 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem15 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem16 As System.Windows.Forms.MenuItem
    Friend WithEvents log_lb As System.Windows.Forms.ListBox
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents Bt_F5 As System.Windows.Forms.Button
    Friend WithEvents Bt_F4 As System.Windows.Forms.Button
    Friend WithEvents Bt_F3 As System.Windows.Forms.Button
    Friend WithEvents Bt_F2 As System.Windows.Forms.Button
    Friend WithEvents Bt_F1 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents MenuItem19 As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.MenuItem19 = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem27 = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem11 = New System.Windows.Forms.MenuItem()
        Me.MenuItem20 = New System.Windows.Forms.MenuItem()
        Me.MenuItem21 = New System.Windows.Forms.MenuItem()
        Me.MenuItem12 = New System.Windows.Forms.MenuItem()
        Me.MenuItem13 = New System.Windows.Forms.MenuItem()
        Me.MenuItem14 = New System.Windows.Forms.MenuItem()
        Me.MenuItem15 = New System.Windows.Forms.MenuItem()
        Me.MenuItem32 = New System.Windows.Forms.MenuItem()
        Me.MenuItem33 = New System.Windows.Forms.MenuItem()
        Me.MenuItem30 = New System.Windows.Forms.MenuItem()
        Me.MenuItem31 = New System.Windows.Forms.MenuItem()
        Me.MenuItem24 = New System.Windows.Forms.MenuItem()
        Me.MenuItem26 = New System.Windows.Forms.MenuItem()
        Me.MenuItem25 = New System.Windows.Forms.MenuItem()
        Me.MenuItem16 = New System.Windows.Forms.MenuItem()
        Me.MenuItem22 = New System.Windows.Forms.MenuItem()
        Me.MenuItem23 = New System.Windows.Forms.MenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Memo = New System.Windows.Forms.TextBox()
        Me.Done = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.timecode = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Bt_F5 = New System.Windows.Forms.Button()
        Me.Bt_F4 = New System.Windows.Forms.Button()
        Me.Bt_F3 = New System.Windows.Forms.Button()
        Me.Bt_F2 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Bt_F1 = New System.Windows.Forms.Button()
        Me.log_lb = New System.Windows.Forms.ListBox()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.MenuItem18 = New System.Windows.Forms.MenuItem()
        Me.MenuItem17 = New System.Windows.Forms.MenuItem()
        Me.MenuItem29 = New System.Windows.Forms.MenuItem()
        Me.MenuItem28 = New System.Windows.Forms.MenuItem()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem11, Me.MenuItem22})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem9, Me.MenuItem6, Me.MenuItem2, Me.MenuItem8, Me.MenuItem19, Me.MenuItem7, Me.MenuItem3, Me.MenuItem4, Me.MenuItem27, Me.MenuItem10, Me.MenuItem5})
        Me.MenuItem1.Text = "ファイル(&F)"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 0
        Me.MenuItem9.Shortcut = System.Windows.Forms.Shortcut.CtrlM
        Me.MenuItem9.Text = "動画ファイルを選択...(&M)"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 1
        Me.MenuItem6.Text = "-"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 2
        Me.MenuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.MenuItem2.Text = "新規ログ(&N)"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 3
        Me.MenuItem8.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.MenuItem8.Text = "ログを開く...(&F)"
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = 4
        Me.MenuItem19.Text = "ログを追加する..."
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 5
        Me.MenuItem7.Text = "-"
        '
        'MenuItem3
        '
        Me.MenuItem3.Enabled = False
        Me.MenuItem3.Index = 6
        Me.MenuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.MenuItem3.Text = "ログを上書き保存(&S)"
        '
        'MenuItem4
        '
        Me.MenuItem4.Enabled = False
        Me.MenuItem4.Index = 7
        Me.MenuItem4.Text = "名前を付けてログを保存...(&A)"
        '
        'MenuItem27
        '
        Me.MenuItem27.Enabled = False
        Me.MenuItem27.Index = 8
        Me.MenuItem27.Text = "chapters.txt形式でエクスポート..."
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 9
        Me.MenuItem10.Text = "-"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 10
        Me.MenuItem5.Text = "終了(&X)"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 1
        Me.MenuItem11.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem20, Me.MenuItem21, Me.MenuItem12, Me.MenuItem13, Me.MenuItem14, Me.MenuItem15, Me.MenuItem32, Me.MenuItem33, Me.MenuItem30, Me.MenuItem31, Me.MenuItem24, Me.MenuItem26, Me.MenuItem25, Me.MenuItem16})
        Me.MenuItem11.Text = "編集(&E)"
        '
        'MenuItem20
        '
        Me.MenuItem20.Enabled = False
        Me.MenuItem20.Index = 0
        Me.MenuItem20.Shortcut = System.Windows.Forms.Shortcut.CtrlZ
        Me.MenuItem20.Text = "元に戻す(&U)"
        '
        'MenuItem21
        '
        Me.MenuItem21.Index = 1
        Me.MenuItem21.Text = "-"
        '
        'MenuItem12
        '
        Me.MenuItem12.Enabled = False
        Me.MenuItem12.Index = 2
        Me.MenuItem12.Shortcut = System.Windows.Forms.Shortcut.CtrlX
        Me.MenuItem12.Text = "カット(&T)"
        '
        'MenuItem13
        '
        Me.MenuItem13.Enabled = False
        Me.MenuItem13.Index = 3
        Me.MenuItem13.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.MenuItem13.Text = "コピー(&C)"
        '
        'MenuItem14
        '
        Me.MenuItem14.Enabled = False
        Me.MenuItem14.Index = 4
        Me.MenuItem14.Shortcut = System.Windows.Forms.Shortcut.CtrlV
        Me.MenuItem14.Text = "ペースト(&P)"
        '
        'MenuItem15
        '
        Me.MenuItem15.Index = 5
        Me.MenuItem15.Text = "-"
        '
        'MenuItem32
        '
        Me.MenuItem32.Index = 6
        Me.MenuItem32.Shortcut = System.Windows.Forms.Shortcut.CtrlF
        Me.MenuItem32.Text = "検索(次)...(&F)"
        '
        'MenuItem33
        '
        Me.MenuItem33.Enabled = False
        Me.MenuItem33.Index = 7
        Me.MenuItem33.Shortcut = System.Windows.Forms.Shortcut.CtrlG
        Me.MenuItem33.Text = "次を検索"
        '
        'MenuItem30
        '
        Me.MenuItem30.Index = 8
        Me.MenuItem30.Text = "検索(全選択)..."
        '
        'MenuItem31
        '
        Me.MenuItem31.Index = 9
        Me.MenuItem31.Text = "-"
        '
        'MenuItem24
        '
        Me.MenuItem24.Index = 10
        Me.MenuItem24.Shortcut = System.Windows.Forms.Shortcut.CtrlR
        Me.MenuItem24.Text = "選択行を修正..."
        '
        'MenuItem26
        '
        Me.MenuItem26.Index = 11
        Me.MenuItem26.Shortcut = System.Windows.Forms.Shortcut.Del
        Me.MenuItem26.Text = "選択行を削除"
        '
        'MenuItem25
        '
        Me.MenuItem25.Index = 12
        Me.MenuItem25.Text = "-"
        '
        'MenuItem16
        '
        Me.MenuItem16.Index = 13
        Me.MenuItem16.Text = "環境設定...(&O)"
        '
        'MenuItem22
        '
        Me.MenuItem22.Index = 2
        Me.MenuItem22.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem23})
        Me.MenuItem22.Text = "ヘルプ(&H)"
        '
        'MenuItem23
        '
        Me.MenuItem23.Index = 0
        Me.MenuItem23.Text = "動画眼について"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.FileName = "doc1"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'Memo
        '
        Me.Memo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Memo.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.Memo.Location = New System.Drawing.Point(455, 19)
        Me.Memo.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Memo.Name = "Memo"
        Me.Memo.Size = New System.Drawing.Size(510, 28)
        Me.Memo.TabIndex = 0
        '
        'Done
        '
        Me.Done.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Done.Location = New System.Drawing.Point(983, 14)
        Me.Done.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Done.Name = "Done"
        Me.Done.Size = New System.Drawing.Size(103, 42)
        Me.Done.TabIndex = 1
        Me.Done.Text = "記録"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Memo)
        Me.Panel1.Controls.Add(Me.timecode)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.Bt_F5)
        Me.Panel1.Controls.Add(Me.Bt_F4)
        Me.Panel1.Controls.Add(Me.Bt_F3)
        Me.Panel1.Controls.Add(Me.Bt_F2)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Done)
        Me.Panel1.Controls.Add(Me.Bt_F1)
        Me.Panel1.Location = New System.Drawing.Point(0, 924)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1100, 112)
        Me.Panel1.TabIndex = 1
        '
        'timecode
        '
        Me.timecode.Location = New System.Drawing.Point(160, 19)
        Me.timecode.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.timecode.Name = "timecode"
        Me.timecode.Size = New System.Drawing.Size(85, 28)
        Me.timecode.TabIndex = 2
        Me.timecode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(257, 16)
        Me.Button1.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(103, 40)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "解除(&U)"
        '
        'Bt_F5
        '
        Me.Bt_F5.Location = New System.Drawing.Point(865, 70)
        Me.Bt_F5.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F5.Name = "Bt_F5"
        Me.Bt_F5.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F5.TabIndex = 8
        Me.Bt_F5.Text = "F5: タスク完了"
        '
        'Bt_F4
        '
        Me.Bt_F4.Location = New System.Drawing.Point(660, 70)
        Me.Bt_F4.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F4.Name = "Bt_F4"
        Me.Bt_F4.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F4.TabIndex = 7
        Me.Bt_F4.Text = "F4: 問題発生"
        '
        'Bt_F3
        '
        Me.Bt_F3.Location = New System.Drawing.Point(455, 70)
        Me.Bt_F3.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F3.Name = "Bt_F3"
        Me.Bt_F3.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F3.TabIndex = 6
        Me.Bt_F3.Text = "F3: 教示者発話"
        '
        'Bt_F2
        '
        Me.Bt_F2.Location = New System.Drawing.Point(249, 70)
        Me.Bt_F2.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F2.Name = "Bt_F2"
        Me.Bt_F2.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F2.TabIndex = 5
        Me.Bt_F2.Text = "F2: 被験者発話"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(372, 24)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 32)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "メモ(&M):"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(15, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "タイムコード(&L):"
        '
        'Bt_F1
        '
        Me.Bt_F1.Location = New System.Drawing.Point(44, 70)
        Me.Bt_F1.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F1.Name = "Bt_F1"
        Me.Bt_F1.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F1.TabIndex = 4
        Me.Bt_F1.Text = "F1: タスク開始"
        '
        'log_lb
        '
        Me.log_lb.AllowDrop = True
        Me.log_lb.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.log_lb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.log_lb.ContextMenu = Me.ContextMenu1
        Me.log_lb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.log_lb.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.log_lb.ItemHeight = 12
        Me.log_lb.Location = New System.Drawing.Point(0, 0)
        Me.log_lb.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.log_lb.Name = "log_lb"
        Me.log_lb.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.log_lb.Size = New System.Drawing.Size(1098, 902)
        Me.log_lb.Sorted = True
        Me.log_lb.TabIndex = 2
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem18, Me.MenuItem17, Me.MenuItem29, Me.MenuItem28})
        '
        'MenuItem18
        '
        Me.MenuItem18.Index = 0
        Me.MenuItem18.Text = "この行を修正"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 1
        Me.MenuItem17.Text = "この行を削除"
        '
        'MenuItem29
        '
        Me.MenuItem29.Index = 2
        Me.MenuItem29.Text = "この直前(1秒前)に空白行を挿入"
        '
        'MenuItem28
        '
        Me.MenuItem28.Index = 3
        Me.MenuItem28.Text = "ここの画面写真を撮る"
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1100, 1052)
        Me.Controls.Add(Me.log_lb)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(200, 100)
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Menu = Me.MainMenu1
        Me.MinimumSize = New System.Drawing.Size(1095, 1002)
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "新規ログファイル.txt"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        '自身がアクティブになった時に、動画ウインドウやコントロールウインドウもアクティブにする
        'player_frm.BringToFront()
        'control_frm.BringToFront()
        'Me.BringToFront()
        'Memo.Focus()
    End Sub





    '------------------------------------------------------------------------------------------------------------------------------------
    ' コントロール中のスクリプト
    '------------------------------------------------------------------------------------------------------------------------------------

    'メイン・ウインドウが開かれた時の処理
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '上書きアップデート時に設定を引き継ぐ
        If My.Settings.UpdateRequired Then
            ' After an update old user scoped settings are overwritten
            ' We add an updatedrequired property to settings 
            ' This will be true first time an application is run after update
            ' We then call My.Settings.Upgrade to copy the old user settings

            My.Settings.Upgrade()
            My.Settings.UpdateRequired = False
            My.Settings.Save()
        End If

        Me.Width = 600
        Me.Height = 500
        'ログ欄が空だとオーナードローで仕切線が描画されないので空行を入れとく
        'log_lb.Items.Add(vbTab)

        'コマンドライン引数を配列で取得する
        cmds = System.Environment.GetCommandLineArgs()
        'ファイルをドロップされて起動した場合の処理
        If cmds.Length > 1 Then
            main_frm.LoadMovie(cmds.GetValue(1))
        End If
    End Sub



    '再生ボタンで再生開始
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        control_frm.play_btn.PerformClick()
    End Sub

    ' 記録ボタン押下プロシージャの呼び出し
    Private Sub Done_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Done.Click
        insert_memo()
    End Sub



    'ショートカットキーの処理
    ' フォームのKeyPreview = trueで、まずフォームがキーイベントを拾う
    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        keyinput(sender, e)
    End Sub

    'メモ欄でキー入力があった場合の動作
    Private Sub Memo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Memo.KeyDown
        ' Altショートカットでビープを鳴らなくする
        If e.Alt = True Then
            e.SuppressKeyPress = True
        End If
    End Sub


    Private Sub memo_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Memo.KeyPress
        '自動ロックオン
        If My.Settings.auto_lockon = True And e.KeyChar <> vbBack And e.KeyChar <> vbCr And main_frm.Memo.Text = "" Then
            LockOn()
        End If

        'メモ・フィールドでEnterが押された時、Beepを鳴らさないようにする。
        If e.KeyChar = vbCr Then
            e.Handled = True
        End If
    End Sub



    'ファイル・メニューで「終了」を選んだ時の動作
    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        If dirty = True Then
            Select Case MessageBox.Show("ログが保存されていません。保存しますか？", "未保存警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
                Case DialogResult.Yes
                    SaveLog()
                Case DialogResult.Cancel
                    Exit Sub
                Case DialogResult.No
                    End
            End Select
        Else
            End
        End If
    End Sub

    'クローズボタンが押された時の処理
    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If dirty = True Then
            Select Case MessageBox.Show("ログが保存されていません。保存しますか？", "未保存警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
                Case DialogResult.Yes
                    SaveLog()
                Case DialogResult.Cancel
                    e.Cancel = True
                Case DialogResult.No
                    End
            End Select
        End If
    End Sub


    '名前を付けてログを保存
    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        GetSaveFile()
        If logfile <> "" Then
            SaveLog()
        End If
    End Sub


    ' ファイルメニューの「新規ログ」が選択された時の処理
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        new_document()
    End Sub

    ' ファイルメニューの「動画を開く」が選択された時の処理
    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        '未保存ログがある場合は保存確認をした上でログを抹消
        new_document()
        With OpenFileDialog1
            .Title = "分析する動画ファイルを選択"
            .CheckFileExists = True
            .RestoreDirectory = True
            .Filter = "動画ファイル(*.mp4;*.m4v;*.m2ts;*.asf;*.avi;*.mpg;*.wmv)|*.mp4;*.m4v;*.m2ts;*.asf;*.avi;*.mpg;*.wmv|すべてのファイル(*.*)|*.*"
            .CheckFileExists = True
            If .ShowDialog = DialogResult.OK Then
                LoadMovie(OpenFileDialog1.FileName)
            End If
        End With
    End Sub

    '動画を開く
    Sub LoadMovie(ByVal movfile As String)
        If player_frm Is Nothing Then
            player_frm = New Form2
        End If
        If player_frm.IsDisposed Then
            player_frm = New Form2
        End If
        'ファイル名を取り出し、動画ウインドウのタイトルバーに表示
        player_frm.Text = GetFullPathToFileName(movfile)
        player_frm.Show()
        If Me.Location.X - 640 * dpiScale > 0 Then
            frm_rect.X = Me.Location.X - 640 * dpiScale
        Else
            frm_rect.X = 0
        End If
        frm_rect.Y = Me.Location.Y
        frm_rect.Width = 640 * dpiScale
        frm_rect.Height = 550 * dpiScale
        player_frm.DesktopBounds = frm_rect

        ' 動画操作パネルを開く
        If control_frm Is Nothing Then
            control_frm = New Form3
        End If
        If control_frm.IsDisposed Then
            control_frm = New Form3
        End If
        control_frm.Show()
        frm_rect.X = player_frm.Location.X
        frm_rect.Y = player_frm.Location.Y + player_frm.Height
        control_frm.DesktopBounds = frm_rect
        'switch_grab(get_filename_extension(movfile)) '撮影ボタンの切り替え


        '同名のテキストファイルがあればログと判断し読み込む
        If System.IO.File.Exists(get_filename_body(movfile) & ".txt") Then
            LoadLogFile(get_filename_body(movfile) & ".txt")
            enable_savemenu()
        Else
            'ない場合は新規名称をつける
            Me.Text = "新規ログファイル.txt"
            logfile = ""
        End If

        player_frm.Player1.URL = movfile
        '再生が始まったらメモ欄にフォーカスを移動
        Memo.Focus()
    End Sub


    'ログ欄でダブルクリックされた行のタイムコードの位置に動画をジャンプ
    Private Sub log_lb_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles log_lb.DoubleClick

        'If log_lb.SelectedItem <> "" And IsPlaying() Then
        If log_lb.SelectedItem <> "" Then
            If player_frm.Player1.playState = 2 Or player_frm.Player1.playState = 1 Then
                player_frm.Player1.Ctlcontrols.play()
            End If
            player_frm.Player1.Ctlcontrols.currentPosition = minsec_to_sec(log_lb.SelectedItem.Substring(0, log_lb.SelectedItem.IndexOf(vbTab))) - My.Settings.offset
        End If
        Memo.Focus()
    End Sub


    'ログ欄でクリックされた行のタイムコードの位置に動画をジャンプ
    Private Sub log_lb_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles log_lb.SelectedIndexChanged

    End Sub



    ' ログ欄で右クリックした場合に、コンテクストメニューを表示する前にそこの項目を選択する
    Private Sub log_lb_mousedown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles log_lb.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim point As New Point(e.X, e.Y)
            log_lb.SelectedIndex = log_lb.IndexFromPoint(point)
        End If
    End Sub


    'ログ欄で無関係なところをクリックされたらフォーカスをメモ欄に戻す
    Private Sub log_lb_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles log_lb.MouseUp
        Memo.Focus()
    End Sub

    'タイムコード欄がフォーカスを得たら、現時点のタイムコードを挿入（ロックオン）
    Public Sub timecode_gotfocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timecode.GotFocus
        LockOn()
    End Sub

    '上書き保存が選択された時の動作
    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        SaveLog()
    End Sub

    'ログファイル読み込みが選択された時の動作
    Private Sub MenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem8.Click
        new_document()
        LoadLog()
    End Sub

    'メニューから「ログを追加する...」（既存データを消さない）
    Private Sub MenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem19.Click
        LoadLog()
    End Sub

    ' オーナードローでListBoxの外観を変更する
    Private Sub log_lb_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles log_lb.DrawItem
        ' Set the DrawMode property to draw fixed sized items.
        log_lb.DrawMode = DrawMode.OwnerDrawFixed
        ' Draw the background of the ListBox control for each item.
        e.DrawBackground()

        'デリミタ位置に縦線を描画する。
        Dim mypen As Pen
        mypen = Pens.Gray
        Dim lineX As Int16 = Int(45 * dpiScale)
        e.Graphics.DrawLine(mypen, lineX, 0, lineX, log_lb.Width)

        '選択行の背景色をかえる
        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            Dim bgColor As Brush = New SolidBrush(Color.LightSkyBlue)
            e.Graphics.FillRectangle(bgColor, New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
        End If


        ' 文字色をかえる
        Dim textColor As Brush
        textColor = Brushes.Black

        ' 例外が起きるので、アイテムが空の時は描画処理しない
        If log_lb.Items.Count > 0 Then
            e.Graphics.DrawString(log_lb.Items(e.Index), e.Font, textColor, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
        End If

        ' If the ListBox has focus, draw a focus rectangle around the selected item.
        e.DrawFocusRectangle()
    End Sub


    '環境設定ウインドウを開く
    Private Sub MenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem16.Click
        option_frm = New Form4
        option_frm.Show()
    End Sub


    ' ログ欄の選択された行を削除（コンテクストメニューから呼び出し）
    Private Sub MenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem17.Click
        If log_lb.SelectedIndex <> -1 Then
            SaveUndoBuffer()
            Dim i As Integer
            For i = 0 To log_lb.SelectedIndices.Count - 1
                log_lb.Items.RemoveAt(log_lb.SelectedIndices.Item(0))
                'リストボックスが空だとオーナードローで縦線が表示されないから空行を入れる
                'If log_lb.Items.Count = 0 Then
                'log_lb.Items.Add(vbTab)
                'End If
            Next
        Else
            MsgBox("対象となる行が選択されていません。")
        End If
    End Sub


    ' ログ欄の選択された行を修正（コンテクストメニューから呼び出し）
    Private Sub MenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem18.Click
        If log_lb.SelectedIndex <> -1 Then

            Dim rest, tmp(1), tc(2) As String
            tmp = log_lb.SelectedItem.split(vbTab)

            'メモにタブが含まれている場合の対処
            rest = log_lb.SelectedItem.ToString.Replace(tmp(0) & vbTab, "")

            tc = tmp(0).Split(":")
            cmt_dialog = New Dialog1
            cmt_dialog.tc_hour.Value = tc(0)
            cmt_dialog.tc_min.Value = tc(1)
            cmt_dialog.tc_sec.Value = tc(2)
            cmt_dialog.Cmt_Text.Text = rest
            cmt_dialog.ShowDialog()

        Else
            MsgBox("対象となる行が選択されていません。")
        End If
    End Sub

    'メニューから「この行を修正」を実行
    Private Sub MenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem24.Click
        MenuItem18.PerformClick()
    End Sub


    'メニューから「この行を削除」を実行
    Private Sub MenuItem26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem26.Click
        MenuItem17.PerformClick()
    End Sub


    Sub change_memo(ByVal memo As String)
        SaveUndoBuffer()
        log_lb.Items.Item(log_lb.SelectedIndex) = memo(0) & vbTab & memo
        flag_dirty()

    End Sub


    Private Sub Memo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Memo.KeyUp
        'ファンクション F1～F5にショートカット動作
        Select Case e.KeyCode
            Case 112
                f1_keypress()
            Case 113
                f2_keypress()
            Case 114
                f3_keypress()
            Case 115
                f4_keypress()
            Case 116
                f5_keypress()
        End Select

    End Sub

    Sub f1_keypress()
        insert_memo_value("now", "↓↓↓↓↓　タスク開始　↓↓↓↓↓")
    End Sub

    Sub f2_keypress()
        If Memo.Text = "" Then
            LockOn()
            Memo.Text = "被験者「」"
            Memo.SelectionStart = 4
            Memo.Focus()
        Else
            Memo.Text = "被験者「" & Memo.Text & "」"
            Memo.SelectionStart = Memo.TextLength - 1
            Memo.Focus()
        End If
    End Sub
    Sub f3_keypress()
        If Memo.Text = "" Then
            LockOn()
            Memo.Text = "教示者「」"
            Memo.SelectionStart = 4
            Memo.Focus()
        Else
            Memo.Text = "教示者「" & Memo.Text & "」"
            Memo.SelectionStart = Memo.TextLength - 1
            Memo.Focus()
        End If
    End Sub
    Sub f4_keypress()
        If Memo.Text = "" Then
            LockOn()
            Memo.Text = "問題発生："
            Memo.SelectionStart = 5
            Memo.Focus()
        Else
            Memo.Text = "問題発生：" & Memo.Text
            Memo.SelectionStart = Memo.TextLength
            Memo.Focus()
        End If
    End Sub
    Sub f5_keypress()
        insert_memo_value("now", "↑↑↑↑↑　タスク完了　↑↑↑↑↑")
    End Sub


    Private Sub Bt_F1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bt_F1.Click
        f1_keypress()
    End Sub

    Private Sub Bt_F2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bt_F2.Click
        f2_keypress()
    End Sub

    Private Sub Bt_F3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bt_F3.Click
        f3_keypress()
    End Sub

    Private Sub Bt_F4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bt_F4.Click
        f4_keypress()
    End Sub

    Private Sub Bt_F5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bt_F5.Click
        f5_keypress()
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        timecode.Text = ""
    End Sub

    ''ログ欄にファイルをドロップされたらアペンド
    'Private Sub log_lb_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles log_lb.DragDrop
    '    MsgBox("hoge")
    '    If e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop) Then
    '        e.Effect = DragDropEffects.Copy
    '    Else
    '        e.Effect = DragDropEffects.Copy
    '    End If
    'End Sub



    Private Sub Memo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Memo.TextChanged
        If My.Settings.auto_lockon = True And timecode.Text = "" Then
            LockOn()
        End If
    End Sub

    'ログ・フォームの中身をアンドゥ用バッファに保存
    Sub SaveUndoBuffer()
        Dim i As Integer
        undobuffer = ""
        For i = 0 To log_lb.Items.Count - 1
            undobuffer = undobuffer & log_lb.Items(i) & vbCrLf
        Next i
        'メニュー項目を有効に
        MenuItem20.Enabled = True
    End Sub

    'ログ・フォームの中身をアンドゥ用バッファから復元
    Sub LoadUndoBuffer()
        '実行前のスクロール位置を保存
        Dim topindex As Integer = log_lb.TopIndex
        Dim selectedindex As Integer = log_lb.SelectedIndex

        '読み込み中描画処理を中断し高速化
        log_lb.BeginUpdate()
        'ログ欄を空に
        log_lb.Items.Clear()

        'アンドゥバッファから１行ずつ読み込んでログ欄に追加
        Dim i As Integer
        Dim line() As String = Split(undobuffer, vbCrLf)
        For i = 0 To line.Length - 2
            log_lb.Items.Add(line(i))
        Next

        log_lb.TopIndex = topindex
        log_lb.SelectedIndex = selectedindex

        '描画処理を再開
        log_lb.EndUpdate()

        'メニュー項目を無効に
        MenuItem20.Enabled = False
    End Sub

    Private Sub log_lb_ValueMemberChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles log_lb.ValueMemberChanged
        SaveUndoBuffer()
    End Sub

    ' メニューから「元に戻す」が選択されてたら実行
    Private Sub MenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem20.Click
        LoadUndoBuffer()
    End Sub

    'ログ欄上でマウスホイールスクロールを実装
    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If e.Delta > 0 Then
            log_lb.TopIndex = log_lb.TopIndex - My.Settings.save_wheel
        Else
            log_lb.TopIndex = log_lb.TopIndex + My.Settings.save_wheel
        End If
    End Sub



    Private Sub MenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem23.Click
        AboutBox1.Show()
    End Sub



    '################## chapters.txt関連 #############################
    'ログ・フォームの中身を保存
    Sub SaveLog_chapters()
        '上書きの現象が出ているので、毎回ファイル名問い合わせにしてみる。
        'If chapterfile = "" Then
        GetSaveFile_chapters()
        'End If

        Dim line() As String
        Dim body As String = ""

        If chapterfile <> "" Then
            Dim i As Integer
            For i = 0 To log_lb.Items.Count - 1
                line = log_lb.Items(i).ToString.Split(vbTab)
                body = body & line(0) & ".000 " & line(1) & vbCrLf
            Next i


            'ファイル書き出し実行
            Dim sw As New System.IO.StreamWriter(chapterfile, False, System.Text.Encoding.UTF8)
            'bodyの内容をすべて書き込む
            sw.Write(body)
            '閉じる
            sw.Close()

        End If
    End Sub

    '保存するログファイルを選択するダイアログを表示して取得
    Sub GetSaveFile_chapters()
        With SaveFileDialog1
            .Title = "名前をつけてログを保存"
            'デフォルトのファイル名は、動画ファイル名の拡張子を.txtに替えたもの
            If movfile <> "" Then
                .FileName = GetFullPathToFileName(movfile).Substring(0, GetFullPathToFileName(movfile).LastIndexOf(".")) & ".chapters.txt"
            ElseIf logfile <> "" Then
                .FileName = logfile.Replace(".txt", ".chapters.txt")
            Else
                .FileName = "新規チャプターファイル.chapters.txt"
            End If
            .Filter = "chapters.txt形式テキスト(*.chapters.txt)|*.chapters.txt"
            .OverwritePrompt = True
            .RestoreDirectory = True
            If .ShowDialog = DialogResult.OK Then
                chapterfile = SaveFileDialog1.FileName
            End If
        End With
    End Sub



    Private Sub MenuItem27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem27.Click
        SaveLog_chapters()
    End Sub

    '検索...
    Private Sub MenuItem32_Click(sender As Object, e As EventArgs) Handles MenuItem32.Click
        Dim inputText As String
        inputText = InputBox("検索ワードを入れてください。現在の最初の選択行から順方向に最も近い行が選択されます。", "検索(次)")
        log_lb.ClearSelected()

        '次検索を有効にする
        lastSearchWord = inputText
        MenuItem33.Enabled = True

        Call lbSearchNext(inputText) 'マッチした行を選択
    End Sub

    '次を検索
    Private Sub MenuItem33_Click(sender As Object, e As EventArgs) Handles MenuItem33.Click
        Dim LastIndex As Integer = log_lb.SelectedIndex
        log_lb.ClearSelected()
        If log_lb.SelectedIndex >= log_lb.Items.Count - 2 Then
            '最後の行が選択されていたら最初に戻る
            log_lb.SetSelected(0, True)
        Else
            '次の行から始める
            log_lb.SetSelected(LastIndex + 1, True)
        End If
        Call lbSearchNext(lastSearchWord) '最後に使った検索語で再検索

    End Sub



    '検索(全選択)
    Private Sub MenuItem30_Click(sender As Object, e As EventArgs) Handles MenuItem30.Click
        Dim inputText As String
        inputText = InputBox("検索ワードを入れてください。そのワードを含む行が全て選択されます。", "検索(全選択)")
        log_lb.ClearSelected()
        Call lbSearchSelectAll(inputText) 'マッチした行を選択
        If log_lb.SelectedIndices.Count > 1 Then
            MessageBox.Show(log_lb.SelectedIndices.Count & " 箇所見つかりました")
        Else
            MessageBox.Show("見付かりませんでした")
            log_lb.ClearSelected()
        End If

    End Sub


    '右クリックメニューから静止画保存
    Private Sub MenuItem28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem28.Click
        If log_lb.SelectedIndex <> -1 Then
            Dim i As Integer
            For i = 0 To log_lb.SelectedIndices.Count - 1
                Dim tmp(1) As String
                tmp = log_lb.SelectedItems(i).Split(vbTab)
                ScreenShot(minsec_to_sec(minsec_to_hourminsec(tmp(0))), True, tmp(1))
            Next
        Else
            MsgBox("対象となる行が選択されていません。")
        End If
    End Sub



    '直前に空白行を挿入
    Private Sub MenuItem29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem29.Click
        If log_lb.SelectedIndex <> -1 Then

            Dim tmp(1), tc As String
            tmp = log_lb.SelectedItem.split(vbTab)
            tc = sec_to_minsec(minsec_to_sec(tmp(0)) - 1)

            SaveUndoBuffer()
            log_lb.Items.Add(tc & vbTab)
            flag_dirty()

        Else
            MsgBox("対象となる行が選択されていません。")
        End If
    End Sub

    '画面再描画で呼ばれる
    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        'ディスプレイスケール値を更新
        Dim g As Graphics = e.Graphics
        Console.WriteLine(g.DpiX)
        dpiScale = g.DpiX / 96.0F
        If 12 * dpiScale < 256 Then
            log_lb.ItemHeight = Int(12 * dpiScale) '12はデフォルトのItemHeight
        End If

    End Sub


    '　検索（下方）
    Private Sub lbSearchNext(ByVal SearchStr As String)
        Console.WriteLine("検索:" + SearchStr + " 選択数:" + log_lb.SelectedIndices.Count.ToString)
        If log_lb.SelectedIndices.Count = 0 Then
            Console.WriteLine("1行も選択されてない場合は先頭行")
            log_lb.SetSelected(0, True)
        ElseIf log_lb.SelectedIndices.Count > 1 Then
            Console.WriteLine(" 複数選択されていた時は最初の項目を使う")
            Dim FirstIndex As Integer = log_lb.SelectedIndices(0)
            log_lb.ClearSelected()
            log_lb.SetSelected(FirstIndex, True)
        End If
        Console.WriteLine("Start:" + log_lb.SelectedIndex.ToString + " End:" + log_lb.Items.Count.ToString)
        For i As Integer = log_lb.SelectedIndex To log_lb.Items.Count - 1
            log_lb.ClearSelected()
            log_lb.SetSelected(i, True)
            Console.Write(log_lb.Text)
            If Split(log_lb.Text, vbTab)(1).IndexOf(SearchStr) = -1 Then
                Console.Write("(Not Match)" + vbCr)
            Else
                Console.Write("(Match)" + vbCr)
                Exit Sub
            End If
        Next
        MessageBox.Show("見付かりませんでした")
        log_lb.ClearSelected()

    End Sub

    '　検索（全選択する）
    Private Sub lbSearchSelectAll(ByVal SearchStr As String)
        Dim Index As Integer = 0
        Dim Matched As New ArrayList
        For Each line As String In log_lb.Items
            If Split(line, vbTab)(1).IndexOf(SearchStr) > -1 Then
                Matched.Add(Index)
            End If
            Index = Index + 1
        Next
        For Each MatchedIndex As Integer In Matched
            log_lb.SetSelected(MatchedIndex, True)
        Next
    End Sub


End Class