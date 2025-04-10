namespace OpenSilver.Samples.Showcase

open System.Windows

type Xaml_Controls() as this =
    inherit Xaml_ControlsXaml()

    do
        this.InitializeComponent()

        this.NonModalChildWindow.Visibility <- Visibility.Collapsed
        this.ScrollBarDemo.Visibility <- Visibility.Collapsed
        this.ThumbDemo.Visibility <- Visibility.Collapsed
