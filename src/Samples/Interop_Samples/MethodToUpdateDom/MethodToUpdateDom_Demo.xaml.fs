namespace OpenSilver.Samples.Showcase

open OpenSilver
open System.Windows
open System.Windows.Controls

type MethodToUpdateDom_Demo() as this =
    inherit MethodToUpdateDom_DemoXaml()

    do
        this.InitializeComponent()
        
    member private this.ButtonSetCSS_Click(sender: obj, e: RoutedEventArgs) =
        let div = Interop.GetDiv(this)

        Interop.ExecuteJavaScript("$0.style.textDecoration = 'line-through'", div) |> ignore
