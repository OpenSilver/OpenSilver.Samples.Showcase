namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type ListBox_Demo() as this =
    inherit ListBox_DemoXaml()
    
    do
        this.InitializeComponent()
        this.ListBox1.ItemsSource <- Planet.GetListOfPlanets()
