namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type FullScreen_Demo() as this =
    inherit FullScreen_DemoXaml()

    do
        this.InitializeComponent()

    member private this.EnterFullScreen_Click(sender : obj, e : RoutedEventArgs) =
        Application.Current.Host.Content.IsFullScreen <- true

    member private this.ExitFullSCreen_Click(sender : obj, e : RoutedEventArgs) =
        Application.Current.Host.Content.IsFullScreen <- false
