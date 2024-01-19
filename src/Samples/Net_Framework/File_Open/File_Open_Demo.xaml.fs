namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
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

type File_Open_Demo() as this =
    inherit File_Open_DemoXaml()

    do
        this.InitializeComponent()

    member this.OnFileOpened(sender : obj, e : OpenSilver.Extensions.FileOpenDialog.FileOpenedEventArgs) =
        MessageBox.Show(e.Text) |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "File_Open_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/File_Open_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "File_Open_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/File_Open_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "FileOpenDialog.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/FileOpenDialog.cs");
            ViewSourceButtonInfo(TabHeader = "File_Open_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/File_Open_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "FileOpenDialog.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/FileOpenDialog.vb");
            ViewSourceButtonInfo(TabHeader = "File_Open_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/File_Open_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "FileOpenDialog.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/File_Open/FileOpenDialog.fs")
        ])
