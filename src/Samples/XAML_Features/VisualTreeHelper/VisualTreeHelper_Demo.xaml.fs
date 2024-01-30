namespace OpenSilver.Samples.Showcase

open System.Text
open System.Windows
open System.Windows.Controls
open System.Windows.Media

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
