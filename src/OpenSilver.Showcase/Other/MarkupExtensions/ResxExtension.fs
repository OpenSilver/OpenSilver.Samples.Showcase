namespace OpenSilver.Showcase

open System
open System.Windows.Markup

[<ContentProperty("Key")>]
type ResxExtension() =
    inherit MarkupExtension()

    let mutable key = ""

    member this.Key
        with get() = key
        and  set(value) = key <- value

    override this.ProvideValue(serviceProvider: IServiceProvider) : obj = []
        //SampleResourceFile.ResourceManager.GetString(key) :> obj
