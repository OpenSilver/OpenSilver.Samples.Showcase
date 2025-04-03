namespace OpenSilver.Samples.Showcase

open System
open System.IO
open System.Windows
open System.Windows.Controls
open System.Windows.Documents
open System.Windows.Media.Imaging
open Microsoft.Maui.ApplicationModel
open Microsoft.Maui.Devices
open Microsoft.Maui.Media
open Microsoft.Maui.Storage
open Microsoft.Maui.Devices.Sensors
open OpenSilver.Samples.Showcase.Search
open CSHTML5.Native.Html.Controls
open CSHTML5
open OpenSilver

[<SearchKeywords("maui", "hybrid", "device", "native", "photo", "image", "picture")>]
type public Camera_Demo() as this =
    inherit Camera_DemoXaml()

    let mutable _videoGrid: Grid = null
    let mutable _isWatching = false

    do
        this.InitializeComponent()

    member private this.TakePhotoButton_Click(_sender: obj, _e: RoutedEventArgs) =
        if DeviceInfo.Current.Platform = DevicePlatform.Unknown then
            this.GeneralContainer.Width <- 500.0
            this.GetPhoto_Browser()
        else
            this.GetPhoto_Maui()

    member private this.CancelButton_Click(_sender: obj, _e: RoutedEventArgs) =
        this.StopWatchingCamera()
        this.GetVideoGrid().Visibility <- Visibility.Collapsed

    member private this.GetPhoto_Browser() =
        this.GetVideoGrid().Visibility <- Visibility.Visible
        this.StartWatchingCamera()

    member private this.GetVideoGrid() : Grid =
        if isNull _videoGrid then
            let grid = Grid()

            let htmlPresenter = HtmlPresenter()
            htmlPresenter.Width <- 480.0
            htmlPresenter.Html <- """<video id="video" width="480" height="270" style="width: 480px;">Video stream not available.</video>"""

            let stack = StackPanel()
            stack.Orientation <- Orientation.Horizontal
            stack.HorizontalAlignment <- HorizontalAlignment.Right
            stack.VerticalAlignment <- VerticalAlignment.Bottom

            let acceptButton = Button(Content = "Accept", Margin = Thickness(10.0))
            acceptButton.Click.Add(fun e -> this.AcceptButton_Click(null, e))

            let cancelButton = Button(Content = "Cancel", Margin = Thickness(10.0))
            cancelButton.Click.Add(fun e -> this.CancelButton_Click(null, e))

            stack.Children.Add(acceptButton)
            stack.Children.Add(cancelButton)

            grid.Children.Add(htmlPresenter)
            grid.Children.Add(stack)

            this.SampleContainer.Children.Add(grid)
            _videoGrid <- grid

        _videoGrid

    member private this.StartWatchingCamera() =
        if not _isWatching then
            _isWatching <- true
            this.Dispatcher.BeginInvoke(
                Action(fun () ->
                    Interop.ExecuteJavaScriptAsync(
                        """
((function () {
    navigator.mediaDevices
        .getUserMedia({ video: true, audio: false })
        .then((stream) => {
            let v = document.getElementById("video");
            v.srcObject = stream;
            v.play();
            $0();
        })
        .catch((err) => {
            console.error(`An error occurred while getting camera feed: ${err}`);
            $1();
        });
}) ())""",
                        Action(fun () -> this.OnFeedGet()),
                        Action(fun () -> this.OnFeedError())
                    )
                    |> ignore
                )
            )
            |> ignore



    member private this.OnFeedGet() =
        _isWatching <- true
        this.FeatureNotAllowedTextBlock.Visibility <- Visibility.Collapsed

    member private this.OnFeedError() =
        _isWatching <- false
        this.FeatureNotAllowedTextBlock.Text <- "Unable to access the camera. Please ensure that a camera is available and that you have granted the necessary permissions to this app."
        this.FeatureNotAllowedTextBlock.Visibility <- Visibility.Visible

    member private this.StopWatchingCamera() =
        Interop.ExecuteJavaScriptAsync("""let v = document.getElementById("video"); v.srcObject = null;""")
        _isWatching <- false

    member private this.AcceptButton_Click(_sender: obj, _e: RoutedEventArgs) =
        let imgData =
            Interop.ExecuteJavaScriptGetResult<string>(
                """
((function () {
    var canvas = document.createElement("canvas");
    var v = document.getElementById("video");
    var width = v.videoWidth;
    var height = v.videoHeight;
    canvas.setAttribute("width", width);
    canvas.setAttribute("height", height);
    var data = "";
    const context = canvas.getContext("2d");

    if (width && height) {
        canvas.width = width;
        canvas.height = height;
        context.drawImage(video, 0, 0, width, height);
        data = canvas.toDataURL("image/png");
    }
    return data;
}) ())"""
            )

        this.PhotoTakenImage.Source <- Camera_Demo.LoadImageFromBase64(imgData)
        this.PhotoTakenImage.Visibility <- Visibility.Visible
        this.StopWatchingCamera()
        this.GetVideoGrid().Visibility <- Visibility.Collapsed

    static member LoadImageFromBase64(base64String: string) : BitmapImage =
        try
            let cleanBase64 =
                if base64String.Contains(",") then base64String.Split(',').[1]
                else base64String

            let bytes = Convert.FromBase64String(cleanBase64)
            use ms = new MemoryStream(bytes)
            let bmp = BitmapImage()
            bmp.SetSource(ms)
            bmp
        with ex ->
            Console.WriteLine($"Error loading image: {ex.Message}")
            null

    member private this.GetPhoto_Maui() =
        MainThread.BeginInvokeOnMainThread(fun () ->
            Async.StartImmediate(
                async {
                let! status = Permissions.CheckStatusAsync<Permissions.Camera>() |> Async.AwaitTask
                let! status =
                    if status <> PermissionStatus.Granted then
                        Permissions.RequestAsync<Permissions.Camera>() |> Async.AwaitTask
                    else
                        async.Return status

                if status = PermissionStatus.Granted then
                    try
                        if MediaPicker.Default.IsCaptureSupported then
                            let! photo = MediaPicker.Default.CapturePhotoAsync() |> Async.AwaitTask
                            if not (isNull photo) then
                                this.FeatureNotAllowedTextBlock.Visibility <- Visibility.Collapsed
                                use! stream = photo.OpenReadAsync() |> Async.AwaitTask
                                let bmp = BitmapImage()
                                bmp.SetSource(stream)
                                this.PhotoTakenImage.Source <- bmp
                                this.PhotoTakenImage.Visibility <- Visibility.Visible
                    with
                    | :? PermissionException ->
                        this.FeatureNotAllowedTextBlock.Visibility <- Visibility.Visible
                    | ex ->
                        MessageBox.Show(ex.Message) |> ignore
            })
        )
