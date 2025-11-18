namespace OpenSilver.Extensions.Plotly

open System
open System.Collections.Generic

[<Flags>]
type TraceMode =
    | None = 0
    | Lines = 1
    | Markers = 2
    | Text = 4

module TraceModeExtensions =
    let ToJavaScriptString(traceMode: TraceMode) =
        match traceMode with
        | TraceMode.None -> "none"
        | _ ->
            let mutable result = List.empty<string>
            let allOptions = Enum.GetValues(typeof<TraceMode>)
            for option in allOptions do
                if traceMode.HasFlag(option :?> TraceMode) then
                    result <- List.append result [option.ToString()]
            
            String.Join("+", result).ToLower()
