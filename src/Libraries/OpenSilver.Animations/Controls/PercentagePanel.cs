
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
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Animations
{
    /// <summary>
    /// A custom Panel that contains a single child and reports a percentage of the child's desired size
    /// to the layout system during measurement, while always arranging the child at its full size.
    /// This allows the panel to shrink its reported size without affecting the rendered size of the child,
    /// enabling layout reflow effects such as progressive disappearance without scaling the child.
    /// </summary>
    public class PercentagePanel : Panel
    {
        public static readonly DependencyProperty WidthAsPercentageOfChildProperty =
            DependencyProperty.Register(
                nameof(WidthAsPercentageOfChild),
                typeof(double),
                typeof(PercentagePanel),
                new PropertyMetadata(1.0, OnLayoutPropertyChanged));

        public static readonly DependencyProperty HeightAsPercentageOfChildProperty =
            DependencyProperty.Register(
                nameof(HeightAsPercentageOfChild),
                typeof(double),
                typeof(PercentagePanel),
                new PropertyMetadata(1.0, OnLayoutPropertyChanged));

        /// <summary>
        /// Gets or sets the width this element reports to the layout system,
        /// expressed as a percentage (0 to 1) of its child's desired width.
        /// This does not affect the actual size of the child, which is always arranged at full size.
        /// </summary>
        public double WidthAsPercentageOfChild
        {
            get => (double)GetValue(WidthAsPercentageOfChildProperty);
            set => SetValue(WidthAsPercentageOfChildProperty, value);
        }

        /// <summary>
        /// Gets or sets the height this element reports to the layout system,
        /// expressed as a percentage (0 to 1) of its child's desired height.
        /// This does not affect the actual size of the child, which is always arranged at full size.
        /// </summary>
        public double HeightAsPercentageOfChild
        {
            get => (double)GetValue(HeightAsPercentageOfChildProperty);
            set => SetValue(HeightAsPercentageOfChildProperty, value);
        }

        private static void OnLayoutPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((PercentagePanel)d).InvalidateMeasure();

        protected override Size MeasureOverride(Size availableSize)
        {
            if (Children.Count == 0)
                return new Size(0, 0);

            var child = Children[0];
            child.Measure(availableSize);
            var desired = child.DesiredSize;

            // Report only the percentage of the child's desired size:
            double w = EnsurePositive(WidthAsPercentageOfChild) * desired.Width;
            double h = EnsurePositive(HeightAsPercentageOfChild) * desired.Height;
            return new Size(w, h);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count > 0)
            {
                var child = Children[0];
                var desired = child.DesiredSize;

                // When percent <= 1, we center‐overflow exactly as before:
                //    child size = max(finalSize, desired)
                // When percent  > 1, we center *inside*:
                //    child size = desired
                double arrangeW = (EnsurePositive(WidthAsPercentageOfChild) > 1.0)
                                    ? desired.Width
                                    : Math.Max(finalSize.Width, desired.Width);

                double arrangeH = (EnsurePositive(HeightAsPercentageOfChild) > 1.0)
                                    ? desired.Height
                                    : Math.Max(finalSize.Height, desired.Height);

                double offsetX = (finalSize.Width - arrangeW) / 2;
                double offsetY = (finalSize.Height - arrangeH) / 2;

                child.Arrange(new Rect(offsetX, offsetY, arrangeW, arrangeH));
            }

            // To our parent, we fill exactly finalSize:
            return finalSize;
        }

        /*
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count > 0)
            {
                var child = Children[0];
                var desired = child.DesiredSize;

                // Give the child whichever is larger:
                //  – the space the parent actually gave us (finalSize), or
                //  – the child's own DesiredSize.
                double arrangeW = Math.Max(finalSize.Width, desired.Width);
                double arrangeH = Math.Max(finalSize.Height, desired.Height);

                // Center it: if finalSize < arrangeSize these offsets will be negative
                double offsetX = (finalSize.Width - arrangeW) / 2;
                double offsetY = (finalSize.Height - arrangeH) / 2;

                child.Arrange(new Rect(offsetX, offsetY, arrangeW, arrangeH));
            }

            // To our parent, we fill exactly finalSize:
            return finalSize;
        }
        */

        private static double EnsurePositive(double v) => v < 0 ? 0 : v;
    }
}
