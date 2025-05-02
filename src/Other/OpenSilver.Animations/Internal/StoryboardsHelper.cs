
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

using OpenSilver.Samples.Showcase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;

namespace OpenSilver.Animations.Internal
{
    internal static class StoryboardsHelper
    {
        public static void AnimateElement(FrameworkElement elementToAnimate,
            int duration,
            int delay,
            double bounciness = 0.0,
            bool includeFade = false,
            bool includeScale = false)
        {
            if (elementToAnimate == null) return;

            // Slow down for debugging:
            if (SlowDownAnimationsForDebugging != 1.0)
            {
                duration = (int)(duration * SlowDownAnimationsForDebugging);
                delay = (int)(delay * SlowDownAnimationsForDebugging);
            }

            // Create storyboard
            var storyboard = new Storyboard();

            if (includeFade)
            {
                // Set initial opacity and scale
                elementToAnimate.Opacity = 0;

                // Opacity animation
                var opacityAnim = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromMilliseconds(duration),
                    BeginTime = TimeSpan.FromMilliseconds(delay),
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                Storyboard.SetTarget(opacityAnim, elementToAnimate);
                Storyboard.SetTargetProperty(opacityAnim, new PropertyPath(UIElement.OpacityProperty));
                storyboard.Children.Add(opacityAnim);
            }

            if (includeScale)
            {
                // Ensure RenderTransform is a ScaleTransform (for scaling)
                if (elementToAnimate.RenderTransform is not ScaleTransform)
                {
                    elementToAnimate.RenderTransform = new ScaleTransform()
                    {
                        ScaleX = 0.5,
                        ScaleY = 0.5
                    };
                    elementToAnimate.RenderTransformOrigin = new Point(0.5, 0.5); // center pivot
                }

                // Scale X animation
                var scaleXAnim = new DoubleAnimation
                {
                    From = 0.5,
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(duration),
                    BeginTime = TimeSpan.FromMilliseconds(delay),
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction =
                        bounciness > 0.0 ?
                        new BackEase { Amplitude = bounciness, EasingMode = EasingMode.EaseOut } :
                        new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                Storyboard.SetTarget(scaleXAnim, elementToAnimate);
                Storyboard.SetTargetProperty(scaleXAnim, new PropertyPath("RenderTransform.ScaleX"));
                storyboard.Children.Add(scaleXAnim);

                // Scale Y animation
                var scaleYAnim = new DoubleAnimation
                {
                    From = 0.5,
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(duration),
                    BeginTime = TimeSpan.FromMilliseconds(delay),
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction =
                        bounciness > 0.0 ?
                        new BackEase { Amplitude = bounciness, EasingMode = EasingMode.EaseOut } :
                        new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                Storyboard.SetTarget(scaleYAnim, elementToAnimate);
                Storyboard.SetTargetProperty(scaleYAnim, new PropertyPath("RenderTransform.ScaleY"));
                storyboard.Children.Add(scaleYAnim);
            }

            // If we're animating an "AnimationContentControl", we can animate its layout too:
            if (elementToAnimate is AnimatedContentControl animatedContentControl)
            {
                // Set initial size:
                animatedContentControl.WidthAsPercentageOfChild = 0.0;
                animatedContentControl.HeightAsPercentageOfChild = 0.0;

                // Animate Width:
                var widthAnim = new DoubleAnimation
                {
                    From = 0,
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(duration * 1.5), // We multiply by 1.5 for a nice effect where surrounding element moved with a small delay.
                    BeginTime = TimeSpan.FromMilliseconds(delay),
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction =
                        bounciness > 0.0 ?
                        new BackEase { Amplitude = bounciness, EasingMode = EasingMode.EaseOut } :
                        new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                Storyboard.SetTarget(widthAnim, elementToAnimate);
                Storyboard.SetTargetProperty(widthAnim, new PropertyPath("WidthAsPercentageOfChild"));
                storyboard.Children.Add(widthAnim);

                // Animate Height:
                var heightAnim = new DoubleAnimation
                {
                    From = 0,
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(duration * 1.5), // We multiply by 1.5 for a nice effect where surrounding element moved with a small delay.
                    BeginTime = TimeSpan.FromMilliseconds(delay),
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction =
                        bounciness > 0.0 ?
                        new BackEase { Amplitude = bounciness, EasingMode = EasingMode.EaseOut } :
                        new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                Storyboard.SetTarget(heightAnim, elementToAnimate);
                Storyboard.SetTargetProperty(heightAnim, new PropertyPath("HeightAsPercentageOfChild"));
                storyboard.Children.Add(heightAnim);
            }
            
            // Begin storyboard
            storyboard.Begin();
        }

        public static double SlowDownAnimationsForDebugging { get; set; } = 1.0;
    }
}
