namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search
open PreviewOnWinRT

[<SearchKeywords("layout", "window", "popup", "nonmodal", "non-modal", "dialog")>]
type public NonModalChildWindow_Demo() as this =
    inherit NonModalChildWindow_DemoXaml()

    let mutable n = 1

    do
        this.InitializeComponent()

    member private this.ButtonTestChildWindow_Modal_Click(_sender: obj, _e: RoutedEventArgs) =
        let cw = SmallChildWindow()
        cw.Title <- $"ChildWindow (Modal) #{n}"
        cw.IsModal <- true
        cw.Show()
        n <- n + 1

    member private this.ButtonTestChildWindow_NonModal_Click(_sender: obj, _e: RoutedEventArgs) =
        let cw = SmallChildWindow()
        cw.Title <- $"ChildWindow (Non-Modal) #{n}"
        cw.IsModal <- false
        cw.optionsStack.Visibility <- Visibility.Collapsed
        cw.Show()
        n <- n + 1
