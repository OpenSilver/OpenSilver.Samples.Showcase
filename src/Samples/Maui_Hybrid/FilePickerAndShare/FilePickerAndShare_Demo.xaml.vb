Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.ApplicationModel.DataTransfer
Imports Microsoft.Maui.Devices
Imports Microsoft.Maui.Storage
Imports System.Windows.Media.Imaging
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "storage", "access")>
    Partial Public Class FilePickerAndShare_Demo
        Inherits UserControl

        Private _pickedFileFullPath As String

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "This sample is not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            End If
        End Sub

        Private Async Sub PickImageButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         ' Check the current storage permission status.
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.StorageRead)()

                                                         ' If permission is not granted, request it.
                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.StorageRead)()
                                                         End If

                                                         ' If permission is granted, allow file picking.
                                                         If status = PermissionStatus.Granted Then
                                                             Try
                                                                 Dim options As PickOptions = PickOptions.Images
                                                                 Dim result As FileResult = Await FilePicker.Default.PickAsync(options)

                                                                 If result IsNot Nothing Then
                                                                     _pickedFileFullPath = result.FullPath
                                                                     ShareButton.Visibility = Visibility.Visible
                                                                     PickedImageControl.Visibility = Visibility.Visible
                                                                     FeatureNotAllowedTextBlock.Visibility = Visibility.Collapsed

                                                                     If result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) OrElse
                                                                        result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) Then

                                                                         Using stream As System.IO.Stream = Await result.OpenReadAsync()
                                                                             Dim bitmapImage As New BitmapImage()
                                                                             bitmapImage.SetSource(stream)
                                                                             PickedImageControl.Source = bitmapImage
                                                                         End Using
                                                                     End If
                                                                 End If
                                                             Catch ex As PermissionException
                                                                 FeatureNotAllowedTextBlock.Visibility = Visibility.Visible
                                                             Catch ex As Exception
                                                                 ' The user canceled or something went wrong
                                                             End Try
                                                         End If
                                                     End Function)
        End Sub

        Private Async Sub ShareImageButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         If _pickedFileFullPath IsNot Nothing Then
                                                             Try
                                                                 Await Share.Default.RequestAsync(New ShareFileRequest With {
                                                                     .Title = "Share the picked image",
                                                                     .File = New ShareFile(_pickedFileFullPath)
                                                                 })
                                                             Catch ex As Exception
                                                                 ' The user canceled or something went wrong
                                                             End Try
                                                         Else
                                                             MessageBox.Show("Pick an image first before attempting to share it.")
                                                         End If
                                                     End Function)
        End Sub
    End Class
End Namespace
