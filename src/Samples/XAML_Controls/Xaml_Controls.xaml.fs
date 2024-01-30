namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type Xaml_Controls() as this =
    inherit Xaml_ControlsXaml()

    do
        this.InitializeComponent()

#if OPENSILVER
        this.NonModalChildWindow.Visibility <- Visibility.Collapsed
#endif
        this.ScrollBarDemo.Visibility <- Visibility.Collapsed
        this.ThumbDemo.Visibility <- Visibility.Collapsed
        this.FrameDemo.Visibility <- Visibility.Collapsed; // The Showcase already uses a Frame to change pages anyway
