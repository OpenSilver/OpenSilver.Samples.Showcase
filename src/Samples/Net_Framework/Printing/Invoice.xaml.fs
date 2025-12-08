namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type Invoice() as this =
    inherit InvoiceXaml()

    do
        this.InitializeComponent()
        