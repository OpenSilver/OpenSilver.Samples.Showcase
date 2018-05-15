using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHTML5.Extensions.Plotly
{
    public class Legend
    {
        public Legend()
        {
            // Set default values:
            this.X = 0;
            this.Y = 0;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public string BgColor { get; set; }

        public string BorderColor { get; set; }

        public object ToJavaScriptObject()
        {
            var jsLegend = Interop.ExecuteJavaScript("[]");

            Interop.ExecuteJavaScript("$0['x'] = $1;",
                jsLegend,
                this.X);
            Interop.ExecuteJavaScript("$0['y'] = $1;",
                jsLegend,
                this.Y);

            if (!string.IsNullOrEmpty(this.BgColor))
            {
                Interop.ExecuteJavaScript("$0['bgcolor'] = $1;",
                    jsLegend,
                    this.BgColor);
            }

            if (!string.IsNullOrEmpty(this.BorderColor))
            {
                Interop.ExecuteJavaScript("$0['bordercolor'] = $1;",
                    jsLegend,
                    this.BorderColor);
            }

            return jsLegend;
        }
    }
}
