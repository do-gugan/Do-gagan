Public Class Form5
    Inherits System.Windows.Forms.Form

#Region " Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h "

    Public Sub New()
        MyBase.New()

        ' ���̌Ăяo���� Windows �t�H�[�� �f�U�C�i�ŕK�v�ł��B
        InitializeComponent()

        ' InitializeComponent() �Ăяo���̌�ɏ�������ǉ����܂��B

    End Sub

    ' Form �́A�R���|�[�l���g�ꗗ�Ɍ㏈�������s���邽�߂� dispose ���I�[�o�[���C�h���܂��B
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
    'Windows �t�H�[�� �f�U�C�i���g���ĕύX���Ă��������B  
    ' �R�[�h �G�f�B�^���g���ĕύX���Ȃ��ł��������B
    Friend WithEvents Bt_Change As System.Windows.Forms.Button
    Friend WithEvents Bt_Cancel As System.Windows.Forms.Button
    Friend WithEvents TB_Edit As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.TB_Edit = New System.Windows.Forms.TextBox
        Me.Bt_Change = New System.Windows.Forms.Button
        Me.Bt_Cancel = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'TB_Edit
        '
        Me.TB_Edit.Location = New System.Drawing.Point(32, 16)
        Me.TB_Edit.Name = "TB_Edit"
        Me.TB_Edit.Size = New System.Drawing.Size(384, 19)
        Me.TB_Edit.TabIndex = 0
        Me.TB_Edit.Text = ""
        '
        'Bt_Change
        '
        Me.Bt_Change.Location = New System.Drawing.Point(352, 48)
        Me.Bt_Change.Name = "Bt_Change"
        Me.Bt_Change.Size = New System.Drawing.Size(80, 24)
        Me.Bt_Change.TabIndex = 1
        Me.Bt_Change.Text = "�C��"
        '
        'Bt_Cancel
        '
        Me.Bt_Cancel.Location = New System.Drawing.Point(256, 48)
        Me.Bt_Cancel.Name = "Bt_Cancel"
        Me.Bt_Cancel.Size = New System.Drawing.Size(80, 24)
        Me.Bt_Cancel.TabIndex = 2
        Me.Bt_Cancel.Text = "�L�����Z��"
        '
        'Form5
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 12)
        Me.ClientSize = New System.Drawing.Size(448, 78)
        Me.Controls.Add(Me.Bt_Cancel)
        Me.Controls.Add(Me.Bt_Change)
        Me.Controls.Add(Me.TB_Edit)
        Me.Name = "Form5"
        Me.Text = "Form5"
        Me.ResumeLayout(False)

    End Sub

#End Region

End Class
