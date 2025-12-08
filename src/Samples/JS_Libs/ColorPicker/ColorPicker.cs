using CSHTML5.Native.Html.Controls;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase;

public class ColorPicker : HtmlPresenter
{
    private object _domElement;

    #region Color
    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Black));
    #endregion

    static ColorPicker()
    {
        AllowScrollOnTouchMoveProperty.OverrideMetadata(typeof(ColorPicker), new PropertyMetadata(false));
    }

    public ColorPicker()
    {
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        if (!await FileLoader.TryLoadCssFile("https://cdn.jsdelivr.net/npm/alwan/dist/css/alwan.min.css") ||
            !await FileLoader.TryLoadJavaScriptFile("https://cdn.jsdelivr.net/npm/alwan/dist/js/alwan.min.js"))
            return;

        _domElement = Interop.GetDiv(this);
        Html = "<div></div>";

        Interop.ExecuteJavaScriptVoidAsync($@"
          const alwan = new Alwan($0.firstChild.firstChild, {{
            theme: 'dark',
            toggle: false,
            popover: false,
            color: {JsHexColor},
            margin: 0,
            format: 'rgb',
            inputs: {{hex: true, rgb: true,  hsl: true}},
            singleInput: true,
          }});
          alwan.on('color', (ev) => {{
            $1(ev.r, ev.g, ev.b, ev.a);
          }});
          alwan.on('change', (ev) => {{
            $1(ev.r, ev.g, ev.b, ev.a);
          }});
          $0.alwan = alwan;
        ", _domElement, (Action<byte, byte, byte, float>)OnColorChangedInAlwan);
    }

    private void OnColorChangedInAlwan(byte r, byte g, byte b, float a)
    {
        Color = Color.FromArgb((byte)Math.Round(a * byte.MaxValue), r, g, b);
    }

    private string JsHexColor => $"'#{GetHex(Color.R)}{GetHex(Color.G)}{GetHex(Color.B)}{GetHex(Color.A)}'";

    private string GetHex(byte number) => number.ToString("X2", CultureInfo.InvariantCulture);
}
