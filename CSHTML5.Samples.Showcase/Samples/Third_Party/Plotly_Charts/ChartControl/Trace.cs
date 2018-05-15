using CSHTML5;
using System.Collections.Generic;

namespace CSHTML5.Extensions.Plotly
{
    public class Trace
    {
        public Trace()
        {
            // Set default values:
            this.X = new List<object>();
            this.Y = new List<object>();
            this.Text = new List<string>();
            this.Mode = TraceMode.Lines;
            this.Hole = 0;
        }

        public string Name { get; set; }

        public List<object> X { get; set; }

        public List<object> Y { get; set; }

        public object Text { get; set; }

        public List<object> Values { get; set; }

        public List<string> Labels { get; set; }

        public TraceType Type { get; set; }

        public TraceMode Mode { get; set; }

        public Marker Marker { get; set; }

        public Domain Domain { get; set; }

        public string HoverInfo { get; set; }

        public string TextInfo { get; set; }

        public double Hole { get; set; }

        public string TextPosition { get; set; }

        public object ToJavaScriptObject()
        {
            var jsX = Interop.ExecuteJavaScript("[]");
            foreach (var xPoint in this.X)
            {
                Interop.ExecuteJavaScript("$0.push($1)", jsX, xPoint);
            }

            var jsY = Interop.ExecuteJavaScript("[]");
            foreach (var yPoint in this.Y)
            {
                Interop.ExecuteJavaScript("$0.push($1)", jsY, yPoint);
            }

            var jsText = Interop.ExecuteJavaScript("[]");
            if (this.Text is List<string>)
            {
                foreach (var text in (List<string>)this.Text)
                {
                    Interop.ExecuteJavaScript("$0.push($1)", jsText, text);
                }
            }
            else if (this.Text is string && !string.IsNullOrEmpty((string)this.Text))
            {
                Interop.ExecuteJavaScript("$0 = $1;",
                    jsText,
                    (string)this.Text);
            }

            var jsTrace = Interop.ExecuteJavaScript(@"new Object()");
            Interop.ExecuteJavaScript(@"
                      $0['x'] = $1;
                      $0['y'] = $2;
                      $0['type'] = $3;
                      $0['mode'] = $4;
                      if ($5 != '') {
                        $0['name'] = $5;
                      }
                      if ($6.length == 1) {
                        $0['text'] = $6[0];
                      } else if ($6.length > 1) {
                        $0['text'] = $6;
                      }
                ",
                 jsTrace,
                 jsX,
                 jsY,
                 this.Type.ToString().ToLower(),
                 this.Mode.ToJavaScriptString(),
                 this.Name ?? "",
                 jsText);

            if (this.Marker != null)
            {
                Interop.ExecuteJavaScript("$0['marker'] = $1;",
                     jsTrace,
                     this.Marker.ToJavaScriptObject());
            }

            if (this.Values != null)
            {
                var jsValues = Interop.ExecuteJavaScript("[]");

                foreach (object Val in this.Values)
                {
                    Interop.ExecuteJavaScript("$0.push($1);", jsValues, Val);
                }

                Interop.ExecuteJavaScript("$0['values'] = $1;",
                    jsTrace,
                    jsValues);
            }

            if (this.Labels != null)
            {
                var jsLabels = Interop.ExecuteJavaScript("[]");

                foreach (string Lbl in this.Labels)
                {
                    Interop.ExecuteJavaScript("$0.push($1);",
                        jsLabels,
                        Lbl);
                }

                Interop.ExecuteJavaScript("$0['labels'] = $1;",
                    jsTrace,
                    jsLabels);
            }

            if (this.Domain != null)
            {
                Interop.ExecuteJavaScript("$0['domain'] = $1;",
                    jsTrace,
                    this.Domain.ToJavaScriptObject());
            }

            if (!string.IsNullOrEmpty(this.HoverInfo))
            {
                Interop.ExecuteJavaScript("$0['hoverinfo'] = $1;",
                    jsTrace,
                    this.HoverInfo);
            }

            if (!string.IsNullOrEmpty(this.TextInfo))
            {
                Interop.ExecuteJavaScript("$0['textinfo'] = $1;",
                    jsTrace,
                    this.TextInfo);
            }

            Interop.ExecuteJavaScript("$0['hole'] = $1;",
                jsTrace,
                this.Hole);

            if (!string.IsNullOrEmpty(this.TextPosition))
            {
                Interop.ExecuteJavaScript("$0['textposition'] = $1;",
                    jsTrace,
                    this.TextPosition);
            }

            return jsTrace;
        }
    }
}
