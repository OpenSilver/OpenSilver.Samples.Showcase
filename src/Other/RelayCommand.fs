namespace OpenSilver.Samples.Showcase

open System
open System.Windows.Input

/// Interface extending ICommand with external CanExecuteChanged trigger
type IRelayCommand =
    inherit ICommand
    abstract member NotifyCanExecuteChanged: unit -> unit

/// Non-generic RelayCommand implementation
type RelayCommand(execute: Action, ?canExecute: Func<bool>) =
    let canExec = defaultArg canExecute null
    let canExecuteChanged = new Event<EventHandler, EventArgs>()

    do
        if isNull execute then raise (ArgumentNullException(nameof execute))

    interface IRelayCommand with
        member _.NotifyCanExecuteChanged() =
            canExecuteChanged.Trigger(null, EventArgs.Empty)

    interface ICommand with
        member _.CanExecute(_param: obj) =
            match canExec with
            | null -> true
            | f -> f.Invoke()

        member _.Execute(_param: obj) =
            execute.Invoke()

        [<CLIEvent>]
        member _.CanExecuteChanged = canExecuteChanged.Publish

/// Generic relay command interface
type IRelayCommand<'T> =
    inherit IRelayCommand
    abstract member CanExecute: 'T -> bool
    abstract member Execute: 'T -> unit

/// Generic RelayCommand implementation
type RelayCommand<'T>(execute: Action<'T>, ?canExecute: Predicate<'T>) =
    let canExec = defaultArg canExecute null
    let canExecuteChanged = new Event<EventHandler, EventArgs>()

    do
        if isNull execute then raise (ArgumentNullException(nameof execute))

    interface IRelayCommand<'T> with
        member _.CanExecute(parameter: 'T) =
            match canExec with
            | null -> true
            | pred -> pred.Invoke(parameter)

        member _.Execute(parameter: 'T) =
            execute.Invoke(parameter)

    interface IRelayCommand with
        member _.NotifyCanExecuteChanged() =
            canExecuteChanged.Trigger(null, EventArgs.Empty)

    interface ICommand with
        member this.CanExecute(parameter: obj) =
            match parameter with
            | null when typeof<'T>.IsValueType && Nullable.GetUnderlyingType(typeof<'T>) = null ->
                false
            | :? 'T as typed -> (this :> IRelayCommand<'T>).CanExecute(typed)
            | _ -> raise (ArgumentException(sprintf "Invalid parameter type. Expected %s." typeof<'T>.Name))

        member this.Execute(parameter: obj) =
            match parameter with
            | :? 'T as typed -> (this :> IRelayCommand<'T>).Execute(typed)
            | _ -> raise (ArgumentException(sprintf "Invalid parameter type. Expected %s." typeof<'T>.Name))

        [<CLIEvent>]
        member _.CanExecuteChanged = canExecuteChanged.Publish
