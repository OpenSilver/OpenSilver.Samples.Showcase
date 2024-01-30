namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type Button_Demo() as this =
    inherit Button_DemoXaml()

    do
        this.InitializeComponent()

    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You clicked the button!") |> ignore
