namespace OpenSilver.Samples.Showcase

open System.Threading.Tasks
open System.Windows

type UIElementHelpers() =
    static member WaitForLoadedAsync(element: FrameworkElement) : Task =
        if element.IsLoaded then
            Task.CompletedTask
        else
            let tcs = new TaskCompletionSource<obj>()
            let mutable handler : RoutedEventHandler = null
            
            let handler = RoutedEventHandler(fun s e ->
                element.Loaded.RemoveHandler(handler)
                tcs.SetResult(null)
            )
            
            element.Loaded.AddHandler(handler)
            tcs.Task
