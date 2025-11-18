namespace OpenSilver.Showcase

open OpenSilver.Showcase.Search
open System.Windows.Data

[<SearchKeywords("data", "display", "grid", "table", "binding", "grouping")>]
type DataGridGrouping() as this =
    inherit DataGridGroupingXaml()
    do
        this.InitializeComponent()

        let pcv = PagedCollectionView(Contact.People)
        pcv.GroupDescriptions.Add(PropertyGroupDescription(nameof Unchecked.defaultof<Contact>.State))
        pcv.GroupDescriptions.Add(PropertyGroupDescription(nameof Unchecked.defaultof<Contact>.City))
        this.dataGrid.ItemsSource <- pcv
