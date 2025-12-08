namespace OpenSilver.Samples.Showcase

open System.Windows
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("menu", "context", "right-click", "items", "commands", "options", "control", "separator")>]
type ContextMenu_Demo() as this =
    inherit ContextMenu_DemoXaml()

    do
        this.InitializeComponent()

    member private this.MenuItem1_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Menu Item 1") |> ignore

    member private this.MenuItem2_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Menu Item 2") |> ignore

    member private this.MenuItem3_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Menu Item 3") |> ignore
