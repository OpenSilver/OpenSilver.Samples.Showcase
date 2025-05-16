using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace OpenSilver.Samples.Showcase
{
    public static class ItemsControlLoadingBehavior
    {
        // Public attached property
        public static readonly DependencyProperty ShowLoadingOnGeneratingProperty =
            DependencyProperty.RegisterAttached(
                "ShowLoadingOnGenerating",
                typeof(bool),
                typeof(ItemsControlLoadingBehavior),
                new PropertyMetadata(false, OnShowLoadingOnGeneratingChanged)
            );

        public static void SetShowLoadingOnGenerating(DependencyObject element, bool value) =>
            element.SetValue(ShowLoadingOnGeneratingProperty, value);

        public static bool GetShowLoadingOnGenerating(DependencyObject element) =>
            (bool)element.GetValue(ShowLoadingOnGeneratingProperty);

        // Private attached properties to hold our handlers & state per-control
        private static readonly DependencyProperty StatusChangedHandlerProperty =
            DependencyProperty.RegisterAttached(
                "StatusChangedHandler",
                typeof(EventHandler),
                typeof(ItemsControlLoadingBehavior),
                null
            );

        private static readonly DependencyProperty ScrollChangedHandlerProperty =
            DependencyProperty.RegisterAttached(
                "ScrollChangedHandler",
                typeof(ScrollChangedEventHandler),
                typeof(ItemsControlLoadingBehavior),
                null
            );

        private static readonly DependencyProperty LoadingPopupProperty =
            DependencyProperty.RegisterAttached(
                "LoadingPopup",
                typeof(Popup),
                typeof(ItemsControlLoadingBehavior),
                null
            );

        private static readonly DependencyProperty HasUserScrolledProperty =
            DependencyProperty.RegisterAttached(
                "HasUserScrolled",
                typeof(bool),
                typeof(ItemsControlLoadingBehavior),
                new PropertyMetadata(false)
            );

        private static readonly DependencyProperty IsGeneratingProperty =
            DependencyProperty.RegisterAttached(
                "IsGenerating",
                typeof(bool),
                typeof(ItemsControlLoadingBehavior),
                new PropertyMetadata(false)
            );

        private static readonly DependencyProperty LoadedHandlerProperty =
            DependencyProperty.RegisterAttached(
                "LoadedHandler",
                typeof(RoutedEventHandler),
                typeof(ItemsControlLoadingBehavior),
                null
            );

        private static readonly DependencyProperty ScrollViewerProperty =
            DependencyProperty.RegisterAttached(
                "ScrollViewer",
                typeof(ScrollViewer),
                typeof(ItemsControlLoadingBehavior),
                null
            );

        private static void OnShowLoadingOnGeneratingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ItemsControl itemsControl))
                return;

            bool enabled = (bool)e.NewValue;
            if (enabled)
            {
                // Status changed handler (for container generation)
                EventHandler statusHandler = (s, args) => OnStatusChanged(itemsControl);
                itemsControl.SetValue(StatusChangedHandlerProperty, statusHandler);
                itemsControl.ItemContainerGenerator.StatusChanged += statusHandler;

                // Reset tracking properties
                itemsControl.SetValue(HasUserScrolledProperty, false);
                itemsControl.SetValue(IsGeneratingProperty, false);

                // Setup for finding ScrollViewer and attaching to scroll events
                RoutedEventHandler loadedHandler = (s, args) => OnItemsControlLoaded(itemsControl);
                itemsControl.SetValue(LoadedHandlerProperty, loadedHandler);
                itemsControl.Loaded += loadedHandler;
                
                // If the control is already loaded, call the handler immediately
                if (itemsControl.IsLoaded)
                {
                    OnItemsControlLoaded(itemsControl);
                }
            }
            else
            {
                // Detach status handler
                var statusHandler = (EventHandler)itemsControl.GetValue(StatusChangedHandlerProperty);
                if (statusHandler != null)
                {
                    itemsControl.ItemContainerGenerator.StatusChanged -= statusHandler;
                    itemsControl.ClearValue(StatusChangedHandlerProperty);
                }

                // Detach loaded handler
                var loadedHandler = (RoutedEventHandler)itemsControl.GetValue(LoadedHandlerProperty);
                if (loadedHandler != null)
                {
                    itemsControl.Loaded -= loadedHandler;
                    itemsControl.ClearValue(LoadedHandlerProperty);
                }

                // Detach ScrollChanged handler
                DetachScrollChangedHandler(itemsControl);

                // Clear tracking properties
                itemsControl.ClearValue(HasUserScrolledProperty);
                itemsControl.ClearValue(IsGeneratingProperty);
                itemsControl.ClearValue(ScrollViewerProperty);

                // Ensure any popup is closed
                var existing = (Popup)itemsControl.GetValue(LoadingPopupProperty);
                if (existing != null)
                {
                    existing.IsOpen = false;
                    itemsControl.ClearValue(LoadingPopupProperty);
                }
            }
        }

        private static void OnItemsControlLoaded(ItemsControl itemsControl)
        {
            AttachToScrollViewer(itemsControl);
        }

        private static void AttachToScrollViewer(ItemsControl itemsControl)
        {
            // First detach any existing handlers to avoid duplicates
            DetachScrollChangedHandler(itemsControl);

            // Find ScrollViewer
            ScrollViewer scrollViewer = FindScrollViewer(itemsControl);
            if (scrollViewer != null)
            {
                // Store reference to ScrollViewer
                itemsControl.SetValue(ScrollViewerProperty, scrollViewer);

                // Create and attach handler for ScrollChanged event
                ScrollChangedEventHandler scrollHandler = (s, e) => OnScrollChanged(itemsControl, e);
                itemsControl.SetValue(ScrollChangedHandlerProperty, scrollHandler);
                scrollViewer.ScrollChanged += scrollHandler;
            }
        }

        private static void DetachScrollChangedHandler(ItemsControl itemsControl)
        {
            var scrollViewer = itemsControl.GetValue(ScrollViewerProperty) as ScrollViewer;
            if (scrollViewer != null)
            {
                var scrollHandler = (ScrollChangedEventHandler)itemsControl.GetValue(ScrollChangedHandlerProperty);
                if (scrollHandler != null)
                {
                    scrollViewer.ScrollChanged -= scrollHandler;
                    itemsControl.ClearValue(ScrollChangedHandlerProperty);
                }
            }
        }

        private static void OnScrollChanged(ItemsControl itemsControl, ScrollChangedEventArgs e)
        {
            // Only consider it scrolling if the vertical offset changed
            if (e.VerticalChange != 0)
            {
                // Mark that the user has scrolled
                itemsControl.SetValue(HasUserScrolledProperty, true);

                // If we're generating containers, show the loading popup
                if ((bool)itemsControl.GetValue(IsGeneratingProperty))
                {
                    ShowLoadingPopup(itemsControl);
                }
            }
        }

        private static void OnStatusChanged(ItemsControl itemsControl)
        {
            var status = itemsControl.ItemContainerGenerator.Status;

            if (status == GeneratorStatus.GeneratingContainers)
            {
                // Mark that we're generating
                itemsControl.SetValue(IsGeneratingProperty, true);

                // Only show the loading popup if the user has scrolled
                if ((bool)itemsControl.GetValue(HasUserScrolledProperty))
                {
                    ShowLoadingPopup(itemsControl);
                }
            }
            else
            {
                // Mark that we're no longer generating
                itemsControl.SetValue(IsGeneratingProperty, false);

                // close & cleanup
                var popup = (Popup)itemsControl.GetValue(LoadingPopupProperty);
                if (popup != null)
                {
                    popup.IsOpen = false;
                    itemsControl.ClearValue(LoadingPopupProperty);
                }
            }
        }

        private static void ShowLoadingPopup(ItemsControl itemsControl)
        {
            // If we already have a popup open, no need to create another one
            var existingPopup = (Popup)itemsControl.GetValue(LoadingPopupProperty);
            if (existingPopup != null && existingPopup.IsOpen)
                return;

            // Create & show the popup
            var popup = new Popup
            {
                Child = new LoadingControl(),
                Placement = PlacementMode.Absolute,
                IsHitTestVisible = false,
                IsOpen = false
            };

            // Size & position to overlay the whole window
            var host = (FrameworkElement)Application.Current.RootVisual;
            popup.Width = host.ActualWidth;
            popup.Height = host.ActualHeight;

            itemsControl.SetValue(LoadingPopupProperty, popup);
            popup.IsOpen = true;
        }

        // Helper method to find the ScrollViewer in an ItemsControl
        private static ScrollViewer FindScrollViewer(ItemsControl itemsControl)
        {
            // First try to get the ScrollViewer from the template
            ScrollViewer scrollViewer = itemsControl.Template?.FindName("ScrollViewer", itemsControl) as ScrollViewer;
            if (scrollViewer != null)
                return scrollViewer;

            // Look for a ScrollViewer in the parent chain
            return FindScrollViewerInVisualTree(itemsControl);
        }

        // Helper method to find a ScrollViewer in the visual tree by walking up
        private static ScrollViewer FindScrollViewerInVisualTree(DependencyObject element)
        {
            if (element == null)
                return null;

            // Start with the parent of the provided element
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            
            while (parent != null)
            {
                // Try to cast the parent to a ScrollViewer
                ScrollViewer scrollViewer = parent as ScrollViewer;
                if (scrollViewer != null)
                    return scrollViewer;
                
                // Move up to the next parent
                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }
    }
}
