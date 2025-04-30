using OpenSilver.Samples.Showcase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace OpenSilver.Animations
{
    public class FadeAndScale : MarkupExtension, IAnimationType
    {
        /// <summary>
        /// Delay in milliseconds
        /// </summary>
        public int Delay { get; set; } = 0;

        /// <summary>
        /// Duration in milliseconds
        /// </summary>
        public int Duration { get; set; } = 167;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public void AnimateElementIn(FrameworkElement element)
        {
            if (element == null) return;

            // Set initial opacity and scale
            element.Opacity = 0;

            // Ensure RenderTransform is a ScaleTransform (for scaling)
            if (element.RenderTransform is not ScaleTransform)
            {
                element.RenderTransform = new ScaleTransform()
                {
                    ScaleX = 0.5,
                    ScaleY = 0.5
                };
                element.RenderTransformOrigin = new Point(0.5, 0.5); // center pivot
            }

            // Create storyboard
            var storyboard = new Storyboard();

            // Opacity animation
            var opacityAnim = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(Duration),
                BeginTime = TimeSpan.FromMilliseconds(Delay),
                FillBehavior = FillBehavior.HoldEnd,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(opacityAnim, element);
            Storyboard.SetTargetProperty(opacityAnim, new PropertyPath(UIElement.OpacityProperty));
            storyboard.Children.Add(opacityAnim);

            // Scale X animation
            var scaleXAnim = new DoubleAnimation
            {
                From = 0.5,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(Duration),
                BeginTime = TimeSpan.FromMilliseconds(Delay),
                FillBehavior = FillBehavior.HoldEnd,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(scaleXAnim, element);
            Storyboard.SetTargetProperty(scaleXAnim, new PropertyPath("RenderTransform.ScaleX"));
            storyboard.Children.Add(scaleXAnim);

            // Scale Y animation
            var scaleYAnim = new DoubleAnimation
            {
                From = 0.5,
                To = 1.0,
                Duration = TimeSpan.FromMilliseconds(Duration),
                BeginTime = TimeSpan.FromMilliseconds(Delay),
                FillBehavior = FillBehavior.HoldEnd,
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };
            Storyboard.SetTarget(scaleYAnim, element);
            Storyboard.SetTargetProperty(scaleYAnim, new PropertyPath("RenderTransform.ScaleY"));
            storyboard.Children.Add(scaleYAnim);

            // Begin storyboard
            storyboard.Begin();
        }
    }
}
