Option Explicit On
Option Strict On

Public Class SaveMessageHandler
    Public Sub Start()
        FrmMain.lblSaveMsg.Visible = True
        Dim timer = New saveTimer
        Dim strTime As String
        timer.StartTimer()
        While (CInt(timer.GetTime) < 60000)
            strTime = CStr(timer.GetTime)
            If (CInt(strTime) < 1000) Then
                strTime = "1"
            ElseIf (CInt(strTime) < 10000) Then
                strTime = strTime.Chars(0)
            ElseIf (CInt(strTime) >= 10000) Then
                strTime = strTime.Chars(0) + strTime.Chars(1)
            End If
            FrmMain.lblSaveMsg.Text = "All changes saved " + strTime + " seconds ago."
            Threading.Thread.Sleep(tick) : Application.DoEvents()
        End While
        FrmMain.lblSaveMsg.Text = "All changes saved over a minute ago."
    End Sub
End Class