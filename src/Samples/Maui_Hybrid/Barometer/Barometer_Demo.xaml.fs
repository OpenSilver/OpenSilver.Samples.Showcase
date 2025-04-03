namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls
open System.Windows.Media
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open Microsoft.Maui.Devices.Sensors
open OpenSilver.Samples.Showcase.Search
open System

[<SearchKeywords("maui", "hybrid", "device", "native", "pressure", "measure", "sensor", "information")>]
type Barometer_Demo() as this =
    inherit Barometer_DemoXaml()

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(
                TextBlock(Text = "The barometer feature is not supported in the browser.", TextWrapping = TextWrapping.Wrap)
            )
        elif not Barometer.Default.IsSupported then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(
                TextBlock(Text = "This device does not support the barometer.", TextWrapping = TextWrapping.Wrap)
            )

    member private this.BarometerToggleButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Sensors>() |> Async.AwaitTask

                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Sensors>() |> Async.AwaitTask
                    else
                        async.Return status

                if status = PermissionStatus.Granted then
                    if Barometer.Default.IsSupported then
                        if not Barometer.Default.IsMonitoring then
                            Barometer.Default.ReadingChanged.AddHandler(
                                EventHandler<BarometerChangedEventArgs>(fun sender args ->
                                    this.Barometer_ReadingChanged(sender, args)
                                )
                            )

                            Barometer.Default.Start(SensorSpeed.UI)
                            this.BarometerTextBlock.Foreground <- SolidColorBrush(Colors.Green)
                        else
                            Barometer.Default.Stop()
                            Barometer.Default.ReadingChanged.RemoveHandler(
                                EventHandler<BarometerChangedEventArgs>(fun sender args ->
                                    this.Barometer_ReadingChanged(sender, args)
                                )
                            )

                            this.BarometerTextBlock.ClearValue(TextBlock.ForegroundProperty)
                    else
                        this.BarometerTextBlock.Text <- "The barometer is not supported on this device."
            } |> Async.StartImmediate
        )

    member private this.Barometer_ReadingChanged(_sender: obj, e: BarometerChangedEventArgs) =
        this.BarometerTextBlock.Text <- $"Barometer: {e.Reading}"
