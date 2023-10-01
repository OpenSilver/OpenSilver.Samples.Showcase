Imports System.Collections.ObjectModel
#If SLMIGRATION
Imports System.Windows
Imports System.Windows.Controls
#Else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#End If

Namespace Global.OpenSilver.Samples.VBShowcase
    Public Partial Class Material_Design
        Inherits UserControl
        Public Sub New()
            Me.InitializeComponent()

#If OPENSILVER
            'DatePickerDemo.Visibility = Visibility.Collapsed; 
#End If

            Dim items As ObservableCollection(Of String) = New ObservableCollection(Of String)() From {
                "Item 1",
                "Item 2",
                "Item 3"
            }
            DataContext = items

            Me.DataGrid.ItemsSource = DataGridDataInstance.GetDataSet()
        End Sub

        Private Sub ButtonDisplayContextMenu_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim button = CType(sender, Button)
            button.ContextMenu.IsOpen = True
        End Sub

        Private Sub ButtonShowChildWindow_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim childWindow = New SampleMaterialChildWindow()
            childWindow.Show()
        End Sub

        Private Sub MenuEditDraft_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim menuItem = CType(sender, MenuItem)
            Dim message = String.Format("You clicked to edit the draft on the item ""{0}"".", CType(menuItem.DataContext, DataGridDataInstance).Name)
            MessageBox.Show(message)
        End Sub

        Private Sub MenuRemoveDraft_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim menuItem = CType(sender, MenuItem)
            Dim message = String.Format("You clicked to remove the draft on the item ""{0}"".", CType(menuItem.DataContext, DataGridDataInstance).Name)
            MessageBox.Show(message)
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim b = CType(sender, Button)
            Dim content = b.Content
            Dim tag = b.Tag
        End Sub
    End Class


    Public Class DataGridDataInstance
        Private Sub New(ByVal name As String, ByVal location As String, ByVal subscription As String)
            Me.Name = name
            Me.Location = location
            Me.Subscription = subscription
        End Sub

        Public Property Name As String

        Public Property Location As String

        Public Property Subscription As String

        Public Shared Function GetDataSet() As ObservableCollection(Of DataGridDataInstance)
            Return New ObservableCollection(Of DataGridDataInstance)() From {
    New DataGridDataInstance("Default-ApplicationInsights-EastUS", "East US", "Subscription-1"),
    New DataGridDataInstance("Default-SQL-CentralUS", "Central US", "Subscription-1"),
    New DataGridDataInstance("Default-SQL-NorthCentralUS", "North Central US", "Subscription-1"),
    New DataGridDataInstance("Default-SQL-SouthCentralUS", "South Central US", "Subscription-1"),
    New DataGridDataInstance("Default-Storage-NorthCentralUS", "North Central US", "Subscription-1"),
    New DataGridDataInstance("Default-Storage-WestUS", "West US", "Subscription-1"),
    New DataGridDataInstance("Default-Web-EastUS", "East US", "Subscription-1"),
    New DataGridDataInstance("Default-Web-NorthCentralUS", "North Central US", "Subscription-1")
}
        End Function
    End Class
End Namespace
