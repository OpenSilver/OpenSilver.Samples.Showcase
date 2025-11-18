Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports OpenSilver.Showcase.Search

Namespace OpenSilver.Showcase

    <SearchKeywords("clip")>
    Partial Public Class Clipping_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            AddHandler clipCheckBox.Checked, AddressOf OnClipCheckBoxStateChanged
            AddHandler clipCheckBox.Unchecked, AddressOf OnClipCheckBoxStateChanged
        End Sub

        Private Sub OnClipCheckBoxStateChanged(sender As Object, e As RoutedEventArgs)
            If clipCheckBox.IsChecked = True Then
                button.Clip = New EllipseGeometry With {
                    .Center = New Point(70, 30),
                    .RadiusX = 70,
                    .RadiusY = 30
                }
            Else
                button.Clip = Nothing
            End If
        End Sub

    End Class

End Namespace
