namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type PasswordBox_Demo() as this =
    inherit PasswordBox_DemoXaml()

    do
        this.InitializeComponent()

    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        this.DisplayPasswordIfNotEmpty()

    member private this.DisplayPasswordIfNotEmpty() =
        if this.PasswordBox.Password.Length <> 0 then
            MessageBox.Show($"The password typed is\n\"{this.PasswordBox.Password}\"") |> ignore
        else
            MessageBox.Show("Please enter a password") |> ignore
