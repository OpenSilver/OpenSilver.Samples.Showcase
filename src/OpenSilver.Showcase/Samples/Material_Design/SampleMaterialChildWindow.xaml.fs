namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls
open System.Collections.ObjectModel

type SampleMaterialChildWindow() as this =
    inherit SampleMaterialChildWindowXaml()

    do
        this.InitializeComponent()
        let items = ObservableCollection<string>(["Item 1"; "Item 2"; "Item 3"])
        this.DataContext <- items

    member this.ButtonOK_Click(sender: obj, e: RoutedEventArgs) =
        this.DialogResult <- true

    member this.ButtonCancel_Click(sender: obj, e: RoutedEventArgs) =
        this.DialogResult <- true
