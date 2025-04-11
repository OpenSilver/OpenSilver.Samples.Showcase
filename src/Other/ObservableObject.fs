namespace OpenSilver.Samples.Showcase

open System.Collections.Generic
open System.ComponentModel
open System.Runtime.CompilerServices

[<AbstractClass>]
type ObservableObject() =

    let propertyChanged = new Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member _.PropertyChanged = propertyChanged.Publish

    member this.OnPropertyChanged([<CallerMemberName>] ?propertyName: string) =
        let name = defaultArg propertyName null
        propertyChanged.Trigger(this, PropertyChangedEventArgs(name))

    member this.SetProperty<'T>(field: byref<'T>, newValue: 'T, [<CallerMemberName>] ?propertyName: string) : bool =
        if EqualityComparer<'T>.Default.Equals(field, newValue) then
            false
        else
            field <- newValue
            this.OnPropertyChanged(?propertyName = propertyName)
            true
