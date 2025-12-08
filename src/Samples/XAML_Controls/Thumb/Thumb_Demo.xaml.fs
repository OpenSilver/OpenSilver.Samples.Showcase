namespace OpenSilver.Samples.Showcase

open System.Windows.Controls.Primitives
open System.Windows.Input
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("drag", "scroll")>]
type Thumb_Demo() as this =
    inherit Thumb_DemoXaml()

    do this.InitializeComponent()

    let onThumbDragStarted (sender: obj) (e: DragStartedEventArgs) =
        this.infoTextBlock.Text <- sprintf "DragStarted X: %f; Y: %f" e.HorizontalOffset e.VerticalOffset
        this.Cursor <- Cursors.ScrollAll

    let onThumbDragDelta (sender: obj) (e: DragDeltaEventArgs) =
        this.infoTextBlock.Text <- sprintf "DragDelta X: %f; Y: %f" e.HorizontalChange e.VerticalChange

    let onThumbDragCompleted (sender: obj) (e: DragCompletedEventArgs) =
        this.infoTextBlock.Text <- sprintf "DragCompleted X: %f; Y: %f" e.HorizontalChange e.VerticalChange
        this.Cursor <- null

    // Optionally expose the handlers
    member _.OnThumbDragStarted = onThumbDragStarted
    member _.OnThumbDragDelta = onThumbDragDelta
    member _.OnThumbDragCompleted = onThumbDragCompleted
