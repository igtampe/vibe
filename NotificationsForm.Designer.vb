﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotificationsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem(New String() {"12/4/2018 11:32:25 AM", "57174\UMSNB Sent 2p to your GBANK account"}, -1)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NotificationsForm))
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RemoveNotifBTN = New System.Windows.Forms.Button()
        Me.OKBTN = New System.Windows.Forms.Button()
        Me.GetNotificationsBW = New System.ComponentModel.BackgroundWorker()
        Me.ClearNotifsBTN = New System.Windows.Forms.Button()
        Me.RemoveNotificationBW = New System.ComponentModel.BackgroundWorker()
        Me.ClearAllNotificationsBW = New System.ComponentModel.BackgroundWorker()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.ListView1.Location = New System.Drawing.Point(12, 37)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(454, 254)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Date and Time"
        Me.ColumnHeader1.Width = 132
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Transaction"
        Me.ColumnHeader2.Width = 318
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(150, 25)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Notifications:"
        '
        'RemoveNotifBTN
        '
        Me.RemoveNotifBTN.Location = New System.Drawing.Point(310, 297)
        Me.RemoveNotifBTN.Name = "RemoveNotifBTN"
        Me.RemoveNotifBTN.Size = New System.Drawing.Size(75, 23)
        Me.RemoveNotifBTN.TabIndex = 2
        Me.RemoveNotifBTN.Text = "Remove"
        Me.RemoveNotifBTN.UseVisualStyleBackColor = True
        '
        'OKBTN
        '
        Me.OKBTN.Location = New System.Drawing.Point(391, 297)
        Me.OKBTN.Name = "OKBTN"
        Me.OKBTN.Size = New System.Drawing.Size(75, 23)
        Me.OKBTN.TabIndex = 3
        Me.OKBTN.Text = "OK"
        Me.OKBTN.UseVisualStyleBackColor = True
        '
        'GetNotificationsBW
        '
        '
        'ClearNotifsBTN
        '
        Me.ClearNotifsBTN.Location = New System.Drawing.Point(12, 297)
        Me.ClearNotifsBTN.Name = "ClearNotifsBTN"
        Me.ClearNotifsBTN.Size = New System.Drawing.Size(147, 23)
        Me.ClearNotifsBTN.TabIndex = 4
        Me.ClearNotifsBTN.Text = "Clear All Notifications"
        Me.ClearNotifsBTN.UseVisualStyleBackColor = True
        '
        'RemoveNotificationBW
        '
        '
        'ClearAllNotificationsBW
        '
        '
        'NotificationsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(478, 329)
        Me.Controls.Add(Me.ClearNotifsBTN)
        Me.Controls.Add(Me.OKBTN)
        Me.Controls.Add(Me.RemoveNotifBTN)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NotificationsForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Notifications"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ListView1 As ListView
    Friend WithEvents Label1 As Label
    Friend WithEvents RemoveNotifBTN As Button
    Friend WithEvents OKBTN As Button
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents GetNotificationsBW As System.ComponentModel.BackgroundWorker
    Friend WithEvents ClearNotifsBTN As Button
    Friend WithEvents RemoveNotificationBW As System.ComponentModel.BackgroundWorker
    Friend WithEvents ClearAllNotificationsBW As System.ComponentModel.BackgroundWorker
End Class
