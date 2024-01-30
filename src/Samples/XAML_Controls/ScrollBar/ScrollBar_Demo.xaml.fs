namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open System.Windows.Controls.Primitives


type ScrollBar_Demo() as this =
    inherit ScrollBar_DemoXaml()

    do
        this.InitializeComponent()
        this.TextDisplay.Text <- this.Scrollbar.Value.ToString("0.000")

    member private this.ScrollBar_Scroll(sender : obj, e : ScrollEventArgs) =
        this.TextDisplay.Text <- e.NewValue.ToString("0.000")
