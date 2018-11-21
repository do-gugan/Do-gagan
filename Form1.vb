Public Class Form1
    Inherits System.Windows.Forms.Form

    'Form1���ŃA�N�Z�X�ł���ϐ��̐錾
    '------------------------------------------------------------------------------------------------------------------------------------
    ' �ϐ��̒�`
    '------------------------------------------------------------------------------------------------------------------------------------

    Dim logfile, chapterfile As String           '���O�t�@�C���̃p�X�ƃt�@�C����
    Dim dirty As Boolean = False    '�Ō�ɕۑ����Ă��烍�O���ύX���ꂽ���ǂ����̐^�U�l�iTrue�Ȃ�X�V�L��j
    Dim white_line As Boolean = True    '���O���̍ŏ��̍s���A��O�h�~�̋�s���ǂ����iTrue�Ȃ��s�j
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
    '���͑Ώۂ̓���t�@�C���̃p�X�ƃt�@�C����
    Dim droped_file As String


    '------------------------------------------------------------------------------------------------------------------------------------
    ' Sub�v���V�[�W���A�֐��̒�`
    '------------------------------------------------------------------------------------------------------------------------------------

    '�t���p�X����t�@�C�����𒊏o����֐�
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


    '���O�E�t�H�[���̒��g��ۑ�
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
            ' ���ۑ��t���O���N���A
            dirty = False
            ' �E�B���h�E�̃^�C�g�����t�@�C�����ɕύX
            Me.Text = logfile

            ''�X�N���[���V���b�g�ۑ���f�B���N�g����ݒ�
            'GrabFile = get_filename_body(logfile)
        End If
    End Sub

    '�ۑ����郍�O�t�@�C����I������_�C�A���O��\�����Ď擾
    Sub GetSaveFile()
        With SaveFileDialog1
            .Title = "���O�����ă��O��ۑ�"
            '�f�t�H���g�̃t�@�C�����́A����t�@�C�����̊g���q��.txt�ɑւ�������
            If movfile <> "" Then
                .FileName = GetFullPathToFileName(movfile).Substring(0, GetFullPathToFileName(movfile).LastIndexOf(".")) & ".txt"
            Else
                .FileName = "�V�K���O�t�@�C��.txt"
                .FileName = player_frm.Text.Substring(0, player_frm.Text.LastIndexOf(".")) & ".txt"

            End If
            .Filter = "�e�L�X�g�i�^�u��؂�j(*.txt)|*.txt|���ׂẴt�@�C��(*.*)|*.*"
            .OverwritePrompt = True
            .RestoreDirectory = True
            If .ShowDialog = DialogResult.OK Then
                logfile = SaveFileDialog1.FileName
            End If
        End With
    End Sub

    '�����t�@�C���ǂݍ���
    Sub LoadLog()
        With OpenFileDialog1
            .Title = "���O�t�@�C����I��"
            .CheckFileExists = True
            .RestoreDirectory = True
            .Filter = "�e�L�X�g�i�^�u��؂�j(*.txt)|*.txt|���ׂẴt�@�C��(*.*)|*.*"
            .CheckFileExists = True
            If .ShowDialog = DialogResult.OK Then
                '�����ł�����x���O�����N���A�i��O�h�~�̋�s���폜���邽�߁j
                'log_lb.Items.Clear()
                LoadLogFile(OpenFileDialog1.FileName)

                enable_savemenu()
            End If
        End With
    End Sub

    '�w�肳�ꂽ�t�@�C�����œǂݍ��݂����s
    Sub LoadLogFile(ByVal filename As String)
        '�E�C���h�E�^�C�g����ύX
        logfile = filename
        Me.Text = logfile

        ''�X�N���[���V���b�g�ۑ���f�B���N�g����ݒ�
        'GrabFile = get_filename_body(logfile)

        '�ǂݍ��ݒ��`�揈���𒆒f��������
        log_lb.BeginUpdate()
        '�t�@�C������P�s���ǂݍ���Ń��O���ɒǉ�
        Dim file As New System.IO.StreamReader(filename, System.Text.Encoding.Default)
        Dim line As String = file.ReadLine
        Dim col(), rest As String
        While Not line Is Nothing
            col = Split(line, vbTab)

            '�����Ƀ^�u���܂܂�Ă���ꍇ�̑Ώ��i�s�S�̂���^�C���R�[�h�𔲂������̂��g�p
            rest = line.Replace(col(0) & vbTab, "")

            '�^�C���R�[�h�̃t�H�[�}�b�g�ϊ�
            If (InStr(col(0), ":") = 0) Then
                col(0) = sec_to_minsec(col(0))
            End If

            '���O�̍č����ƒǉ�
            log_lb.Items.Add(col(0) & vbTab & rest)
            line = file.ReadLine
        End While
        file.Close()
        '�`�揈�����ĊJ
        log_lb.EndUpdate()
        dirty = False
        white_line = False

    End Sub

    '�V�K���O�쐬
    Sub new_document()
        If dirty = True Then
            Select Case MessageBox.Show("���O���ۑ�����Ă��܂���B�ۑ����܂����H", "���ۑ��x��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
                Case DialogResult.Yes
                    SaveLog()
                Case DialogResult.Cancel
                    Exit Sub
                Case DialogResult.No
                    main_frm.log_lb.Items.Clear()
                    'main_frm.log_lb.Items.Add(vbTab)
                    dirty = False
                    white_line = True
                    main_frm.Text = "�V�K���O�t�@�C��.txt"
            End Select
        Else
            main_frm.log_lb.Items.Clear()
            'main_frm.log_lb.Items.Add(vbTab)
            dirty = False
            white_line = True
            main_frm.Text = "�V�K���O�t�@�C��.txt"

        End If
    End Sub

    '�n���ꂽ���e�����O���ɑ}��
    Public Sub insert_memo()
        insert_memo_value(timecode.Text, Memo.Text)
        Memo.Text = ""
        timecode.Text = ""
    End Sub

    Sub insert_memo_value(ByVal tcode As String, ByVal memo_text As String)
        '�A���h�D�o�b�t�@�ۑ�
        SaveUndoBuffer()
        If IsPlaying() Then
            If tcode = "now" Then
                tcode = sec_to_minsec(player_frm.Player1.Ctlcontrols.currentPosition() - My.Settings.offset)
            End If

            If tcode = "" Then
                timecode.Focus()
                tcode = timecode.Text
                '�Đ��I����Ƀ^�C���R�[�h���󗓂ɂȂ錏�ɑΏ�
                If tcode = "" Then
                    tcode = "00:00:00"
                End If
            End If

            '��O�h�~�̋�s������
            If white_line = True Then
                log_lb.Items.Clear()
                white_line = False
            End If

            log_lb.Items.Add(tcode & vbTab & memo_text)

            '�}���������ڂ�I��
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
        '���ۑ��t���O�𗧂Ă�
        If dirty = False Then
            dirty = True
            Me.Text &= "*"
        End If
        enable_savemenu()

    End Sub

    Sub enable_savemenu()
        '�ۑ��n���j���[��L���ɂ���
        MenuItem3.Enabled = True
        MenuItem4.Enabled = True
        MenuItem27.Enabled = True

    End Sub


#Region " Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h "

    Public Sub New()
        MyBase.New()

        ' ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
        InitializeComponent()

        ' InitializeComponent() �Ăяo���̌�ɏ�������ǉ����܂��B

    End Sub

    ' Form �� dispose ���I�[�o�[���C�h���ăR���|�[�l���g�ꗗ���������܂��B
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
    Private components As System.ComponentModel.IContainer

    ' ���� : �ȉ��̃v���V�[�W���́AWindows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
    ' Windows �t�H�[�� �f�U�C�i���g���ĕύX���Ă��������B  
    ' �R�[�h �G�f�B�^�͎g�p���Ȃ��ł��������B
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
        Me.MenuItem1.Text = "�t�@�C��(&F)"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 0
        Me.MenuItem9.Shortcut = System.Windows.Forms.Shortcut.CtrlM
        Me.MenuItem9.Text = "����t�@�C����I��...(&M)"
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
        Me.MenuItem2.Text = "�V�K���O(&N)"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 3
        Me.MenuItem8.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.MenuItem8.Text = "���O���J��...(&F)"
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = 4
        Me.MenuItem19.Text = "���O��ǉ�����..."
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
        Me.MenuItem3.Text = "���O���㏑���ۑ�(&S)"
        '
        'MenuItem4
        '
        Me.MenuItem4.Enabled = False
        Me.MenuItem4.Index = 7
        Me.MenuItem4.Text = "���O��t���ă��O��ۑ�...(&A)"
        '
        'MenuItem27
        '
        Me.MenuItem27.Enabled = False
        Me.MenuItem27.Index = 8
        Me.MenuItem27.Text = "chapters.txt�`���ŃG�N�X�|�[�g..."
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 9
        Me.MenuItem10.Text = "-"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 10
        Me.MenuItem5.Text = "�I��(&X)"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 1
        Me.MenuItem11.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem20, Me.MenuItem21, Me.MenuItem12, Me.MenuItem13, Me.MenuItem14, Me.MenuItem15, Me.MenuItem32, Me.MenuItem33, Me.MenuItem30, Me.MenuItem31, Me.MenuItem24, Me.MenuItem26, Me.MenuItem25, Me.MenuItem16})
        Me.MenuItem11.Text = "�ҏW(&E)"
        '
        'MenuItem20
        '
        Me.MenuItem20.Enabled = False
        Me.MenuItem20.Index = 0
        Me.MenuItem20.Shortcut = System.Windows.Forms.Shortcut.CtrlZ
        Me.MenuItem20.Text = "���ɖ߂�(&U)"
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
        Me.MenuItem12.Text = "�J�b�g(&T)"
        '
        'MenuItem13
        '
        Me.MenuItem13.Enabled = False
        Me.MenuItem13.Index = 3
        Me.MenuItem13.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.MenuItem13.Text = "�R�s�[(&C)"
        '
        'MenuItem14
        '
        Me.MenuItem14.Enabled = False
        Me.MenuItem14.Index = 4
        Me.MenuItem14.Shortcut = System.Windows.Forms.Shortcut.CtrlV
        Me.MenuItem14.Text = "�y�[�X�g(&P)"
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
        Me.MenuItem32.Text = "����(��)...(&F)"
        '
        'MenuItem33
        '
        Me.MenuItem33.Enabled = False
        Me.MenuItem33.Index = 7
        Me.MenuItem33.Shortcut = System.Windows.Forms.Shortcut.CtrlG
        Me.MenuItem33.Text = "��������"
        '
        'MenuItem30
        '
        Me.MenuItem30.Index = 8
        Me.MenuItem30.Text = "����(�S�I��)..."
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
        Me.MenuItem24.Text = "�I���s���C��..."
        '
        'MenuItem26
        '
        Me.MenuItem26.Index = 11
        Me.MenuItem26.Shortcut = System.Windows.Forms.Shortcut.Del
        Me.MenuItem26.Text = "�I���s���폜"
        '
        'MenuItem25
        '
        Me.MenuItem25.Index = 12
        Me.MenuItem25.Text = "-"
        '
        'MenuItem16
        '
        Me.MenuItem16.Index = 13
        Me.MenuItem16.Text = "���ݒ�...(&O)"
        '
        'MenuItem22
        '
        Me.MenuItem22.Index = 2
        Me.MenuItem22.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem23})
        Me.MenuItem22.Text = "�w���v(&H)"
        '
        'MenuItem23
        '
        Me.MenuItem23.Index = 0
        Me.MenuItem23.Text = "�����ɂ���"
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
        Me.Done.Text = "�L�^"
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
        Me.Button1.Text = "����(&U)"
        '
        'Bt_F5
        '
        Me.Bt_F5.Location = New System.Drawing.Point(865, 70)
        Me.Bt_F5.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F5.Name = "Bt_F5"
        Me.Bt_F5.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F5.TabIndex = 8
        Me.Bt_F5.Text = "F5: �^�X�N����"
        '
        'Bt_F4
        '
        Me.Bt_F4.Location = New System.Drawing.Point(660, 70)
        Me.Bt_F4.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F4.Name = "Bt_F4"
        Me.Bt_F4.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F4.TabIndex = 7
        Me.Bt_F4.Text = "F4: ��蔭��"
        '
        'Bt_F3
        '
        Me.Bt_F3.Location = New System.Drawing.Point(455, 70)
        Me.Bt_F3.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F3.Name = "Bt_F3"
        Me.Bt_F3.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F3.TabIndex = 6
        Me.Bt_F3.Text = "F3: �����Ҕ��b"
        '
        'Bt_F2
        '
        Me.Bt_F2.Location = New System.Drawing.Point(249, 70)
        Me.Bt_F2.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F2.Name = "Bt_F2"
        Me.Bt_F2.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F2.TabIndex = 5
        Me.Bt_F2.Text = "F2: �팱�Ҕ��b"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(372, 24)
        Me.Label4.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 32)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "����(&M):"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(15, 24)
        Me.Label1.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(154, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "�^�C���R�[�h(&L):"
        '
        'Bt_F1
        '
        Me.Bt_F1.Location = New System.Drawing.Point(44, 70)
        Me.Bt_F1.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.Bt_F1.Name = "Bt_F1"
        Me.Bt_F1.Size = New System.Drawing.Size(176, 40)
        Me.Bt_F1.TabIndex = 4
        Me.Bt_F1.Text = "F1: �^�X�N�J�n"
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
        Me.MenuItem18.Text = "���̍s���C��"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 1
        Me.MenuItem17.Text = "���̍s���폜"
        '
        'MenuItem29
        '
        Me.MenuItem29.Index = 2
        Me.MenuItem29.Text = "���̒��O(1�b�O)�ɋ󔒍s��}��"
        '
        'MenuItem28
        '
        Me.MenuItem28.Index = 3
        Me.MenuItem28.Text = "�����̉�ʎʐ^���B��"
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
        Me.Text = "�V�K���O�t�@�C��.txt"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        '���g���A�N�e�B�u�ɂȂ������ɁA����E�C���h�E��R���g���[���E�C���h�E���A�N�e�B�u�ɂ���
        'player_frm.BringToFront()
        'control_frm.BringToFront()
        'Me.BringToFront()
        'Memo.Focus()
    End Sub





    '------------------------------------------------------------------------------------------------------------------------------------
    ' �R���g���[�����̃X�N���v�g
    '------------------------------------------------------------------------------------------------------------------------------------

    '���C���E�E�C���h�E���J���ꂽ���̏���
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�㏑���A�b�v�f�[�g���ɐݒ�������p��
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
        '���O�����󂾂ƃI�[�i�[�h���[�Ŏd�ؐ����`�悳��Ȃ��̂ŋ�s�����Ƃ�
        'log_lb.Items.Add(vbTab)

        '�R�}���h���C��������z��Ŏ擾����
        cmds = System.Environment.GetCommandLineArgs()
        '�t�@�C�����h���b�v����ċN�������ꍇ�̏���
        If cmds.Length > 1 Then
            main_frm.LoadMovie(cmds.GetValue(1))
        End If
    End Sub



    '�Đ��{�^���ōĐ��J�n
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        control_frm.play_btn.PerformClick()
    End Sub

    ' �L�^�{�^�������v���V�[�W���̌Ăяo��
    Private Sub Done_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Done.Click
        insert_memo()
    End Sub



    '�V���[�g�J�b�g�L�[�̏���
    ' �t�H�[����KeyPreview = true�ŁA�܂��t�H�[�����L�[�C�x���g���E��
    Private Sub Form1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        keyinput(sender, e)
    End Sub

    '�������ŃL�[���͂��������ꍇ�̓���
    Private Sub Memo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Memo.KeyDown
        ' Alt�V���[�g�J�b�g�Ńr�[�v���Ȃ�����
        If e.Alt = True Then
            e.SuppressKeyPress = True
        End If
    End Sub


    Private Sub memo_keyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Memo.KeyPress
        '�������b�N�I��
        If My.Settings.auto_lockon = True And e.KeyChar <> vbBack And e.KeyChar <> vbCr And main_frm.Memo.Text = "" Then
            LockOn()
        End If

        '�����E�t�B�[���h��Enter�������ꂽ���ABeep��炳�Ȃ��悤�ɂ���B
        If e.KeyChar = vbCr Then
            e.Handled = True
        End If
    End Sub



    '�t�@�C���E���j���[�Łu�I���v��I�񂾎��̓���
    Private Sub MenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem5.Click
        If dirty = True Then
            Select Case MessageBox.Show("���O���ۑ�����Ă��܂���B�ۑ����܂����H", "���ۑ��x��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
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

    '�N���[�Y�{�^���������ꂽ���̏���
    Private Sub Form1_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        If dirty = True Then
            Select Case MessageBox.Show("���O���ۑ�����Ă��܂���B�ۑ����܂����H", "���ۑ��x��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
                Case DialogResult.Yes
                    SaveLog()
                Case DialogResult.Cancel
                    e.Cancel = True
                Case DialogResult.No
                    End
            End Select
        End If
    End Sub


    '���O��t���ă��O��ۑ�
    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        GetSaveFile()
        If logfile <> "" Then
            SaveLog()
        End If
    End Sub


    ' �t�@�C�����j���[�́u�V�K���O�v���I�����ꂽ���̏���
    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        new_document()
    End Sub

    ' �t�@�C�����j���[�́u������J���v���I�����ꂽ���̏���
    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        '���ۑ����O������ꍇ�͕ۑ��m�F��������Ń��O�𖕏�
        new_document()
        With OpenFileDialog1
            .Title = "���͂��铮��t�@�C����I��"
            .CheckFileExists = True
            .RestoreDirectory = True
            .Filter = "����t�@�C��(*.mp4;*.m4v;*.m2ts;*.asf;*.avi;*.mpg;*.wmv)|*.mp4;*.m4v;*.m2ts;*.asf;*.avi;*.mpg;*.wmv|���ׂẴt�@�C��(*.*)|*.*"
            .CheckFileExists = True
            If .ShowDialog = DialogResult.OK Then
                LoadMovie(OpenFileDialog1.FileName)
            End If
        End With
    End Sub

    '������J��
    Sub LoadMovie(ByVal movfile As String)
        If player_frm Is Nothing Then
            player_frm = New Form2
        End If
        If player_frm.IsDisposed Then
            player_frm = New Form2
        End If
        '�t�@�C���������o���A����E�C���h�E�̃^�C�g���o�[�ɕ\��
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

        ' ���摀��p�l�����J��
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
        'switch_grab(get_filename_extension(movfile)) '�B�e�{�^���̐؂�ւ�


        '�����̃e�L�X�g�t�@�C��������΃��O�Ɣ��f���ǂݍ���
        If System.IO.File.Exists(get_filename_body(movfile) & ".txt") Then
            LoadLogFile(get_filename_body(movfile) & ".txt")
            enable_savemenu()
        Else
            '�Ȃ��ꍇ�͐V�K���̂�����
            Me.Text = "�V�K���O�t�@�C��.txt"
            logfile = ""
        End If

        player_frm.Player1.URL = movfile
        '�Đ����n�܂����烁�����Ƀt�H�[�J�X���ړ�
        Memo.Focus()
    End Sub


    '���O���Ń_�u���N���b�N���ꂽ�s�̃^�C���R�[�h�̈ʒu�ɓ�����W�����v
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


    '���O���ŃN���b�N���ꂽ�s�̃^�C���R�[�h�̈ʒu�ɓ�����W�����v
    Private Sub log_lb_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles log_lb.SelectedIndexChanged

    End Sub



    ' ���O���ŉE�N���b�N�����ꍇ�ɁA�R���e�N�X�g���j���[��\������O�ɂ����̍��ڂ�I������
    Private Sub log_lb_mousedown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles log_lb.MouseDown
        If e.Button = MouseButtons.Right Then
            Dim point As New Point(e.X, e.Y)
            log_lb.SelectedIndex = log_lb.IndexFromPoint(point)
        End If
    End Sub


    '���O���Ŗ��֌W�ȂƂ�����N���b�N���ꂽ��t�H�[�J�X���������ɖ߂�
    Private Sub log_lb_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles log_lb.MouseUp
        Memo.Focus()
    End Sub

    '�^�C���R�[�h�����t�H�[�J�X�𓾂���A�����_�̃^�C���R�[�h��}���i���b�N�I���j
    Public Sub timecode_gotfocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timecode.GotFocus
        LockOn()
    End Sub

    '�㏑���ۑ����I�����ꂽ���̓���
    Private Sub MenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem3.Click
        SaveLog()
    End Sub

    '���O�t�@�C���ǂݍ��݂��I�����ꂽ���̓���
    Private Sub MenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem8.Click
        new_document()
        LoadLog()
    End Sub

    '���j���[����u���O��ǉ�����...�v�i�����f�[�^�������Ȃ��j
    Private Sub MenuItem19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem19.Click
        LoadLog()
    End Sub

    ' �I�[�i�[�h���[��ListBox�̊O�ς�ύX����
    Private Sub log_lb_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles log_lb.DrawItem
        ' Set the DrawMode property to draw fixed sized items.
        log_lb.DrawMode = DrawMode.OwnerDrawFixed
        ' Draw the background of the ListBox control for each item.
        e.DrawBackground()

        '�f���~�^�ʒu�ɏc����`�悷��B
        Dim mypen As Pen
        mypen = Pens.Gray
        Dim lineX As Int16 = Int(45 * dpiScale)
        e.Graphics.DrawLine(mypen, lineX, 0, lineX, log_lb.Width)

        '�I���s�̔w�i�F��������
        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            Dim bgColor As Brush = New SolidBrush(Color.LightSkyBlue)
            e.Graphics.FillRectangle(bgColor, New Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
        End If


        ' �����F��������
        Dim textColor As Brush
        textColor = Brushes.Black

        ' ��O���N����̂ŁA�A�C�e������̎��͕`�揈�����Ȃ�
        If log_lb.Items.Count > 0 Then
            e.Graphics.DrawString(log_lb.Items(e.Index), e.Font, textColor, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
        End If

        ' If the ListBox has focus, draw a focus rectangle around the selected item.
        e.DrawFocusRectangle()
    End Sub


    '���ݒ�E�C���h�E���J��
    Private Sub MenuItem16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem16.Click
        option_frm = New Form4
        option_frm.Show()
    End Sub


    ' ���O���̑I�����ꂽ�s���폜�i�R���e�N�X�g���j���[����Ăяo���j
    Private Sub MenuItem17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem17.Click
        If log_lb.SelectedIndex <> -1 Then
            SaveUndoBuffer()
            Dim i As Integer
            For i = 0 To log_lb.SelectedIndices.Count - 1
                log_lb.Items.RemoveAt(log_lb.SelectedIndices.Item(0))
                '���X�g�{�b�N�X���󂾂ƃI�[�i�[�h���[�ŏc�����\������Ȃ������s������
                'If log_lb.Items.Count = 0 Then
                'log_lb.Items.Add(vbTab)
                'End If
            Next
        Else
            MsgBox("�ΏۂƂȂ�s���I������Ă��܂���B")
        End If
    End Sub


    ' ���O���̑I�����ꂽ�s���C���i�R���e�N�X�g���j���[����Ăяo���j
    Private Sub MenuItem18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem18.Click
        If log_lb.SelectedIndex <> -1 Then

            Dim rest, tmp(1), tc(2) As String
            tmp = log_lb.SelectedItem.split(vbTab)

            '�����Ƀ^�u���܂܂�Ă���ꍇ�̑Ώ�
            rest = log_lb.SelectedItem.ToString.Replace(tmp(0) & vbTab, "")

            tc = tmp(0).Split(":")
            cmt_dialog = New Dialog1
            cmt_dialog.tc_hour.Value = tc(0)
            cmt_dialog.tc_min.Value = tc(1)
            cmt_dialog.tc_sec.Value = tc(2)
            cmt_dialog.Cmt_Text.Text = rest
            cmt_dialog.ShowDialog()

        Else
            MsgBox("�ΏۂƂȂ�s���I������Ă��܂���B")
        End If
    End Sub

    '���j���[����u���̍s���C���v�����s
    Private Sub MenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem24.Click
        MenuItem18.PerformClick()
    End Sub


    '���j���[����u���̍s���폜�v�����s
    Private Sub MenuItem26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem26.Click
        MenuItem17.PerformClick()
    End Sub


    Sub change_memo(ByVal memo As String)
        SaveUndoBuffer()
        log_lb.Items.Item(log_lb.SelectedIndex) = memo(0) & vbTab & memo
        flag_dirty()

    End Sub


    Private Sub Memo_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Memo.KeyUp
        '�t�@���N�V���� F1�`F5�ɃV���[�g�J�b�g����
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
        insert_memo_value("now", "�����������@�^�X�N�J�n�@����������")
    End Sub

    Sub f2_keypress()
        If Memo.Text = "" Then
            LockOn()
            Memo.Text = "�팱�ҁu�v"
            Memo.SelectionStart = 4
            Memo.Focus()
        Else
            Memo.Text = "�팱�ҁu" & Memo.Text & "�v"
            Memo.SelectionStart = Memo.TextLength - 1
            Memo.Focus()
        End If
    End Sub
    Sub f3_keypress()
        If Memo.Text = "" Then
            LockOn()
            Memo.Text = "�����ҁu�v"
            Memo.SelectionStart = 4
            Memo.Focus()
        Else
            Memo.Text = "�����ҁu" & Memo.Text & "�v"
            Memo.SelectionStart = Memo.TextLength - 1
            Memo.Focus()
        End If
    End Sub
    Sub f4_keypress()
        If Memo.Text = "" Then
            LockOn()
            Memo.Text = "��蔭���F"
            Memo.SelectionStart = 5
            Memo.Focus()
        Else
            Memo.Text = "��蔭���F" & Memo.Text
            Memo.SelectionStart = Memo.TextLength
            Memo.Focus()
        End If
    End Sub
    Sub f5_keypress()
        insert_memo_value("now", "�����������@�^�X�N�����@����������")
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

    ''���O���Ƀt�@�C�����h���b�v���ꂽ��A�y���h
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

    '���O�E�t�H�[���̒��g���A���h�D�p�o�b�t�@�ɕۑ�
    Sub SaveUndoBuffer()
        Dim i As Integer
        undobuffer = ""
        For i = 0 To log_lb.Items.Count - 1
            undobuffer = undobuffer & log_lb.Items(i) & vbCrLf
        Next i
        '���j���[���ڂ�L����
        MenuItem20.Enabled = True
    End Sub

    '���O�E�t�H�[���̒��g���A���h�D�p�o�b�t�@���畜��
    Sub LoadUndoBuffer()
        '���s�O�̃X�N���[���ʒu��ۑ�
        Dim topindex As Integer = log_lb.TopIndex
        Dim selectedindex As Integer = log_lb.SelectedIndex

        '�ǂݍ��ݒ��`�揈���𒆒f��������
        log_lb.BeginUpdate()
        '���O�������
        log_lb.Items.Clear()

        '�A���h�D�o�b�t�@����P�s���ǂݍ���Ń��O���ɒǉ�
        Dim i As Integer
        Dim line() As String = Split(undobuffer, vbCrLf)
        For i = 0 To line.Length - 2
            log_lb.Items.Add(line(i))
        Next

        log_lb.TopIndex = topindex
        log_lb.SelectedIndex = selectedindex

        '�`�揈�����ĊJ
        log_lb.EndUpdate()

        '���j���[���ڂ𖳌���
        MenuItem20.Enabled = False
    End Sub

    Private Sub log_lb_ValueMemberChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles log_lb.ValueMemberChanged
        SaveUndoBuffer()
    End Sub

    ' ���j���[����u���ɖ߂��v���I������Ă�����s
    Private Sub MenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem20.Click
        LoadUndoBuffer()
    End Sub

    '���O����Ń}�E�X�z�C�[���X�N���[��������
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



    '################## chapters.txt�֘A #############################
    '���O�E�t�H�[���̒��g��ۑ�
    Sub SaveLog_chapters()
        '�㏑���̌��ۂ��o�Ă���̂ŁA����t�@�C�����₢���킹�ɂ��Ă݂�B
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


            '�t�@�C�������o�����s
            Dim sw As New System.IO.StreamWriter(chapterfile, False, System.Text.Encoding.UTF8)
            'body�̓��e�����ׂď�������
            sw.Write(body)
            '����
            sw.Close()

        End If
    End Sub

    '�ۑ����郍�O�t�@�C����I������_�C�A���O��\�����Ď擾
    Sub GetSaveFile_chapters()
        With SaveFileDialog1
            .Title = "���O�����ă��O��ۑ�"
            '�f�t�H���g�̃t�@�C�����́A����t�@�C�����̊g���q��.txt�ɑւ�������
            If movfile <> "" Then
                .FileName = GetFullPathToFileName(movfile).Substring(0, GetFullPathToFileName(movfile).LastIndexOf(".")) & ".chapters.txt"
            ElseIf logfile <> "" Then
                .FileName = logfile.Replace(".txt", ".chapters.txt")
            Else
                .FileName = "�V�K�`���v�^�[�t�@�C��.chapters.txt"
            End If
            .Filter = "chapters.txt�`���e�L�X�g(*.chapters.txt)|*.chapters.txt"
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

    '����...
    Private Sub MenuItem32_Click(sender As Object, e As EventArgs) Handles MenuItem32.Click
        Dim inputText As String
        inputText = InputBox("�������[�h�����Ă��������B���݂̍ŏ��̑I���s���珇�����ɍł��߂��s���I������܂��B", "����(��)")
        log_lb.ClearSelected()

        '��������L���ɂ���
        lastSearchWord = inputText
        MenuItem33.Enabled = True

        Call lbSearchNext(inputText) '�}�b�`�����s��I��
    End Sub

    '��������
    Private Sub MenuItem33_Click(sender As Object, e As EventArgs) Handles MenuItem33.Click
        Dim LastIndex As Integer = log_lb.SelectedIndex
        log_lb.ClearSelected()
        If log_lb.SelectedIndex >= log_lb.Items.Count - 2 Then
            '�Ō�̍s���I������Ă�����ŏ��ɖ߂�
            log_lb.SetSelected(0, True)
        Else
            '���̍s����n�߂�
            log_lb.SetSelected(LastIndex + 1, True)
        End If
        Call lbSearchNext(lastSearchWord) '�Ō�Ɏg����������ōČ���

    End Sub



    '����(�S�I��)
    Private Sub MenuItem30_Click(sender As Object, e As EventArgs) Handles MenuItem30.Click
        Dim inputText As String
        inputText = InputBox("�������[�h�����Ă��������B���̃��[�h���܂ލs���S�đI������܂��B", "����(�S�I��)")
        log_lb.ClearSelected()
        Call lbSearchSelectAll(inputText) '�}�b�`�����s��I��
        If log_lb.SelectedIndices.Count > 1 Then
            MessageBox.Show(log_lb.SelectedIndices.Count & " �ӏ�������܂���")
        Else
            MessageBox.Show("���t����܂���ł���")
            log_lb.ClearSelected()
        End If

    End Sub


    '�E�N���b�N���j���[����Î~��ۑ�
    Private Sub MenuItem28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem28.Click
        If log_lb.SelectedIndex <> -1 Then
            Dim i As Integer
            For i = 0 To log_lb.SelectedIndices.Count - 1
                Dim tmp(1) As String
                tmp = log_lb.SelectedItems(i).Split(vbTab)
                ScreenShot(minsec_to_sec(minsec_to_hourminsec(tmp(0))), True, tmp(1))
            Next
        Else
            MsgBox("�ΏۂƂȂ�s���I������Ă��܂���B")
        End If
    End Sub



    '���O�ɋ󔒍s��}��
    Private Sub MenuItem29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem29.Click
        If log_lb.SelectedIndex <> -1 Then

            Dim tmp(1), tc As String
            tmp = log_lb.SelectedItem.split(vbTab)
            tc = sec_to_minsec(minsec_to_sec(tmp(0)) - 1)

            SaveUndoBuffer()
            log_lb.Items.Add(tc & vbTab)
            flag_dirty()

        Else
            MsgBox("�ΏۂƂȂ�s���I������Ă��܂���B")
        End If
    End Sub

    '��ʍĕ`��ŌĂ΂��
    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        '�f�B�X�v���C�X�P�[���l���X�V
        Dim g As Graphics = e.Graphics
        Console.WriteLine(g.DpiX)
        dpiScale = g.DpiX / 96.0F
        If 12 * dpiScale < 256 Then
            log_lb.ItemHeight = Int(12 * dpiScale) '12�̓f�t�H���g��ItemHeight
        End If

    End Sub


    '�@�����i�����j
    Private Sub lbSearchNext(ByVal SearchStr As String)
        Console.WriteLine("����:" + SearchStr + " �I��:" + log_lb.SelectedIndices.Count.ToString)
        If log_lb.SelectedIndices.Count = 0 Then
            Console.WriteLine("1�s���I������ĂȂ��ꍇ�͐擪�s")
            log_lb.SetSelected(0, True)
        ElseIf log_lb.SelectedIndices.Count > 1 Then
            Console.WriteLine(" �����I������Ă������͍ŏ��̍��ڂ��g��")
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
        MessageBox.Show("���t����܂���ł���")
        log_lb.ClearSelected()

    End Sub

    '�@�����i�S�I������j
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