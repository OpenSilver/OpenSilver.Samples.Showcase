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

type HyperlinkButton_Demo() as this =
    inherit HyperlinkButton_DemoXaml()
    
    do
        this.InitializeComponent()

#if OPENSILVER
        this.HyperlinkButtonDemo.NavigateUri <- Uri("http://www.opensilver.net")
#endif

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "HyperlinkButton.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/HyperlinkButton/HyperlinkButton.xaml");
            ViewSourceButtonInfo(TabHeader = "HyperlinkButton.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/HyperlinkButton/HyperlinkButton.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "HyperlinkButton.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/HyperlinkButton/HyperlinkButton.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "HyperlinkButton.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/HyperlinkButton/HyperlinkButton.xaml.fs")
        ])
