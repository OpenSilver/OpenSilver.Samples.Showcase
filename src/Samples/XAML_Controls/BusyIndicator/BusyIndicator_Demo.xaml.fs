namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Threading
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Data
open System.Windows.Input
open System.Windows.Media
open System.Windows.Navigation
open System.Windows.Threading

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
