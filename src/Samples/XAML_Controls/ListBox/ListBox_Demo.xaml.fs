namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("input", "selection", "list", "items", "choices")>]
type ListBox_Demo() as this =
    inherit ListBox_DemoXaml()
    
    do
        this.InitializeComponent()
        this.ListBox1.ItemsSource <- Planet.GetListOfPlanets()
