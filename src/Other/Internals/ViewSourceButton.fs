namespace OpenSilver.Samples.Showcase

open System.Collections.Generic
open System.Windows
open System.Windows.Controls

type ViewSourceButton() as this =
    inherit Button()

    static let mutable showSourceCode : (ViewSourcePanel -> unit) = fun _ -> ()

    do
        this.Style <- Application.Current.Resources["ButtonViewSource_Style"] :?> Style

    static member ShowSourceCode
        with get() = showSourceCode
        and set v = showSourceCode <- v

    member val Sources = new List<ViewSourceButtonInfo>() with get

    override this.OnClick() =
        base.OnClick()
        ViewSourceButton.ViewSource(this.Sources)

    static member private ViewSource(sourcePaths: ICollection<ViewSourceButtonInfo>) =
        if sourcePaths = null || sourcePaths.Count = 0 then
            ()
        else
            let panel = new ViewSourcePanel()
            panel.ViewSource(sourcePaths)
            ViewSourceButton.ShowSourceCode(panel)
