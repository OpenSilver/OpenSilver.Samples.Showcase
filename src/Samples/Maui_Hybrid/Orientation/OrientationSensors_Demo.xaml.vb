Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports Microsoft.Maui.ApplicationModel
Imports Microsoft.Maui.Devices
Imports Microsoft.Maui.Devices.Sensors
Imports OpenSilver.Samples.Showcase.Search
Imports CSHTML5.Internal

Namespace OpenSilver.Samples.Showcase
    <SearchKeywords("maui", "hybrid", "device", "native", "sensor", "information", "compass", "accelerometer", "gyroscope", "magnetometer", "orientation")>
    Partial Public Class OrientationSensors_Demo
        Inherits UserControl

        Private queueHandler As New INTERNAL_DispatcherQueueHandler()

        Public Sub New()
            InitializeComponent()

            If DeviceInfo.Current.Platform = DevicePlatform.Unknown Then
                SampleContainer.Children.Clear()
                SampleContainer.Children.Add(New TextBlock() With {
                    .Text = "These samples are not supported in the browser.",
                    .TextWrapping = TextWrapping.Wrap
                })
            Else
                Dim unsupportedFeatures As New List(Of String)()
                If Not Compass.Default.IsSupported Then
                    CompassContainer.Visibility = Visibility.Collapsed
                    unsupportedFeatures.Add("Compass")
                End If
                If Not Accelerometer.Default.IsSupported Then
                    AccelerometerContainer.Visibility = Visibility.Collapsed
                    unsupportedFeatures.Add("Accelerometer")
                End If
                If Not Gyroscope.Default.IsSupported Then
                    GyroscopeContainer.Visibility = Visibility.Collapsed
                    unsupportedFeatures.Add("Gyroscope")
                End If
                If Not Magnetometer.Default.IsSupported Then
                    MagnetometerContainer.Visibility = Visibility.Collapsed
                    unsupportedFeatures.Add("Magnetometer")
                End If
                If Not OrientationSensor.Default.IsSupported Then
                    OrientationContainer.Visibility = Visibility.Collapsed
                    unsupportedFeatures.Add("Orientation sensor")
                End If

                If unsupportedFeatures.Any() Then
                    UnsupportedTextBlock.Text = $"The following sections of this have been hidden because this device does not support them: {vbCrLf} - {String.Join($",{vbCrLf} - ", unsupportedFeatures)}."
                    UnsupportedTextBlock.Visibility = Visibility.Visible
                End If
            End If
        End Sub

#Region "Compass"
        Private Async Sub CompassToggleButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Sensors)()

                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Sensors)()
                                                         End If

                                                         If status = PermissionStatus.Granted Then
                                                             If Compass.Default.IsSupported Then
                                                                 If Not Compass.Default.IsMonitoring Then
                                                                     AddHandler Compass.Default.ReadingChanged, AddressOf Compass_ReadingChanged
                                                                     Compass.Default.Start(SensorSpeed.UI)
                                                                     TextBlock.SetForeground(CompassPath, New SolidColorBrush(Colors.Green))
                                                                 Else
                                                                     Compass.Default.Stop()
                                                                     RemoveHandler Compass.Default.ReadingChanged, AddressOf Compass_ReadingChanged
                                                                     CompassPath.SetValue(TextBlock.ForegroundProperty, DependencyProperty.UnsetValue)
                                                                 End If
                                                             End If
                                                         End If
                                                     End Function)
        End Sub

        Private Sub Compass_ReadingChanged(sender As Object, e As CompassChangedEventArgs)
            CompassTransform.Angle = -e.Reading.HeadingMagneticNorth
        End Sub
#End Region

#Region "Accelerometer"
        Private Async Sub AccelerometerToggleButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Sensors)()

                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Sensors)()
                                                         End If

                                                         If status = PermissionStatus.Granted Then
                                                             If Accelerometer.Default.IsSupported Then
                                                                 If Not Accelerometer.Default.IsMonitoring Then
                                                                     AddHandler Accelerometer.Default.ReadingChanged, AddressOf Accelerometer_ReadingChanged
                                                                     Accelerometer.Default.Start(SensorSpeed.UI)
                                                                 Else
                                                                     Accelerometer.Default.Stop()
                                                                     RemoveHandler Accelerometer.Default.ReadingChanged, AddressOf Accelerometer_ReadingChanged
                                                                 End If
                                                             End If
                                                         End If
                                                     End Function)
        End Sub

        Private Sub Accelerometer_ReadingChanged(sender As Object, e As AccelerometerChangedEventArgs)
            Dim acceleration = e.Reading.Acceleration
            AccelX.Text = $"X: {acceleration.X}G"
            AccelY.Text = $"Y: {acceleration.Y}G"
            AccelZ.Text = $"Z: {acceleration.Z}G"
        End Sub
