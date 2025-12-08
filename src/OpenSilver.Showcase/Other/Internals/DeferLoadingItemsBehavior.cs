using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace OpenSilver.Showcase;

/// <summary>
/// Provides behavior for deferring the loading of items in an <see cref="ItemsControl"/>  until they are needed, based
/// on the scroll position of a parent <see cref="ScrollViewer"/>.
/// </summary>
/// <remarks>This behavior is designed to improve performance by loading items incrementally as the user scrolls.
/// Items are loaded in batches when the scroll position approaches the bottom of the current content. The behavior also
/// supports showing a loading indicator while items are being added.</remarks>
public class DeferLoadingItemsBehavior
{
    private readonly ItemsControl _itemsControl;
    private readonly Queue<object> _itemsQueue;
    private readonly double _loadItemsHeight;
    private readonly TimeSpan _maxLoadingTime;
    private readonly double _bottomScrollOffset;
    private ScrollViewer _scrollViewer;

    private DeferLoadingItemsBehavior(ItemsControl itemsControl, double loadItemsHeight, TimeSpan maxLoadingTime, double bottomScrollOffset = 0)
    {
        _itemsControl = itemsControl ?? throw new ArgumentNullException(nameof(itemsControl));

        if (loadItemsHeight <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(loadItemsHeight), "Load items height must be greater than zero.");
        }

        _loadItemsHeight = loadItemsHeight;
        _maxLoadingTime = maxLoadingTime;
        _bottomScrollOffset = bottomScrollOffset;

        ItemsControlLoadingBehavior.SetShowLoadingOnGenerating(_itemsControl, false);

        _itemsQueue = new Queue<object>(_itemsControl.Items);
        _itemsControl.Items.Clear();

        _itemsControl.InvokeOnLayoutUpdated(async () =>
        {
            _scrollViewer = GetParentScrollViewer(itemsControl)
                ?? throw new InvalidOperationException("DeferLoadingItemsBehavior requires the ItemsControl to be within a ScrollViewer.");
            await LoadMoreItems(0, showLoadingPopup: false);
        });
    }

    /// <summary>
    /// Attaches the deferred loading behavior to the specified <see cref="ItemsControl"/>.
    /// </summary>
    /// <param name="itemsControl">The ItemsControl to apply deferred loading behavior to.</param>
    /// <param name="loadItemsHeight">The height threshold that determines when to stop loading more items in a batch.
    /// Once items loaded in the current batch exceed this height, loading pauses until the next scroll to the bottom event.</param>
    /// <param name="maxLoadingTime">Maximum time allowed for loading items in a batch.</param>
    /// <param name="bottomScrollOffset">The distance from the bottom of the ScrollViewer at which to trigger loading more items.
    /// A value of 0 means loading occurs when scrolled all the way to the bottom, while larger values trigger loading earlier.</param>
    public static void Attach(ItemsControl itemsControl, double loadItemsHeight, TimeSpan maxLoadingTime, double bottomScrollOffset = 0)
    {
        _ = new DeferLoadingItemsBehavior(itemsControl, loadItemsHeight, maxLoadingTime, bottomScrollOffset);
    }

    private async Task LoadMoreItems(double initialExtentHeight, bool showLoadingPopup = true)
    {
        _scrollViewer.ScrollChanged -= OnScrollViewerScrollChanged;
        var timeStart = DateTime.Now;

        if (showLoadingPopup)
        {
            _itemsControl.ShowLoadingPopup();
        }

        while (_itemsQueue.Count > 0)
        {
            var item = _itemsQueue.Dequeue();
            _itemsControl.Items.Add(item);

            if (item is FrameworkElement element)
            {
                var tcs = new TaskCompletionSource<object>();
                element.InvokeOnLayoutUpdated(() => tcs.SetResult(null));
                await tcs.Task;
            }

            if (_scrollViewer.ExtentHeight - initialExtentHeight > _loadItemsHeight ||
                DateTime.Now - timeStart > _maxLoadingTime)
            {
                break;
            }
        }

        _itemsControl.HideLoadingPopup();

        if (_itemsQueue.Count > 0)
        {
            _scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
        }
    }

    private async void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        var isCloseToBottom = e.VerticalOffset + e.ViewportHeight > e.ExtentHeight - _bottomScrollOffset;
        if (isCloseToBottom)
        {
            await LoadMoreItems(e.ExtentHeight);
        }
    }

    private static ScrollViewer GetParentScrollViewer(DependencyObject child)
    {
        DependencyObject parent = child;
        while (parent is not null and not ScrollViewer)
        {
            parent = VisualTreeHelper.GetParent(parent);
        }
        return parent as ScrollViewer;
    }
}
