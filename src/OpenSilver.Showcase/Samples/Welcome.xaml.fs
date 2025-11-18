namespace OpenSilver.Showcase

open System.Windows.Controls

type Welcome() as this =
    inherit WelcomeXaml()

    do
        this.InitializeComponent()
