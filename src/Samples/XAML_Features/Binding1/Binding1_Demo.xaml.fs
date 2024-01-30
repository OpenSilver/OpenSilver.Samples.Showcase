namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type Binding1_Demo() as this =
    inherit Binding1_DemoXaml()

    do
        this.InitializeComponent()

        let listOfPlanets = Planet.GetListOfPlanets()
        this.ItemsControl1.ItemsSource <- listOfPlanets
        this.ContentControl1.Content <- listOfPlanets.[0]

    member private this.ButtonPlanet_Click(sender : obj, e : RoutedEventArgs) =
        let planet = (sender :?> Button).DataContext
        this.ContentControl1.Content <- planet
