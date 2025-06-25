
/*===================================================================================
* 
*   Copyright (c) Userware/OpenSilver.net
*      
*   This file is part of OpenSilver (https://opensilver.net), which is
*   licensed under the MIT license: https://opensource.org/licenses/MIT
*   
*   IMPORTANT: Make sure to preserve this copyright notice on all copies of this code.
*  
\*====================================================================================*/

using OpenSilver.Animations.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace OpenSilver.Animations
{
    public class Animation
    {

        public static IAnimationType GetOnAppear(DependencyObject obj)
        {
            return (IAnimationType)obj.GetValue(OnAppearProperty);
        }
        public static void SetOnAppear(DependencyObject obj, IAnimationType value)
        {
            obj.SetValue(OnAppearProperty, value);
        }
        public static readonly DependencyProperty OnAppearProperty =
            DependencyProperty.RegisterAttached("OnAppear", typeof(IAnimationType), typeof(Animation), new PropertyMetadata(null, OnAppearProperty_Changed));

        private static void OnAppearProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.IsInDesignTool)
            {
                return;
            }

            if (d is FrameworkElement element)
            {
                // Always clean up existing subscriptions first
                CleanupElement(element);

                // If new value is not null, set up new subscriptions
                if (e.NewValue is IAnimationType)
                {
                    SetupElement(element);
                }
            }
        }

        private static void SetupElement(FrameworkElement element)
        {
            // Subscribe to Loaded/Unloaded to manage IsVisibleChanged subscription lifecycle
            element.Loaded += Element_Loaded;
            element.Unloaded += Element_Unloaded;

            // If element is already loaded, subscribe immediately
            if (element.IsLoaded)
            {
                element.IsVisibleChanged += ElementToAnimate_IsVisibleChanged;
            }
        }

        private static void CleanupElement(FrameworkElement element)
        {
            // Unsubscribe from all events
            element.Loaded -= Element_Loaded;
            element.Unloaded -= Element_Unloaded;
            element.IsVisibleChanged -= ElementToAnimate_IsVisibleChanged;
        }

        private static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                // Element is now in visual tree, start listening for visibility changes
                element.IsVisibleChanged -= ElementToAnimate_IsVisibleChanged; // Ensure no duplicates
                element.IsVisibleChanged += ElementToAnimate_IsVisibleChanged;
            }
        }

        private static void Element_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                // Element is being removed from visual tree, stop listening for visibility changes
                element.IsVisibleChanged -= ElementToAnimate_IsVisibleChanged;
            }
        }

        private static void ElementToAnimate_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FrameworkElement elementToAnimate)
            {
                IAnimationType animationType = GetOnAppear(elementToAnimate);

                if (animationType != null && elementToAnimate.IsVisible)
                {
                    animationType.AnimateElementIn(elementToAnimate);
                }
            }
        }

        public static double SlowDownAnimationsForDebugging
        {
            get => StoryboardsHelper.SlowDownAnimationsForDebugging;
            set => StoryboardsHelper.SlowDownAnimationsForDebugging = value;
        }
    }
}
