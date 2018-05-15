using CSHTML5;
using System.Collections.Generic;

namespace CSHTML5.Extensions.Plotly
{
    public class Marker
    {
        public Marker()
        {
            // Set default values:
            this.Size = 6;
            this.Opacity = 1.0;
        }

        public int Size { get; set; }

        public object Color { get; set; }

        public List<string> Colors { get; set; }

        public double Opacity { get; set; }

        public Line Line { get; set; }

        public object ToJavaScriptObject()
        {
            var jsMarker = Interop.ExecuteJavaScript(@"new Object()");

            Interop.ExecuteJavaScript(@"
                      $0['size'] = $1;
                ",
                 jsMarker,
                 this.Size);

            if (this.Color is string && !string.IsNullOrEmpty((string)this.Color))
            {
                Interop.ExecuteJavaScript("$0['color'] = $1;",
                    jsMarker,
                    this.Color);
            }
            else if (this.Color is List<string> && this.Color != null)
            {
                Interop.ExecuteJavaScript("$0['color'] = [];", jsMarker);

                foreach (string Color in (List<string>)this.Color)
                {
                    Interop.ExecuteJavaScript("$0['color'].push($1);",
                        jsMarker,
                        Color);
                }
            }

            Interop.ExecuteJavaScript("$0['opacity'] = $1;",
                jsMarker,
                this.Opacity);

            if (this.Line != null)
            {
                Interop.ExecuteJavaScript("$0['line'] = $1;",
                    jsMarker,
                    this.Line.ToJavaScriptObject());
            }

            if (this.Colors != null)
            {
                Interop.ExecuteJavaScript("$0['colors'] = [];", jsMarker);

                foreach (string Color in this.Colors)
                {
                    Interop.ExecuteJavaScript("$0['colors'].push($1);",
                        jsMarker,
                        Color);
                }
            }

            return jsMarker;
        }
    }
}
