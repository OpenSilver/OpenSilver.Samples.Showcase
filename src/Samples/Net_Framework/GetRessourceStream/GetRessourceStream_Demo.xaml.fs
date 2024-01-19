namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Threading.Tasks
open System.Linq
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

type GetRessourceStream_Demo() as this =
    inherit GetRessourceStream_DemoXaml()
    
    let mutable currentUri : Uri = Unchecked.defaultof<_>

    do
        base.InitializeComponent()

    member private this.ViewFile_Click (sender : obj, e : RoutedEventArgs) =
        currentUri <- Uri("ms-appx:/Other/SampleText.txt")
        let getFileContent = this.RetrieveFileContent(currentUri)

        async {
            let! currentFileContent = getFileContent
            MessageBox.Show("Contains: " + currentFileContent) |> ignore
        } |> Async.Start

    member private this.RetrieveFileContent (uri : Uri) =
        async {
            let! resourceStream = Application.GetResourceStream uri |> Async.AwaitTask
            use currentReader = new StreamReader(resourceStream.Stream)

            MessageBox.Show("Opening : " + uri.AbsoluteUri) |> ignore
            let! result = currentReader.ReadToEndAsync() |> Async.AwaitTask
            return result
        }

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "GetResourceStream_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/GetResourceStream/GetResourceStream_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "GetResourceStream_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/GetResourceStream/GetResourceStream_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "GetResourceStream_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/GetResourceStream/GetResourceStream_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "GetResourceStream_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/GetResourceStream/GetResourceStream_Demo.xaml.fs")
        ])
