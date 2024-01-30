namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Input

type Keyboard_Demo() as this =
    inherit Keyboard_DemoXaml()

    do
        this.InitializeComponent()

    member private this.TextBoxInput_KeyDown(sender : obj, e : KeyEventArgs) =
        if e.Key = Key.Enter then
            MessageBox.Show(sprintf "You pressed Enter!%s%sThis is the text that you entered: %s" Environment.NewLine Environment.NewLine this.TextBoxInput.Text) |> ignore
