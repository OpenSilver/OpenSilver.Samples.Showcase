namespace OpenSilver.Samples.Showcase

open System
open System.Text
open System.Windows
open System.Windows.Controls
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("maui", "hybrid", "device", "native", "screen", "information", "pixel", "display", "size")>]
type public DisplayInfo_Demo() as this =
    inherit DisplayInfo_DemoXaml()

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(
                TextBlock(Text = "The display info sample is not supported in the browser.", TextWrapping = TextWrapping.Wrap)
            )

    member private this.ButtonGetDisplayInfo_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            try
                let sb = StringBuilder()
                let info = DeviceDisplay.Current.MainDisplayInfo

                sb.AppendLine($"Pixel width: {info.Width} / Pixel Height: {info.Height}") |> ignore
                sb.AppendLine($"Density: {info.Density}") |> ignore
                sb.AppendLine($"Orientation: {info.Orientation}") |> ignore
                sb.AppendLine($"Rotation: {info.Rotation}") |> ignore
                sb.AppendLine($"Refresh Rate: {info.RefreshRate}") |> ignore

                this.DisplayInfo.Text <- sb.ToString()
            with _ -> ()
        )
