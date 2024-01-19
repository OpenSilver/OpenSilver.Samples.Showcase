namespace OpenSilver.Samples.Showcase

open OpenSilver.Extensions.FileSystem
open System
open System.Collections.Generic
open System.ComponentModel
open System.IO
open System.Linq
open System.Threading.Tasks
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

open System.ComponentModel

type Person() =
    let mutable name = ""
    let mutable age = 1

    let propertyChanged = Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()
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
