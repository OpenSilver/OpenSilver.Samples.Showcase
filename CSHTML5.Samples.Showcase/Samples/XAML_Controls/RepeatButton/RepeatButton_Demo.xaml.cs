using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace CSHTML5.Samples.Showcase
{
    public partial class RepeatButton_Demo : UserControl
    {
        public RepeatButton_Demo()
        {
            this.InitializeComponent();
        }

        void ButtonTranslate_Click(object sender, RoutedEventArgs e)
        {
            if (TestTransformBorder.RenderTransform == null || !(TestTransformBorder.RenderTransform is TranslateTransform))
            {
                TranslateTransform translateTransform = new TranslateTransform();
                TestTransformBorder.RenderTransform = translateTransform;
            }
            ((TranslateTransform)TestTransformBorder.RenderTransform).X += 10;
            ((TranslateTransform)TestTransformBorder.RenderTransform).Y += 10;
        }

        void ButtonRotate_Click(object sender, RoutedEventArgs e)
        {
            if (TestTransformBorder.RenderTransform == null || !(TestTransformBorder.RenderTransform is RotateTransform))
            {
                RotateTransform rotateTransform = new RotateTransform();
                TestTransformBorder.RenderTransform = rotateTransform;
            }
            ((RotateTransform)TestTransformBorder.RenderTransform).Angle += 10;
        }

        void TransformButton_Click(object sender, RoutedEventArgs e)
        {
            Random r = new Random();

            SolidColorBrush brush = new SolidColorBrush();

            brush.Color = Color.FromArgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255));
            TestTransformBorder.Background = brush;
        }
    }
}
