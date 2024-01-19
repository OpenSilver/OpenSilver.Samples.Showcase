namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Input
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

type Drag_And_Drop_Demo() as this =
    inherit Drag_And_Drop_DemoXaml()

    let mutable _isPointerCaptured = false
    let mutable _pointerX = 0.0
    let mutable _pointerY = 0.0
    let mutable _objectLeft = 0.0
    let mutable _objectTop = 0.0

    do
        this.InitializeComponent()

#if SLMIGRATION
    member this.DragAndDropItem_PointerPressed(sender: obj, e: MouseButtonEventArgs) =
#else
    member this.DragAndDropItem_PointerPressed(sender: obj, e: PointerRoutedEventArgs) =
#endif
        let uielement: UIElement  = sender :?> UIElement
        _objectLeft <- Canvas.GetLeft(uielement)
        _objectTop <- Canvas.GetTop(uielement)

#if SLMIGRATION
        _pointerX <- e.GetPosition(null).X
        _pointerY <- e.GetPosition(null).Y
        uielement.CaptureMouse() |> ignore
#else
        _pointerX <- e.GetCurrentPoint(null).Position.X
        _pointerY <- e.GetCurrentPoint(null).Position.Y
        uielement.CapturePointer(e.Pointer)
#endif
        _isPointerCaptured <- true

#if SLMIGRATION
    member this.DragAndDropItem_PointerMoved(sender: obj, e: MouseEventArgs) =
#else
    member this.DragAndDropItem_PointerMoved(sender: obj, e : PointerRoutedEventArgs) =
#endif
        let uielement : UIElement = sender :?> UIElement
        if _isPointerCaptured then
            // Calculate the new position of the object:
#if SLMIGRATION
            let deltaH : double = e.GetPosition(null).X - _pointerX
            let deltaV: double  = e.GetPosition(null).Y - _pointerY
#else
            let deltaH : double = e.GetCurrentPoint(null).Position.X - _pointerX
            let deltaV: double  = e.GetCurrentPoint(null).Position.Y - _pointerY
#endif
            _objectLeft <- deltaH + _objectLeft
            _objectTop <- deltaV + _objectTop

            // Update the obj position:
            Canvas.SetLeft(uielement, _objectLeft)
            Canvas.SetTop(uielement, _objectTop)

            // Remember the pointer position:
#if SLMIGRATION
            _pointerX <- e.GetPosition(null).X
            _pointerY <- e.GetPosition(null).Y
#else
            _pointerX <- e.GetCurrentPoint(null).Position.X
            _pointerY <- e.GetCurrentPoint(null).Position.Y
#endif

#if SLMIGRATION
    member this.DragAndDropItem_PointerReleased(sender: obj, e : MouseButtonEventArgs) =
#else
    member this.DragAndDropItem_PointerReleased(sender: obj, e : PointerRoutedEventArgs) =
#endif
        let uielement : UIElement = sender :?> UIElement
        _isPointerCaptured <- false
#if SLMIGRATION
        uielement.ReleaseMouseCapture()
#else
        uielement.ReleasePointerCapture(e.Pointer)
#endif

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Drag_And_Drop_Demo_Silverlight.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Drag_And_Drop_Demo_Silverlight.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Drag_And_Drop_Demo_Silverlight.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Drag_And_Drop_Demo_Silverlight.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Drag_And_Drop/Drag_And_Drop_Demo.xaml.fs")
        ])
