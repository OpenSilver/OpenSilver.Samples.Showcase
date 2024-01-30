namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type Third_Party() as this =
    inherit Third_PartyXaml()
    
    do
        this.InitializeComponent()
