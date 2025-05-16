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

        private static readonly DependencyProperty MouseWheelHandlerProperty =
            DependencyProperty.RegisterAttached(
                "MouseWheelHandler",
                typeof(MouseWheelEventHandler),
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

                // Mouse wheel handler (for scroll detection)
                MouseWheelEventHandler mouseWheelHandler = (s, args) => OnMouseWheel(itemsControl, args);
                itemsControl.SetValue(MouseWheelHandlerProperty, mouseWheelHandler);
                itemsControl.MouseWheel += mouseWheelHandler;

                // Reset tracking properties
                itemsControl.SetValue(HasUserScrolledProperty, false);
                itemsControl.SetValue(IsGeneratingProperty, false);
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

                // Detach mouse wheel handler
                var mouseWheelHandler = (MouseWheelEventHandler)itemsControl.GetValue(MouseWheelHandlerProperty);
                if (mouseWheelHandler != null)
                {
                    itemsControl.MouseWheel -= mouseWheelHandler;
                    itemsControl.ClearValue(MouseWheelHandlerProperty);
                }

                // Clear tracking properties
                itemsControl.ClearValue(HasUserScrolledProperty);
                itemsControl.ClearValue(IsGeneratingProperty);

                // Ensure any popup is closed
                var existing = (Popup)itemsControl.GetValue(LoadingPopupProperty);
                if (existing != null)
                {
                    existing.IsOpen = false;
                    itemsControl.ClearValue(LoadingPopupProperty);
                }
            }
        }

        private static void OnMouseWheel(ItemsControl itemsControl, MouseWheelEventArgs e)
        {
            // Mark that the user has scrolled
            itemsControl.SetValue(HasUserScrolledProperty, true);

            // If we're generating containers, show the loading popup
            if ((bool)itemsControl.GetValue(IsGeneratingProperty))
            {
                ShowLoadingPopup(itemsControl);
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
    }
}
