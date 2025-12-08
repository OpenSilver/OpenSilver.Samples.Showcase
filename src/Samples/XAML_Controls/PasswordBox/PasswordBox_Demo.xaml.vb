Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("input", "password", "text", "entry", "form")>
    Partial Public Class PasswordBox_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            DisplayPasswordIfNotEmpty()
        End Sub

        Private Sub DisplayPasswordIfNotEmpty()
            If Me.PasswordBox.Password.Length <> 0 Then
                MessageBox.Show("The password typed is " & Microsoft.VisualBasic.Constants.vbLf & """" & Me.PasswordBox.Password & """")
            Else
                MessageBox.Show("Please enter a password")
            End If
        End Sub
    End Class
End Namespace
