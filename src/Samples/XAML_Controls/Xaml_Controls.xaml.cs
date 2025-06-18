using Microsoft.Maui.Devices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace OpenSilver.Samples.Showcase;

public partial class Xaml_Controls : Page
{
    private readonly ItemsControl _itemsControl;
    private readonly Queue<object> _items;
    private readonly double _bottomOffset = 200.0;
    private readonly double _screenHeight;
    private ScrollViewer _scrollViewer;
    private readonly double _screenHeightMultiplier = 2.0;

    public Xaml_Controls()
    {
        InitializeComponent();

        var dataGridDemoIndex = SamplesPanel.Items.IndexOf(DataGridDemo);
        SamplesPanel.Items.Insert(dataGridDemoIndex, new DataGridGrouping { HorizontalAlignment = HorizontalAlignment.Center });

        if (DeviceInfo.Current.Platform == DevicePlatform.Android)
        {
            _itemsControl = SamplesPanel;
            ItemsControlLoadingBehavior.SetShowLoadingOnGenerating(_itemsControl, false);
            _items = new Queue<object>(_itemsControl.Items);
            _itemsControl.Items.Clear();
            _screenHeight = Application.Current.Host.Content.ActualHeight;

            _itemsControl.InvokeOnLayoutUpdated(async () =>
            {
                _scrollViewer = GetParentScrollViewer(this);
                await LoadMoreItems(0);
            });
        }
    }

    private async Task LoadMoreItems(double initialExtentHeight)
    {
        _scrollViewer.ScrollChanged -= OnScrollViewerScrollChanged;
        _itemsControl.ShowLoadingPopup();

        while (_items.Count > 0)
        {
            var item = _items.Dequeue();
            _itemsControl.Items.Add(item);

            if (item is FrameworkElement element)
            {
                var tcs = new TaskCompletionSource<object>();
                element.InvokeOnLayoutUpdated(() => tcs.SetResult(null));
                await tcs.Task;
            }

            if (_scrollViewer.ExtentHeight - initialExtentHeight > _screenHeight * _screenHeightMultiplier)
            {
                break;
            }
        }

        _itemsControl.HideLoadingPopup();

        if (_items.Count > 0)
        {
            _scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
        }
    }

    private async void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        var isCloseToBottom = e.VerticalOffset + e.ViewportHeight > e.ExtentHeight - _bottomOffset;
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
