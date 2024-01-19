namespace OpenSilver.Samples.Showcase

open OpenSilver
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

type MethodToUpdateDom_Demo() as this =
    inherit MethodToUpdateDom_DemoXaml()

    do
        this.InitializeComponent()
        
    member private this.ButtonSetCSS_Click(sender: obj, e: RoutedEventArgs) =
        let div = Interop.GetDiv(this)

        Interop.ExecuteJavaScript("$0.style.textDecoration = 'line-through'", div) |> ignore

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "AttachedPropertiesWithMethodToUpdateDom.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/MethodToUpdateDom/AttachedPropertiesWithMethodToUpdateDom.cs");
            ViewSourceButtonInfo(TabHeader = "AttachedPropertiesWithMethodToUpdateDom.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/MethodToUpdateDom/AttachedPropertiesWithMethodToUpdateDom.vb");
            ViewSourceButtonInfo(TabHeader = "AttachedPropertiesWithMethodToUpdateDom.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/MethodToUpdateDom/AttachedPropertiesWithMethodToUpdateDom.fs");
            ViewSourceButtonInfo(TabHeader = "MethodToUpdateDom_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/MethodToUpdateDom/MethodToUpdateDom_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "MethodToUpdateDom_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/MethodToUpdateDom/MethodToUpdateDom_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "MethodToUpdateDom_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/MethodToUpdateDom/MethodToUpdateDom_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "MethodToUpdateDom_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/Interop_Samples/MethodToUpdateDom/MethodToUpdateDom_Demo.xaml.fs");
        ])
