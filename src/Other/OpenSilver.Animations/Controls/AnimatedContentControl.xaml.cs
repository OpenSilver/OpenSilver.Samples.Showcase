
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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Animations
{
    public partial class AnimatedContentControl : ContentControl
    {
        public AnimatedContentControl()
        {
            this.InitializeComponent();
        }

        #region Size-related dependency properties (See description of "PercentagePanel")

        public double WidthAsPercentageOfChild
        {
            get => (double)GetValue(WidthAsPercentageOfChildProperty);
            set => SetValue(WidthAsPercentageOfChildProperty, value);
        }
        public static readonly DependencyProperty WidthAsPercentageOfChildProperty =
            DependencyProperty.Register(
                nameof(WidthAsPercentageOfChild),
                typeof(double),
                typeof(AnimatedContentControl),
                new PropertyMetadata(1.0),
                value => (double)value >= 0.0);

        public double HeightAsPercentageOfChild
        {
            get => (double)GetValue(HeightAsPercentageOfChildProperty);
            set => SetValue(HeightAsPercentageOfChildProperty, value);
        }
        public static readonly DependencyProperty HeightAsPercentageOfChildProperty =
            DependencyProperty.Register(
                nameof(HeightAsPercentageOfChild),
                typeof(double),
                typeof(AnimatedContentControl),
                new PropertyMetadata(1.0),
                value => (double)value >= 0.0);

        #endregion
    }
}
