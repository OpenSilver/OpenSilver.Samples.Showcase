#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#End If


Namespace PreviewOnWinRT
    Partial Public Class LoginWindow
        Inherits ChildWindow
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub OKButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = True
        End Sub

        Private Sub CancelButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DialogResult = False
        End Sub

        Private Sub LoginWindow_Closing(ByVal sender As Object, ByVal e As ComponentModel.CancelEventArgs)
            If DialogResult.HasValue AndAlso DialogResult.Value = True AndAlso (Equals(Me.nameBox.Text, String.Empty) OrElse Equals(Me.passwordBox.Password, String.Empty)) Then
                e.Cancel = True
                Dim cw As ChildWindow = New ChildWindow()
                cw.Content = "Please Enter your name and password or click Cancel."
                cw.Show()
            End If
        End Sub

        'Public ReadOnly Property NameBox As TextBox
        '    Get
        '        Return Me.nameBoxField
        '    End Get
        'End Property

        'Public ReadOnly Property PasswordBox As PasswordBox
        '    Get
        '        Return Me.passwordBoxField
        '    End Get
        'End Property
    End Class
End Namespace
