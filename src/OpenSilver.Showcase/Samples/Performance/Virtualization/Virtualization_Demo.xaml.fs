namespace OpenSilver.Showcase

open System
open System.Collections.Generic
open System.Windows.Media
open OpenSilver.Showcase.Search

[<SearchKeywords("performance", "virtualization", "lazy loading", "memory optimization", "UI")>]
type Virtualization_Demo() as this =
    inherit Virtualization_DemoXaml()

    do
        this.InitializeComponent()
        // this.DataContext <- this.colors
        this.DataContext <- Virtualization_Demo.GetColorList()

    // Define a static member for GetColorList
    static member GetColorList() =
        let colorList = new List<Tuple<string, string>>()

        // Get all predefined colors
        for property in typeof<Colors>.GetProperties() do
            if property.PropertyType = typeof<Color> then
                let color = property.GetValue(null, null) :?> Color
                let colorName = property.Name
                let colorCode = "#" + color.ToString().Substring(3) // Convert to hex format

                colorList.Add(Tuple.Create(colorName, colorCode))
        colorList

    // Define an instance member for colors
    //member val colors = Virtualization_Demo.GetColorList() with get

    
