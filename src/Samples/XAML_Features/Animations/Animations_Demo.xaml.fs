namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Media.Animation
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Media.Animation
open Windows.UI.Xaml.Navigation
#endif

type Animations_Demo() as this =
    inherit Animations_DemoXaml()

    do
        this.InitializeComponent()

    member this.ButtonToStartAnimationOpen_Click(sender : obj, e : RoutedEventArgs) =
        let storyboard = this.CanvasForAnimationsDemo.Resources.["AnimationToOpen"] :?> Storyboard
        storyboard.Begin()
        this.ButtonToStartAnimationOpen.Visibility <- Visibility.Collapsed
        this.ButtonToStartAnimationClose.Visibility <- Visibility.Visible

    member this.ButtonToStartAnimationClose_Click(sender : obj, e : RoutedEventArgs) =
        let storyboard = this.CanvasForAnimationsDemo.Resources.["AnimationToClose"] :?> Storyboard
        storyboard.Begin()
        this.ButtonToStartAnimationOpen.Visibility <- Visibility.Visible
        this.ButtonToStartAnimationClose.Visibility <- Visibility.Collapsed

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource [
            ViewSourceButtonInfo(TabHeader = "Animations_Demo.xaml",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Animations/Animations_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Animations_Demo.xaml.cs",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Animations/Animations_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Animations_Demo.xaml.vb",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Animations/Animations_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Animations_Demo.xaml.fs",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/Animations/Animations_Demo.xaml.fs");
        ]


