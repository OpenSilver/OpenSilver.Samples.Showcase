namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls

type RadioButton_Demo() as this =
    inherit RadioButton_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.RadioButton_Click(sender : obj, e : RoutedEventArgs) =
        this.Dispatcher.BeginInvoke(Action(fun () -> 
            MessageBox.Show(
                if this.RadioButton1.IsChecked.Value then "Option 1 selected" else "Option 2 selected"
            ) |> ignore
        )) |> ignore

