namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type Location() as this =
    inherit LocationXaml()

    do
        this.InitializeComponent()

    member private this.GetButton_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Not implemented") |> ignore
