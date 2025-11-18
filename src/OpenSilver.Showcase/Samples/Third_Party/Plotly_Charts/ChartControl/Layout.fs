namespace OpenSilver.Extensions.Plotly

open OpenSilver
open System
open System.Collections.Generic

type Layout() =
    // Set default values:
    member val BarMode = BarMode.Group with get, set
    member val Title = "" with get, set
    member val Xaxis = Axis() with get, set
    member val Yaxis = Axis() with get, set
    member val Annotations = [] : Annotation list with get, set
    member val ShowLegend = true with get, set
    member val Font = Font() with get, set
    member val BarGap = 0.0 with get, set
    member val Legend = Legend() with get, set
    member val BarGroupGap = 0.0 with get, set
    member val PaperBgColor = "" with get, set
    member val PlotBgColor = "" with get, set
    member val Width = 600 with get, set
    member val Height = 600 with get, set

    member this.ToJavaScriptObject() =
        let jsLayout = Interop.ExecuteJavaScript(@"new Object()")

        Interop.ExecuteJavaScript(@"
                $0['barmode'] = $1;
            ",
             jsLayout,
             this.BarMode.ToString().ToLower()) |> ignore

        if not (String.IsNullOrEmpty this.Title) then
            Interop.ExecuteJavaScript("$0['title'] = $1;", jsLayout, this.Title) |> ignore

        //if this.Xaxis <> null then
        Interop.ExecuteJavaScript("$0['xaxis'] = $1;", jsLayout, this.Xaxis.ToJavascriptObject()) |> ignore

        //if this.Yaxis <> null then
        Interop.ExecuteJavaScript("$0['yaxis'] = $1;", jsLayout, this.Yaxis.ToJavascriptObject()) |> ignore

        if this.Annotations.Length <> 0 then
            Interop.ExecuteJavaScript("$0['annotations'] = [];", jsLayout) |> ignore

            for annotation in this.Annotations do
                Interop.ExecuteJavaScript("$0['annotations'].push($1);", jsLayout, annotation.ToJavaScriptObject()) |> ignore

        //if this.Font <> null then
        Interop.ExecuteJavaScript("$0['font'] = $1;", jsLayout, this.Font.ToJavaScriptObject()) |> ignore

        Interop.ExecuteJavaScript("$0['showlegend'] = $1;", jsLayout, InteropHelper.Unbox this.ShowLegend) |> ignore

        //if this.Legend <> null then
        Interop.ExecuteJavaScript("$0['legend'] = $1;", jsLayout, this.Legend.ToJavaScriptObject()) |> ignore

        Interop.ExecuteJavaScript("$0['bargroupgap'] = $1;", jsLayout, InteropHelper.Unbox this.BarGroupGap) |> ignore

        if not (String.IsNullOrEmpty this.PaperBgColor) then
            Interop.ExecuteJavaScript("$0['paper_bgcolor'] = $1;", jsLayout, this.PaperBgColor) |> ignore

        if not (String.IsNullOrEmpty this.PlotBgColor) then
            Interop.ExecuteJavaScript("$0['plot_bgcolor'] = $1;", jsLayout, this.PlotBgColor) |> ignore

        Interop.ExecuteJavaScript("$0['width'] = $1", jsLayout, InteropHelper.Unbox this.Width) |> ignore
        Interop.ExecuteJavaScript("$0['height'] = $1", jsLayout, InteropHelper.Unbox this.Height) |> ignore

        jsLayout
