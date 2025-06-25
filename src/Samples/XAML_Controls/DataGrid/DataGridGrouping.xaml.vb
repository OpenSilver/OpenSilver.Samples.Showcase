Imports OpenSilver.Samples.Showcase.Search
Imports System.Windows.Controls
Imports System.Windows.Data

Namespace Global.OpenSilver.Samples.Showcase
    <SearchKeywords("data", "display", "grid", "table", "binding", "grouping")>
    Partial Public Class DataGridGrouping
        Inherits UserControl

        Public Sub New()
            InitializeComponent()

            Dim pcv As New PagedCollectionView(Contact.People)
            pcv.GroupDescriptions.Add(New PropertyGroupDescription(NameOf(Contact.State)))
            pcv.GroupDescriptions.Add(New PropertyGroupDescription(NameOf(Contact.City)))
            dataGrid.ItemsSource = pcv
        End Sub

End Class
End Namespace