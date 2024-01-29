namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open System.Windows.Input

type MouseWheel_Demo() as this =
    inherit MouseWheel_DemoXaml()

    let mutable scrollingDistance = 0

    let CountTotalScrollingDistance(sender : obj, e : MouseWheelEventArgs) =
        e.Handled <- true

        let delta =
            e.Delta

        scrollingDistance <- scrollingDistance + delta
        this.ScrollingDistanceTextBlock.Text <- "Distance scrolled (with the mouse) on the border below: " + scrollingDistance.ToString() + "."

    do
        this.InitializeComponent()

        this.TitleContentControl.Content <- "MouseWheel Event"
        this.ScrollBorder.MouseWheel.Add(fun args -> CountTotalScrollingDistance(this.ScrollBorder, args))
