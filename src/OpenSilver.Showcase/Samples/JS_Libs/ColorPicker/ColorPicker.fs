namespace OpenSilver.Showcase

open System
open System.Globalization
open System.Threading.Tasks
open System.Windows
open System.Windows.Media
open OpenSilver
open CSHTML5.Native.Html.Controls

type ColorPicker() =
    inherit HtmlPresenter()

    let mutable domElement: obj = null

    static let colorProperty =
        DependencyProperty.Register(
            "Color",
            typeof<Color>,
            typeof<ColorPicker>,
            PropertyMetadata(Colors.Black)
        )

    member this.Color
        with get() = this.GetValue(colorProperty) :?> Color
        and set(value: Color) = this.SetValue(colorProperty, value)

    //let onColorChangedInAlwan (r: byte) (g: byte) (b: byte) (a: float32) =
    //    let aByte = byte (Math.Round(float a * float byte.MaxValue))
    //    this.Color <- Color.FromArgb(aByte, r, g, b)

    //let getHex (n: byte) = n.ToString("X2", CultureInfo.InvariantCulture)

    //let jsHexColor () =
    //    let c = this.Color
    //    sprintf "'#%s%s%s%s'" (getHex c.R) (getHex c.G) (getHex c.B) (getHex c.A)

    //let loadJsLibrary () =
    //    task {
    //        if not isJsLibLoaded then
    //            do! Interop.LoadCssFile("https://cdn.jsdelivr.net/npm/alwan/dist/css/alwan.min.css") |> ignore
    //            do! Interop.LoadJavaScriptFile("https://cdn.jsdelivr.net/npm/alwan/dist/js/alwan.min.js") |> ignore
    //            isJsLibLoaded <- true
    //    }

    //let onLoaded (_: obj) (_: RoutedEventArgs) =
    //    task {
    //        this.Loaded.RemoveHandler(RoutedEventHandler(onLoaded))

    //        do! loadJsLibrary ()
    //        domElement <- Interop.GetDiv(this)

    //        this.Html <- "<div></div>"

    //        do!
    //            Interop.ExecuteJavaScriptVoidAsync(
    //                $"""
    //                const alwan = new Alwan($0.firstChild.firstChild, {{
    //                    theme: 'dark',
    //                    toggle: false,
    //                    popover: false,
    //                    color: {jsHexColor ()},
    //                    margin: 0,
    //                    format: 'rgb',
    //                    inputs: {{hex: true, rgb: true, hsl: true}},
    //                    singleInput: true,
    //                }});
    //                alwan.on('color', (ev) => {{
    //                    $1(ev.r, ev.g, ev.b, ev.a);
    //                }});
    //                alwan.on('change', (ev) => {{
    //                    $1(ev.r, ev.g, ev.b, ev.a);
    //                }});
    //                $0.alwan = alwan;
    //                """,
    //                domElement,
    //                Action<byte, byte, byte, float32>(onColorChangedInAlwan)
    //            )
    //        |> Async.AwaitTask
    //        |> Async.StartImmediate
    //    }

    //do
    //    this.Loaded.AddHandler(RoutedEventHandler(onLoaded))

    static member ColorProperty = colorProperty
