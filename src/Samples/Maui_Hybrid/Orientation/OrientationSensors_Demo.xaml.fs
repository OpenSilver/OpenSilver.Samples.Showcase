namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open System.Windows.Shapes
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open Microsoft.Maui.Devices.Sensors
open OpenSilver.Samples.Showcase.Search
open CSHTML5.Internal
open OpenSilver

[<SearchKeywords("maui", "hybrid", "device", "native", "sensor", "information", "compass", "accelerometer", "gyroscope", "magnetometer", "orientation")>]
type public OrientationSensors_Demo() as this =
    inherit OrientationSensors_DemoXaml()

    let queueHandler = INTERNAL_DispatcherQueueHandler()

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(TextBlock(Text = "These samples are not supported in the browser.", TextWrapping = TextWrapping.Wrap))
        else
            let unsupported = ResizeArray<string>()

            if not Compass.Default.IsSupported then
                this.CompassContainer.Visibility <- Visibility.Collapsed
                unsupported.Add("Compass")

            if not Accelerometer.Default.IsSupported then
                this.AccelerometerContainer.Visibility <- Visibility.Collapsed
                unsupported.Add("Accelerometer")

            if not Gyroscope.Default.IsSupported then
                this.GyroscopeContainer.Visibility <- Visibility.Collapsed
                unsupported.Add("Gyroscope")

            if not Magnetometer.Default.IsSupported then
                this.MagnetometerContainer.Visibility <- Visibility.Collapsed
                unsupported.Add("Magnetometer")

            if not OrientationSensor.Default.IsSupported then
                this.OrientationContainer.Visibility <- Visibility.Collapsed
                unsupported.Add("Orientation sensor")

            if unsupported.Count > 0 then
                let joined = String.Join("\r\n - ", unsupported)
                this.UnsupportedTextBlock.Text <- $"The following sections of this sample have been hidden because this device does not support them:\r\n - {joined}"
                this.UnsupportedTextBlock.Visibility <- Visibility.Visible



    // -------- Compass --------
    member private this.CompassToggleButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Sensors>() |> Async.AwaitTask
                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Sensors>() |> Async.AwaitTask
                    else async.Return status

                if status = PermissionStatus.Granted && Compass.Default.IsSupported then
                    if not Compass.Default.IsMonitoring then
                        Compass.Default.ReadingChanged.AddHandler(
                            EventHandler<CompassChangedEventArgs>(fun s e -> this.Compass_ReadingChanged(s, e))
                        )
                        Compass.Default.Start(SensorSpeed.UI)
                        TextBlock.SetForeground(this.CompassPath, SolidColorBrush(Colors.Green))
                    else
                        Compass.Default.Stop()
                        Compass.Default.ReadingChanged.RemoveHandler(
                            EventHandler<CompassChangedEventArgs>(fun s e -> this.Compass_ReadingChanged(s, e))
                        )
                        this.CompassPath.ClearValue(TextBlock.ForegroundProperty)
            } |> Async.StartImmediate
        )

    member private this.Compass_ReadingChanged(_sender: obj, e: CompassChangedEventArgs) =
        this.CompassTransform.Angle <- -e.Reading.HeadingMagneticNorth

    // -------- Accelerometer --------
    member private this.AccelerometerToggleButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Sensors>() |> Async.AwaitTask
                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Sensors>() |> Async.AwaitTask
                    else async.Return status

                if status = PermissionStatus.Granted && Accelerometer.Default.IsSupported then
                    if not Accelerometer.Default.IsMonitoring then
                        Accelerometer.Default.ReadingChanged.AddHandler(
                            EventHandler<AccelerometerChangedEventArgs>(fun s e -> this.Accelerometer_ReadingChanged(s, e))
                        )
                        Accelerometer.Default.Start(SensorSpeed.UI)
                    else
                        Accelerometer.Default.Stop()
                        Accelerometer.Default.ReadingChanged.RemoveHandler(
                            EventHandler<AccelerometerChangedEventArgs>(fun s e -> this.Accelerometer_ReadingChanged(s, e))
                        )
            } |> Async.StartImmediate
        )

    member private this.Accelerometer_ReadingChanged(_sender: obj, e: AccelerometerChangedEventArgs) =
        let a = e.Reading.Acceleration
        this.AccelX.Text <- $"X: {a.X}G"
        this.AccelY.Text <- $"Y: {a.Y}G"
        this.AccelZ.Text <- $"Z: {a.Z}G"

    // -------- Gyroscope --------
    member private this.GyroscopeToggleButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Sensors>() |> Async.AwaitTask
                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Sensors>() |> Async.AwaitTask
                    else async.Return status

                if status = PermissionStatus.Granted && Gyroscope.Default.IsSupported then
                    if not Gyroscope.Default.IsMonitoring then
                        Gyroscope.Default.ReadingChanged.AddHandler(
                            EventHandler<GyroscopeChangedEventArgs>(fun s e -> this.Gyroscope_ReadingChanged(s, e))
                        )
                        Gyroscope.Default.Start(SensorSpeed.UI)
                    else
                        Gyroscope.Default.Stop()
                        Gyroscope.Default.ReadingChanged.RemoveHandler(
                            EventHandler<GyroscopeChangedEventArgs>(fun s e -> this.Gyroscope_ReadingChanged(s, e))
                        )
            } |> Async.StartImmediate
        )

    member private this.Gyroscope_ReadingChanged(_sender: obj, e: GyroscopeChangedEventArgs) =
        let v = e.Reading.AngularVelocity
        this.GyroX.Text <- $"X: {v.X}rad/s"
        this.GyroY.Text <- $"Y: {v.Y}rad/s"
        this.GyroZ.Text <- $"Z: {v.Z}rad/s"

    // -------- Magnetometer --------
    member private this.MagnetometerToggleButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Sensors>() |> Async.AwaitTask
                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Sensors>() |> Async.AwaitTask
                    else async.Return status

                if status = PermissionStatus.Granted && Magnetometer.Default.IsSupported then
                    if not Magnetometer.Default.IsMonitoring then
                        Magnetometer.Default.ReadingChanged.AddHandler(
                            EventHandler<MagnetometerChangedEventArgs>(fun s e -> this.Magnetometer_ReadingChanged(s, e))
                        )
                        Magnetometer.Default.Start(SensorSpeed.Default)
                    else
                        Magnetometer.Default.Stop()
                        Magnetometer.Default.ReadingChanged.RemoveHandler(
                            EventHandler<MagnetometerChangedEventArgs>(fun s e -> this.Magnetometer_ReadingChanged(s, e))
                        )
            } |> Async.StartImmediate
        )

    member private this.Magnetometer_ReadingChanged(_sender: obj, e: MagnetometerChangedEventArgs) =
        queueHandler.QueueActionIfQueueIsEmpty(fun () ->
            let v = e.Reading.MagneticField
            this.MagnX.Text <- $"X: {v.X}µT"
            this.MagnY.Text <- $"Y: {v.Y}µT"
            this.MagnZ.Text <- $"Z: {v.Z}µT"
        )

    // -------- Orientation Sensor --------
    member private this.OrientationSensorToggleButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Sensors>() |> Async.AwaitTask
                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Sensors>() |> Async.AwaitTask
                    else async.Return status

                if status = PermissionStatus.Granted && OrientationSensor.Default.IsSupported then
                    if not OrientationSensor.Default.IsMonitoring then
                        OrientationSensor.Default.ReadingChanged.AddHandler(
                            EventHandler<OrientationSensorChangedEventArgs>(fun s e -> this.OrientationSensor_ReadingChanged(s, e))
                        )
                        OrientationSensor.Default.Start(SensorSpeed.Default)
                    else
                        OrientationSensor.Default.Stop()
                        OrientationSensor.Default.ReadingChanged.RemoveHandler(
                            EventHandler<OrientationSensorChangedEventArgs>(fun s e -> this.OrientationSensor_ReadingChanged(s, e))
                        )
            } |> Async.StartImmediate
        )

    member private this.OrientationSensor_ReadingChanged(_sender: obj, e: OrientationSensorChangedEventArgs) =
        queueHandler.QueueActionIfQueueIsEmpty(fun () ->
            let q = e.Reading.Orientation
            this.OriW.Text <- $"W: {q.W}"
            this.OriX.Text <- $"X: {q.X}"
            this.OriY.Text <- $"Y: {q.Y}"
            this.OriZ.Text <- $"Z: {q.Z}"
        )
