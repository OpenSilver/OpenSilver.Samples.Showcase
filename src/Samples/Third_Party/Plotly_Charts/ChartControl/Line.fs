namespace OpenSilver.Extensions.Plotly

open System
open OpenSilver

type Line() =
    // Set default values:
    member val Width = 0.0 with get, set

    member val Color : string = "" with get, set

    member this.ToJavaScriptObject() =
        let jsLine = Interop.ExecuteJavaScript("[]")

        if not (String.IsNullOrEmpty this.Color) then
            Interop.ExecuteJavaScript("$0['color'] = $1;",
                jsLine,
                this.Color) |> ignore

        Interop.ExecuteJavaScript("$0['width'] = $1;",
            jsLine,
            InteropHelper.Unbox this.Width) |> ignore

        jsLine
