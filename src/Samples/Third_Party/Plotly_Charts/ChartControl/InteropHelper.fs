namespace OpenSilver.Extensions.Plotly

open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks

#nowarn "0044" // Suppress warning FS0044 (about deprecated JSIL.Meta.JSReplacement)

module InteropHelper =

#if !BRIDGE
    [<JSIL.Meta.JSReplacement("$value")>]
#else
    [<Bridge.Template("({value}.v != undefined ? {value}.v : {value})")>]
#endif
    let Unbox (value : obj) =
        value
