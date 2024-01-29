namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type ComboBox_Demo() as this =
    inherit ComboBox_DemoXaml()
    
    do
        this.InitializeComponent()
        this.ComboBox1.ItemsSource <- Planet.GetListOfPlanets()

