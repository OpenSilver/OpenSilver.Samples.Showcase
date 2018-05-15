using CSHTML5;
using System;

namespace CSHTML5.Extensions.Plotly
{
    public class Axis
    {
        public Axis()
        {
            // Set default values:
            this.Tickangle = 0;
            this.ZeroLine = true;
            this.GrigWidth = 1;
        }

        public string Title { get; set; }

        public int Tickangle { get; set; }

        public bool ZeroLine { get; set; }

        public int GrigWidth { get; set; }

        public Font TickFont { get; set; }

        public Font TitleFont { get; set; }

        public object ToJavascriptObject()
        {
            var jsAxis = Interop.ExecuteJavaScript("[]");

            if (this.Title != null)
            {
                Interop.ExecuteJavaScript("$0['title'] = $1;",
                    jsAxis,
                    this.Title);
            }

            Interop.ExecuteJavaScript("$0['tickangle'] = $1;",
                jsAxis,
                this.Tickangle);

            Interop.ExecuteJavaScript("$0['zeroline'] = $1;",
                jsAxis,
                this.ZeroLine);

            Interop.ExecuteJavaScript("$0['grigwidth'] = $1;",
                jsAxis,
                this.GrigWidth);

            if (this.TickFont != null)
            {
                Interop.ExecuteJavaScript("$0['tickfont'] = $1;",
                    jsAxis,
                    this.TickFont.ToJavaScriptObject());
            }

            if (this.TitleFont != null)
            {
                Interop.ExecuteJavaScript("$0['titlefont'] = $1;",
                    jsAxis,
                    this.TitleFont.ToJavaScriptObject());
            }

            return jsAxis;
        }
    }
}
