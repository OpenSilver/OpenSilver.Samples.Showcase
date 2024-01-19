namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
#endif

type ViewSourceButtonHelper() =
    static member val OnViewSourceRequested : (TabControl -> unit) option = None with get, set

    static member ViewSource(sourcePaths : ViewSourceButtonInfo list) =
        if List.length sourcePaths > 0 then
            let tabControl = new TabControl()

            for viewSourceButtonInfo in sourcePaths do
                let tabItem = new TabItem(Header = viewSourceButtonInfo.TabHeader)
                tabItem.Content <- new ControlToDisplayCodeHostedOnGitHub(FilePathOnGitHub = viewSourceButtonInfo.FilePathOnGitHub)

                tabControl.Items.Add(tabItem)

            (tabControl.Items.[0] :?> TabItem).IsSelected <- true

            match ViewSourceButtonHelper.OnViewSourceRequested with
            | Some(callback) -> callback(tabControl)
            | None -> () // Handle the case when no callback is set
