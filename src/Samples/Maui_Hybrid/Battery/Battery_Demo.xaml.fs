namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Documents
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open OpenSilver.Samples.Showcase.Search
open OpenSilver

[<SearchKeywords("maui", "hybrid", "device", "native", "charge", "charging", "status", "information")>]
type public Battery_Demo() as this =
    inherit Battery_DemoXaml()

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            let isUnsupported = Interop.ExecuteJavaScriptGetResult<bool>("!navigator.getBattery")
            if isUnsupported then
                this.SampleContainer.Children.Clear()

                let link = Hyperlink(NavigateUri = Uri("https://developer.mozilla.org/en-US/docs/Web/API/Battery_Status_API#browser_compatibility"), TargetName = "_blank")
                link.Inlines.Add("here") |> ignore

                let tb = TextBlock(TextWrapping = TextWrapping.Wrap)
                tb.Inlines.Add("The battery status API is not supported in this browser. Check ") |> ignore
                tb.Inlines.Add(link) |> ignore
                tb.Inlines.Add(" to see its compatibility table.") |> ignore

                this.SampleContainer.Children.Add(tb)

    member private this.CheckBatteryStatusButton_Click(_sender: obj, _e: RoutedEventArgs) =
        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.CheckBattery_Browser()
        else
            this.CheckBattery_Maui()

    member private this.CheckBattery_Browser() : unit =
        Interop.ExecuteJavaScriptAsync(
            "navigator.getBattery().then((battery) => { $0(\"Battery is \" + (battery.charging ? \"\" : \"not \") + \"charging\\r\\nCharge level: \" + battery.level * 100 + \"%\", true) })",
            Action<string>(fun s -> this.UpdateBatteryStatus_Browser(s))
        ) |> ignore

    member private this.UpdateBatteryStatus_Browser(text: string) =
        this.BatteryStatus.Text <- text

    member private this.CheckBattery_Maui() =
        // Define the async block in a separate binding BEFORE using it
        let batteryCheckAsync =
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.Battery>() |> Async.AwaitTask

                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Battery>() |> Async.AwaitTask
                    else
                        async.Return status

                try
                    if status = PermissionStatus.Granted then
                        // TEMP: define message locally without using `this`
                        let mutable str = $"Battery is {Battery.Default.ChargeLevel * 100.0}%% charged.{Environment.NewLine}"

                        str <- str +
                            match Battery.Default.State with
                            | BatteryState.Charging -> "Battery is currently charging"
                            | BatteryState.Discharging -> "Charger is not connected and the battery is discharging"
                            | BatteryState.Full -> "Battery is full"
                            | BatteryState.NotCharging -> "The battery isn't charging"
                            | BatteryState.NotPresent -> "Battery is not available"
                            | _ -> "Battery is unknown"

                        // TEMP: explicitly return the result instead of assigning to UI
                        Console.WriteLine(str) // Use console for now
                    return ()
                with
                | :? FeatureNotSupportedException ->
                    Console.WriteLine("Battery status not supported.")
                    return ()
                | :? PermissionException ->
                    Console.WriteLine("Permission denied.")
                    return ()
                | _ ->
                    Console.WriteLine("General battery error.")
                    return ()
            }

        //// Only use `this` outside the async block
        MainThread.BeginInvokeOnMainThread(fun () ->
            Async.StartImmediate(batteryCheckAsync)
        )