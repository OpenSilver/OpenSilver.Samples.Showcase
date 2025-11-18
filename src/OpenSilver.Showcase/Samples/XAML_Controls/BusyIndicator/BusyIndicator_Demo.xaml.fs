namespace OpenSilver.Showcase

open System
open System.Windows
open System.Windows.Threading
open OpenSilver.Showcase.Search

[<SearchKeywords("loading", "busy", "indicator", "status", "loading", "progress")>]
type BusyIndicator_Demo() as this =
    inherit BusyIndicator_DemoXaml()

    let timer = new DispatcherTimer(Interval = TimeSpan(0, 0, 3))

    do
        this.InitializeComponent()
        timer.Tick.Add(fun args -> this.stopBusyIndicator(timer, args))

    member private this.Button_Click(sender: obj, e: RoutedEventArgs) =
        this.MyBusyIndicator.IsBusy <- true
        timer.Start()

    member private this.stopBusyIndicator(sender: obj, e: EventArgs) =
        timer.Stop()
        this.MyBusyIndicator.IsBusy <- false
