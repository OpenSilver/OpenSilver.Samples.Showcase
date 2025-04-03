namespace OpenSilver.Samples.Showcase.Search

open System
open System.Collections.Generic
open System.Linq
open System.Reflection

module SamplesInfoLoader =

    // Private mutable dictionary to store name -> type mapping
    let private controlTypeMap = Dictionary<string, Type>()

    let GetAllControls () : List<SearchableItem> =
        let controls = List<SearchableItem>()

        let types =
            Assembly.GetExecutingAssembly().GetTypes()
            |> Seq.filter (fun t -> t.IsClass && not (isNull (t.GetCustomAttribute(typeof<SearchKeywordsAttribute>))))

        for t in types do
            let attr = t.GetCustomAttribute(typeof<SearchKeywordsAttribute>) :?> SearchKeywordsAttribute
            let name = t.Name
            let keywords =
                if isNull attr then
                    List<string>()
                else
                    attr.Keywords |> Seq.toList |> ResizeArray

            let item = SearchableItem(Name = name, Keywords = keywords)
            controls.Add(item)
            controlTypeMap.[name] <- t

        controls

    let GetControlTypeByName (name: string) : Type =
        match controlTypeMap.TryGetValue(name) with
        | true, t -> t
        | _ -> null
