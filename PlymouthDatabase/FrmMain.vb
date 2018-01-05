Option Explicit On
Option Strict On
Imports System.IO

Public Class FrmMain

    Public Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Console.WriteLine("Program started, initializing...")
        timeKeeper = New Timerr
        timeKeeper.StartTimer()
        crypto = New CSPTripleDES("PlymouthCrossroads") 'If this argument is changed, all cryptography functions will become incompatible with legacy data
        Dim logInForm = New FrmSignIn
        logInForm.Show()
        CreateAccountStructure()
        'Application.Exit()
    End Sub

    'MenuStrip Subroutines

    'File, Log In
    Private Sub Menu_LogIn_Click(sender As Object, e As EventArgs) Handles menuStripLogIn.Click
        Dim signInForm = New FrmSignIn
        signInForm.Show()
    End Sub

    'File, Exit
    Private Sub Menu_Exit_Click(sender As Object, e As EventArgs) Handles menuStripExit.Click
        Console.WriteLine("Exiting Application Now.")
        Application.Exit()
    End Sub

    'Admin, Staff User Manager
    Private Sub Menu_StaffUserManager_Click(sender As Object, e As EventArgs) Handles menuStripStaffUserManager.Click
        If userList(currentID).isAdmin Then
            Dim userListForm = New frmStaffUserManager
            userListForm.Show()
        ElseIf Not userList(currentID).isAdmin Then
            MsgBox("You are not an administrator.")
        Else
            MsgBox("User has an undefined administrator property")
        End If
    End Sub

    'Admin, Resident Profile Manager
    Private Sub Menu_ResidentProfileManager_Click(sender As Object, e As EventArgs) Handles menuStripResProfileManager.Click
        If userList(currentID).isAdmin Then
            Dim residentManagerForm = New frmResidentManager
            residentManagerForm.Show()
        ElseIf Not userList(currentID).isAdmin Then
            MsgBox("You are not an administrator.")
        Else
            MsgBox("User has an undefined administrator property")
        End If
    End Sub

    'Help, About
    Private Sub Menu_About_Click(sender As Object, e As EventArgs) Handles menuStripAbout.Click

    End Sub
    'End MenuStrip Subroutines

    Private Sub MonthCalendar_DateSelected(sender As Object, e As DateRangeEventArgs) Handles monthCalendar.DateSelected
        selectedDay = monthCalendar.SelectionStart.ToString
        selectedDayArr = selectedDay.Split()
        'Console.WriteLine(selectedDayArr(0)) 'Sanity Check
        ClearAllLogs()
        FillShiftLogs(selectedDayArr(0))
    End Sub

    'Keypress triggers for all 12 text boxes. Upon keypress trigger, timer will reset and restart.
    Private Sub txtMorning1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMorning1.KeyPress
        morningTimer1.Stop()
        morningTimer1.Start()
    End Sub

    Private Sub txtMorning2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMorning2.KeyPress
        morningTimer2.Stop()
        morningTimer2.Start()
    End Sub

    Private Sub txtMorning3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMorning3.KeyPress
        morningTimer3.Stop()
        morningTimer3.Start()
    End Sub

    Private Sub txtMorning4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtMorning4.KeyPress
        morningTimer4.Stop()
        morningTimer4.Start()
    End Sub

    Private Sub txtAfternoon1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAfternoon1.KeyPress
        afternoonTimer1.Stop()
        afternoonTimer1.Start()
    End Sub

    Private Sub txtAfternoon2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAfternoon2.KeyPress
        afternoonTimer2.Stop()
        afternoonTimer2.Start()
    End Sub

    Private Sub txtAfternoon3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAfternoon3.KeyPress
        afternoonTimer3.Stop()
        afternoonTimer3.Start()
    End Sub

    Private Sub txtAfternoon4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAfternoon4.KeyPress
        afternoonTimer4.Stop()
        afternoonTimer4.Start()
    End Sub

    Private Sub txtNight1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNight1.KeyPress
        nightTimer1.Stop()
        nightTimer1.Start()
    End Sub

    Private Sub txtNight2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNight2.KeyPress
        nightTimer2.Stop()
        nightTimer2.Start()
    End Sub

    Private Sub txtNight3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNight3.KeyPress
        nightTimer3.Stop()
        nightTimer3.Start()
    End Sub

    Private Sub txtNight4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtNight4.KeyPress
        nightTimer4.Stop()
        nightTimer4.Start()
    End Sub
    'End Keypress triggers

    'Keypress timers
    Private Sub morningTimer1_Tick(sender As Object, e As EventArgs) Handles morningTimer1.Tick
        morningTimer1.Stop()
        TxtChangeHandler(1, txtMorning1.Text, txtMorning1.Name, 4)
    End Sub

    Private Sub morningTimer2_Tick(sender As Object, e As EventArgs) Handles morningTimer2.Tick
        morningTimer2.Stop()
        TxtChangeHandler(1, txtMorning2.Text, txtMorning2.Name, 5)
    End Sub

    Private Sub morningTimer3_Tick(sender As Object, e As EventArgs) Handles morningTimer3.Tick
        morningTimer3.Stop()
        TxtChangeHandler(1, txtMorning3.Text, txtMorning3.Name, 6)
    End Sub

    Private Sub morningTimer4_Tick(sender As Object, e As EventArgs) Handles morningTimer4.Tick
        morningTimer4.Stop()
        TxtChangeHandler(1, txtMorning4.Text, txtMorning4.Name, 7)
    End Sub

    Private Sub afternoonTimer1_Tick(sender As Object, e As EventArgs) Handles afternoonTimer1.Tick
        afternoonTimer1.Stop()
        TxtChangeHandler(2, txtAfternoon1.Text, txtAfternoon1.Name, 8)
    End Sub

    Private Sub afternoonTimer2_Tick(sender As Object, e As EventArgs) Handles afternoonTimer2.Tick
        afternoonTimer2.Stop()
        TxtChangeHandler(2, txtAfternoon2.Text, txtAfternoon2.Name, 9)
    End Sub

    Private Sub afternoonTimer3_Tick(sender As Object, e As EventArgs) Handles afternoonTimer3.Tick
        afternoonTimer3.Stop()
        TxtChangeHandler(2, txtAfternoon3.Text, txtAfternoon3.Name, 10)
    End Sub

    Private Sub afternoonTimer4_Tick(sender As Object, e As EventArgs) Handles afternoonTimer4.Tick
        afternoonTimer4.Stop()
        TxtChangeHandler(2, txtAfternoon4.Text, txtAfternoon4.Name, 11)
    End Sub

    Private Sub nightTimer1_Tick(sender As Object, e As EventArgs) Handles nightTimer1.Tick
        nightTimer1.Stop()
        TxtChangeHandler(3, txtNight1.Text, txtNight1.Name, 0)
    End Sub

    Private Sub nightTimer2_Tick(sender As Object, e As EventArgs) Handles nightTimer2.Tick
        nightTimer2.Stop()
        TxtChangeHandler(3, txtNight2.Text, txtNight2.Name, 1)
    End Sub

    Private Sub nightTimer3_Tick(sender As Object, e As EventArgs) Handles nightTimer3.Tick
        nightTimer3.Stop()
        TxtChangeHandler(3, txtNight3.Text, txtNight3.Name, 2)
    End Sub

    Private Sub nightTimer4_Tick(sender As Object, e As EventArgs) Handles nightTimer4.Tick
        nightTimer4.Stop()
        TxtChangeHandler(3, txtNight4.Text, txtNight4.Name, 3)
    End Sub

    'End Keypress timers

End Class