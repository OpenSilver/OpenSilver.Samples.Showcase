using OpenSilver.Animations;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace OpenSilver.Showcase
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

        // Private attached properties to hold our handler & popup per-control
        private static readonly DependencyProperty StatusChangedHandlerProperty =
            DependencyProperty.RegisterAttached(
                "StatusChangedHandler",
                typeof(EventHandler),
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

        public static void ShowLoadingPopup(this ItemsControl itemsControl, IAnimationType animation = null)
        {
            // create & show
            var popup = new Popup
            {
                Child = new LoadingControl(animation),
                Placement = PlacementMode.Absolute,
                IsHitTestVisible = false,
                IsOpen = false
            };

            /*
            // size & position to overlay the ItemsControl
            var root = Application.Current.RootVisual as FrameworkElement;
            var transform = itemsControl.TransformToVisual(root);
            var topLeft = transform.Transform(new Point(0, 0));
            popup.HorizontalOffset = topLeft.X;
            popup.VerticalOffset = topLeft.Y;
            popup.Width = itemsControl.ActualWidth;
            popup.Height = itemsControl.ActualHeight;
            */

            // size & position to overlay the whole window
            var host = (FrameworkElement)Application.Current.RootVisual;
            popup.Width = host.ActualWidth;
            popup.Height = host.ActualHeight;

            itemsControl.SetValue(LoadingPopupProperty, popup);
            popup.IsOpen = true;
        }

        public static void HideLoadingPopup(this ItemsControl itemsControl)
        {
            // close & cleanup
            var popup = (Popup)itemsControl.GetValue(LoadingPopupProperty);
            if (popup != null)
            {
                popup.IsOpen = false;
                itemsControl.ClearValue(LoadingPopupProperty);
            }
        }

        private static void OnShowLoadingOnGeneratingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ItemsControl itemsControl))
                return;

            bool enabled = (bool)e.NewValue;
            if (enabled)
            {
                // attach
                EventHandler handler = (s, args) => OnStatusChanged(itemsControl);
                itemsControl.SetValue(StatusChangedHandlerProperty, handler);
                itemsControl.ItemContainerGenerator.StatusChanged += handler;
            }
            else
            {
                // detach
                var handler = (EventHandler)itemsControl.GetValue(StatusChangedHandlerProperty);
                if (handler != null)
                {
                    itemsControl.ItemContainerGenerator.StatusChanged -= handler;
                    itemsControl.ClearValue(StatusChangedHandlerProperty);
                }

                // ensure any popup is closed
                HideLoadingPopup(itemsControl);
            }
        }

        private static void OnStatusChanged(ItemsControl itemsControl)
        {
            var status = itemsControl.ItemContainerGenerator.Status;

            if (status == GeneratorStatus.GeneratingContainers)
            {
                ShowLoadingPopup(itemsControl, new FadeAndScale { Bounciness = 0.3, Delay = 1000 });
            }
            else
            {
                HideLoadingPopup(itemsControl);
            }
        }
    }
}
