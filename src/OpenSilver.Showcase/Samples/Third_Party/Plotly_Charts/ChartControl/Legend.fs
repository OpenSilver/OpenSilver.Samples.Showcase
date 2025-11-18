namespace OpenSilver.Extensions.Plotly

open OpenSilver
open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks

type Legend() =
    // Set default values:
    member val X = 0.0 with get, set
    member val Y = 0.0 with get, set
    member val BgColor = "" with get, set
    member val BorderColor = "" with get, set

    member this.ToJavaScriptObject() =
        let jsLegend = Interop.ExecuteJavaScript("[]")

        Interop.ExecuteJavaScript("$0['x'] = $1;", jsLegend, InteropHelper.Unbox this.X) |> ignore
        Interop.ExecuteJavaScript("$0['y'] = $1;", jsLegend, InteropHelper.Unbox this.Y) |> ignore

        if not (String.IsNullOrEmpty this.BgColor) then
            Interop.ExecuteJavaScript("$0['bgcolor'] = $1;", jsLegend, this.BgColor) |> ignore

        if not (String.IsNullOrEmpty this.BorderColor) then
            Interop.ExecuteJavaScript("$0['bordercolor'] = $1;", jsLegend, this.BorderColor) |> ignore

        jsLegend