#End Region

#Region "Gyroscope"
        Private Async Sub GyroscopeToggleButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Sensors)()

                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Sensors)()
                                                         End If

                                                         If status = PermissionStatus.Granted Then
                                                             If Gyroscope.Default.IsSupported Then
                                                                 If Not Gyroscope.Default.IsMonitoring Then
                                                                     AddHandler Gyroscope.Default.ReadingChanged, AddressOf Gyroscope_ReadingChanged
                                                                     Gyroscope.Default.Start(SensorSpeed.UI)
                                                                 Else
                                                                     Gyroscope.Default.Stop()
                                                                     RemoveHandler Gyroscope.Default.ReadingChanged, AddressOf Gyroscope_ReadingChanged
                                                                 End If
                                                             End If
                                                         End If
                                                     End Function)
        End Sub

        Private Sub Gyroscope_ReadingChanged(sender As Object, e As GyroscopeChangedEventArgs)
            Dim velocity = e.Reading.AngularVelocity
            GyroX.Text = $"X: {velocity.X}rad/s"
            GyroY.Text = $"Y: {velocity.Y}rad/s"
            GyroZ.Text = $"Z: {velocity.Z}rad/s"
        End Sub
#End Region

#Region "Magnetometer"
        Private Async Sub MagnetometerToggleButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         Dim status As PermissionStatus = Await Permissions.CheckStatusAsync(Of Permissions.Sensors)()

                                                         If status <> PermissionStatus.Granted Then
                                                             status = Await Permissions.RequestAsync(Of Permissions.Sensors)()
                                                         End If

                                                         If status = PermissionStatus.Granted Then
                                                             If Magnetometer.Default.IsSupported Then
                                                                 If Not Magnetometer.Default.IsMonitoring Then
                                                                     AddHandler Magnetometer.Default.ReadingChanged, AddressOf Magnetometer_ReadingChanged
                                                                     Magnetometer.Default.Start(SensorSpeed.Default)
                                                                 Else
                                                                     Magnetometer.Default.Stop()
                                                                     RemoveHandler Magnetometer.Default.ReadingChanged, AddressOf Magnetometer_ReadingChanged
                                                                 End If
                                                             End If
                                                         End If
                                                     End Function)
        End Sub

        Private Sub Magnetometer_ReadingChanged(sender As Object, e As MagnetometerChangedEventArgs)
            queueHandler.QueueActionIfQueueIsEmpty(Sub()
                                                       Dim velocity = e.Reading.MagneticField
                                                       MagnX.Text = $"X: {velocity.X}µT"
                                                       MagnY.Text = $"Y: {velocity.Y}µT"
                                                       MagnZ.Text = $"Z: {velocity.Z}µT"
                                                   End Sub)
        End Sub
#End Region

#Region "OrientationSensor"
        Private Async Sub OrientationSensorToggleButton_Click(sender As Object, e As RoutedEventArgs)
            Await MainThread.InvokeOnMainThreadAsync(Async Function()
                                                         If OrientationSensor.Default.IsSupported Then
                                                             If Not OrientationSensor.Default.IsMonitoring Then
                                                                 AddHandler OrientationSensor.Default.ReadingChanged, AddressOf OrientationSensor_ReadingChanged
                                                                 OrientationSensor.Default.Start(SensorSpeed.Default)
                                                             Else
                                                                 OrientationSensor.Default.Stop()
                                                                 RemoveHandler OrientationSensor.Default.ReadingChanged, AddressOf OrientationSensor_ReadingChanged
                                                             End If
                                                         End If
                                                     End Function)
        End Sub

        Private Sub OrientationSensor_ReadingChanged(sender As Object, e As OrientationSensorChangedEventArgs)
            queueHandler.QueueActionIfQueueIsEmpty(Sub()
                                                       Dim velocity = e.Reading.Orientation
                                                       OriW.Text = $"W: {velocity.W}"
                                                       OriX.Text = $"X: {velocity.X}"
                                                       OriY.Text = $"Y: {velocity.Y}"
                                                       OriZ.Text = $"Z: {velocity.Z}"
                                                   End Sub)
        End Sub
#End Region
    End Class
End Namespace
