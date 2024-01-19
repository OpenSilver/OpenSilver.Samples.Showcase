namespace OpenSilver.Extensions.Plotly

open OpenSilver

open System
open System.Collections.Generic

type Marker() =
    // Set default values:
    member val Size = 6 with get, set
    member val Color : option<obj> = None with get, set
    member val Colors = [] : string list with get, set
    member val Opacity = 1.0 with get, set
    member val Line = Line() with get, set

    member this.ToJavaScriptObject() =
        let jsMarker = Interop.ExecuteJavaScript("new Object()")

        Interop.ExecuteJavaScript("$0['size'] = $1;", jsMarker, InteropHelper.Unbox this.Size) |> ignore

        let objVal : Option<obj> = None

        if this.Color <> None then
            let value = Option.defaultValue (box null) this.Color

            match value with
            | :? string as strColor ->
                Interop.ExecuteJavaScript("$0['color'] = $1;", jsMarker, strColor) |> ignore
            | :?  List<string> as listColor ->
                Interop.ExecuteJavaScript("$0['color'] = [];", jsMarker) |> ignore
                for color in listColor do
                    Interop.ExecuteJavaScript("$0['color'].push($1);", jsMarker, color) |> ignore
            | _ ->
                ()

        Interop.ExecuteJavaScript("$0['opacity'] = $1;", jsMarker, InteropHelper.Unbox this.Opacity) |> ignore

        //if this.Line <> null then
        Interop.ExecuteJavaScript("$0['line'] = $1;", jsMarker, this.Line.ToJavaScriptObject()) |> ignore

        //if this.Colors <> null then
        Interop.ExecuteJavaScript("$0['colors'] = [];", jsMarker) |> ignore
        for color in this.Colors do
                    Interop.ExecuteJavaScript("$0['colors'].push($1);", jsMarker, color) |> ignore

        jsMarker

