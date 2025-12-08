namespace OpenSilver.Showcase

open System.Windows
open System.Windows.Controls
open OpenSilver.Showcase.Search

[<SearchKeywords("click", "interaction", "command")>]
type Button_Demo() as this =
    inherit Button_DemoXaml()

    do
        this.InitializeComponent()

    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You clicked the button!") |> ignore
