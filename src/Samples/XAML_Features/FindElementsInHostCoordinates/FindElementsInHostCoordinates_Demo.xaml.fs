namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Input
open System.Windows.Media
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

#if SLMIGRATION
        this.MouseLeftButtonDown.Add(fun args -> this.FindElementsInHostCoordinates_Demo_PointerPressed(this, args))
#else
        this.PointerPressed.Add(fun args -> this.FindElementsInHostCoordinates_Demo_PointerPressed(this, args))
#endif

#if SLMIGRATION
    member this.FindElementsInHostCoordinates_Demo_PointerPressed(sender : obj, e : MouseButtonEventArgs) =
        // Get the absolute coordinates of the pointer:
        let currentPoint = e.GetPosition(null)

#else
    member this.FindElementsInHostCoordinates_Demo_PointerPressed(sender : obj, e : PointerRoutedEventArgs) =
        // Get the absolute coordinates of the pointer:
        let currentPoint = e.GetCurrentPoint(null).Position
#endif
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

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "FindElementsInHostCoordinates_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/FindElementsInHostCoordinates/FindElementsInHostCoordinates_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "FindElementsInHostCoordinates_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/FindElementsInHostCoordinates/FindElementsInHostCoordinates_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "FindElementsInHostCoordinates_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/FindElementsInHostCoordinates/FindElementsInHostCoordinates_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "FindElementsInHostCoordinates_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/FindElementsInHostCoordinates/FindElementsInHostCoordinates_Demo.xaml.fs")
        ])
