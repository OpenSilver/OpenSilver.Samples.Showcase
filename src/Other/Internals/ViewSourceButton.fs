namespace OpenSilver.Samples.Showcase

open System.Collections.Generic
open System.Windows
open System.Windows.Controls

type ViewSourceButton() as this =
    inherit Button()

    do
        this.Style <- Application.Current.Resources["ButtonViewSource_Style"] :?> Style

    member val Sources = new List<ViewSourceButtonInfo>() with get

    override this.OnClick() =
        base.OnClick()
        ViewSourceButtonHelper.ViewSource(this.Sources)
