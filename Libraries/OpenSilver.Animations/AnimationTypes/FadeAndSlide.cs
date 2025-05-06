
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
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media;
using OpenSilver.Animations.Internal;

namespace OpenSilver.Animations
{
    public class FadeAndSlide : MarkupExtension, IAnimationType
    {
        /// <summary>
        /// Delay in milliseconds
        /// </summary>
        public int Delay { get; set; } = 0;

        /// <summary>
        /// Duration in milliseconds
        /// </summary>
        public int Duration { get; set; } = 250;

        /// <summary>
        /// Direction of the slide animation
        /// </summary>
        public Direction Direction { get; set; } = Direction.DownToUp;

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
                direction: Direction,
                bounciness: Bounciness,
                includeFade: true,
                includeSlide: true);
        }
    }
}
