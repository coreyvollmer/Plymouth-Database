Option Explicit On
Option Strict On

Imports System.IO

Public Class frmUserList

    Private Sub frmUserList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreateAccountStructure()
        populateList()
    End Sub

    Private Function populateList() As Boolean
        lstUsers.Items.Clear()
        For i As Integer = 0 To idCount - 1
            lstUsers.Items.Add(userList(i).GetFirstName & " " & userList(i).GetLastName)
        Next
        Return True
    End Function

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        If tryToAddUser() Then
            MsgBox("Success")
            txtUser.Text = ""
            txtFirstName.Text = ""
            txtLastName.Text = ""
            txtPassword.Text = ""
            chkAdmin.Checked = False
        End If
    End Sub

    Private Function tryToAddUser() As Boolean
        If txtUser.Text = "" Then
            MsgBox("Please enter a username.", MsgBoxStyle.OkOnly)
            Return False
        End If
        If txtFirstName.Text = "" Then
            MsgBox("Please enter a first name.", MsgBoxStyle.OkOnly)
            Return False
        End If
        If txtLastName.Text = "" Then
            MsgBox("Please enter a last name.", MsgBoxStyle.OkOnly)
            Return False
        End If
        If txtPassword.Text = "" Then
            MsgBox("Please enter a password", MsgBoxStyle.OkOnly)
            Return False
        End If
        CheckAvailableIDs()
        Dim fileWriter As System.IO.StreamWriter
        Dim isAdmin As Integer

        If (chkAdmin.Checked) Then
            isAdmin = 1
        End If
        If (Not chkAdmin.Checked) Then
            isAdmin = 0
        End If
        fileWriter = My.Computer.FileSystem.OpenTextFileWriter(acctFilePath, True)
        fileWriter.WriteLine(
        firstAvailiD() & " " &
        crypto.EncryptData(txtUser.Text) & " " &
        crypto.EncryptData(txtPassword.Text) & " " &
        crypto.EncryptData(txtFirstName.Text) & " " &
        crypto.EncryptData(txtLastName.Text) & " " &
        isAdmin)
        fileWriter.Close()
        CreateAccountStructure()
        populateList()
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtPassword.Text = ""
        txtUser.Text = ""
        chkAdmin.Checked = False
        Return True
    End Function

    Public Function firstAvailiD() As Integer
        For i As Integer = 0 To 100
            If Not iDsUsed.Contains(i) Then
                Return i
            End If
        Next
        Return 100
    End Function

    Private Sub lstUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstUsers.SelectedIndexChanged

        Dim curIndex As Integer
        curIndex = lstUsers.SelectedIndex
        lblName.Text = userList(curIndex).GetFirstName & " " & userList(curIndex).GetLastName
        lblUser.Text = userList(curIndex).GetUserName
        If currentID < 10 Then
            lbliD.Text = "0" + CStr(userList(curIndex).iD)
        Else
            lbliD.Text = CType(userList(curIndex).iD, String)
        End If

        If userList(curIndex).isAdmin Then
            lblAdmin.Text = "True"
        End If
        If Not userList(curIndex).isAdmin Then
            lblAdmin.Text = "False"
        End If
        chkSelectedPassword.Checked = False
        txtSelectedPassword.Text = userList(curIndex).GetPassword
    End Sub

    Private Sub chkShowNewPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowNewPassword.CheckedChanged
        If chkShowNewPassword.Checked Then
            txtPassword.UseSystemPasswordChar = False
        End If
        If Not chkShowNewPassword.Checked Then
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub chkSelectedPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelectedPassword.CheckedChanged
        If chkSelectedPassword.Checked Then
            txtSelectedPassword.UseSystemPasswordChar = False
        End If
        If Not chkSelectedPassword.Checked Then
            txtSelectedPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim msgResult As Byte
        msgResult = CByte(MessageBox.Show("Are You Sure?", "Delete User Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
        If msgResult = DialogResult.Yes Then
            removeUserEntry(lstUsers.SelectedIndex)
            populateList()
            lblUser.Text = ""
            lblName.Text = ""
            lblAdmin.Text = ""
            txtSelectedPassword.Text = ""
        End If
    End Sub

    Public Sub removeUserEntry(iD As Integer)

        If Not lstUsers.SelectedIndex = -1 Then
            Dim fileLines As List(Of String)
            fileLines = New List(Of String)(File.ReadAllLines(acctFilePath))
            fileLines.RemoveAt(iD)
            If File.Exists(acctFilePath) Then
                File.Delete(acctFilePath)
            End If
            File.WriteAllLines(acctFilePath, fileLines.ToArray)
            CreateAccountStructure()
            CheckAvailableIDs()
        End If
    End Sub

End Class