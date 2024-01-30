namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type Binding2_Demo() as this =
    inherit Binding2_DemoXaml()

    do
        this.InitializeComponent()
        this.Title.Content <- "Binding (2 of 3)"
