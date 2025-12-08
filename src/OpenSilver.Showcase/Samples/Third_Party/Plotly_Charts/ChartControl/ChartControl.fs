namespace OpenSilver.Extensions.Plotly

open OpenSilver
open System
open System.Windows.Controls

//------------------------------------------------------------------------
// INTRODUCTION:
//
// This is a C#-based wrapper around the JavaScript library "Plotly.js".
// With such a "wrapper", it is possible to use the JavaScript library directly from C#,
// as if it was a C# library.
//
// Documentation of this concept of "wrapper" can be found at:
// http://opensilver.net/links/how-to-create-extensions.aspx
// and
// http://opensilver.net/links/how-to-call-javascript.aspx
//
// WHERE CAN I FIND THE DOCUMENTATION OF THE PLOTLY LIBRARY?
//
// Documentation of the JavaScript Plotly library can be found at:
// - Plotly API Reference: https://plot.ly/javascript/reference/
// - Plotly JS samples: https://plot.ly/javascript/#basic-charts 
//------------------------------------------------------------------------

type ChartControl() =
    inherit Canvas()

    static let mutable JSLibraryWasLoaded = false

    member val Data = Data() with get, set
    member val Layout = Layout() with get, set

    /// <summary>
    /// Loads the "Plotly" JavaScript library if it is not already loaded.
    /// THIS METHOD MUST BE CALLED BEFORE "RENDER"
    /// </summary>
    static member Initialize() =
        async {
            if not JSLibraryWasLoaded then
                // Load the "typedarray.js" polyfill for IE compatibility:
                let! result1 = Interop.LoadJavaScriptFile("ms-appx:///OpenSilver.Samples.Showcase/Samples/Third_Party/Plotly_Charts/ChartControl/typedarray.js") |> Async.AwaitTask

                // Load the "plotly.js" library:
                let! result2 = Interop.LoadJavaScriptFile("ms-appx:///OpenSilver.Samples.Showcase/Samples/Third_Party/Plotly_Charts/ChartControl/plotly.min.js") |> Async.AwaitTask

                // Remember that the libraries have been loaded in order to not load them again:
                JSLibraryWasLoaded <- true
        }

    /// <summary>
    /// Renders the chart.
    /// </summary>
    member this.Render() =
        // Get a reference to the HTML DOM representation of the control (must be in the Visual Tree):
        let div = Interop.GetDiv(this)

        // Make sure that the Div has an ID, because "Plotly" requires an ID:
        Interop.ExecuteJavaScript("if (!$0.id) { $0.id = $1 }", div, Guid.NewGuid().ToString()) |> ignore

        // Get the data, layout, and other JS objects:
        let jsData = this.Data.ToJavaScriptObject()
        let jsLayout = this.Layout.ToJavaScriptObject()

        // Render the control:
        Interop.ExecuteJavaScript(@"
            Plotly.newPlot($0.id, $1, $2);
            ", div, jsData, jsLayout) |> ignore
