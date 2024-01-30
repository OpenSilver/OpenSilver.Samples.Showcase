namespace OpenSilver.Extensions.Plotly

open OpenSilver
open System.Collections.Generic
open System

type Trace() =
    member val Name = "" with get, set
    member val X = [] : obj list with get, set
    member val Y = [] : obj list with get, set
    member val Text : option<obj> = None with get, set
    member val Values = [] : obj list with get, set
    member val Labels = [] : string list with get, set
    member val Type = TraceType.Bar with get, set
    member val Mode = TraceMode.Lines with get,set
    member val Marker = Marker() with get, set
    member val Domain = Domain() with get, set
    member val HoverInfo = "" with get, set
    member val TextInfo = "" with get, set
    member val Hole = 0.0 with get, set
    member val TextPosition = "" with get, set

    member this.ToJavaScriptObject() =
        let jsX = Interop.ExecuteJavaScript("[]")
        for xPoint in this.X do
            Interop.ExecuteJavaScript("$0.push($1)", jsX, InteropHelper.Unbox xPoint) |> ignore

        let jsY = Interop.ExecuteJavaScript("[]")
        for yPoint in this.Y do
            Interop.ExecuteJavaScript("$0.push($1)", jsY, InteropHelper.Unbox yPoint) |> ignore

        let jsText = Interop.ExecuteJavaScript("[]")
        if this.Text <> None then
            let value = Option.defaultValue (box null) this.Text

            match value with
            | :? string as strText ->
                Interop.ExecuteJavaScript("$0 = $1;", jsText, strText) |> ignore
            | :?  List<string> as listText ->
                for text in listText do
                    Interop.ExecuteJavaScript("$0.push($1)", jsText, text) |> ignore
            | _ ->
                ()

        let jsTrace = Interop.ExecuteJavaScript("new Object()")
        Interop.ExecuteJavaScript(@"
                      $0['x'] = $1;
                      $0['y'] = $2;
                      $0['type'] = $3;
                      $0['mode'] = $4;
                      if ($5 != '') {
                        $0['name'] = $5;
                      }
                      if ($6.length == 1) {
                        $0['text'] = $6[0];
                      } else if ($6.length > 1) {
                        $0['text'] = $6;
                      }
                ", jsTrace, jsX, jsY, this.Type.ToString().ToLower(), TraceModeExtensions.ToJavaScriptString(this.Mode), this.Name, jsText) |> ignore

        Interop.ExecuteJavaScript("$0['marker'] = $1;", jsTrace, this.Marker.ToJavaScriptObject()) |> ignore

        let jsValues = Interop.ExecuteJavaScript("[]")
        for valObj in this.Values do
            Interop.ExecuteJavaScript("$0.push($1);", jsValues, InteropHelper.Unbox valObj) |> ignore
        Interop.ExecuteJavaScript("$0['values'] = $1;", jsTrace, jsValues) |> ignore

        let jsLabels = Interop.ExecuteJavaScript("[]")
        for lbl in this.Labels do
            Interop.ExecuteJavaScript("$0.push($1);", jsLabels, lbl) |> ignore
        Interop.ExecuteJavaScript("$0['labels'] = $1;", jsTrace, jsLabels) |> ignore

        Interop.ExecuteJavaScript("$0['domain'] = $1;", jsTrace, this.Domain.ToJavaScriptObject()) |> ignore

        if not (String.IsNullOrEmpty this.HoverInfo) then
            Interop.ExecuteJavaScript("$0['hoverinfo'] = $1;", jsTrace, this.HoverInfo) |> ignore

        if not (String.IsNullOrEmpty this.TextInfo) then
            Interop.ExecuteJavaScript("$0['textinfo'] = $1;", jsTrace, this.TextInfo) |> ignore

        Interop.ExecuteJavaScript("$0['hole'] = $1;", jsTrace, InteropHelper.Unbox this.Hole) |> ignore

        if not (String.IsNullOrEmpty this.TextPosition) then
            Interop.ExecuteJavaScript("$0['textposition'] = $1;", jsTrace, this.TextPosition) |> ignore

        jsTrace
