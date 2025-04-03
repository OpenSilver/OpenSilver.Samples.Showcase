namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open Microsoft.Maui.Devices.Sensors
open OpenSilver.Samples.Showcase.Search
open OpenSilver

[<SearchKeywords("maui", "hybrid", "device", "native", "location")>]
type public OpenMapApp_Demo() as this =
    inherit OpenMapApp_DemoXaml()

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(
                TextBlock(Text = "The map app is not supported in the browser.", TextWrapping = TextWrapping.Wrap)
            )

    member private this.OpenMapAppButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let location = Location(48.8584, 2.2945)
                let options = MapLaunchOptions(Name = "Eiffel tower")

                try
                    do! Map.Default.OpenAsync(location, options) |> Async.AwaitTask
                with
                | _ ->
                    () // No map application available to open
            } |> Async.StartImmediate
        )
