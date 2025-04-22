using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase;

public partial class TreeViewerWindow : ChildWindow
{
    public TreeViewerWindow(DependencyObject root)
    {
        InitializeComponent();

        var visualTree = BuildVisualTree(root);
        var logicalTree = BuildLogicalTree(root);

        VisualTreeView.ItemsSource = new List<TreeNode> { visualTree };
        LogicalTreeView.ItemsSource = new List<TreeNode> { logicalTree };

        VisualTreeView.InvokeOnLayoutUpdated(VisualTreeView.ExpandAll);
        LogicalTreeView.InvokeOnLayoutUpdated(LogicalTreeView.ExpandAll);
    }

    private TreeNode BuildVisualTree(DependencyObject obj)
    {
        var node = new TreeNode { Name = obj.GetType().Name };

        int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
        for (int i = 0; i < childrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(obj, i);
            node.Children.Add(BuildVisualTree(child));
        }

        return node;
    }

    private TreeNode BuildLogicalTree(DependencyObject obj)
    {
        var node = new TreeNode { Name = obj.GetType().Name };

        foreach (var child in LogicalTreeHelper.GetChildren(obj))
        {
            if (child is DependencyObject depChild)
            {
                node.Children.Add(BuildLogicalTree(depChild));
            }
            else
            {
                node.Children.Add(new TreeNode { Name = child.ToString() });
            }
        }

        return node;
    }
}

public class TreeNode
{
    public string Name { get; set; }
    public List<TreeNode> Children { get; set; } = [];
}
