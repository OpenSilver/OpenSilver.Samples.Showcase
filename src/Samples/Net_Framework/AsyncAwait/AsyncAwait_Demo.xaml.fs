namespace OpenSilver.Samples.Showcase

open System.Windows
open System.Windows.Controls
open System.Threading.Tasks

type AsyncAwait_Demo() as this =
    inherit AsyncAwait_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.ButtonToDemonstrateAsyncAwait_Click (sender: obj, e: RoutedEventArgs) =
        async {
            let button = sender :?> Button
            button.Visibility <- Visibility.Collapsed
            this.TaskBasedCounterTextBlock.Visibility <- Visibility.Visible
            this.TaskBasedCounterTextBlock.Text <- "5 seconds left"
            do! Task.Delay(1000) |> Async.AwaitTask
            this.TaskBasedCounterTextBlock.Text <- "4 seconds left"
            do! Task.Delay(1000) |> Async.AwaitTask
            this.TaskBasedCounterTextBlock.Text <- "3 seconds left"
            do! Task.Delay(1000) |> Async.AwaitTask
            this.TaskBasedCounterTextBlock.Text <- "2 seconds left"
            do! Task.Delay(1000) |> Async.AwaitTask
            this.TaskBasedCounterTextBlock.Text <- "1 second left"
            do! Task.Delay(1000) |> Async.AwaitTask
            this.TaskBasedCounterTextBlock.Text <- "Done!"
            this.TaskBasedCounterTextBlock.Visibility <- Visibility.Collapsed
            button.Visibility <- Visibility.Visible
        } |> Async.Start
