namespace OpenSilver.Samples.Showcase

open System
open System.Globalization
open System.Windows
open System.Windows.Media
open CSHTML5.Native.Html.Controls
open OpenSilver

type HtmlColorPicker() as this =
    inherit HtmlPresenter()

    let mutable domElement: obj = null
    let mutable jsHexColor: string = ""

    let getHex (number: byte) = number.ToString("X2", CultureInfo.InvariantCulture)

    let getJsHexColor (color: Color) =
        $"#{getHex color.R}{getHex color.G}{getHex color.B}".ToLower()

    let onJsColorChanged (hex: string) =
        if jsHexColor <> hex && not (isNull hex) && hex.StartsWith("#") && hex.Length = 7 then
            jsHexColor <- hex
            let r = Convert.ToByte(hex.Substring(1, 2), 16)
            let g = Convert.ToByte(hex.Substring(3, 2), 16)
            let b = Convert.ToByte(hex.Substring(5, 2), 16)
            this.Color <- Color.FromRgb(r, g, b)

    let updateValue () =
        if isNull domElement then
            ()
        else
            jsHexColor <- getJsHexColor this.Color
            Interop.ExecuteJavaScriptVoidAsync("$0.firstChild.firstChild.value = $1", domElement, jsHexColor)

    do
        this.Html <- "<input type='color'>"
        this.Loaded.AddHandler(RoutedEventHandler(fun _ _ ->
            this.Loaded.RemoveHandler(null)
            domElement <- Interop.GetDiv(this)
            jsHexColor <- getJsHexColor this.Color
            Interop.ExecuteJavaScriptVoidAsync(
                """
                const picker = $0.firstChild.firstChild;
                picker.value = $1;
                picker.addEventListener('input', e => $2(e.target.value));
                """,
                domElement, jsHexColor, Action<string>(onJsColorChanged)
            )
        ))

    static let colorProperty =
        DependencyProperty.Register(
            "Color",
            typeof<Color>,
            typeof<HtmlColorPicker>,
            PropertyMetadata(Colors.Black, PropertyChangedCallback(fun d _ ->
                let picker = d :?> HtmlColorPicker
                picker.updateValue()
            ))
        )

    member this.Color
        with get() = this.GetValue(colorProperty) :?> Color
        and set(v: Color) = this.SetValue(colorProperty, v)

    member private this.updateValue() = updateValue()
