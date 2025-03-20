Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Media
Imports Microsoft.Maui.Storage
Imports Microsoft.Maui.Devices
Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media.Imaging

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "photo", "image", "picture")>
    Partial Public Class Camera_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "The camera app is not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            End If
        End Sub

        Private Async Sub TakePhotoButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         ' Check the current camera permission status.
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Camera)()

                                                         ' If permission is not granted, request it.
                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Camera)()
                                                         End If

                                                         ' If permission is granted, capture photo.
                                                         If status = PermissionStatus.Granted Then
                                                             Try
                                                                 If MediaPicker.Default.IsCaptureSupported Then
                                                                     Dim photo As FileResult = Await MediaPicker.Default.CapturePhotoAsync()

                                                                     If photo IsNot Nothing Then
                                                                         FeatureNotAllowedTextBlock.Visibility = Visibility.Collapsed

                                                                         Using stream As Stream = Await photo.OpenReadAsync()
                                                                             Dim bitmapImage As New BitmapImage()
                                                                             bitmapImage.SetSource(stream)
                                                                             PhotoTakenImage.Source = bitmapImage
                                                                             PhotoTakenImage.Visibility = Visibility.Visible
                                                                         End Using
                                                                     End If
                                                                 End If
                                                             Catch ex As PermissionException
                                                                 FeatureNotAllowedTextBlock.Visibility = Visibility.Visible
                                                             Catch ex As Exception
                                                                 MessageBox.Show(ex.Message)
                                                             End Try
                                                         End If
                                                     End Function)
        End Sub

    End Class
End Namespace
