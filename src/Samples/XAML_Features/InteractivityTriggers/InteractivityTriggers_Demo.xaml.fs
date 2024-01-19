namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.Collections.ObjectModel
open System.IO
open System.Linq
open System.Windows.Input
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

type InteractivityTestViewModel() =
    let testCommand = InteractivityTestICommandClass()

    member this.TestCommand with get() = testCommand :> ICommand

and InteractivityTestICommandClass() =
    let canExecuteChanged = new Event<EventHandler, EventArgs>()

    interface ICommand with
        member this.CanExecute parameter = true

        member this.Execute parameter = 
            MessageBox.Show("The command was successfully executed.") |> ignore

        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish        

type InteractivityTriggers_Demo() as this =
    inherit InteractivityTriggers_DemoXaml()

    do
        this.InitializeComponent()
        this.DataContext <- InteractivityTestViewModel()

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Commanding_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/InteractivityTriggers/InteractivityTriggers_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Commanding_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/InteractivityTriggers/InteractivityTriggers_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "Commanding_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/InteractivityTriggers/InteractivityTriggers_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "Commanding_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/InteractivityTriggers/InteractivityTriggers_Demo.xaml.fs")
        ])

