namespace OpenSilver.Samples.Showcase

open OpenSilver.Extensions.FileSystem
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

type File_Save_Demo() as this =
    inherit File_Save_DemoXaml()

    do
        this.InitializeComponent()

    member this.ButtonSave_Click (sender : obj, e : RoutedEventArgs) =
        async {
            do! FileSaver.SaveTextToFile("Hello World!", "MyTestFile.txt")
        } |> Async.StartImmediate

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "File_Save_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/File_Save_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "File_Save_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/File_Save_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "FileSaver.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/FileSaver.cs");
            ViewSourceButtonInfo(TabHeader = "File_Save_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/File_Save_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "FileSaver.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/FileSaver.vb");
            ViewSourceButtonInfo(TabHeader = "File_Save_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/File_Save_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "FileSaver.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Save/FileSaver.fs")
        ])
