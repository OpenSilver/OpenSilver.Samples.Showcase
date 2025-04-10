namespace OpenSilver.Samples.Showcase

open System.Windows

type Net_Framework() as this =
    inherit Net_FrameworkXaml()

    do
        this.InitializeComponent()

        this.JSON_SerializerDemo.Visibility <- Visibility.Collapsed
        this.GetRessourceStreamDemo.Visibility <- Visibility.Collapsed
        this.ConsoleDemo.Visibility <- Visibility.Collapsed
        this.RESXDemo.Visibility <- Visibility.Collapsed
