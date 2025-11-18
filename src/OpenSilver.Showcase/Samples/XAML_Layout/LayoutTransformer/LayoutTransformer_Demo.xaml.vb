Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Showcase.Search

Namespace OpenSilver.Showcase

    <SearchKeywords("transformation")>
    Partial Public Class LayoutTransformer_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnSliderValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
            layoutTransformer.ApplyLayoutTransform()
        End Sub

    End Class

End Namespace
