namespace OpenSilver.Samples.Showcase

open System
open System.IO
open System.Windows
open System.Windows.Input
open System.Windows.Media
open CSHTML5.Native.Html.Controls
open OpenSilver

type SvgImage() as this =
    inherit HtmlPresenter()

    // --------- Source Property ----------
    static let OnSourceChanged =
        new PropertyChangedCallback(fun d e ->
            let control = d :?> SvgImage
            let source = e.NewValue :?> string

            if String.IsNullOrEmpty(source) then
                control.Content <- ""
            else
                // todo: fix getting the resource (see C# comments)
                let isAbsolutePath =
                    source.Contains(";component/") ||
                    source.StartsWith("http") ||
                    source.StartsWith("pack:/") ||
                    source.StartsWith("ms-appx:/")
                let actualSource =
                    if isAbsolutePath then source
                    else
                        let asm = control.GetType().Assembly
                        sprintf "/%s;component/%s" (asm.GetName().Name) source

                let result =
                    Application.GetResourceStream(Uri(actualSource, UriKind.Relative)).Result

                let stream = if isNull result then null else result.Stream
                if isNull stream then
                    control.Content <- $"<p style='color:red'>Icon '{actualSource}' is not found</p>"
                else
                    use reader = new StreamReader(stream)
                    control.Content <- reader.ReadToEnd()
        )

    do
        this.ScrollMode <- System.Windows.Controls.ScrollMode.Disabled
        // Set UseShadowDom via reflection for compatibility
        let shadowDomProp = this.GetType().GetProperty("UseShadowDom")
        if not (isNull shadowDomProp) then
            shadowDomProp.SetValue(this, true)

    static member val SourceProperty =
        DependencyProperty.Register(
            "Source",
            typeof<string>,
            typeof<SvgImage>,
            PropertyMetadata(null, OnSourceChanged)
        )

    member this.Source
        with get() = this.GetValue(SvgImage.SourceProperty) :?> string
        and set (v: string) = this.SetValue(SvgImage.SourceProperty, v)

    // --------- Content Property ----------
    static member val ContentProperty =
        DependencyProperty.Register(
            "Content",
            typeof<string>,
            typeof<SvgImage>,
            PropertyMetadata(
                "",
                MethodToUpdateDom =
                    fun (d: DependencyObject) (newValue: obj) ->
                        let control = d :?> SvgImage
                        control.Html <- newValue :?> string
                        control.SetAutoSize()
                        control.UpdateFillColor()
                        control.UpdateStrokeColor()
            )
        )

    member this.Content
        with get() = this.GetValue(SvgImage.ContentProperty) :?> string
        and set v = this.SetValue(SvgImage.ContentProperty, v)

    // --------- FillColor Property ----------
    member val FillElementSelector = "path" with get, set
    member val ForceSetFill = false with get, set

    static member val FillColorProperty =
        DependencyProperty.Register(
            "FillColor",
            typeof<Nullable<Color>>,
            typeof<SvgImage>,
            PropertyMetadata(null, MethodToUpdateDom = (fun (d: DependencyObject) (newValue: obj) -> (d :?> SvgImage).UpdateFillColor()))
        )

    member this.FillColor
        with get() = this.GetValue(SvgImage.FillColorProperty) :?> Nullable<Color>
        and set (v: Nullable<Color>) = this.SetValue(SvgImage.FillColorProperty, v)

    member private this.UpdateFillColor() =
        this.UpdateColor(this.FillColor, this.FillElementSelector, "fill", this.ForceSetFill)

    // --------- StrokeColor Property ----------
    member val StrokeElementSelector = "path" with get, set
    member val ForceSetStroke = false with get, set

    static member val StrokeColorProperty =
        DependencyProperty.Register(
            "StrokeColor",
            typeof<Nullable<Color>>,
            typeof<SvgImage>,
            PropertyMetadata(null, MethodToUpdateDom = (fun (d: DependencyObject) (newValue: obj) -> (d :?> SvgImage).UpdateStrokeColor()))
        )

    member this.StrokeColor
        with get() = this.GetValue(SvgImage.StrokeColorProperty) :?> Nullable<Color>
        and set (v: Nullable<Color>) = this.SetValue(SvgImage.StrokeColorProperty, v)

    member private this.UpdateStrokeColor() =
        this.UpdateColor(this.StrokeColor, this.StrokeElementSelector, "stroke", this.ForceSetStroke)

    // --------- Color Helper ----------
    member private this.UpdateColor(color: Nullable<Color>, elementSelector: string, attributeName: string, forceUpdate: bool) =
        if color.HasValue then
            // Convert ARGB to #RRGGBBAA as expected by JS
            let col = color.Value
            let hexARGB = col.ToString() // "#AARRGGBB"
            let jsColor = $"#{hexARGB.Substring(3, 6)}{hexARGB.Substring(1, 2)}"
            Interop.ExecuteJavaScriptAsync(
                $"""{{
$0.firstChild.shadowRoot
  .querySelectorAll('{elementSelector}')
  .forEach(el => {{
    if ({forceUpdate.ToString().ToLower()} || (el.hasAttribute('{attributeName}') && el.getAttribute('{attributeName}') != 'none')) {{
      el.setAttribute('{attributeName}', '{jsColor}');
    }}
  }});
}}""",
                Interop.GetDiv(this)
            )
            |> ignore


    // --------- SetAutoSize Helper ----------
    member private this.SetAutoSize() =
        Interop.ExecuteJavaScriptVoidAsync(
            $"""var svg = $0.firstChild.shadowRoot.querySelector('svg');
if (svg) {{
  svg.setAttribute('width', '100%%');
  svg.setAttribute('height', '100%%');
}}""",
            Interop.GetDiv(this)
        )
        |> ignore

    // --------- Mouse Wheel override ----------
    override this.OnMouseWheel(e: MouseWheelEventArgs) =
        base.OnMouseWheel(e)
        e.Handled <- false
