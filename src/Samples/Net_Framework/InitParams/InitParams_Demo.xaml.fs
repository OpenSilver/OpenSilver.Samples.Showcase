namespace OpenSilver.Samples.Showcase

open System.Windows
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("initialization", "parameters", "startup", "configuration", "settings")>]
type InitParams_Demo() as this =
    inherit InitParams_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonShowInitParams_Click(sender: obj, e: RoutedEventArgs) =
        let parameters = Application.Current.Host.InitParams
        let mutable initParamsString = "I found this in init param:"

        for param in parameters do
            initParamsString <- initParamsString + sprintf "\r\nkey: %s, value: %s" param.Key param.Value

        MessageBox.Show(initParamsString) |> ignore
