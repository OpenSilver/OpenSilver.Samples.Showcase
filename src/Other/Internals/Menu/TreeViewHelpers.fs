namespace OpenSilver.Samples.Showcase

open System
open System.Threading.Tasks
open System.Windows.Controls
open System.Windows.Controls.Primitives

type TreeViewHelpers() =
    static member SelectItemInTreeViewAsync(treeView: TreeView, itemToSelect: obj, ?searchInCollapsedNodesToo: bool) : Task<bool> =
        let searchInCollapsedNodesToo = defaultArg searchInCollapsedNodesToo false
        async {
            do! UIElementHelpers.WaitForLoadedAsync(treeView) |> Async.AwaitTask
            do! TreeViewHelpers.WaitForContainerGenerationAsync(treeView.ItemContainerGenerator) |> Async.AwaitTask

            let mutable found = false
            let enumerator = treeView.Items.GetEnumerator()
            while enumerator.MoveNext() && not found do
                let item = enumerator.Current
                match treeView.ItemContainerGenerator.ContainerFromItem(item) with
                | :? TreeViewItem as treeViewItem ->
                    let! result = TreeViewHelpers.SelectInTreeViewItemAsync(treeViewItem, itemToSelect, searchInCollapsedNodesToo) |> Async.AwaitTask
                    if result then
                        found <- true
                | _ -> ()
            
            return false
        } |> Async.StartAsTask

    static member private SelectInTreeViewItemAsync(treeViewItem: TreeViewItem, itemToSelect: obj, searchInCollapsedNodesToo: bool) : Task<bool> =
        async {
            if treeViewItem.DataContext = itemToSelect then
                treeViewItem.IsSelected <- true
                return true
            else
                if searchInCollapsedNodesToo then
                    treeViewItem.IsExpanded <- true
                    treeViewItem.UpdateLayout() // Make sure the child items are created

                if treeViewItem.Items.Count > 0 then
                    do! TreeViewHelpers.WaitForContainerGenerationAsync(treeViewItem.ItemContainerGenerator) |> Async.AwaitTask

                let mutable found = false
                let enumerator = treeViewItem.Items.GetEnumerator()
                while enumerator.MoveNext() && not found do
                    let item = enumerator.Current
                    match treeViewItem.ItemContainerGenerator.ContainerFromItem(item) with
                    | :? TreeViewItem as childItem ->
                        let! result = TreeViewHelpers.SelectInTreeViewItemAsync(childItem, itemToSelect, searchInCollapsedNodesToo) |> Async.AwaitTask
                        if result then
                            found <- true
                    | _ -> ()
                
                return false
        } |> Async.StartAsTask

    static member WaitForContainerGenerationAsync(generator: ItemContainerGenerator) : Task =
        if generator.Status = GeneratorStatus.ContainersGenerated then
            Task.CompletedTask
        else
            let tcs = new TaskCompletionSource<obj>()
            let mutable handler : EventHandler = null
            
            let handler = EventHandler(fun s e ->
                if generator.Status = GeneratorStatus.ContainersGenerated then
                    generator.StatusChanged.RemoveHandler(handler)
                    tcs.SetResult(null)
            )
            
            generator.StatusChanged.AddHandler(handler)
            tcs.Task
