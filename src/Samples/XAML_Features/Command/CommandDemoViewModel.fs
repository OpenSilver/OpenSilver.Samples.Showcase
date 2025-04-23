namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open CommunityToolkit.Mvvm.ComponentModel
open CommunityToolkit.Mvvm.Input

type CommandDemoViewModel() as this =
    inherit ObservableObject()

    let showMessageCommand =
        RelayCommand<string>(
            (fun msg -> this.ShowMessage(msg)),
            (fun s -> not (String.IsNullOrWhiteSpace(s)))
        )

    let mutable message = "Message"

    member this.ShowMessageCommand: IRelayCommand = showMessageCommand

    member this.Message
        with get() = message
        //and set(value) =
        //    if this.SetProperty(&message, value) then
        //        showMessageCommand.NotifyCanExecuteChanged()

    member private _.ShowMessage(msg: string) =
        MessageBox.Show($"Command is executed: {msg}") |> ignore
