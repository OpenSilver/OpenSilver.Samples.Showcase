using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    /// <summary>
    /// A responsive <see cref="Panel"/> that dynamically switches between layouts based on its width:
    /// when its width meets or exceeds <c>NoColumnsBelowWidth</c>, children are arranged into equal-width columns (one per visible child);
    /// otherwise, they are stacked vertically in a single column.
    /// Child horizontal and vertical alignments are respected within each allocated slot.
    /// </summary>
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

        /// <summary>
        /// When the available width is ≤ this threshold, children stack vertically.
        /// When > this threshold, children lay out in N equal-width columns (N = # of children).
        /// </summary>
        public double NoColumnsBelowWidth
        {
            get => (double)GetValue(NoColumnsBelowWidthProperty);
            set => SetValue(NoColumnsBelowWidthProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double layoutWidth = double.IsNaN(this.Width)
                                 ? availableSize.Width
                                 : this.Width;
            bool hasFinite = !double.IsInfinity(layoutWidth);

            // collect only visible children
            var children = Children.Cast<UIElement>()
                                .Where(c => c.Visibility != Visibility.Collapsed
                                && c is FrameworkElement)
                                .Cast<FrameworkElement>()
                                .ToList();
            int count = children.Count;

            // Decide mode using our fixed layoutWidth
            bool useColumns = hasFinite
                              && layoutWidth > NoColumnsBelowWidth
                              && count > 0;

            if (!useColumns)
            {
                // — vertical stack —
                double desiredW = 0, desiredH = 0;
                foreach (UIElement child in children)
                {
                    // each child can be up to "layoutWidth" wide, infinite height
                    child.Measure(new Size(layoutWidth, double.PositiveInfinity));
                    var fe = child as FrameworkElement;
                    double mW = (fe?.Margin.Left ?? 0) + (fe?.Margin.Right ?? 0);
                    double mH = (fe?.Margin.Top ?? 0) + (fe?.Margin.Bottom ?? 0);
                    desiredW = Math.Max(desiredW, child.DesiredSize.Width + mW);
                    desiredH += child.DesiredSize.Height + mH;
                }
                return new Size(desiredW, desiredH);
            }
            else
            {
                // — N equal‐width columns —
                double colW = layoutWidth / count;
                double maxChildH = 0;
                foreach (UIElement child in children)
                {
                    child.Measure(new Size(colW, double.PositiveInfinity));
                    var fe = child as FrameworkElement;
                    double mH = (fe?.Margin.Top ?? 0) + (fe?.Margin.Bottom ?? 0);
                    maxChildH = Math.Max(maxChildH, child.DesiredSize.Height + mH);
                }
                return new Size(layoutWidth, maxChildH);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double layoutWidth = finalSize.Width;
            bool hasFinite = !double.IsInfinity(layoutWidth);

            var children = Children.Cast<UIElement>()
                                .Where(c => c.Visibility != Visibility.Collapsed
                                && c is FrameworkElement)
                                .Cast<FrameworkElement>()
                                .ToList();
            int count = children.Count;

            // Use the same condition as in MeasureOverride
            bool useColumns = hasFinite
                              && layoutWidth > NoColumnsBelowWidth
                              && count > 0;

            if (!useColumns)
            {
                // vertical stack
                double y = 0;
                foreach (var child in children)
                {
                    var desired = child.DesiredSize;
                    double marginLeft = child.Margin.Left;
                    double marginTop = child.Margin.Top;
                    double marginRight = child.Margin.Right;
                    double marginBottom = child.Margin.Bottom;

                    double availableWidth = finalSize.Width - marginLeft - marginRight;
                    double w = (child.HorizontalAlignment == HorizontalAlignment.Stretch)
                               ? availableWidth
                               : desired.Width;
                    double x = AlignOffset(availableWidth, w, child.HorizontalAlignment) + marginLeft;

                    Rect rect = new Rect(x, y + marginTop, w, desired.Height);
                    child.Arrange(rect);

                    y += desired.Height + marginTop + marginBottom;
                }

                return finalSize;
            }
            else
            {
                // Compute the actual height we need (same as in MeasureOverride)
                double maxChildH = 0;
                foreach (var child in children)
                {
                    double mH = child.Margin.Top + child.Margin.Bottom;
                    maxChildH = Math.Max(maxChildH, child.DesiredSize.Height + mH);
                }

                // Use our calculated height rather than the finalSize.Height
                Size actualSize = new Size(finalSize.Width, maxChildH);

                // columns
                double colW = finalSize.Width / count;
                for (int i = 0; i < count; i++)
                {
                    var child = children[i];
                    var desired = child.DesiredSize;

                    double marginLeft = child.Margin.Left;
                    double marginTop = child.Margin.Top;
                    double marginRight = child.Margin.Right;

                    double availableWidth = colW - marginLeft - marginRight;

                    double w = (child.HorizontalAlignment == HorizontalAlignment.Stretch)
                               ? availableWidth
                               : Math.Min(desired.Width, availableWidth);

                    double x = i * colW + marginLeft + AlignOffset(availableWidth, w, child.HorizontalAlignment);
                    double y = marginTop;

                    if (child.VerticalAlignment != VerticalAlignment.Top)
                    {
                        // Only do vertical alignment if it's not Top-aligned
                        double h = desired.Height;
                        y = marginTop + AlignOffset(maxChildH - marginTop - child.Margin.Bottom, h, child.VerticalAlignment);
                    }

                    Rect rect = new Rect(x, y, w, desired.Height);
                    child.Arrange(rect);
                }

                // Return our calculated size instead of finalSize
                return actualSize;
            }
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
