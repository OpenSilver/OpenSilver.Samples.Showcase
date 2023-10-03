Imports System
Imports System.Collections.Generic
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
#Else
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.Showcase
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

        Private Sub ButtonViewSource_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Call ViewSource(New List(Of ViewSourceButtonInfo)() From {
                    New ViewSourceButtonInfo() With {
        .TabHeader = "RepeatButton_Demo.xaml",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/RepeatButton/RepeatButton_Demo.xaml"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "RepeatButton_Demo.xaml.cs",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/RepeatButton/RepeatButton_Demo.xaml.cs"
    },
                    New ViewSourceButtonInfo() With {
        .TabHeader = "RepeatButton_Demo.xaml.vb",
        .FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/RepeatButton/RepeatButton_Demo.xaml.vb"
    }
})
        End Sub

    End Class
End Namespace
