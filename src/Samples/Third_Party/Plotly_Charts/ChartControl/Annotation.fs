namespace OpenSilver.Extensions.Plotly

open System
open OpenSilver

type Annotation() as this =
    // Set default values:
    member val X = Unchecked.defaultof<_> with get, set
    member val Y = Unchecked.defaultof<_> with get, set
    member val Text = "" with get, set
    member val Xanchor = "" with get, set
    member val Yanchor = "" with get, set
    member val ShowArrow = true with get, set
    member val Font = Unchecked.defaultof<Font> with get, set

    member this.ToJavaScriptObject() =
        let jsAnnotation = Interop.ExecuteJavaScript("[]")

        if this.X <> null then
            Interop.ExecuteJavaScript("$0['x'] = $1;",
                jsAnnotation,
                InteropHelper.Unbox this.X) |> ignore

        if this.Y <> null then
            Interop.ExecuteJavaScript("$0['y'] = $1;",
                jsAnnotation,
                InteropHelper.Unbox this.Y) |> ignore

        if not (String.IsNullOrEmpty this.Text) then
            Interop.ExecuteJavaScript("$0['text'] = $1;",
                jsAnnotation,
                this.Text) |> ignore

        if not (String.IsNullOrEmpty this.Xanchor) then
            Interop.ExecuteJavaScript("$0['xanchor'] = $1;",
                jsAnnotation,
                this.Xanchor) |> ignore

        if not (String.IsNullOrEmpty this.Yanchor) then
            Interop.ExecuteJavaScript("$0['yanchor'] = $1;",
                jsAnnotation,
                this.Yanchor) |> ignore

        Interop.ExecuteJavaScript("$0['showarrow'] = $1;",
            jsAnnotation,
            InteropHelper.Unbox this.ShowArrow) |> ignore

        Interop.ExecuteJavaScript("$0['font'] = $1;",
            jsAnnotation,
            this.Font.ToJavaScriptObject()) |> ignore

        jsAnnotation
