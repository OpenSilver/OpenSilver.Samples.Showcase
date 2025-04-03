namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("maui", "hybrid", "device", "native")>]
type public Flashlight_Demo() as this =
    inherit Flashlight_DemoXaml()

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(
                TextBlock(Text = "The flashlight is not supported in the browser.", TextWrapping = TextWrapping.Wrap)
            )
        else
            MainThread.BeginInvokeOnMainThread(fun () ->
                async {
                    let! supported = Flashlight.Default.IsSupportedAsync() |> Async.AwaitTask
                    if not supported then
                        this.FlashlightButton.Visibility <- Visibility.Collapsed
                        this.UnsupportedTextBlock.Text <- "The flashlight is not supported on this device."
                        this.UnsupportedTextBlock.Visibility <- Visibility.Visible
                } |> Async.StartImmediate
            )

    member private this.SwitchButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Flashlight>() |> Async.AwaitTask

                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Flashlight>() |> Async.AwaitTask
                    else
                        async.Return status

                if status = PermissionStatus.Granted then
                    try
                        if this.FlashlightButton.IsChecked = Nullable true then
                            do! Flashlight.Default.TurnOnAsync() |> Async.AwaitTask
                        else
                            do! Flashlight.Default.TurnOffAsync() |> Async.AwaitTask
                    with
                    | :? PermissionException ->
                        this.UnsupportedTextBlock.Text <- "This sample requires your permission to use the flashlight."
                        this.UnsupportedTextBlock.Visibility <- Visibility.Visible
                    | _ ->
                        () // Could not toggle flashlight
            } |> Async.StartImmediate
        )
