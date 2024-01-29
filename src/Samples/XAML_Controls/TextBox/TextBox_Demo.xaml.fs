namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type TextBox_Demo() as this =
    inherit TextBox_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.OKButton_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("Your name is: " + this.TextBoxName.Text) |> ignore
