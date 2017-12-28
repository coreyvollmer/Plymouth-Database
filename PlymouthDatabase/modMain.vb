Option Explicit On
Option Strict On

Imports System.IO

Module modMain
    'TODO map all debugging to this dim "debug"
    Public debug As Boolean = True
    Public timeKeeper As Timerr
    Public crypto As CSPTripleDES
    Public tick As Integer = 25
    Public userList() As User
    Public idCount As Integer
    Public iDsUsed As List(Of Integer)
    Public databaseRoot As String = "C:\Users\Corey Vollmer\"
    'Public databaseRoot As String = "C:\\Users\corey\"
    'Public databaseRoot2 As String = "\\PCSVR\Shared\db\"
    Public acctFilePath As String = databaseRoot + "Accounts.dat"
    Public shiftLogPath As String = databaseRoot + "Shift Logs\"
    Public currentID As Integer 'id of currently logged in user
    Public saveMsger As SaveMessageHandler

    Public shift1Seen, shift2Seen, shift3Seen As Boolean
    Public morning, afternoon, night As String
    Public selectedDay As String
    Public selectedDayTimeArr As String()
    Public selectedDayArr As String()
    Public lineCount As Integer
    Public userHasTab(0 To 2) As Boolean

    Dim q As Queue(Of String) = New Queue(Of String)

    'Searhes userList for matching iD and returns initials
    Public Function GetInitials(iD As Integer) As String
        Dim debug As Boolean = True
        If (debug) Then
            Console.WriteLine("Getting initals for iD: " + CStr(iD))
        End If
        Dim fNameArray As Char()
        Dim lNameArray As Char()
        For Each User In userList
            If User.iD = iD Then
                fNameArray = User.GetFirstName.ToCharArray
                lNameArray = User.GetLastName.ToCharArray
            End If
        Next
        If fNameArray(0) = "" Then
            fNameArray = CType("X", Char())
        End If
        If lNameArray(0) = "" Then
            lNameArray = CType("X", Char())
        End If
        Dim initials(2) As Char
        initials(0) = fNameArray(0)
        initials(1) = lNameArray(0)
        Return initials
    End Function

    Structure User
        Private firstName, lastName, userName, password As String
        Dim isAdmin As Boolean
        Public iD As Integer

        Public Function GetInitials() As String
            Dim fNameArray As Char() = CType(GetFirstName(), Char())
            Dim lNameArray As Char() = CType(GetLastName(), Char())
            Dim initials(2) As Char
            initials(0) = fNameArray(0)
            initials(1) = lNameArray(0)
            Return initials
        End Function

        Public Function GetFirstName() As String
            Return firstName
        End Function

        Public Function GetLastName() As String
            Return lastName
        End Function

        Public Function GetUserName() As String
            Return userName
        End Function

        Public Function GetPassword() As String
            Return password
        End Function

        Public Sub SetFirstName(fName As String)
            firstName = fName
        End Sub

        Public Sub SetLastName(lName As String)
            lastName = lName
        End Sub

        Public Sub SetUserName(uName As String)
            userName = uName
        End Sub

        Public Sub SetPassword(pass As String)
            password = pass
        End Sub
    End Structure

    Structure DateForm
        Public Day As UInt16
        Public Month As String
        Public Year As UInt16
    End Structure

    Structure ProgressNote
        Public residentNumber As Integer
        Public todaysDate As DateForm
        Public goalNum As Integer
        Public goalArray() As String
        Public achievementArray As String
    End Structure

    Public Sub AdjustTimeSlider()
        Dim explicit As Boolean = False
        If explicit Then
            Console.WriteLine("Adjusting time")
        End If
        Dim timer = New Timerr
        timer.StartTimer()
        Dim sliderVal As Integer
        While (True)
            Dim currentTime As Integer() = GetMilitaryTime()
            If explicit Then
                Console.WriteLine(currentTime(0))
                Console.WriteLine(currentTime(1))
            End If
            If (currentTime(0) = 0) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 0
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 1
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 2
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 3
                End If
            ElseIf (currentTime(0) = 1) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 4
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 5
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 6
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 7
                End If

            ElseIf (currentTime(0) = 2) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 8
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 9
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 10
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 11
                End If
            ElseIf (currentTime(0) = 3) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 12
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 13
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 14
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 15
                End If
            ElseIf (currentTime(0) = 4) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 16
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 17
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 18
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 19
                End If
            ElseIf (currentTime(0) = 5) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 20
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 21
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 22
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 23
                End If
            ElseIf (currentTime(0) = 6) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 24
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 25
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 26
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 27
                End If
            ElseIf (currentTime(0) = 7) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 28
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 29
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 30
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 31
                End If
            ElseIf (currentTime(0) = 8) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 32
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 33
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 34
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 35
                End If
            ElseIf (currentTime(0) = 9) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 36
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 37
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 38
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 39
                End If
            ElseIf (currentTime(0) = 10) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 40
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 41
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 42
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 43
                End If
            ElseIf (currentTime(0) = 11) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 44
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 45
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 46
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 47
                End If
            ElseIf (currentTime(0) = 12) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 48
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 49
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 50
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 51
                End If
            ElseIf (currentTime(0) = 13) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 52
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 53
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 54
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 55
                End If
            ElseIf (currentTime(0) = 14) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 56
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 57
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 58
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 59
                End If
            ElseIf (currentTime(0) = 15) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 60
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 61
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 62
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 63
                End If
            ElseIf (currentTime(0) = 16) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 64
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 65
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 66
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 67
                End If
            ElseIf (currentTime(0) = 17) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 68
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 69
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 70
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 71
                End If
            ElseIf (currentTime(0) = 18) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 72
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 73
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 74
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 75
                End If
            ElseIf (currentTime(0) = 19) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 76
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 77
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 78
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 79
                End If
            ElseIf (currentTime(0) = 20) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 80
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 81
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 82
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 83
                End If
            ElseIf (currentTime(0) = 21) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 84
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 85
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 86
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 87
                End If
            ElseIf (currentTime(0) = 22) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 88
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 89
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 90
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 91
                End If
            ElseIf (currentTime(0) = 23) Then
                If (currentTime(1) >= 0 And currentTime(1) < 15) Then
                    sliderVal = 92
                ElseIf (currentTime(1) >= 15 And currentTime(1) < 30) Then
                    sliderVal = 93
                ElseIf (currentTime(1) >= 30 And currentTime(1) < 45) Then
                    sliderVal = 94
                ElseIf (currentTime(1) >= 45 And currentTime(1) < 60) Then
                    sliderVal = 95
                End If
            End If
            FrmMain.tBarTime.Value = sliderVal
            If explicit Then
                Console.WriteLine(CStr(currentTime(0)) + CStr(currentTime(1)) + " Adjusted to " + CStr(sliderVal))
            End If
            Threading.Thread.Sleep(tick) : Application.DoEvents()
        End While
    End Sub

    Public Sub CheckAvailableIDs()
        iDsUsed = New List(Of Integer)
        'iDsUsed.Clear()
        Dim verbose As Boolean = True
        Console.WriteLine("Checking available ids...")
        Try
            idCount = File.ReadAllLines(acctFilePath).Length()
        Catch
            MsgBox("File Error")
        End Try
        Dim fileReader As System.IO.StreamReader
        fileReader = My.Computer.FileSystem.OpenTextFileReader(acctFilePath)
        Dim line As String
        Dim lineArray() As String
        While Not fileReader.EndOfStream
            line = fileReader.ReadLine()
            lineArray = line.Split(CType(" ", Char()))
            iDsUsed.Add(CInt(lineArray(0)))
        End While
        fileReader.Close()
    End Sub

    Public Sub CreateAccountStructure()
        If debug Then
            Console.WriteLine("Creating Account Structure... Time: " + CStr(timeKeeper.GetTime) + "ms")
        End If

        If Not File.Exists(acctFilePath) Then 'This handles case when account.dat file is not found. Creates user admin with password admin
            MsgBox("File error. New environment detected. Initializing...")
            File.Create(acctFilePath).Dispose()
            Dim newWriter As System.IO.StreamWriter
            newWriter = My.Computer.FileSystem.OpenTextFileWriter(acctFilePath, False)
            newWriter.WriteLine("0 tDoKrrPineiXtyLMJAyOiA== tDoKrrPineiXtyLMJAyOiA== tDoKrrPineiXtyLMJAyOiA== tDoKrrPineiXtyLMJAyOiA== 1")
            newWriter.Close()
        End If
        idCount = File.ReadAllLines(acctFilePath).Length()
        ReDim userList(idCount)
        Dim fileReader As System.IO.StreamReader
        fileReader = My.Computer.FileSystem.OpenTextFileReader(acctFilePath)
        Dim place As Integer = 0
        Dim line(0 To 5) As String
        While Not fileReader.EndOfStream
            If line IsNot "" Then
                line = fileReader.ReadLine().Split()
                userList(place).iD = CInt((line(0)))
                userList(place).SetUserName(crypto.DecryptData(line(1)))
                userList(place).SetPassword(crypto.DecryptData(line(2)))
                userList(place).SetFirstName(crypto.DecryptData(line(3)))
                userList(place).SetLastName(crypto.DecryptData(line(4)))
                If line(5) = "0" Then
                    userList(place).isAdmin = False
                End If
                If line(5) = "1" Then
                    userList(place).isAdmin = True
                End If
                If debug Then
                    Console.WriteLine("Added user iD: " + CStr(userList(place).iD) + " Name: " + userList(place).GetFirstName + " " + userList(place).GetLastName)
                End If
            End If
            place = place + 1
        End While
        fileReader.Close()
    End Sub

    'Empty??
    Public Sub ClearAllLogs()
        FrmMain.txtMorning1.Text = ""
        FrmMain.tabMorning1.Text = "Day Log"
        FrmMain.txtMorning2.Text = ""
        FrmMain.tabMorning2.Text = "Empty"
        FrmMain.txtMorning3.Text = ""
        FrmMain.tabMorning3.Text = "Empty"
        FrmMain.txtMorning4.Text = ""
        FrmMain.tabMorning4.Text = "Empty"
        FrmMain.txtAfternoon1.Text = ""
        FrmMain.tabAfternoon1.Text = "Afternoon Log"
        FrmMain.txtAfternoon2.Text = ""
        FrmMain.tabAfternoon2.Text = "Empty"
        FrmMain.txtAfternoon3.Text = ""
        FrmMain.tabAfternoon3.Text = "Empty"
        FrmMain.txtAfternoon4.Text = ""
        FrmMain.tabAfternoon4.Text = "Empty"
        FrmMain.txtNight1.Text = ""
        FrmMain.tabNight1.Text = "Night Log"
        FrmMain.txtNight2.Text = ""
        FrmMain.tabNight2.Text = "Empty"
        FrmMain.txtNight3.Text = ""
        FrmMain.tabNight3.Text = "Empty"
        FrmMain.txtNight4.Text = ""
        FrmMain.tabNight4.Text = "Empty"
    End Sub

    'Takes array of file names, and determines count for each time of day section
    Public Function GetShiftFileCount(files As String()) As Integer()
        Dim consoleDebug As Boolean = False 'Sub debugger
        If consoleDebug Then
            Console.WriteLine("Counting logs for selected day... Time: " + CStr(timeKeeper.GetTime) + "ms")
        End If

        Dim secFileCount(3) As Integer
        For i As Integer = 0 To 2
            secFileCount(i) = 0
        Next
        For Each file As String In files
            If file.Contains("Night") Then
                secFileCount(0) = secFileCount(0) + 1
            ElseIf file.StartsWith("Morning") Then
                secFileCount(1) = secFileCount(1) + 1
            ElseIf file.Contains("Evening") Then
                secFileCount(2) = secFileCount(2) + 1
            End If
        Next

        If consoleDebug Then
            Console.Write("Found " + CStr(secFileCount(0)) + " Overnight Files, ")
            Console.Write("Found " + CStr(secFileCount(1)) + " Morning files, and ")
            Console.WriteLine("Found " + CStr(secFileCount(2)) + " Evening Files.")
        End If
        Return secFileCount
    End Function

    'Fills log text boxes for date passed into argument
    Public Sub FillShiftLogs(day As String)
        Dim consoleDebug As Boolean = False 'Sub debugger
        If consoleDebug Then
            Console.WriteLine("Filling shift logs with data..." + CStr(timeKeeper.GetTime) + "ms")
        End If
        Dim monthDayYear As String() = day.Split(CType("/", Char()))
        Dim fullFilePath As String = shiftLogPath + monthDayYear(2) + "\" + monthDayYear(0) + "\" + monthDayYear(1) + "\"
        CheckDirectory(monthDayYear) ' Make sure directories are in place
        Dim Nopass As Boolean = True
        Dim files As String()
        While Nopass
            Try
                files = IO.Directory.GetFiles(fullFilePath) 'Get all files in directory
                Nopass = False
            Catch ex As Exception
                Console.WriteLine("Waiting to get directory...")
                Threading.Thread.Sleep(tick) : Application.DoEvents()
            End Try
        End While
        Dim tabCounter(0 To 2) As Integer
        Dim filledCounter(0 To 2) As Integer
        For i As Integer = 0 To 2
            filledCounter(i) = 0
        Next
        tabCounter = GetShiftFileCount(files) 'Calculate entries for each time column
        Dim fileReader As System.IO.StreamReader
        Dim filled As Boolean
        Dim tabID As String
        For Each file As String In files
            filled = False
            While Not (filled)
                Dim waiter As Boolean = True
                While waiter = True
                    Try
                        fileReader = My.Computer.FileSystem.OpenTextFileReader(file.ToString)
                        waiter = False
                    Catch ex As Exception
                        Console.WriteLine("Waiting to read...")
                        Threading.Thread.Sleep(tick) : Application.DoEvents()
                    End Try
                End While
                tabID = file.Substring(file.Length - 6, 2) 'gets tab id from filename, dynamic to length. do not change file naming convention

                If file.Contains("Night") Then
                    Console.WriteLine("Filling a night tab")
                    If CInt(tabID) = currentID Then
                        userHasTab(0) = True
                        Console.WriteLine("has night tab")
                    End If
                    Select Case filledCounter(2)
                        Case 0
                            FrmMain.txtNight1.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabNight1.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(2) = filledCounter(2) + 1
                            modifyTextControlHandler(0, tabID)
                            filled = True
                        Case 1
                            FrmMain.txtNight2.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabNight2.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(2) = filledCounter(2) + 1
                            filled = True
                        Case 2
                            FrmMain.txtNight3.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabNight3.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(2) = filledCounter(2) + 1
                            filled = True

                        Case 3
                            FrmMain.txtNight4.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabNight4.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(2) = filledCounter(2) + 1
                            filled = True
                    End Select
                End If


                If file.Contains("Morning") Then
                    Console.WriteLine("Filling a morning tab")
                    If CInt(tabID) = currentID Then
                        userHasTab(1) = True
                    End If
                    Select Case filledCounter(0)
                        Case 0
                            FrmMain.txtMorning1.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabMorning1.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(0) = filledCounter(0) + 1

                            filled = True

                        Case 1
                            FrmMain.txtMorning2.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabMorning2.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(0) = filledCounter(0) + 1
                            filled = True
                        Case 2
                            FrmMain.txtMorning3.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabMorning3.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(0) = filledCounter(0) + 1
                            filled = True
                        Case 3
                            FrmMain.txtMorning4.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabMorning4.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(0) = filledCounter(0) + 1
                            filled = True
                    End Select
                End If

                If file.Contains("Evening") Then
                    Console.WriteLine("Filling an evening tab")
                    If CInt(tabID) = currentID Then
                        userHasTab(2) = True
                        Console.WriteLine("has afternoon tab")
                    End If
                    Select Case filledCounter(1)
                        Case 0
                            FrmMain.txtAfternoon1.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabAfternoon1.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(1) = filledCounter(1) + 1
                            filled = True
                        Case 1
                            FrmMain.txtAfternoon2.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabAfternoon2.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(1) = filledCounter(1) + 1
                            filled = True
                        Case 2
                            FrmMain.txtAfternoon3.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabAfternoon3.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(1) = filledCounter(1) + 1
                            filled = True
                        Case 3
                            FrmMain.txtAfternoon4.Text = crypto.DecryptData(fileReader.ReadToEnd)
                            FrmMain.tabAfternoon4.Text = tabID + " " + userList(CInt(tabID)).GetInitials
                            filledCounter(1) = filledCounter(1) + 1
                            filled = True
                    End Select
                End If
            End While
        Next
        Console.WriteLine("Logs Filled Successfully, hiding...")
        Console.WriteLine("tabCounter(0)=" + CStr(tabCounter(0)) + " tabCounter(1)=" + CStr(tabCounter(1)) + " tabCounter(2)=" + CStr(tabCounter(2)))
        HideTabs(tabCounter)
    End Sub

    Public Function GetMilitaryTime() As Integer()
        Dim timeArray As String()
        Dim clockArray As String()
        Dim clockIntArray(3) As Integer
        Dim explicit As Boolean = False

        timeArray = CStr(TimeOfDay).Split(CType(" ", Char()))
        clockArray = timeArray(0).Split(CType(":", Char()))
        If (timeArray(1) = "PM") Then
            Dim hour As Integer = CInt(clockArray(0))
            'hour = hour + 12
            clockArray(0) = CStr(hour)
        End If
        For i As Integer = 0 To 2
            clockIntArray(i) = CInt(clockArray(i))
        Next
        If explicit Then
            Console.WriteLine("Getting Military Time: " + CStr(clockIntArray(0)) + ":" + CStr(clockIntArray(1)) + ":" + CStr(clockIntArray(2)))
        End If
        Return clockIntArray
    End Function

    Public Sub InitializeShiftLogs()
        Console.WriteLine("Initializing Shift Logs...")
        Dim todaysDate As String = CType(Date.Today, String)
        Dim monthDayYear As String() = todaysDate.Split(CType("/", Char()))
        Dim fullFilePath As String = shiftLogPath + monthDayYear(2) + "\" + monthDayYear(0) + "\" + monthDayYear(1) + "\"
        modMain.CheckDirectory(monthDayYear) ' Make sure directories are in place
        Dim files As String() = IO.Directory.GetFiles(fullFilePath) 'Get all files in directory
        'hideTabs(getShiftFileCount(files))
        FillShiftLogs(todaysDate)
        AdjustTimeSlider()
    End Sub

    Public Sub modifyTextControlHandler(tabID As Integer, userID As String)
        Dim consoleDebug As Boolean = True
        Dim userIDInt As Integer = Integer.Parse(userID)
        If consoleDebug Then
            Console.WriteLine("modifyTextControlHandler(tabID: " + CStr(tabID) + " userID: " + userID + "Current userID: " + CStr(currentID))
        End If
        Select Case tabID
            Case 0
                If currentID = userIDInt Then
                    FrmMain.tabNight1.Enabled = True
                Else
                    FrmMain.tabNight1.Enabled = False
                End If
            Case 1
                If currentID = userIDInt Then
                    FrmMain.tabNight2.Enabled = True
                Else
                    FrmMain.tabNight2.Enabled = False
                End If
            Case 2
                If currentID = userIDInt Then
                    FrmMain.tabNight3.Enabled = True
                Else
                    FrmMain.tabNight3.Enabled = False
                End If
            Case 3
                If currentID = userIDInt Then
                    FrmMain.tabNight4.Enabled = True
                Else
                    FrmMain.tabNight4.Enabled = False
                End If
            Case 4
                If currentID = userIDInt Then
                    FrmMain.tabMorning1.Enabled = True
                Else
                    FrmMain.tabMorning1.Enabled = False
                End If
            Case 5
                If currentID = userIDInt Then
                    FrmMain.tabMorning2.Enabled = True
                Else
                    FrmMain.tabMorning2.Enabled = False
                End If
            Case 6
                If currentID = userIDInt Then
                    FrmMain.tabMorning3.Enabled = True
                Else
                    FrmMain.tabMorning3.Enabled = False
                End If
            Case 7
                If currentID = userIDInt Then
                    FrmMain.tabMorning4.Enabled = True
                Else
                    FrmMain.tabMorning4.Enabled = False
                End If
            Case 8
                If currentID = userIDInt Then
                    FrmMain.tabAfternoon1.Enabled = True
                Else
                    FrmMain.tabAfternoon1.Enabled = False
                End If
            Case 9
                If currentID = userIDInt Then
                    FrmMain.tabAfternoon2.Enabled = True
                Else
                    FrmMain.tabAfternoon2.Enabled = False
                End If
            Case 10
                If currentID = userIDInt Then
                    FrmMain.tabAfternoon3.Enabled = True
                Else
                    FrmMain.tabAfternoon3.Enabled = False
                End If
            Case 11
                If currentID = userIDInt Then
                    FrmMain.tabAfternoon4.Enabled = True
                Else
                    FrmMain.tabAfternoon4.Enabled = False
                End If
        End Select
    End Sub

    'TODO hide tabs when it is not time to start a report
    Public Sub HideTabs(tabCounter As Integer())
        Dim hideDebug As Boolean = True
        Console.WriteLine("Hiding tabs")
        Dim Time As Integer() = GetMilitaryTime()
        Dim curTimeSec As Integer

        'Determine current time of day section
        If Time(0) < 8 Then
            curTimeSec = 0 '00:00-7:59, overnight shift
        End If
        If Time(0) >= 8 And Time(0) < 16 Then
            curTimeSec = 1 '8:00-15:59, day shift
        End If
        If Time(0) >= 16 Then
            curTimeSec = 2 '16:00-23:59, evening shift
        End If

        Dim currentTabCount As Integer

        For i As Integer = 0 To 2 Step 1
            If hideDebug Then
                Console.WriteLine("Hiding " + "---" + CStr(i) + "---")
            End If

            currentTabCount = tabCounter(i)
            If userHasTab(i) Then
                currentTabCount = currentTabCount - 1
                Console.WriteLine("User has tab, currentTabCount= " + CStr(currentTabCount))
            End If
            'If Not timeSec.Equals(i) Then
            'currentTabCount = currentTabCount - 1
            'End If
            Console.WriteLine("I value: " + CStr(i) + " currentTabCount=" + CStr(currentTabCount))
            If i = 0 Then

                If currentTabCount = 0 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 3 Night Tabs")
                    End If
                    FrmMain.TabControlOvernight.TabPages.Remove(FrmMain.tabNight2)
                    FrmMain.TabControlOvernight.TabPages.Remove(FrmMain.tabNight3)
                    FrmMain.TabControlOvernight.TabPages.Remove(FrmMain.tabNight4)
                ElseIf currentTabCount = 1 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 2 Night Tabs")
                    End If
                    FrmMain.TabControlOvernight.TabPages.Remove(FrmMain.tabNight3)
                    FrmMain.TabControlOvernight.TabPages.Remove(FrmMain.tabNight4)
                ElseIf currentTabCount = 2 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 1 Night Tabs")
                    End If
                    FrmMain.TabControlOvernight.TabPages.Remove(FrmMain.tabNight4)
                ElseIf currentTabCount = 3 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 0 Night Tabs")
                    End If
                End If

            ElseIf i = 1 Then
                If currentTabCount = 0 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 3 Morning Tabs")
                    End If
                    FrmMain.TabControlMorning.TabPages.Remove(FrmMain.tabMorning2)
                    FrmMain.TabControlMorning.TabPages.Remove(FrmMain.tabMorning3)
                    FrmMain.TabControlMorning.TabPages.Remove(FrmMain.tabMorning4)
                ElseIf currentTabCount = 1 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 2 Morning Tabs")
                    End If
                    FrmMain.TabControlMorning.TabPages.Remove(FrmMain.tabMorning3)
                    FrmMain.TabControlMorning.TabPages.Remove(FrmMain.tabMorning4)
                ElseIf currentTabCount = 2 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 1 Morning Tabs")
                    End If
                    FrmMain.TabControlMorning.TabPages.Remove(FrmMain.tabMorning4)
                ElseIf currentTabCount = 3 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 0 Morning Tabs")
                    End If
                End If

            ElseIf i = 2 Then
                If currentTabCount = 0 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 3 Afternoon Tabs")
                    End If
                    FrmMain.TabControlAfternoon.TabPages.Remove(FrmMain.tabAfternoon2)
                    FrmMain.TabControlAfternoon.TabPages.Remove(FrmMain.tabAfternoon3)
                    FrmMain.TabControlAfternoon.TabPages.Remove(FrmMain.tabAfternoon4)
                ElseIf currentTabCount = 1 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 2 Afternoon Tabs")
                    End If
                    FrmMain.TabControlAfternoon.TabPages.Remove(FrmMain.tabAfternoon3)
                    FrmMain.TabControlAfternoon.TabPages.Remove(FrmMain.tabAfternoon4)
                ElseIf currentTabCount = 2 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 1 Afternoon Tabs")
                    End If
                    FrmMain.TabControlAfternoon.TabPages.Remove(FrmMain.tabAfternoon4)
                ElseIf currentTabCount = 3 Then
                    If hideDebug Then
                        Console.WriteLine("Hiding 0 Afternoon Tabs")
                    End If
                End If
            End If 'closing i for loop
        Next
    End Sub

    Public Sub CheckDirectory(mDY As String())
        Dim testing As String = shiftLogPath + "\" + mDY(2)
        If (Not Directory.Exists(testing)) Then
            Directory.CreateDirectory(testing)
        End If

        testing = testing + "\" + mDY(0)

        If (Not Directory.Exists(testing)) Then
            Directory.CreateDirectory(testing)
        End If

        testing = testing + "\" + mDY(1)

        If (Not Directory.Exists(testing)) Then
            Directory.CreateDirectory(testing)
            '           Dim fs1 As FileStream = File.Create(testing + "\morning.dat")
            '            fs1.Close()
            '            Dim fs2 As FileStream = File.Create(testing + "\evening.dat")
            '            fs2.Close()
            '            Dim fs3 As FileStream = File.Create(testing + "\night.dat")
            '            fs3.Close()
        End If

    End Sub

    Public Sub ChangeTabName(tabID As Integer, userID As Integer)
        Select Case tabID
            Case 0
                FrmMain.tabNight1.Text = CStr(userID) + " " + GetInitials(userID)
            Case 1
                FrmMain.tabNight2.Text = CStr(userID) + " " + GetInitials(userID)
            Case 2
                FrmMain.tabNight3.Text = CStr(userID) + " " + GetInitials(userID)
            Case 3
                FrmMain.tabNight4.Text = CStr(userID) + " " + GetInitials(userID)
            Case 4
                FrmMain.tabMorning1.Text = CStr(userID) + " " + GetInitials(userID)
            Case 5
                FrmMain.tabMorning2.Text = CStr(userID) + " " + GetInitials(userID)
            Case 6
                FrmMain.tabMorning3.Text = CStr(userID) + " " + GetInitials(userID)
            Case 7
                FrmMain.tabMorning4.Text = CStr(userID) + " " + GetInitials(userID)
            Case 8
                FrmMain.tabAfternoon1.Text = CStr(userID) + " " + GetInitials(userID)
            Case 9
                FrmMain.tabAfternoon2.Text = CStr(userID) + " " + GetInitials(userID)
            Case 10
                FrmMain.tabAfternoon3.Text = CStr(userID) + " " + GetInitials(userID)
            Case 11
                FrmMain.tabAfternoon4.Text = CStr(userID) + " " + GetInitials(userID)
        End Select
    End Sub

    Public Sub TxtChangeHandler(timeOfDay As Integer, txt As String, tabName As String, tabID As Integer)
        Console.WriteLine("Tab Name: " + tabName)
        If (tabName IsNot CStr(currentID) + " " + GetInitials(currentID)) Then
            Console.WriteLine("Changing Name")
            ChangeTabName(tabID, currentID)
        End If
        Dim day As String = CType(FrmMain.monthCalendar.SelectionStart.Date, String)
        Dim monthDayYear As String() = day.Split(CType("/", Char()))
        Dim fullFilePath As String = shiftLogPath + monthDayYear(2) + "\" + monthDayYear(0) + "\" + monthDayYear(1) + "\"
        Dim idStr = CStr(currentID)
        queueSave(timeOfDay, currentID, fullFilePath, txt)
    End Sub

    'TODO fix save queue arguments
    'I think i fixed this? not confirmed.
    Public Function WaitToSave(msg As String) As Boolean
        Console.WriteLine("Enquing: " + msg)
        q.Enqueue(msg)
        Dim wait As Boolean
        If q.Peek IsNot msg Then
            Console.WriteLine("Queue is not empty! Waiting paitently...")
            wait = True
        End If
        While wait = True
            If q.Peek Is Nothing Then
                wait = False
            End If
            Threading.Thread.Sleep(tick) : Application.DoEvents()
        End While
        Console.WriteLine("Safely Saving a file")
        Console.WriteLine("Saving file, TOD: " + CStr(TimeOfDay))
        Dim msges As String()
        msges = msg.Split(CChar(","))

        Dim day As String = CType(FrmMain.monthCalendar.SelectionStart.Date, String)
        Dim monthDayYear As String() = day.Split(CType("/", Char()))
        CheckDirectory(monthDayYear)
        Dim file As System.IO.StreamWriter
        Dim strID As String
        If CInt(msges(1)) < 10 Then
            strID = "0" + CStr(CInt(msges(1)))
        Else
            strID = CStr(CInt(msges(1)))
        End If
        Dim doWrite As Boolean = False
        While doWrite = False
            Try
                'file = My.Computer.FileSystem.OpenTextFileWriter(msges(2) + "Morning" + strID + ".dat", False)
                If (CInt(msges(0)) = 1) Then
                    'Console.WriteLine("Saving a morning file: " + msges(3))
                    file = My.Computer.FileSystem.OpenTextFileWriter(msges(2) + "Morning" + strID + ".dat", False)

                ElseIf (CInt(msges(0)) = 2) Then
                    file = My.Computer.FileSystem.OpenTextFileWriter(msges(2) + "Evening" + strID + ".dat", False)

                ElseIf (CInt(msges(0)) = 3) Then
                    file = My.Computer.FileSystem.OpenTextFileWriter(msges(2) + "Night" + strID + ".dat", False)
                Else
                    Console.WriteLine("msges(0) returned the wrong value!")
                End If
                'Console.WriteLine("saving message:" + msges(3))
                file.Write(crypto.EncryptData(msges(3)))

                file.Close()
                doWrite = True
            Catch ex As Exception
                Console.Write("waitin.. ")
                Threading.Thread.Sleep(tick) : Application.DoEvents()
            End Try
        End While




        q.Dequeue()
        Console.WriteLine("Dequed!")
        ' Threading.Thread.Sleep(500)

        saveMsger = New SaveMessageHandler
        saveMsger.Start()
        Return True

    End Function

    Public Function queueSave(timeOfDay As Integer, iD As Integer, filePath As String, text As String) As Boolean
        Dim msg As String
        msg = CStr(timeOfDay) + "," + CStr(iD) + "," + filePath + "," + text
        WaitToSave(msg)
        Console.WriteLine("Enqueued: " + msg)
    End Function

    'set text fields for top of user log tab to current user
    Public Sub UpdateUser()
        Try
            FrmMain.lblName.Text = userList(currentID).GetFirstName & " " & userList(currentID).GetLastName
            If currentID < 10 Then
                FrmMain.lbliD.Text = "0" + CStr(currentID)
            Else
                FrmMain.lbliD.Text = CType(currentID, String)
            End If
        Catch
            MsgBox("invalid user")
        End Try
    End Sub

End Module