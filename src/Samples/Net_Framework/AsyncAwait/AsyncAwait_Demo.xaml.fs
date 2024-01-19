namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading.Tasks
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
#else
open Windows.Foundation
open Windows.UI.Xaml
open Windows.UI.Xaml.Controls
open Windows.UI.Xaml.Controls.Primitives
open Windows.UI.Xaml.Data
open Windows.UI.Xaml.Input
open Windows.UI.Xaml.Media
open Windows.UI.Xaml.Navigation
#endif

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

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "AsyncAwait_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/AsyncAwait/AsyncAwait_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "AsyncAwait_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/AsyncAwait/AsyncAwait_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "AsyncAwait_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/AsyncAwait/AsyncAwait_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "AsyncAwait_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/AsyncAwait/AsyncAwait_Demo.xaml.fs");
        ])
