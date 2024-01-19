namespace OpenSilver.Samples.Showcase

open OpenSilver.Extensions.FileSystem
open Ionic.Zip
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

type Zip_Demo() as this =
    inherit Zip_DemoXaml()

    do
        this.InitializeComponent()

    member private this.ButtonGenerateZip_Click (sender: obj, e: RoutedEventArgs) =
        async {
            use zipFile = new ZipFile()
            do! zipFile.AddFile("SampleText.txt", "Hello World!")
            let! jsBlob = zipFile.SaveToJavaScriptBlob()
            do! FileSaver.SaveJavaScriptBlobToFile(jsBlob, "MyTestFile.zip")
        } |> Async.Start

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "Zip_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/Zip_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "Zip_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/Zip_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "ZipFile.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/ZipFile.cs");
            ViewSourceButtonInfo(TabHeader = "Zip_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/Zip_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "ZipFile.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/ZipFile.vb");
            ViewSourceButtonInfo(TabHeader = "Zip_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/Zip_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "ZipFile.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Net_Framework/Zip/ZipFile.fs")
        ])
