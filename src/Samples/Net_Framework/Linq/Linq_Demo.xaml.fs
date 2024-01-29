namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls

type Linq_Demo() as this =
    inherit Linq_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.ButtonToDemonstrateLinq_Click(sender : obj, e : RoutedEventArgs) =
        let planets = Planet.GetListOfPlanets()

        let result = 
            [ for p in planets do
                if p.Radius > 7000 then
                    yield p.Name 
            ] |> List.sort

        MessageBox.Show(sprintf "List of planets that have a radius greater than 7000km sorted alphabetically: %s" (String.Join(", ", result))) |> ignore
