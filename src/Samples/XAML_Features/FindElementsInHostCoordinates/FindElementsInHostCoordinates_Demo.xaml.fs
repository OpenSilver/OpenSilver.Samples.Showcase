namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open System.Windows.Input
open System.Windows.Media

type FindElementsInHostCoordinates_Demo() as this =
    inherit FindElementsInHostCoordinates_DemoXaml()

    let mutable _highestZIndex = 2

    let InitAllZIndex () =
        // 0 -> 2 is from the background to the front
        Canvas.SetZIndex(this.BlueRectangle, 0)
        Canvas.SetZIndex(this.GreenRectangle, 1)
        Canvas.SetZIndex(this.YellowRectangle, 2)

    do
        this.InitializeComponent()
        InitAllZIndex()

        this.MouseLeftButtonDown.Add(fun args -> this.FindElementsInHostCoordinates_Demo_PointerPressed(this, args))
    member this.FindElementsInHostCoordinates_Demo_PointerPressed(sender : obj, e : MouseButtonEventArgs) =
        // Get the absolute coordinates of the pointer:
        let currentPoint = e.GetPosition(null)
        // Find the element that is under the pointer:
        let uiElement =
            VisualTreeHelper.FindElementsInHostCoordinates(currentPoint, this.CanvasParent)
            |> Seq.tryHead

        // Bring the clicked element to the front:
        match uiElement with
        | Some(element) ->
            if (element :? Border) then
                _highestZIndex <- _highestZIndex + 1
                Canvas.SetZIndex(element, _highestZIndex)
        | _ -> ()

        //if uiElement <> None then
        //if ((box uiElement) :? Border) then
        //_highestZIndex <- _highestZIndex + 1
        //Canvas.SetZIndex((box uiElement) :?> UIElement, _highestZIndex)
