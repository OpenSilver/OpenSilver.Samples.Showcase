namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Text

#if SLMIGRATION
open System.Windows.Controls
#else
open Windows.UI.Xaml.Controls
#endif

type WrapPanelWithProgressiveLoading() as this =
    inherit WrapPanel()

    do
        this.ProgressiveRenderingChunkSize <- 3
        