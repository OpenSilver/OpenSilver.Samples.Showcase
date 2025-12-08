namespace OpenSilver.Showcase.Search

open System

[<AllowNullLiteral>]
[<AttributeUsage(AttributeTargets.Class, AllowMultiple = false)>]
type SearchKeywordsAttribute([<ParamArray>] keywords: string[]) =
    inherit Attribute()

    member _.Keywords = keywords
