namespace OpenSilver.Extensions.Plotly

open System
open OpenSilver

type Axis() as this =
    // Set default values:
    member val Title = "" with get, set
    member val Tickangle = 0 with get, set
    member val ZeroLine = true with get, set
    member val GrigWidth = 1 with get, set
    member val TickFont = new Font() with get, set
    member val TitleFont = new Font() with get, set

    member this.ToJavascriptObject() =
        let jsAxis = Interop.ExecuteJavaScript("[]")

        if not (String.IsNullOrEmpty this.Title) then
            Interop.ExecuteJavaScript("$0['title'] = $1;",
                jsAxis,
                this.Title) |> ignore

        Interop.ExecuteJavaScript("$0['tickangle'] = $1;",
            jsAxis,
            InteropHelper.Unbox this.Tickangle) |> ignore

        Interop.ExecuteJavaScript("$0['zeroline'] = $1;",
            jsAxis,
            InteropHelper.Unbox this.ZeroLine) |> ignore

        Interop.ExecuteJavaScript("$0['grigwidth'] = $1;",
            jsAxis,
            InteropHelper.Unbox this.GrigWidth) |> ignore

        Interop.ExecuteJavaScript("$0['tickfont'] = $1;",
            jsAxis,
            this.TickFont.ToJavaScriptObject()) |> ignore

        Interop.ExecuteJavaScript("$0['titlefont'] = $1;",
            jsAxis,
            this.TitleFont.ToJavaScriptObject()) |> ignore

        jsAxis

