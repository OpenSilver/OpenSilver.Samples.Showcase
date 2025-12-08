Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Media

Namespace OpenSilver.Samples.Showcase

    Partial Public Class TreeViewerWindow
        Inherits ChildWindow

        Public Sub New(root As DependencyObject)
            InitializeComponent()

            Dim visualTree = BuildVisualTree(root)
            Dim logicalTree = BuildLogicalTree(root)

            VisualTreeView.ItemsSource = New List(Of TreeNode) From {visualTree}
            LogicalTreeView.ItemsSource = New List(Of TreeNode) From {logicalTree}

            VisualTreeView.InvokeOnLayoutUpdated(Sub() VisualTreeView.ExpandAll())
            LogicalTreeView.InvokeOnLayoutUpdated(Sub() LogicalTreeView.ExpandAll())
        End Sub

        Private Function BuildVisualTree(obj As DependencyObject) As TreeNode
            Dim node As New TreeNode With {.Name = obj.GetType().Name}

            Dim count As Integer = VisualTreeHelper.GetChildrenCount(obj)
            For i As Integer = 0 To count - 1
                Dim child = VisualTreeHelper.GetChild(obj, i)
                node.Children.Add(BuildVisualTree(child))
            Next

            Return node
        End Function

        Private Function BuildLogicalTree(obj As DependencyObject) As TreeNode
            Dim node As New TreeNode With {.Name = obj.GetType().Name}

            For Each child In LogicalTreeHelper.GetChildren(obj)
                If TypeOf child Is DependencyObject Then
                    node.Children.Add(BuildLogicalTree(CType(child, DependencyObject)))
                Else
                    node.Children.Add(New TreeNode With {.Name = child.ToString()})
                End If
            Next

            Return node
        End Function
    End Class

    Public Class TreeNode
        Public Property Name As String
        Public Property Children As New List(Of TreeNode)
    End Class

End Namespace
