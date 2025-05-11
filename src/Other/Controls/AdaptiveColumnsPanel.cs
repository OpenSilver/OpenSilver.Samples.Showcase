using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public class AdaptiveColumnsPanel : Panel
    {
        /// <summary>
        /// When the available width is ≤ this threshold, children stack vertically.
        /// When > this threshold, children lay out in N equal-width columns (N = # of children).
        /// </summary>
        public static readonly DependencyProperty NoColumnsBelowWidthProperty =
            DependencyProperty.Register(
                nameof(NoColumnsBelowWidth),
                typeof(double),
                typeof(AdaptiveColumnsPanel),
                new FrameworkPropertyMetadata(
                    500d,
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double NoColumnsBelowWidth
        {
            get => (double)GetValue(NoColumnsBelowWidthProperty);
            set => SetValue(NoColumnsBelowWidthProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            // collect only visible children
            var children = Children.Cast<UIElement>()
                                .Where(c => c.Visibility != Visibility.Collapsed
                                && c is FrameworkElement)
                                .Cast<FrameworkElement>()
                                .ToList();
            int count = children.Count;

            // decide mode: vertical stack if infinite or ≤ NoColumnsBelowWidth, else columns
            bool useColumns =
                !double.IsInfinity(availableSize.Width)
                && availableSize.Width > NoColumnsBelowWidth
                && count > 0;

            if (!useColumns)
            {
                // vertical stacking
                double width = 0, height = 0;
                foreach (var child in children)
                {
                    // let child be as wide as we're given, but infinite height
                    child.Measure(new Size(availableSize.Width, double.PositiveInfinity));
                    var d = child.DesiredSize;
                    width = Math.Max(width, d.Width);
                    height += d.Height;
                }
                return new Size(width, height);
            }
            else
            {
                // N-column layout
                double columnWidth = availableSize.Width / count;
                double maxHeight = 0;
                foreach (var child in children)
                {
                    // each child gets columnWidth, infinite height
                    child.Measure(new Size(columnWidth, double.PositiveInfinity));
                    maxHeight = Math.Max(maxHeight, child.DesiredSize.Height);
                }
                return new Size(availableSize.Width, maxHeight);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var children = Children.Cast<UIElement>()
                                .Where(c => c.Visibility != Visibility.Collapsed
                                && c is FrameworkElement)
                                .Cast<FrameworkElement>()
                                .ToList();
            int count = children.Count;

            bool useColumns =
                !double.IsInfinity(finalSize.Width)
                && finalSize.Width > NoColumnsBelowWidth
                && count > 0;

            if (!useColumns)
            {
                // vertical stack
                double y = 0;
                foreach (var child in children)
                {
                    var desired = child.DesiredSize;
                    double w = (child.HorizontalAlignment == HorizontalAlignment.Stretch)
                               ? finalSize.Width
                               : desired.Width;
                    double x = AlignOffset(finalSize.Width, w, child.HorizontalAlignment);
                    Rect rect = new Rect(x, y, w, desired.Height);
                    child.Arrange(rect);
                    y += desired.Height;
                }
            }
            else
            {
                // columns
                double colW = finalSize.Width / count;
                for (int i = 0; i < count; i++)
                {
                    var child = children[i];
                    var desired = child.DesiredSize;
                    double h = (child.VerticalAlignment == VerticalAlignment.Stretch)
                               ? finalSize.Height
                               : desired.Height;
                    double y = AlignOffset(finalSize.Height, h, child.VerticalAlignment);
                    Rect rect = new Rect(i * colW, y, colW, h);
                    child.Arrange(rect);
                }
            }

            return finalSize;
        }

        private double AlignOffset(double container, double element, HorizontalAlignment align)
        {
            switch (align)
            {
                case HorizontalAlignment.Center: return (container - element) / 2;
                case HorizontalAlignment.Right: return container - element;
                case HorizontalAlignment.Stretch: return 0;
                case HorizontalAlignment.Left:
                default: return 0;
            }
        }

        private double AlignOffset(double container, double element, VerticalAlignment align)
        {
            switch (align)
            {
                case VerticalAlignment.Center: return (container - element) / 2;
                case VerticalAlignment.Bottom: return container - element;
                case VerticalAlignment.Stretch: return 0;
                case VerticalAlignment.Top:
                default: return 0;
            }
        }
    }
}
