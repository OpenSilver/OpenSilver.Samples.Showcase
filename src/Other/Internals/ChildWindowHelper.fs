namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls

type ChildWindowHelper() =
    static member ShowChildWindow (content: UIElement) =
        let childWindow = new ChildWindow()
        childWindow.Content <- content
        childWindow.Style <- App.Current.Resources["ViewMoreChildWindow_Style"] :?> Style
        childWindow.Show()

