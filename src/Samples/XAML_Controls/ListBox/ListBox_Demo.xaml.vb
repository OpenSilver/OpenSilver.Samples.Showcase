Imports System.Windows.Controls

Namespace Global.OpenSilver.Samples.Showcase
    Partial Public Class ListBox_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

            Me.ListBox1.ItemsSource = Planet.GetListOfPlanets()
        End Sub
    End Class
End Namespace
