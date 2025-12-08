using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CSHTML5.Native.Html.Controls;
using OpenSilver;

namespace OpenSilver.Showcase
{
    public class SvgImage : HtmlPresenter
    {
        #region Source
        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(string), typeof(SvgImage), new PropertyMetadata(null, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (SvgImage)d;
            var source = (string)e.NewValue;

            if (string.IsNullOrEmpty(source))
            {
                control.Content = "";
            }
            else
            {
                // todo: fix getting the resource
                // current workaround: every assembly that has svg resources must have it's own SvgImage control
                // possible solution:
                //   1. Wait until the control is loaded
                //   2. Get the path by INTERNAL_UriHelper.ConvertToHtml5Path(Source, this)
                //   3. Either fetch and assign the svg content in js, or parse the path find the corresponding assembly and get the stream of the source
                // another possible solution: add support for .svg extension to Application.GetResourceString() method

                var isAbsolutePath =
                    source.Contains(";component/") ||
                    source.StartsWith("http") ||
                    source.StartsWith("pack:/") ||
                    source.StartsWith("ms-appx:/");

                if (!isAbsolutePath)
                {
                    var assembly = control.GetType().Assembly;
                    source = $"/{assembly.GetName().Name};component/{source}";
                }

                var stream = Application.GetResourceStream(new Uri(source, UriKind.Relative)).Result?.Stream;
                if (stream is null)
                {
                    control.Content = $"<p style='color:red'>Icon '{source}' is not found</p>";
                }
                else
                {
                    using var reader = new StreamReader(stream);
                    control.Content = reader.ReadToEnd();
                }
            }
        }
        #endregion

        #region Content
        public string Content
        {
            get => (string)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register(nameof(Content), typeof(string), typeof(SvgImage),
                new PropertyMetadata(string.Empty)
                {
                    MethodToUpdateDom = static (d, newValue) =>
                    {
                        var control = (SvgImage)d;
                        control.Html = (string)newValue;
                        control.SetAutoSize();
                        control.UpdateFillColor();
                        control.UpdateStrokeColor();
                    },
                });
        #endregion

        #region FillColor
        public string FillElementSelector { get; set; } = "path";
        public bool ForceSetFill { get; set; } = false;

        public Color? FillColor
        {
            get => (Color?)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        public static readonly DependencyProperty FillColorProperty =
            DependencyProperty.Register(nameof(FillColor), typeof(Color?), typeof(SvgImage),
                new PropertyMetadata(null) { MethodToUpdateDom = (d, _) => ((SvgImage)d).UpdateFillColor() });

        private void UpdateFillColor()
        {
            UpdateColor(FillColor, FillElementSelector, "fill", ForceSetFill);
        }
        #endregion

        #region StrokeColor
        public string StrokeElementSelector { get; set; } = "path";
        public bool ForceSetStroke { get; set; } = false;

        public Color? StrokeColor
        {
            get => (Color?)GetValue(StrokeColorProperty);
            set => SetValue(StrokeColorProperty, value);
        }

        public static readonly DependencyProperty StrokeColorProperty =
            DependencyProperty.Register(nameof(StrokeColor), typeof(Color?), typeof(SvgImage),
                new PropertyMetadata(null) { MethodToUpdateDom = (d, _) => ((SvgImage)d).UpdateStrokeColor() });

        private void UpdateStrokeColor()
        {
            UpdateColor(StrokeColor, StrokeElementSelector, "stroke", ForceSetStroke);
        }
        #endregion

        private void UpdateColor(Color? color, string elementSelector, string attributeName, bool forceUpdate)
        {
            if (color == null)
                return;

            // converting C# ARGB to JS RGBA color
            var hexARGB = color.ToString(); // #AARRGGBB
            var jsColor = $"#{hexARGB.Substring(3, 6)}{hexARGB.Substring(1, 2)}";

            Interop.ExecuteJavaScriptAsync($@"
$0.firstChild.shadowRoot
  .querySelectorAll('{elementSelector}')
  .forEach(el => {{
    if ({forceUpdate.ToString().ToLower()} || (el.hasAttribute('{attributeName}') && el.getAttribute('{attributeName}') != 'none')) {{
      el.setAttribute('{attributeName}', '{jsColor}');
    }}
  }});
", Interop.GetDiv(this));
        }

        public SvgImage()
        {
            ScrollMode = System.Windows.Controls.ScrollMode.Disabled;

            // Setting shadow dom via reflection for back compatibility
            var shadowDom = GetType().GetProperty(nameof(UseShadowDom));
            shadowDom?.SetValue(this, true);
        }

        private void SetAutoSize()
        {
            Interop.ExecuteJavaScriptVoidAsync(@$"
var svg = $0.firstChild.shadowRoot.querySelector('svg');
if (svg) {{
  svg.setAttribute('width', '100%');
  svg.setAttribute('height', '100%');
}}
", Interop.GetDiv(this));
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            e.Handled = false;
        }
    }
}
