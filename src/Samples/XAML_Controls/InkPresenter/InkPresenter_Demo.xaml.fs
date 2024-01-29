namespace OpenSilver.Samples.Showcase

open System.Collections.Generic
open System.ComponentModel
open System.Runtime.CompilerServices
open System.Windows
open System.Windows.Controls
open System.Windows.Ink
open System.Windows.Input

type InkPresenter_Demo() as this =
    inherit InkPresenter_DemoXaml()

    let mutable LastStroke : Stroke = null
    let nextStrokes = Stack<Stroke>()

    let mutable _canClearStrokes = false
    let mutable _canUndoStroke = false
    let mutable _canRedoStroke = false

    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()
    
    do
        this.InitializeComponent()

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

    member this.CanClearStrokes
        with get() = _canClearStrokes
        and set(value) = 
            _canClearStrokes <- value
            this.OnPropertyChanged("CanClearStrokes")

    member this.CanUndoStroke
        with get() = _canUndoStroke
        and set(value) = 
            _canUndoStroke <- value
            this.OnPropertyChanged("CanUndoStroke")

    member this.CanRedoStroke
        with get() = _canRedoStroke
        and set(value) = 
            _canRedoStroke <- value
            this.OnPropertyChanged("CanRedoStroke")

    member private this.OnPropertyChanged(propertyName : string) =
        propertyChanged.Trigger(this, PropertyChangedEventArgs(propertyName))

    //A new stroke object named MyStroke is created. MyStroke is added to the StrokeCollection of the InkPresenter named MyIP
    member private this.OnIP_MouseLeftButtonDown(sender: obj, e: MouseButtonEventArgs) =
        this.InkPad.CaptureMouse()
        let myStylusPointCollection = new StylusPointCollection()
        myStylusPointCollection.Add(e.StylusDevice.GetStylusPoints(this.InkPad))

        LastStroke <- new Stroke(myStylusPointCollection)
        this.InkPad.Strokes.Add(LastStroke)
        this.CanUndoStroke <- true
        this.CanClearStrokes <- true

        this.CanRedoStroke <- false
        nextStrokes.Clear()

    member private this.OnIP_MouseLeftButtonUp(sender: obj, e: MouseButtonEventArgs) =
        this.InkPad.ReleaseMouseCapture()

    member private this.OnIP_MouseMove(sender: obj, e: MouseEventArgs) =
        if not (isNull LastStroke) && this.InkPad.IsMouseCaptured then
            LastStroke.StylusPoints.Add(e.StylusDevice.GetStylusPoints(this.InkPad))

    member private this.OnIP_LostMouseCapture(sender: obj, e: MouseEventArgs) =
        ()

    member private this.OnClearInkPad(sender: obj, e: RoutedEventArgs) =
        LastStroke <- null
        nextStrokes.Clear()
        this.InkPad.Strokes.Clear()
        this.CanClearStrokes <- false
        this.CanUndoStroke <- false
        this.CanRedoStroke <- false

    member this.OnUndoLastStroke(sender: obj, e: RoutedEventArgs) =
        let strokes = this.InkPad.Strokes
        if strokes.Count > 0 then
            nextStrokes.Push(strokes.[strokes.Count - 1])
            strokes.RemoveAt(strokes.Count - 1)
            this.CanRedoStroke <- true

        if strokes.Count = 0 then
            this.CanUndoStroke <- false

    member this.OnRedoLastStroke(sender: obj, e: RoutedEventArgs) =
        if nextStrokes.Count > 0 then
            this.InkPad.Strokes.Add(nextStrokes.Pop())
            this.CanUndoStroke <- true

        if nextStrokes.Count = 0 then
            this.CanRedoStroke <- false
