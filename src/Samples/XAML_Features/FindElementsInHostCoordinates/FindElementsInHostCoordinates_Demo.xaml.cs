using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if SLMIGRATION
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
#else
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
#endif

namespace OpenSilver.Samples.Showcase
{
    public partial class FindElementsInHostCoordinates_Demo : UserControl
    {
        private int _highestZIndex;

        public FindElementsInHostCoordinates_Demo()
        {
            this.InitializeComponent();

            InitAllZIndex();
            _highestZIndex = 2;

#if SLMIGRATION
            this.MouseLeftButtonDown += FindElementsInHostCoordinates_Demo_PointerPressed;
#else
            this.PointerPressed += FindElementsInHostCoordinates_Demo_PointerPressed; 
#endif
        }

#if SLMIGRATION
        void FindElementsInHostCoordinates_Demo_PointerPressed(object sender, MouseButtonEventArgs e)
        {
            // Get the absolute coordinates of the pointer:
            Point currentPoint = e.GetPosition(null);

#else
        void FindElementsInHostCoordinates_Demo_PointerPressed(object sender, PointerRoutedEventArgs e) 
        {
            // Get the absolute coordinates of the pointer:
            Point currentPoint = e.GetCurrentPoint(null).Position;
#endif

            // Find the element that is under the pointer:
            var uiElement = VisualTreeHelper.FindElementsInHostCoordinates(currentPoint, CanvasParent).FirstOrDefault();

            // Bring the clicked element to the front:
            if (uiElement is Border)
            {
                _highestZIndex++;
                Canvas.SetZIndex(uiElement, _highestZIndex);
            }
        }
        
        void InitAllZIndex()
        {
            // 0 -> 2 is from the background to the front
            Canvas.SetZIndex(BlueRectangle, 0);
            Canvas.SetZIndex(GreenRectangle, 1);
            Canvas.SetZIndex(YellowRectangle, 2);
        }

        private void ButtonViewSource_Click(object sender, RoutedEventArgs e)
        {
            ViewSourceButtonHelper.ViewSource(new List<ViewSourceButtonInfo>()
            {
                new ViewSourceButtonInfo()
                {
                    TabHeader = "FindElementsInHostCoordinates_Demo.xaml",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/FindElementsInHostCoordinates/FindElementsInHostCoordinates_Demo.xaml"
                },
                new ViewSourceButtonInfo()
                {
                    TabHeader = "FindElementsInHostCoordinates_Demo.xaml.cs",
                    FilePathOnGitHub = "github/OpenSilver/OpenSilver.Samples.Showcase/blob/master/src/Samples/XAML_Features/FindElementsInHostCoordinates/FindElementsInHostCoordinates_Demo.xaml.cs"
                }
            });
        }


    }
}
