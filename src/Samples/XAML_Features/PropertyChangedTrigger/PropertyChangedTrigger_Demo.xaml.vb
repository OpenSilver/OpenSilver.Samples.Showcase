Imports System.Collections.Generic

#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
#End If
Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class PropertyChangedTrigger_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Me.borderText.Text = If(Equals(Me.borderText.Text, "Yellow"), "Orange", "Yellow")
        End Sub
    End Class
End Namespace
