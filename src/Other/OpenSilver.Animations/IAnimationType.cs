using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace OpenSilver.Animations
{
    [TypeConverter(typeof(AnimationTypeConverter))]
    public interface IAnimationType
    {
        void AnimateElementIn(FrameworkElement element);

        /// <summary>
        /// Delay in milliseconds
        /// </summary>
        int Delay { get; set; }

        /// <summary>
        /// Duration in milliseconds
        /// </summary>
        int Duration { get; set; }
    }
}
