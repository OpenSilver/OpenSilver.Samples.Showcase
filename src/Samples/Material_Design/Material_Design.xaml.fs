namespace OpenSilver.Samples.Showcase

open System.Collections.ObjectModel
open System.Windows
open System.Windows.Controls
open System.Windows.Input


type Material_Design() as this=
    inherit Material_DesignXaml()
    
    let items = ObservableCollection<string>(["Item 1"; "Item 2"; "Item 3"])

    do
        this.InitializeComponent()
        this.DataContext <- items
        this.DataGrid.ItemsSource <- DataGridDataInstance.GetDataSet()

    member this.DisplayContextMenu_Click(sender: obj, e: MouseButtonEventArgs) =
        let cc = unbox<ContentControl>(sender)
        cc.ContextMenu.HorizontalOffset <- e.GetPosition(null).X
        cc.ContextMenu.VerticalOffset <- e.GetPosition(null).Y
        cc.ContextMenu.IsOpen <- true

    member this.ContextMenu_Closed(sender: obj, e: RoutedEventArgs) =
        let contextMenu = unbox<ContextMenu>(sender)
        contextMenu.HorizontalOffset <- 0.0
        contextMenu.VerticalOffset <- 0.0

    member this.ButtonShowChildWindow_Click(sender: obj, e: RoutedEventArgs) =
        let childWindow = new SampleMaterialChildWindow()
        childWindow.Show()

    member this.MenuEditDraft_Click(sender: obj, e: RoutedEventArgs) =
        let menuItem = unbox<MenuItem>(sender)
        let message = sprintf "You clicked to edit the draft on the item \"%s\"." (menuItem.DataContext :?> DataGridDataInstance).Name
        MessageBox.Show(message) |> ignore

    member this.MenuRemoveDraft_Click(sender: obj, e: RoutedEventArgs) =
        let menuItem = unbox<MenuItem>(sender)
        let message = sprintf "You clicked to remove the draft on the item \"%s\"." (menuItem.DataContext :?> DataGridDataInstance).Name
        MessageBox.Show(message) |> ignore

    member this.Button_Click(sender: obj, e: RoutedEventArgs) =
        let b = unbox<Button>(sender)
        let content = b.Content
        let tag = b.Tag
        ()

and DataGridDataInstance(name: string, location: string, subscription: string) =
    member val Name = name with get, set
    member val Location = location with get, set
    member val Subscription = subscription with get, set

    static member GetDataSet() =
        ObservableCollection<DataGridDataInstance>
            [ DataGridDataInstance("Default-ApplicationInsights-EastUS", "East US", "Subscription-1")
              DataGridDataInstance("Default-SQL-CentralUS", "Central US", "Subscription-1")
              DataGridDataInstance("Default-SQL-NorthCentralUS", "North Central US", "Subscription-1")
              DataGridDataInstance("Default-SQL-SouthCentralUS", "South Central US", "Subscription-1")
              DataGridDataInstance("Default-Storage-NorthCentralUS", "North Central US", "Subscription-1")
              DataGridDataInstance("Default-Storage-WestUS", "West US", "Subscription-1")
              DataGridDataInstance("Default-Web-EastUS", "East US", "Subscription-1")
              DataGridDataInstance("Default-Web-NorthCentralUS", "North Central US", "Subscription-1") ]
