Option Explicit On
Option Strict On

Public Class FrmSignIn

    Dim failClose As Boolean = True
    Dim userCipher, passCipher As String
    Private retryCount As Integer = 3

    Private Sub FrmSignIn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FrmMain.Hide()
        txtUser.Select()
    End Sub

    Private Sub BtnSignIn_Click(sender As Object, e As EventArgs) Handles btnSignIn.Click
        retryCount = retryCount - 1
        If retryCount = 0 Then
            Close()
            FrmMain.Close()
        End If
        If Not TryToSignIn(txtUser.Text, txtPassword.Text) Then
            lblMessage.Text = "Invalid Credentials, " & retryCount & " attempts remaining."
        End If
    End Sub

    Function TryToSignIn(username As String, password As String) As Boolean
        Console.WriteLine("Attempting to sign in...")
        Console.WriteLine("Credentials: Username: " + username + ", Password: " + password)
        For i As Integer = 0 To idCount - 1
            If userList(i).GetUserName = username Then
                If userList(i).GetPassword = password Then
                    SignIn(userList(i).iD)
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Function SignIn(iD As Integer) As Boolean
        currentID = iD
        failClose = False
        Me.Close()
        UpdateUser()
        FrmMain.Show()
        FrmMain.WindowState = FormWindowState.Normal
        InitializeShiftLogs()
        Return True
    End Function

    Private Sub ChkBoxPW_CheckedChanged(sender As Object, e As EventArgs) Handles chkBoxPW.CheckedChanged
        If chkBoxPW.Checked Then
            txtPassword.UseSystemPasswordChar = True
        Else
            txtPassword.UseSystemPasswordChar = False
        End If
    End Sub

    Private Sub FrmSignIn_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        If failClose Then
            FrmMain.Close()
        End If
    End Sub
End Class