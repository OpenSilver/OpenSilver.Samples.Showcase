namespace OpenSilver.Samples.Showcase

open System.Windows

type Net_Framework() as this =
    inherit Net_FrameworkXaml()

    do
        this.InitializeComponent()

        this.ConsoleDemo.Visibility <- Visibility.Collapsed
        this.RESXDemo.Visibility <- Visibility.Collapsed
