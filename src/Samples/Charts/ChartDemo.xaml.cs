using System;
using System.Windows;
using System.Windows.Controls;

namespace OpenSilver.Samples.Showcase
{
    public partial class ChartDemo : ContentControl
    {
        private const int MinLargeWidth = 700;
        private const int CardMargin = 50;
        private const int BigCardWidth = 645;
        private const int BigCardHeight = 385;
        private const int SmallCardHeight = 300;

        private bool? _isWideScreen;
        private double _lastCardWidth;

        public ChartDemo()
        {
            InitializeComponent();
        }

        protected override void INTERNAL_OnAttachedToVisualTree()
        {
            var parent = Parent as FrameworkElement;
            UpdateAdaptiveSize(parent.ActualWidth);
            parent.SizeChanged += (_, e) => UpdateAdaptiveSize(e.NewSize.Width);

            base.INTERNAL_OnAttachedToVisualTree();
        }

        private void UpdateAdaptiveSize(double screenWidth)
        {
            if (screenWidth >= MinLargeWidth)
            {
                if (_isWideScreen != true)
                {
                    _isWideScreen = true;
                    UpdateCardSize(BigCardWidth, BigCardHeight);
                }
            }
            else
            {
                var cardWidth = Math.Max(screenWidth - CardMargin, CardMargin * 2);
                if (_isWideScreen != false || Math.Abs(_lastCardWidth - cardWidth) > CardMargin / 2)
                {
                    UpdateCardSize(cardWidth, SmallCardHeight);
                }
                _isWideScreen = false;
            }
        }

        private void UpdateCardSize(double cardWidth, double cardHeight)
        {
            _lastCardWidth = cardWidth;

            Width = cardWidth;
            Height = cardHeight;
        }
    }
}
