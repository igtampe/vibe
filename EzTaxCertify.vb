﻿Imports System.Drawing.Imaging
Imports System.IO

Public Class EzTaxCertify
    Public ServerMSG
    Public ItemName
    Public ItemIncome

    Private Sub Quit_Click(sender As Object, e As EventArgs) Handles DetailsButton.Click
        If Size = MaximumSize Then
            Size = MinimumSize
        Else
            Size = MaximumSize
        End If

    End Sub

    Private Sub EzTaxCertify_Load(sender As Object, e As EventArgs) Handles Me.Load
        PictureBox1.Image = My.Resources.Hourglass
        TitleLBL.Text = "Certifying item"
        SubtitleLBL.Text = "Please wait..."
        OKButton.Enabled = False
        DetailsButton.Enabled = False
        Size = MinimumSize
        ItemName = EZTaxWizard.ItemNameTXB.Text
        ItemIncome = EZTaxWizard.TotalIncome.Text
        BackgroundWorker1.RunWorkerAsync()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Close()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ServerMSG = EZTaxMain.ServerCommand("CERT" & ItemName & " Has a calculated income of " & ItemIncome)

    End Sub

    Private Sub Woopdiedooitsdone() Handles BackgroundWorker1.RunWorkerCompleted

        Select Case ServerMSG
            Case "S"
                TitleLBL.Text = "Item Certified!"
                SubtitleLBL.Text = EZTaxWizard.ItemNameTXB.Text & " Has a calculated income of " & EZTaxWizard.TotalIncome.Text
                OKButton.Enabled = True
                DetailsButton.Enabled = True
                PictureBox1.Image = My.Resources.EzTaxApproved
                DetailsTXB.Text = EZTaxWizard.ItemCompleteDetails
                WaitForRender.RunWorkerAsync()

            Case "E"
                MsgBox("Something happened... I do not know what happened.", MsgBoxStyle.Critical, "Please Help")
                Close()
        End Select

    End Sub

    Private Sub TakeScreenshot()
        Dim bmpScreenShot As Bitmap
        Dim gfxScreenshot As Graphics

        bmpScreenShot = New Bitmap(Width, Height, PixelFormat.Format32bppArgb)

        gfxScreenshot = Graphics.FromImage(bmpScreenShot)
        gfxScreenshot.CopyFromScreen(Location.X, Location.Y, 0, 0, Size, CopyPixelOperation.SourceCopy)
        If File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp & "\ViBEScrSHT.png") Then File.Delete(My.Computer.FileSystem.SpecialDirectories.Temp & "\ViBEScrSHT.png")
        bmpScreenShot.Save(My.Computer.FileSystem.SpecialDirectories.Temp & "\ViBEScrSHT.png", ImageFormat.Png)

        Dim Coso As Image
        Coso = Image.FromFile(My.Computer.FileSystem.SpecialDirectories.Temp & "\ViBEScrSHT.png")
        My.Computer.Clipboard.SetImage(Coso)
        Coso.Dispose()
        ClipboardNotice.Show()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        TakeScreenshot()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim bmpScreenShot As Bitmap
        Dim gfxScreenshot As Graphics

        bmpScreenShot = New Bitmap(Width, Height, PixelFormat.Format32bppArgb)

        gfxScreenshot = Graphics.FromImage(bmpScreenShot)
        gfxScreenshot.CopyFromScreen(Location.X, Location.Y, 0, 0, Size, CopyPixelOperation.SourceCopy)
        If File.Exists(My.Computer.FileSystem.SpecialDirectories.Temp & "\ViBEScrSHT.png") Then File.Delete(My.Computer.FileSystem.SpecialDirectories.Temp & "\ViBEScrSHT.png")

        Dim FD As SaveFileDialog = New SaveFileDialog()
        FD.Title = "Save As"
        FD.AddExtension = True
        FD.AutoUpgradeEnabled = True
        FD.CheckFileExists = False
        FD.CheckPathExists = True
        FD.DefaultExt = ".png"
        FD.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyPictures
        FD.OverwritePrompt = True
        FD.SupportMultiDottedExtensions = False
        FD.ValidateNames = True
        FD.FileName = "Doot.png"

        If FD.ShowDialog() = DialogResult.OK Then
            bmpScreenShot.Save(FD.FileName, ImageFormat.Png)
        Else
            'ok no
        End If



    End Sub

    Private Sub WaitForRender_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles WaitForRender.DoWork
        Threading.Thread.Sleep(50)
    End Sub

    Sub OKTakeTheScreenshotpls() Handles WaitForRender.RunWorkerCompleted
        TakeScreenshot()
    End Sub

End Class