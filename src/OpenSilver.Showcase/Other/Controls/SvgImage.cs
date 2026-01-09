using CSHTML5.Internal;
using CSHTML5.Native.Html.Controls;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace OpenSilver.Showcase;

public class SvgImage : HtmlPresenter
{
    private static HttpClient _httpClient;

    #region Source
    public string Source
    {
        get => (string)GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(
            nameof(Source),
            typeof(string),
            typeof(SvgImage),
            new PropertyMetadata(null)
            {
                MethodToUpdateDom = async (d, newValue) => await ((SvgImage)d).RefreshSource(),
            });

    private async Task RefreshSource()
    {
        Content = string.IsNullOrEmpty(Source) ? "" : await GetContent(Source);
    }

    private async Task<string> GetContent(string path)
    {
        var uri = INTERNAL_UriHelper.ConvertToHtml5Path(path, this);
        if (Interop.IsRunningInTheSimulator)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "wwwroot", uri);
            return File.ReadAllText(filePath);
        }
        else
        {
            _httpClient ??= new HttpClient { BaseAddress = new Uri(Interop.ExecuteJavaScriptGetResult<string>("document.baseURI")) };
            return await _httpClient.GetStringAsync(uri);
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
