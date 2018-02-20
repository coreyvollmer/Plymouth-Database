Public Class frmResUserList
    Private Sub lstUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstUsers.SelectedIndexChanged

    End Sub

    Private Sub frmResUserList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        createResidentDataBase()
    End Sub

End Class