namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.Primitives
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

type ScrollBar_Demo() as this =
    inherit ScrollBar_DemoXaml()

    do
        this.InitializeComponent()

        this.TextDisplay.Text <- this.Scrollbar.Value.ToString("0.000")

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "ScrollBar_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ScrollBar/ScrollBar_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "ScrollBar_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ScrollBar/ScrollBar_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "ScrollBar_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ScrollBar/ScrollBar_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "ScrollBar_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ScrollBar/ScrollBar_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "DefaultScrollBarStyle.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Controls/ScrollBar/Styles/DefaultScrollBarStyle.xaml")
        ])

    member private this.ScrollBar_Scroll(sender : obj, e : ScrollEventArgs) =
        this.TextDisplay.Text <- e.NewValue.ToString("0.000")
