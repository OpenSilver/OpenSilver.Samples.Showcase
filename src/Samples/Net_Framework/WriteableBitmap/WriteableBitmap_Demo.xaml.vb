Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.Windows.Controls
Imports System.Windows.Media.Imaging

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("bitmap", "image", "graphics", "drawing", "rendering")>
    Partial Public Class WriteableBitmap_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
            ivDestination.Source = Nothing
        End Sub

        Private Async Sub MirrorButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
            Dim bitmap = New WriteableBitmap(200, 200)
            Await bitmap.RenderAsync(ivSource, Nothing) 'TODO Change to default(_) if this is not a reference type
            'Let's modify pixels:
            Array.Reverse(bitmap.Pixels)
            bitmap.Invalidate() 'Invalidate once Pixels are manipulated:
            ivDestination.Source = bitmap
        End Sub
    End Class
End Namespace
