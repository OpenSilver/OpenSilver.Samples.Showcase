namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open Microsoft.Maui.Devices.Sensors
open OpenSilver.Samples.Showcase.Search
open OpenSilver

[<SearchKeywords("maui", "hybrid", "device", "native", "shaking")>]
type public Shake_Demo() as this =
    inherit Shake_DemoXaml()

    let mutable shakes = 0

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(TextBlock(Text = "The shake sample is not supported in the browser.", TextWrapping = TextWrapping.Wrap))
        elif not Accelerometer.Default.IsSupported then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(TextBlock(Text = "This device does not support detecting shakes.", TextWrapping = TextWrapping.Wrap))

    member private this.ShakeToggleButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Sensors>() |> Async.AwaitTask

                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Sensors>() |> Async.AwaitTask
                    else async.Return status

                if status = PermissionStatus.Granted then
                    if Accelerometer.Default.IsSupported then
                        if not Accelerometer.Default.IsMonitoring then
                            Accelerometer.Default.ShakeDetected.AddHandler(
                                EventHandler(fun s e -> this.ShakeSensor_ReadingChanged(s, e))
                            )
                            Accelerometer.Default.Start(SensorSpeed.Game)
                            this.ShakeTextBlock.Foreground <- SolidColorBrush(Colors.Green)
                        else
                            Accelerometer.Default.Stop()
                            Accelerometer.Default.ShakeDetected.RemoveHandler(
                                EventHandler(fun s e -> this.ShakeSensor_ReadingChanged(s, e))
                            )
                            this.ShakeTextBlock.ClearValue(TextBlock.ForegroundProperty)
                    else
                        this.ShakeTextBlock.Text <- "The ShakeSensor is not supported on this device."
            } |> Async.StartImmediate
        )

    member private this.ShakeSensor_ReadingChanged(_sender: obj, _e: EventArgs) =
        shakes <- shakes + 1
        this.ShakeTextBlock.Text <- $"{shakes} shakes detected."
