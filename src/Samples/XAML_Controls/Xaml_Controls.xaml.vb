Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class Xaml_Controls
        Inherits UserControl
        Public Sub New()
            InitializeComponent()

            NonModalChildWindow.Visibility = Visibility.Collapsed
            ScrollBarDemo.Visibility = Visibility.Collapsed
            ThumbDemo.Visibility = Visibility.Collapsed
        End Sub
    End Class
End Namespace
