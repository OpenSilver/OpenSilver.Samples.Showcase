namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Collections.ObjectModel
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

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
