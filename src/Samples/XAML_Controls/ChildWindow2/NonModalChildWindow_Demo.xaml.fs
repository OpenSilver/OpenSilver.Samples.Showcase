namespace OpenSilver.Samples.Showcase

open PreviewOnWinRT
open System.Windows
open System.Windows.Controls

type NonModalChildWindow_Demo() as this =
    inherit NonModalChildWindow_DemoXaml()

    let mutable _n = 0

    do
        this.InitializeComponent()

    member private this.ButtonTestChildWindow_Modal_Click(sender : obj, e : RoutedEventArgs) =
        let childWindow = SmallChildWindow()
        childWindow.Title <- "ChildWindow (Modal)" + string _n
        _n <- _n + 1
#if !OPENSILVER
        childWindow.IsModal <- true
#endif
        childWindow.Show()

    member private this. ButtonTestChildWindow_NonModal_Click(sender : obj, e : RoutedEventArgs) =
        let childWindow = SmallChildWindow()
        childWindow.Title <- "ChildWindow (Non-modal)" + string _n
        _n <- _n + 1
#if !OPENSILVER
        childWindow.IsModal <- false
#endif
        childWindow.Show()
