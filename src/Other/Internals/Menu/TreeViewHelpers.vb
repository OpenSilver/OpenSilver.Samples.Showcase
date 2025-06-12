Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives

Namespace OpenSilver.Samples.Showcase
    Public NotInheritable Class TreeViewHelpers
        Public Shared Async Function SelectItemInTreeViewAsync(
            treeView As TreeView,
            itemToSelect As Object,
            Optional searchInCollapsedNodesToo As Boolean = False) As Task(Of Boolean)

            Await UIElementHelpers.WaitForLoadedAsync(treeView)
            Await WaitForContainerGenerationAsync(treeView.ItemContainerGenerator)

            For Each item In treeView.Items
                Dim treeViewItem = TryCast(treeView.ItemContainerGenerator.ContainerFromItem(item), TreeViewItem)
                If treeViewItem IsNot Nothing Then
                    Dim found = Await SelectInTreeViewItemAsync(treeViewItem, itemToSelect, searchInCollapsedNodesToo)
                    If found Then
                        Return True
                    End If
                End If
            Next

            Return False
        End Function

        Private Shared Async Function SelectInTreeViewItemAsync(
            treeViewItem As TreeViewItem,
            itemToSelect As Object,
            searchInCollapsedNodesToo As Boolean) As Task(Of Boolean)

            If treeViewItem.DataContext Is itemToSelect Then
                treeViewItem.IsSelected = True
                Return True
            End If

            If searchInCollapsedNodesToo Then
                treeViewItem.IsExpanded = True
                treeViewItem.UpdateLayout() ' Make sure the child items are created
            End If

            If treeViewItem.Items.Count > 0 Then
                Await WaitForContainerGenerationAsync(treeViewItem.ItemContainerGenerator)
            End If

            For Each child In treeViewItem.Items
                Dim childItem = TryCast(treeViewItem.ItemContainerGenerator.ContainerFromItem(child), TreeViewItem)
                If childItem IsNot Nothing Then
                    Dim found = Await SelectInTreeViewItemAsync(childItem, itemToSelect, searchInCollapsedNodesToo)
                    If found Then
                        Return True
                    End If
                End If
            Next

            Return False
        End Function

        Public Shared Function WaitForContainerGenerationAsync(generator As ItemContainerGenerator) As Task
            If generator.Status = GeneratorStatus.ContainersGenerated Then
                Return Task.CompletedTask
            End If

            Dim tcs As New TaskCompletionSource(Of Object)()

            Dim handler As EventHandler = Nothing
            handler = Sub(s, e)
                          If generator.Status = GeneratorStatus.ContainersGenerated Then
                              RemoveHandler generator.StatusChanged, handler
                              tcs.SetResult(Nothing)
                          End If
                      End Sub

            AddHandler generator.StatusChanged, handler
            Return tcs.Task
        End Function
    End Class
End Namespace
