namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type PropertyChangedTrigger_Demo() as this =
    inherit PropertyChangedTrigger_DemoXaml()

    do
        this.InitializeComponent(); 

    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        this.borderText.Text <- if this.borderText.Text = "Yellow" then "Orange" else "Yellow"
