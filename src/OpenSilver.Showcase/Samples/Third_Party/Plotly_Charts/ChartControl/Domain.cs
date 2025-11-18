using System.Collections.Generic;

namespace OpenSilver.Extensions.Plotly
{
    public class Domain
    {
        public List<double> X { get; set; }

        public List<double> Y { get; set; }

        public object ToJavaScriptObject()
        {
            var jsDomain = Interop.ExecuteJavaScript("[]");

            if (this.X != null)
            {
                var jsX = Interop.ExecuteJavaScript("[]");

                foreach (double d in this.X)
                {
                    Interop.ExecuteJavaScript("$0.push($1);",
                        jsX,
                        InteropHelper.Unbox(d));
                }
                Interop.ExecuteJavaScript("$0['x'] = $1;",
                    jsDomain,
                    jsX);
            }

            if (this.Y != null)
            {
                var jsY = Interop.ExecuteJavaScript("[]");

                foreach (double d in this.Y)
                {
                    Interop.ExecuteJavaScript("$0.push($1);",
                        jsY,
                        InteropHelper.Unbox(d));
                }
                Interop.ExecuteJavaScript("$0['y'] = $1;",
                    jsDomain,
                    jsY);
            }

            return jsDomain;
        }
    }
}
