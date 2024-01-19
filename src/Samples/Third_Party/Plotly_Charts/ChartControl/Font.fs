namespace OpenSilver.Extensions.Plotly

open System
open OpenSilver
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.UI
open Windows.UI.Xaml
#endif

type Font() as this =
    // Set default values:
    member val Family = "" with get, set
    member val Size = 14 with get, set
    member val Color = "" with get, set

    member this.ToJavaScriptObject() =
        let jsFont = Interop.ExecuteJavaScript("[]")

        if not (String.IsNullOrEmpty this.Family) then
            Interop.ExecuteJavaScript("$0['family'] = $1;",
                jsFont,
                this.Family) |> ignore

        Interop.ExecuteJavaScript("$0['size'] = $1;",
            jsFont,
            InteropHelper.Unbox this.Size) |> ignore

        if not (String.IsNullOrEmpty this.Color) then
            Interop.ExecuteJavaScript("$0['color'] = $1;",
                jsFont,
                this.Color) |> ignore

        jsFont

