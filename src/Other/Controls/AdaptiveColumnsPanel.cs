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

        // Get visible children only once and as FrameworkElement directly
        private List<FrameworkElement> GetVisibleChildren() =>
            Children.OfType<FrameworkElement>()
                    .Where(c => c.Visibility != Visibility.Collapsed)
                    .ToList();

        // Determine layout mode in one place
        private bool ShouldUseColumns(double availableWidth, int childCount) =>
            !double.IsInfinity(availableWidth) &&
            availableWidth > NoColumnsBelowWidth &&
            childCount > 0;

        protected override Size MeasureOverride(Size availableSize)
        {
            double layoutWidth = double.IsNaN(this.Width) ? availableSize.Width : this.Width;
            var children = GetVisibleChildren();
            int count = children.Count;

            bool useColumns = ShouldUseColumns(layoutWidth, count);

            if (!useColumns)
            {
                // Vertical stack mode
                double desiredW = 0, desiredH = 0;
                foreach (var child in children)
                {
                    child.Measure(new Size(layoutWidth, double.PositiveInfinity));
                    double mW = child.Margin.Left + child.Margin.Right;
                    double mH = child.Margin.Top + child.Margin.Bottom;
                    desiredW = Math.Max(desiredW, child.DesiredSize.Width + mW);
                    desiredH += child.DesiredSize.Height + mH;
                }
                return new Size(desiredW, desiredH);
            }
            else
            {
                // Column mode
                double colW = layoutWidth / count;
                double maxChildH = 0;
                foreach (var child in children)
                {
                    child.Measure(new Size(colW, double.PositiveInfinity));
                    double mH = child.Margin.Top + child.Margin.Bottom;
                    maxChildH = Math.Max(maxChildH, child.DesiredSize.Height + mH);
                }
                return new Size(layoutWidth, maxChildH);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var children = GetVisibleChildren();
            int count = children.Count;
            bool useColumns = ShouldUseColumns(finalSize.Width, count);

            if (!useColumns)
            {
                // Vertical stack mode
                double y = 0;
                foreach (var child in children)
                {
                    // Account for margins
                    double marginLeft = child.Margin.Left;
                    double marginRight = child.Margin.Right;
                    double marginTop = child.Margin.Top;
                    double marginBottom = child.Margin.Bottom;

                    // Calculate available width for this child
                    double availableWidth = finalSize.Width - marginLeft - marginRight;

                    // Determine width based on alignment
                    double width = (child.HorizontalAlignment == HorizontalAlignment.Stretch)
                                  ? availableWidth
                                  : Math.Min(child.DesiredSize.Width, availableWidth);

                    // Calculate x position with alignment
                    double x = marginLeft + GetHorizontalAlignmentOffset(availableWidth, width, child.HorizontalAlignment);

                    // Arrange the child
                    child.Arrange(new Rect(x, y + marginTop, width, child.DesiredSize.Height));

                    // Move to next vertical position
                    y += child.DesiredSize.Height + marginTop + marginBottom;
                }
            }
            else
            {
                // Column mode - calculate actual height needed
                double maxChildH = 0;
                foreach (var child in children)
                {
                    double mH = child.Margin.Top + child.Margin.Bottom;
                    maxChildH = Math.Max(maxChildH, child.DesiredSize.Height + mH);
                }

                // Column width
                double colW = finalSize.Width / count;

                for (int i = 0; i < count; i++)
                {
                    var child = children[i];
                    double marginLeft = child.Margin.Left;
                    double marginRight = child.Margin.Right;
                    double marginTop = child.Margin.Top;
                    double marginBottom = child.Margin.Bottom;

                    // Available width for this column
                    double availableWidth = colW - marginLeft - marginRight;

                    // Determine width based on alignment
                    double width = (child.HorizontalAlignment == HorizontalAlignment.Stretch)
                                  ? availableWidth
                                  : Math.Min(child.DesiredSize.Width, availableWidth);

                    // Calculate horizontal position
                    double x = (i * colW) + marginLeft +
                               GetHorizontalAlignmentOffset(availableWidth, width, child.HorizontalAlignment);

                    // Calculate height based on alignment
                    double height = (child.VerticalAlignment == VerticalAlignment.Stretch)
                                   ? maxChildH - marginTop - marginBottom
                                   : child.DesiredSize.Height;

                    // Calculate vertical position
                    double availableHeight = maxChildH - marginTop - marginBottom;
                    double y = marginTop + GetVerticalAlignmentOffset(availableHeight, height, child.VerticalAlignment);

                    // Arrange the child
                    child.Arrange(new Rect(x, y, width, height));
                }

                // Return the correct size
                return new Size(finalSize.Width, maxChildH);
            }

            return finalSize;
        }

        private double GetHorizontalAlignmentOffset(double container, double element, HorizontalAlignment align)
        {
            return align switch
            {
                HorizontalAlignment.Center => (container - element) / 2,
                HorizontalAlignment.Right => container - element,
                _ => 0 // Left or Stretch
            };
        }

        private double GetVerticalAlignmentOffset(double container, double element, VerticalAlignment align)
        {
            return align switch
            {
                VerticalAlignment.Center => (container - element) / 2,
                VerticalAlignment.Bottom => container - element,
                _ => 0 // Top or Stretch
            };
        }
    }
}