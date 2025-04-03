namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open OpenSilver.Samples.Showcase.Search
open OpenSilver

[<SearchKeywords("maui", "hybrid", "device", "native", "haptic", "feedback")>]
type public Vibration_Demo() as this =
    inherit Vibration_DemoXaml()

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(TextBlock(Text = "The vibration sample is not supported in the browser.", TextWrapping = TextWrapping.Wrap))
        elif not Vibration.Default.IsSupported then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(TextBlock(Text = "This device does not support vibrations.", TextWrapping = TextWrapping.Wrap))

    member private this.VibrateButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Vibrate>() |> Async.AwaitTask
                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Vibrate>() |> Async.AwaitTask
                    else async.Return status

                if status = PermissionStatus.Granted then
                    try
                        Vibration.Default.Vibrate(TimeSpan(0, 0, 1))
                    with
                    | :? FeatureNotSupportedException -> () // Not supported
                    | :? PermissionException -> () // Permission issue
                    | _ -> () // Other issue
            } |> Async.StartImmediate
        )

    member private this.HapticFeedbackButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Vibrate>() |> Async.AwaitTask
                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Vibrate>() |> Async.AwaitTask
                    else async.Return status

                if status = PermissionStatus.Granted then
                    try
                        HapticFeedback.Default.Perform(HapticFeedbackType.Click)
                    with
                    | :? FeatureNotSupportedException ->
                        MessageBox.Show("Haptic feedback is not supported on this device.") |> ignore
                    | :? PermissionException -> ()
                    | _ -> ()
            } |> Async.StartImmediate
        )
