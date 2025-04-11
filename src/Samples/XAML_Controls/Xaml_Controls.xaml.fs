namespace OpenSilver.Samples.Showcase

open System.Windows

type Xaml_Controls() as this =
    inherit Xaml_ControlsXaml()

    do
        this.InitializeComponent()

        this.ThumbDemo.Visibility <- Visibility.Collapsed
