namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("graphics", "shadow", "effect", "blur", "UI")>]
type DropShadowEffect_Demo() as this =
    inherit DropShadowEffect_DemoXaml()
    
    do
        this.InitializeComponent()

        let binding = System.Windows.Data.Binding("Color", ElementName = nameof(this.RectangleShadow), Mode = System.Windows.Data.BindingMode.TwoWay)

        this.ColorPicker.SetBinding(HtmlColorPicker.ColorProperty, binding) |> ignore
