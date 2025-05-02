using OpenSilver.Samples.Showcase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media;
using OpenSilver.Animations.Internal;

namespace OpenSilver.Animations
{
    public class FadeAndScale : MarkupExtension, IAnimationType
    {
        /// <summary>
        /// Delay in milliseconds.
        /// Default is 0.
        /// </summary>
        public int Delay { get; set; } = 0;

        /// <summary>
        /// Duration in milliseconds.
        /// Default is 250.
        /// </summary>
        public int Duration { get; set; } = 250;

        /// <summary>
        /// Determines the intensity of the elastic effect.
        /// Higher values result in greater overshoot and oscillation,
        /// simulating a more dynamic and spring-like motion.
        /// Default is 0.0 (no bouncing).
        /// </summary>
        public double Bounciness { get; set; } = 0.0;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public void AnimateElementIn(FrameworkElement elementToAnimate)
        {
            StoryboardsHelper.AnimateElement(
                elementToAnimate: elementToAnimate,
                duration: Duration,
                delay: Delay,
                bounciness: Bounciness,
                includeFade: true,
                includeScale: true);
        }
    }
}
