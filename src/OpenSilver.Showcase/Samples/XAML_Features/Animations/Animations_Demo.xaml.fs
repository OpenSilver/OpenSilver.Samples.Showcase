namespace OpenSilver.Showcase

open System.Windows
open System.Windows.Media.Animation
open OpenSilver.Showcase.Search

[<SearchKeywords("animation", "effects", "motion", "behavior")>]
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
