namespace OpenSilver.Samples.Showcase

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
open Windows.UI.Xaml.Navigation
#endif

type Printing_Demo() as this =
    inherit Printing_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonPrint_Click(sender : obj, e : RoutedEventArgs) =
        let invoiceToPrint = Invoice()
        CSHTML5.Native.Html.Printing.PrintManager.Print(invoiceToPrint)

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Printing_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Printing/Printing_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Printing_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Printing/Printing_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Printing_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Printing/Printing_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Printing_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Printing/Printing_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "Invoice.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Printing/Invoice.xaml")
        ])
