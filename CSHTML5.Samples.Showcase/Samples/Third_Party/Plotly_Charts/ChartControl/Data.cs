using CSHTML5;
using System.Collections.Generic;

namespace CSHTML5.Extensions.Plotly
{
    public class Data
    {
        public Data()
        {
            // Set default values:
            this.Traces = new List<Trace>();
        }

        public List<Trace> Traces { get; set; }

        public object ToJavaScriptObject()
        {
            // Create the JS array:
            var jsArray = Interop.ExecuteJavaScript("[]"); 

            foreach (var trace in this.Traces)
            {
                var jsTrace = trace.ToJavaScriptObject();
                Interop.ExecuteJavaScript("$0.push($1)", jsArray, jsTrace);
            }

            return jsArray;
        }
    }
}
