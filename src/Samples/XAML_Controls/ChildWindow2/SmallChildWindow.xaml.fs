namespace PreviewOnWinRT

open System
open System.Collections.Generic
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
open Windows.UI.Xaml.Navigation; 
#endif

type SmallChildWindow() as this =
    inherit SmallChildWindowXaml()

    do
        this.InitializeComponent()

    member private this.OKButton_Click(sender : obj, e : RoutedEventArgs) =
        this.DialogResult <- true

    member private this.CancelButton_Click(sender : obj, e : RoutedEventArgs) =
        this.DialogResult <- false
