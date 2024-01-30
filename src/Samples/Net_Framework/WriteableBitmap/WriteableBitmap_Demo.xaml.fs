namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Data
open System.Windows.Input
open System.Windows.Media
open System.Windows.Media.Imaging
open System.Windows.Navigation

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
