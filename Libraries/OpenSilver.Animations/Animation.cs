
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
using System.Xml.Linq;

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
            // Output to the log for debugging:
            if (LogAnimationsForDebugging)
            {
                LogForDebugging("Animations: set up", element);
            }

            // Subscribe to Loaded/Unloaded to manage IsVisibleChanged subscription lifecycle
            element.Loaded += Element_Loaded;
            element.Unloaded += Element_Unloaded;

            // If element is already loaded, subscribe immediately
            if (element.IsLoaded)
            {
                element.IsVisibleChanged -= ElementToAnimate_IsVisibleChanged; // Ensure no duplicates
                element.IsVisibleChanged += ElementToAnimate_IsVisibleChanged;

                // If element is already visible, do the OnAppear animation immediately:
                if (element.IsVisible)
                {
                    DoOnAppearAnimationIfAny(element);
                }
            }
        }

        private static void CleanupElement(FrameworkElement element)
        {
            // Output to the log for debugging:
            if (LogAnimationsForDebugging)
            {
                LogForDebugging("Animations: cleaned up", element);
            }

            // Unsubscribe from all events
            element.Loaded -= Element_Loaded;
            element.Unloaded -= Element_Unloaded;
            element.IsVisibleChanged -= ElementToAnimate_IsVisibleChanged;
        }

        private static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                // Output to the log for debugging:
                if (LogAnimationsForDebugging)
                {
                    LogForDebugging("Animations: loaded event handled", element);
                }

                // Element is now in visual tree, start listening for visibility changes
                element.IsVisibleChanged -= ElementToAnimate_IsVisibleChanged; // Ensure no duplicates
                element.IsVisibleChanged += ElementToAnimate_IsVisibleChanged;

                // If element is already visible, do the OnAppear animation immediately:
                if (element.IsVisible)
                {
                    DoOnAppearAnimationIfAny(element);
                }
            }
        }

        private static void Element_Unloaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                // Output to the log for debugging:
                if (LogAnimationsForDebugging)
                {
                    LogForDebugging("Animations: unloaded event handled", element);
                }

                // Element is being removed from visual tree, stop listening for visibility changes
                element.IsVisibleChanged -= ElementToAnimate_IsVisibleChanged;
            }
        }

        private static void ElementToAnimate_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FrameworkElement elementToAnimate)
            {
                // Output to the log for debugging:
                if (LogAnimationsForDebugging)
                {
                    LogForDebugging("Animations: IsVisibleChanged", elementToAnimate);
                }

                // Check if the element has become visible or it has become hidden:
                if (elementToAnimate.IsVisible)
                {
                    DoOnAppearAnimationIfAny(elementToAnimate);
                }
            }
        }

        private static void DoOnAppearAnimationIfAny(FrameworkElement elementToAnimate)
        {
            // Output to the log for debugging:
            if (LogAnimationsForDebugging)
            {
                LogForDebugging("Animations: do the Appear animation (if any)", elementToAnimate);
            }

            IAnimationType animationType = GetOnAppear(elementToAnimate);
            if (animationType != null)
            {
                animationType.AnimateElementIn(elementToAnimate);
            }
        }

        private static void LogForDebugging(string actionDisplayName, FrameworkElement element)
        {
            string elementName = !string.IsNullOrEmpty(element.Name) ? "'" + element.Name + "' " : "";
            string instanceHashCode = element.GetHashCode().ToString();
            string textToDisplay = $"{actionDisplayName} for element {elementName}(#{instanceHashCode}) of type {element.GetType().Name} at timestamp '{DateTime.Now.ToString()}'.";

            // Display in the Console (useful when running in the browser, to see the log via the F12 developer tools of the browser):
            Console.WriteLine(textToDisplay);

            // Display in the Debug (useful when debugging with an IDE):
            System.Diagnostics.Debug.WriteLine(textToDisplay);
        }

        /// <summary>
        /// The factor by which to slow down the animations for debugging.
        /// Default is 1.0, which means that animations are not slowed down.
        /// For example, set ths property to 10.0 to slow down the animations
        /// 10 times.
        /// </summary>
        public static double SlowDownAnimationsForDebugging
        {
            get => StoryboardsHelper.SlowDownAnimationsForDebugging;
            set => StoryboardsHelper.SlowDownAnimationsForDebugging = value;
        }

        /// <summary>
        /// A boolean that indicates whether the animations framework shall
        /// print debug information to the output Console.
        /// </summary>
        public static bool LogAnimationsForDebugging { get; set; }

    }
}
