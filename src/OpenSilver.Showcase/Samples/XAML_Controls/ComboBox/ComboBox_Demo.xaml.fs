namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search

[<SearchKeywords("input", "selection", "list", "dropdown", "choices")>]
type ComboBox_Demo() as this =
    inherit ComboBox_DemoXaml()
    
    do
        this.InitializeComponent()
        this.ComboBox1.ItemsSource <- Planet.GetListOfPlanets()

