namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Markup

[<ContentProperty("PopupContent")>]
type TogglePopupControl() as this =
    inherit TogglePopupControlXaml()

    do
        this.InitializeComponent()

    static member val IsPopupOpenProperty =
        DependencyProperty.Register("IsPopupOpen", typeof<bool>, typeof<TogglePopupControl>, PropertyMetadata(false))

    static member val PopupBorderStyleProperty =
        DependencyProperty.Register("PopupBorderStyle", typeof<Style>, typeof<TogglePopupControl>)

    static member val PopupContentProperty =
        DependencyProperty.Register("PopupContent", typeof<obj>, typeof<TogglePopupControl>)

    static member val ToggleButtonContentProperty =
        DependencyProperty.Register("ToggleButtonContent", typeof<obj>, typeof<TogglePopupControl>)

    static member val ToggleButtonStyleProperty =
        DependencyProperty.Register("ToggleButtonStyle", typeof<Style>, typeof<TogglePopupControl>)

    member this.IsPopupOpen
        with get () = this.GetValue(TogglePopupControl.IsPopupOpenProperty) :?> bool
        and  set (value: bool) = this.SetValue(TogglePopupControl.IsPopupOpenProperty, value)

    member this.PopupBorderStyle
        with get () = this.GetValue(TogglePopupControl.PopupBorderStyleProperty) :?> Style
        and  set (value: Style) = this.SetValue(TogglePopupControl.PopupBorderStyleProperty, value)

    member this.PopupContent
        with get () = this.GetValue(TogglePopupControl.PopupContentProperty)
        and  set (value: obj) = this.SetValue(TogglePopupControl.PopupContentProperty, value)

    member this.ToggleButtonContent
        with get () = this.GetValue(TogglePopupControl.ToggleButtonContentProperty)
        and  set (value: obj) = this.SetValue(TogglePopupControl.ToggleButtonContentProperty, value)

    member this.ToggleButtonStyle
        with get () = this.GetValue(TogglePopupControl.ToggleButtonStyleProperty) :?> Style
        and  set (value: Style) = this.SetValue(TogglePopupControl.ToggleButtonStyleProperty, value)
