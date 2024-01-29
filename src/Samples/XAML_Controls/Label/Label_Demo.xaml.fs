namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type Label_Demo() as this =
    inherit Label_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.ButtonViewMore_Click(sender : obj, e : RoutedEventArgs) =
        //this.ChildWindowHelper.ShowChildWindow(Button_Demo_More())
        ()

    member private this.Button_Click(sender : obj, e : RoutedEventArgs) =
        MessageBox.Show("You clicked the button!") |> ignore
