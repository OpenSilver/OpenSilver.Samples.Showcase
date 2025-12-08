Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Showcase.Search

Namespace OpenSilver.Showcase

    <SearchKeywords("position", "relative")>
    Partial Public Class TransformToVisual_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            AddHandler Loaded, AddressOf OnLoaded
            AddHandler Application.Current.MainWindow.SizeChanged, Sub(s, e) CalculatePosition()
        End Sub

        Private Sub OnLoaded(sender As Object, e As RoutedEventArgs)
            AddHandler TryCast(Parent, FrameworkElement).LayoutUpdated, Sub(s, args) CalculatePosition()
        End Sub

        Private Sub CalculatePosition()
            Dim transform = TransformToVisual(Application.Current.MainWindow)
            Dim topLeft = transform.Transform(New Point())

            resultTextBlock.Text = $"X: {topLeft.X:N1}  Y: {topLeft.Y:N1}"
        End Sub

    End Class

End Namespace
