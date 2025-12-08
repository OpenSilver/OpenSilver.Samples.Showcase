namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type jQueryAjax_Demo() as this = 
    inherit jQueryAjax_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.Button_Click (sender: obj, e: RoutedEventArgs) =
        async {
            try
                let! result = OpenSilver.Extensions.jQueryAjaxHelper.MakeAjaxCall("http://fiddle.jshell.net/echo/html/", "some sample text", "post")
                MessageBox.Show("The server returned the following result: " + result) |> ignore
            with
            | ex -> MessageBox.Show(ex.Message) |> ignore
        } |> Async.Start

