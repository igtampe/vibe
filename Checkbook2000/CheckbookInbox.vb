﻿''' <summary>Displays and manages a Checkbook inbox</summary>
Public Class CheckbookInbox

    '--------------------------------[Variables]--------------------------------

    Private ReadOnly MyUser As User
    Public Inbox() As CheckbookMain.InboxItem
    Private BWError As String

    '--------------------------------[Initialization]--------------------------------

    Public Sub New(User As User, Inbox As CheckbookMain.InboxItem())
        InitializeComponent()
        MyUser = User
        Me.Inbox = Inbox

        UMSNBRButton.Enabled = User.UMSNB
        GBANKRbutton.Enabled = User.GBANK
        RIVERRButton.Enabled = User.RIVER

    End Sub

    Private Sub LoadingTime() Handles Me.Load
        Populatelistview()
    End Sub

    Private Sub Populatelistview()
        UpdateGraphic(-1)
        ActionBTN.Enabled = False
        DELETTHIS.Enabled = False


        ListView1.Items.Clear()
        ListView1.MultiSelect = False
        ListView1.FullRowSelect = True
        ListView1.HideSelection = False

        Dim itemtype As String

        For I = 0 To Inbox.Count - 1

            Dim CLVI As ListViewItem
            CLVI = New ListViewItem With {.Text = Inbox(I).Time}

            Select Case Inbox(I).Type
                Case 0
                    itemtype = "Check"
                Case 1
                    itemtype = "Bill"
                Case Else
                    itemtype = "Unknown"
            End Select

            CLVI.SubItems.Add(itemtype)
            CLVI.SubItems.Add(Inbox(I).FromName)
            CLVI.SubItems.Add(Inbox(I).Amount.ToString("N0") & "p")
            ListView1.Items.Add(CLVI)

        Next
    End Sub

    '--------------------------------[Buttons]--------------------------------

    Private Sub SelectedChanged() Handles ListView1.SelectedIndexChanged
        Dim I As Integer

        Try
            I = ListView1.SelectedIndices(0)
        Catch
            ActionBTN.Enabled = False
            DELETTHIS.Enabled = False
            Exit Sub
        End Try

        UpdateGraphic(-1)

        Select Case Inbox(I).Type
            Case 0
                UpdateGraphic(Inbox(I).Type, Inbox(I).Subtype)
                CheckAmount.Text = Inbox(I).Amount.ToString("N0") & "p"
                CheckWordAmount.Text = NumberToText(Inbox(I).Amount) & " Pecunia"
                CheckComment.Text = Inbox(I).Comment
                CheckDate.Text = Inbox(I).Time
                CheckName.Text = Inbox(I).FromName
                CheckFrom.Text = Inbox(I).FromBank
                CheckTo.Text = VibeMainScreen.NameLabel.Text
                ActionBTN.Text = "Cash this check"
            Case 1
                UpdateGraphic(Inbox(I).Type, Inbox(I).Subtype)
                BillAmount.Text = Inbox(I).Amount.ToString("N0") & "p"
                BillComment.Text = Inbox(I).Comment
                BillDate.Text = Inbox(I).Time
                BillFrom.Text = Inbox(I).FromBank
                BillName.Text = Inbox(I).FromName
                ActionBTN.Text = "Pay this bill"
            Case Else
                'uh do nothing
                Exit Sub
        End Select

        ActionBTN.Enabled = True
        DELETTHIS.Enabled = True


    End Sub

    Private Sub RemoveItem() Handles DELETTHIS.Click
        Dim servermsg = RemoCheck(MyUser.ID, ListView1.SelectedIndices(0))
        Select Case servermsg
            Case "E"
                MsgBox("A server side error has occurred. Contact CHOPO!", vbExclamation)

            Case "N"
                MsgBox("There's No Check File.", vbInformation)
                Close()
            Case "S"
                If Inbox.Count = 1 Then
                    Inbox = Nothing
                    Close()
                End If
                RefreshNotice.Show()
                BackgroundWorker1.RunWorkerAsync()
        End Select
    End Sub

    Private Sub ActionBTN_Click(sender As Object, e As EventArgs) Handles ActionBTN.Click
        Dim I As Integer
        I = ListView1.SelectedIndices(0)
        Dim TheUser As String
        If UMSNBRButton.Checked Then
            TheUser = MyUser.ID & "\UMSNB"
        ElseIf GBANKRbutton.Checked Then
            TheUser = MyUser.ID & "\GBANK"
        ElseIf RIVERRButton.Checked Then
            TheUser = MyUser.ID & "\RIVER"
        Else
            MsgBox("Please Select a Bank to apply the check to, or to pay the bill from", MsgBoxStyle.Critical)
            Exit Sub
        End If

        Dim TheSender As String
        Dim Amount As Long
        Dim ServerMSG As String


        Amount = Inbox(I).Amount
        TheSender = Inbox(I).FromBank

        Select Case Inbox(I).Type
            Case 0
                ServerMSG = SM(TheSender, TheUser, Amount)
                Select Case ServerMSG
                    Case "1"
                        MsgBox("Improperly Coded Vibing Request", vbInformation, "Transfer unsuccessful")
                    Case "E"
                        MsgBox("The transaction could not be completed. The check may have bounced", vbInformation, "Transfer unsuccessful")
                    Case "S"
                        MsgBox("Successfully cashed the check through ViBE.", vbInformation, "Transfer Successful")
                        RemoveItem()
                End Select
            Case 1
                ServerMSG = SM(TheUser, TheSender, Amount)
                Select Case ServerMSG
                    Case "1"
                        MsgBox("Improperly Coded Vibing Request", vbInformation, "Transfer unsuccessful")
                    Case "E"
                        MsgBox("The transaction could not be completed. Do you have enough funds?", vbInformation, "Transfer unsuccessful")
                    Case "S"
                        MsgBox("The bill was successfully paid using ViBE.", vbInformation, "Transfer Successful")
                        RemoveItem()
                End Select
            Case Else
                MsgBox("Unknown transaction type", vbInformation, "Transfer unsuccessful")
        End Select





    End Sub

    '--------------------------------[Background Worker]--------------------------------

    Private Sub UpdateInbox() Handles BackgroundWorker1.DoWork
        BWError = "ono"
        Dim Servermsg = ReadChecks(MyUser.ID)
        If Servermsg = "N" Or Servermsg = "E" Or Servermsg = "F" Then
            BWError = Servermsg
            Exit Sub
        End If

        Dim Doot() As String = Servermsg.Split("`")

        Dim I As Integer = -1
        Dim N As Integer = 0

        Do
            ReDim Preserve Inbox(N)
            I += 1
            Inbox(N).Type = Doot(I)
            I += 1
            Inbox(N).Time = Doot(I)
            I += 1
            Inbox(N).FromName = Doot(I)
            I += 1
            Inbox(N).FromBank = Doot(I)
            I += 1
            Inbox(N).Amount = Doot(I)
            I += 1
            Inbox(N).Comment = Doot(I)
            N += 1
            If I = Doot.Count - 1 Then Exit Do
        Loop


    End Sub

    Private Sub DoneUpdatingInbox() Handles BackgroundWorker1.RunWorkerCompleted
        If BWError = "N" Then
            Exit Sub
        ElseIf BWError = "F" Then
            Close()
            Exit Sub
        ElseIf BWError = "E" Then
            MsgBox("A server side error has occurred. Contact CHOPO!", vbExclamation)
            Exit Sub
        End If

        Populatelistview()

        RefreshNotice.Close()

    End Sub

    '--------------------------------[Other Functions]--------------------------------

    Private Function NumberToText(ByVal n As Integer) As String

        Select Case n
            Case 0
                Return ""

            Case 1 To 19
                Dim arr() As String = {"One", "Two", "Three", "Four", "Five", "Six", "Seven",
                  "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen",
                    "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"}
                Return arr(n - 1) & " "

            Case 20 To 99
                Dim arr() As String = {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"}
                Return arr(n \ 10 - 2) & " " & NumberToText(n Mod 10)

            Case 100 To 199
                Return "One Hundred " & NumberToText(n Mod 100)

            Case 200 To 999
                Return NumberToText(n \ 100) & "Hundred " & NumberToText(n Mod 100)

            Case 1000 To 1999
                Return "One Thousand " & NumberToText(n Mod 1000)

            Case 2000 To 999999
                Return NumberToText(n \ 1000) & "Thousand " & NumberToText(n Mod 1000)

            Case 1000000 To 1999999
                Return "One Million " & NumberToText(n Mod 1000000)

            Case 1000000 To 999999999
                Return NumberToText(n \ 1000000) & "Millions " & NumberToText(n Mod 1000000)

            Case 1000000000 To 1999999999
                Return "One Billion " & NumberToText(n Mod 1000000000)

            Case Else
                Return NumberToText(n \ 1000000000) & "Billion " _
                  & NumberToText(n Mod 1000000000)
        End Select
    End Function

    Private Sub ChangeCheckLabelBGTo(ByVal NewColor As Color)
        CheckComment.BackColor = NewColor
        CheckDate.BackColor = NewColor
        CheckName.BackColor = NewColor
        CheckTo.BackColor = NewColor
        CheckFrom.BackColor = NewColor
        CheckWordAmount.BackColor = NewColor
    End Sub

    Private Sub ChangeCheckLabelFGTo(ByVal NewColor As Color)
        CheckAmount.ForeColor = NewColor
        CheckComment.ForeColor = NewColor
        CheckDate.ForeColor = NewColor
        CheckName.ForeColor = NewColor
        CheckTo.ForeColor = NewColor
        CheckFrom.ForeColor = NewColor
        CheckWordAmount.ForeColor = NewColor
    End Sub

    Private Sub UpdateGraphic(type As Integer, Optional Subtype As Integer = 0)
        Dim status As Boolean = True
        Select Case type
            Case 0
                'check
                CheckAmount.Visible = status
                CheckComment.Visible = status
                CheckDate.Visible = status
                CheckName.Visible = status
                CheckTo.Visible = status
                CheckFrom.Visible = status
                CheckWordAmount.Visible = status

                Dim DefaultColor As Color = Color.FromArgb(219, 241, 245)
                Dim GrayColor As Color = Color.FromArgb(235, 235, 235)
                Dim YellowColor As Color = Color.FromArgb(255, 255, 182)
                Dim PurpleColor As Color = Color.FromArgb(252, 229, 255)
                Dim RedColor As Color = Color.FromArgb(255, 233, 231)
                Dim GreenColor As Color = Color.FromArgb(178, 255, 192)
                Dim BlackColor As Color = Color.FromArgb(20, 20, 20)


                Select Case Subtype
                    Case 0
                        'Default
                        PictureBox1.Image = My.Resources.Check
                        ChangeCheckLabelBGTo(DefaultColor)
                        CheckAmount.BackColor = Color.FromArgb(235, 245, 247)
                        ChangeCheckLabelFGTo(Color.Black)

                    Case 1
                        'Gray
                        PictureBox1.Image = My.Resources.CheckGray
                        ChangeCheckLabelBGTo(GrayColor)
                        CheckAmount.BackColor = Color.FromArgb(243, 243, 243)
                        ChangeCheckLabelFGTo(Color.Black)

                    Case 2
                        'Green
                        PictureBox1.Image = My.Resources.CheckGreen
                        ChangeCheckLabelBGTo(GreenColor)
                        CheckAmount.BackColor = Color.FromArgb(194, 255, 194)
                        ChangeCheckLabelFGTo(Color.Black)

                    Case 3
                        'Purple
                        PictureBox1.Image = My.Resources.CheckPurple
                        ChangeCheckLabelBGTo(PurpleColor)
                        CheckAmount.BackColor = Color.FromArgb(255, 233, 255)
                        ChangeCheckLabelFGTo(Color.Black)

                    Case 4
                        'Red
                        PictureBox1.Image = My.Resources.CheckRed
                        ChangeCheckLabelBGTo(RedColor)
                        CheckAmount.BackColor = Color.FromArgb(255, 239, 233)
                        ChangeCheckLabelFGTo(Color.Black)

                    Case 5
                        'Yellow
                        PictureBox1.Image = My.Resources.CheckYellow
                        ChangeCheckLabelBGTo(YellowColor)
                        CheckAmount.BackColor = Color.FromArgb(255, 255, 184)
                        ChangeCheckLabelFGTo(Color.Black)

                    Case 6
                        'Black
                        PictureBox1.Image = My.Resources.CheckBlack
                        ChangeCheckLabelBGTo(BlackColor)
                        CheckAmount.BackColor = Color.FromArgb(12, 12, 12)
                        ChangeCheckLabelFGTo(Color.White)

                    Case Else
                        'Default
                        PictureBox1.Image = My.Resources.Check
                        ChangeCheckLabelBGTo(DefaultColor)
                        CheckAmount.BackColor = Color.FromArgb(235, 245, 247)
                        ChangeCheckLabelFGTo(Color.Black)

                End Select

            Case 1
                BillAmount.Visible = status
                BillComment.Visible = status
                BillDate.Visible = status
                BillFrom.Visible = status
                BillName.Visible = status

                'Future Color Definitions

                Select Case Subtype

                    Case Else
                        'Default
                        PictureBox1.Image = My.Resources.Bill

                End Select



            Case Else

                PictureBox1.Image = Nothing
                status = False

                BillAmount.Visible = status
                BillComment.Visible = status
                BillDate.Visible = status
                BillFrom.Visible = status
                BillName.Visible = status
                CheckAmount.Visible = status
                CheckComment.Visible = status
                CheckDate.Visible = status
                CheckName.Visible = status
                CheckTo.Visible = status
                CheckFrom.Visible = status
                CheckWordAmount.Visible = status


        End Select
    End Sub


End Class