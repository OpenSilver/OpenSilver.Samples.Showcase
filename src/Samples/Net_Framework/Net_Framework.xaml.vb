Imports System.Windows
Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Public Partial Class Net_Framework
        Inherits UserControl
        Public Sub New()
            InitializeComponent()

            JSON_SerializerDemo.Visibility = Visibility.Collapsed
            GetRessourceStreamDemo.Visibility = Visibility.Collapsed
            ConsoleDemo.Visibility = Visibility.Collapsed
            RESXDemo.Visibility = Visibility.Collapsed
        End Sub
    End Class
End Namespace
