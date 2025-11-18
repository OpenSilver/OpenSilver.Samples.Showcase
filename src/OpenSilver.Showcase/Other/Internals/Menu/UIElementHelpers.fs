namespace OpenSilver.Showcase

open System.Threading.Tasks
open System.Windows

type UIElementHelpers() =
    static member WaitForLoadedAsync(element: FrameworkElement) : Task =
        if element.IsLoaded then
            Task.CompletedTask
        else
            let tcs = TaskCompletionSource<unit>()
            let mutable handler : RoutedEventHandler = Unchecked.defaultof<_>
            handler <- RoutedEventHandler(fun _ _ ->
                element.Loaded.RemoveHandler(handler)
                tcs.SetResult(())
            )
            element.Loaded.AddHandler(handler)
            tcs.Task :> Task
