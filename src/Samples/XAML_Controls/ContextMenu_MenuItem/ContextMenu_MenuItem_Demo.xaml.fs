namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type ContextMenu_MenuItem_Demo() as this =
    inherit ContextMenu_MenuItem_DemoXaml()

    do
        this.InitializeComponent()

    member private this.MenuItem1_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Menu Item 1") |> ignore

    member private this.MenuItem2_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Menu Item 2") |> ignore

    member private this.MenuItem3_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Menu Item 3") |> ignore
