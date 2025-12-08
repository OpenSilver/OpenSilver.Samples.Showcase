namespace OpenSilver.Samples.Showcase

open OpenSilver.Samples.Showcase.Search

[<SearchKeywords("form", "input", "editor", "data", "binding")>]
type DataForm_Demo() as this =
    inherit DataForm_DemoXaml()

    do
        this.InitializeComponent()
        
        // Populate the data form with the list of planets:
        this.DataForm1.ItemsSource <- Planet.GetListOfPlanets()
