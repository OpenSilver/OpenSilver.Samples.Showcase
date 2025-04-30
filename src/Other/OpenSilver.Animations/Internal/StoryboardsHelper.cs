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
            bool includeFade = false,
            bool includeScale = false)
        {
            if (elementToAnimate == null) return;

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
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
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
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
                };
                Storyboard.SetTarget(scaleYAnim, elementToAnimate);
                Storyboard.SetTargetProperty(scaleYAnim, new PropertyPath("RenderTransform.ScaleY"));
                storyboard.Children.Add(scaleYAnim);
            }
            
            // Begin storyboard
            storyboard.Begin();
        }
    }
}
