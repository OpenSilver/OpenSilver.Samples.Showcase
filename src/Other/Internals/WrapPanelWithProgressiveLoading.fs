namespace OpenSilver.Samples.Showcase

open System.Windows.Controls

type WrapPanelWithProgressiveLoading() as this =
    inherit WrapPanel()

    do
        this.ProgressiveRenderingChunkSize <- 3
        