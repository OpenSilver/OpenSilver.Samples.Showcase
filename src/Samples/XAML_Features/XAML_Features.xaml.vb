Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class Xaml_Features
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
#If OPENSILVER Then
            'Binding1Demo.Visibility = Visibility.Collapsed;
#End If
            Me.MarkupExtensionsDemo.Visibility = Visibility.Collapsed
        End Sub
    End Class
End Namespace
