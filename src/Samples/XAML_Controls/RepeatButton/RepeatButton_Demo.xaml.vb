Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("input", "repeat", "button", "click", "control")>
    Partial Public Class RepeatButton_Demo
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub ButtonTranslate_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Me.TestTransformBorder.RenderTransform Is Nothing OrElse Not (TypeOf Me.TestTransformBorder.RenderTransform Is TranslateTransform) Then
                Dim translateTransform As TranslateTransform = New TranslateTransform()
                Me.TestTransformBorder.RenderTransform = translateTransform
            End If
            CType(Me.TestTransformBorder.RenderTransform, TranslateTransform).X += 10
            CType(Me.TestTransformBorder.RenderTransform, TranslateTransform).Y += 10
        End Sub

        Private Sub ButtonRotate_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            If Me.TestTransformBorder.RenderTransform Is Nothing OrElse Not (TypeOf Me.TestTransformBorder.RenderTransform Is RotateTransform) Then
                Dim rotateTransform As RotateTransform = New RotateTransform()
                Me.TestTransformBorder.RenderTransform = rotateTransform
            End If
            CType(Me.TestTransformBorder.RenderTransform, RotateTransform).Angle += 10
        End Sub

        Private Sub TransformButton_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim r As Random = New Random()

            Dim brush As SolidColorBrush = New SolidColorBrush()

            brush.Color = Color.FromArgb(r.Next(255), r.Next(255), r.Next(255), r.Next(255))
            Me.TestTransformBorder.Background = brush
        End Sub
    End Class
End Namespace
