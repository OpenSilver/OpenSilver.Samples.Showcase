namespace OpenSilver.Extensions.Plotly
{
    public class Font
    {
        public Font()
        {
            // Set default values:
            this.Size = 14;
        }

        public string Family { get; set; }

        public int Size { get; set; }

        public string Color { get; set; }

        public object ToJavaScriptObject()
        {
            var jsFont = Interop.ExecuteJavaScript("[]");

            if (!string.IsNullOrEmpty(this.Family))
            {
                Interop.ExecuteJavaScript("$0['family'] = $1;",
                    jsFont,
                    this.Family);
            }

            Interop.ExecuteJavaScript("$0['size'] = $1;",
                jsFont,
                InteropHelper.Unbox(this.Size));

            if (!string.IsNullOrEmpty(this.Color))
            {
                Interop.ExecuteJavaScript("$0['color'] = $1;",
                    jsFont,
                    this.Color);
            }

            return jsFont;
        }
    }
}
