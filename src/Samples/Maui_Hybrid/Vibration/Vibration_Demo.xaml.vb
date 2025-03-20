Imports System.Windows
Imports System.Windows.Controls
Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Devices
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "haptic", "feedback")>
    Partial Public Class Vibration_Demo
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "The vibration sample is not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            ElseIf Not Vibration.Default.IsSupported Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "This device does not support vibrations.",
                    .TextWrapping = TextWrapping.Wrap
                })
            End If
        End Sub

        Private Async Sub VibrateButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         ' Check the current vibration permission status.
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Vibrate)()

                                                         ' If permission is not granted, request it.
                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Vibrate)()
                                                         End If

                                                         ' If permission is granted, trigger vibration.
                                                         If status = PermissionStatus.Granted Then
                                                             Try
                                                                 Vibration.Default.Vibrate(New TimeSpan(0, 0, 1))
                                                             Catch ex As FeatureNotSupportedException
                                                                 ' Handle not supported on device exception
                                                             Catch ex As PermissionException
                                                                 ' Handle permission exception
                                                             Catch ex As Exception
                                                                 ' Unable to perform vibration
                                                             End Try
                                                         End If
                                                     End Function)
        End Sub

        Private Async Sub HapticFeedbackButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         ' Check the current vibration permission status.
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Vibrate)()

                                                         ' If permission is not granted, request it.
                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Vibrate)()
                                                         End If

                                                         ' If permission is granted, trigger haptic feedback.
                                                         If status = PermissionStatus.Granted Then
                                                             Try
                                                                 HapticFeedback.Default.Perform(HapticFeedbackType.Click)
                                                             Catch ex As FeatureNotSupportedException
                                                                 ' Handle not supported on device exception
                                                                 MessageBox.Show("Haptic feedback is not supported on this device.")
                                                             Catch ex As PermissionException
                                                                 ' Handle permission exception
                                                             Catch ex As Exception
                                                                 ' Unable to perform haptic feedback
                                                             End Try
                                                         End If
                                                     End Function)
        End Sub
    End Class
End Namespace
