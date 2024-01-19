namespace OpenSilver.Samples.Showcase

open System
open System.Collections.Generic
open System.IO
open System.Linq
open System.Text
#if SLMIGRATION
open System.Windows
open System.Windows.Controls
open System.Windows.Media
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

type VisualTreeHelper_Demo() as this =
    inherit VisualTreeHelper_DemoXaml()

    do
        this.InitializeComponent()

    member this.RevealTree_Click(sender : obj, e : RoutedEventArgs) =
        let finalVisualTree = VisualTreeHelper_Demo.GetVisualTree(this)
        MessageBox.Show(finalVisualTree) |> ignore

    static member GetVisualTree(parent : DependencyObject) =
        let visualTreeStringBuilder = StringBuilder("Visual Tree : \n\n")
        VisualTreeHelper_Demo.GetChildren(parent, visualTreeStringBuilder)
        visualTreeStringBuilder.ToString()

    static member private GetChildren (parent : DependencyObject, visualTreeStringBuilder : StringBuilder, ?indentation : int) =
        let indentation = defaultArg indentation 0
        let childrenCount = VisualTreeHelper.GetChildrenCount(parent)

        if childrenCount > 0 then
            for i in 0 .. childrenCount - 1 do
                for e in 0 .. indentation - 1 do
                    visualTreeStringBuilder.Append("    ") |> ignore

                let currentChild = VisualTreeHelper.GetChild(parent, i)
                visualTreeStringBuilder.AppendLine(currentChild.ToString()) |> ignore
                VisualTreeHelper_Demo.GetChildren(currentChild, visualTreeStringBuilder, indentation + 1)

    member private this.ButtonViewSource_Click(sender : obj, e : RoutedEventArgs) =
        ViewSourceButtonHelper.ViewSource([
            ViewSourceButtonInfo(TabHeader = "VisualTreeHelper_Demo.xaml", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml");
            ViewSourceButtonInfo(TabHeader = "VisualTreeHelper_Demo.xaml.cs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml.cs");
            ViewSourceButtonInfo(TabHeader = "VisualTreeHelper_Demo.xaml.vb", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml.vb");
            ViewSourceButtonInfo(TabHeader = "VisualTreeHelper_Demo.xaml.fs", FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/VisualTreeHelper/VisualTreeHelper_Demo.xaml.fs")
        ])
