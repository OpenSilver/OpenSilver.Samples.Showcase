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

type MouseWheel_Demo() as this =
    inherit MouseWheel_DemoXaml()

    let mutable scrollingDistance = 0

#if SLMIGRATION
    let CountTotalScrollingDistance(sender : obj, e : MouseWheelEventArgs) =
#else
    let CountTotalScrollingDistance(sender : obj, e : PointerRoutedEventArgs) =
#endif
        e.Handled <- true

        let delta =
#if SLMIGRATION
            e.Delta
#else
            e.GetCurrentPoint(null).Properties.MouseWheelDelta
#endif

        scrollingDistance <- scrollingDistance + delta
        this.ScrollingDistanceTextBlock.Text <- "Distance scrolled (with the mouse) on the border below: " + scrollingDistance.ToString() + "."

    do
        this.InitializeComponent()

#if SLMIGRATION
        this.TitleContentControl.Content <- "MouseWheel Event"
        this.ScrollBorder.MouseWheel.Add(fun args -> CountTotalScrollingDistance(this.ScrollBorder, args))
#else
        this.TitleContentControl.Content <- "PointerWheelChanged Event"
        this.ScrollBorder.PointerWheelChanged.Add(fun args -> CountTotalScrollingDistance(this.ScrollBorder, args))
#endif

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "MouseWheel_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "MouseWheel_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "MouseWheel_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "MouseWheel_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/MouseWheel/MouseWheel_Demo.xaml.fs")
        ])
