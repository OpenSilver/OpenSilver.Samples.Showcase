Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Devices
Imports OpenSilver.Samples.Showcase.Search
Imports System
Imports System.Windows
Imports System.Windows.Controls

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native")>
    Partial Public Class Flashlight_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "The flashlight is not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            Else
                MainThread.InvokeOnMainThreadAsync(Async Function()
                                                       If Not Await Flashlight.Default.IsSupportedAsync() Then
                                                           FlashlightButton.Visibility = Visibility.Collapsed
                                                           UnsupportedTextBlock.Text = "The flashlight is not supported on this device."
                                                           UnsupportedTextBlock.Visibility = Visibility.Visible
                                                       End If
                                                   End Function)
            End If
        End Sub

        Private Async Sub SwitchButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         ' Check the current flashlight permission status.
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Flashlight)()

                                                         ' If permission is not granted, request it.
                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Flashlight)()
                                                         End If

                                                         ' If permission is granted, toggle the flashlight.
                                                         If status = PermissionStatus.Granted Then
                                                             Try
                                                                 If FlashlightButton.IsChecked = True Then
                                                                     Await Flashlight.Default.TurnOnAsync()
                                                                 Else
                                                                     Await Flashlight.Default.TurnOffAsync()
                                                                 End If
                                                             Catch ex As PermissionException
                                                                 UnsupportedTextBlock.Text = "This sample requires your permission to use the flashlight."
                                                                 UnsupportedTextBlock.Visibility = Visibility.Visible
                                                             Catch ex As Exception
                                                                 ' Unable to turn on/off flashlight
                                                             End Try
                                                         End If
                                                     End Function)
        End Sub
    End Class
End Namespace
