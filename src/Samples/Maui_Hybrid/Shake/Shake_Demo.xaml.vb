Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Devices
Imports Microsoft.Maui.Devices.Sensors
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "shaking")>
    Partial Public Class Shake_Demo
        Inherits UserControl

        Private shakes As Integer = 0

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "The shake sample is not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            ElseIf Not Accelerometer.Default.IsSupported Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "This device does not support detecting shakes.",
                    .TextWrapping = TextWrapping.Wrap
                })
            End If
        End Sub

        Private Async Sub ShakeToggleButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         ' Check the current sensor permission status.
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Sensors)()

                                                         ' If permission is not granted, request it.
                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Sensors)()
                                                         End If

                                                         ' If permission is granted, enable or disable shake detection.
                                                         If status = PermissionStatus.Granted Then
                                                             If Accelerometer.Default.IsSupported Then
                                                                 If Not Accelerometer.Default.IsMonitoring Then
                                                                     ' Turn on ShakeSensor
                                                                     AddHandler Accelerometer.Default.ShakeDetected, AddressOf ShakeSensor_ReadingChanged
                                                                     Accelerometer.Default.Start(SensorSpeed.Game)
                                                                     ShakeTextBlock.Foreground = New SolidColorBrush(Colors.Green)
                                                                 Else
                                                                     ' Turn off ShakeSensor
                                                                     Accelerometer.Default.Stop()
                                                                     RemoveHandler Accelerometer.Default.ShakeDetected, AddressOf ShakeSensor_ReadingChanged
                                                                     ShakeTextBlock.SetValue(TextBlock.ForegroundProperty, DependencyProperty.UnsetValue)
                                                                 End If
                                                             Else
                                                                 ShakeTextBlock.Text = "The ShakeSensor is not supported on this device."
                                                             End If
                                                         End If
                                                     End Function)
        End Sub

        Private Sub ShakeSensor_ReadingChanged(sender As Object, e As EventArgs)
            ShakeTextBlock.Text = $"{Threading.Interlocked.Increment(shakes)} shakes detected."
        End Sub
    End Class
End Namespace
