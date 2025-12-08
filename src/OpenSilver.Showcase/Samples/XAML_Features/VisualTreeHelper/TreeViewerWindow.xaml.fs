namespace OpenSilver.Showcase

open System.Collections.Generic
open System.Windows
open System.Windows.Controls
open System.Windows.Controls.Primitives
open System.Windows.Media

type TreeViewerWindow(root: DependencyObject) as this =
    inherit TreeViewerWindowXaml()

    do
        this.InitializeComponent()

        let visualTree = this.BuildVisualTree(root)
        let logicalTree = this.BuildLogicalTree(root)

        this.VisualTreeView.ItemsSource <- [ visualTree ]
        this.LogicalTreeView.ItemsSource <- [ logicalTree ]

        this.VisualTreeView.InvokeOnLayoutUpdated(fun () -> this.VisualTreeView.ExpandAll())
        this.LogicalTreeView.InvokeOnLayoutUpdated(fun () -> this.LogicalTreeView.ExpandAll())

    member private this.BuildVisualTree(obj: DependencyObject) =
        let node = TreeNode(obj.GetType().Name)
        let count = VisualTreeHelper.GetChildrenCount(obj)
        for i in 0 .. count - 1 do
            let child = VisualTreeHelper.GetChild(obj, i)
            node.Children.Add(this.BuildVisualTree(child))
        node

    member private this.BuildLogicalTree(obj: DependencyObject) =
        let node = TreeNode(obj.GetType().Name)
        for child in LogicalTreeHelper.GetChildren(obj) do
            match child with
            | :? DependencyObject as depChild ->
                node.Children.Add(this.BuildLogicalTree(depChild))
            | _ ->
                node.Children.Add(TreeNode(child.ToString()))
        node
