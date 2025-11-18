namespace OpenSilver.Showcase

open System
open System.Windows
open OpenSilver.Showcase.Search

[<SearchKeywords("error handling", "exception", "debugging", "logging")>]
type UnhandledException_Demo() as this =
    inherit UnhandledException_DemoXaml()

    do
        Application.Current.UnhandledException.Add(fun args -> UnhandledException_Demo.OnUnhandledException(this, args))

    do
        this.InitializeComponent()

    member private this.ButtonThrowException_Click(sender: obj, e: RoutedEventArgs) =
        raise (Exception("This exception was thrown outside of a Try/Catch statement and handled using UnhandledException"))
        ()

    static member private OnUnhandledException(sender: obj, e: ApplicationUnhandledExceptionEventArgs) =
        let mutable exceptionStackMessages = "Received an unhandled Exception: "
        let mutable ex = e.ExceptionObject
        let mutable spacing = "  "
        while ex <> null do
            exceptionStackMessages <- exceptionStackMessages + Environment.NewLine + spacing + "-" + ex.GetType().Name + ": " + ex.Message
            spacing <- spacing + "  "
            ex <- ex.InnerException
        MessageBox.Show(exceptionStackMessages) |> ignore
