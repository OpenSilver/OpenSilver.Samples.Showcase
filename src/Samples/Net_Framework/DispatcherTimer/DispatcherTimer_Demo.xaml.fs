namespace OpenSilver.Samples.Showcase

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Threading

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
