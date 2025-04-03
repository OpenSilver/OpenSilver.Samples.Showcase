namespace OpenSilver.Samples.Showcase.Search

open System
open System.Collections.Generic
open System.Linq

module ControlSearch =

    let private controls: List<SearchableItem> = SamplesInfoLoader.GetAllControls()

    let SearchKeywords: string[] =
        controls
        |> Seq.collect (fun x -> seq { yield! x.Keywords; yield x.Name })
        |> Seq.map (fun s -> s.ToLowerInvariant().Replace("_demo", ""))
        |> Seq.distinct
        |> Seq.sort
        |> Seq.toArray

    let Search (query: string) : List<SearchableItem> =
        let query = query.ToLowerInvariant()

        controls
        |> Seq.filter (fun control ->
            control.Name.ToLowerInvariant().Contains(query) ||
            control.Keywords |> Seq.exists (fun k -> k.ToLowerInvariant().Contains(query))
        )
        |> Seq.toList
        |> fun lst -> List<SearchableItem>(lst)
