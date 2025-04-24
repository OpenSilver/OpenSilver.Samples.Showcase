namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Media
open CSHTML5.Native.Html.Controls

type SvgImage() =
    inherit HtmlPresenter()

    // Properties (Auto-implemented)
    member val Source: string = null with get, set
    member val Content: string = null with get, set

    member val FillElementSelector: string = "path" with get, set
    member val ForceSetFill: bool = false with get, set
   // member val FillColor: Nullable<Color> = Nullable() with get, set

    member val StrokeElementSelector: string = "path" with get, set
    member val ForceSetStroke: bool = false with get, set
    member val StrokeColor: Nullable<Color> = Nullable() with get, set

    static member val FillColorProperty =
        DependencyProperty.Register("FillColor", typeof<Nullable<Color>>, typeof<SvgImage>,
            PropertyMetadata(null))

    // CLR wrapper for DependencyProperty
    member this.FillColor
        with get() = this.GetValue(SvgImage.FillColorProperty) :?> Nullable<Color>
        and set(value: Nullable<Color>) = this.SetValue(SvgImage.FillColorProperty, value)