namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Media.Imaging
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.ApplicationModel.DataTransfer
open Microsoft.Maui.Devices
open Microsoft.Maui.Storage
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("maui", "hybrid", "device", "native", "storage", "access")>]
type public FilePickerAndShare_Demo() as this =
    inherit FilePickerAndShare_DemoXaml()

    let mutable _pickedFileFullPath: string = null

    do
        this.InitializeComponent()

        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.SampleContainer.Children.Clear()
            this.SampleContainer.Children.Add(
                TextBlock(Text = "This sample is not supported in the browser.", TextWrapping = TextWrapping.Wrap)
            )

    member private this.PickImageButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            async {
                let! status = Permissions.CheckStatusAsync<Permissions.StorageRead>() |> Async.AwaitTask

                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.StorageRead>() |> Async.AwaitTask
                    else
                        async.Return status

                if status = PermissionStatus.Granted then
                    try
                        let! result = FilePicker.Default.PickAsync(PickOptions.Images) |> Async.AwaitTask
                        if not (isNull result) then
                            _pickedFileFullPath <- result.FullPath
                            this.ShareButton.Visibility <- Visibility.Visible
                            this.PickedImageControl.Visibility <- Visibility.Visible
                            this.FeatureNotAllowedTextBlock.Visibility <- Visibility.Collapsed

                            if result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                               result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) then
                                use! stream = result.OpenReadAsync() |> Async.AwaitTask
                                let bmp = BitmapImage()
                                bmp.SetSource(stream)
                                this.PickedImageControl.Source <- bmp
                    with
                    | :? PermissionException ->
                        this.FeatureNotAllowedTextBlock.Visibility <- Visibility.Visible
                    | _ ->
                        () // user cancelled or other issue
            } |> Async.StartImmediate
        )

    member private this.ShareImageButton_Click(_sender: obj, _e: RoutedEventArgs) =
        MainThread.BeginInvokeOnMainThread(fun () ->
            Async.StartImmediate(
                async {
                if not (isNull _pickedFileFullPath) then
                    try
                        let request = ShareFileRequest(
                            Title = "Share the picked image",
                            File = ShareFile(_pickedFileFullPath)
                        )
                        do! Share.Default.RequestAsync(request) |> Async.AwaitTask
                    with _ -> ()
                else
                    MessageBox.Show("Pick an image first before attempting to share it.") |> ignore
            })
        )
