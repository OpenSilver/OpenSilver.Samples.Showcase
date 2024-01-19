namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Threading
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

type DispatcherTimer_Demo() as this =
    inherit DispatcherTimer_DemoXaml()

    let mutable _dispatcherTimer = DispatcherTimer(Interval = TimeSpan.FromMilliseconds(100.0))

    do
        this.InitializeComponent()

        // Initialize the DispatcherTimer
        _dispatcherTimer.Tick.Add(fun _ -> this.DispatcherTimer_Tick())

    member private this.ButtonToStartTimer_Click(sender : obj, e : RoutedEventArgs) =
        _dispatcherTimer.Start()

    member private this.ButtonToStopTimer_Click(sender : obj, e : RoutedEventArgs) =
        _dispatcherTimer.Stop()

    member private this.DispatcherTimer_Tick() =
        // Increment the counter by 1
        this.CounterTextBlock.Text <- 
            match this.CounterTextBlock.Text with
            | null | "" -> "0"
            | text -> (int text + 1).ToString()

    interface IDisposable with
        member this.Dispose() =
            _dispatcherTimer.Stop()
            ()

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "DispatcherTimer_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DispatcherTimer/DispatcherTimer_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "DispatcherTimer_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DispatcherTimer/DispatcherTimer_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "DispatcherTimer_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DispatcherTimer/DispatcherTimer_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "DispatcherTimer_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/DispatcherTimer/DispatcherTimer_Demo.xaml.fs")
        ])
