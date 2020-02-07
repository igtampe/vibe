﻿Imports VIBE__But_on_Visual_Studio_.ServerCommand

''' <summary>
''' The Bank ServerCommand Set
''' </summary>
Public Class BankCommands

    'BNKA57174GBANK

    ''' <summary>
    ''' Closes the specified bank of the Currently logged in user
    ''' </summary>
    ''' <param name="Bank"></param>
    ''' <returns>S for Success, E For Error</returns>
    Public Shared Function CloseBank(Bank As String)
        Return RawCommand("BNK" & "C" & VibeMainScreen.ID & Bank)
    End Function

    ''' <summary>
    ''' Opens an account on the specified bank for the currently logged in user
    ''' </summary>
    ''' <param name="Bank"></param>
    ''' <returns>S for Success, E For Error</returns>
    Public Shared Function OpenBank(Bank As String)
        Return RawCommand("BNK" & "O" & VibeMainScreen.ID & Bank)
    End Function

    ''' <summary>
    ''' Retrieves the Log of the Currently logged in user on the specified bank
    ''' </summary>
    ''' <param name="Bank"></param>
    ''' <returns>S for success, or E for Error. Log is ready for download if S is received</returns>
    Public Shared Function BankLog(Bank As String)
        Return RawCommand("BNK" & "L" & VibeMainScreen.ID & Bank)
    End Function

End Class