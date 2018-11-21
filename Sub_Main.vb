Module Common_functions
    Public main_frm As Form1 = Nothing
    Public player_frm As Form2 = Nothing
    Public control_frm As Form3 = Nothing
    Public option_frm As Form4 = Nothing
    Public cmt_dialog As Dialog1 = Nothing
    Public cmds(), cmd, memo(1) As String
    
    '環境設定関係の変数
    'Public offset As Integer = My.Settings.offset    'タイムコードと実際の再生位置とのオフセット
    'Public wheel_move As Integer = My.Settings.save_wheel 'ホイール１ノッチ辺りのスクロール行数
    'Public auto_lockon As Boolean = My.Settings.auto_lockon  'メモ欄入力開始時にロックオンするかどうか
    'Public save_win_pos As Boolean = My.Settings.save_pos 'ウィンドウの位置とサイズを保存するかどうか

    'レジストリ保存関係の変数
    Public frm_rect As Rectangle    'ウインドウの位置

    ''GrabFrame.dll関連の定義
    'Declare Function GrabFrame Lib "GrabFrame.dll" (ByVal strFilename As String, ByVal nFrameIndex As Integer) As Int32
    'Declare Sub ReleaseGrabBuffer Lib "GrabFrame.dll" ()
    ''PNGファイルの保存先
    'Public GrabFile As String = System.Environment.CurrentDirectory
    'Public hBitmap As Int32

    <STAThread()>
    Sub Main()
        '.NET 4.7 HiDPI対応
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        'メインパネルを開く
        main_frm = New Form1
        main_frm.ShowDialog() '表示後、フォームを閉じない
    End Sub


    ' キーボードショートカット（キーイベント）の処理
    Sub keyinput(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        ' ALTキーを一緒押された場合、ショートカットルーチンを呼び出し
        If e.Alt = True Then
            Select Case e.KeyCode
                Case Keys.S
                    Start_movie()
                Case Keys.L
                    LockOn()
                Case Keys.J
                    Jump_seconds(control_frm.PosMin.Text * 60 + control_frm.Pos.Text)
                Case Keys.Q
                    Rew_seconds(control_frm.BackSec.Text)
                Case Keys.W
                    Fw_seconds(control_frm.FwSec.Text)
                Case Keys.C
                    ScreenShot_Button()
                Case Keys.D1
                    Rew_seconds(1)
                Case Keys.D2
                    Rew_seconds(2)
                Case Keys.D3
                    Rew_seconds(3)
                Case Keys.D4
                    Rew_seconds(4)
                Case Keys.D5
                    Rew_seconds(5)
            End Select
        Else
            'リターンキーが押された場合、メモ欄の記入を実行
            If e.KeyCode = Keys.Return Then
                main_frm.insert_memo()
                e.Handled = True
            End If
        End If
        'e.Handled = True
        main_frm.Memo.Focus()
    End Sub

    ' 例外防止に、動画が再生中かチェックする関数（再生中ならtrueを返し終了。それ以外ならfalseを返し、ダイアログを出す。）
    Function IsPlaying() As Boolean
        If player_frm Is Nothing Then
            'MsgBox("動画ファイルが開かれていません。")
            Return False
        ElseIf player_frm.IsDisposed() Then
            'MsgBox("動画ファイルが開かれていません")
            Return False
        Else
            Return True
        End If
    End Function

    '動画の再生と一時停止
    Public Sub Start_movie()
        If IsPlaying() Then
            If player_frm.Player1.playState = 3 Then
                player_frm.Player1.Ctlcontrols.pause()
            Else
                player_frm.Player1.Ctlcontrols.play()
            End If
        End If
    End Sub

    '指定秒数ジャンプ
    Public Sub Jump_seconds(ByVal sec As Integer)
        If IsPlaying() Then
            player_frm.Player1.Ctlcontrols.currentPosition = sec
        End If
    End Sub

    ' 指定秒数巻き戻すボタンの処理
    Public Sub Rew_seconds(ByVal sec As Double)
        If IsPlaying() Then
            player_frm.Player1.Ctlcontrols.currentPosition = player_frm.Player1.Ctlcontrols.currentPosition - sec
        End If
    End Sub

    ' 指定秒数進めるボタンの処理
    Public Sub Fw_seconds(ByVal sec As Double)
        If IsPlaying() Then
            player_frm.Player1.Ctlcontrols.currentPosition = player_frm.Player1.Ctlcontrols.currentPosition + sec
        End If
    End Sub

    Public Sub LockOn()
        'エラーダイアログ表示後に無限ループに入らないよう、フォーカスを移動しておく
        main_frm.Memo.Focus()
        If IsPlaying() Then
            ' 記入時はオフセットを反映させないように変更（1.1.2）
            'main_frm.timecode.Text = sec_to_minsec(player_frm.Player1.Ctlcontrols.currentPosition() - My.Settings.offset)
            main_frm.timecode.Text = minsec_to_hourminsec(player_frm.Player1.Ctlcontrols.currentPositionString())
        End If
    End Sub

    'ファイル名から拡張子を抜いた文字列を返す
    Function get_filename_body(ByVal filename As String) As String
        'とりあえず拡張子が3文字の場合のみ想定
        get_filename_body = Mid$(filename, 1, Len(filename) - 4)
    End Function

    'ファイル名から拡張子を抜き出す
    Function get_filename_extension(ByVal filename As String) As String
        get_filename_extension = Mid$(filename, Len(filename) - 2, Len(filename))
    End Function

    ''撮影ボタンの利用可否切り替え
    'Function switch_grab(ByVal extension As String)
    '    'GrabFrame.dll関連の定義
    '    If System.IO.File.Exists("GrabFrame.dll") Then
    '        If extension = "avi" Or extension = "AVI" Then
    '            control_frm.Button2.Enabled = True
    '        End If
    '    End If

    'End Function

    ' 累積秒数表示をhh:mm:ss形式に変換（誤差が出るので使用中止）
    Function sec_to_minsec(ByVal sec_src As Int16) As String
        Dim hour, min, sec As Int16
        hour = Math.Floor(sec_src / 3600)
        min = Math.Floor((sec_src - (hour * 3600)) / 60)
        sec = sec_src - (hour * 3600) - (min * 60)
        Return Format(hour, "00") & ":" & Format(min, "00") & ":" & Format(sec, "00")
    End Function

    ' 累積分秒数表示に必要に応じてhh:を付加（旧形式ログ用）
    Function minsec_to_hourminsec(ByVal minsec As String) As String
        If Len(minsec) = 5 Then
            minsec = "00:" & minsec
        End If

        Return minsec
    End Function

    'hh:mm:ss形式を累積秒数表示に変換（WMコンポーネント渡し用）
    Function minsec_to_sec(ByVal timecode As String) As Integer
        'MsgBox(timecode)
        Dim sec As Integer
        Dim col(2) As String
        col = Split(timecode, ":")
        sec = col(0) * 3600 + col(1) * 60 + col(2)
        Return sec
    End Function

    '画面写真ボタンが押された時の処理（現在TCを絶対秒数に変換して関数呼び出し）
    Sub ScreenShot_Button()
        ScreenShot(minsec_to_sec(minsec_to_hourminsec(player_frm.Player1.Ctlcontrols.currentPositionString())), True, "")
    End Sub

    '指定秒数の画面写真を保存（ffmpegを使用）
    Sub ScreenShot(ByVal tc As Integer, ByVal sound As Boolean, ByVal memo As String)
        '環境設定で保存パスが設定されているか確認
        If My.Settings.CapturePath = "" Then
            MsgBox("環境設定で静止画保存先を設定して下さい。")
        Else

            'ffmpegの存在を確認
            If System.IO.File.Exists("ffmpeg.exe") Then
                '再生中のファイル名を取得
                Dim src As String = player_frm.Player1.currentMedia.sourceURL
                'プロセスを実行
                Dim Process1 As New Process
                Process1.StartInfo.FileName = "ffmpeg.exe"
                Dim arg As String 'コマンド引数
                If My.Settings.IncludeMemo = True Then
                    'ファイル名にメモ内容を含める
                    arg = "-ss " & tc & " -vframes 1 -i " & Chr(34) & src & Chr(34) & " -f image2 " & My.Settings.CapturePath & "\" & player_frm.Player1.currentMedia.name & "_" & Format(tc, "00000") & "_" & memo & ".jpg"
                Else
                    'ファイル名にメモ内容を含めない
                    arg = "-ss " & tc & " -vframes 1 -i " & Chr(34) & src & Chr(34) & " -f image2 " & My.Settings.CapturePath & "\" & player_frm.Player1.currentMedia.name & "_" & Format(tc, "00000") & ".jpg"
                End If
                'MsgBox(arg)
                Process1.StartInfo.Arguments = arg
                'DOS窓を表示させない
                Process1.StartInfo.UseShellExecute = False
                Process1.StartInfo.CreateNoWindow = True

                'イベントハンドラがフォームを作成したスレッドで実行されるようにする
                'Process1.SynchronizingObject = 

                'シャッター音を鳴らす
                If sound = True Then
                    Dim strm As System.IO.Stream = My.Resources.shutter
                    Dim player As New System.Media.SoundPlayer(strm)
                    player.PlaySync()
                    '後始末
                    player.Dispose()
                End If

                'プロセス実行
                Process1.Start()
                Process1.WaitForExit() '逐次処理をする

            Else
                MessageBox.Show("インストールフォルダにffmpeg.exeが存在しません。")
            End If
        End If

    End Sub

End Module

