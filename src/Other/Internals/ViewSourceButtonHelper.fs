namespace OpenSilver.Samples.Showcase

open System.Windows.Controls
open System.Collections.Generic

type internal ViewSourceButtonHelper() =
    static member val OnViewSourceRequested : (TabControl -> unit) option = None with get, set

    static member viewSourceImpl(sourcePaths: ICollection<ViewSourceButtonInfo>) =
        if sourcePaths.Count = 0 then
            ()

        let tabControl = new TabControl()

        for viewSourceButtonInfo in sourcePaths do
            let tabItem = new TabItem(Header = viewSourceButtonInfo.GetHeader())
            tabItem.Content <- new ControlToDisplayCodeHostedOnGitHub(FilePathOnGitHub = viewSourceButtonInfo.GetAbsoluteUrl())

            tabControl.Items.Add(tabItem)

        (tabControl.Items.[0] :?> TabItem).IsSelected <- true

        match ViewSourceButtonHelper.OnViewSourceRequested with
        | Some(callback) -> callback(tabControl)
        | None -> () // Handle the case when no callback is set

    static member ViewSource(sourcePaths: List<ViewSourceButtonInfo>) = 
        ViewSourceButtonHelper.viewSourceImpl(sourcePaths)

    static member ViewSource(sourcePaths: ViewSourceButtonInfo[]) = 
        ViewSourceButtonHelper.viewSourceImpl(sourcePaths)
