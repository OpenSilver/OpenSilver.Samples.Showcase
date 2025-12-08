namespace OpenSilver.Showcase

open System.Windows
open OpenSilver.Showcase.Search

[<SearchKeywords("print", "document", "preview")>]
type Printing_Demo() as this =
    inherit Printing_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonPrint_Click(sender : obj, e : RoutedEventArgs) =
        let invoiceToPrint = Invoice()
        CSHTML5.Native.Html.Printing.PrintManager.Print(invoiceToPrint)
