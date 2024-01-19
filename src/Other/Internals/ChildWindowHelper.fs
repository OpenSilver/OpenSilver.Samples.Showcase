namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Linq
open System.Text
open System.Threading.Tasks
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
#endif

type ChildWindowHelper() =
    static member ShowChildWindow (content: UIElement) =
        let childWindow = new ChildWindow()
        childWindow.Content <- content
        childWindow.Style <- App.Current.Resources["ViewMoreChildWindow_Style"] :?> Style
        childWindow.Show()

