
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

namespace OpenSilver.Animations.Easing
{
    public class CubicIn : MarkupExtension, IEasingType
    {
        public IEasingFunction EasingFunction => new CubicEase() { EasingMode = EasingMode.EaseIn };

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
