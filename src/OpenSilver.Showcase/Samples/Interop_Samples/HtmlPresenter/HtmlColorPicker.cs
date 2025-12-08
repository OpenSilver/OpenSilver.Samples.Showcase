using CSHTML5.Native.Html.Controls;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace OpenSilver.Showcase;

public class HtmlColorPicker : HtmlPresenter
{
    private object _domElement;

    #region Color
    private string _jsHexColor;

    public Color Color
    {
        get => (Color)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }

    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(nameof(Color), typeof(Color), typeof(HtmlColorPicker), new PropertyMetadata(Colors.Black, OnColorChanged));

    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var picker = (HtmlColorPicker)d;
        picker.UpdateValue();
    }
    #endregion

    public HtmlColorPicker()
    {
        Html = "<input type='color'>";

        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        Loaded -= OnLoaded;

        _domElement = Interop.GetDiv(this);
        _jsHexColor = JsHexColor;

        Interop.ExecuteJavaScriptVoidAsync("""
            const picker = $0.firstChild.firstChild;
            picker.value = $1;
            picker.addEventListener('input', e => $2(e.target.value));
            """, _domElement, _jsHexColor, (Action<string>)OnJsColorChanged);
    }

    private void OnJsColorChanged(string jsHexColor)
    {
        if (_jsHexColor != jsHexColor && jsHexColor?.StartsWith("#") == true && jsHexColor.Length == 7)
        {
            _jsHexColor = jsHexColor;
            byte r = Convert.ToByte(jsHexColor.Substring(1, 2), 16);
            byte g = Convert.ToByte(jsHexColor.Substring(3, 2), 16);
            byte b = Convert.ToByte(jsHexColor.Substring(5, 2), 16);
            Color = Color.FromRgb(r, g, b);
        }
    }

    private void UpdateValue()
    {
        if (_domElement == null)
            return;

        _jsHexColor = JsHexColor;
        Interop.ExecuteJavaScriptVoidAsync("$0.firstChild.firstChild.value = $1", _domElement, _jsHexColor);
    }

    private string JsHexColor => $"#{GetHex(Color.R)}{GetHex(Color.G)}{GetHex(Color.B)}".ToLower();

    private string GetHex(byte number) => number.ToString("X2", CultureInfo.InvariantCulture);
}