namespace OpenSilver.Samples.Showcase.Search

open System.Collections.Generic

/// <summary>
/// Represents data used to search for a Sample by the search function.
/// </summary>
type SearchableItem() =
    /// <summary>
    /// The name of the sample to search for.
    /// </summary>
    member val Name: string = null with get, set

    /// <summary>
    /// The keywords the sample can be associated with.
    /// </summary>
    member val Keywords: List<string> = List<string>() with get, set
