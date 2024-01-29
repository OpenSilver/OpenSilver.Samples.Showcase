namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type Performance() as this =
    inherit PerformanceXaml()
    
    do
        this.InitializeComponent()
