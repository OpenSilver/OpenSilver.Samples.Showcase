namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("behavior", "interaction", "draggable", "mousedrag", "mousedragelementbehavior", "fluidmove", "fluidmovebehavior", "datastatebehavior", "effects")>]
type Behaviors_Demo() as this =
    inherit Behaviors_DemoXaml()

    do this.InitializeComponent()

    member this.OnMoveLeftButtonClick(sender: obj, e: RoutedEventArgs) =
        let currentLeft = Canvas.GetLeft(this.MovingRectangle)
        if currentLeft > 10.0 then
            Canvas.SetLeft(this.MovingRectangle, currentLeft - 50.0)

    member this.OnMoveRightButtonClick(sender: obj, e: RoutedEventArgs) =
        let currentLeft = Canvas.GetLeft(this.MovingRectangle)
        if currentLeft < 150.0 then
            Canvas.SetLeft(this.MovingRectangle, currentLeft + 50.0)

    member this.OnAddButtonClick(sender: obj, e: RoutedEventArgs) =
        let border = new Border()
        this.wrapPanel.Children.Insert(0, border)
        this.UpdateRemoveButtonState()

    member this.OnRemoveButtonClick(sender: obj, e: RoutedEventArgs) =
        if this.wrapPanel.Children.Count > 0 then
            this.wrapPanel.Children.RemoveAt(0)
            this.UpdateRemoveButtonState()

    member this.UpdateRemoveButtonState() =
        this.RemoveButton.IsEnabled <- this.wrapPanel.Children.Count > 0
