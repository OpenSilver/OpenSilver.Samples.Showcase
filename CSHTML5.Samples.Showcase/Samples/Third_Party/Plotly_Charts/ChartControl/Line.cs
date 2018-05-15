using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSHTML5;

namespace CSHTML5.Extensions.Plotly
{
    public class Line
    {
        public Line()
        {
            // Set default values:
            this.Width = 0.0;
        }

        public string Color { get; set; }

        public double Width { get; set; }

        public object ToJavaScriptObject()
        {
            var jsLine = Interop.ExecuteJavaScript("[]");

            if (!string.IsNullOrEmpty(this.Color))
            {
                Interop.ExecuteJavaScript("$0['color'] = $1;",
                    jsLine,
                    this.Color);
            }

            Interop.ExecuteJavaScript("$0['width'] = $1;",
                jsLine,
                this.Width);

            return jsLine;
        }
    }
}
