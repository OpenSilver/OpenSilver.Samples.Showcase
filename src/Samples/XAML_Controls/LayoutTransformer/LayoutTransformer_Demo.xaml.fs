namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls
open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("transformation", "scale", "rotate", "skew")>]
type public LayoutTransformer_Demo() as this =
    inherit LayoutTransformer_DemoXaml()

    do
        this.InitializeComponent()

    member private this.OnSliderValueChanged(_sender: obj, _e: RoutedPropertyChangedEventArgs<double>) =
        this.layoutTransformer.ApplyLayoutTransform()
