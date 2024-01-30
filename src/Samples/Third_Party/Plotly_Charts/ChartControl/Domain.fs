namespace OpenSilver.Extensions.Plotly

open OpenSilver
open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks

type Domain() =
    member val X = [] : double list with get, set
    member val Y = [] : double list with get, set

    member this.ToJavaScriptObject() =
        let jsDomain = Interop.ExecuteJavaScript("[]")

        if this.X.Length <> 0 then
            let jsX = Interop.ExecuteJavaScript("[]")
            for d in this.X do
                Interop.ExecuteJavaScript("$0.push($1);", jsX, InteropHelper.Unbox(d)) |> ignore
            Interop.ExecuteJavaScript("$0['x'] = $1;", jsDomain, jsX) |> ignore

        if this.Y.Length <> 0 then
            let jsY = Interop.ExecuteJavaScript("[]")
            for d in this.Y do
                Interop.ExecuteJavaScript("$0.push($1);", jsY, InteropHelper.Unbox(d)) |> ignore
            Interop.ExecuteJavaScript("$0['y'] = $1;", jsDomain, jsY) |> ignore

        jsDomain
