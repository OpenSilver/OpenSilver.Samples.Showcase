Imports System.Windows
Imports System.Windows.Controls
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase

    <SearchKeywords("transformation")>
    Partial Public Class LayoutTransformer_Demo
        Inherits ContentControl

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnSliderValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double))
            layoutTransformer.ApplyLayoutTransform()
        End Sub

    End Class

End Namespace
