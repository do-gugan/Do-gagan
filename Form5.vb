Public Class Form5
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
        Me.Bt_Change.Text = "修正"
        '
        'Bt_Cancel
        '
        Me.Bt_Cancel.Location = New System.Drawing.Point(256, 48)
        Me.Bt_Cancel.Name = "Bt_Cancel"
        Me.Bt_Cancel.Size = New System.Drawing.Size(80, 24)
        Me.Bt_Cancel.TabIndex = 2
        Me.Bt_Cancel.Text = "キャンセル"
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
