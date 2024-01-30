namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type DataGrid_Demo() as this =
    inherit DataGrid_DemoXaml()

    do
        this.InitializeComponent()

        // Populate the data grid with the list of planets:
        this.DataGrid1.ItemsSource <- AtomicElements.Elements
