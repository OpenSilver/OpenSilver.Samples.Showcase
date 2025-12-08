namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("animation")>]
type TransitioningContentControl_Demo() as this =
    inherit TransitioningContentControl_DemoXaml()

    do
        this.InitializeComponent()

    let setContentWithTransition (transition: string) =
        this.defaultTCC.Transition <- transition
        this.defaultTCC.Content <- DateTime.Now.Ticks

    member this.ChangeContentWithDefaultTransition(sender: obj, e: RoutedEventArgs) =
        setContentWithTransition TransitioningContentControl.DefaultTransitionState

    member this.ChangeContentWithDownTransition(sender: obj, e: RoutedEventArgs) =
        setContentWithTransition "DownTransition"

    member this.ChangeContentWithUpTransition(sender: obj, e: RoutedEventArgs) =
        setContentWithTransition "UpTransition"
