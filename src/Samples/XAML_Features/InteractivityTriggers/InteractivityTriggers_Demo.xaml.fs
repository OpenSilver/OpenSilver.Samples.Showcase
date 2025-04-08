namespace OpenSilver.Samples.Showcase

open System
open System.Windows.Input
open System.Windows
open OpenSilver.Samples.Showcase.Search


type InteractivityTestViewModel() =
    let testCommand = InteractivityTestICommandClass()

    member this.TestCommand with get() = testCommand :> ICommand

and InteractivityTestICommandClass() =
    let canExecuteChanged = new Event<EventHandler, EventArgs>()

    interface ICommand with
        member this.CanExecute parameter = true

        member this.Execute parameter = 
            MessageBox.Show("The command was successfully executed.") |> ignore

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish        

[<SearchKeywords("interaction", "triggers", "behavior", "events", "UI")>]
type InteractivityTriggers_Demo() as this =
    inherit InteractivityTriggers_DemoXaml()

    do
        this.InitializeComponent()
        this.DataContext <- InteractivityTestViewModel()
