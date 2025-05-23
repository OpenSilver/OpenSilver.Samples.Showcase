
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
using System.Windows.Media.Animation;

namespace OpenSilver.Animations
{
    [TypeConverter(typeof(EasingTypeConverter))]
    public interface IEasingType
    {
        /// <summary>
        /// Easing function to be used for the animation.
        /// </summary>
        IEasingFunction EasingFunction { get; }
    }
}
