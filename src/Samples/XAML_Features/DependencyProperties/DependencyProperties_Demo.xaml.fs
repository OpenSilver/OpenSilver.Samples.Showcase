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

type DependencyProperties_Demo() as this =
    inherit DependencyProperties_DemoXaml()

    do
        this.InitializeComponent()

    static member DependencyProperty_Changed (d : DependencyObject) (newValue : obj) =
        MessageBox.Show("The DependencyProperty value has changed!") |> ignore

    static member mySampleDependencyPropertyProperty =
        DependencyProperty.Register(
            "MySampleDependencyProperty",
            typeof<int>,
            typeof<DependencyProperties_Demo>,
            new PropertyMetadata(defaultValue = 0, MethodToUpdateDom = DependencyProperties_Demo.DependencyProperty_Changed))

    member this.MySampleDependencyProperty
        with get() = (this.GetValue(DependencyProperties_Demo.mySampleDependencyPropertyProperty) :?> int)
        and set(value) = this.SetValue(DependencyProperties_Demo.mySampleDependencyPropertyProperty, value)

    member this.Decrementation_Click(sender : obj, e : RoutedEventArgs) =
        this.MySampleDependencyProperty <- this.MySampleDependencyProperty - 1

    member this.Incrementation_Click(sender : obj, e : RoutedEventArgs) =
        this.MySampleDependencyProperty <- this.MySampleDependencyProperty + 1

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "DependencyProperties_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/DependencyProperties/DependencyProperties_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "DependencyProperties_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/DependencyProperties/DependencyProperties_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "DependencyProperties_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/DependencyProperties/DependencyProperties_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "DependencyProperties_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/DependencyProperties/DependencyProperties_Demo.xaml.fs")
        ])
