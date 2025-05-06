
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;

namespace OpenSilver.Animations.Internal
{
    internal static class StoryboardsHelper
    {
        public static void AnimateElement(FrameworkElement elementToAnimate,
            int duration,
            int delay,
            double bounciness = 0.0,
            Direction direction = Direction.DownToUp,
            bool includeFade = false,
            bool includeScale = false,
            bool includeSlide = false)
        {
            RunAfterDispatcherBeginInvokeIfTrue(
                IsActualElementMeasureNeeded(elementToAnimate, includeSlide, direction),
                elementToAnimate.Dispatcher,
                () =>
                {
                    if (IsActualElementMeasureNeeded(elementToAnimate, includeSlide, direction))
                    {
                        // We enter here if the element is not yet measured (it means that the Dispatched.BeginInvoke call was insufficient).
                        return;
                    }

                    // All good, we can animate the element now:
                    ApplyAnimateElement(
                            elementToAnimate: elementToAnimate,
                            duration: duration,
                            delay: delay,
                            bounciness: bounciness,
                            direction: direction,
                            includeFade: includeFade,
                            includeScale: includeScale,
                            includeSlide: includeSlide);
                });
        }

        public static void ApplyAnimateElement(FrameworkElement elementToAnimate,
            int duration,
            int delay,
            double bounciness = 0.0,
            Direction direction = Direction.DownToUp,
            bool includeFade = false,
            bool includeScale = false,
            bool includeSlide = false)
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

            //-------------------------------------------------
            // FADE ANIMATION
            //-------------------------------------------------

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

            //-------------------------------------------------
            // SCALE ANIMATION
            //-------------------------------------------------

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

            //-------------------------------------------------
            // SLIDE ANIMATION
            //-------------------------------------------------

            if (includeSlide)
            {
                // Ensure RenderTransform is a TranslateTransform (for sliding)
                if (elementToAnimate.RenderTransform is not TranslateTransform)
                {
                    elementToAnimate.RenderTransform = new TranslateTransform();
                }

                if (direction == Direction.LeftToRight || direction == Direction.RightToLeft)
                {
                    double actualWidth = elementToAnimate.ActualWidth;

                    if (!double.IsNaN(actualWidth) && actualWidth != 0.0)
                    {
                        // Translate X animation
                        var translateXAnim = new DoubleAnimation
                        {
                            From = (direction == Direction.LeftToRight ? -actualWidth / 2 : actualWidth / 2),
                            To = 0.0,
                            Duration = TimeSpan.FromMilliseconds(duration),
                            BeginTime = TimeSpan.FromMilliseconds(delay),
                            FillBehavior = FillBehavior.HoldEnd,
                            EasingFunction =
                                bounciness > 0.0 ?
                                new BackEase { Amplitude = bounciness, EasingMode = EasingMode.EaseOut } :
                                new CubicEase { EasingMode = EasingMode.EaseOut }
                        };
                        Storyboard.SetTarget(translateXAnim, elementToAnimate);
                        Storyboard.SetTargetProperty(translateXAnim, new PropertyPath("RenderTransform.X"));
                        storyboard.Children.Add(translateXAnim);
                    }
                }

                if (direction == Direction.DownToUp || direction == Direction.UpToDown)
                {
                    double actualHeight = elementToAnimate.ActualHeight;

                    if (!double.IsNaN(actualHeight) && actualHeight != 0.0)
                    {
                        // Translate Y animation
                        var translateYAnim = new DoubleAnimation
                        {
                            From = (direction == Direction.UpToDown ? -actualHeight / 2 : actualHeight / 2),
                            To = 0.0,
                            Duration = TimeSpan.FromMilliseconds(duration),
                            BeginTime = TimeSpan.FromMilliseconds(delay),
                            FillBehavior = FillBehavior.HoldEnd,
                            EasingFunction =
                                bounciness > 0.0 ?
                                new BackEase { Amplitude = bounciness, EasingMode = EasingMode.EaseOut } :
                                new CubicEase { EasingMode = EasingMode.EaseOut }
                        };
                        Storyboard.SetTarget(translateYAnim, elementToAnimate);
                        Storyboard.SetTargetProperty(translateYAnim, new PropertyPath("RenderTransform.Y"));
                        storyboard.Children.Add(translateYAnim);
                    }
                }
            }

            //-------------------------------------------------
            // LAYOUT ANIMATION
            //-------------------------------------------------

            // If we're animating an "AnimationContentControl", we can animate its layout too:
            if (elementToAnimate is AnimatedContentControl animatedContentControl)
            {
                // Set initial size:
                animatedContentControl.WidthAsPercentageOfChild = 0.0;
                animatedContentControl.HeightAsPercentageOfChild = 0.0;

                // Determine whether to apply "bounciness" on the layout animation (note: when using the Slide animation, the "bounciness" should only be applied in the direction of the Slide):
                bool applyBouncinessOnWidthAnimation = (bounciness > 0.0 && !(includeSlide && (direction == Direction.UpToDown || direction == Direction.DownToUp)));
                bool applyBouncinessOnHeight = (bounciness > 0.0 && !(includeSlide && (direction == Direction.LeftToRight || direction == Direction.RightToLeft)));

                // Animate Width:
                var widthAnim = new DoubleAnimation
                {
                    From = 0,
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(duration * 1.5), // We multiply by 1.5 for a nice effect where surrounding element moved with a small delay.
                    BeginTime = TimeSpan.FromMilliseconds(delay),
                    FillBehavior = FillBehavior.HoldEnd,
                    EasingFunction =
                        applyBouncinessOnWidthAnimation ?
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
                        applyBouncinessOnHeight ?
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

        private static bool IsActualElementMeasureNeeded(FrameworkElement elementToAnimate, bool includeSlide, Direction direction)
        {
            if (includeSlide)
            {
                // Check if the element is not already measured
                if (direction == Direction.LeftToRight || direction == Direction.RightToLeft)
                {
                    return elementToAnimate.ActualWidth == 0.0;
                }
                else if (direction == Direction.DownToUp || direction == Direction.UpToDown)
                {
                    return elementToAnimate.ActualHeight == 0.0;
                }
            }
            return false;
        }

        private static void RunAfterDispatcherBeginInvokeIfTrue(bool value, Dispatcher dispatcher, Action action)
        {
            if (value)
            {
                dispatcher.BeginInvoke(() =>
                {
                    action();
                });
            }
            else
            {
                action();
            }
        }

        public static double SlowDownAnimationsForDebugging { get; set; } = 1.0;
    }
}
