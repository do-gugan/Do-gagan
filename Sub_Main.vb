Module Common_functions
    Public main_frm As Form1 = Nothing
    Public player_frm As Form2 = Nothing
    Public control_frm As Form3 = Nothing
    Public option_frm As Form4 = Nothing
    Public cmt_dialog As Dialog1 = Nothing
    Public cmds(), cmd, memo(1) As String
    
    '���ݒ�֌W�̕ϐ�
    'Public offset As Integer = My.Settings.offset    '�^�C���R�[�h�Ǝ��ۂ̍Đ��ʒu�Ƃ̃I�t�Z�b�g
    'Public wheel_move As Integer = My.Settings.save_wheel '�z�C�[���P�m�b�`�ӂ�̃X�N���[���s��
    'Public auto_lockon As Boolean = My.Settings.auto_lockon  '���������͊J�n���Ƀ��b�N�I�����邩�ǂ���
    'Public save_win_pos As Boolean = My.Settings.save_pos '�E�B���h�E�̈ʒu�ƃT�C�Y��ۑ����邩�ǂ���

    '���W�X�g���ۑ��֌W�̕ϐ�
    Public frm_rect As Rectangle    '�E�C���h�E�̈ʒu

    ''GrabFrame.dll�֘A�̒�`
    'Declare Function GrabFrame Lib "GrabFrame.dll" (ByVal strFilename As String, ByVal nFrameIndex As Integer) As Int32
    'Declare Sub ReleaseGrabBuffer Lib "GrabFrame.dll" ()
    ''PNG�t�@�C���̕ۑ���
    'Public GrabFile As String = System.Environment.CurrentDirectory
    'Public hBitmap As Int32

    <STAThread()>
    Sub Main()
        '.NET 4.7 HiDPI�Ή�
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        '���C���p�l�����J��
        main_frm = New Form1
        main_frm.ShowDialog() '�\����A�t�H�[������Ȃ�
    End Sub


    ' �L�[�{�[�h�V���[�g�J�b�g�i�L�[�C�x���g�j�̏���
    Sub keyinput(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        ' ALT�L�[���ꏏ�����ꂽ�ꍇ�A�V���[�g�J�b�g���[�`�����Ăяo��
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
            '���^�[���L�[�������ꂽ�ꍇ�A�������̋L�������s
            If e.KeyCode = Keys.Return Then
                main_frm.insert_memo()
                e.Handled = True
            End If
        End If
        'e.Handled = True
        main_frm.Memo.Focus()
    End Sub

    ' ��O�h�~�ɁA���悪�Đ������`�F�b�N����֐��i�Đ����Ȃ�true��Ԃ��I���B����ȊO�Ȃ�false��Ԃ��A�_�C�A���O���o���B�j
    Function IsPlaying() As Boolean
        If player_frm Is Nothing Then
            'MsgBox("����t�@�C�����J����Ă��܂���B")
            Return False
        ElseIf player_frm.IsDisposed() Then
            'MsgBox("����t�@�C�����J����Ă��܂���")
            Return False
        Else
            Return True
        End If
    End Function

    '����̍Đ��ƈꎞ��~
    Public Sub Start_movie()
        If IsPlaying() Then
            If player_frm.Player1.playState = 3 Then
                player_frm.Player1.Ctlcontrols.pause()
            Else
                player_frm.Player1.Ctlcontrols.play()
            End If
        End If
    End Sub

    '�w��b���W�����v
    Public Sub Jump_seconds(ByVal sec As Integer)
        If IsPlaying() Then
            player_frm.Player1.Ctlcontrols.currentPosition = sec
        End If
    End Sub

    ' �w��b�������߂��{�^���̏���
    Public Sub Rew_seconds(ByVal sec As Double)
        If IsPlaying() Then
            player_frm.Player1.Ctlcontrols.currentPosition = player_frm.Player1.Ctlcontrols.currentPosition - sec
        End If
    End Sub

    ' �w��b���i�߂�{�^���̏���
    Public Sub Fw_seconds(ByVal sec As Double)
        If IsPlaying() Then
            player_frm.Player1.Ctlcontrols.currentPosition = player_frm.Player1.Ctlcontrols.currentPosition + sec
        End If
    End Sub

    Public Sub LockOn()
        '�G���[�_�C�A���O�\����ɖ������[�v�ɓ���Ȃ��悤�A�t�H�[�J�X���ړ����Ă���
        main_frm.Memo.Focus()
        If IsPlaying() Then
            ' �L�����̓I�t�Z�b�g�𔽉f�����Ȃ��悤�ɕύX�i1.1.2�j
            'main_frm.timecode.Text = sec_to_minsec(player_frm.Player1.Ctlcontrols.currentPosition() - My.Settings.offset)
            main_frm.timecode.Text = minsec_to_hourminsec(player_frm.Player1.Ctlcontrols.currentPositionString())
        End If
    End Sub

    '�t�@�C��������g���q�𔲂����������Ԃ�
    Function get_filename_body(ByVal filename As String) As String
        '�Ƃ肠�����g���q��3�����̏ꍇ�̂ݑz��
        get_filename_body = Mid$(filename, 1, Len(filename) - 4)
    End Function

    '�t�@�C��������g���q�𔲂��o��
    Function get_filename_extension(ByVal filename As String) As String
        get_filename_extension = Mid$(filename, Len(filename) - 2, Len(filename))
    End Function

    ''�B�e�{�^���̗��p�ې؂�ւ�
    'Function switch_grab(ByVal extension As String)
    '    'GrabFrame.dll�֘A�̒�`
    '    If System.IO.File.Exists("GrabFrame.dll") Then
    '        If extension = "avi" Or extension = "AVI" Then
    '            control_frm.Button2.Enabled = True
    '        End If
    '    End If

    'End Function

    ' �ݐϕb���\����hh:mm:ss�`���ɕϊ��i�덷���o��̂Ŏg�p���~�j
    Function sec_to_minsec(ByVal sec_src As Int16) As String
        Dim hour, min, sec As Int16
        hour = Math.Floor(sec_src / 3600)
        min = Math.Floor((sec_src - (hour * 3600)) / 60)
        sec = sec_src - (hour * 3600) - (min * 60)
        Return Format(hour, "00") & ":" & Format(min, "00") & ":" & Format(sec, "00")
    End Function

    ' �ݐϕ��b���\���ɕK�v�ɉ�����hh:��t���i���`�����O�p�j
    Function minsec_to_hourminsec(ByVal minsec As String) As String
        If Len(minsec) = 5 Then
            minsec = "00:" & minsec
        End If

        Return minsec
    End Function

    'hh:mm:ss�`����ݐϕb���\���ɕϊ��iWM�R���|�[�l���g�n���p�j
    Function minsec_to_sec(ByVal timecode As String) As Integer
        'MsgBox(timecode)
        Dim sec As Integer
        Dim col(2) As String
        col = Split(timecode, ":")
        sec = col(0) * 3600 + col(1) * 60 + col(2)
        Return sec
    End Function

    '��ʎʐ^�{�^���������ꂽ���̏����i����TC���Εb���ɕϊ����Ċ֐��Ăяo���j
    Sub ScreenShot_Button()
        ScreenShot(minsec_to_sec(minsec_to_hourminsec(player_frm.Player1.Ctlcontrols.currentPositionString())), True, "")
    End Sub

    '�w��b���̉�ʎʐ^��ۑ��iffmpeg���g�p�j
    Sub ScreenShot(ByVal tc As Integer, ByVal sound As Boolean, ByVal memo As String)
        '���ݒ�ŕۑ��p�X���ݒ肳��Ă��邩�m�F
        If My.Settings.CapturePath = "" Then
            MsgBox("���ݒ�ŐÎ~��ۑ����ݒ肵�ĉ������B")
        Else

            'ffmpeg�̑��݂��m�F
            If System.IO.File.Exists("ffmpeg.exe") Then
                '�Đ����̃t�@�C�������擾
                Dim src As String = player_frm.Player1.currentMedia.sourceURL
                '�v���Z�X�����s
                Dim Process1 As New Process
                Process1.StartInfo.FileName = "ffmpeg.exe"
                Dim arg As String '�R�}���h����
                If My.Settings.IncludeMemo = True Then
                    '�t�@�C�����Ƀ������e���܂߂�
                    arg = "-ss " & tc & " -vframes 1 -i " & Chr(34) & src & Chr(34) & " -f image2 " & My.Settings.CapturePath & "\" & player_frm.Player1.currentMedia.name & "_" & Format(tc, "00000") & "_" & memo & ".jpg"
                Else
                    '�t�@�C�����Ƀ������e���܂߂Ȃ�
                    arg = "-ss " & tc & " -vframes 1 -i " & Chr(34) & src & Chr(34) & " -f image2 " & My.Settings.CapturePath & "\" & player_frm.Player1.currentMedia.name & "_" & Format(tc, "00000") & ".jpg"
                End If
                'MsgBox(arg)
                Process1.StartInfo.Arguments = arg
                'DOS����\�������Ȃ�
                Process1.StartInfo.UseShellExecute = False
                Process1.StartInfo.CreateNoWindow = True

                '�C�x���g�n���h�����t�H�[�����쐬�����X���b�h�Ŏ��s�����悤�ɂ���
                'Process1.SynchronizingObject = 

                '�V���b�^�[����炷
                If sound = True Then
                    Dim strm As System.IO.Stream = My.Resources.shutter
                    Dim player As New System.Media.SoundPlayer(strm)
                    player.PlaySync()
                    '��n��
                    player.Dispose()
                End If

                '�v���Z�X���s
                Process1.Start()
                Process1.WaitForExit() '��������������

            Else
                MessageBox.Show("�C���X�g�[���t�H���_��ffmpeg.exe�����݂��܂���B")
            End If
        End If

    End Sub

End Module

