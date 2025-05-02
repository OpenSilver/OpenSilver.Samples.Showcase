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
                // We unregister before registering so that, if this method is called multiple times, we don't end up registering multiple Loaded events:
                ((FrameworkElement)d).Loaded -= ElementToAnimate_Loaded; // Note: this is safely ignored if there was no previous event registration.
                ((FrameworkElement)d).Loaded += ElementToAnimate_Loaded;
            }
        }

        private static void ElementToAnimate_Loaded(object sender, RoutedEventArgs e)
        {
            //DebugAnimations(sender)

            FrameworkElement elementToAnimate = sender as FrameworkElement;
            IAnimationType animationType = GetOnAppear(elementToAnimate);

            // Unregister the event:
            elementToAnimate.Loaded -= ElementToAnimate_Loaded;

            if (animationType != null)
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
