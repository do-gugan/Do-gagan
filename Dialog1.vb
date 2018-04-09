Imports System.Windows.Forms

Public Class Dialog1

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        main_frm.SaveUndoBuffer()
        main_frm.log_lb.Items.Item(main_frm.log_lb.SelectedIndex) = Format(tc_hour.Value, "00") & ":" & Format(tc_min.Value, "00") & ":" & Format(tc_sec.Value, "00") & vbTab & Cmt_Text.Text
        main_frm.flag_dirty() '未保存フラグ
        main_frm.log_lb.Sorted = False
        main_frm.log_lb.Sorted = True
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ' 秒更新時、分への桁あがり、桁下がり処理
    Private Sub tc_sec_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tc_sec.ValueChanged
        If tc_sec.Value = 60 Then
            tc_sec.Value = 0
            tc_min.Value = tc_min.Value + 1
        ElseIf tc_sec.Value = -1 Then
            If tc_hour.Value * 60 + tc_min.Value > 0 Then
                tc_sec.Value = 59
                tc_min.Value = tc_min.Value - 1
            Else
                tc_sec.Value = 0
            End If
            End If
    End Sub

    ' 分更新時、時への桁あがり、桁下がり処理
    Private Sub tc_min_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tc_min.ValueChanged
        If tc_min.Value = 60 Then
            tc_min.Value = 0
            tc_hour.Value = tc_hour.Value + 1
        ElseIf tc_min.Value = -1 Then
            If tc_hour.Value > 0 Then
                tc_min.Value = 59
                tc_hour.Value = tc_hour.Value - 1
            Else
                tc_min.Value = 0
            End If
        End If
    End Sub
End Class
