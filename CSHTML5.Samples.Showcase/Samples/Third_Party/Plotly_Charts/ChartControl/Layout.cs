using CSHTML5;
using System.Collections.Generic;

namespace CSHTML5.Extensions.Plotly
{
    public class Layout
    {
        public Layout()
        {
            // Set default values:
            this.BarMode = BarMode.Group;
            this.ShowLegend = true;
            this.BarGap = 0.0;
            this.Width = 600;
            this.Height = 600;
        }

        public string Title { get; set; }

        public BarMode BarMode { get; set; }

        public Axis Xaxis { get; set; }

        public Axis Yaxis { get; set; }

        public List<Annotation> Annotations { get; set; }

        public bool ShowLegend { get; set; }

        public Font Font { get; set; }

        public double BarGap { get; set; }

        public Legend Legend { get; set; }

        public double BarGroupGap { get; set; }

        public string PaperBgColor { get; set; }

        public string PlotBgColor { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public object ToJavaScriptObject()
        {
            var jsLayout = Interop.ExecuteJavaScript(@"new Object()");

            Interop.ExecuteJavaScript(@"
                    $0['barmode'] = $1;
                    ",
                     jsLayout,
                     this.BarMode.ToString().ToLower());

            if (!string.IsNullOrEmpty(this.Title))
            {
                Interop.ExecuteJavaScript("$0['title'] = $1;",
                     jsLayout,
                     this.Title);
            }

            if (this.Xaxis != null)
            {
                Interop.ExecuteJavaScript("$0['xaxis'] = $1;",
                    jsLayout, 
                    this.Xaxis.ToJavascriptObject());
            }

            if (this.Yaxis != null)
            {
                Interop.ExecuteJavaScript("$0['yaxis'] = $1;", 
                    jsLayout, 
                    this.Yaxis.ToJavascriptObject());
            }

            if (this.Annotations != null)
            {
                Interop.ExecuteJavaScript("$0['annotations'] = [];", jsLayout);

                foreach (Annotation Annotation in Annotations)
                {
                    Interop.ExecuteJavaScript("$0['annotations'].push($1);",
                        jsLayout,
                        Annotation.ToJavaScriptObject());
                }
            }

            if (this.Font != null)
            {
                Interop.ExecuteJavaScript("$0['font'] = $1;",
                    jsLayout,
                    this.Font.ToJavaScriptObject());
            }

            Interop.ExecuteJavaScript("$0['showlegend'] = $1;",
                jsLayout,
                this.ShowLegend);

            if (this.Legend != null)
            {
                Interop.ExecuteJavaScript("$0['legend'] = $1;",
                    jsLayout,
                    this.Legend.ToJavaScriptObject());
            }

            Interop.ExecuteJavaScript("$0['bargroupgap'] = $1;",
                jsLayout,
                this.BarGroupGap);

            if (!string.IsNullOrEmpty(this.PaperBgColor))
            {
                Interop.ExecuteJavaScript("$0['paper_bgcolor'] = $1;",
                    jsLayout,
                    this.PaperBgColor);
            }

            if (!string.IsNullOrEmpty(this.PlotBgColor))
            {
                Interop.ExecuteJavaScript("$0['plot_bgcolor'] = $1;",
                    jsLayout,
                    this.PlotBgColor);
            }

            Interop.ExecuteJavaScript("$0['width'] = $1",
                jsLayout,
                this.Width);
            Interop.ExecuteJavaScript("$0['height'] = $1",
                jsLayout,
                this.Height);

            return jsLayout;
        }
    }
}
