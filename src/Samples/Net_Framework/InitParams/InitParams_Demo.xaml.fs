namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Data
open System.Windows.Input
open System.Windows.Media
open System.Windows.Navigation

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
