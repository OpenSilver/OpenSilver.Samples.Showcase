using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace OpenSilver.Samples.Showcase
{
    public static class TreeViewHelpers
    {
        public static async Task<bool> SelectItemInTreeViewAsync(
            TreeView treeView,
            object itemToSelect,
            bool searchInCollapsedNodesToo = false)
        {
            await UIElementHelpers.WaitForLoadedAsync(treeView);
            await WaitForContainerGenerationAsync(treeView.ItemContainerGenerator);

            foreach (var item in treeView.Items)
            {
                if (treeView.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem treeViewItem)
                {
                    bool found = await SelectInTreeViewItemAsync(treeViewItem, itemToSelect, searchInCollapsedNodesToo);
                    if (found)
                        return true;
                }
            }

            return false;
        }

        private static async Task<bool> SelectInTreeViewItemAsync(
            TreeViewItem treeViewItem,
            object itemToSelect,
            bool searchInCollapsedNodesToo)
        {
            if (treeViewItem.DataContext == itemToSelect)
            {
                treeViewItem.IsSelected = true;
                //treeViewItem.BringIntoView();
                return true;
            }

            if (searchInCollapsedNodesToo)
            {
                treeViewItem.IsExpanded = true;
                treeViewItem.UpdateLayout(); // Make sure the child items are created
            }

            if (treeViewItem.Items.Count > 0)
            {
                await WaitForContainerGenerationAsync(treeViewItem.ItemContainerGenerator);
            }

            foreach (var child in treeViewItem.Items)
            {
                if (treeViewItem.ItemContainerGenerator.ContainerFromItem(child) is TreeViewItem childItem)
                {
                    bool found = await SelectInTreeViewItemAsync(childItem, itemToSelect, searchInCollapsedNodesToo);
                    if (found)
                        return true;
                }
            }

            return false;
        }

        public static Task WaitForContainerGenerationAsync(ItemContainerGenerator generator)
        {
            if (generator.Status == GeneratorStatus.ContainersGenerated)
            {
                return Task.CompletedTask;
            }

            var tcs = new TaskCompletionSource<object>();

            EventHandler handler = null;
            handler = (s, e) =>
            {
                if (generator.Status == GeneratorStatus.ContainersGenerated)
                {
                    generator.StatusChanged -= handler;
                    tcs.SetResult(null);
                }
            };

            generator.StatusChanged += handler;
            return tcs.Task;
        }
    }
}
