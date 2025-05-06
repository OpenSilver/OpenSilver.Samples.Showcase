
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
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace OpenSilver.Animations
{
    [TypeConverter(typeof(AnimationTypeConverter))]
    public interface IAnimationType
    {
        void AnimateElementIn(FrameworkElement elementToAnimate);

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
