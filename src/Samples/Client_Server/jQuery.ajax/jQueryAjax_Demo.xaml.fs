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

type jQueryAjax_Demo() as this = 
    inherit jQueryAjax_DemoXaml()
    
    do
        this.InitializeComponent()

    member this.Button_Click (sender: obj, e: RoutedEventArgs) =
        async {
            try
                let! result = OpenSilver.Extensions.jQueryAjaxHelper.MakeAjaxCall("http://fiddle.jshell.net/echo/html/", "some sample text", "post")
                MessageBox.Show("The server returned the following result: " + result) |> ignore
            with
            | ex -> MessageBox.Show(ex.Message) |> ignore
        } |> Async.Start


    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "jQueryAjax_Demo.xaml",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "jQueryAjax_Demo.xaml.cs",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "jQueryAjaxHelper.cs",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjaxHelper.cs");
            ViewSourceButtonInfo(TabHeader = "jQueryAjax_Demo.xaml.vb",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "jQueryAjaxHelper.vb",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjaxHelper.vb");
            ViewSourceButtonInfo(TabHeader = "jQueryAjax_Demo.xaml.fs",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjax_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "jQueryAjaxHelper.fs",
                                 FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Client_Server/jQuery.ajax/jQueryAjaxHelper.fs")
        ])
