﻿''' <summary> Handles ServerCommands for Checkbook 2000</summary>
Public Module CheckbookCommands

    ''' <summary>Read All Checks from the specified user</summary>
    Public Function ReadChecks(ID As String) As String
        'CHCKBKREAD57174
        Return RawCommand("CHCKBKREAD" + ID)
    End Function

    ''' <summary>Removes a Check</summary>
    ''' <param name="ID">ID of the user</param>
    ''' <param name="Index">Check you want to remove</param>
    Public Function RemoCheck(ID As String, Index As Integer) As String
        'CHKBKREMO5717410
        Return RawCommand("CHCKBKREMO" & ID & Index)
    End Function

    ''' <summary>Adds a Check</summary>
    Public Function AddCheck(Destination As String, Type As Integer, Time As String, Name As String, Bank As String, Amount As Long, Subtype As Integer, Comment As String) As String
        Return RawCommand("CHCKBKADD" & Destination & Type & "`" & Time & "`" & Name & "`" & Bank & "`" & Amount & "`::" & Subtype & "::" & Comment)
    End Function

End Module
