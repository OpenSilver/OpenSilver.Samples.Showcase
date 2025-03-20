Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Devices
Imports Microsoft.Maui.Devices.Sensors
Imports OpenSilver.Samples.Showcase.Search

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "pressure", "measure", "sensor", "information")>
    Partial Public Class Barometer_Demo
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "The barometer feature is not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            ElseIf Not Barometer.[Default].IsSupported Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "This device does not support the barometer.",
                    .TextWrapping = TextWrapping.Wrap
                })
            End If
        End Sub

        Private Async Sub BarometerToggleButton_Click(sender As Object, e As System.Windows.RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         ' Check the current location permission status.
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Sensors)()

                                                         ' If permission is not granted, request it.
                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Sensors)()
                                                         End If

                                                         ' If permission is granted, manage the Barometer.
                                                         If status = PermissionStatus.Granted Then
                                                             If Barometer.Default.IsSupported Then
                                                                 If Not Barometer.Default.IsMonitoring Then
                                                                     ' Turn on Barometer
                                                                     AddHandler Barometer.Default.ReadingChanged, AddressOf Barometer_ReadingChanged
                                                                     Barometer.Default.Start(SensorSpeed.UI)
                                                                     BarometerTextBlock.Foreground = New SolidColorBrush(Colors.Green)
                                                                 Else
                                                                     ' Turn off Barometer
                                                                     Barometer.Default.Stop()
                                                                     RemoveHandler Barometer.Default.ReadingChanged, AddressOf Barometer_ReadingChanged
                                                                     BarometerTextBlock.SetValue(TextBlock.ForegroundProperty, DependencyProperty.UnsetValue)
                                                                 End If
                                                             Else
                                                                 BarometerTextBlock.Text = "The barometer is not supported on this device."
                                                             End If
                                                         End If
                                                     End Function)
        End Sub


        Private Sub Barometer_ReadingChanged(ByVal sender As Object, ByVal e As BarometerChangedEventArgs)
            BarometerTextBlock.Text = $"Barometer: {e.Reading}"
        End Sub
    End Class
End Namespace
