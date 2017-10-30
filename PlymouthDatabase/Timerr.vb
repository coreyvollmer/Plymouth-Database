Public Class Timerr

    Public stopWatch As Stopwatch

    Public Sub StartTimer()
        stopWatch = Stopwatch.StartNew()
    End Sub

    Public Function GetTime()
        Return stopWatch.ElapsedMilliseconds
    End Function

    Public Function ElapsedFiveSeconds() As Boolean
        Dim curTime As String = stopWatch.ElapsedMilliseconds.ToString
        If (curTime >= 5000) Then
            Return True
        End If
        Return False
    End Function
End Class


