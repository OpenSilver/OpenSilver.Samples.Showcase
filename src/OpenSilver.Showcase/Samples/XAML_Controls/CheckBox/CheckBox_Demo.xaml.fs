namespace OpenSilver.Showcase

open System.Windows
open System.Windows.Controls
open OpenSilver.Showcase.Search

[<SearchKeywords("input", "toggle", "boolean", "selection", "form")>]
type CheckBox_Demo() as this =
    inherit CheckBox_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.CheckBox_Checked(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You checked me.") |> ignore

    member this.CheckBox_Unchecked(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You unchecked me.") |> ignore
