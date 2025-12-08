namespace PreviewOnWinRT

open System.Windows
open System.Windows.Controls

type SmallChildWindow() as this =
    inherit SmallChildWindowXaml()

    do
        this.InitializeComponent()

    member private this.OKButton_Click(sender : obj, e : RoutedEventArgs) =
        this.DialogResult <- true

    member private this.CancelButton_Click(sender : obj, e : RoutedEventArgs) =
        this.DialogResult <- false
