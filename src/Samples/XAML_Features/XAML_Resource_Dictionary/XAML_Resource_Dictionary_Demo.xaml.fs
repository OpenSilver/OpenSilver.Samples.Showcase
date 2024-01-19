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

type XAML_Resource_Dictionary_Demo() as this =
    inherit XAML_Resource_Dictionary_DemoXaml()
    
    do
        this.InitializeComponent()

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "XAML_Resource_Dictionary_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/XAML_Resource_Dictionary/XAML_Resource_Dictionary_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "XAML_Resource_Dictionary_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/XAML_Resource_Dictionary/XAML_Resource_Dictionary_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "XAML_Resource_Dictionary_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/XAML_Resource_Dictionary/XAML_Resource_Dictionary_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "XAML_Resource_Dictionary_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/XAML_Resource_Dictionary/XAML_Resource_Dictionary_Demo.xaml.fs");
            ViewSourceButtonInfo(TabHeader = "Style1.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/XAML_Resource_Dictionary/Dictionaries/Style1.xaml");
            ViewSourceButtonInfo(TabHeader = "Style2.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/XAML_Resource_Dictionary/Dictionaries/Style2.xaml")
        ])
