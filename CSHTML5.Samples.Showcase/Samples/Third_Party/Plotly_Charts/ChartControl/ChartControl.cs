using CSHTML5;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CSHTML5.Extensions.Plotly
{
    //------------------------------------------------------------------------
    // INTRODUCTION:
    //
    // This is a C#-based wrapper around the JavaScript library "Plotly.js".
    // With such a "wrapper", it is possible to use the JavaScript library directly from C#,
    // as if it was a C# library.
    //
    // Documentation of this concept of "wrapper" can be found at:
    // http://cshtml5.com/links/how-to-create-extensions.aspx
    // and
    // http://cshtml5.com/links/how-to-call-javascript.aspx
    //
    // WHERE CAN I FIND THE DOCUMENTATION OF THE PLOTLY LIBRARY?
    //
    // Documentation of the JavaScript Plotly library can be found at:
    // - Plotly API Reference: https://plot.ly/javascript/reference/
    // - Plotly JS samples: https://plot.ly/javascript/#basic-charts 
    //------------------------------------------------------------------------


    public class ChartControl : Canvas
    {
        static bool JSLibraryWasLoaded; // Remembers whether the JS library was loaded in order not to load it twice:

        public ChartControl()
        {
            // Set default values:
            this.Data = new Data();
            this.Layout = new Layout();
        }

        /// <summary>
        /// Loads the "Plotly" JavaScript library if it is not already loaded.
        /// THIS METHOD MUST BE CALLED BEFORE "RENDER"
        /// </summary>
        public static async Task Initialize()
        {
            if (!JSLibraryWasLoaded)
            {
                // Load the "typedarray.js" polyfill for IE compatibility:
                await Interop.LoadJavaScriptFile("ms-appx:///CSHTML5.Samples.Showcase/Samples/Third_Party/Plotly_Charts/ChartControl/typedarray.js");

                // Load the "plotly.js" library:
                await Interop.LoadJavaScriptFile("ms-appx:///CSHTML5.Samples.Showcase/Samples/Third_Party/Plotly_Charts/ChartControl/plotly.min.js");

                // Remember that the libraries have been loaded in order to not load them again:
                JSLibraryWasLoaded = true;
            }
        }

        /// <summary>
        /// Renders the chart.
        /// </summary>
        public void Render()
        {
            // Get a reference to the HTML DOM representation of the control (must be in the Visual Tree):
            object div = Interop.GetDiv(this);

            // Make sure that the Div has an ID, because "Plotly" requires an ID:
            Interop.ExecuteJavaScript("if (!$0.id) { $0.id = $1 }", div, Guid.NewGuid().ToString());

            // Get the data, layout, and other JS objects:
            object jsData = this.Data.ToJavaScriptObject();
            object jsLayout = this.Layout.ToJavaScriptObject();

            // Render the control:
            Interop.ExecuteJavaScript(@"
                Plotly.newPlot($0.id, $1, $2);
                ", div, jsData, jsLayout);
        }

        public Data Data { get; set; }

        public Layout Layout { get; set; }
    }
}
