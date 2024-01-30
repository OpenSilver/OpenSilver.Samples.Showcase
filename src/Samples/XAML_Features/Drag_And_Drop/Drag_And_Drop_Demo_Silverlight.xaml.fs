namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls
open System.Windows.Input

type Drag_And_Drop_Demo() as this =
    inherit Drag_And_Drop_DemoXaml()

    let mutable _isPointerCaptured = false
    let mutable _pointerX = 0.0
    let mutable _pointerY = 0.0
    let mutable _objectLeft = 0.0
    let mutable _objectTop = 0.0

    do
        this.InitializeComponent()

    member this.DragAndDropItem_PointerPressed(sender: obj, e: MouseButtonEventArgs) =
        let uielement: UIElement  = sender :?> UIElement
        _objectLeft <- Canvas.GetLeft(uielement)
        _objectTop <- Canvas.GetTop(uielement)

        _pointerX <- e.GetPosition(null).X
        _pointerY <- e.GetPosition(null).Y
        uielement.CaptureMouse() |> ignore
        _isPointerCaptured <- true

    member this.DragAndDropItem_PointerMoved(sender: obj, e: MouseEventArgs) =
        let uielement : UIElement = sender :?> UIElement
        if _isPointerCaptured then
            // Calculate the new position of the object:
            let deltaH : double = e.GetPosition(null).X - _pointerX
            let deltaV: double  = e.GetPosition(null).Y - _pointerY
            _objectLeft <- deltaH + _objectLeft
            _objectTop <- deltaV + _objectTop

            // Update the obj position:
            Canvas.SetLeft(uielement, _objectLeft)
            Canvas.SetTop(uielement, _objectTop)

            // Remember the pointer position:
            _pointerX <- e.GetPosition(null).X
            _pointerY <- e.GetPosition(null).Y

    member this.DragAndDropItem_PointerReleased(sender: obj, e : MouseButtonEventArgs) =
        let uielement : UIElement = sender :?> UIElement
        _isPointerCaptured <- false
        uielement.ReleaseMouseCapture()
