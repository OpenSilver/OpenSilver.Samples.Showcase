using CSHTML5;
using System;

namespace CSHTML5.Extensions.Plotly
{
    public class Annotation
    {
        public Annotation()
        {
            // Set default values:
            this.ShowArrow = true;
        }

        public object X { get; set; }

        public object Y { get; set; }

        public string Text { get; set; }

        public string Xanchor { get; set; }

        public string Yanchor { get; set; }

        public bool ShowArrow { get; set; }

        public Font Font { get; set; }

        public object ToJavaScriptObject()
        {
            var jsAnnotation = Interop.ExecuteJavaScript("[]");

            if (this.X != null)
            {
                Interop.ExecuteJavaScript("$0['x'] = $1;",
                    jsAnnotation,
                    this.X);
            }

            if (this.Y != null)
            {
                Interop.ExecuteJavaScript("$0['y'] = $1;",
                    jsAnnotation,
                    this.Y);
            }

            if (!string.IsNullOrEmpty(this.Text))
            {
                Interop.ExecuteJavaScript("$0['text'] = $1;",
                    jsAnnotation,
                    this.Text);
            }

            if (!string.IsNullOrEmpty(this.Xanchor))
            {
                Interop.ExecuteJavaScript("$0['xanchor'] = $1;",
                    jsAnnotation,
                    this.Xanchor);
            }

            if (!string.IsNullOrEmpty(this.Yanchor))
            {
                Interop.ExecuteJavaScript("$0['yanchor'] = $1;",
                    jsAnnotation,
                    this.Yanchor);
            }

            Interop.ExecuteJavaScript("$0['showarrow'] = $1;",
                jsAnnotation,
                this.ShowArrow);

            if (this.Font != null)
            {
                Interop.ExecuteJavaScript("$0['font'] = $1;",
                    jsAnnotation,
                    this.Font.ToJavaScriptObject());
            }

            return jsAnnotation;
        }
    }
}
