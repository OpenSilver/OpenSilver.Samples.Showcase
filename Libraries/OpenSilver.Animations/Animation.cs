
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

            if (d is FrameworkElement elementToAnimate
                && e.NewValue is IAnimationType animationType)
            {
                // We unregister before registering so that, if this method is called multiple times, we don't end up registering multiple IsVisibleChanged events:
                ((FrameworkElement)d).IsVisibleChanged -= ElementToAnimate_IsVisibleChanged; // Note: this is safely ignored if there was no previous event registration.
                ((FrameworkElement)d).IsVisibleChanged += ElementToAnimate_IsVisibleChanged;
            }
        }

        private static void ElementToAnimate_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //DebugAnimations(sender)

            FrameworkElement elementToAnimate = sender as FrameworkElement;
            IAnimationType animationType = GetOnAppear(elementToAnimate);

            // Unregister the event:
            //elementToAnimate.IsVisibleChanged -= ElementToAnimate_IsVisibleChanged;

            if (animationType != null && elementToAnimate.IsVisible)
            {
                animationType.AnimateElementIn(elementToAnimate);
            }
        }

        //private string DebugAnimations(object obj)
        //{
        //    string text = obj != null ? obj.GetType().Name + " loaded!" : "null" + " " + DateTime.Now.ToString();
        //    Console.WriteLine(text);
        //    MessageBox.Show(text);
        //}

        public static double SlowDownAnimationsForDebugging
        {
            get => StoryboardsHelper.SlowDownAnimationsForDebugging;
            set => StoryboardsHelper.SlowDownAnimationsForDebugging = value;
        }
    }
}
