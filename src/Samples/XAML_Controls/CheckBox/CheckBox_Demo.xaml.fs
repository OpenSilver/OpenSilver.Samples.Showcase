namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type CheckBox_Demo() as this =
    inherit CheckBox_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.CheckBox_Checked(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You checked me.") |> ignore

    member this.CheckBox_Unchecked(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You unchecked me.") |> ignore
