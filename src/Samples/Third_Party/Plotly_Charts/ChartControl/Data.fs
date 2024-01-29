namespace OpenSilver.Extensions.Plotly

open OpenSilver
open System.Collections.Generic

type Data() =
    // Set default values:
    member val Traces = [] : Trace list with get, set

    member this.ToJavaScriptObject() =
        // Create the JS array:
        let jsArray = Interop.ExecuteJavaScript("[]")

        // Iterate through traces and add them to the array:
        for trace in this.Traces do
            let jsTrace = trace.ToJavaScriptObject()
            Interop.ExecuteJavaScript("$0.push($1)", jsArray, jsTrace) |> ignore

        jsArray
