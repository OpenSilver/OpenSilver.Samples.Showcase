namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open Microsoft.Maui.Devices.Sensors
open OpenSilver.Samples.Showcase.Search
open OpenSilver

[<SearchKeywords("maui", "hybrid", "device", "native", "coordinates", "longitude", "latitude", "information")>]
type public Geolocation() as this =
    inherit GeolocationXaml()

    do
        this.InitializeComponent()

    member private this.GetButton_Click(_sender: obj, _e: RoutedEventArgs) =
        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.GetLocation_Browser()
        else
            this.GetLocation_Maui()

    member private this.GetLocation_Browser() : unit =
        Interop.ExecuteJavaScriptAsync(
            "navigator.geolocation.getCurrentPosition((pos) => {$0(\"lat: \"+ pos.coords.latitude + \"\r\nlong: \" + pos.coords.longitude)})",
            Action<string>(fun s -> this.ReceiveLocation(s))
        )
        |> ignore

    member private this.ReceiveLocation(obj: string) =
        this.LocationTextBlock.Text <- obj

    member private this.GetLocation_Maui() =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>() |> Async.AwaitTask

                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.LocationWhenInUse>() |> Async.AwaitTask
                    else
                        async.Return status

                if status = PermissionStatus.Granted then
                    try
                        let request = GeolocationRequest(GeolocationAccuracy.Medium)
                        let! location = Microsoft.Maui.Devices.Sensors.Geolocation.Default.GetLocationAsync(request) |> Async.AwaitTask

                        if location <> null then
                            this.Dispatcher.BeginInvoke(
                                Action(fun () ->
                                    this.LocationTextBlock.Text <- location.ToString()
                                )
                            )
                            |> ignore
                    with
                    | :? FeatureNotSupportedException ->
                        this.LocationTextBlock.Text <- "This device does not support Geolocation."
                    | :? FeatureNotEnabledException ->
                        this.LocationTextBlock.Text <- "The Geolocation feature is not enabled."
                    | :? PermissionException ->
                        this.LocationTextBlock.Text <- "This sample requires permission to use Geolocation."
                    | _ ->
                        this.LocationTextBlock.Text <- "Could not get location."
                else
                    this.LocationTextBlock.Text <- "This sample requires permission to use Geolocation."
            } |> Async.StartImmediate
        )
