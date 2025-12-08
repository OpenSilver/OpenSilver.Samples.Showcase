namespace OpenSilver.Showcase

open System
open System.Diagnostics
open System.Windows
open OpenSilver.Showcase.Search

[<SearchKeywords("console", "output", "debugging", "logging")>]
type Console_Demo() as this =
    inherit Console_DemoXaml()

    do this.InitializeComponent()

    member _.OnWriteToConsoleButtonClick(_sender: obj, _e: RoutedEventArgs) =
        Console.WriteLine($"Console test message {DateTime.Now}")

    member _.OnWriteToDebugButtonClick(_sender: obj, _e: RoutedEventArgs) =
        Debug.WriteLine($"Debug test message {DateTime.Now}")
