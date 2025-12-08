namespace OpenSilver.Showcase

open System
open System.Windows
open System.Windows.Media.Imaging
open OpenSilver.Showcase.Search

[<SearchKeywords("bitmap", "image", "graphics", "drawing", "rendering")>]
type WriteableBitmap_Demo() as this =
    inherit WriteableBitmap_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.ClearButton_Click(sender: obj, e: RoutedEventArgs) =
        this.ivDestination.Source <- null

    member private this.MirrorButton_Click(sender: obj, e: RoutedEventArgs) =
        async {
            let bitmap = new WriteableBitmap(200, 200)
            do! bitmap.RenderAsync(this.ivSource, null) |> Async.AwaitTask

            // Let's modify pixels
            Array.Reverse(bitmap.Pixels)

            bitmap.Invalidate() // Invalidate once Pixels are manipulated

            this.ivDestination.Source <- bitmap
        } |> Async.Start
