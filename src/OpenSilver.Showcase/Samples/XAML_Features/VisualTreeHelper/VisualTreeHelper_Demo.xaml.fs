namespace OpenSilver.Showcase

open System.Windows
open OpenSilver.Showcase.Search

[<SearchKeywords("logicaltreehelper", "UI elements", "XAML", "hierarchy", "UI", "treeview")>]
type VisualTreeHelper_Demo() as this =
    inherit VisualTreeHelper_DemoXaml()
    do this.InitializeComponent()

    member this.RevealTree_Click(sender: obj, e: RoutedEventArgs) =
        let viewer = new TreeViewerWindow(this)
        viewer.Show()
