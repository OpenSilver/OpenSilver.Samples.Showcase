namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type TabControl_Demo() as this =
    inherit TabControl_DemoXaml()
    
    do
        this.InitializeComponent()
