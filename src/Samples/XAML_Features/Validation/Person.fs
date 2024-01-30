namespace OpenSilver.Samples.Showcase

open System
open System.ComponentModel

type Person() =
    let mutable name = ""
    let mutable age = 1

    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()
    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

    member this.Name
        with get() = name
        and set value =
            if String.IsNullOrWhiteSpace(value) then
                raise (System.Exception("Name cannot be empty."))
            name <- value
            propertyChanged.Trigger(this, PropertyChangedEventArgs("Name"))

    member this.Age
        with get() = age
        and set value =
            if value <= 0 then
                raise (System.Exception("Age cannot be lower than 0."))
            age <- value
            propertyChanged.Trigger(this, PropertyChangedEventArgs("Age"))
